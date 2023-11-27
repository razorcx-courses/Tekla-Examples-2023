﻿using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace SpliceConn
{
    public class PlateBuilder
    {
        public void CreatePlates(double plateLength, double webThickness, double beamHeight, double edgeDistance,
            out Beam plate1, out Beam plate2)
        {
            var profile = "PL" + (int)webThickness + "*" + (int)plateLength;
            var finish = "PAINT";
            var label = "WEBPLATE";

            var plate1StartPoint = new Point(0, (-beamHeight / 2.0) + edgeDistance, webThickness);
            var plate1EndPoint = new Point(plate1StartPoint)
            {
                Y = beamHeight / 2 - edgeDistance
            };

            plate1 = CreatePlate(plate1StartPoint, plate1EndPoint, profile, finish, label + "01");

            var plate2StartPoint = new Point(plate1StartPoint.X, plate1StartPoint.Y, -webThickness);
            var plate2EndPoint = new Point(plate1EndPoint.X, plate1EndPoint.Y, -webThickness);

            plate2 = CreatePlate(plate2StartPoint, plate2EndPoint, profile, finish, label + "02");
        }

        private static Beam CreatePlate(Point startPoint, Point endPoint, string profile, string finish, string label)
        {
            var plate1 = new Beam
            {
                Position =
                {
                    Depth = Position.DepthEnum.MIDDLE,
                    Rotation = Position.RotationEnum.FRONT
                },
                StartPoint = startPoint,
                EndPoint = endPoint,
                Profile =
                {
                    ProfileString = profile
                },
                Finish = finish
            };
            plate1.SetLabel(label);
            plate1.Insert();
            return plate1;
        }
    }
}
