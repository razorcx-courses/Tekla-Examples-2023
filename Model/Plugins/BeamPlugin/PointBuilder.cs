using System.Collections.Generic;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Plugins;

namespace BeamPlugin
{
    public class PointBuilder
    {
        public List<Point> GetPoints(List<PluginBase.InputDefinition> inputs, double lengthFactor)
        {
            var point1 = GetPoint1(inputs[0]);
            var point2 = GetPoint2(inputs[1], point1, lengthFactor);

            var points = new List<Point>
            {
                point1,
                point2
            };
            return points;
        }

        private Point GetPoint1(PluginBase.InputDefinition input)
        {
            var point1 = (Point)(input).GetInput();
            return point1;
        }

        private Point GetPoint2(PluginBase.InputDefinition input, Point point1, double lengthFactor)
        {
            var point2 = (Point)(input).GetInput();

            var lengthVector = GetLengthVector(point1, point2);

            if (!(lengthFactor > 0)) return point2;

            point2 = GetNewPoint2(point1, point2, lengthVector, lengthFactor);

            return point2;
        }

        private static Point GetLengthVector(Point point1, Point point2)
        {
            return new Point(point2.X - point1.X, point2.Y - point1.Y, point2.Z - point1.Z);
        }

        private Point GetNewPoint2(Point point1, Point point2, Point lengthVector, double lengthFactor)
        {
            point2.X = lengthFactor * lengthVector.X + point1.X;
            point2.Y = lengthFactor * lengthVector.Y + point1.Y;
            point2.Z = lengthFactor * lengthVector.Z + point1.Z;

            return point2;
        }

    }
}