using System;
using System.Collections;
using Tekla.Structures.Drawing;
using Tekla.Structures.Geometry3d;

namespace BasicViews
{
    public class DrawingHelper
    {
        private readonly CoordinateSystemHelper _coordinateSystemHelper = new CoordinateSystemHelper();

        /// <summary>
        /// Create all basic views
        /// </summary>
        /// <param name="modelObjectCoordSys"></param>
        /// <param name="modelObjectName"></param>
        /// <param name="myDrawing"></param>
        /// <param name="parts"></param>
        public void CreateViews(CoordinateSystem modelObjectCoordSys, string modelObjectName,
            GADrawing myDrawing, ArrayList parts, FormData formData)
        {
            if (formData.FrontView)
                AddView("Front view of " + modelObjectName, myDrawing, parts,
                    _coordinateSystemHelper.GetBasicViewsCoordinateSystemForFrontView(modelObjectCoordSys));

            if (formData.TopView)
                AddView("Top view of " + modelObjectName, myDrawing, parts,
                    _coordinateSystemHelper.GetBasicViewsCoordinateSystemForTopView(modelObjectCoordSys));

            if (formData.EndView)
                AddView("End view of " + modelObjectName, myDrawing, parts,
                    _coordinateSystemHelper.GetBasicViewsCoordinateSystemForEndView(modelObjectCoordSys));

            if (formData.RotatedView)
                AddRotatedView("3d view of " + modelObjectName, myDrawing, parts,
                    _coordinateSystemHelper.GetBasicViewsCoordinateSystemForFrontView(modelObjectCoordSys));
        }

        /// <summary>
        /// Add one basic view
        /// </summary>
        /// <param name="name"></param>
        /// <param name="myDrawing"></param>
        /// <param name="parts"></param>
        /// <param name="coordinateSystem"></param>
        private void AddView(string name, Drawing myDrawing, ArrayList parts, CoordinateSystem coordinateSystem)
        {
            var myView = new View(
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

            var frontView = new View(myDrawing.GetSheet(),
                coordinateSystem,
                displayCoordinateSystem,
                parts);

            frontView.Name = name;
            frontView.Insert();
        }
    }
}