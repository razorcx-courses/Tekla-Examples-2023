using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace SpliceConn
{
    /// <summary>
    /// Class for building planes in Tekla Structures model.
    /// </summary>
    public class PlaneBuilder
    {
        /// <summary>
        /// Constant representing the length of the constructed plane.
        /// </summary>
        private const double PlaneLength = 500;

        /// <summary>
        /// Creates a plane for fitting in the Tekla Structures model.
        /// </summary>
        /// <param name="coordinateSystem">The coordinate system.</param>
        /// <param name="point">A point in space.</param>
        /// <param name="distanceVector">A vector describing a distance in space.</param>
        /// <returns>A <see cref="Plane"/> object.</returns>
        public Plane GetPlaneForFitting(CoordinateSystem coordinateSystem, Point point, Vector distanceVector)
        {
            // Calculate the origin point for the plane.
            var originPoint = GetOriginPointForFitting(point, distanceVector);

            // Retrieve the X-axis for the plane.
            var axisX = GetAxisXForFitting(coordinateSystem);

            // Construct and return a Plane object using the calculated origin point and X-axis.
            var plane = GetPlane(originPoint, axisX, coordinateSystem);
            return plane;
        }

        /// <summary>
        /// Calculates the origin point for the fitting plane.
        /// </summary>
        /// <param name="point">A point in space.</param>
        /// <param name="distanceVector">A vector describing a distance in space.</param>
        /// <returns>A new <see cref="Point"/> representing the origin point for the plane.</returns>
        private static Point GetOriginPointForFitting(Point point, Vector distanceVector)
        {
            // Create a new point based on the input point.
            var originPoint = new Point(point);

            // Translate the new point by the components of the distanceVector.
            originPoint.Translate(distanceVector.X, distanceVector.Y, distanceVector.Z);

            // Return the translated point.
            return originPoint;
        }

        /// <summary>
        /// Calculates the X-axis for the fitting plane.
        /// </summary>
        /// <param name="coordinateSystem">The coordinate system.</param>
        /// <returns>A new <see cref="Vector"/> representing the X-axis for the plane.</returns>
        private static Vector GetAxisXForFitting(CoordinateSystem coordinateSystem)
        {
            // Calculate the X-axis for the plane using cross products and normalization.
            var axisX = new Vector(Vector.Cross(coordinateSystem.AxisX,
                Vector.Cross(coordinateSystem.AxisX, coordinateSystem.AxisY)));
            return axisX;
        }

        /// <summary>
        /// Creates a Plane object in Tekla Structures model.
        /// </summary>
        /// <param name="point">A point in space.</param>
        /// <param name="axisX">The X-axis for the plane.</param>
        /// <param name="coordinateSystem">The coordinate system.</param>
        /// <returns>A new <see cref="Plane"/> object.</returns>
        private static Plane GetPlane(Point point, Vector axisX, CoordinateSystem coordinateSystem)
        {
            // Create a new Plane object with the specified origin and X-axis.
            var plane = new Plane
            {
                Origin = new Point(point),
                AxisX = axisX
            };

            // Normalize the X-axis to the specified PlaneLength.
            plane.AxisX.Normalize(PlaneLength);

            // Calculate and set the Y-axis for the plane based on the cross product of the coordinate system's X and Y axes.
            plane.AxisY = new Vector(Vector.Cross(coordinateSystem.AxisX, coordinateSystem.AxisY));
            plane.AxisY.Normalize(PlaneLength);

            // Return the constructed Plane object.
            return plane;
        }
    }
}
