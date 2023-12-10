using System;
using System.Collections.Generic;
using System.Linq;
using Tekla.Structures.Drawing;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using Tekla.Structures.Solid;
using Part = Tekla.Structures.Drawing.Part;

//using Part = Tekla.Structures.Drawing.Part;

namespace DimensionCreator
{
    /// <summary>
    /// Represents a drawing processing class.
    /// </summary>
    public class DrawingProcessor
    {
        /// <summary>
        /// Process the drawing.
        /// </summary>
        public void ProcessDrawing()
        {
            // Initialize drawing components
            var drawingHandler = new DrawingHandler();
            var currentDrawing = drawingHandler.GetActiveDrawing();
            var sheet = currentDrawing.GetSheet();
            var views = sheet.GetViews().Cast<View>().ToList();

            // Process front view
            ProcessFrontView(views);

            // Process other views
            ProcessOtherViews(sheet);
        }

        /// <summary>
        /// Process the front view of the drawing.
        /// </summary>
        /// <param name="views">List of views in the drawing.</param>
        private void ProcessFrontView(List<View> views)
        {
            // Get the front view
            var frontView = views.FirstOrDefault(v => v.ViewType == View.ViewTypes.FrontView);

            // Get the drawing objects in the front view
            var objList = frontView?.GetObjects(new[] { typeof(DrawingObject) }).Cast<DrawingObject>().ToList();

            // Delete mark sets and dimensions
            DeleteMarkSetsAndDimensions(objList);

            // Process bolts
            ProcessBolts(frontView);
        }

        /// <summary>
        /// Delete mark sets and dimensions.
        /// </summary>
        /// <param name="objList">List of drawing objects.</param>
        private void DeleteMarkSetsAndDimensions(List<DrawingObject> objList)
        {
            // Check if there are drawing objects
            objList?.OfType<MarkSet>().ToList().ForEach(o => o.Delete());
            objList?.OfType<StraightDimensionSet>().ToList().ForEach(o => o.Delete());
        }

        /// <summary>
        /// Process bolts in the drawing.
        /// </summary>
        /// <param name="frontView">Front view of the drawing.</param>
        private void ProcessBolts(View frontView)
        {
            // Get the bolts in the front view
            var bolts = frontView?.GetObjects(new[] { typeof(Bolt) }).Cast<Bolt>().ToList();
            var bolt = bolts?.FirstOrDefault();

            // Process parts associated with the bolts
            ProcessParts(frontView);
        }

        /// <summary>
        /// Process views other than the front view.
        /// </summary>
        /// <param name="sheet">Sheet containing views.</param>
        private void ProcessOtherViews(ContainerView sheet)
        {
            // Get all drawing objects of type Part in the sheet
            var drawingObjectEnumerator = sheet.GetAllObjects(typeof(Part)).Cast<Part>();



            // Process each part in the sheet
            foreach (Part part in drawingObjectEnumerator)
            {
                // Process only FrontView parts
                if (part.GetView()?.ViewType == View.ViewTypes.FrontView)
                {
                    ProcessPart(part);
                }
            }
        }

        /// <summary>
        /// Process a specific part in the drawing.
        /// </summary>
        /// <param name="part">Part to be processed.</param>
        private void ProcessPart(Part part)
        {
            // Save the current work plane and set a new one based on part's display coordinate system
            var savePlane = new Model().GetWorkPlaneHandler().GetCurrentTransformationPlane();
            new Model().GetWorkPlaneHandler().SetCurrentTransformationPlane(new TransformationPlane(part.DisplayCoordinateSystem));

            var identifier = part.ModelIdentifier;
            var modelSideObject = new Model().SelectModelObject(identifier);

            var pointList = new PointList();
            var beam = modelSideObject as Beam;

            if (beam != null)
            {
                // Process dimensions, marks, and hole notes for the beam
                ProcessBeamDimensions(beam, part);
                ProcessBoltMark(part, beam);
                ProcessMark(part, beam);
                ProcessHoleNotes(part);
            }

            // Restore the original work plane
            new Model().GetWorkPlaneHandler().SetCurrentTransformationPlane(savePlane);
        }

        /// <summary>
        /// Process dimensions for the beam.
        /// </summary>
        /// <param name="beam">The beam in the drawing.</param>
        /// <param name="part">The part associated with the beam.</param>
        private void ProcessBeamDimensions(Beam beam, Part part)
        {
            var viewBase = part.GetView() as View;
            var attr = new StraightDimensionSet.StraightDimensionSetAttributes(part, "SRS-OVERALL");

            if (Math.Abs(beam.StartPoint.X - beam.EndPoint.X) > 0.1)
            {
                var pointList = new PointList { beam.StartPoint, beam.EndPoint };
                new StraightDimensionSetHandler().CreateDimensionSet(viewBase, pointList, new Vector(0, 450, 0), 450, attr);
            }

            var boltObjects = beam.GetBolts();
            var bolts = new List<BoltArray>();

            while (boltObjects.MoveNext())
            {
                bolts.Add(boltObjects.Current as BoltArray);
            }

            var orderedBolts = bolts
                .SelectMany(b => b.BoltPositions.OfType<Point>().OrderBy(p => p.X).ToList())
                .ToList();

            var orderedXBolts = orderedBolts.OrderByDescending(p => p.Y).ThenBy(p => p.X).ToList();

            var maxY = orderedXBolts.Max(b => b.Y);
            var minY = orderedXBolts.Min(b => b.Y);

            var firstBottomBolt = orderedXBolts.FirstOrDefault(p => p.Y == minY);
            var topBolts = orderedXBolts.Take(orderedXBolts.Count / 2).ToList();

            var boltGroupsOf3 = topBolts.Count % 3 == 0;

            var middleBoltPositions = topBolts
                .Skip(1)
                .Where((x, i) => i % 3 == 0)
                .ToList();

            var boltPointList = new PointList();

            boltPointList.Add(beam.StartPoint);
            middleBoltPositions.ForEach(b => boltPointList.Add(b));
            boltPointList.Add(beam.EndPoint);

            if (Math.Abs(beam.StartPoint.X - beam.EndPoint.X) > 0.1)
            {
                var extensionOffset = 300;
                var xDimensions = new StraightDimensionSetHandler()
                    .CreateDimensionSet(viewBase, boltPointList, new Vector(0, extensionOffset, 0), extensionOffset, attr);
            }
        }

        /// <summary>
        /// Process the bolt mark in the drawing.
        /// </summary>
        /// <param name="part">The part associated with the bolt mark.</param>
        /// <param name="beam">The beam associated with the bolt mark.</param>
        private void ProcessBoltMark(Part part, Beam beam)
        {
            var currentDrawing = new DrawingHandler().GetActiveDrawing();
            var boltMark = new Mark(part);

            boltMark.Attributes.LoadAttributes("SRS");
            boltMark.Attributes.Content.Clear();
            boltMark.Attributes.Content.Add(new TextElement($"{beam.GetBolts().Cast<BoltArray>.Count} HOLES DIA 13"));
            boltMark.Placing = new LeaderLinePlacing(beam.BoltArray.BoltPositions.First());

            var inPoint = new Point(beam.BoltArray.BoltPositions.First());
            inPoint.Y -= 150;
            inPoint.X += 100;
            boltMark.InsertionPoint = inPoint;
            boltMark.Insert();
        }

        /// <summary>
        /// Process a general mark for the given part.
        /// </summary>
        /// <param name="part">The part associated with the mark.</param>
        /// <param name="beam">The beam associated with the mark.</param>
        private void ProcessMark(Part part, Beam beam)
        {
            var mark = new Mark(part);
            mark.Attributes.LoadAttributes("SRS");
            mark.Attributes.Content.Clear();

            var font = new FontAttributes(DrawingColors.Black, 2.5, "Arial", false, true);
            mark.Attributes.Content.Add(new TextElement("SATELLITE RAIL SUPPORT BEAM - ", font));
            mark.Attributes.Content.Add(new PropertyElement(PropertyElement.PropertyElementType.PartMarkPropertyElementTypes.AssemblyPosition()));
            mark.Attributes.Content.Add(new TextElement(" - PUNCHING LAYOUT", font));
            mark.Attributes.Content.Add(new NewLineElement());

            var scale = new PropertyElement(PropertyElement.PropertyElementType.ViewLabelMarkPropertyElementTypes.Scale());
            mark.Attributes.Content.Add(new TextElement($"SCALE - {scale.Value}"));
            mark.Attributes.Content.Add(new PropertyElement(PropertyElement.PropertyElementType.ViewLabelMarkPropertyElementTypes.Scale()));
            mark.Attributes.Content.Add(new NewLineElement());

            mark.Attributes.Content.Add(new TextElement("TOLERANCE ON ALL HOLE POSITIONS IS 1.0mm"));
            mark.Attributes.Content.Add(new NewLineElement());
            mark.Attributes.Content.Add(new TextElement("TOLERANCE ON OVERALL LENGTH IS 1.5mm"));

            var intPoint = new Point(beam.StartPoint);
            intPoint.X += new Vector(beam.EndPoint - beam.StartPoint).GetLength() / 4;
            intPoint.Y += -350;

            mark.InsertionPoint = intPoint;
            mark.Placing = new PointPlacing();
            mark.Insert();
        }

        /// <summary>
        /// Process hole notes for the given part.
        /// </summary>
        /// <param name="part">The part associated with the hole notes.</param>
        private void ProcessHoleNotes(Part part)
        {
            var beam = part as Beam;

            var middleBoltPositions = beam.GetBoltArrays()
                .SelectMany(b => b.BoltPositions.OfType<Point>().OrderBy(p => p.X).ToList())
                .Skip(1)
                .Where((x, i) => i % 3 == 0)
                .ToList();

            foreach (Point point in middleBoltPositions)
            {
                var insertionPoint = point;
                insertionPoint.X -= 25;
                insertionPoint.Y += 200;

                var textAttributes = new Text.TextAttributes
                {
                    PreferredPlacing = PreferredPlacingTypes.PointPlacingType(),
                    Angle = 90
                };

                var text = new Text(part.GetView(), insertionPoint, "CL TOOL", new PointPlacing(), textAttributes);
                text.Insert();
            }

            // All holes general note
            var generalNote = new Text(part.GetView(), new Point(170, 100), "ALL HOLES TO BE\nDIAMETER 13mm", new PointPlacing());
            generalNote.Attributes.Frame = new Frame(FrameTypes.Rectangular, DrawingColors.Black);
            generalNote.Insert();
        }
    }

    public static class SheetExtensions
    {
        /// <summary>
        /// Get all objects of a specific type from the sheet.
        /// </summary>
        /// <typeparam name="T">Type of objects to retrieve.</typeparam>
        /// <param name="sheet">The sheet to get objects from.</param>
        /// <returns>List of objects of the specified type.</returns>
        public static List<T> Cast<T>(this ContainerView sheet)
        {
            // Initialize the list to store objects
            var objectsOfTypeT = new List<T>();

            // Get the enumerator for the specified type
            var drawingObjectEnumerator = sheet.GetAllObjects(typeof(T));

            // Iterate over the enumerator and add objects to the list
            while (drawingObjectEnumerator.MoveNext())
            {
                var currentObject = drawingObjectEnumerator.Current;
                if (currentObject is T typedObject)
                {
                    objectsOfTypeT.Add(typedObject);
                }
                else
                {
                    // Handle the case where the object is not of the expected type
                    throw new InvalidOperationException($"Unexpected type encountered: {currentObject.GetType().Name}");
                }
            }

            return objectsOfTypeT;
        }


        /// <summary>
        /// Get all bolts associated with the beam.
        /// </summary>
        /// <param name="beam">The beam to get bolts from.</param>
        /// <returns>List of bolts associated with the beam.</returns>
        public static List<BoltArray> GetBoltArrays(this Beam beam)
        {
            // Ensure the beam is not null
            if (beam == null)
            {
                throw new ArgumentNullException(nameof(beam));
            }

            // Get the bolts enumerator from the beam
            var boltObjects = beam.GetBolts();

            // Initialize the list to store bolts
            var boltsList = new List<BoltArray>();

            // Iterate over the enumerator and add bolts to the list
            while (boltObjects.MoveNext())
            {
                var currentObject = boltObjects.Current;
                if (currentObject is BoltArray boltArray)
                {
                    boltsList.Add(boltArray);
                }
                else
                {
                    // Handle the case where the object is not a BoltArray
                    throw new InvalidOperationException($"Unexpected type encountered: {currentObject.GetType().Name}");
                }
            }

            return boltsList;
        }

        public static List<BoltArray> CastTo<T>(this DrawingEnumerator enumerator)
        {
            // Ensure the enumerator is not null
            if (enumerator == null)
            {
                throw new ArgumentNullException(nameof(enumerator));
            }

            // Get the bolts enumerator from the beam

            // Initialize the list to store bolts
            var boltsList = new List<BoltArray>();

            // Iterate over the enumerator and add bolts to the list
            while (enumerator.MoveNext())
            {
                var currentObject = enumerator.Current;
                if (currentObject is DrawingObject)
                {
                    currentObject.
                    boltsList.Add(boltArray);
                }
                else
                {
                    // Handle the case where the object is not a BoltArray
                    throw new InvalidOperationException($"Unexpected type encountered: {currentObject.GetType().Name}");
                }
            }

            return boltsList;
        }
    }
}

