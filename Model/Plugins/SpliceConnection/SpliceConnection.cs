using System;
using System.Windows.Forms;
using Tekla.Structures;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Plugins;
using Tekla.Structures.Model;
using TSMUI = Tekla.Structures.Model.UI;

namespace SpliceConn
{
    // If the plug-in is inherited from ConnectionPluginBase, the dialog class name must be the same as plug-in name
    [Plugin("SpliceConnection")] //Name of the connection in the catalog
    [PluginUserInterface("SpliceConnectionForm")]
    [SecondaryType(ConnectionBase.SecondaryType.SECONDARYTYPE_ONE)]
    [AutoDirectionType(AutoDirectionTypeEnum.AUTODIR_BASIC)]
    [PositionType(PositionTypeEnum.COLLISION_PLANE)]
    public class SpliceConnection : ConnectionBase
    {
        #region Fields
        private readonly Model _model;
        private readonly StructuresData _data;
        private const double GAP = 10.0;
        private const double EPSILON = 0.001;
        private double _plateLength;
        private string _boltStandard = string.Empty;
        private int _upDirection = new int();
        private double _rotationAngleY;
        private double _rotationAngleX;
        private int _locked = new int();
        private int _class = new int();
        private string _connectionCode = string.Empty;
        private string _autoDefaults = string.Empty;
        private string _autoConnection = string.Empty;
        #endregion

        #region Constructor
        public SpliceConnection(StructuresData data)
        {
            _model = new Model();
            _data = data;
        }
        #endregion

        #region Overriden methods
        public override bool Run()
        {
            try
            {
                GetValuesFromDialog();


                //for debugging as windows forms app
                var objects = new TSMUI.ModelObjectSelector().GetSelectedObjects();

                objects.MoveNext();
                var primary = objects.Current as Beam;
                objects.MoveNext();
                var secondary = objects.Current as Beam;
                var primaryBeam = primary;
                var secondaryBeam = secondary;


                //for connection plugin
                //Get primary and secondary
                //if (Primary == null || Secondaries.Count < 1) return false;
                //Beam PrimaryBeam = _model.SelectModelObject(Primary) as Beam;
                //Beam SecondaryBeam = _model.SelectModelObject(Secondaries[0]) as Beam;

                if (primaryBeam == null || secondaryBeam == null) return false;

                var primaryProfileType = "";
                var secondaryProfileType = "";

                primaryBeam.GetReportProperty("PROFILE_TYPE", ref primaryProfileType);
                secondaryBeam.GetReportProperty("PROFILE_TYPE", ref secondaryProfileType);

                if (primaryBeam.Profile.ProfileString != secondaryBeam.Profile.ProfileString ||
                    primaryProfileType != secondaryProfileType) return false;

                if (!CheckIfBeamsAreAligned(primaryBeam, secondaryBeam)) return false;

                return CreateSpliceConnection(primaryBeam, secondaryBeam, _model, _plateLength, _boltStandard);
            }

            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
                return false;
            }

            finally
            {
                _model.CommitChanges();
            }
        }
        #endregion

        #region Private methods
        private static bool CheckIfBeamsAreAligned(Beam primaryBeam, Beam secondaryBeam)
        {
            if (primaryBeam == null || secondaryBeam == null) return false;

            var primaryLine = new Line(primaryBeam.StartPoint, primaryBeam.EndPoint);
            var secondaryLine = new Line(secondaryBeam.StartPoint, secondaryBeam.EndPoint);

            return Parallel.LineToLine(primaryLine, secondaryLine);
        }

        private static bool CreateSpliceConnection(Beam primaryBeam, Beam secondaryBeam, Model model, 
            double plateLength, string boltStandard)
        {
            var originalTransformationPlane = model.GetWorkPlaneHandler().GetCurrentTransformationPlane();

            try
            {
                var webThickness = 0.0;
                var beamHeight = 0.0;
                var flangeHeight = 0.0;
                var innerRoundingRadius = 0.0;
                const double innerMargin = 5.0;

                // First get the essential dimensions from the beam
                var canCreateGaps = CreateGapBetweenBeams(primaryBeam, secondaryBeam, GAP);
                var canGetProperties = secondaryBeam.GetReportProperty("PROFILE.WEB_THICKNESS", ref webThickness) &&
                                       secondaryBeam.GetReportProperty("PROFILE.HEIGHT", ref beamHeight) &&
                                       secondaryBeam.GetReportProperty("PROFILE.FLANGE_THICKNESS", ref flangeHeight);

                if (!canCreateGaps || !canGetProperties) return false;

                var coordSys = primaryBeam.GetCoordinateSystem();

                if (Distance.PointToPoint(primaryBeam.EndPoint, secondaryBeam.StartPoint) < EPSILON ||
                    Distance.PointToPoint(primaryBeam.EndPoint, secondaryBeam.EndPoint) < EPSILON)
                {
                    coordSys.Origin.Translate(coordSys.AxisX.X, coordSys.AxisX.Y, coordSys.AxisX.Z);
                    coordSys.AxisX = -1 * coordSys.AxisX;
                }
                //Creates plates on both sides of the beam.

                #region Create the Plates

                var plate1 = new Beam();
                var plate2 = new Beam();

                // And then the optionals if they exist
                secondaryBeam.GetReportProperty("PROFILE.ROUNDING_RADIUS_1", ref innerRoundingRadius);
                var edgeDistance = (flangeHeight + innerRoundingRadius + innerMargin);
                var plateHeight = beamHeight - 2 * edgeDistance;

                model.GetWorkPlaneHandler().SetCurrentTransformationPlane(new TransformationPlane(coordSys));

                plate1.Position.Depth = plate2.Position.Depth = Position.DepthEnum.MIDDLE;
                plate1.Position.Rotation = plate2.Position.Rotation = Position.RotationEnum.FRONT;


                plate1.StartPoint = new Point(-GAP, (-beamHeight / 2.0) + edgeDistance, webThickness);
                plate1.EndPoint = new Point(plate1.StartPoint.X, (-beamHeight / 2.0) + beamHeight - edgeDistance,
                    webThickness);
                plate2.StartPoint = new Point(plate1.StartPoint.X, plate1.StartPoint.Y, -webThickness);
                plate2.EndPoint = new Point(plate1.EndPoint.X, plate1.EndPoint.Y, -webThickness);

                plate1.Profile.ProfileString =
                    plate2.Profile.ProfileString = "PL" + (int)webThickness + "*" + (int)plateLength;
                plate1.Finish = plate2.Finish = "PAINT";

                // With this we help internal code to assign same ID to plates when plugin is modified.
                // To avoid some problems related to links with UDA values or booleans (cuts, fittings) for example.
                plate1.SetLabel("MyPlate01");
                plate2.SetLabel("MyPlate02");

                plate1.Insert();
                plate2.Insert();

                #endregion

                model.GetWorkPlaneHandler()
                    .SetCurrentTransformationPlane(new TransformationPlane(plate1.GetCoordinateSystem()));

                //Creates two boltArrays to connect the plates
                return CreateBoltArray(primaryBeam, plate1, plate2, plateHeight, true, plateLength, boltStandard) &&
                       CreateBoltArray(secondaryBeam, plate1, plate2, plateHeight, false, plateLength, boltStandard);
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                model.GetWorkPlaneHandler().SetCurrentTransformationPlane(originalTransformationPlane);
            }
        }

        private static bool CreateBoltArray(Beam beam, Beam plate1, Beam plate2, double plateHeight, 
            bool primary, double plateLength, string boltStandard)
        {
            var boltArray = new BoltArray
            {
                PartToBoltTo = plate1,
                PartToBeBolted = beam
            };

            boltArray.AddOtherPartToBolt(plate2);

            if (primary)
            {
                boltArray.FirstPosition = new Point(plateHeight / 2.0, plateLength / 2.0, 0.0);
                boltArray.SecondPosition = new Point(plateHeight / 2.0, -plateLength / 2.0, 0.0);
            }
            else
            {
                boltArray.FirstPosition = new Point(plateHeight / 2.0, -plateLength / 2.0, 0.0);
                boltArray.SecondPosition = new Point(plateHeight / 2.0, plateLength / 2.0, 0.0);
            }

            boltArray.StartPointOffset.Dx = boltArray.EndPointOffset.Dx = plateLength - 75;
            boltArray.StartPointOffset.Dy = boltArray.EndPointOffset.Dy = 0;
            boltArray.StartPointOffset.Dz = boltArray.EndPointOffset.Dz = 0;

            boltArray.BoltSize = 20;
            boltArray.Tolerance = 2.00;
            boltArray.BoltStandard = boltStandard;
            boltArray.BoltType = BoltGroup.BoltTypeEnum.BOLT_TYPE_SITE;
            boltArray.CutLength = 105;

            boltArray.Length = 60;
            boltArray.ExtraLength = 0;
            boltArray.ThreadInMaterial = BoltGroup.BoltThreadInMaterialEnum.THREAD_IN_MATERIAL_YES;

            boltArray.Position.Depth = Position.DepthEnum.MIDDLE;
            boltArray.Position.Plane = Position.PlaneEnum.MIDDLE;
            boltArray.Position.Rotation = Position.RotationEnum.FRONT;

            boltArray.Bolt = true;
            boltArray.Washer1 = false;
            boltArray.Washer2 = boltArray.Washer3 = true;
            boltArray.Nut1 = true;
            boltArray.Nut2 = false;
            boltArray.Hole1 = boltArray.Hole2 = boltArray.Hole3 = boltArray.Hole4 = boltArray.Hole5 = false;

            boltArray.AddBoltDistX(0.0);
            boltArray.AddBoltDistY(plateHeight - 150.0);

            return boltArray.Insert();
        }

        //Creates a gap between the beams
        private static bool CreateGapBetweenBeams(Beam primaryBeam, Beam secondaryBeam, double gap)
        {
            if (primaryBeam == null || secondaryBeam == null) return false;

            //Get vectors defined by the beams, to move their extremes along them when creating the gaps
            var primaryBeamVector = new Vector(primaryBeam.EndPoint.X - primaryBeam.StartPoint.X,
                                                  primaryBeam.EndPoint.Y - primaryBeam.StartPoint.Y,
                                                  primaryBeam.EndPoint.Z - primaryBeam.StartPoint.Z);
            primaryBeamVector.Normalize(gap);

            var secondaryBeamVector = new Vector(secondaryBeam.EndPoint.X - secondaryBeam.StartPoint.X,
                                                    secondaryBeam.EndPoint.Y - secondaryBeam.StartPoint.Y,
                                                    secondaryBeam.EndPoint.Z - secondaryBeam.StartPoint.Z);
            secondaryBeamVector.Normalize(gap);

            Point primaryBeamEdge = null;
            Point secondaryBeamEdge = null;

            if (primaryBeam.StartPoint == secondaryBeam.StartPoint)
            {
                primaryBeamEdge = primaryBeam.StartPoint;
                secondaryBeamEdge = secondaryBeam.StartPoint;
            }

            if (primaryBeam.EndPoint == secondaryBeam.StartPoint)
            {
                ChangeVectorDirection(primaryBeamVector);

                primaryBeamEdge = primaryBeam.EndPoint;
                secondaryBeamEdge = secondaryBeam.StartPoint;
            }

            if (primaryBeam.StartPoint == secondaryBeam.EndPoint)
            {
                ChangeVectorDirection(secondaryBeamVector);

                primaryBeamEdge = primaryBeam.StartPoint;
                secondaryBeamEdge = secondaryBeam.EndPoint;
            }

            if (primaryBeam.EndPoint == secondaryBeam.EndPoint)
            {
                ChangeVectorDirection(primaryBeamVector);
                ChangeVectorDirection(secondaryBeamVector);

                primaryBeamEdge = primaryBeam.EndPoint;
                secondaryBeamEdge = secondaryBeam.EndPoint;
            }

            return CreateFittings(primaryBeam, secondaryBeam, primaryBeamEdge, secondaryBeamEdge,
                primaryBeamVector, secondaryBeamVector);

        }

        //Creates the fittings used to create the gap between the beams
        private static bool CreateFittings(Beam primaryBeam, Beam secondaryBeam, Point primaryBeamEdge, Point secondaryBeamEdge,
                                           Vector primaryBeamVector, Vector secondaryBeamVector)
        {
            var primaryCoordinateSystem = primaryBeam.GetCoordinateSystem();
            var secondaryCoordinateSystem = primaryBeam.GetCoordinateSystem();

            primaryBeamEdge.Translate(primaryBeamVector.X, primaryBeamVector.Y, primaryBeamVector.Z);
            secondaryBeamEdge.Translate(secondaryBeamVector.X, secondaryBeamVector.Y, secondaryBeamVector.Z);

            var fitPrimary = new Fitting
            {
                Plane = new Plane
                {
                    Origin = new Point(primaryBeamEdge.X, primaryBeamEdge.Y, primaryBeamEdge.Z),
                    AxisX = new Vector(Vector.Cross(primaryCoordinateSystem.AxisX,
                        Vector.Cross(primaryCoordinateSystem.AxisX, primaryCoordinateSystem.AxisY)))
                }
            };
            fitPrimary.Plane.AxisX.Normalize(500);
            fitPrimary.Plane.AxisY = new Vector(Vector.Cross(primaryCoordinateSystem.AxisX, primaryCoordinateSystem.AxisY));
            fitPrimary.Plane.AxisY.Normalize(500);
            fitPrimary.Father = primaryBeam;

            var fitSecondary = new Fitting
            {
                Plane = new Plane
                {
                    Origin = new Point(secondaryBeamEdge.X, secondaryBeamEdge.Y, secondaryBeamEdge.Z),
                    AxisX = new Vector(Vector.Cross(secondaryCoordinateSystem.AxisX,
                        Vector.Cross(secondaryCoordinateSystem.AxisX, secondaryCoordinateSystem.AxisY)))
                }
            };
            fitSecondary.Plane.AxisX.Normalize(500);
            fitSecondary.Plane.AxisY = new Vector(Vector.Cross(secondaryCoordinateSystem.AxisX, secondaryCoordinateSystem.AxisY));
            fitSecondary.Plane.AxisY.Normalize(500);
            fitSecondary.Father = secondaryBeam;

            return fitPrimary.Insert() && fitSecondary.Insert();
        }

        private static void ChangeVectorDirection(Point vector)
        {
            vector.X = -1 * vector.X;
            vector.Y = -1 * vector.Y;
            vector.Z = -1 * vector.Z;
        }

        /// <summary>
        /// Gets the values from the dialog and sets the default values if needed
        /// </summary>
        private void GetValuesFromDialog()
        {
            _plateLength = _data.PlateLength;
            _boltStandard = _data.BoltStandard;
            _upDirection = _data.UpDirection;
            _rotationAngleY = _data.RotationAngleY;
            _rotationAngleX = _data.RotationAngleX;
            _locked = _data.Locked;
            _class = _data.Class;
            _connectionCode = _data.ConnectionCode;
            _autoDefaults = _data.AutoDefaults;
            _autoConnection = _data.AutoConnection;

            if (IsDefaultValue(_plateLength) || _plateLength == 0)
                _plateLength = 300.0;
            if (IsDefaultValue(_boltStandard) || _boltStandard == "")
                _boltStandard = "7990";
            if (IsDefaultValue(_upDirection) || _upDirection == 0)
                _upDirection = 0;
            if (IsDefaultValue(_rotationAngleY))
                _rotationAngleY = 0;
            if (IsDefaultValue(_rotationAngleX))
                _rotationAngleX = 0;
            if (IsDefaultValue(_locked))
                _locked = 0;
            if (IsDefaultValue(_class))
                _class = 99;
            if (IsDefaultValue(_connectionCode))
                _connectionCode = "";
            if (IsDefaultValue(_autoDefaults) || _autoDefaults == "")
                _autoDefaults = "albl_no_root";
            if (IsDefaultValue(_autoConnection) || _autoConnection == "")
                _autoConnection = "albl_no_root";
        }
        #endregion

        #region Debug method for windows app
        //this is required for future debugging plugin
        [STAThread]
        static void Main()
        {
            var pluginData = new StructuresData();
            var debugPlugin = new SpliceConnection(pluginData);
            //var inputDefinitions = debugPlugin.DefineInput();
            //var form = new SpliceConnectionForm();
            //form.ShowDialog();
            debugPlugin.Run();
        }
        #endregion
    }
}
