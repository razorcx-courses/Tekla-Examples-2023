using System;
using System.Windows.Forms;
using Tekla.Structures.Drawing;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Part = Tekla.Structures.Model.Part;
using TSMUI = Tekla.Structures.Model.UI;

namespace BasicViews
{
    public class AppLogic
    {
        private readonly Model _model = new Model();
        private readonly DrawingHandler _drawingHandler = new DrawingHandler();

        private DrawingHelper _drawingHelper = new DrawingHelper();
        private PartFetcher _partFetcher = new PartFetcher();

        public void Run(FormData formData)
        {
            var current = _model.GetWorkPlaneHandler().GetCurrentTransformationPlane(); // We use global transformation

            try
            {
                _model.GetWorkPlaneHandler().SetCurrentTransformationPlane(new TransformationPlane()); // We use global transformation
                var selectedModelObjects = new TSMUI.ModelObjectSelector().GetSelectedObjects();

                GADrawing myDrawing = null;

                while (selectedModelObjects.MoveNext())
                {
                    var drawingName = "Part basic views" + (selectedModelObjects.Current as Tekla.Structures.Model.ModelObject).Identifier;

                    GetCoordinateSystemAndNameOfSelectedObject(selectedModelObjects, out var modelObjectCoordSys, out var modelObjectName);

                    // Creates new empty general arrangement drawing
                    myDrawing = new GADrawing(drawingName, "standard");
                    myDrawing.Insert();

                    if (formData.OpenDrawing)
                        _drawingHandler.SetActiveDrawing(myDrawing, true); // Open drawing in editor
                    else
                        _drawingHandler.SetActiveDrawing(myDrawing, false); // Open drawing in invisible mode. When drawing is opened in invisible mode, it must always be saved and closed.

                    // Handle different model object types

                    //fetch parts
                    var parts = _partFetcher.FetchParts(selectedModelObjects);

                    _drawingHelper.CreateViews(modelObjectCoordSys, modelObjectName, myDrawing, parts, formData);

                    myDrawing.PlaceViews();

                    _drawingHandler.CloseActiveDrawing(true); // Save and close the active drawing
                }

                if (myDrawing != null && formData.OpenDrawing)
                    _drawingHandler.SetActiveDrawing(myDrawing);

                _model.GetWorkPlaneHandler().SetCurrentTransformationPlane(current); // return original transformation
            }
            catch (Exception exception)
            {
                _model.GetWorkPlaneHandler().SetCurrentTransformationPlane(current); // return original transformation
                MessageBox.Show(exception.ToString());
            }
        }

        /// <summary>
        /// Gets coordinate system of selected object
        /// Different model objects have different way of getting a proper coordinate system, eg. for Assemblies the main part coordinate system is used, and task has no coordinate system
        /// </summary>
        /// <param name="selectedModelObjects"></param>
        /// <param name="modelObjectCoordSys"></param>
        /// <param name="modelObjectName"></param>
        private static void GetCoordinateSystemAndNameOfSelectedObject(ModelObjectEnumerator selectedModelObjects, out CoordinateSystem modelObjectCoordSys, out string modelObjectName)
        {
            switch (selectedModelObjects.Current)
            {
                case Part part:
                    modelObjectCoordSys = part.GetCoordinateSystem();
                    modelObjectName = part.GetPartMark();
                    break;
                case Assembly assembly:
                    modelObjectCoordSys = assembly.GetMainPart().GetCoordinateSystem();
                    modelObjectName = assembly.AssemblyNumber.Prefix + assembly.AssemblyNumber.StartNumber;
                    break;
                case BaseComponent component:
                    modelObjectCoordSys = component.GetCoordinateSystem();
                    modelObjectName = component.Name;
                    break;
                case Task task:
                    modelObjectCoordSys = new CoordinateSystem();
                    modelObjectName = task.Name;
                    break;
                default:
                    modelObjectCoordSys = new CoordinateSystem();
                    modelObjectName = "";
                    break;
            }
        }
    }
}