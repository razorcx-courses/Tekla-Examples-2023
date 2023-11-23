using Tekla.Structures.Plugins;

namespace BeamPlugin
{
    public class StructuresData
    {
        [StructuresField("LengthFactor")]
        public double LengthFactor;

        [StructuresField("Profile")]
        public string Profile;
    }
}