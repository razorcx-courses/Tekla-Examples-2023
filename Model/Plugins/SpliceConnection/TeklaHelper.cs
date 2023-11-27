using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Parallel = Tekla.Structures.Geometry3d.Parallel;

namespace SpliceConn
{
    public class TeklaHelper
    {
        private const double EPSILON = 0.001;

        public static bool CheckIfBeamsAreAligned(Beam primaryBeam, Beam secondaryBeam)
        {
            if (primaryBeam == null || secondaryBeam == null) return false;

            var primaryLine = new Line(primaryBeam.StartPoint, primaryBeam.EndPoint);
            var secondaryLine = new Line(secondaryBeam.StartPoint, secondaryBeam.EndPoint);

            return Parallel.LineToLine(primaryLine, secondaryLine);
        }

        public static CoordinateSystem GetCoordinateSystem(Beam primaryBeam, Beam secondaryBeam)
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
    }
}
