using System;
using System.Windows.Forms;
using Newtonsoft.Json;
using Tekla.Structures;
using Tekla.Structures.Plugins;
using Tekla.Structures.Model;

namespace SpliceConn
{
    // If the plug-in is inherited from ConnectionPluginBase, the dialog class name must be the same as plug-in name
    [Plugin("SpliceConnection")] //Name of the connection in the catalog
    [PluginUserInterface("SpliceConnectionForm")]
    [SecondaryType(ConnectionBase.SecondaryType.SECONDARYTYPE_ONE)]
    [AutoDirectionType(AutoDirectionTypeEnum.AUTODIR_BASIC)]
    [PositionType(PositionTypeEnum.COLLISION_PLANE)]
    public class SpliceConnection : ConnectionBase
    {
        private readonly StructuresData _structuresData;
        private readonly Model _model = new Model();

        public bool Debug = false;

        private readonly SpliceConnectionBuilder _spliceConnectionBuilder = new SpliceConnectionBuilder();

        #region Constructor
        public SpliceConnection(StructuresData data)
        {
            _structuresData = data;
        }
        #endregion

        #region Overriden methods
        public override bool Run()
        {
            try
            {
                var data = GetValuesFromDialog();

                return _spliceConnectionBuilder.Create(data);
            }

            catch (Exception exc)
            {
                MessageBox.Show(exc.ToString());
                return false;
            }

            finally
            {
                //only required for debugging as a windows forms app
                //automatic when used as a connection plugin
                _model.CommitChanges();
            }
        }
        #endregion

        /// <summary>
        /// Gets the values from the dialog and sets the default values if needed
        /// </summary>
        private Data GetValuesFromDialog()
        {
            var data = JsonConvert.DeserializeObject<Data>(JsonConvert.SerializeObject(_structuresData)) ?? new Data();

            if (Debug)
            {
                data.PlateLength = ConnectionSpliceDefaults.PlateLength;
                data.BoltStandard = ConnectionSpliceDefaults.BoltStandard;
                data.UpDirection = ConnectionSpliceDefaults.UpDirection;
                data.RotationAngleY = ConnectionSpliceDefaults.RotationAngleY;
                data.RotationAngleX = ConnectionSpliceDefaults.RotationAngleX;
                data.Locked = ConnectionSpliceDefaults.Locked;
                data.Class = ConnectionSpliceDefaults.Class;
                data.ConnectionCode = ConnectionSpliceDefaults.ConnectionCode;
                data.AutoDefaults = ConnectionSpliceDefaults.AutoDefaults;
                data.AutoConnection = ConnectionSpliceDefaults.AutoConnection;
            }
            else
            {
                if (IsDefaultValue(data.PlateLength) || data.PlateLength == 0)
                    data.PlateLength = ConnectionSpliceDefaults.PlateLength;
                if (IsDefaultValue(data.BoltStandard) || data.BoltStandard == "")
                    data.BoltStandard = ConnectionSpliceDefaults.BoltStandard;
                if (IsDefaultValue(data.UpDirection) || data.UpDirection == 0)
                    data.UpDirection = ConnectionSpliceDefaults.UpDirection;
                if (IsDefaultValue(data.RotationAngleY))
                    data.RotationAngleY = ConnectionSpliceDefaults.RotationAngleY;
                if (IsDefaultValue(data.RotationAngleX))
                    data.RotationAngleX = ConnectionSpliceDefaults.RotationAngleX;
                if (IsDefaultValue(data.Locked))
                    data.Locked = ConnectionSpliceDefaults.Locked;
                if (IsDefaultValue(data.Class))
                    data.Class = ConnectionSpliceDefaults.Class;
                if (IsDefaultValue(data.ConnectionCode))
                    data.ConnectionCode = ConnectionSpliceDefaults.ConnectionCode;
                if (IsDefaultValue(data.AutoDefaults) || data.AutoDefaults == "")
                    data.AutoDefaults = ConnectionSpliceDefaults.AutoDefaults;
                if (IsDefaultValue(data.AutoConnection) || data.AutoConnection == "")
                    data.AutoConnection = ConnectionSpliceDefaults.AutoConnection;
            }

            return data;
        }

  

        #region Debug method for windows app
        //this is required for future debugging plugin
        [STAThread]
        static void Main()
        {
            var pluginData = new StructuresData();
            var debugPlugin = new SpliceConnection(pluginData)
            {
                Debug = true
            };

            //var inputDefinitions = debugPlugin.DefineInput();
            //var form = new SpliceConnectionForm();
            //form.ShowDialog();
            debugPlugin.Run();
        }
        #endregion
    }
}
