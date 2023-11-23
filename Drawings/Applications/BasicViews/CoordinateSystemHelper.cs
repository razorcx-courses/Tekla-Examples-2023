using Tekla.Structures.Geometry3d;

namespace BasicViews
{
    public class CoordinateSystemHelper
    {
        readonly Vector _upDirection = new Vector(0.0, 0.0, 1.0);

        /// <summary>
        /// Gets part default front view coordinate system
        /// Gets coordinate system as it is defined in the TS core for part/component basic views, which is different than in singlepart/assembly drawings.
        /// </summary>
        /// <param name="objectCoordinateSystem"></param>
        /// <returns></returns>
        public CoordinateSystem GetBasicViewsCoordinateSystemForFrontView(CoordinateSystem objectCoordinateSystem)
        {
            var coordSystem = GetCoordinateSystem(objectCoordinateSystem);

            var tempVector = GetTempVector(objectCoordinateSystem, coordSystem);

            coordSystem.AxisX = tempVector.Cross(_upDirection).GetNormal();
            coordSystem.AxisY = _upDirection.GetNormal();

            return coordSystem;
        }

        /// <summary>
        /// Gets part default top view coordinate system
        /// Gets coordinate system as it is defined in the TS core for part/component basic views, which is different than in singlepart/assembly drawings.
        /// </summary>
        /// <param name="objectCoordinateSystem"></param>
        /// <returns></returns>
        public CoordinateSystem GetBasicViewsCoordinateSystemForTopView(CoordinateSystem objectCoordinateSystem)
        {
            var coordSystem = GetCoordinateSystem(objectCoordinateSystem);

            var tempVector = GetTempVector(objectCoordinateSystem, coordSystem);

            coordSystem.AxisX = tempVector.Cross(_upDirection);
            coordSystem.AxisY = tempVector;

            return coordSystem;
        }

        /// <summary>
        /// Gets part default end view coordinate system
        /// Gets coordinate system as it is defined in the TS core for part/component basic views, which is different than in singlepart/assembly drawings.
        /// </summary>
        /// <param name="objectCoordinateSystem"></param>
        /// <returns></returns>
        public CoordinateSystem GetBasicViewsCoordinateSystemForEndView(CoordinateSystem objectCoordinateSystem)
        {
            var coordSystem = GetCoordinateSystem(objectCoordinateSystem);

            var tempVector = GetTempVector(objectCoordinateSystem, coordSystem);

            coordSystem.AxisX = tempVector;
            coordSystem.AxisY = _upDirection;

            return coordSystem;
        }

        private Vector GetTempVector(CoordinateSystem objectCoordinateSystem, CoordinateSystem coordSystem)
        {
            var tempVector = (coordSystem.AxisX.Cross(_upDirection));

            if (tempVector == new Vector())
                tempVector = (objectCoordinateSystem.AxisY.Cross(_upDirection));
            return tempVector;
        }

        private static CoordinateSystem GetCoordinateSystem(CoordinateSystem objectCoordinateSystem)
        {
            return new CoordinateSystem
            {
                Origin = new Point(objectCoordinateSystem.Origin),
                AxisX = new Vector(objectCoordinateSystem.AxisX) * -1.0,
                AxisY = new Vector(objectCoordinateSystem.AxisY)
            };
        }
    }
}