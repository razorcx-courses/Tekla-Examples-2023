using System;
using System.Collections;
using System.Windows.Forms;
using Tekla.Structures.Drawing;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using ModelObject = Tekla.Structures.Model.ModelObject;
using Part = Tekla.Structures.Model.Part;
using TSMUI = Tekla.Structures.Model.UI;

namespace BasicViews
{
    public partial class BasicViews : Form
    {
        public BasicViews()
        {
            InitializeComponent();
        }

        private readonly Model _model = new Model();
        private readonly DrawingHandler _drawingHandler = new DrawingHandler();

        #region Coordinate system calculations

        readonly Vector _upDirection = new Vector(0.0, 0.0, 1.0);
        /// <summary>
        /// Gets part default front view coordinate system
        /// Gets coordinate system as it is defined in the TS core for part/component basic views, which is different than in singlepart/assembly drawings.
        /// </summary>
        /// <param name="objectCoordinateSystem"></param>
        /// <returns></returns>
        private CoordinateSystem GetBasicViewsCoordinateSystemForFrontView(CoordinateSystem objectCoordinateSystem)
        {
            var coordSystem = GetCoordinateSystem(objectCoordinateSystem);

            var tempVector = GetTempVector(objectCoordinateSystem, coordSystem);

            coordSystem.AxisX = tempVector.Cross(_upDirection).GetNormal();
            coordSystem.AxisY = _upDirection.GetNormal();

            return coordSystem;
        }
        
        /// <summary>
        /// Gets part default top view coordinate system
        /// Gets coordinate system as it is defined in the TS core for part/component basic views, which is different than in singlepart/assembly drawings.
        /// </summary>
        /// <param name="objectCoordinateSystem"></param>
        /// <returns></returns>
        private CoordinateSystem GetBasicViewsCoordinateSystemForTopView(CoordinateSystem objectCoordinateSystem)
        {
            var coordSystem = GetCoordinateSystem(objectCoordinateSystem);

            var tempVector = GetTempVector(objectCoordinateSystem, coordSystem);

            coordSystem.AxisX = tempVector.Cross(_upDirection);
            coordSystem.AxisY = tempVector;

            return coordSystem;
        }

        /// <summary>
        /// Gets part default end view coordinate system
        /// Gets coordinate system as it is defined in the TS core for part/component basic views, which is different than in singlepart/assembly drawings.
        /// </summary>
        /// <param name="objectCoordinateSystem"></param>
        /// <returns></returns>
        private CoordinateSystem GetBasicViewsCoordinateSystemForEndView(CoordinateSystem objectCoordinateSystem)
        {
            var coordSystem = GetCoordinateSystem(objectCoordinateSystem);

            var tempVector = GetTempVector(objectCoordinateSystem, coordSystem);

            coordSystem.AxisX = tempVector;
            coordSystem.AxisY = _upDirection;

            return coordSystem;
        }

        private Vector GetTempVector(CoordinateSystem objectCoordinateSystem, CoordinateSystem coordSystem)
        {
            var tempVector = (coordSystem.AxisX.Cross(_upDirection));

            if (tempVector == new Vector())
                tempVector = (objectCoordinateSystem.AxisY.Cross(_upDirection));
            return tempVector;
        }

        private static CoordinateSystem GetCoordinateSystem(CoordinateSystem objectCoordinateSystem)
        {
            return new CoordinateSystem
            {
                Origin = new Point(objectCoordinateSystem.Origin),
                AxisX = new Vector(objectCoordinateSystem.AxisX) * -1.0,
                AxisY = new Vector(objectCoordinateSystem.AxisY)
            };
        }

        #endregion

        /// <summary>
        /// Creates the basic views
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Create_click(object sender, EventArgs e)
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

                    if (openDrawings.Checked)
                        _drawingHandler.SetActiveDrawing(myDrawing, true); // Open drawing in editor
                    else
                        _drawingHandler.SetActiveDrawing(myDrawing, false); // Open drawing in invisible mode. When drawing is opened in invisible mode, it must always be saved and closed.

                    // Handle different model object types

                    var parts = new ArrayList();

                    switch (selectedModelObjects.Current)
                    {
                        case Part _:
                            parts.Add(selectedModelObjects.Current.Identifier);
                            break;
                        case Assembly assembly:
                            parts = GetAssemblyParts(assembly);
                            break;
                        case BaseComponent component:
                            parts = GetComponentParts(component);
                            break;
                        case Task task:
                            parts = GetTaskParts(task);
                            break;
                    }

                    CreateViews(modelObjectCoordSys, modelObjectName, myDrawing, parts);

                    myDrawing.PlaceViews();

                    _drawingHandler.CloseActiveDrawing(true); // Save and close the active drawing
                }

                if (myDrawing != null && openDrawings.Checked)
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

        #region Model object child part fetching
        /// <summary>
        /// Gets list of assembly parts 
        /// </summary>
        /// <param name="SelectedModelObjects"></param>
        /// <returns></returns>
        private static ArrayList GetAssemblyParts(Assembly assembly)
        {
            var parts = new ArrayList();
            var assemblyChildren = (assembly).GetSecondaries().GetEnumerator();

            parts.Add((assembly).GetMainPart().Identifier);

            while(assemblyChildren.MoveNext())
                parts.Add(((ModelObject)assemblyChildren.Current)?.Identifier);
            
            return parts;
        }

        /// <summary>
        /// Gets list of component parts
        /// </summary>
        /// <param name="SelectedModelObjects"></param>
        /// <returns></returns>
        private static ArrayList GetComponentParts(BaseComponent component)
        {
            var parts = new ArrayList();
            IEnumerator myChildren = component.GetChildren();

            while(myChildren.MoveNext())
                parts.Add(((ModelObject)myChildren.Current)?.Identifier);

            return parts;
        }

        /// <summary>
        /// Gets list of task parts
        /// </summary>
        /// <param name="TaskMembers"></param>
        /// <returns></returns>
        private static ArrayList GetTaskParts(Task task)
        {
            var parts = new ArrayList();

            var myMembers = task.GetChildren();
            
            while(myMembers.MoveNext())
            {
                switch (myMembers.Current)
                {
                    case Task current:
                        parts.AddRange(GetTaskParts(current));
                        break;
                    case Part _:
                        parts.Add(myMembers.Current.Identifier);
                        break;
                }
            }

            return parts;
        }

        #endregion
        
        /// <summary>
        /// Create all basic views
        /// </summary>
        /// <param name="modelObjectCoordSys"></param>
        /// <param name="modelObjectName"></param>
        /// <param name="myDrawing"></param>
        /// <param name="parts"></param>
        private void CreateViews(CoordinateSystem modelObjectCoordSys, string modelObjectName, 
            GADrawing myDrawing, ArrayList parts)
        {
            if(createFrontView.Checked)
                AddView("Front view of " + modelObjectName, myDrawing, parts, 
                    GetBasicViewsCoordinateSystemForFrontView(modelObjectCoordSys));

            if(createTopView.Checked)
                AddView("Top view of " + modelObjectName, myDrawing, parts, 
                    GetBasicViewsCoordinateSystemForTopView(modelObjectCoordSys));

            if(createEndView.Checked)
                AddView("End view of " + modelObjectName, myDrawing, parts,
                    GetBasicViewsCoordinateSystemForEndView(modelObjectCoordSys));

            if(create3dView.Checked)
                AddRotatedView("3d view of " + modelObjectName, myDrawing, parts, 
                    GetBasicViewsCoordinateSystemForFrontView(modelObjectCoordSys));
        }     

        /// <summary>
        /// Add one basic view
        /// </summary>
        /// <param name="name"></param>
        /// <param name="myDrawing"></param>
        /// <param name="parts"></param>
        /// <param name="coordinateSystem"></param>
        private void AddView(String name, Drawing myDrawing, ArrayList parts, CoordinateSystem coordinateSystem)
        {
            var myView = new Tekla.Structures.Drawing.View(
                myDrawing.GetSheet(), coordinateSystem, coordinateSystem, parts);

            myView.Name = name;
            myView.Insert();
       }
   
        /// <summary>
        /// Add rotated view
        /// </summary>
        /// <param name="name"></param>
        /// <param name="myDrawing"></param>
        /// <param name="parts"></param>
        /// <param name="coordinateSystem"></param>
        private void AddRotatedView(string name, Drawing myDrawing, ArrayList parts, CoordinateSystem coordinateSystem)
        {
            var displayCoordinateSystem = new CoordinateSystem();

            var rotationAroundX = MatrixFactory.Rotate(20.0 * Math.PI * 2.0 / 360.0, coordinateSystem.AxisX);
            var rotationAroundZ = MatrixFactory.Rotate(30.0 * Math.PI * 2.0 / 360.0, coordinateSystem.AxisY);

            var rotation = rotationAroundX * rotationAroundZ;

            displayCoordinateSystem.AxisX = new Vector(rotation.Transform(new Point(coordinateSystem.AxisX)));
            displayCoordinateSystem.AxisY = new Vector(rotation.Transform(new Point(coordinateSystem.AxisY)));
            
            var frontView = new Tekla.Structures.Drawing.View(myDrawing.GetSheet(),
                                                                                        coordinateSystem,
                                                                                        displayCoordinateSystem,
                                                                                        parts);
           
            frontView.Name = name;
            frontView.Insert();
       }
   }
}