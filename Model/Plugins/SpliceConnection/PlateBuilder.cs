using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace SpliceConn
{
    /// <summary>
    /// Class for building plates in Tekla Structures model.
    /// </summary>
    public class PlateBuilder
    {
        /// <summary>
        /// Creates two plates in the Tekla Structures model.
        /// </summary>
        /// <param name="length">The length of the plates.</param>
        /// <param name="thickness">The thickness of the plates.</param>
        /// <param name="height">The height of the plates.</param>
        /// <param name="edgeDistance">The distance from the plate edges to the centerline.</param>
        /// <param name="plate1">Out parameter for the first plate created.</param>
        /// <param name="plate2">Out parameter for the second plate created.</param>
        public void CreatePlates(double length, double thickness, double height, double edgeDistance,
            out Beam plate1, out Beam plate2)
        {
            // Forming profile string for the plates.
            var profile = "PL" + (int)thickness + "*" + (int)length;
            var finish = "PAINT";
            var label = "WEBPLATE";

            // Calculating start and end points for plate 1.
            var plate1StartPoint = new Point(0, (-height / 2.0) + edgeDistance, thickness);
            var plate1EndPoint = new Point(plate1StartPoint)
            {
                Y = height / 2 - edgeDistance
            };

            // Creating plate 1.
            plate1 = CreatePlate(plate1StartPoint, plate1EndPoint, profile, finish, label + "01");

            // Calculating start and end points for plate 2.
            var plate2StartPoint = new Point(plate1StartPoint.X, plate1StartPoint.Y, -thickness);
            var plate2EndPoint = new Point(plate1EndPoint.X, plate1EndPoint.Y, -thickness);

            // Creating plate 2.
            plate2 = CreatePlate(plate2StartPoint, plate2EndPoint, profile, finish, label + "02");
        }

        /// <summary>
        /// Creates a plate (Beam) in the Tekla Structures model.
        /// </summary>
        /// <param name="startPoint">The start point of the plate.</param>
        /// <param name="endPoint">The end point of the plate.</param>
        /// <param name="profile">The profile string for the plate.</param>
        /// <param name="finish">The finish of the plate.</param>
        /// <param name="label">The label of the plate.</param>
        /// <returns>The created plate (Beam).</returns>
        private static Beam CreatePlate(Point startPoint, Point endPoint, string profile, string finish, string label)
        {
            // Creating a new plate (Beam) object.
            var plate = new Beam
            {
                // Setting plate properties.
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

            // Setting the label for the plate.
            plate.SetLabel(label);

            // Inserting the plate into the Tekla Structures model.
            plate.Insert();

            // Returning the created plate.
            return plate;
        }
    }
}

