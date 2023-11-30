using System;
using System.Windows.Forms;
using Newtonsoft.Json;
using Tekla.Structures;
using Tekla.Structures.Drawing;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using ModelObject = Tekla.Structures.Model.ModelObject;
using Part = Tekla.Structures.Model.Part;
using TSMUI = Tekla.Structures.Model.UI;
using View = Tekla.Structures.Drawing.View;

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

                AssemblyDrawing myDrawing = null;

                while (selectedModelObjects.MoveNext())
                {
                    var currentObject = selectedModelObjects.Current;

                    var drawingName = "Part basic views" + currentObject.Identifier;

                    var modelObjectCoordSys = GetCoordinateSystemOfSelectedObject(currentObject);
                    var modelObjectName = GetNameOfSelectedObject(currentObject);

                    // Creates new empty general arrangement drawing

                    var part = currentObject as Part;
                    var assy = part.GetAssembly();
                    myDrawing = new AssemblyDrawing(assy.Identifier, "All-Views");
                    var pl1 = myDrawing.PlaceViews();
                    //myDrawing = new GADrawing(drawingName, "standard");
                    myDrawing.Insert();

                    //if (formData.OpenDrawing)
                    //    _drawingHandler.SetActiveDrawing(myDrawing, true); // Open drawing in editor
                    //else
                    //    _drawingHandler.SetActiveDrawing(myDrawing, false); // Open drawing in invisible mode. When drawing is opened in invisible mode, it must always be saved and closed.

                    // Handle different model object types

                    //fetch parts
                    //var parts = _partFetcher.FetchParts(selectedModelObjects);

                    //_drawingHelper.CreateViews(modelObjectCoordSys, modelObjectName, myDrawing, parts, formData);

                    var pl2 = myDrawing.PlaceViews();
                    myDrawing.Modify();

                    myDrawing.CommitChanges();

                    myDrawing.Select();
                    var pl3 = myDrawing.PlaceViews();
                    myDrawing.Modify();
                    myDrawing.CommitChanges();

                    _drawingHandler.CloseActiveDrawing(true); // Save and close the active drawing
                }

                foreach (View view in myDrawing.GetSheet().GetViews())
                {
                    var viewDetails = JsonConvert.SerializeObject(view, Formatting.Indented);

                    var sheet = myDrawing.GetSheet();
                    var height = sheet.Height;

                    view.Origin = new Point(40, height-50, view.Origin.Z);
                    view.Name = "This is the BEAM name which can be changed with API";
                    //view.Attributes.PartialProfileLength = 100;
                    view.Attributes.Scale = 10;
                    view.Modify();
                    
                }

                myDrawing.CommitChanges();

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
        private static CoordinateSystem GetCoordinateSystemOfSelectedObject(ModelObject selectedModelObject)
        {
            CoordinateSystem modelObjectCoordSys;

            switch (selectedModelObject)
            {
                case Part part:
                    modelObjectCoordSys = part.GetCoordinateSystem();
                    break;
                case Assembly assembly:
                    modelObjectCoordSys = assembly.GetMainPart().GetCoordinateSystem();
                    break;
                case BaseComponent component:
                    modelObjectCoordSys = component.GetCoordinateSystem();
                    break;
                case Task task:
                    modelObjectCoordSys = new CoordinateSystem();
                    break;
                default:
                    modelObjectCoordSys = new CoordinateSystem();
                    break;
            }

            return modelObjectCoordSys;
        }

        private static string GetNameOfSelectedObject(ModelObject selectedModelObject)
        {
            string modelObjectName;

            switch (selectedModelObject)
            {
                case Part part:
                    modelObjectName = part.GetPartMark();
                    break;
                case Assembly assembly:
                    modelObjectName = assembly.AssemblyNumber.Prefix + assembly.AssemblyNumber.StartNumber;
                    break;
                case BaseComponent component:
                    modelObjectName = component.Name;
                    break;
                case Task task:
                    modelObjectName = task.Name;
                    break;
                default:
                    modelObjectName = "";
                    break;
            }

            return modelObjectName;
        }
    }
}