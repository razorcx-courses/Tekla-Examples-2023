using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace SpliceConn
{
    public class FittingBuilder
    {
        //Creates a gap between the beams
        public bool CreateGapBetweenBeams(Beam primaryBeam, Beam secondaryBeam, double gap)
        {
            if (primaryBeam == null || secondaryBeam == null) return false;

            var primaryBeamGapVector = new Vector(primaryBeam.EndPoint - primaryBeam.StartPoint);
            primaryBeamGapVector.Normalize(gap);

            var secondaryBeamGapVector = new Vector(secondaryBeam.EndPoint - secondaryBeam.StartPoint);
            secondaryBeamGapVector.Normalize(gap);

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
    }
}
