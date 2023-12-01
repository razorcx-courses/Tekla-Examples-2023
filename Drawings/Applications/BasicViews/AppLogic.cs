using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
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

        public void Run(Data formData)
        {
            try
            {
                var selectedModelObjects = new TSMUI.ModelObjectSelector().GetSelectedObjects();
                selectedModelObjects.MoveNext();

                var currentObject = selectedModelObjects.Current;

                var part = currentObject as Part;
                if (part == null) return;

                var assembly = part.GetAssembly();
                if (assembly == null) return;

                var assemblyDrawing = new AssemblyDrawing(assembly.Identifier, "All-Views");
                var insert = assemblyDrawing.Insert();

                var views = new Dictionary<View.ViewTypes, View>();
                foreach (View view in assemblyDrawing.GetSheet().GetViews())
                {
                    views.Add(view.ViewType, view);
                }

                var sheet = assemblyDrawing.GetSheet();
                var height = sheet.Height;

                views.Keys.ToList().ForEach(type =>
                {
                    var view = views[type];

                    var x = 0;
                    var y = 500;

                    switch (type)
                    {
                        case View.ViewTypes.FrontView:
                            if (!formData.showFrontView)
                            {
                                //view.Origin = new Point(x, y, 0);
                                // view.Modify();
                                view.Delete();
                            }
                            break;
                        case View.ViewTypes.TopView:
                            if (!formData.showTopView)
                            {
                                //view.Origin = new Point(x, y, 0);
                                //view.Modify();
                                view.Delete();
                            }
                            break;
                        case View.ViewTypes.BottomView:
                            if (!formData.showBottomView)
                            {
                                //view.Origin = new Point(x, y, 0);
                                //view.Modify();
                                view.Delete();
                            }
                            break;
                        case View.ViewTypes.SectionView:
                            if (!formData.showSectionView)
                            {
                                //view.Origin = new Point(x, y, 0);
                                //view.Modify();
                                view.Delete();
                            }
                            break;
                        case View.ViewTypes.EndView:
                            if (!formData.showEndView)
                            {
                                //view.Origin = new Point(x, y, 0);
                                //view.Modify();
                                view.Delete();
                            }
                            break;

                    }
                });

                assemblyDrawing.Modify();

                _drawingHandler.SetActiveDrawing(assemblyDrawing);

                var pl = assemblyDrawing.PlaceViews();
                var save = _drawingHandler.SaveActiveDrawing();

            }
            catch (Exception exception)
            {
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