using System.Collections.Generic;
using Tekla.Structures.Model.UI;
using Tekla.Structures.Plugins;

namespace BeamPlugin
{
    public class InputService
    {
        public List<PluginBase.InputDefinition> GetInputDefinitions()
        {
            var picker = new Picker();
            var inputDefinitions = new List<PluginBase.InputDefinition>();

            var point1 = picker.PickPoint();
            var point2 = picker.PickPoint();

            var input1 = new PluginBase.InputDefinition(point1);
            var input2 = new PluginBase.InputDefinition(point2);

            //Add inputs to InputDefinition list.
            inputDefinitions.Add(input1);
            inputDefinitions.Add(input2);

            return inputDefinitions;
        }
    }
}