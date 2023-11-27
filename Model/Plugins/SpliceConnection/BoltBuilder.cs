using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace SpliceConn
{
    public class BoltBuilder
    {
        public bool CreateBoltArray(Beam beam, Beam plate1, Beam plate2, double plateHeight,
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

    }
}
