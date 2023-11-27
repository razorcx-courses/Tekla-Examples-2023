using System.Collections;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace SpliceConn
{
    public class BoltBuilder
    {
        public bool CreateBoltArray(Beam beam, Beam plate1, Beam plate2, double plateHeight,
            bool primary, double plateLength, string boltStandard)
        {
            //default bolt array
            var boltArray = GetDefaultBoltArray();

            //set parts
            boltArray.PartToBoltTo = plate1;
            boltArray.PartToBeBolted = beam;

            //add parts
            boltArray.AddOtherPartToBolt(plate2);

            //calculate bolt position
            var point1 = new Point(plateHeight / 2.0, plateLength / 2.0, 0.0);
            var point2 = new Point(point1);
            point2.Y *= -1;

            var firstPosition = primary ? point1 : point2;
            var secondPosition = primary ? point2 : point1;

            //set first and second positions
            boltArray.FirstPosition = firstPosition;
            boltArray.SecondPosition = secondPosition;

            //set bolt standard
            boltArray.BoltStandard = boltStandard;

            //set start and end point offsets
            boltArray.StartPointOffset.Dx = plateLength - 75;
            boltArray.EndPointOffset.Dx = plateLength - 75;

            //add bolt columns & rows
            boltArray.AddBoltDistX(0.0);
            boltArray.AddBoltDistY(plateHeight - 150.0);

            return boltArray.Insert();
        }

        private BoltArray GetDefaultBoltArray()
        {
            var boltArray = new BoltArray
            {
                FirstPosition = new Point(),
                SecondPosition = new Point(),
                StartPointOffset =
                {
                    Dx = 100,
                    Dy = 0,
                    Dz = 0
                },
                EndPointOffset =
                {
                    Dx = 100,
                    Dy = 0,
                    Dz = 0
                },
                BoltSize = 19.05,
                Tolerance = 1.00,
                BoltStandard = "A325N",
                BoltType = BoltGroup.BoltTypeEnum.BOLT_TYPE_SITE,
                CutLength = 105,
                Length = 60,
                ExtraLength = 0,
                ThreadInMaterial = BoltGroup.BoltThreadInMaterialEnum.THREAD_IN_MATERIAL_YES,
                Position =
                {
                    Depth = Position.DepthEnum.MIDDLE,
                    Plane = Position.PlaneEnum.MIDDLE,
                    Rotation = Position.RotationEnum.FRONT
                },
                Bolt = true,
                Washer1 = false
            };

            boltArray.Washer2 = boltArray.Washer3 = true;
            boltArray.Nut1 = true;
            boltArray.Nut2 = false;
            boltArray.Hole1 = boltArray.Hole2 = boltArray.Hole3 = boltArray.Hole4 = boltArray.Hole5 = false;

            return boltArray;
        }
    }
}
