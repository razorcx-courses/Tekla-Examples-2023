using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Z_TestingApp
{
    public static class ExtensionMethods
    {
        public static List<T> FindAllChildrenByType<T>(this Control control)
        {
            var controls = control.Controls.Cast<Control>().ToList();

            return controls
                .OfType<T>()
                .Concat(controls.SelectMany(FindAllChildrenByType<T>)).ToList();
        }
    }
}