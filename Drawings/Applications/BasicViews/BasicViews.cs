using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Tekla.Structures.Drawing;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Model.UI;
using View = Tekla.Structures.Drawing.View;

namespace BasicViews
{
    public partial class BasicViews : Form
    {
        private AppLogic _appLogic = new AppLogic();

        public BasicViews()
        {
            InitializeComponent();
            RegisterEventHandler();
        }

        private Events _events = new Events();
        private object _statusChangedEventHandlerLock = new object();

        public void RegisterEventHandler()
        {
            //_events.DrawingStatusChanged += Events_DrawingStatusChangedEvent;
            _events.DrawingStatusChanged += Events_DrawingUpdatedEvent;
            _events.Register();
        }

        private void Events_DrawingUpdatedEvent(Drawing drawing, Events.DrawingUpdateTypeEnum type)
        {
            lock (_statusChangedEventHandlerLock)
            {
            }
        }

        public void UnRegisterEventHandler()
        {
            _events.UnRegister();
        }

        //void Events_DrawingStatusChangedEvent()
        //{
        //    /* Make sure that the inner code block is running synchronously */
        //    lock (_statusChangedEventHandlerLock)
        //    {
        //        _events.
        //        System.Console.WriteLine("Selection changed event received.");
        //    }
        //}

        void Events_DrawingUpdatedEvent()
        {
            /* Make sure that the inner code block is running synchronously */
            lock (_statusChangedEventHandlerLock)
            {
                //_events.
                //    System.Console.WriteLine("Selection changed event received.");
            }
        }

        private void Create_click(object sender, EventArgs e)
        {
            var formData = new FormData
            {
                OpenDrawing = openDrawings.Checked,
                EndView = endViewShowCheckBox.Checked,
                FrontView = frontViewCheckBox.Checked,
                TopView = topViewShowCheckBox.Checked,
                RotatedView = create3dView.Checked
            };

            _appLogic.Run(formData);
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            var drawingHandler = new DrawingHandler();
            var drawing = drawingHandler.GetActiveDrawing();

            var trackBar = sender as TrackBar;

            foreach (View view in drawing.GetSheet().GetViews())
            {
                var viewDetails = JsonConvert.SerializeObject(view, Formatting.Indented);

                //var sheet = drawing.GetSheet();
                //var height = sheet.Height;

                var x = trackBar2.Value;
                var y = trackBar3.Value;
                var scale = trackBar1.Value;

                view.Origin = new Point(x, y, view.Origin.Z);
                view.Name = "This is the BEAM name which can be changed with API";
                //view.Attributes.PartialProfileLength = 100;
                view.Attributes.Scale = scale;
                view.Modify();

            }

            drawing.CommitChanges();
        }

        private void BasicViews_Load(object sender, EventArgs e)
        {
            var drawingHandler = new DrawingHandler();
            var drawing = drawingHandler.GetActiveDrawing();

            if (drawing == null) return;

            var sheet = drawing.GetSheet();
            var height = sheet.Height;

            trackBar3.Value = Convert.ToInt16(height) - 20;

        }

        private void anyTrackBar_Scroll(object sender, EventArgs e)
        {
            var drawingHandler = new DrawingHandler();
            var drawing = drawingHandler.GetActiveDrawing();

            var trackBar = sender as TrackBar;

            foreach (View view in drawing.GetSheet().GetViews())
            {
                var viewDetails = JsonConvert.SerializeObject(view, 
                    Formatting.Indented);

                var sheet = drawing.GetSheet();
                var height = sheet.Height;

                var x = trackBar2.Value;
                var y = trackBar3.Value;
                var scale = trackBar1.Maximum - trackBar1.Value;
                var minLength = trackBar5.Value;

                view.Origin = new Vector(x, y, view.Origin.Z);

                view.Attributes.Shortening.MinimumLength = minLength;

                view.Name = textBox1.Text;
                view.Attributes.Scale = scale;
                view.Modify();


                //var text = new Text(view, new Point(0, 0, 0), "Test Text");
                //text.Insert();

                ////removes the leader line - see example code below
                //text.Attributes.PreferredPlacing = PreferredTextPlacingTypes.PointPlacingType(); 

                //var json = JsonConvert.SerializeObject(text,
                //    Formatting.Indented);

                //text.MoveObjectRelative(new Vector(-200, 200, 0));
                //text.Modify();

            }

            //drawing.CommitChanges();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            var drawingHandler = new DrawingHandler();
            var drawing = drawingHandler.GetActiveDrawing();

            var views = drawing.GetSheet().GetViews();
            views.MoveNext();
            var view = views.Current as View;

            view.Name = textBox1.Text;

            view.Modify();
            drawing.CommitChanges();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //CutViewsInLineWithMain

            var drawingHandler = new DrawingHandler();
            var drawing = drawingHandler.GetActiveDrawing();

            var select = drawing.Select();

            drawing.GetIntegerUserProperties(out Dictionary<string, int> intValues);
            drawing.GetDoubleUserProperties(out Dictionary<string, double> doubleValues);
            drawing.GetStringUserProperties(out Dictionary<string, string> stringValues);

            var sheet = drawing.GetSheet();
            sheet.GetIntegerUserProperties(out Dictionary<string, int> intValuesS);
            sheet.GetDoubleUserProperties(out Dictionary<string, double> doubleValuesS);
            sheet.GetStringUserProperties(out Dictionary<string, string> stringValuesS);

            int v = -1;
            var success = drawing.GetUserProperty("d1.CutViewsInLineWithMain", ref v);

            var djson = JsonConvert.SerializeObject(drawing,
                Formatting.Indented);

            var loaded = drawing.Layout.LoadAttributes("Assembly11x17");

            var place = drawing.PlaceViews();


            var modify = drawing.Modify();

            var mSheet = sheet.Modify();

            var ajson = JsonConvert.SerializeObject(drawing.Layout,
                Formatting.Indented);

            var views = drawing.GetSheet().GetViews();
            views.MoveNext();
            var view = views.Current as View;

            var attributes = view.Attributes.LoadAttributes("Face-Beam");
            view.Modify();

            drawing.CommitChanges();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            var drawingHandler = new DrawingHandler();
            var drawing = drawingHandler.GetActiveDrawing();

            var views = drawing.GetSheet().GetViews();
            views.MoveNext();
            var view = views.Current as View;

            var json = JsonConvert.SerializeObject(view,
                Formatting.Indented);

            var viewObjects = view.GetAllObjects();
            viewObjects.Reset();
            var objectList = new List<DrawingObject>();

            while (viewObjects.MoveNext())
            {
                var viewObject = viewObjects.Current;
                objectList.Add(viewObject);
            }
        }

        private void domainUpDown1_SelectedItemChanged(object sender, EventArgs e)
        {
            var drawingHandler = new DrawingHandler();
            var drawing = drawingHandler.GetActiveDrawing();

            //var trackBar = sender as DomainUpDown;

            foreach (View view in drawing.GetSheet().GetViews())
            {
                //var viewDetails = JsonConvert.SerializeObject(view,
                //    Formatting.Indented);

                //var sheet = drawing.GetSheet();
                //var height = sheet.Height;

                //var x = trackBar2.Value;
                //var y = trackBar3.Value;
                var scale = trackBar1.Maximum - int.Parse(domainUpDown1.Text);
                //var minLength = trackBar5.Value;

                //view.Origin = new Vector(x, y, view.Origin.Z);

                //view.Attributes.Shortening.MinimumLength = minLength;

                //view.Name = textBox1.Text;
                view.Attributes.Scale = scale;
                view.Modify();
            }
        }

        private void domainUpDown2_SelectedItemChanged(object sender, EventArgs e)
        {
            var control = sender as DomainUpDown;
            if (control == null) return;

            var drawingHandler = new DrawingHandler();
            var drawing = drawingHandler.GetActiveDrawing();

            foreach (View view in drawing.GetSheet().GetViews())
            {
                var minLength = int.Parse(control.Text);
                view.Attributes.Shortening.MinimumLength = minLength;
                view.Modify();
            }
        }

        private void domainUpDown3_SelectedItemChanged(object sender, EventArgs e)
        {
            var drawingHandler = new DrawingHandler();
            var drawing = drawingHandler.GetActiveDrawing();

            foreach (View view in drawing.GetSheet().GetViews())
            {
                if(view.ViewType != View.ViewTypes.FrontView) continue;

                var x = int.Parse(domainUpDown4.Text);
                var y = int.Parse(domainUpDown3.Text);

                view.Origin = new Vector(x, y, view.Origin.Z);
                view.Modify();
            }
        }

        private void domainUpDown4_SelectedItemChanged(object sender, EventArgs e)
        {
            var drawingHandler = new DrawingHandler();
            var drawing = drawingHandler.GetActiveDrawing();

            foreach (View view in drawing.GetSheet().GetViews())
            {
                if (view.ViewType != View.ViewTypes.FrontView) continue;

                var x = int.Parse(domainUpDown4.Text);
                var y = int.Parse(domainUpDown3.Text);

                view.Origin = new Vector(x, y, view.Origin.Z);
                view.Modify();
            }
        }
    }
}

//using Tekla.Structures.Drawing;
//using Point = Tekla.Structures.Geometry3d.Point;

//public class Example
//{
//    public void Example1()
//    {
//        DrawingHandler DrawHandler = new DrawingHandler();
//        Drawing CurrentDrawing = DrawHandler.GetActiveDrawing();
//        ContainerView Sheet = CurrentDrawing.GetSheet();
//        DrawingObjectEnumerator AllViews = Sheet.GetViews();
//        while (AllViews.MoveNext())
//        {
//            if (AllViews.Current is View)
//            {
//                View CurrentView = (View)AllViews.Current;

//                Point MyInsertionPoint = new Point(200, 400);
//                Text.TextAttributes MyTextAttributes = new Text.TextAttributes();

//                // Here we create one Text with a specific placing given to it.
//                // Please note that should the object be arranged (using the command Arrange Drawing Objects (Freeplace)),
//                // then it will use the PreferredPlacingType specified in the attributes (which in this case is the same).
//                MyTextAttributes.PreferredPlacing = PreferredTextPlacingTypes.LeaderLinePlacingType();
//                Text MyText = new Text(CurrentView, MyInsertionPoint, "Text with a LeaderLine.",
//                                       new LeaderLinePlacing(new Point(100, 100)), MyTextAttributes);
//                MyText.Insert();

//                // Here we create a text that is placed along a line with an arrow indicating the starting point of the line.
//                MyInsertionPoint = new Point(200, 600);
//                MyText = new Text(CurrentView, MyInsertionPoint, "Text with a LeaderLine.",
//                                  new LeaderLinePlacing(new Point(100, 100)));
//                MyText.Insert();
//                MyText.Attributes.PreferredPlacing = PreferredTextPlacingTypes.PointPlacingType();
//                MyText.TextString = "Text will now be without a LeaderLine.";
//                MyText.Modify();
//            }
//            else
//            {
//                AllViews = ((ContainerView)AllViews.Current).GetViews();
//            }
//        }
//    }
//}
