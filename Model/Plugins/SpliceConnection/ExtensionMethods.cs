using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;

namespace SpliceConn
{
    public static class ExtensionMethods
    {
        public static T GetReportProperty<T>(this ModelObject modelObject, string property)
        {
            var stringValue = string.Empty;
            var intValue = 0;
            var doubleValue = 0.0;

            if (typeof(T) == typeof(string))
            {
                modelObject.GetReportProperty(property, ref stringValue);
                return (T)(stringValue as object);
            }
            if (typeof(T) == typeof(int))
            {
                modelObject.GetReportProperty(property, ref intValue);
                return (T)(intValue as object);
            }
            if (typeof(T) == typeof(double))
            {
                modelObject.GetReportProperty(property, ref doubleValue);
                return (T)(doubleValue as object);
            }

            return (T)(new object());
        }

        public static List<T> FindAllChildrenByType<T>(this Control control)
        {
            var controls = control.Controls.Cast<Control>().ToList();

            return controls
                .OfType<T>()
                .Concat(controls.SelectMany(FindAllChildrenByType<T>)).ToList();
        }

        public static void SetTransformationPlane(this Model model, CoordinateSystem coordinateSystem)
        {
            model.GetWorkPlaneHandler().SetCurrentTransformationPlane(new TransformationPlane(coordinateSystem));
        }

        public static void SetTransformationPlane(this Model model, TransformationPlane transformationPlane)
        {
            model.GetWorkPlaneHandler().SetCurrentTransformationPlane(transformationPlane);
        }

        public static TransformationPlane GetTransformationPlane(this Model model)
        {
            return model.GetWorkPlaneHandler().GetCurrentTransformationPlane(); 
        }
    }
}