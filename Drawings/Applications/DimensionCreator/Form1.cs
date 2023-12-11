using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Tekla.Structures;
using Tekla.Structures.Drawing;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model;
using ModelObject = Tekla.Structures.Model.ModelObject;
using Part = Tekla.Structures.Drawing.Part;
using Point = Tekla.Structures.Geometry3d.Point;
using View = Tekla.Structures.Drawing.View;

namespace DimensionCreator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void angleDimensionButton_Click(object sender, EventArgs e)
        {
            try
            {
                DimensionCreator.CreateAngleDimension();
            }
            catch (Exception) { }
        }

        private void radiusDimensionButton_Click(object sender, EventArgs e)
        {
            try { DimensionCreator.CreateRadiusDimension((double)this.distanceNumericUpDown.Value); }
            catch (Exception) { }
        }

        private void straightDimensionButton_Click(object sender, EventArgs e)
        {
            try { DimensionCreator.CreateStraightDimension((double)this.distanceNumericUpDown.Value); }
            catch (Exception) { }
        }

        private void curvedRadialDimensionButton_Click(object sender, EventArgs e)
        {
            try { DimensionCreator.CreateCurvedRadialDimension((double)this.distanceNumericUpDown.Value); }
            catch
            {
            }
        }

        private void curvedOrthogonalDimensionButton_Click(object sender, EventArgs e)
        {
            try
            {
                DimensionCreator.CreateCurvedOrthoDimension((double)this.distanceNumericUpDown.Value);
            }
            catch (Exception) { }
        }

        private void repeatCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            DimensionCreator.Repeat = !DimensionCreator.Repeat;
        }

        private void numberOfPointsToPickNumericUpDown_ValueChanged(object sender, EventArgs e)
        {
            DimensionCreator.Points = (int)this.numberOfPointsToPickNumericUpDown.Value;
        }

        private void quitButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var drawingHandler = new DrawingHandler();

            var currentDrawing = drawingHandler.GetActiveDrawing();

            var sheet = currentDrawing.GetSheet();

            var viewEnumerator = sheet.GetViews();

            var views = new List<View>();

            while (viewEnumerator.MoveNext())
            {
                views.Add(viewEnumerator.Current as View);
            }

            var frontView = views.FirstOrDefault(v => v.ViewType == View.ViewTypes.FrontView);

            var objs = frontView?.GetObjects(new[] { typeof(DrawingObject) });
            var objList = new List<DrawingObject>();
            while (objs.MoveNext())
            {
                objList.Add(objs.Current as DrawingObject);
            }

            //delete mark sets
            objList.Where(o => o is MarkSet).ToList().ForEach(o => o.Delete());

            //delete dimension
            objList.Where(o => o is StraightDimensionSet).ToList().ForEach(o => o.Delete());

            var drawingObjects = frontView?.GetObjects(new[] { typeof(Bolt) });

            var boltList = new List<Bolt>();
            while (drawingObjects.MoveNext())
            {
                boltList.Add(drawingObjects.Current as Bolt);
            }

            var bolt = boltList.FirstOrDefault();

            var drawingObjectEnumerator = sheet.GetAllObjects(typeof(Part));

            foreach (Part part in drawingObjectEnumerator)
            {
                var view = part.GetView() as View;
                if(view.ViewType != View.ViewTypes.FrontView) continue;

                var savePlane = new Model().GetWorkPlaneHandler().GetCurrentTransformationPlane();
                new Model().GetWorkPlaneHandler().SetCurrentTransformationPlane(new TransformationPlane(view.DisplayCoordinateSystem));

                var identifier = part.ModelIdentifier;
                var modelSideObject = new Model().SelectModelObject(identifier);

                //this wont work with beam end extensions
                //need to use actual part Solid and/or part centerline

                var pointList = new PointList();
                var beam = new Beam();
                var centerline = new List<Point>();

                if (modelSideObject is Beam)
                {
                    beam.Identifier = identifier;
                    beam.Select();

                    //pointList.Add(beam.StartPoint);
                    //pointList.Add(beam.EndPoint);

                    //var solid = beam.GetSolid(Solid.SolidCreationTypeEnum.NORMAL);
                    centerline = beam.GetCenterLine(true).OfType<Point>().ToList();

                    pointList.Add(centerline.First());
                    pointList.Add(centerline.Last());
                }




                var viewBase = part.GetView();
                var attr = new StraightDimensionSet.StraightDimensionSetAttributes(part, "SRS-OVERALL");

                if (Math.Abs(beam.StartPoint.X - beam.EndPoint.X) > 0.1)
                {
                    var xDimensions = new StraightDimensionSetHandler()
                        .CreateDimensionSet(viewBase, pointList, new Vector(0, 450, 0), 450, attr);
                    currentDrawing.CommitChanges();
                }

                var boltObjects = beam.GetBolts();

                var bolts = new List<BoltArray>();

                while (boltObjects.MoveNext())
                {
                    bolts.Add(boltObjects.Current as BoltArray);
                }

                var orderedBolts = bolts
                    .SelectMany(b =>
                    {
                        return b.BoltPositions.OfType<Point>().OrderBy(p => p.X).ToList();
                    })
                    .ToList();

                var orderedXBolts = orderedBolts.OrderByDescending(p => p.Y).ThenBy(p => p.X).ToList();

                var maxY = orderedXBolts.Max(b => b.Y);
                var minY = orderedXBolts.Min(b => b.Y);

                var firstBottomBolt = orderedXBolts.FirstOrDefault(p => p.Y == minY);

                var topBolts = orderedXBolts.Take(orderedXBolts.Count / 2).ToList(); //orderedXBolts.Where(b => Math.Round(b.Y, 1) - Math.Round(maxY, 1) < 0.1).ToList();

                var boltGroupsOf3 = topBolts.Count % 3 == 0;  //divisible by 3 so we have bolt groups of 3

                var middleBoltPositions = topBolts
                    .Skip(1) //start with the second bolt
                    .Where((x, i) => i % 3 == 0)
                    .ToList();

                var boltPointList = new PointList();

                //use beam centerline for start and end points of bolt dimensions
                boltPointList.Add(centerline.First());
                middleBoltPositions.ForEach(b => boltPointList.Add(b));
                boltPointList.Add(centerline.Last());

                //create bolt dimensions
                viewBase = part.GetView();
                attr = new StraightDimensionSet.StraightDimensionSetAttributes(part, "SRS");

                var ce = new ContainerElement { new TextElement("CL TOOL") };
                attr.LeftMiddleTag = ce;

                if (Math.Abs(beam.StartPoint.X - beam.EndPoint.X) > 0.1)
                {
                    var extensionOffset = 300;
                    var xDimensions = new StraightDimensionSetHandler()
                        .CreateDimensionSet(viewBase, boltPointList, new Vector(0, extensionOffset, 0), extensionOffset, attr);
                    currentDrawing.CommitChanges();
                }

                var marks = currentDrawing.GetSheet().GetAllObjects(typeof(Mark));
                //var nMarksBefore = marks.GetSize();

                var boltMark = new Mark(part);
                boltMark.Attributes.LoadAttributes("SRS");
                boltMark.Attributes.Content.Clear();
                //boltMark.Attributes.Content.Add(new PropertyElement(PropertyElement.PropertyElementType.BoltMarkPropertyElementTypes.HoleDiameter()));
                boltMark.Attributes.Content.Add(new TextElement($"{bolts.Count} HOLES DIA 13"));
                boltMark.Placing = new LeaderLinePlacing(firstBottomBolt);

                var inPoint = new Point(firstBottomBolt);
                inPoint.Y -= 150;
                inPoint.X += 100;
                boltMark.InsertionPoint = inPoint;
                boltMark.Insert();


                var mark = new Mark(part);
                mark.Attributes.LoadAttributes("SRS");
                mark.Attributes.Content.Clear();

                //var markAtt = new PropertyElement(PropertyElement.PropertyElementType.PartMarkPropertyElementTypes
                //    .AssemblyPosition());

                //var title = $"SATELLITE RAIL SUPPORT BEAM {markAtt.Value} - PUNCHING LAYOUT";
                var font = new FontAttributes(DrawingColors.Black, 2.5, "Arial", false, true);
                mark.Attributes.Content.Add(new TextElement("SATELLITE RAIL SUPPORT BEAM - ", font));

                //change this as required.  could use a user property or UDA or ???
                //add new TextElement as required
                mark.Attributes.Content.Add(new PropertyElement(PropertyElement.PropertyElementType.PartMarkPropertyElementTypes
                    .AssemblyPosition()));
                //mark.Attributes.Content.Add(new PropertyElement(PropertyElement.PropertyElementType.ViewLabelMarkPropertyElementTypes.DrawingName()));

                mark.Attributes.Content.Add(new TextElement(" - PUNCHING LAYOUT", font));

                mark.Attributes.Content.Add(new NewLineElement());
                var scale = new PropertyElement(PropertyElement.PropertyElementType.ViewLabelMarkPropertyElementTypes
                    .Scale());
                mark.Attributes.Content.Add(new TextElement($"SCALE - {scale.Value}"));
                mark.Attributes.Content.Add(new PropertyElement(PropertyElement.PropertyElementType.ViewLabelMarkPropertyElementTypes
                    .Scale()));

                //var profile = new PropertyElement(PropertyElement.PropertyElementType.PartMarkPropertyElementTypes
                //    .Profile());

                //mark.Attributes.Content.Add(new NewLineElement());
                //mark.Attributes.Content.Add(profile);

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

                foreach (Point point in middleBoltPositions)
                {
                    var insertionPoint = point;
                    insertionPoint.X -= 25;
                    insertionPoint.Y += 200;

                    var textAttributes = new Text.TextAttributes
                    {
                        // Here we create one Text with a LeaderLine attached to it.
                        // MyTextAttributes.PreferredPlacing = PreferredTextPlacingTypes.LeaderLinePlacingType();  // with leader line
                        PreferredPlacing = PreferredPlacingTypes.PointPlacingType(), //no leader line
                        Angle = 90 //rotate text 90 deg
                    };

                    // var text = new Text(view, insertionPoint, "CL TOOL",
                    //    new LeaderLinePlacing(new Point(10, 10)), textAttributes);

                    var text = new Text(view, insertionPoint, "CL TOOL", new PointPlacing(), textAttributes);
                    text.Insert();
                }

                //all holes general note
                var generalNote = new Text(sheet, new Point(170, 100), $@"ALL HOLES TO BE
DIAMETER 13mm", new PointPlacing());
                generalNote.Attributes.Frame = new Frame(FrameTypes.Rectangular, DrawingColors.Black);
                generalNote.Insert();

                new Model().GetWorkPlaneHandler().SetCurrentTransformationPlane(savePlane);

            }
        }
    }
}