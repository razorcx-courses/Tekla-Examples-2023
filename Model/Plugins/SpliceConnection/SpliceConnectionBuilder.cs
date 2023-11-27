using System;
using Tekla.Structures.Model;
using TSMUI = Tekla.Structures.Model.UI;

namespace SpliceConn
{
    public class SpliceConnectionBuilder
    {
        private readonly Model _model = new Model();
        private Data _data;
        private const double GAP = 10.0;

        private readonly FittingBuilder _fittingBuilder = new FittingBuilder();
        private readonly PlateBuilder _plateBuilder = new PlateBuilder();
        private readonly BoltBuilder _boltBuilder = new BoltBuilder();

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

            if (!TeklaHelper.CheckIfBeamsAreAligned(primaryBeam, secondaryBeam)) return false;

            return CreateSpliceConnection(primaryBeam, secondaryBeam, _model, _data.PlateLength, _data.BoltStandard);
        }

        private bool CreateSpliceConnection(Beam primaryBeam, Beam secondaryBeam, Model model,
            double plateLength, string boltStandard)
        {
            var originalTransformationPlane = model.GetTransformationPlane();

            try
            {
                var webThickness = secondaryBeam.GetReportProperty<double>("PROFILE.WEB_THICKNESS");
                var beamHeight = secondaryBeam.GetReportProperty<double>("PROFILE.HEIGHT");
                var flangeHeight = secondaryBeam.GetReportProperty<double>("PROFILE.FLANGE_THICKNESS");

                var validProperties = webThickness > 0.0 && beamHeight > 0.0 && flangeHeight > 0.0;
                if (!validProperties) return false;

                var canCreateGaps = _fittingBuilder.CreateGapBetweenBeams(primaryBeam, secondaryBeam, GAP);
                if (!canCreateGaps) return false;

                var coordSys = TeklaHelper.GetCoordinateSystem(primaryBeam, secondaryBeam);

                //Creates plates on both sides of the beam.
                var innerRoundingRadius = secondaryBeam.GetReportProperty<double>("PROFILE.ROUNDING_RADIUS_1");
                const double innerMargin = 5.0;

                var edgeDistance = (flangeHeight + innerRoundingRadius + innerMargin);
                var plateHeight = beamHeight - 2 * edgeDistance;

                model.SetTransformationPlane(coordSys);

                _plateBuilder.CreatePlates(plateLength, webThickness, beamHeight, edgeDistance, 
                    out var plate1, out var plate2);

                model.SetTransformationPlane(plate1.GetCoordinateSystem());

                //Creates two boltArrays to connect the plates
                return _boltBuilder.CreateBoltArray(primaryBeam, plate1, plate2, plateHeight, 
                           true, plateLength, boltStandard) &&
                       _boltBuilder.CreateBoltArray(secondaryBeam, plate1, plate2, plateHeight, 
                           false, plateLength, boltStandard);
            }
            catch (Exception e)
            {
                return false;
            }
            finally
            {
                model.SetTransformationPlane(originalTransformationPlane);
            }
        }
    }
}
