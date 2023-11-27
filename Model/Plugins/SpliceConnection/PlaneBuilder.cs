using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace SpliceConn
{
    public class PlaneBuilder
    {
        private const double PlaneLength = 500;

        public Plane GetPlaneForFitting(CoordinateSystem coordinateSystem, Point point, Vector distanceVector)
        {
            var originPoint = GetOriginPointForFitting(point, distanceVector);

            var axisX = GetAxisXForFitting(coordinateSystem);

            var plane = GetPlane(originPoint, axisX, coordinateSystem);
            return plane;
        }

        private static Point GetOriginPointForFitting(Point point, Vector distanceVector)
        {
            var originPoint = new Point(point);
            originPoint.Translate(distanceVector.X, distanceVector.Y, distanceVector.Z);
            return originPoint;
        }

        private static Vector GetAxisXForFitting(CoordinateSystem coordinateSystem)
        {
            var axisX = new Vector(Vector.Cross(coordinateSystem.AxisX,
                Vector.Cross(coordinateSystem.AxisX, coordinateSystem.AxisY)));
            return axisX;
        }

        private static Plane GetPlane(Point point, Vector axisX, CoordinateSystem coordinateSystem)
        {
            var plane = new Plane
            {
                Origin = new Point(point),
                AxisX = axisX
            };

            plane.AxisX.Normalize(PlaneLength);

            plane.AxisY = new Vector(Vector.Cross(coordinateSystem.AxisX, coordinateSystem.AxisY));
            plane.AxisY.Normalize(PlaneLength);
            return plane;
        }
    }
}