using Tekla.Structures.Plugins;

namespace SpliceConn
{
    public class StructuresData
    {
        [StructuresField("PlateLength")]
        public double PlateLength;
        [StructuresField("BoltStandard")]
        public string BoltStandard;
        [StructuresField("zsuunta")] // It is mandatory to use type integer for this attribute
        public int UpDirection;
        [StructuresField("zang1")] // It is mandatory to use type double for this attribute
        public double RotationAngleY;
        [StructuresField("zang2")] // It is mandatory to use type double for this attribute
        public double RotationAngleX;
        [StructuresField("OBJECT_LOCKED")] // It is mandatory to use type integer for this attribute
        public int Locked;
        [StructuresField("group_no")] // It is mandatory to use type integer for this attribute
        public int Class;
        [StructuresField("joint_code")] // It is mandatory to use type string for this attribute
        public string ConnectionCode;
        [StructuresField("ad_root")] // It is mandatory to use type string for this attribute
        public string AutoDefaults;
        [StructuresField("ac_root")] // It is mandatory to use type string for this attribute
        public string AutoConnection;
    }
}