using System;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using TSMUI = Tekla.Structures.Model.UI;

namespace SpliceConn
{

    public class SpliceConnectionBuilder
    {
        #region Fields
        private readonly Model _model = new Model();
        private Data _data;
        private const double GAP = 10.0;
        private const double EPSILON = 0.001;
        #endregion

        public SpliceConnectionBuilder()
        {

        }

        public bool Create(Data data)
        {
            _data = data;

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

            var primaryProfileType = primaryBeam.GetReportProperty<string>("PROFILE_TYPE");
            var secondaryProfileType = primaryBeam.GetReportProperty<string>("PROFILE_TYPE");

            if (primaryBeam.Profile.ProfileString != secondaryBeam.Profile.ProfileString ||
                primaryProfileType != secondaryProfileType) return false;

            if (!CheckIfBeamsAreAligned(primaryBeam, secondaryBeam)) return false;

            return CreateSpliceConnection(primaryBeam, secondaryBeam, _model, _data.PlateLength, _data.BoltStandard);
        }


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
                var webThickness = secondaryBeam.GetReportProperty<double>("PROFILE.WEB_THICKNESS");
                var beamHeight = secondaryBeam.GetReportProperty<double>("PROFILE.HEIGHT");
                var flangeHeight = secondaryBeam.GetReportProperty<double>("PROFILE.FLANGE_THICKNESS");

                var validProperties = webThickness > 0.0 && beamHeight > 0.0 && flangeHeight > 0.0;
                if (!validProperties) return false;

                var canCreateGaps = CreateGapBetweenBeams(primaryBeam, secondaryBeam, GAP);
                if (!canCreateGaps) return false;

                var coordSys = GetCoordinateSystem(primaryBeam, secondaryBeam);

                //Creates plates on both sides of the beam.

                #region Create the Plates

                // And then the optionals if they exist
                var innerRoundingRadius = secondaryBeam.GetReportProperty<double>("PROFILE.ROUNDING_RADIUS_1");
                const double innerMargin = 5.0;

                var edgeDistance = (flangeHeight + innerRoundingRadius + innerMargin);
                var plateHeight = beamHeight - 2 * edgeDistance;

                model.GetWorkPlaneHandler().SetCurrentTransformationPlane(new TransformationPlane(coordSys));

                CreatePlates(plateLength, webThickness, beamHeight, edgeDistance, out var plate1, out var plate2);

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

        private static CoordinateSystem GetCoordinateSystem(Beam primaryBeam, Beam secondaryBeam)
        {
            var coordSys = primaryBeam.GetCoordinateSystem();

            if (Distance.PointToPoint(primaryBeam.EndPoint, secondaryBeam.StartPoint) < EPSILON ||
                Distance.PointToPoint(primaryBeam.EndPoint, secondaryBeam.EndPoint) < EPSILON)
            {
                coordSys.Origin.Translate(coordSys.AxisX.X, coordSys.AxisX.Y, coordSys.AxisX.Z);
                coordSys.AxisX *= -1;
            }

            return coordSys;
        }

        private static void CreatePlates(double plateLength, double webThickness, double beamHeight, double edgeDistance,
            out Beam plate1, out Beam plate2)
        {
            var profile = "PL" + (int)webThickness + "*" + (int)plateLength;
            var finish = "PAINT";
            var label = "WEBPLATE";

            var plate1StartPoint = new Point(0, (-beamHeight / 2.0) + edgeDistance, webThickness);
            var plate1EndPoint = new Point(plate1StartPoint)
            {
                Y = beamHeight / 2 - edgeDistance
            };

            plate1 = CreatePlate(plate1StartPoint, plate1EndPoint, profile, finish, label + "01");

            var plate2StartPoint = new Point(plate1StartPoint.X, plate1StartPoint.Y, -webThickness);
            var plate2EndPoint = new Point(plate1EndPoint.X, plate1EndPoint.Y, -webThickness);

            plate2 = CreatePlate(plate2StartPoint, plate2EndPoint, profile, finish, label + "02");
        }

        private static Beam CreatePlate(Point startPoint, Point endPoint, string profile, string finish, string label)
        {
            var plate1 = new Beam
            {
                Position =
                {
                    Depth = Position.DepthEnum.MIDDLE,
                    Rotation = Position.RotationEnum.FRONT
                },
                StartPoint = startPoint,
                EndPoint = endPoint,
                Profile =
                {
                    ProfileString = profile
                },
                Finish = finish
            };
            plate1.SetLabel(label);
            plate1.Insert();
            return plate1;
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

            var point1 = new Point(plateHeight / 2.0, plateLength / 2.0, 0.0);
            var point2 = new Point(point1);
            point2.Y *= -1;

            var firstPosition = primary ? point1 : point2;
            var secondPosition = primary ? point2 : point1;

            boltArray.FirstPosition = firstPosition;
            boltArray.SecondPosition = secondPosition;

            boltArray.StartPointOffset.Dx = plateLength - 75;
            boltArray.StartPointOffset.Dy = 0;
            boltArray.StartPointOffset.Dz = 0;

            boltArray.EndPointOffset.Dx = plateLength - 75;
            boltArray.EndPointOffset.Dy = 0;
            boltArray.EndPointOffset.Dz = 0;

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

            var primaryBeamGapVector = new Vector(primaryBeam.EndPoint - primaryBeam.StartPoint);
            primaryBeamGapVector.Normalize(gap);

            var secondaryBeamGapVector = new Vector(secondaryBeam.EndPoint - secondaryBeam.StartPoint);
            secondaryBeamGapVector.Normalize(gap);

            //var sameVector = primaryBeamGapVector == secondaryBeamGapVector;
            //var isAEqualB = sameVector && Distance.PointToPoint(primaryBeam.StartPoint, secondaryBeam.EndPoint) < 1.0;
            //var isBEqualB = !sameVector && Distance.PointToPoint(primaryBeam.EndPoint, secondaryBeam.EndPoint) < 1.0;

            //if (primaryBeam.StartPoint == secondaryBeam.StartPoint)
            var primaryBeamEndPoint = primaryBeam.StartPoint;
            var secondaryBeamEndPoint = secondaryBeam.StartPoint;

            if (primaryBeam.EndPoint == secondaryBeam.StartPoint)
            {
                primaryBeamGapVector *= -1; //reverse vector

                primaryBeamEndPoint = primaryBeam.EndPoint;
                secondaryBeamEndPoint = secondaryBeam.StartPoint;
            }

            if (primaryBeam.StartPoint == secondaryBeam.EndPoint)
            {
                secondaryBeamGapVector *= -1;

                primaryBeamEndPoint = primaryBeam.StartPoint;
                secondaryBeamEndPoint = secondaryBeam.EndPoint;
            }

            if (primaryBeam.EndPoint == secondaryBeam.EndPoint)
            {
                primaryBeamGapVector *= -1;
                secondaryBeamGapVector *= -1; 

                primaryBeamEndPoint = primaryBeam.EndPoint;
                secondaryBeamEndPoint = secondaryBeam.EndPoint;
            }

            var primaryFittingResult = CreateFitting(primaryBeam, primaryBeamEndPoint, primaryBeamGapVector);

            var secondaryFittingResult = CreateFitting(secondaryBeam, secondaryBeamEndPoint, secondaryBeamGapVector);

            return primaryFittingResult && secondaryFittingResult;

        }

        //Creates the fittings used to create the gap between the beams
        private static bool CreateFitting(Beam beam, Point beamEndPoint, Vector beamGapVector)
        {
            var plane = GetPlaneForFitting(beam, beamEndPoint, beamGapVector);

            var fitting = new Fitting
            {
                Plane = plane,
                Father = beam
            };

            return fitting.Insert();
        }

        private static Plane GetPlaneForFitting(Beam beam, Point beamEndPoint, Vector beamGapVector)
        {
            var coordinateSystem = beam.GetCoordinateSystem();

            var originPoint = GetOriginPointForFitting(beamEndPoint, beamGapVector);

            var axisX = GetAxisXForFitting(coordinateSystem);

            var plane = GetPlane(originPoint, axisX, coordinateSystem);
            return plane;
        }

        private static Point GetOriginPointForFitting(Point beamEdge, Vector beamVector)
        {
            var originPoint = new Point(beamEdge);
            originPoint.Translate(beamVector.X, beamVector.Y, beamVector.Z);
            return originPoint;
        }

        private static Vector GetAxisXForFitting(CoordinateSystem coordinateSystem)
        {
            var axisX = new Vector(Vector.Cross(coordinateSystem.AxisX,
                Vector.Cross(coordinateSystem.AxisX, coordinateSystem.AxisY)));
            return axisX;
        }

        private static Plane GetPlane(Point beamEdge, Vector axisX, CoordinateSystem coordinateSystem)
        {
            var length = 500;

            var plane = new Plane
            {
                Origin = new Point(beamEdge),
                AxisX = axisX
            };

            plane.AxisX.Normalize(length);

            plane.AxisY = new Vector(Vector.Cross(coordinateSystem.AxisX, coordinateSystem.AxisY));
            plane.AxisY.Normalize(length);
            return plane;
        }

        #endregion

    }
}
