using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace BeamPlugin
{
    public class BeamBuilder
    {
        public void CreateBeam(Point point1, Point point2, string profile, string finish, string label)
        {
            var myBeam = new Beam(point1, point2);

            myBeam.Profile.ProfileString = profile;
            myBeam.Finish = finish;

            // With this we help internal code to assign same ID to beam when plugin is modified.
            // To avoid some problems related to links with UDA values or booleans (cuts, fittings) for example.
            myBeam.SetLabel(label);

            myBeam.Insert();
        }
    }
}