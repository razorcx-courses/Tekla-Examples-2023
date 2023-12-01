using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace SpliceConn
{
    public class FittingBuilder
    {
        private readonly PlaneBuilder _planeBuilder = new PlaneBuilder();

        //Creates a gap between the beams
        public bool CreateGapBetweenBeams(Beam primaryBeam, Beam secondaryBeam, double gap)
        {
            if (primaryBeam == null || secondaryBeam == null) return false;
            if (gap <= 1) return false;

            //find gap vector distances
            var primaryBeamGapVector = new Vector(primaryBeam.EndPoint - primaryBeam.StartPoint);
            primaryBeamGapVector.Normalize(gap);

            var secondaryBeamGapVector = new Vector(secondaryBeam.EndPoint - secondaryBeam.StartPoint);
            secondaryBeamGapVector.Normalize(gap);

            //reverse gap vectors depending on direction of beams relative to each other
            ReverseGapVector(primaryBeam, secondaryBeam, ref primaryBeamGapVector, ref secondaryBeamGapVector);

            //find start and end points for gap(fittings)
            //default - if (primaryBeam.StartPoint == secondaryBeam.StartPoint)
            var primaryBeamEndPoint = primaryBeam.StartPoint;
            var secondaryBeamEndPoint = secondaryBeam.StartPoint;

            //adjust points depending on direction of beams relative to each other
            AdjustStartAndEndPoints(primaryBeam, secondaryBeam, ref primaryBeamEndPoint, ref secondaryBeamEndPoint);

            //create fittings
            var primaryFittingResult = CreateFitting(primaryBeam, primaryBeamEndPoint, primaryBeamGapVector);
            var secondaryFittingResult = CreateFitting(secondaryBeam, secondaryBeamEndPoint, secondaryBeamGapVector);

            return primaryFittingResult && secondaryFittingResult;

        }

        private static void ReverseGapVector(Beam primaryBeam, Beam secondaryBeam, ref Vector primaryBeamGapVector,
            ref Vector secondaryBeamGapVector)
        {
            if (primaryBeam.EndPoint == secondaryBeam.StartPoint)
            {
                primaryBeamGapVector *= -1; //reverse vector
            }

            if (primaryBeam.StartPoint == secondaryBeam.EndPoint)
            {
                secondaryBeamGapVector *= -1;
            }

            if (primaryBeam.EndPoint == secondaryBeam.EndPoint)
            {
                primaryBeamGapVector *= -1;
                secondaryBeamGapVector *= -1;
            }
        }

        private static void AdjustStartAndEndPoints(Beam primaryBeam, Beam secondaryBeam, ref Point primaryBeamEndPoint,
            ref Point secondaryBeamEndPoint)
        {
            if (primaryBeam.EndPoint == secondaryBeam.StartPoint)
            {
                primaryBeamEndPoint = primaryBeam.EndPoint;
                secondaryBeamEndPoint = secondaryBeam.StartPoint;
            }

            if (primaryBeam.StartPoint == secondaryBeam.EndPoint)
            {
                primaryBeamEndPoint = primaryBeam.StartPoint;
                secondaryBeamEndPoint = secondaryBeam.EndPoint;
            }

            if (primaryBeam.EndPoint == secondaryBeam.EndPoint)
            {
                primaryBeamEndPoint = primaryBeam.EndPoint;
                secondaryBeamEndPoint = secondaryBeam.EndPoint;
            }
        }

        //Creates the fittings used to create the gap between the beams
        private bool CreateFitting(Beam beam, Point beamEndPoint, Vector beamGapVector)
        {
            var coordinateSystem = beam.GetCoordinateSystem();

            var plane = _planeBuilder.GetPlaneForFitting(coordinateSystem, beamEndPoint, beamGapVector);

            var fitting = new Fitting
            {
                Plane = plane,
                Father = beam
            };

            return fitting.Insert();
        }
    }
}
