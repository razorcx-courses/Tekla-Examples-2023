using System;
using Tekla.Structures.Model;

namespace SpliceConn
{
    public class SpliceConnectionBuilder
    {
        private readonly Model _model = new Model();
        private const double GAP = 10.0;

        private readonly FittingBuilder _fittingBuilder = new FittingBuilder();
        private readonly PlateBuilder _plateBuilder = new PlateBuilder();
        private readonly BoltBuilder _boltBuilder = new BoltBuilder();

        public bool Create(Data data, Beam primaryBeam, Beam secondaryBeam)
        {
            if (primaryBeam == null || secondaryBeam == null) return false;

            if (!TeklaHelper.AreProfilesEqual(primaryBeam, secondaryBeam)) return false;

            if (!TeklaHelper.AreBeamsAligned(primaryBeam, secondaryBeam)) return false;

            return CreateSpliceConnection(primaryBeam, secondaryBeam, data.PlateLength, data.BoltStandard);
        }

        private bool CreateSpliceConnection(Beam primaryBeam, Beam secondaryBeam,
            double plateLength, string boltStandard)
        {
            var originalTransformationPlane = _model.GetTransformationPlane();

            try
            {
                var webThickness = secondaryBeam.GetReportProperty<double>("PROFILE.WEB_THICKNESS");
                var beamHeight = secondaryBeam.GetReportProperty<double>("PROFILE.HEIGHT");
                var flangeHeight = secondaryBeam.GetReportProperty<double>("PROFILE.FLANGE_THICKNESS");

                //make sure we have beam profiles for calculating plate sizes and bolts
                var validProperties = webThickness > 0.0 && beamHeight > 0.0 && flangeHeight > 0.0;
                if (!validProperties) return false;

                //create gap between beams
                var canCreateGaps = _fittingBuilder.CreateGapBetweenBeams(primaryBeam, secondaryBeam, GAP);
                if (!canCreateGaps) return false;

                //create plates on both sides of the beam.
                var innerRoundingRadius = secondaryBeam.GetReportProperty<double>("PROFILE.ROUNDING_RADIUS_1");
                const double innerMargin = 5.0;

                var edgeDistance = (flangeHeight + innerRoundingRadius + innerMargin);
                var plateHeight = beamHeight - 2 * edgeDistance;

                var coordSys = TeklaHelper.GetCoordinateSystem(primaryBeam, secondaryBeam);
                _model.SetTransformationPlane(coordSys);

                _plateBuilder.CreatePlates(plateLength, webThickness, beamHeight, edgeDistance, 
                    out var plate1, out var plate2);

                //change coordinate system for bolt creation
                _model.SetTransformationPlane(plate1.GetCoordinateSystem());

                //create two boltArrays to connect the plates
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
                _model.SetTransformationPlane(originalTransformationPlane);
            }
        }
    }
}
