using System;
using System.Collections;
using System.Windows.Forms;
using Tekla.Structures;
using Tekla.Structures.Geometry3d;
using Tekla.Structures.Solid;
using TSM = Tekla.Structures.Model;
using Tekla.Structures.Model;

namespace ObjectTestApplication
{
    /// <summary>
    /// Summary description for Form1.
    /// </summary>
    public class Form1 : Form
    {
        private StatusBar _statusBar1;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container _components = null;
        private StatusBarPanel _statusBarPanel1;
        private StatusBarPanel _statusBarPanel2;
        private TextBox _textBox1;
        private static Model _model = new Model();
        private Button _button6;
        private CheckedListBox _checkedListBox1;
        private Button _button1;
        private static ArrayList _objectList = new ArrayList();

        public Form1()
        {
            //
            // Required for Windows Form Designer support
            //
            InitializeComponent();
            if (!_model.GetConnectionStatus())
            {
                WriteLine("Failed to connect to TS!");
                Environment.Exit(-1);
            }
            var info = _model.GetInfo();
            TeklaStructuresInfo.GetCurrentProgramVersion();
            Text = "ObjectTest with " + info.ModelName;

            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_components != null)
                {
                    _components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._statusBar1 = new System.Windows.Forms.StatusBar();
            this._statusBarPanel1 = new System.Windows.Forms.StatusBarPanel();
            this._statusBarPanel2 = new System.Windows.Forms.StatusBarPanel();
            this._textBox1 = new System.Windows.Forms.TextBox();
            this._button6 = new System.Windows.Forms.Button();
            this._checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this._button1 = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this._statusBarPanel1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._statusBarPanel2)).BeginInit();
            this.SuspendLayout();
            // 
            // statusBar1
            // 
            this._statusBar1.Location = new System.Drawing.Point(0, 523);
            this._statusBar1.Name = "_statusBar1";
            this._statusBar1.Panels.AddRange(new System.Windows.Forms.StatusBarPanel[] {
            this._statusBarPanel1,
            this._statusBarPanel2});
            this._statusBar1.ShowPanels = true;
            this._statusBar1.Size = new System.Drawing.Size(568, 25);
            this._statusBar1.TabIndex = 1;
            // 
            // statusBarPanel1
            // 
            this._statusBarPanel1.AutoSize = System.Windows.Forms.StatusBarPanelAutoSize.Spring;
            this._statusBarPanel1.Name = "_statusBarPanel1";
            this._statusBarPanel1.Text = "Objecttest started..";
            this._statusBarPanel1.Width = 447;
            // 
            // statusBarPanel2
            // 
            this._statusBarPanel2.Alignment = System.Windows.Forms.HorizontalAlignment.Right;
            this._statusBarPanel2.Name = "_statusBarPanel2";
            // 
            // textBox1
            // 
            this._textBox1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this._textBox1.Location = new System.Drawing.Point(0, 207);
            this._textBox1.MaxLength = 2147483647;
            this._textBox1.Multiline = true;
            this._textBox1.Name = "_textBox1";
            this._textBox1.ReadOnly = true;
            this._textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this._textBox1.Size = new System.Drawing.Size(568, 316);
            this._textBox1.TabIndex = 5;
            // 
            // button6
            // 
            this._button6.Location = new System.Drawing.Point(416, 28);
            this._button6.Name = "_button6";
            this._button6.Size = new System.Drawing.Size(144, 72);
            this._button6.TabIndex = 7;
            this._button6.Text = "Run Selected Tests";
            this._button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // checkedListBox1
            // 
            this._checkedListBox1.CheckOnClick = true;
            this._checkedListBox1.ColumnWidth = 140;
            this._checkedListBox1.Items.AddRange(new object[] {
            "All",
            "BeamTest",
            "PolyBeamTest",
            "ContourPlateTest",
            "BooleanTests",
            "RebarTests",
            "BoltTests",
            "WeldTest",
            "CastUnitTest",
            "AssemblyTest",
            "LoadTests",
            "PlaneTests",
            "SurfaceTreatmentTest",
            "EnumerationTest",
            "SolidTests",
            "ComponentTests"});
            this._checkedListBox1.Location = new System.Drawing.Point(19, 28);
            this._checkedListBox1.MultiColumn = true;
            this._checkedListBox1.Name = "_checkedListBox1";
            this._checkedListBox1.Size = new System.Drawing.Size(346, 157);
            this._checkedListBox1.TabIndex = 9;
            this._checkedListBox1.ThreeDCheckBoxes = true;
            // 
            // button1
            // 
            this._button1.Location = new System.Drawing.Point(416, 120);
            this._button1.Name = "_button1";
            this._button1.Size = new System.Drawing.Size(144, 65);
            this._button1.TabIndex = 10;
            this._button1.Text = "Clear Model";
            this._button1.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 15);
            this.ClientSize = new System.Drawing.Size(568, 548);
            this.Controls.Add(this._button1);
            this.Controls.Add(this._checkedListBox1);
            this.Controls.Add(this._button6);
            this.Controls.Add(this._textBox1);
            this.Controls.Add(this._statusBar1);
            this.Name = "Form1";
            this.Text = "Object Test Application for Tekla Open API";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this._statusBarPanel1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._statusBarPanel2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.Run(new Form1());
        }

        private void WriteLine(string text)
        {
            if (_textBox1.TextLength + text.Length < _textBox1.MaxLength)
            {
                _textBox1.AppendText(text);
                _textBox1.AppendText("\r\n");
            }
            _statusBarPanel1.Text = text;
            _statusBarPanel2.Text = _objectList.Count.ToString() + " Objects Inserted";
        }

        /// <summary>
        /// Method to test the creation and modification of a Beam object.
        /// </summary>
        private void BeamTest()
        {
            // Initialize variables
            double test = 0;

            // Output a message indicating the start of BeamTest
            WriteLine("Starting BeamTest...");

            // Create two points to define the start and end of the Beam
            var point = new Point(0, 0, 0);
            var point2 = new Point(1000, 0, 0);

            // Create a Beam object using the defined points
            var beam = new Beam(point, point2);

            // Set properties of the Beam object
            beam.Profile.ProfileString = "L60*6";
            beam.Material.MaterialString = "Steel_Undefined";
            beam.Finish = "PAINT";

            // Attempt to insert the Beam object into the model
            if (!beam.Insert())
            {
                // Output a failure message if the insertion fails
                WriteLine("BeamTest failed - unable to create beam");
                MessageBox.Show("Insert failed!");
                return;
            }
            else
            {
                // Add the successfully inserted Beam object to an object list
                _objectList.Add(beam);
            }

            // Retrieve and display specific properties of the Beam object
            if (beam.GetReportProperty("PROFILE.FLANGE_THICKNESS_1", ref test))
                WriteLine("PROFILE.FLANGE_THICKNESS_1 returned " + test.ToString());

            if (beam.GetReportProperty("PROFILE.FLANGE_THICKNESS_2", ref test))
                WriteLine("PROFILE.FLANGE_THICKNESS_2 returned " + test.ToString());

            // Output the identifier of the inserted Beam object
            WriteLine(beam.Identifier.ID + " Inserted");

            // Attempt to select the Beam object
            if (!beam.Select())
                MessageBox.Show("Select failed!");

            // Translate the start and end points of the Beam object
            beam.StartPoint.Translate(0, 1000, 0);
            beam.EndPoint.Translate(0, 1000, 0);

            // Attempt to modify the Beam object
            if (!beam.Modify())
                MessageBox.Show("Modify failed!");

            // Output a completion message for BeamTest
            WriteLine("BeamTest complete!");
        }


        /// <summary>
        /// Performs a test for PolyBeam creation and insertion.
        /// </summary>
        private void PolyBeamTest()
        {
            // Display message indicating the start of the PolyBeamTest.
            WriteLine("PolyBeamTest complete!");

            // Create ContourPoints for PolyBeam contour.
            var point = new ContourPoint(new Point(0, 2000, 0), null);
            var point2 = new ContourPoint(new Point(2000, 2000, 0), new Chamfer(0, 0, Chamfer.ChamferTypeEnum.CHAMFER_ARC_POINT));
            var point3 = new ContourPoint(new Point(0, 4000, 0), null);

            // Create a PolyBeam instance.
            var polyBeam = new PolyBeam();

            // Add ContourPoints to the PolyBeam.
            polyBeam.AddContourPoint(point);
            polyBeam.AddContourPoint(point2);
            polyBeam.AddContourPoint(point3);

            // Set properties of the PolyBeam.
            polyBeam.Profile.ProfileString = "HI400-15-20*400";
            polyBeam.Material.MaterialString = "Steel_Undefined";
            polyBeam.Finish = "PAINT";

            // Insert the PolyBeam and handle failure.
            if (!polyBeam.Insert())
            {
                WriteLine("PolyBeamTest failed - unable to create polybeam");
                MessageBox.Show("Insert failed!");
                return;
            }
            else
            {
                // Add the PolyBeam to the ObjectList.
                _objectList.Add(polyBeam);
            }

            // Select the PolyBeam and display completion message.
            if (!polyBeam.Select())
                MessageBox.Show("Select failed!");

            WriteLine("PolyBeamTest complete!");
        }

        /// <summary>
        /// Performs a test for ContourPlate creation and insertion.
        /// </summary>
        private void ContourPlateTest()
        {
            // Display message indicating the start of ContourPlateTest.
            WriteLine("Starting ContourPlateTest...");

            // Create ContourPoints for ContourPlate contour.
            var point = new ContourPoint(new Point(0, 4000, 0), null);
            var point2 = new ContourPoint(new Point(2000, 4000, 0), new Chamfer(0, 0, Chamfer.ChamferTypeEnum.CHAMFER_ARC_POINT));
            var point3 = new ContourPoint(new Point(0, 6000, 0), null);

            // Set specific properties for ContourPlate.
            point2.Chamfer.DZ1 = 2500;
            point2.Chamfer.DZ2 = 300;

            // Create a ContourPlate instance.
            var cp = new ContourPlate();

            // Add ContourPoints to the ContourPlate.
            cp.AddContourPoint(point);
            cp.AddContourPoint(point2);
            cp.AddContourPoint(point3);

            // Set properties of the ContourPlate.
            cp.Profile.ProfileString = "PL10";
            cp.Finish = "FOO";
            cp.Material.MaterialString = "Concrete_Undefined";
            cp.Name = "FOOSLAB";

            // Insert the ContourPlate and handle failure.
            if (!cp.Insert())
            {
                WriteLine("PolyBeamTest failed - unable to create polybeam");
                MessageBox.Show("Insert failed!");
                return;
            }
            else
            {
                // Add the ContourPlate to the ObjectList.
                _objectList.Add(cp);
            }

            // Select and modify the ContourPlate, then display completion message.
            if (!cp.Select())
                MessageBox.Show("Select failed!");

            if (!cp.Modify())
                MessageBox.Show("Modify failed!");

            WriteLine(cp.Identifier.ID + " Inserted");
            WriteLine("ContourPlateTest complete!");
        }

        /// <summary>
        /// Performs a test for BooleanPart creation and insertion.
        /// </summary>
        /// <returns>The ID of the created BooleanPart as a string.</returns>
        private string BooleanPartTest()
        {
            // Display message indicating the start of BooleanPartTest.
            WriteLine("Starting BooleanPartTest...");

            // Create points for Beam creation.
            var point = new Point(0, 7000, 0);
            var point2 = new Point(1000, 7000, 0);

            // Create the first Beam instance.
            var beam1 = new Beam();
            beam1.StartPoint = point;
            beam1.EndPoint = point2;
            beam1.Profile.ProfileString = "HI400-15-20*400";
            beam1.Material.MaterialString = "Steel_Undefined";
            beam1.Finish = "PAINT";

            // Insert Beam1 and handle failure.
            if (!beam1.Insert())
            {
                WriteLine("BooleanPartTest failed - unable to create beam1");
                MessageBox.Show("Insert failed!");
                return string.Empty;
            }
            else
            {
                // Add Beam1 to the ObjectList.
                _objectList.Add(beam1);
            }

            // Create the second Beam instance.
            var beam2 = new Beam();
            beam2.Profile.ProfileString = "HI100-10-10*100";
            beam2.Material.MaterialString = "Steel_Undefined";
            beam2.StartPoint = new Point(500, 6000, 0);
            beam2.EndPoint = new Point(500, 8000, 0);
            // set Class for operative part
            beam2.Class = BooleanPart.BooleanOperativeClassName;

            // Insert Beam2 and handle failure.
            if (!beam2.Insert())
            {
                WriteLine("BooleanPartTest failed - unable to create beam2");
                MessageBox.Show("Insert failed!");
                return string.Empty;
            }

            // Create a BooleanPart instance and set its properties.
            var beam = new BooleanPart();
            beam.Father = beam1;
            beam.SetOperativePart(beam2);

            // Insert BooleanPart and handle failure.
            if (!beam.Insert())
            {
                WriteLine("BooleanPartTest failed - unable to create boolean part");
                MessageBox.Show("Insert failed!");
                return string.Empty;
            }
            else
            {
                // Add BooleanPart to the ObjectList.
                _objectList.Add(beam);
            }

            // Delete Beam2, select BooleanPart, modify it, and display completion message.
            beam2.Delete();

            if (!beam.Select())
                MessageBox.Show("Select failed!");

            beam.OperativePart.Profile.ProfileString = "HI200-15-10*200";

            if (!beam.Modify())
                MessageBox.Show("Modify failed!");

            WriteLine("BooleanPartTest complete!");
            return beam.Identifier.ID.ToString();
        }

        /// <summary>
        /// Performs a test for creating and modifying a CutPlane.
        /// </summary>
        /// <returns>The ID of the created CutPlane as a string.</returns>
        private string CutTest()
        {
            // Display message indicating the start of CutTest.
            WriteLine("Starting CutTest...");

            // Create points for Beam creation.
            var point = new Point(5000, 5000, 0);
            var point2 = new Point(6000, 5000, 0);

            // Create the Beam instance.
            var beam = new Beam();
            beam.StartPoint = point;
            beam.EndPoint = point2;
            beam.Profile.ProfileString = "HI400-15-20*400";
            beam.Material.MaterialString = "Steel_Undefined";
            beam.Finish = "PAINT";

            // Insert the Beam and handle failure.
            if (!beam.Insert())
            {
                WriteLine("CutTest failed - unable to create beam");
                MessageBox.Show("Insert failed!");
                return string.Empty;
            }
            else
            {
                // Add the Beam to the ObjectList.
                _objectList.Add(beam);
            }

            // Create a CutPlane instance and set its properties.
            var cut = new CutPlane();
            cut.Father = beam;
            cut.Plane.Origin = new Point(5500, 0, 0);
            cut.Plane.AxisX = new Vector(0, 1.0, 0);
            cut.Plane.AxisY = new Vector(0, 0, 1.0);

            // Insert the CutPlane and handle failure.
            if (!cut.Insert())
            {
                WriteLine("CutTest failed - unable to create cut");
                MessageBox.Show("Insert failed!");
                return string.Empty;
            }
            else
            {
                // Add the CutPlane to the ObjectList.
                _objectList.Add(cut);
            }

            // Select the CutPlane and modify it, then display completion message.
            if (!cut.Select())
                MessageBox.Show("Select failed!");

            cut.Plane.AxisX = new Vector(0, 500, 0);
            cut.Plane.AxisY = new Vector(0, 0, 5000);

            if (!cut.Modify())
                MessageBox.Show("Modify failed!");

            WriteLine("CutTest complete!");
            return cut.Identifier.ID.ToString();
        }

        /// <summary>
        /// Performs a test for creating and modifying a Fitting.
        /// </summary>
        /// <returns>The ID of the created Fitting as a string.</returns>
        private string FittingTest()
        {
            // Display message indicating the start of FittingTest.
            WriteLine("Starting FittingTest...");

            // Create points for Beam creation.
            var point = new Point(5000, 6000, 0);
            var point2 = new Point(6000, 6000, 0);

            // Create the Beam instance.
            var beam = new Beam();
            beam.StartPoint = point;
            beam.EndPoint = point2;
            beam.Profile.ProfileString = "HI400-15-20*400";
            beam.Material.MaterialString = "Steel_Undefined";
            beam.Finish = "PAINT";

            // Insert the Beam and handle failure.
            if (!beam.Insert())
            {
                WriteLine("FittingTest failed - unable to create beam");
                MessageBox.Show("Insert failed!");
                return string.Empty;
            }
            else
            {
                // Add the Beam to the ObjectList.
                _objectList.Add(beam);
            }

            // Create a Fitting instance and set its properties.
            var fitting = new Fitting();
            fitting.Father = beam;
            fitting.Plane.Origin.X = 5800;
            fitting.Plane.Origin.Y = 5800;
            fitting.Plane.Origin.Z = -500;
            fitting.Plane.AxisX = new Vector(0, 6000, 1000);
            fitting.Plane.AxisY = new Vector(0, 0, 6000);

            // Insert the Fitting and handle failure.
            if (!fitting.Insert())
            {
                WriteLine("FittingTest failed - unable to create fitting");
                MessageBox.Show("Insert failed!");
                return string.Empty;
            }
            else
            {
                // Add the Fitting to the ObjectList.
                _objectList.Add(fitting);
            }

            // Select the Fitting and modify it, then display completion message.
            if (!fitting.Select())
                MessageBox.Show("Select failed!");

            fitting.Plane.AxisX = new Vector(0, 500, 0);
            fitting.Plane.AxisY = new Vector(3000, 0, 4000);

            if (!fitting.Modify())
                MessageBox.Show("Modify failed!");

            WriteLine("FittingTest complete!");
            return fitting.Identifier.ID.ToString();
        }

        private void SingleRebarTest()
        {
            WriteLine("Starting the SingleRebarTest...");

            var beam = new Beam(new Point(5000, 7000, 0), new Point(6000, 7000, 0));
            beam.Profile.ProfileString = "250*250";
            beam.Material.MaterialString = "Concrete_Undefined";
            beam.Finish = "PAINT";

            if (!beam.Insert())
            {
                WriteLine("SingleRebarTest failed - unable to create beam");
                MessageBox.Show("Insert beam failed!");
                return;
            }
            else
            {
                _objectList.Add(beam);
            }

            var minimumY = beam.GetSolid().MinimumPoint.Y;
            var minimumX = beam.GetSolid().MinimumPoint.X;
            var minimumZ = beam.GetSolid().MinimumPoint.Z;
            var maximumY = beam.GetSolid().MaximumPoint.Y;
            var maximumX = beam.GetSolid().MaximumPoint.X;
            var maximumZ = beam.GetSolid().MaximumPoint.Z;

            // 1st single 
            var polygon = new Polygon();
            polygon.Points.Add(new Point(minimumX, maximumY, maximumZ));
            polygon.Points.Add(new Point(maximumX, maximumY, maximumZ));

            var singleRebar = new SingleRebar();
            singleRebar.Polygon = polygon;
            singleRebar.Father = beam;
            singleRebar.Name = "SingleRebar1";
            singleRebar.Class = 9;
            singleRebar.Size = "12";
            singleRebar.NumberingSeries.StartNumber = 0;
            singleRebar.NumberingSeries.Prefix = "Single 1";
            singleRebar.Grade = "A500HW";
            singleRebar.OnPlaneOffsets.Add(25.00);
            singleRebar.FromPlaneOffset = 50;
            singleRebar.StartHook.Angle = -90;
            singleRebar.StartHook.Length = 10;
            singleRebar.StartHook.Radius = 10;
            singleRebar.StartHook.Shape = RebarHookData.RebarHookShapeEnum.CUSTOM_HOOK;
            singleRebar.EndHook.Angle = 90;
            singleRebar.EndHook.Length = 10;
            singleRebar.EndHook.Radius = 10;
            singleRebar.EndHook.Shape = RebarHookData.RebarHookShapeEnum.CUSTOM_HOOK;

            if (!singleRebar.Insert())
            {
                WriteLine("SingleRebarTest failed - unable to create single rebar");
                MessageBox.Show("Insert single rebar failed!");
                return;
            }
            else
            {
                _objectList.Add(singleRebar);
            }

            WriteLine(singleRebar.Identifier.ID.ToString());

            // 2nd single
            polygon.Points.Clear();
            polygon.Points.Add(new Point(minimumX, minimumY, maximumZ));
            polygon.Points.Add(new Point(maximumX, minimumY, maximumZ));
            var singleRebar2 = new SingleRebar();
            singleRebar2.Polygon = polygon;
            singleRebar2.Father = beam;
            singleRebar2.Name = "SingleRebar2";
            singleRebar2.Class = 8;
            singleRebar2.Size = "12";
            singleRebar2.NumberingSeries.StartNumber = 0;
            singleRebar2.NumberingSeries.Prefix = "Single 2";
            singleRebar2.Grade = "A500HW";
            singleRebar2.OnPlaneOffsets.Add(65.00);
            singleRebar2.OnPlaneOffsets.Add(65.00);
            singleRebar2.OnPlaneOffsets.Add(65.00);
            singleRebar2.FromPlaneOffset = -30;
            singleRebar2.StartPointOffsetType = Reinforcement.RebarOffsetTypeEnum.OFFSET_TYPE_COVER_THICKNESS;
            singleRebar2.StartPointOffsetValue = 15;
            singleRebar2.EndPointOffsetType = Reinforcement.RebarOffsetTypeEnum.OFFSET_TYPE_COVER_THICKNESS;
            singleRebar2.EndPointOffsetValue = 15;
            singleRebar2.StartHook.Angle = 50;
            singleRebar2.StartHook.Length = 50;
            singleRebar2.StartHook.Radius = 30;
            singleRebar2.StartHook.Shape = RebarHookData.RebarHookShapeEnum.CUSTOM_HOOK;
            singleRebar2.EndHook.Angle = 50;
            singleRebar2.EndHook.Length = 50;
            singleRebar2.EndHook.Radius = 30;
            singleRebar2.EndHook.Shape = RebarHookData.RebarHookShapeEnum.CUSTOM_HOOK;

            if (!singleRebar2.Insert())
            {
                WriteLine("SingleRebarTest failed - unable to create single rebar 2");
                MessageBox.Show("Insert single rebar 2 failed!");
                return;
            }
            else
            {
                _objectList.Add(singleRebar2);
            }

            WriteLine(singleRebar2.Identifier.ID.ToString());

            if (!singleRebar.Select())
                WriteLine("Select failed!");

            WriteLine(singleRebar.Identifier.ID.ToString());

            singleRebar.Class = 10;
            singleRebar.Name = "Modified single 1";

            if (!singleRebar.Modify())
                WriteLine("Modify failed!");

            WriteLine(singleRebar.Identifier.ID.ToString());
            WriteLine("SingleRebarTest complete!");
        }

        private static bool CompareRebarGeometries(RebarGeometry g1, RebarGeometry g2)
        {
            var points1 = g1.Shape.Points;
            var points2 = g2.Shape.Points;

            var result = points1.Count == points2.Count;

            for (var ii = 0; ii < points1.Count && result; ii++)
            {
                var point1 = points1[ii] as Point;
                var point2 = points2[ii] as Point;

                result = point1 != null && point2 != null &&
                        (Math.Abs(point1.X - point2.X) < Epsilon.Value) &&
                        (Math.Abs(point1.Y - point2.Y) < Epsilon.Value) &&
                        (Math.Abs(point1.Z - point2.Z) < Epsilon.Value);
            }

            return result;
        }

        /// <summary>
        /// Inserts a ContourPlate into the model and returns the created ContourPlate instance.
        /// </summary>
        /// <returns>The created ContourPlate instance or null if insertion fails.</returns>
        private static ContourPlate InsertPlate()
        {
            // Create a new ContourPlate instance.
            var cp = new ContourPlate();

            // Define contour points to form the shape of the ContourPlate.
            var point1 = new ContourPoint(new Point(0, 0, 0), null);
            var point2 = new ContourPoint(new Point(7200, 0, 0), null);
            var point3 = new ContourPoint(new Point(7200, 6000, 0), null);
            var point4 = new ContourPoint(new Point(0, 6000, 0), null);

            // Add contour points to the ContourPlate.
            cp.AddContourPoint(point1);
            cp.AddContourPoint(point2);
            cp.AddContourPoint(point3);
            cp.AddContourPoint(point4);

            // Set properties of the ContourPlate.
            cp.Profile.ProfileString = "PL200";
            cp.Material.MaterialString = "Concrete_Undefined";
            cp.Finish = "PAINT";

            // Attempt to insert the ContourPlate into the model.
            if (!cp.Insert())
            {
                // Display a message if insertion fails and return null.
                MessageBox.Show("Insert contour plate failed!");
                return null;
            }
            else
            {
                // Add the successfully inserted ContourPlate to the ObjectList.
                _objectList.Add(cp);
            }

            // Return the created ContourPlate instance.
            return cp;
        }


        private void RebarGroupTest1()
        {
            WriteLine("Starting the first RebarGroupTest...");

            var beam = new Beam(new Point(5000, 8000, 0), new Point(6000, 8000, 0));
            beam.Profile.ProfileString = "250*250";
            beam.Material.MaterialString = "Concrete_Undefined";
            beam.Finish = "PAINT";
            if (!beam.Insert())
            {
                WriteLine("RebarGroupTest1 failed - unable to create beam");
                MessageBox.Show("Insert beam failed!");
                return;
            }
            else
            {
                _objectList.Add(beam);
            }

            var minimumX = beam.GetSolid().MinimumPoint.X;
            var minimumY = beam.GetSolid().MinimumPoint.Y;
            var minimumZ = beam.GetSolid().MinimumPoint.Z;
            var maximumX = beam.GetSolid().MaximumPoint.X;
            var maximumY = beam.GetSolid().MaximumPoint.Y;
            var maximumZ = beam.GetSolid().MaximumPoint.Z;

            var polygon = new Polygon();
            polygon.Points.Add(new Point(minimumX, maximumY, minimumZ));
            polygon.Points.Add(new Point(minimumX, minimumY, minimumZ));
            polygon.Points.Add(new Point(minimumX, minimumY, maximumZ));
            polygon.Points.Add(new Point(minimumX, maximumY, maximumZ));

            var polygon2 = new Polygon();
            polygon2.Points.Add(new Point(maximumX, maximumY, minimumZ));
            polygon2.Points.Add(new Point(maximumX, minimumY, minimumZ));
            polygon2.Points.Add(new Point(maximumX, minimumY, maximumZ));
            polygon2.Points.Add(new Point(maximumX, maximumY, maximumZ));

            var rebarGroup = new RebarGroup();
            rebarGroup.Polygons.Add(polygon);
            rebarGroup.Polygons.Add(polygon2);
            rebarGroup.RadiusValues.Add(40.0);
            rebarGroup.SpacingType = BaseRebarGroup.RebarGroupSpacingTypeEnum.SPACING_TYPE_TARGET_SPACE;
            rebarGroup.Spacings.Add(30.0);
            rebarGroup.ExcludeType = BaseRebarGroup.ExcludeTypeEnum.EXCLUDE_TYPE_BOTH;
            rebarGroup.Father = beam;
            rebarGroup.Name = "RebarGroup";
            rebarGroup.Class = 3;
            rebarGroup.Size = "12";
            rebarGroup.NumberingSeries.StartNumber = 0;
            rebarGroup.NumberingSeries.Prefix = "Group";
            rebarGroup.Grade = "A500HW";
            rebarGroup.StartHook.Shape = RebarHookData.RebarHookShapeEnum.CUSTOM_HOOK;
            rebarGroup.StartHook.Angle = -90;
            rebarGroup.StartHook.Length = 3;
            rebarGroup.StartHook.Radius = 20;
            rebarGroup.EndHook.Shape = RebarHookData.RebarHookShapeEnum.CUSTOM_HOOK;
            rebarGroup.EndHook.Angle = -90;
            rebarGroup.EndHook.Length = 3;
            rebarGroup.EndHook.Radius = 20;
            rebarGroup.OnPlaneOffsets.Add(25.0);
            rebarGroup.OnPlaneOffsets.Add(10.0);
            rebarGroup.OnPlaneOffsets.Add(25.0);
            rebarGroup.StartPointOffsetType = Reinforcement.RebarOffsetTypeEnum.OFFSET_TYPE_COVER_THICKNESS;
            rebarGroup.StartPointOffsetValue = 20;
            rebarGroup.EndPointOffsetType = Reinforcement.RebarOffsetTypeEnum.OFFSET_TYPE_COVER_THICKNESS;
            rebarGroup.EndPointOffsetValue = 60;
            rebarGroup.FromPlaneOffset = 40;

            if (!rebarGroup.Insert())
            {
                WriteLine("RebarGroupTest1 failed - unable to create rebar group");
                MessageBox.Show("Cannot insert rebar group!");
                return;
            }
            else
            {
                _objectList.Add(rebarGroup);
            }

            var innerRebars = rebarGroup.GetRebarGeometries(false);
            rebarGroup.Name = "Modified Group 1";

            if (!rebarGroup.Modify())
                MessageBox.Show("Rebar group modify failed");

            WriteLine(rebarGroup.Identifier.ID.ToString());
            WriteLine("The first RebarGroupTest complete!");
        }

        private void RebarGroupTest2()
        {
            WriteLine("Starting the second RebarGroupTest...");

            var beam = new Beam(new Point(5000, 9000, 0), new Point(6000, 9000, 0));
            beam.Profile.ProfileString = "250*250";
            beam.Material.MaterialString = "Concrete_Undefined";
            beam.Finish = "PAINT";
            if (!beam.Insert())
            {
                WriteLine("RebarGroupTest2 failed - unable to create beam");
                MessageBox.Show("Insert beam failed!");
                return;
            }
            else
            {
                _objectList.Add(beam);
            }

            var minimumX = beam.GetSolid().MinimumPoint.X;
            var minimumY = beam.GetSolid().MinimumPoint.Y;
            var minimumZ = beam.GetSolid().MinimumPoint.Z;
            var maximumX = beam.GetSolid().MaximumPoint.X;
            var maximumY = beam.GetSolid().MaximumPoint.Y;
            var maximumZ = beam.GetSolid().MaximumPoint.Z;

            var polygon = new Polygon();
            polygon.Points.Add(new Point(minimumX, maximumY, minimumZ));
            polygon.Points.Add(new Point(minimumX, minimumY, minimumZ));
            polygon.Points.Add(new Point(minimumX, minimumY, maximumZ));
            polygon.Points.Add(new Point(minimumX, maximumY, maximumZ));

            var polygon2 = new Polygon();
            polygon2.Points.Add(new Point(maximumX - (maximumX - minimumX) / 2, maximumY, minimumZ));
            polygon2.Points.Add(new Point(maximumX - (maximumX - minimumX) / 2, minimumY, minimumZ));
            polygon2.Points.Add(new Point(maximumX - (maximumX - minimumX) / 2, minimumY, maximumZ));
            polygon2.Points.Add(new Point(maximumX - (maximumX - minimumX) / 2, maximumY, maximumZ));

            var polygon3 = new Polygon();
            polygon3.Points.Add(new Point(maximumX - 150, maximumY, minimumZ));
            polygon3.Points.Add(new Point(maximumX - 150, minimumY, minimumZ));
            polygon3.Points.Add(new Point(maximumX - 150, minimumY, maximumZ));
            polygon3.Points.Add(new Point(maximumX - 150, maximumY, maximumZ));

            var polygon4 = new Polygon();
            polygon4.Points.Add(new Point(maximumX, maximumY, minimumZ));
            polygon4.Points.Add(new Point(maximumX, minimumY, minimumZ));
            polygon4.Points.Add(new Point(maximumX, minimumY, maximumZ));
            polygon4.Points.Add(new Point(maximumX, maximumY, maximumZ));

            var rebarGroup = new RebarGroup();
            rebarGroup.Polygons.Add(polygon);
            rebarGroup.Polygons.Add(polygon2);
            rebarGroup.Polygons.Add(polygon3);
            rebarGroup.Polygons.Add(polygon4);
            rebarGroup.RadiusValues.Add(20.0);
            rebarGroup.SpacingType = BaseRebarGroup.RebarGroupSpacingTypeEnum.SPACING_TYPE_TARGET_SPACE;
            rebarGroup.Spacings.Add(40.0);
            rebarGroup.ExcludeType = BaseRebarGroup.ExcludeTypeEnum.EXCLUDE_TYPE_BOTH;
            rebarGroup.Father = beam;
            rebarGroup.Name = "RebarGroup";
            rebarGroup.Class = 4;
            rebarGroup.Size = "12";
            rebarGroup.NumberingSeries.StartNumber = 0;
            rebarGroup.NumberingSeries.Prefix = "Group2";
            rebarGroup.Grade = "A500HW";
            rebarGroup.OnPlaneOffsets.Add(30.0);
            rebarGroup.StartPointOffsetType = Reinforcement.RebarOffsetTypeEnum.OFFSET_TYPE_COVER_THICKNESS;
            rebarGroup.StartPointOffsetValue = 50;
            rebarGroup.EndPointOffsetType = Reinforcement.RebarOffsetTypeEnum.OFFSET_TYPE_COVER_THICKNESS;
            rebarGroup.EndPointOffsetValue = 50;
            rebarGroup.FromPlaneOffset = 20;

            if (!rebarGroup.Insert())
            {
                WriteLine("RebarGroupTest2 failed - unable to create RebarGroup");
                MessageBox.Show("Cannot insert rebar group!");
                return;
            }
            else
            {
                _objectList.Add(rebarGroup);
            }

            WriteLine(rebarGroup.Identifier.ID.ToString());

            rebarGroup.Name = "Modified Group 2";
            rebarGroup.SpacingType = BaseRebarGroup.RebarGroupSpacingTypeEnum.SPACING_TYPE_EXACT_NUMBER;
            rebarGroup.Spacings.Clear();
            rebarGroup.Spacings.Add(20.0);
            if (!rebarGroup.Modify()) MessageBox.Show("Rebar group modify failed");

            var innerRebars = rebarGroup.GetRebarGeometries(false);

            rebarGroup.Father = null;
            try
            {
                if (!rebarGroup.Modify()) WriteLine("Rebar group disconnected from father");
                WriteLine(rebarGroup.Identifier.ID.ToString());
            }
            catch 
            {
                WriteLine("Rebar group disconnected from father");
            }
            WriteLine("The second RebarGroupTest complete!");
        }

        private void RebarGroupTest3()
        {
            WriteLine("Starting the third RebarGroupTest...");

            var beamPoint = new Point(5000, 10000, 0);
            var beamPoint2 = new Point(6000, 10000, 0);

            var beam = new Beam();
            beam.StartPoint = beamPoint;
            beam.EndPoint = beamPoint2;
            beam.Profile.ProfileString = "250*250";
            beam.Material.MaterialString = "C50/60";
            beam.Finish = "PAINT";
            if (!beam.Insert())
            {
                WriteLine("RebarGroupTest3 failed - unable to create beam");
                MessageBox.Show("Insert beam failed!");
                return;
            }
            else
            {
                _objectList.Add(beam);
            }

            var minimumX = beam.GetSolid().MinimumPoint.X;
            var minimumY = beam.GetSolid().MinimumPoint.Y;
            var minimumZ = beam.GetSolid().MinimumPoint.Z;
            var maximumX = beam.GetSolid().MaximumPoint.X;
            var maximumY = beam.GetSolid().MaximumPoint.Y;
            var maximumZ = beam.GetSolid().MaximumPoint.Z;

            var polygon = new Polygon();
            var polygonPoint1 = new Point();
            polygonPoint1.X = minimumX;
            polygonPoint1.Y = minimumY;
            polygonPoint1.Z = maximumZ;
            polygon.Points.Add(polygonPoint1);
            var polygonPoint2 = new Point();
            polygonPoint2.X = minimumX;
            polygonPoint2.Y = minimumY;
            polygonPoint2.Z = minimumZ;
            polygon.Points.Add(polygonPoint2);
            var polygonPoint3 = new Point();
            polygonPoint3.X = minimumX;
            polygonPoint3.Y = maximumY;
            polygonPoint3.Z = minimumZ;
            polygon.Points.Add(polygonPoint3);
            var polygonPoint4 = new Point();
            polygonPoint4.X = minimumX;
            polygonPoint4.Y = maximumY;
            polygonPoint4.Z = maximumZ;
            polygon.Points.Add(polygonPoint4);

            var polygon2 = new Polygon();
            polygonPoint1 = new Point();
            polygonPoint1.X = minimumX + (maximumX - minimumX) / 2;
            polygonPoint1.Y = minimumY;
            polygonPoint1.Z = maximumZ - 100;
            polygon2.Points.Add(polygonPoint1);
            polygonPoint2 = new Point();
            polygonPoint2.X = minimumX + (maximumX - minimumX) / 2;
            polygonPoint2.Y = minimumY;
            polygonPoint2.Z = minimumZ;
            polygon2.Points.Add(polygonPoint2);
            polygonPoint3 = new Point();
            polygonPoint3.X = minimumX + (maximumX - minimumX) / 2;
            polygonPoint3.Y = maximumY;
            polygonPoint3.Z = minimumZ;
            polygon2.Points.Add(polygonPoint3);
            polygonPoint4 = new Point();
            polygonPoint4.X = minimumX + (maximumX - minimumX) / 2;
            polygonPoint4.Y = maximumY;
            polygonPoint4.Z = maximumZ - 100;
            polygon2.Points.Add(polygonPoint4);

            var polygon3 = new Polygon();
            polygonPoint1 = new Point();
            polygonPoint1.X = maximumX;
            polygonPoint1.Y = minimumY;
            polygonPoint1.Z = maximumZ;
            polygon3.Points.Add(polygonPoint1);
            polygonPoint2 = new Point();
            polygonPoint2.X = maximumX;
            polygonPoint2.Y = minimumY;
            polygonPoint2.Z = minimumZ;
            polygon3.Points.Add(polygonPoint2);
            polygonPoint3 = new Point();
            polygonPoint3.X = maximumX;
            polygonPoint3.Y = maximumY;
            polygonPoint3.Z = minimumZ;
            polygon3.Points.Add(polygonPoint3);
            polygonPoint4 = new Point();
            polygonPoint4.X = maximumX;
            polygonPoint4.Y = maximumY;
            polygonPoint4.Z = maximumZ;
            polygon3.Points.Add(polygonPoint4);

            var rebarGroup = new RebarGroup();
            rebarGroup.Polygons.Add(polygon);
            rebarGroup.Polygons.Add(polygon2);
            rebarGroup.Polygons.Add(polygon3);
            rebarGroup.RadiusValues.Add(50.0);
            rebarGroup.SpacingType = BaseRebarGroup.RebarGroupSpacingTypeEnum.SPACING_TYPE_EXACT_SPACE_FLEX_AT_MIDDLE;
            rebarGroup.Spacings.Add(20.0);
            rebarGroup.ExcludeType = BaseRebarGroup.ExcludeTypeEnum.EXCLUDE_TYPE_NONE;
            rebarGroup.Father = beam;
            rebarGroup.Name = "TestGroup 3";
            rebarGroup.Class = 7;
            rebarGroup.Size = "10";
            rebarGroup.NumberingSeries.StartNumber = 0;
            rebarGroup.NumberingSeries.Prefix = "Group 3";
            rebarGroup.Grade = "A500HW";
            rebarGroup.OnPlaneOffsets.Add(10.0);
            rebarGroup.OnPlaneOffsets.Add(20.0);
            rebarGroup.OnPlaneOffsets.Add(10.0);
            rebarGroup.FromPlaneOffset = 40;

            if (!rebarGroup.Insert())
            {
                WriteLine("RebarGroupTest3 failed - unable to create rebar group");
                MessageBox.Show("Cannot insert rebar group!");
                return;
            }
            else
            {
                _objectList.Add(rebarGroup);
            }

            WriteLine(rebarGroup.Identifier.ID.ToString());

            var innerRebars = rebarGroup.GetRebarGeometries(false);
            rebarGroup.Name = "Modified Group 3";

            if (!rebarGroup.Modify()) MessageBox.Show("Cannot Modify rebar group");

            WriteLine(rebarGroup.Identifier.ID.ToString());
            WriteLine("The third RebarGroupTest complete!");
        }

        private void RebarGroupTest4()
        {
            WriteLine("Starting the fourth RebarGroupTest...");

            var cp = InsertPlate();

            if (cp == null)
            {
                WriteLine("RebarGroupTest4 unable to create contour plate");
            }

            var polygon1 = new Polygon();
            polygon1.Points.Add(new Point(0, 0, 0));
            polygon1.Points.Add(new Point(0, 6000, 0));
            var polygon2 = new Polygon();
            polygon2.Points.Add(new Point(7200, 0, 0));
            polygon2.Points.Add(new Point(7200, 6000, 0));

            var rebarGroup = new RebarGroup();
            rebarGroup.Polygons.Add(polygon1);
            rebarGroup.Polygons.Add(polygon2);
            rebarGroup.RadiusValues.Add(50.0);
            rebarGroup.SpacingType = BaseRebarGroup.RebarGroupSpacingTypeEnum.SPACING_TYPE_TARGET_SPACE;
            rebarGroup.Spacings.Add(1200.0);
            rebarGroup.ExcludeType = BaseRebarGroup.ExcludeTypeEnum.EXCLUDE_TYPE_NONE;
            rebarGroup.Father = cp;
            rebarGroup.Name = "TestGroup 4";
            rebarGroup.Class = 7;
            rebarGroup.Size = "10";
            rebarGroup.NumberingSeries.StartNumber = 0;
            rebarGroup.NumberingSeries.Prefix = "Group 4";
            rebarGroup.Grade = "A500HW";
            rebarGroup.OnPlaneOffsets.Add(10.0);
            rebarGroup.FromPlaneOffset = -50;

            if (!rebarGroup.Insert())
            {
                WriteLine("RebarGroupTest4 failed - unable to create rebar group");
                MessageBox.Show("Cannot insert rebar group!");
                return;
            }
            else
            {
                _objectList.Add(rebarGroup);
            }

            WriteLine(rebarGroup.Identifier.ID.ToString());

            var innerRebars = rebarGroup.GetRebarGeometries(false);
            rebarGroup.Name = "Modified Group 4";

            if (!rebarGroup.Modify()) MessageBox.Show("Cannot Modify rebar group");

            WriteLine(rebarGroup.Identifier.ID.ToString());
            WriteLine("The fourth RebarGroupTest complete!");
        }

        private void RebarSpliceTest()
        {
            WriteLine("Starting the RebarSpliceTest...");
            var beam = new Beam(new Point(5000, 15000, 0), new Point(6000, 15000, 0));
            beam.Profile.ProfileString = "250*250";
            beam.Material.MaterialString = "Concrete_Undefined";
            beam.Finish = "PAINT";
            if (!beam.Insert())
            {
                WriteLine("RebarSpliceTest failed - unable to create beam");
                MessageBox.Show("Insert beam failed!");
                return;
            }
            else
            {
                _objectList.Add(beam);
            }

            var minimumX = beam.GetSolid().MinimumPoint.X;
            var minimumY = beam.GetSolid().MinimumPoint.Y;
            var minimumZ = beam.GetSolid().MinimumPoint.Z;
            var maximumX = beam.GetSolid().MaximumPoint.X;
            var maximumY = beam.GetSolid().MaximumPoint.Y;
            var maximumZ = beam.GetSolid().MaximumPoint.Z;
            var midX = (minimumX + maximumX) / 2.0;

            var polygon = new Polygon();
            polygon.Points.Add(new Point(minimumX, minimumY, maximumZ));
            polygon.Points.Add(new Point(minimumX, minimumY, minimumZ));
            polygon.Points.Add(new Point(midX, minimumY, minimumZ));

            var polygon2 = new Polygon();
            polygon2.Points.Add(new Point(minimumX, maximumY, maximumZ));
            polygon2.Points.Add(new Point(minimumX, maximumY, minimumZ));
            polygon2.Points.Add(new Point(midX, maximumY, minimumZ));

            var rebarGroup1 = new RebarGroup();
            rebarGroup1.Polygons.Add(polygon);
            rebarGroup1.Polygons.Add(polygon2);
            rebarGroup1.RadiusValues.Add(40.0);
            rebarGroup1.SpacingType = BaseRebarGroup.RebarGroupSpacingTypeEnum.SPACING_TYPE_TARGET_SPACE;
            rebarGroup1.Spacings.Add(30.0);
            rebarGroup1.ExcludeType = BaseRebarGroup.ExcludeTypeEnum.EXCLUDE_TYPE_BOTH;
            rebarGroup1.Father = beam;
            rebarGroup1.Name = "RebarGroup1";
            rebarGroup1.Class = 3;
            rebarGroup1.Size = "12";
            rebarGroup1.NumberingSeries.StartNumber = 0;
            rebarGroup1.NumberingSeries.Prefix = "Group";
            rebarGroup1.Grade = "A500HW";
            rebarGroup1.StartHook.Shape = RebarHookData.RebarHookShapeEnum.NO_HOOK;
            rebarGroup1.StartHook.Angle = -90;
            rebarGroup1.StartHook.Length = 3;
            rebarGroup1.StartHook.Radius = 20;
            rebarGroup1.EndHook.Shape = RebarHookData.RebarHookShapeEnum.NO_HOOK;
            rebarGroup1.EndHook.Angle = -90;
            rebarGroup1.EndHook.Length = 3;
            rebarGroup1.EndHook.Radius = 20;
            rebarGroup1.OnPlaneOffsets.Add(25.0);
            rebarGroup1.OnPlaneOffsets.Add(25.0);
            rebarGroup1.OnPlaneOffsets.Add(25.0);
            rebarGroup1.StartPointOffsetType = Reinforcement.RebarOffsetTypeEnum.OFFSET_TYPE_COVER_THICKNESS;
            rebarGroup1.StartPointOffsetValue = 20;
            rebarGroup1.EndPointOffsetType = Reinforcement.RebarOffsetTypeEnum.OFFSET_TYPE_COVER_THICKNESS;
            rebarGroup1.EndPointOffsetValue = 20;
            rebarGroup1.FromPlaneOffset = 40;

            if (!rebarGroup1.Insert())
            {
                WriteLine("RebarSpliceTest failed - unable to create rebar group 1");
                MessageBox.Show("Cannot insert rebar group 1!");
                return;
            }
            else
            {
                _objectList.Add(rebarGroup1);
            }

            var polygon3 = new Polygon();
            polygon3.Points.Add(new Point(midX, minimumY, minimumZ));
            polygon3.Points.Add(new Point(maximumX, minimumY, minimumZ));
            polygon3.Points.Add(new Point(maximumX, minimumY, maximumZ));

            var polygon4 = new Polygon();
            polygon4.Points.Add(new Point(midX, maximumY, minimumZ));
            polygon4.Points.Add(new Point(maximumX, maximumY, minimumZ));
            polygon4.Points.Add(new Point(maximumX, maximumY, maximumZ));

            var rebarGroup2 = new RebarGroup();
            rebarGroup2.Polygons.Add(polygon3);
            rebarGroup2.Polygons.Add(polygon4);
            rebarGroup2.RadiusValues.Add(40.0);
            rebarGroup2.SpacingType = BaseRebarGroup.RebarGroupSpacingTypeEnum.SPACING_TYPE_TARGET_SPACE;
            rebarGroup2.Spacings.Add(30.0);
            rebarGroup2.ExcludeType = BaseRebarGroup.ExcludeTypeEnum.EXCLUDE_TYPE_BOTH;
            rebarGroup2.Father = beam;
            rebarGroup2.Name = "RebarGroup2";
            rebarGroup2.Class = 3;
            rebarGroup2.Size = "12";
            rebarGroup2.NumberingSeries.StartNumber = 0;
            rebarGroup2.NumberingSeries.Prefix = "Group";
            rebarGroup2.Grade = "A500HW";
            rebarGroup2.StartHook.Shape = RebarHookData.RebarHookShapeEnum.NO_HOOK;
            rebarGroup2.StartHook.Angle = -90;
            rebarGroup2.StartHook.Length = 3;
            rebarGroup2.StartHook.Radius = 20;
            rebarGroup2.EndHook.Shape = RebarHookData.RebarHookShapeEnum.NO_HOOK;
            rebarGroup2.EndHook.Angle = -90;
            rebarGroup2.EndHook.Length = 3;
            rebarGroup2.EndHook.Radius = 20;
            rebarGroup2.OnPlaneOffsets.Add(25.0);
            rebarGroup2.OnPlaneOffsets.Add(25.0);
            rebarGroup2.OnPlaneOffsets.Add(25.0);
            rebarGroup2.StartPointOffsetType = Reinforcement.RebarOffsetTypeEnum.OFFSET_TYPE_COVER_THICKNESS;
            rebarGroup2.StartPointOffsetValue = 20;
            rebarGroup2.EndPointOffsetType = Reinforcement.RebarOffsetTypeEnum.OFFSET_TYPE_COVER_THICKNESS;
            rebarGroup2.EndPointOffsetValue = 20;
            rebarGroup2.FromPlaneOffset = 40;

            if (!rebarGroup2.Insert())
            {
                WriteLine("RebarSpliceTest failed - unable to create rebar group 2");
                MessageBox.Show("Cannot insert rebar group 2!");
                return;
            }
            else
            {
                _objectList.Add(rebarGroup2);
            }

            var rebarSplice = new RebarSplice(rebarGroup1, rebarGroup2);

            if (!rebarSplice.Insert())
            {
                WriteLine("RebarSpliceTest failed - unable to create rebar splice");
                MessageBox.Show("Cannot insert rebar splice!");
                return;
            }
            else
            {
                _objectList.Add(rebarSplice);
            }

            WriteLine(rebarSplice.Identifier.ID.ToString());
            if (!rebarSplice.Select()) WriteLine("Can not select rebar splice");
            WriteLine(rebarSplice.Identifier.ID.ToString());

            rebarSplice.LapLength = 300.0;
            rebarSplice.Type = RebarSplice.RebarSpliceTypeEnum.SPLICE_TYPE_LAP_BOTH;
            if (!rebarSplice.Modify()) MessageBox.Show("Can not modify rebar splice");

            WriteLine(rebarSplice.Identifier.ID.ToString());
            WriteLine("The RebarSpliceTest complete!");
        }

        /// <summary>
        /// Performs a test for creating and modifying welds.
        /// </summary>
        private void WeldTest()
        {
            WriteLine("Starting WeldTest...");

            // Define points for creating beams.
            var beam1P1 = new Point(0, 12000, 0);
            var beam1P2 = new Point(3000, 12000, 0);

            var beam2P1 = new Point(3000, 12000, 0);
            var beam2P2 = new Point(3000, 18000, 0);

            // Create two beams.
            var beam1 = new Beam(beam1P1, beam1P2);
            var beam2 = new Beam(beam2P1, beam2P2);

            // Set properties for Beam1.
            beam1.Profile.ProfileString = "HI400-15-20*400";
            beam1.Material.MaterialString = "Steel_Undefined";
            beam1.Finish = "PAINT";
            beam1.Name = "Beam 1";

            // Set properties for Beam2.
            beam2.Profile.ProfileString = "HI300-15-20*300";
            beam2.Material.MaterialString = "Steel_Undefined";
            beam2.Name = "Beam 2";

            // Insert Beam1 and handle failure.
            if (!beam1.Insert())
            {
                WriteLine("WeldTest failed - unable to create beam 1");
                MessageBox.Show("Insert failed!");
                return;
            }
            else
            {
                // Add Beam1 to the ObjectList.
                _objectList.Add(beam1);
            }

            // Insert Beam2 and handle failure.
            if (!beam2.Insert())
            {
                WriteLine("WeldTest failed - unable to create beam 2");
                MessageBox.Show("Insert failed!");
                return;
            }
            else
            {
                // Add Beam2 to the ObjectList.
                _objectList.Add(beam2);
            }

            // Display IDs of the inserted beams.
            WriteLine("Weld Beams Inserted, Ids " + beam1.Identifier.ID.ToString() + " " + beam2.Identifier.ID.ToString());

            // Create a Weld instance and set its properties.
            var weld = new Weld();
            weld.MainObject = beam1;
            weld.SecondaryObject = beam2;
            weld.TypeAbove = BaseWeld.WeldTypeEnum.WELD_TYPE_SQUARE_GROOVE_SQUARE_BUTT;
            weld.ShopWeld = true;

            // Insert the Weld and handle failure.
            if (!weld.Insert())
            {
                WriteLine("WeldTest failed - unable to create weld");
                MessageBox.Show("Insert failed!");
                return;
            }
            else
            {
                // Add the Weld to the ObjectList.
                _objectList.Add(weld);
            }

            // Display the ID of the inserted Weld.
            WriteLine(weld.Identifier.ID.ToString());

            // Attempt to select the Weld and display a message if selection fails.
            if (!weld.Select())
                WriteLine("Weld Select failed!");

            // Display the ID of the Weld again.
            WriteLine(weld.Identifier.ID.ToString());

            // Modify the properties of the Weld and display its ID.
            weld.LengthAbove = 12;
            weld.TypeBelow = BaseWeld.WeldTypeEnum.WELD_TYPE_SLOT;

            // Attempt to modify the Weld and display a message if modification fails.
            if (!weld.Modify())
                WriteLine("Weld Modify failed!");

            // Display the ID of the Weld after modification.
            WriteLine(weld.Identifier.ID.ToString());

            // Create a PolygonWeld instance and set its properties.
            var polyWeld = new PolygonWeld();
            polyWeld.MainObject = beam1;
            polyWeld.SecondaryObject = beam2;
            polyWeld.TypeAbove = BaseWeld.WeldTypeEnum.WELD_TYPE_SQUARE_GROOVE_SQUARE_BUTT;
            polyWeld.Polygon.Points.Add(new Point(3000, 12000, 0));
            polyWeld.Polygon.Points.Add(new Point(3000, 18000, 0));

            // Insert the PolygonWeld and handle failure.
            if (!polyWeld.Insert())
            {
                WriteLine("WeldTest failed - unable to create polyWeld");
                MessageBox.Show("Insert failed!");
                return;
            }
            else
            {
                // Add the PolygonWeld to the ObjectList.
                _objectList.Add(polyWeld);
            }

            // Display the ID of the inserted PolygonWeld.
            WriteLine(polyWeld.Identifier.ID.ToString());
            WriteLine("WeldTest complete!");
        }

        private void CastUnitTest()
        {
            WriteLine("Starting CastUnitTest...");

            var beam1 = new Beam(new Point(6000, 0, 0), new Point(9000, 0, 0));
            var beam2 = new Beam(new Point(9000, 0, 0), new Point(9000, 6000, 0));
            var beam3 = new Beam(new Point(9000, 6000, 0), new Point(12000, 6000, 0));
            var beam4 = new Beam(new Point(12000, 6000, 0), new Point(12000, 12000, 0));

            beam1.Profile.ProfileString = "RHS100*100*4";
            beam1.Name = "Beam 1";
            beam1.Material.MaterialString = "Concrete_Undefined";

            beam2.Profile.ProfileString = "RHS200*100*4";
            beam2.Name = "Beam 2";
            beam2.Material.MaterialString = "Concrete_Undefined";

            beam3.Profile.ProfileString = "RHS300*100*4";
            beam3.Name = "Beam 3";
            beam3.Material.MaterialString = "Concrete_Undefined";

            beam4.Profile.ProfileString = "RHS400*100*4";
            beam4.Name = "Beam 4";
            beam4.Material.MaterialString = "Concrete_Undefined";

            if (!beam1.Insert())
            {
                WriteLine("CastUnitTest failed - unable to create beam 1");
                MessageBox.Show("Insert beam 1 failed!");
                return;
            }
            else
            {
                _objectList.Add(beam1);
            }

            if (!beam2.Insert())
            {
                WriteLine("CastUnitTest failed - unable to create beam 2");
                MessageBox.Show("Insert beam 2 failed!");
                return;
            }
            else
            {
                _objectList.Add(beam2);
            }

            if (!beam3.Insert())
            {
                WriteLine("CastUnitTest failed - unable to create beam 3");
                MessageBox.Show("Insert beam 3 failed!");
                return;
            }
            else
            {
                _objectList.Add(beam3);
            }

            if (!beam4.Insert())
            {
                WriteLine("CastUnitTest failed - unable to create beam 4");
                MessageBox.Show("Insert beam 4 failed!");
                return;
            }
            else
            {
                _objectList.Add(beam4);
            }

            WriteLine("CastUnit Beams Inserted, Ids " + " " + beam1.Identifier.ID.ToString() + " " + beam2.Identifier.ID.ToString() + " " + beam3.Identifier.ID.ToString() + " " + beam4.Identifier.ID.ToString());

            var @as = beam1.GetAssembly();
            WriteLine(@as.Identifier.ID.ToString());
            @as.Add(beam2);

            if (!@as.Modify())
                WriteLine("CastUnit Insert Failed!");

            WriteLine(@as.Identifier.ID.ToString());

            if (!@as.Select())
                WriteLine("CastUnit Select Failed!");

            WriteLine(@as.Identifier.ID.ToString());

            @as.Remove(beam1);
            @as.Add(beam3);
            @as.Add(beam4);

            if (!@as.Modify())
                WriteLine("CastUnit Modify Failed!");

            WriteLine(@as.Identifier.ID.ToString());

            if (!@as.Select())
                WriteLine("CastUnit Select Failed!");

            WriteLine(@as.Identifier.ID.ToString());

            WriteLine("CastUnitTest complete!");
        }

        /// <summary>
        /// Performs a test for creating and modifying a ControlPlane.
        /// </summary>
        private void ControlPlaneTest()
        {
            WriteLine("Starting ControlPlaneTest...");

            // Create a ControlPlane instance.
            var controlPlane = new ControlPlane();

            // Create a Plane instance and set its properties.
            var plane = new Plane();
            plane.Origin = new Point(4500, 4500, 0);
            plane.AxisX = new Vector(-2000, 0, 0);
            plane.AxisY = new Vector(0, 5000, 0);

            // Set properties for the ControlPlane.
            controlPlane.Plane = plane;
            controlPlane.IsMagnetic = true;

            // Insert the ControlPlane and handle failure.
            if (!controlPlane.Insert())
            {
                WriteLine("ControlPlaneTest failed - unable to create ControlPlane");
                MessageBox.Show("Insert ControlPlane failed!");
                return;
            }
            else
            {
                // Add the ControlPlane to the ObjectList.
                _objectList.Add(controlPlane);
            }
            WriteLine(controlPlane.Identifier.ID.ToString());

            // Attempt to select the ControlPlane and display a message if selection fails.
            if (!controlPlane.Select())
                WriteLine("ControlPlane Select failed!");
            WriteLine(controlPlane.Identifier.ID.ToString());

            // Modify the properties of the ControlPlane and display its ID.
            controlPlane.Name = "Plane name changed";
            if (!controlPlane.Modify())
                WriteLine("ControlPlane Modify failed!");
            WriteLine(controlPlane.Identifier.ID.ToString());
            WriteLine("ControlPlaneTest complete!");
        }

        /// <summary>
        /// Performs a test for creating and modifying a Grid.
        /// </summary>
        private Grid GridTest()
        {
            WriteLine("Starting GridTest...");

            // Create a Grid instance.
            var grid = new Grid();

            // Set properties for the Grid.
            grid.CoordinateX = "0.00 4*3000.00";
            grid.CoordinateY = "0.00 5*6000.00";
            grid.CoordinateZ = "0.00 6000.00 8000.00 9000.00";
            grid.LabelX = "A B C D E";
            grid.LabelY = "1 2 3 4 5 6";
            grid.LabelZ = "+0 +6000 +8000 +9000";

            grid.ExtensionLeftX = 2000.0;
            grid.ExtensionLeftY = 2000.0;
            grid.ExtensionLeftZ = 2000.0;
            grid.ExtensionRightX = 2000.0;
            grid.ExtensionRightY = 2000.0;
            grid.ExtensionRightZ = 2000.0;
            grid.IsMagnetic = true;

            // Insert the Grid and handle failure.
            if (!grid.Insert())
            {
                WriteLine("GridTest failed - unable to create Grid");
                MessageBox.Show("Insert Grid failed!");
                return null;
            }
            else
            {
                // Add the Grid to the ObjectList.
                _objectList.Add(grid);
            }
            WriteLine(grid.Identifier.ID.ToString());

            // Attempt to select the Grid and display a message if selection fails.
            if (!grid.Select())
                WriteLine("Grid Select failed!");
            WriteLine(grid.Identifier.ID.ToString());

            // Modify the properties of the Grid and display its ID.
            grid.CoordinateX = "0.00 4*5000.00";
            grid.CoordinateY = "0.00 5*1000.00";
            grid.CoordinateZ = "0.00 3000.00 4000.00 4500.00";
            grid.LabelX = "X Y Z M N";
            grid.LabelY = "6 5 4 3 2 1";
            grid.LabelZ = "+0 +3000 +4000 +4500";
            grid.ExtensionLeftX = 5000.0;
            grid.ExtensionLeftY = 5000.0;
            grid.ExtensionLeftZ = 5000.0;
            grid.ExtensionRightX = 5000.0;
            grid.ExtensionRightY = 5000.0;
            grid.ExtensionRightZ = 5000.0;
            grid.IsMagnetic = false;

            // Attempt to modify the Grid and display a message if modification fails.
            if (!grid.Modify())
                WriteLine("Grid Modify failed!");
            WriteLine(grid.Identifier.ID.ToString());
            WriteLine("Grid complete!");

            return grid;
        }

        /// <summary>
        /// Performs a test for creating and modifying a GridPlane.
        /// </summary>
        private void GridPlaneTest()
        {
            var success = false;

            WriteLine("Starting GridPlaneTest...");

            // Get all objects in the model and find a Grid.
            var enumerator = _model.GetModelObjectSelector().GetAllObjects();

            while (!success && enumerator.MoveNext())
            {
                var modelObject = enumerator.Current as ModelObject;
                var objectType = modelObject.GetType();

                while (objectType != typeof(Grid) &&
                    objectType.BaseType != null)
                    objectType = objectType.BaseType;

                if (objectType == typeof(Grid))
                    success = true;
            }

            Grid grid = null;
            var gridPlane = new GridPlane();

            // If a Grid is found, set it as the parent for the GridPlane.
            if (success)
            {
                grid = enumerator.Current as Grid;
                gridPlane.Parent = grid;
            }
            else
            {
                success = false;
            }

            // Set properties for the GridPlane.
            gridPlane.Plane.Origin = new Point(1000.0, 0.0, 0.0);
            gridPlane.Plane.AxisX = new Vector(0.0, 2000.0, 0.0);
            gridPlane.Plane.AxisY = new Vector(0.0, 0.0, 5000.0);

            gridPlane.Label = "Muksu";
            gridPlane.IsMagnetic = true;
            gridPlane.DrawingVisibility = true;

            // If a Grid is found and the GridPlane is not inserted successfully, set Success to false.
            if (success && !gridPlane.Insert())
            {
                success = false;
                WriteLine("GridPlane Insert failed!");
            }
            else
                _objectList.Add(gridPlane);
            WriteLine(gridPlane.Identifier.ID.ToString());

            // If the GridPlane is selected successfully, display its ID.
            if (success && !gridPlane.Select())
            {
                success = false;
                WriteLine("GridPlane Select failed!");
            }
            WriteLine(gridPlane.Identifier.ID.ToString());

            // Modify the properties of the GridPlane and display its ID.
            gridPlane.Plane.Origin = new Point(1000.0, 0.0, 0.0);
            gridPlane.Plane.AxisX = new Vector(0.0, 6000.0, 0.0);
            gridPlane.Plane.AxisY = new Vector(0.0, 0.0, 2500.0);

            gridPlane.Label = "Tested";
            gridPlane.IsMagnetic = false;
            gridPlane.DrawingVisibility = false;

            // If the GridPlane is not modified successfully, set Success to false.
            if (success && !gridPlane.Modify())
            {
                success = false;
                WriteLine("GridPlane Modify failed!");
            }

            WriteLine(gridPlane.Identifier.ID.ToString());
            WriteLine("GridPlaneTest complete" + (success ? "d successfully " : "") + "!");
        }

        /// <summary>
        /// Performs a test for creating and modifying a SurfaceTreatment.
        /// </summary>
        private void SurfaceTreatmentTest()
        {
            WriteLine("Starting SurfaceTreatmentTest...");

            // Create a ContourPlate instance and define its contour points.
            var cp = new ContourPlate();
            cp.AddContourPoint(new ContourPoint(new Point(6000, 15000, 0), null));
            cp.AddContourPoint(new ContourPoint(new Point(9000, 15000, 0), null));
            cp.AddContourPoint(new ContourPoint(new Point(9000, 21000, 0), null));
            cp.AddContourPoint(new ContourPoint(new Point(10500, 24000, 0), null));
            cp.AddContourPoint(new ContourPoint(new Point(6000, 21000, 0), null));

            // Create a Contour instance and define its contour points with chamfer.
            var c = new Contour();
            c.AddContourPoint(new ContourPoint(new Point(6000, 15000, 5), null));
            c.AddContourPoint(new ContourPoint(new Point(9000, 15000, 5), null));
            c.AddContourPoint(new ContourPoint(new Point(9000, 21000, 5), new Chamfer(300, 300, Chamfer.ChamferTypeEnum.CHAMFER_ARC)));
            c.AddContourPoint(new ContourPoint(new Point(10500, 24000, 5), null));
            c.AddContourPoint(new ContourPoint(new Point(7500, 21000, 5), null));

            // Set properties for the ContourPlate.
            cp.Profile.ProfileString = "PL10";
            cp.Material.MaterialString = "Concrete_Undefined";

            // Insert the ContourPlate and handle failure.
            if (!cp.Insert())
            {
                WriteLine("SurfaceTreatmentTest failed - unable to create contour plate");
                MessageBox.Show("Insert ContourPlate failed!");
                return;
            }
            else
            {
                // Add the ContourPlate to the ObjectList.
                _objectList.Add(cp);
            }
            WriteLine("ContourPlate ID " + " " + cp.Identifier.ID.ToString());

            // Create a SurfaceTreatment instance and set its properties.
            var treatment = new SurfaceTreatment();
            treatment.Father = cp;
            treatment.Polygon = c;
            treatment.StartPoint = new Point(7500, 15000, 5);
            treatment.EndPoint = new Point(7500, 21000, 5);

            treatment.Position.Depth = Position.DepthEnum.MIDDLE;

            treatment.Name = "Treatment Test";
            treatment.Class = "66";
            treatment.Thickness = 40;
            treatment.Color = SurfaceTreatment.SurfaceColorEnum.CYAN;
            treatment.Type = SurfaceTreatment.SurfaceTypeEnum.TILE_SURFACE;
            treatment.Material.MaterialString = "Concrete_Undefined";

            // Insert the SurfaceTreatment and handle failure.
            if (!treatment.Insert())
            {
                WriteLine("SurfaceTreatmentTest failed - unable to create SurfaceTreatment");
                MessageBox.Show("Insert SurfaceTreatment failed!");
                return;
            }
            else
            {
                // Add the SurfaceTreatment to the ObjectList.
                _objectList.Add(treatment);
            }
            WriteLine(treatment.Identifier.ID.ToString());

            // Attempt to select the SurfaceTreatment and display a message if selection fails.
            if (!treatment.Select())
                WriteLine("SurfaceTreatment Select failed!");
            WriteLine(treatment.Identifier.ID.ToString());

            // Modify the properties of the SurfaceTreatment and display its ID.
            treatment.Name = "Name modified";
            treatment.Class = "1";
            treatment.Color = SurfaceTreatment.SurfaceColorEnum.RED;
            treatment.Type = SurfaceTreatment.SurfaceTypeEnum.SPECIAL_MIX;
            if (!treatment.Modify())
                WriteLine("Modify failed!");
            WriteLine(treatment.Identifier.ID.ToString());
            WriteLine("SurfaceTreatmentTest complete!");
        }

        /// <summary>
        /// Performs a test for creating and modifying an Assembly.
        /// </summary>
        private void AssemblyTest()
        {
            WriteLine("Starting AssemblyTest...");

            // Define points for beams.
            var beam1P1 = new Point(9000, 18000, 0);
            var beam1P2 = new Point(12000, 18000, 0);

            var beam2P1 = new Point(12000, 18000, 0);
            var beam2P2 = new Point(12000, 24000, 0);

            // Create Beam instances.
            var beam1 = new Beam(beam1P1, beam1P2);
            var beam2 = new Beam(beam2P1, beam2P2);

            // Set properties for Beam1.
            beam1.Profile.ProfileString = "HI400-15-20*400";
            beam1.Material.MaterialString = "Steel_Undefined";
            beam1.Finish = "PAINT";
            beam1.Name = "Beam 1";

            // Insert Beam1 and handle failure.
            if (!beam1.Insert())
            {
                WriteLine("AssemblyTest failed - unable to create beam 1");
                MessageBox.Show("Insert beam 1 failed!");
                return;
            }
            else
            {
                // Add Beam1 to the ObjectList.
                _objectList.Add(beam1);
            }

            // Set properties for Beam2.
            beam2.Profile.ProfileString = "HI300-15-20*300";
            beam2.Material.MaterialString = "Steel_Undefined";
            beam2.Name = "Beam 2";

            // Insert Beam2 and handle failure.
            if (!beam2.Insert())
            {
                WriteLine("AssemblyTest failed - unable to create beam 2");
                MessageBox.Show("Insert beam 2 failed!");
                return;
            }
            else
            {
                // Add Beam2 to the ObjectList.
                _objectList.Add(beam2);
            }

            WriteLine("Beams Inserted, Ids " + " " + beam1.Identifier.ID.ToString() + " " + beam2.Identifier.ID.ToString());

            // Create a Weld instance between Beam1 and Beam2.
            var weld = new Weld();
            weld.MainObject = beam1;
            weld.SecondaryObject = beam2;
            weld.TypeAbove = BaseWeld.WeldTypeEnum.WELD_TYPE_SQUARE_GROOVE_SQUARE_BUTT;
            weld.ShopWeld = true;

            // Insert the Weld and handle failure.
            if (!weld.Insert())
            {
                WriteLine("AssemblyTest failed - unable to create weld");
                MessageBox.Show("Insert weld failed!");
                return;
            }
            else
            {
                // Add the Weld to the ObjectList.
                _objectList.Add(weld);
            }
            WriteLine("Weld created, Id " + " " + weld.Identifier.ID.ToString());

            // Get the assembly containing Beam1.
            var assembly = beam1.GetAssembly();

            // Check if assembly is null and display its ID.
            if (assembly == null)
                WriteLine("GetAssembly failed!");
            WriteLine(assembly.Identifier.ID.ToString());

            // Modify the assembly's properties.
            assembly.AssemblyNumber.Prefix = "C";
            assembly.AssemblyNumber.StartNumber = 3050;
            assembly.Modify();
            WriteLine("AssemblyTest complete!");
        }

        /// <summary>
        /// Performs a test to retrieve the part marks for two beams.
        /// </summary>
        private void GetPartMarkTest()
        {
            WriteLine("Starting GetPartMark Test...");

            // Define points for Beam1 and Beam2.
            var beam1P1 = new Point(9500, 20000, 0);
            var beam1P2 = new Point(9500, 23000, 0);

            var beam2P1 = new Point(10500, 20000, 0);
            var beam2P2 = new Point(10500, 23000, 0);

            // Create Beam instances.
            var beam1 = new Beam(beam1P1, beam1P2);
            var beam2 = new Beam(beam2P1, beam2P2);

            // Set properties for Beam1.
            beam1.Profile.ProfileString = "HI400-15-20*400";
            beam1.Material.MaterialString = "Steel_Undefined";
            beam1.Finish = "PAINT";
            beam1.Name = "Beam 1";

            // Insert Beam1 and handle failure.
            if (!beam1.Insert())
            {
                WriteLine("GetPartMarkTest failed - unable to create beam 1");
                MessageBox.Show("Insert beam 1 failed!");
                return;
            }
            else
            {
                // Add Beam1 to the ObjectList.
                _objectList.Add(beam1);
            }

            // Set properties for Beam2.
            beam2.Profile.ProfileString = "HI300-15-20*300";
            beam2.Material.MaterialString = "Steel_Undefined";
            beam2.Finish = "PAINT";
            beam2.Name = "Beam 2";

            // Insert Beam2 and handle failure.
            if (!beam2.Insert())
            {
                WriteLine("GetPartMarkTest failed - unable to create beam 2");
                MessageBox.Show("Insert beam 2 failed!");
                return;
            }
            else
            {
                // Add Beam2 to the ObjectList.
                _objectList.Add(beam2);
            }

            WriteLine("Beams Inserted, Id " + " " + beam1.Identifier.ID.ToString() + " " + beam2.Identifier.ID.ToString());

            // Get part marks for Beam1 and Beam2.
            var mark = beam1.GetPartMark();
            var mark2 = beam2.GetPartMark();

            // Check if part marks are null and display them.
            if (mark == null || mark2 == null)
                WriteLine("GetParkMark failed!");
            else
                WriteLine("PartMark is " + " " + mark + "\\" + mark2);
            WriteLine("GetPartMark Test complete!");
        }

        /// <summary>
        /// Performs a test for creating and modifying a BoltArray.
        /// </summary>
        private void BoltArrayTest()
        {
            WriteLine("Starting BoltArrayTest...");

            // Create a ContourPlate that can be bolted to and insert it into the model.
            var cp = new ContourPlate();
            cp.Profile.ProfileString = "PL10";
            cp.Material.MaterialString = "Steel_Undefined";
            cp.AddContourPoint(new ContourPoint(new Point(0, 18000, 0), null));
            cp.AddContourPoint(new ContourPoint(new Point(1000, 18000, 0), null));
            cp.AddContourPoint(new ContourPoint(new Point(1000, 19000, 0), null));
            cp.AddContourPoint(new ContourPoint(new Point(0, 19000, 0), null));

            // Insert the ContourPlate and handle failure.
            if (!cp.Insert())
            {
                WriteLine("BoltArrayTest failed - unable to create contour plate");
                MessageBox.Show("Insert failed!");
                return;
            }
            else
            {
                _objectList.Add(cp);
            }

            WriteLine("ContourPlate ID " + " " + cp.Identifier.ID.ToString());

            // Create a BoltArray instance.
            var b = new BoltArray();

            // Set properties for the BoltArray.
            b.PartToBeBolted = cp;
            b.PartToBoltTo = cp;

            b.FirstPosition = new Point(0, 18000, 0);
            b.SecondPosition = new Point(1000, 19000, 0);

            b.BoltSize = 16;
            b.Tolerance = 3.00;
            b.BoltStandard = "7990";
            b.BoltType = BoltGroup.BoltTypeEnum.BOLT_TYPE_WORKSHOP;
            b.CutLength = 105;

            b.Length = 100;
            b.ExtraLength = 15;
            b.SlottedHoleX = 11;
            b.SlottedHoleY = 22;
            b.ThreadInMaterial = BoltGroup.BoltThreadInMaterialEnum.THREAD_IN_MATERIAL_NO;

            b.RotateSlots = BoltGroup.BoltRotateSlotsEnum.ROTATE_SLOTS_ODD;
            b.HoleType = BoltGroup.BoltHoleTypeEnum.HOLE_TYPE_OVERSIZED;

            // Set bolt-related properties.
            b.Bolt = true;
            b.Washer1 = true;
            b.Washer2 = true;
            b.Washer3 = true;
            b.Nut1 = true;
            b.Nut2 = true;

            // Set hole-related properties.
            b.Hole1 = true;
            b.Hole2 = true;
            b.Hole3 = true;
            b.Hole4 = true;
            b.Hole5 = true;

            // Add bolt distances.
            b.AddBoltDistX(100);
            b.AddBoltDistX(90);
            b.AddBoltDistX(80);

            b.AddBoltDistY(70);
            b.AddBoltDistY(60);
            b.AddBoltDistY(50);

            // Insert the BoltArray and handle failure.
            if (!b.Insert())
                WriteLine("BoltArray Insert Failed!");
            else
                _objectList.Add(b);
            WriteLine(b.Identifier.ID.ToString());

            // Create a new BoltArray instance for selection.
            var bSel = new BoltArray();
            bSel.Identifier = b.Identifier;

            // Select the BoltArray and handle failure.
            if (!bSel.Select())
                WriteLine("BoltArray Select failed!");
            WriteLine(bSel.Identifier.ID.ToString());

            // Modify properties of the BoltArray.
            b.FirstPosition = new Point(1000, 18000, 0);
            b.SecondPosition = new Point(0, 19000, 0);

            b.BoltSize = 20;
            b.Tolerance = 4.00;
            b.BoltStandard = "7990";
            b.BoltType = BoltGroup.BoltTypeEnum.BOLT_TYPE_SITE;
            b.CutLength = 155;

            // Modify the BoltArray and handle failure.
            if (!b.Modify())
                WriteLine("BoltArray Modify failed!");

            WriteLine(b.Identifier.ID.ToString());
            WriteLine("BoltArrayTest complete!");
        }

        /// <summary>
        /// Performs a test for creating and modifying a BoltXYList.
        /// </summary>
        private void BoltXyListTest()
        {
            WriteLine("Starting BoltXYListTest...");

            // Create a ContourPlate that can be bolted to and insert it into the model.
            var cp = new ContourPlate();
            cp.Profile.ProfileString = "PL10";
            cp.Material.MaterialString = "Steel_Undefined";
            cp.AddContourPoint(new ContourPoint(new Point(0, 19000, 0), null));
            cp.AddContourPoint(new ContourPoint(new Point(1000, 19000, 0), null));
            cp.AddContourPoint(new ContourPoint(new Point(1000, 20000, 0), null));
            cp.AddContourPoint(new ContourPoint(new Point(0, 20000, 0), null));

            // Insert the ContourPlate and handle failure.
            if (!cp.Insert())
            {
                WriteLine("BoltXYListTest failed - unable to create contour plate");
                MessageBox.Show("Insert failed!");
                return;
            }
            else
            {
                _objectList.Add(cp);
            }
            WriteLine("ContourPlate ID " + " " + cp.Identifier.ID.ToString());

            // Create a BoltXYList instance.
            var b = new BoltXYList();

            // Set properties for the BoltXYList.
            b.PartToBeBolted = cp;
            b.PartToBoltTo = cp;

            b.FirstPosition = new Point(0, 19000, 0);
            b.SecondPosition = new Point(1000, 20000, 0);

            b.BoltSize = 16;
            b.Tolerance = 3.00;
            b.BoltStandard = "7968";
            b.BoltType = BoltGroup.BoltTypeEnum.BOLT_TYPE_WORKSHOP;
            b.CutLength = 105;

            b.Length = 88;
            b.ExtraLength = 15;
            b.SlottedHoleX = 11;
            b.SlottedHoleY = 22;
            b.ThreadInMaterial = BoltGroup.BoltThreadInMaterialEnum.THREAD_IN_MATERIAL_NO;

            b.RotateSlots = BoltGroup.BoltRotateSlotsEnum.ROTATE_SLOTS_ODD;
            b.HoleType = BoltGroup.BoltHoleTypeEnum.HOLE_TYPE_OVERSIZED;

            // Set bolt-related properties.
            b.Bolt = true;
            b.Washer1 = true;
            b.Washer2 = true;
            b.Washer3 = true;
            b.Nut1 = true;
            b.Nut2 = true;

            // Set hole-related properties.
            b.Hole1 = true;
            b.Hole2 = true;
            b.Hole3 = true;
            b.Hole4 = true;
            b.Hole5 = true;

            // Add bolt distances.
            b.AddBoltDistX(100);
            b.AddBoltDistX(200);
            b.AddBoltDistX(300);

            b.AddBoltDistY(100);
            b.AddBoltDistY(200);
            b.AddBoltDistY(300);

            // Insert the BoltXYList and handle failure.
            if (!b.Insert())
                WriteLine("BoltXYList Insert Failed!");
            else
                _objectList.Add(b);
            WriteLine(b.Identifier.ID.ToString());

            // Create a new BoltXYList instance for selection.
            var bSel = new BoltXYList();
            bSel.Identifier = b.Identifier;

            // Select the BoltXYList and handle failure.
            if (!bSel.Select())
                WriteLine("BoltXYList Select failed!");
            WriteLine(bSel.Identifier.ID.ToString());

            // Modify properties of the BoltXYList.
            b.BoltSize = 20;
            b.Tolerance = 4.00;
            b.BoltStandard = "7990";
            b.BoltType = BoltGroup.BoltTypeEnum.BOLT_TYPE_SITE;
            b.CutLength = 155;

            // Modify the BoltXYList and handle failure.
            if (!b.Modify())
                WriteLine("BoltXYList Modify failed!");
            WriteLine(b.Identifier.ID.ToString());
            WriteLine("BoltXYListTest complete!");
        }


        private void BoltCircleTest()
        {
            WriteLine("Starting BoltCircleTest...");

            //Create for instance a contour plate that we can bolt to, insert it into the model
            var cp = new ContourPlate();
            cp.Profile.ProfileString = "PL10";
            cp.Material.MaterialString = "Steel_Undefined";
            cp.AddContourPoint(new ContourPoint(new Point(0, 20000, 0), null));
            cp.AddContourPoint(new ContourPoint(new Point(1000, 20000, 0), null));
            cp.AddContourPoint(new ContourPoint(new Point(1000, 21000, 0), null));
            cp.AddContourPoint(new ContourPoint(new Point(0, 21000, 0), null));

            if (!cp.Insert())
            {
                WriteLine("BoltCircleTest failed - unable to create contour plate");
                MessageBox.Show("Insert failed!");
                return;
            }
            else
            {
                _objectList.Add(cp);
            }
            WriteLine("ContourPlate ID " + " " + cp.Identifier.ID.ToString());

            var b = new BoltCircle();

            b.PartToBeBolted = cp;
            b.PartToBoltTo = cp;

            b.FirstPosition = new Point(0, 20000, 0);
            b.SecondPosition = new Point(1000, 21000, 0);

            b.BoltSize = 16;
            b.Tolerance = 3.00;
            b.BoltStandard = "7968";
            b.BoltType = BoltGroup.BoltTypeEnum.BOLT_TYPE_WORKSHOP;
            b.CutLength = 105;
            //B.StartPointOffset.Dx = 1;
            //B.EndPointOffset.Dx = 2;
            //B.StartPointOffset.Dy = 3;
            //B.EndPointOffset.Dy = 4;
            //B.StartPointOffset.Dz = 5;
            //B.EndPointOffset.Dz = 6;

            b.Length = 88;
            b.ExtraLength = 15;
            b.SlottedHoleX = 11;
            b.SlottedHoleY = 22;
            b.ThreadInMaterial = BoltGroup.BoltThreadInMaterialEnum.THREAD_IN_MATERIAL_NO;

            b.RotateSlots = BoltGroup.BoltRotateSlotsEnum.ROTATE_SLOTS_ODD;
            b.HoleType = BoltGroup.BoltHoleTypeEnum.HOLE_TYPE_OVERSIZED;

            //B.Position.Depth = Position.DepthEnum.MIDDLE;
            //B.Position.DepthOffset = 3;
            //B.Position.Plane = Position.PlaneEnum.MIDDLE;
            //B.Position.PlaneOffset = 1;
            //B.Position.Rotation = Position.RotationEnum.FRONT;
            //B.Position.RotationOffset = 2;

            b.Bolt = true;
            b.Washer1 = true;
            b.Washer2 = true;
            b.Washer3 = true;
            b.Nut1 = true;
            b.Nut2 = true;

            b.Hole1 = true;
            b.Hole2 = true;
            b.Hole3 = true;
            b.Hole4 = true;
            b.Hole5 = true;

            b.NumberOfBolts = 7;
            b.Diameter = 160;

            if (!b.Insert())
                WriteLine("BoltCircle Insert Failed!");
            else
                _objectList.Add(b);
            WriteLine(b.Identifier.ID.ToString());

            var bSel = new BoltCircle();
            bSel.Identifier = b.Identifier;

            if (!bSel.Select())
                WriteLine("BoltCircle Select failed!");
            WriteLine(bSel.Identifier.ID.ToString());

            b.BoltSize = 20;
            b.Tolerance = 4.00;
            b.BoltStandard = "7990";
            b.BoltType = BoltGroup.BoltTypeEnum.BOLT_TYPE_SITE;
            b.CutLength = 155;
            //B.StartPointOffset.Dx = 11;
            //B.EndPointOffset.Dx = 12;
            //B.StartPointOffset.Dy = 13;
            //B.EndPointOffset.Dy = 14;
            //B.StartPointOffset.Dz = 15;
            //B.EndPointOffset.Dz = 16;

            b.Length = 88;
            b.ExtraLength = 45;
            b.SlottedHoleX = 55;
            b.SlottedHoleY = 66;
            b.ThreadInMaterial = BoltGroup.BoltThreadInMaterialEnum.THREAD_IN_MATERIAL_YES;

            b.RotateSlots = BoltGroup.BoltRotateSlotsEnum.ROTATE_SLOTS_EVEN;
            b.HoleType = BoltGroup.BoltHoleTypeEnum.HOLE_TYPE_SLOTTED;

            //B.Position.Depth = Position.DepthEnum.FRONT;
            //B.Position.DepthOffset = 13;
            //B.Position.Plane = Position.PlaneEnum.LEFT;
            //B.Position.PlaneOffset = 11;
            //B.Position.Rotation = Position.RotationEnum.TOP;
            //B.Position.RotationOffset = 12;

            b.Bolt = true;
            b.Washer1 = false;
            b.Washer2 = false;
            b.Washer3 = false;
            b.Nut1 = true;
            b.Nut2 = true;

            b.Hole1 = true;
            b.Hole2 = false;
            b.Hole3 = true;
            b.Hole4 = false;
            b.Hole5 = true;

            b.NumberOfBolts = 9;
            b.Diameter = 240;

            if (!b.Modify())
                WriteLine("BoltCircle Modify failed!");
            WriteLine(b.Identifier.ID.ToString());
            WriteLine("BoltCircle complete!");
        }

        private void LoadPointTest()
        {
            WriteLine("Starting LoadPointTest...");
            var fatherBeam = new Beam(new Point(0, 24000, 0), new Point(1000, 24000, 0));
            fatherBeam.Profile.ProfileString = "HI400-15-20*400";
            fatherBeam.Material.MaterialString = "Steel_Undefined";
            if (!fatherBeam.Insert())
            {
                WriteLine("LoadPointTest failed - unable to create father beam");
                MessageBox.Show("Insert father beam failed!");
                return;
            }
            else
            {
                _objectList.Add(fatherBeam);
            }
            
            WriteLine(fatherBeam.Identifier.ID.ToString());
            
            var l = new LoadPoint();
            l.P = new Vector(3000, 4000, 5000);
            l.Moment = new Vector(6000, 7000, 8000);
            l.Position = new Point(0, 24000, 0);

            l.FatherId = fatherBeam.Identifier;

            l.AutomaticPrimaryAxisWeight = true;
            l.BoundingBoxDx = 500;
            l.BoundingBoxDy = 500;
            l.BoundingBoxDz = 500;
            l.LoadDispersionAngle = 0.77;
            l.PartFilter = "testing";
            l.PartNames = TSM.Load.LoadPartNamesEnum.LOAD_PART_NAMES_INCLUDE; ;
            l.PrimaryAxisDirection = new Vector(1000, 500, 0);
            l.Spanning = TSM.Load.LoadSpanningEnum.LOAD_SPANNING_SINGLE;
            l.Weight = 2;
            l.CreateFixedSupportConditionsAutomatically = true;

            l.LoadAttachment = TSM.Load.LoadAttachmentEnum.LOAD_ATTACHMENT_ATTACH_TO_MEMBER; //Can't be Set atm

            if (!l.Insert())
            {
                WriteLine("LoadPointTest failed - unable to create load point");
                MessageBox.Show("Insert load point failed!");
                return;
            }
            else
            {
                _objectList.Add(l);
            }
            WriteLine("LoadPoint ID " + " " + l.Identifier.ID.ToString());

            var lSel = new LoadPoint();
            lSel.Identifier = l.Identifier;

            if (!lSel.Select())
                WriteLine("LoadPoint Select failed!");
            WriteLine(lSel.Identifier.ID.ToString());

            l.P = new Vector(13000, 14000, 15000);
            l.Moment = new Vector(16000, 17000, 18000);
            l.Position = new Point(1000, 24000, 0);

            //LP.FatherId = FatherBeam.Identifier; //Can't be changed, at least not right now

            l.AutomaticPrimaryAxisWeight = false;
            l.BoundingBoxDx = 1500;
            l.BoundingBoxDy = 1500;
            l.BoundingBoxDz = 1500;
            l.LoadDispersionAngle = 0.99;
            l.PartFilter = "testing modified";
            l.PartNames = TSM.Load.LoadPartNamesEnum.LOAD_PART_NAMES_EXCLUDE;
            l.PrimaryAxisDirection = new Vector(2000, 1500, 0);
            l.Spanning = TSM.Load.LoadSpanningEnum.LOAD_SPANNING_DOUBLE;
            l.Weight = 5;
            l.CreateFixedSupportConditionsAutomatically = false;

            l.LoadAttachment = TSM.Load.LoadAttachmentEnum.LOAD_ATTACHMENT_DONT_ATTACH; //Can't be Set atm

            if (!l.Modify())
                WriteLine("LoadPoint Modify failed!");
            WriteLine(l.Identifier.ID.ToString());
            WriteLine("LoadPoint complete!");
        }

        private void LoadAreaTest()
        {
            WriteLine("Starting LoadAreaTest...");

            var l = new LoadArea();
            l.P1 = new Vector(1000, 2000, 3000);
            l.P2 = new Vector(4000, 5000, 6000);
            l.P3 = new Vector(7000, 8000, 9000);
            l.DistanceA = 5;
            l.Position1 = new Point(1000, 24000, 0);
            l.Position2 = new Point(2000, 24000, 0);
            l.Position3 = new Point(2000, 26000, 0);
            l.LoadForm = LoadArea.AreaLoadFormEnum.LOAD_FORM_AREA_PARALLELOGRAM;

            //L.FatherId = FatherBeam.Identifier;

            l.AutomaticPrimaryAxisWeight = true;
            l.BoundingBoxDx = 500;
            l.BoundingBoxDy = 500;
            l.BoundingBoxDz = 500;
            l.LoadDispersionAngle = 0.77;
            l.PartFilter = "testing";
            l.PartNames = TSM.Load.LoadPartNamesEnum.LOAD_PART_NAMES_INCLUDE; ;
            l.PrimaryAxisDirection = new Vector(1000, 500, 0);
            l.Spanning = TSM.Load.LoadSpanningEnum.LOAD_SPANNING_SINGLE;
            l.Weight = 2;
            l.CreateFixedSupportConditionsAutomatically = true;

            l.LoadAttachment = TSM.Load.LoadAttachmentEnum.LOAD_ATTACHMENT_DONT_ATTACH;

            if (!l.Insert())
            {
                WriteLine("LoadAreaTest failed - unable to create load area");
                MessageBox.Show("Insert load area failed!");
                return;
            }
            else
            {
                _objectList.Add(l);
            }
            WriteLine("LoadArea ID " + " " + l.Identifier.ID.ToString());

            var lSel = new LoadArea();
            lSel.Identifier = l.Identifier;

            if (!lSel.Select())
                WriteLine("LoadArea Select failed!");
            WriteLine(lSel.Identifier.ID.ToString());

            l.P1 = new Vector(11000, 12000, 13000);
            l.P2 = new Vector(14000, 15000, 16000);
            l.P3 = new Vector(17000, 18000, 19000);
            l.DistanceA = 15;
            l.Position1 = new Point(1000, 24000, 0);
            l.Position2 = new Point(2000, 24000, 0);
            l.Position3 = new Point(1000, 26000, 0);
            l.LoadForm = LoadArea.AreaLoadFormEnum.LOAD_FORM_AREA_TRIANGLE;

            //LP.FatherId = FatherBeam.Identifier; //Can't be changed, at least not right now

            l.AutomaticPrimaryAxisWeight = false;
            l.BoundingBoxDx = 1500;
            l.BoundingBoxDy = 1500;
            l.BoundingBoxDz = 1500;
            l.LoadDispersionAngle = 0.99;
            l.PartFilter = "testing modified";
            l.PartNames = TSM.Load.LoadPartNamesEnum.LOAD_PART_NAMES_EXCLUDE;
            l.PrimaryAxisDirection = new Vector(2000, 1500, 0);
            l.Spanning = TSM.Load.LoadSpanningEnum.LOAD_SPANNING_DOUBLE;
            l.Weight = 5;
            l.CreateFixedSupportConditionsAutomatically = false;

            l.LoadAttachment = TSM.Load.LoadAttachmentEnum.LOAD_ATTACHMENT_DONT_ATTACH;

            if (!l.Modify())
                WriteLine("LoadArea Modify failed!");
            WriteLine(l.Identifier.ID.ToString());
            WriteLine("LoadArea complete!");
        }

        private void LoadLineTest()
        {
            WriteLine("Starting LoadLineTest...");
            var fatherBeam = new Beam(new Point(3000, 24000, 0), new Point(5000, 24000, 0));
            fatherBeam.Profile.ProfileString = "HI400-15-20*400";
            if (!fatherBeam.Insert())
            {
                WriteLine("LoadLineTest failed - unable to create father beam");
                MessageBox.Show("Insert father beam failed!");
                return;
            }
            else
            {
                _objectList.Add(fatherBeam);
            }
            WriteLine(fatherBeam.Identifier.ID.ToString());


            var l = new LoadLine();
            l.P1 = new Vector(0000, 0000, -1000);
            l.P2 = new Vector(0000, 0000, -1000);
            l.DistanceA = 5;
            l.DistanceB = 6;
            l.Torsion1 = 1000;
            l.Torsion2 = 2000;
            l.Position1 = new Point(3000, 24000, 0);
            l.Position2 = new Point(4000, 24000, 0);
            l.LoadForm = LoadLine.LineLoadFormEnum.LOAD_FORM_LINE_1;

            l.FatherId = fatherBeam.Identifier;

            l.AutomaticPrimaryAxisWeight = true;
            l.BoundingBoxDx = 500;
            l.BoundingBoxDy = 500;
            l.BoundingBoxDz = 500;
            l.LoadDispersionAngle = 0.77;
            l.PartFilter = "testing";
            l.PartNames = TSM.Load.LoadPartNamesEnum.LOAD_PART_NAMES_INCLUDE; ;
            l.PrimaryAxisDirection = new Vector(1000, 500, 0);
            l.Spanning = TSM.Load.LoadSpanningEnum.LOAD_SPANNING_SINGLE;
            l.Weight = 2;
            l.CreateFixedSupportConditionsAutomatically = true;

            l.LoadAttachment = TSM.Load.LoadAttachmentEnum.LOAD_ATTACHMENT_ATTACH_TO_MEMBER;

            if (!l.Insert())
            {
                WriteLine("LoadLineTest failed - unable to create load line");
                MessageBox.Show("Insert load line failed!");
                return;
            }
            else
            {
                _objectList.Add(l);
            }
            WriteLine("LoadLine ID " + " " + l.Identifier.ID.ToString());

            var lSel = new LoadLine();
            lSel.Identifier = l.Identifier;

            if (!lSel.Select())
                WriteLine("LoadLine Select failed!");
            WriteLine(lSel.Identifier.ID.ToString());

            l.P1 = new Vector(0000, 0000, -2000);
            l.P2 = new Vector(0000, 0000, -3000);
            l.DistanceA = 15;
            l.DistanceB = 16;
            l.Torsion1 = 1500;
            l.Torsion2 = 2500;
            l.Position1 = new Point(4000, 24000, 0);
            l.Position2 = new Point(5000, 24000, 0);
            l.LoadForm = LoadLine.LineLoadFormEnum.LOAD_FORM_LINE_2;

            //LP.FatherId = FatherBeam.Identifier; //Can't be changed, at least not right now

            l.AutomaticPrimaryAxisWeight = false;
            l.BoundingBoxDx = 1500;
            l.BoundingBoxDy = 1500;
            l.BoundingBoxDz = 1500;
            l.LoadDispersionAngle = 0.99;
            l.PartFilter = "testing modified";
            l.PartNames = TSM.Load.LoadPartNamesEnum.LOAD_PART_NAMES_EXCLUDE;
            l.PrimaryAxisDirection = new Vector(2000, 1500, 0);
            l.Spanning = TSM.Load.LoadSpanningEnum.LOAD_SPANNING_DOUBLE;
            l.Weight = 5;
            l.CreateFixedSupportConditionsAutomatically = false;

            l.LoadAttachment = TSM.Load.LoadAttachmentEnum.LOAD_ATTACHMENT_DONT_ATTACH; //Can't be Set atm

            if (!l.Modify())
                WriteLine("LoadLine Modify failed!");
            WriteLine(l.Identifier.ID.ToString());
            WriteLine("LoadLine complete!");
        }

        private void LoadUniformTest()
        {
            var l = new LoadUniform();
            l.P1 = new Vector(1000, 2000, 3000);
            l.DistanceA = 5;
            l.Polygon.Points.Add(new Point(5000, 24000, 0));
            l.Polygon.Points.Add(new Point(7000, 24000, 0));
            l.Polygon.Points.Add(new Point(8000, 26000, 0));
            l.Polygon.Points.Add(new Point(5000, 26000, 0));

            //L.FatherId = FatherBeam.Identifier;

            l.AutomaticPrimaryAxisWeight = true;
            l.BoundingBoxDx = 500;
            l.BoundingBoxDy = 500;
            l.BoundingBoxDz = 500;
            l.LoadDispersionAngle = 0.77;
            l.PartFilter = "testing";
            l.PartNames = TSM.Load.LoadPartNamesEnum.LOAD_PART_NAMES_INCLUDE; ;
            l.PrimaryAxisDirection = new Vector(1000, 500, 0);
            l.Spanning = TSM.Load.LoadSpanningEnum.LOAD_SPANNING_SINGLE;
            l.Weight = 2;
            l.CreateFixedSupportConditionsAutomatically = true;

            l.LoadAttachment = TSM.Load.LoadAttachmentEnum.LOAD_ATTACHMENT_DONT_ATTACH;

            if (!l.Insert())
            {
                WriteLine("LoadUniformTest failed - unable to create load uniform");
                MessageBox.Show("Insert load uniform failed!");
                return;
            }
            else
            {
                _objectList.Add(l);
            }
            WriteLine("LoadLine ID " + " " + l.Identifier.ID.ToString());

            var lSel = new LoadUniform();
            lSel.Identifier = l.Identifier;

            if (!lSel.Select())
                WriteLine("LoadUniform Select failed!");
            WriteLine(lSel.Identifier.ID.ToString());

            l.P1 = new Vector(11000, 12000, 13000);
            l.DistanceA = 15;

            //LP.FatherId = FatherBeam.Identifier; //Can't be changed, at least not right now

            l.AutomaticPrimaryAxisWeight = false;
            l.BoundingBoxDx = 1500;
            l.BoundingBoxDy = 1500;
            l.BoundingBoxDz = 1500;
            l.LoadDispersionAngle = 0.99;
            l.PartFilter = "testing modified";
            l.PartNames = TSM.Load.LoadPartNamesEnum.LOAD_PART_NAMES_EXCLUDE;
            l.PrimaryAxisDirection = new Vector(2000, 1500, 0);
            l.Spanning = TSM.Load.LoadSpanningEnum.LOAD_SPANNING_DOUBLE;
            l.Weight = 5;
            l.CreateFixedSupportConditionsAutomatically = false;

            l.LoadAttachment = TSM.Load.LoadAttachmentEnum.LOAD_ATTACHMENT_DONT_ATTACH;

            if (!l.Modify())
                WriteLine("LoadUniform Modify failed!");
            WriteLine(l.Identifier.ID.ToString());
            WriteLine("LoadUniform complete!");
        }

        /// <summary>
        /// Performs a test for getting and setting properties of a Beam object.
        /// </summary>
        private void GetAndSetPropertyTest()
        {
            WriteLine("Starting GetAndSetPropertyTest...");

            // Create two points and a Beam between them.
            var point = new Point(3000, 0, 0);
            var point2 = new Point(4000, 0, 0);
            var b = new Beam(point, point2);

            // Set properties for the Beam.
            b.Profile.ProfileString = "HI400-15-20*400";
            b.Material.MaterialString = "Steel_Undefined";
            b.Finish = "PAINT";

            // Insert the Beam and handle failure.
            if (!b.Insert())
            {
                WriteLine("GetAndSetPropertyTest failed - unable to create beam");
                MessageBox.Show("Insert beam failed!");
                return;
            }
            else
            {
                _objectList.Add(b);
            }

            WriteLine(b.Identifier.ID.ToString());

            // Set user-defined properties.
            if (!b.SetUserProperty("comment", "Test comment"))
                WriteLine("SetProperty failed!");
            if (!b.SetUserProperty("fabricator", "Test fabricator"))
                WriteLine("SetProperty failed!");
            if (!b.SetUserProperty("AD_n_part_splits", 66))
                WriteLine("SetProperty failed!");

            // Get user-defined properties.
            var commentVal = "";
            var fabVal = "";
            var splitsVal = 0;
            if (!b.GetUserProperty("comment", ref commentVal))
                WriteLine("GetProperty failed!");
            if (!b.GetUserProperty("fabricator", ref fabVal))
                WriteLine("GetProperty failed!");
            if (!b.GetUserProperty("AD_n_part_splits", ref splitsVal))
                WriteLine("GetProperty failed!");

            // Test report GETTERS
            var assPos = "";
            var cog = 0.0;
            var currPhase = 0;
            var projName = "";
            if (!b.GetReportProperty("ASSEMBLY_POS", ref assPos))
                WriteLine("GetReportProperty failed!!!");
            if (!b.GetReportProperty("COG_X", ref cog))
                WriteLine("GetReportProperty failed!!!");
            if (!b.GetReportProperty("PROJECT.CURRENT_PHASE", ref currPhase))
                WriteLine("GetReportProperty failed!!!");
            if (!b.GetReportProperty("PROJECT.NAME", ref projName))
                WriteLine("GetReportProperty failed!!!");

            WriteLine("GetAndSetPropertyTest complete!");
        }

        /// <summary>
        /// Handles a Part object by printing its solid, coordinates, and connected objects.
        /// </summary>
        /// <param name="part">The Part object to handle.</param>
        private void HandlePart(Part part)
        {
            WriteLine(part.Identifier.ID + " was a part, printing solid and coordsys...");
            var solid = part.GetSolid();

            // Print solid details if available.
            if (solid != null)
            {
                WriteLine(solid.MinimumPoint.ToString());
                WriteLine(solid.MaximumPoint.ToString());
            }

            var coordinateSystem = part.GetCoordinateSystem();
            WriteLine("O: " + coordinateSystem.Origin);
            WriteLine("X: " + coordinateSystem.AxisX);
            WriteLine("Y: " + coordinateSystem.AxisY);
            WriteLine("Part class is " + part.Class);

            // Get and print connected objects.
            var enum2 = part.GetChildren();
            WriteLine("Number of children for the part is " + enum2.GetSize());

            var connectedEnum = part.GetBolts();
            WriteLine("Part " + part.Identifier.ID + " has " + connectedEnum.GetSize() + " connected bolt groups");
            while (connectedEnum.MoveNext())
            {
                var connected = connectedEnum.Current as BoltGroup;
                WriteLine(connected.Identifier.ID.ToString());
            }

            connectedEnum = part.GetBooleans();
            WriteLine("Part " + part.Identifier.ID + " has " + connectedEnum.GetSize() + " connected boolean objects");
            while (connectedEnum.MoveNext())
            {
                var connected = connectedEnum.Current as Tekla.Structures.Model.Boolean;
                WriteLine(connected.Identifier.ID.ToString());
            }

            connectedEnum = part.GetWelds();
            WriteLine("Part " + part.Identifier.ID + " has " + connectedEnum.GetSize() + " connected welds");
            while (connectedEnum.MoveNext())
            {
                WriteLine(connectedEnum.Current.Identifier.ID.ToString());
            }

            connectedEnum = part.GetComponents();

            WriteLine("Part " + part.Identifier.ID + " has " + connectedEnum.GetSize() + " connected components");
            while (connectedEnum.MoveNext())
            {
                var connected = connectedEnum.Current as BaseComponent;
                WriteLine(connected.Identifier.ID.ToString());
            }

            var ass = part.GetAssembly();
            if (ass != null)
            {
                var list = ass.GetSubAssemblies();
                WriteLine("Number of subassemblies is " + list.Count);
            }
        }

        /// <summary>
        /// Performs a test for enumerating various object types in the model.
        /// </summary>
        /// <param name="model">The Model object to enumerate.</param>
        private void EnumTest(Model model)
        {
            // Counters for various object types.
            var counter = 0;
            var partCounter = 0;
            var booleanCounter = 0;
            var weldCounter = 0;
            var reinforcementCounter = 0;
            var cutPlaneCounter = 0;
            var fittingCounter = 0;
            var castUnitCounter = 0;
            var controlPlaneCounter = 0;
            var surfaceTreatmentCounter = 0;
            var boltGroupCounter = 0;
            var loadCounter = 0;

            // ArrayList to store Part objects for selection.
            var parts = new ArrayList();

            WriteLine("Starting EnumTest...");
            var @enum = model.GetModelObjectSelector().GetAllObjects();

            while (@enum.MoveNext())
            {
                WriteLine("-------------------------------------");
                WriteLine("Moving to object number " + counter++ + " ID " + ((ModelObject)@enum.Current).Identifier.ID.ToString());

                var part = @enum.Current as Part;
                if (part != null)
                {
                    partCounter++;
                    HandlePart(part);
                    parts.Add(part);
                    continue;
                }

                // Handle other object types...
            }

            // Print summary of object types and counters.
            WriteLine("-------------------------------------");
            WriteLine("Found " + partCounter.ToString() + " Parts" + ", " + booleanCounter.ToString() + " Booleans, " +
                weldCounter.ToString() + " Welds, " + reinforcementCounter.ToString() + " Reinforcements, " +
                fittingCounter.ToString() + " Fittings, " + cutPlaneCounter + " Cuttings and " +
                castUnitCounter.ToString() + " CastUnits, " + controlPlaneCounter.ToString() + " ControlPlanes, " +
                surfaceTreatmentCounter.ToString() + " SurfaceTreatments, " +
                boltGroupCounter.ToString() + " BoltGroups, " +
                loadCounter.ToString() + " Loads.");

            // Select all parts from the model.
            var selector = new Tekla.Structures.Model.UI.ModelObjectSelector();
            selector.Select(parts);
        }

        private void FilterTest(Model model)
        {
            var filterName = "testfilter";
            var counter = 0;

            WriteLine("Starting FilterTest...");
            var @enum = model.GetModelObjectSelector().GetObjectsByFilterName(filterName);

            while (@enum.MoveNext())
            {
                WriteLine("-------------------------------------");
                WriteLine("Moving to object number " + counter++);

                var part = @enum.Current as Part;
                if (part != null)
                {
                    WriteLine(part.Identifier.ID + " was a part, matched filter " + filterName);
                }
                else
                {
                    WriteLine(part.Identifier.ID + " was not a part...");
                }
            }
            WriteLine("Total number of " + counter + " objects matched filter " + filterName);
            return;
        }

        private void SolidTest(Model model)
        {
            WriteLine("Starting BeamSolidTest...");
            var point = new Point(5000, 0, 0);
            var point2 = new Point(7000, 0, 0);

            var beam = new Beam(point, point2);

            beam.Profile.ProfileString = "PL500*200";
            beam.Material.MaterialString = "Steel_Undefined";
            beam.Name = "SolidTestBeam";
            beam.Finish = "Normal";
            beam.Class = "5";
            if (!beam.Insert())
            {
                WriteLine("SolidTest failed - unable to create beam");
                MessageBox.Show("Insert beam failed!");
                return;
            }
            else
            {
                _objectList.Add(beam);
            }
            WriteLine(beam.Identifier.ID.ToString());

            if (!beam.Select())
                WriteLine("Select failed!");
            WriteLine(beam.Identifier.ID.ToString());

            var solid = beam.GetSolid();

            var faceEnum = solid.GetFaceEnumerator();

            int nFaces = 0, nLoops = 0, nVertexes = 0;

            while (faceEnum.MoveNext())
            {
                nFaces++;
                nLoops = 0;

                var face = faceEnum.Current as Face;
                var loopEnum = face.GetLoopEnumerator();

                while (loopEnum.MoveNext())
                {
                    nLoops++;
                    nVertexes = 0;

                    var loop = loopEnum.Current as Loop;
                    var vertexEnum = loop.GetVertexEnumerator();

                    while (vertexEnum.MoveNext())
                    {
                        nVertexes++;

                        WriteLine("Solid " + beam.Identifier.ID + " Face " + nFaces +
                            " Loop " + nLoops + " Vertex " + nVertexes);
                        var vertex = vertexEnum.Current as Point;
                        WriteLine(vertex.ToString());
                    }
                }
            }

            WriteLine(beam.Identifier.ID.ToString());
            WriteLine("BeamSolidTest complete!");
        }

        private void TransformationPlaneTest(Model model)
        {
            WriteLine("Starting TransformationPlaneTest...");

            var planeHandler = model.GetWorkPlaneHandler();
            var currentPlane = planeHandler.GetCurrentTransformationPlane();

            WriteLine("Current plane in the model:");
            WriteLine(currentPlane.ToString());

            var beam = new Beam(new Point(0, 7000, 0), new Point(0, 8000, 0));

            beam.Profile.ProfileString = "HI400-15-20*400";
            beam.Material.MaterialString = "Steel_Undefined";
            beam.Finish = "PAINT";
            if (!beam.Insert())
            {
                WriteLine("TransformationPlaneTest failed - unable to create Beam");
                MessageBox.Show("Insert Beam failed!");
                return;
            }
            else
            {
                _objectList.Add(beam);
            }
            beam.Select();

            WriteLine("Change current plane to object " + beam.Identifier.ID + " plane");
            var plane = new TransformationPlane(beam.GetCoordinateSystem());
            planeHandler.SetCurrentTransformationPlane(plane);

            plane = planeHandler.GetCurrentTransformationPlane();
            WriteLine("Current plane in the model after change:");
            WriteLine(plane.ToString());

            WriteLine("Change plane to global:");
            var globalPlane = new TransformationPlane();
            WriteLine(globalPlane.ToString());
            planeHandler.SetCurrentTransformationPlane(globalPlane);

            WriteLine("And get it from model:");
            plane = planeHandler.GetCurrentTransformationPlane();
            WriteLine(plane.ToString());

            WriteLine("Then set plane back to original:");
            planeHandler.SetCurrentTransformationPlane(currentPlane);
            WriteLine(currentPlane.ToString());
        }

        private void SolidLineIntersectionTest(Model model)
        {
            WriteLine("Starting SolidIntersectionTest...");
            var point1 = new Point(5000, 4000, 0);
            var point2 = new Point(10000, 4000, 0);

            var beam = new Beam(point1, point2);

            beam.Profile.ProfileString = "PL500*200";
            beam.Material.MaterialString = "Steel_Undefined";
            beam.Name = "SolidLine";
            beam.Finish = "Normal";
            beam.Class = "5";
            if (!beam.Insert())
            {
                WriteLine("SolidLineIntersectionTest failed - unable to create Beam");
                MessageBox.Show("Insert Beam failed!");
                return;
            }
            else
            {
                _objectList.Add(beam);
            }
            WriteLine(beam.Identifier.ID.ToString());

            if (!beam.Select())
                WriteLine("Select failed!");
            WriteLine(beam.Identifier.ID.ToString());

            var solid = beam.GetSolid();

            var points = solid.Intersect(new Point(8000, -1000, -100), new Point(8000, 9000, -100));
            WriteLine("Got " + points.Count + " line intersection points");
            var pointsEnum = points.GetEnumerator();

            while (pointsEnum.MoveNext())
            {
                var point = pointsEnum.Current as Point;

                WriteLine("Point: " + point);
            }
        }

        private void SolidPlaneIntersectionTest()
        {
            var beam = new Beam(new Point(500, 500, -1000), new Point(500, 500, 1000));

            beam.Profile.ProfileString = "RHS150*5";
            beam.Material.MaterialString = "Steel_Undefined";
            beam.Name = "SolidPlane";
            beam.Finish = "Normal";
            beam.Class = "6";
            if (!beam.Insert())
            {
                WriteLine("SolidPlaneIntersectionTest failed - unable to create Beam");
                MessageBox.Show("Insert Beam failed!");
                return;
            }
            else
            {
                _objectList.Add(beam);
            }
            WriteLine(beam.Identifier.ID.ToString());

            if (!beam.Select())
                WriteLine("Select failed!");
            WriteLine(beam.Identifier.ID.ToString());

            var solid = beam.GetSolid();

            var points = solid.IntersectAllFaces(new Point(0, 0, 0), 
                new Point(1000, 0, 0), new Point(0, 1000, 0));
            
            //IEnumerator LoopsEnum = Points.GetEnumerator();
            var nLoops = 0;
            var nPoints = 0;

            while (points.MoveNext())
            {
                nPoints = 0;
                nLoops++;
                var loopPoints = points.Current as ArrayList;
                var loopPointsEnum = loopPoints.GetEnumerator();

                WriteLine(nLoops + ". Loop: " + loopPoints.Count + " plane intersection points");

                while (loopPointsEnum.MoveNext())
                {
                    nPoints++;
                    var point = loopPointsEnum.Current as Point;
                    WriteLine("   " + nPoints + ". Point: " + point);
                }
            }

            WriteLine("Got " + nLoops + " plane intersection loops");
        }

        private void GetCoordSysTest()
        {
            //TODO - Make sure we get a meaningful test here...
            WriteLine("Starting Get CoordSys Test...");

            var point = new Point(0, 0, 0);
            var point2 = new Point(1000, 0, 0);

            var beam = new Beam(point, point2);

            beam.Profile.ProfileString = "HI400-15-20*400";
            beam.Finish = "PAINT";
            if (!beam.Insert())
                WriteLine("Insert failed!");
            else
                _objectList.Add(beam);
            WriteLine(beam.Identifier.ID.ToString());

            beam.Select();

            var test1 = beam.GetCoordinateSystem();

            WriteLine("CoordSys Test complete!");
        }

        private void ComponentTest()
        {
            WriteLine("Starting Component Test...");

            var b = new Beam(new Point(12000, 0, 0), new Point(12000, 0, 6000));
            b.Profile.ProfileString = "380*380";
            b.Material.MaterialString = "Concrete_Undefined";
            if (b.Insert())
            {
                _objectList.Add(b);
                var c = new Component();
                c.Name = "Component Test";
                c.Number = 30000063;

                var ci = new ComponentInput();
                ci.AddInputObject(b);

                c.SetComponentInput(ci);

                c.LoadAttributesFromFile("standard");

                c.SetAttribute("side_bar_space", 333.0);

                if (!c.Insert())
                {
                    WriteLine("ComponentTest failed - unable to create Component");
                    MessageBox.Show("Insert component failed!");
                    return;
                }
                else
                {
                    _objectList.Add(c);
                }
                WriteLine(c.Identifier.ID.ToString());

                var dValue = 0.0;
                if (!c.GetAttribute("side_bar_space", ref dValue) || dValue != 333)
                    WriteLine("Component GetAttribute failed");
            }
            else
            {
                WriteLine("Unable to perform the Component test, creation of the concrete column failed");
            }

            WriteLine("Component Test complete!");
        }

        private void ConnectionTest()
        {
            WriteLine("Starting Connection Test...");

            var b1 = new Beam(new Point(15000, 0, -500), new Point(15000, 0, 6000));
            b1.Profile.ProfileString = "HI400-15-20*400";
            b1.Material.MaterialString = "Steel_Undefined";

            var b2 = new Beam(new Point(15000, 0, 0), new Point(18000, 0, 0));
            b2.Profile.ProfileString = "HI300-15-20*300";
            b2.Material.MaterialString = "Steel_Undefined";

            if (b1.Insert() && b2.Insert())
            {
                _objectList.Add(b1);
                _objectList.Add(b2);
                var c = new Connection();
                c.Name = "Test End Plate";
                c.Number = 144;
                c.LoadAttributesFromFile("standard");
                c.UpVector = new Vector(0, 0, 1000);
                c.PositionType = PositionTypeEnum.COLLISION_PLANE;

                c.SetPrimaryObject(b1);
                c.SetSecondaryObject(b2);

                c.SetAttribute("e2", 10.0);
                c.SetAttribute("e1", 10.0);

                if (!c.Insert())
                {
                    WriteLine("PolyBeamTest failed - unable to create connection");
                    MessageBox.Show("Insert connection failed!");
                    return;
                }
                else
                {
                    _objectList.Add(c);
                }
                WriteLine(c.Identifier.ID.ToString());

                var dValue = 0.0;
                if (!c.GetAttribute("e2", ref dValue) || dValue != 10)
                    WriteLine("Connection GetAttribute failed");

                var cSel = new Connection();
                cSel.Identifier.ID = c.Identifier.ID;
                if (!cSel.Select() || cSel.PositionType != PositionTypeEnum.COLLISION_PLANE)
                    WriteLine("Select failed");
            }
            else
            {
                WriteLine("Unable to perform the connection test, creation of the main and secondary parts failed");
            }

            WriteLine("Connection Test complete!");
        }

        private void SeamTest()
        {
            WriteLine("Starting Seam Test...");

            var b1 = new Beam(new Point(15000, 3000, 0), new Point(21000, 3000, 0));
            b1.Profile.ProfileString = "780*380";
            b1.Material.MaterialString = "Concrete_Undefined";
            b1.Class = "5";
            b1.Position.Plane = Position.PlaneEnum.MIDDLE;
            b1.Position.Rotation = Position.RotationEnum.TOP;
            b1.Position.Depth = Position.DepthEnum.BEHIND;

            var b2 = new Beam(new Point(15000, 3000, 0), new Point(21000, 3000, 0));
            b2.Profile.ProfileString = "780*380";
            b2.Material.MaterialString = "Concrete_Undefined";
            b2.Class = "6";
            b2.Position.Plane = Position.PlaneEnum.MIDDLE;
            b2.Position.Rotation = Position.RotationEnum.TOP;
            b2.Position.Depth = Position.DepthEnum.FRONT;

            if (b1.Insert() && b2.Insert())
            {
                _objectList.Add(b1);
                _objectList.Add(b2);
                var s = new Seam();
                s.Name = "seamdm";
                s.Number = -1;
                s.LoadAttributesFromFile("standard");
                s.UpVector = new Vector(0, 0, 0);
                s.AutoDirectionType = AutoDirectionTypeEnum.AUTODIR_BASIC;
                s.AutoPosition = true;

                s.SetPrimaryObject(b1);
                s.SetSecondaryObject(b2);

                s.SetInputPositions(new Point(15000, 3000, 0), new Point(21000, 3000, 0));

                if (!s.Insert())
                {
                    WriteLine("Seam Insert failed, please check that you have the seam called \"seamdm\" in your model.");
                    WriteLine(s.Identifier.ID.ToString());
                }
                else
                {
                    _objectList.Add(s);
                    var sSel = new Connection();
                    sSel.Identifier.ID = s.Identifier.ID;
                    if (!sSel.Select())
                        WriteLine("Select failed");
                }
            }
            else
            {
                WriteLine("Unable to perform the Seam test, creation of the main and secondary parts failed");
            }

            WriteLine("Seam Test complete!");
        }

        private void TaperedCustomPartTest()
        {
            WriteLine("Starting TaperedCustomPart Test...");

            var ci = new ComponentInput();
            ci.AddOneInputPosition(new Point(5000, 0, 0));

            var c = new Component(ci);

            c.Name = "COL8";
            c.Number = -1;
            c.LoadAttributesFromFile("standard");

            c.SetAttribute("P7", 8.0);
            c.SetAttribute("P1", 8000.0);

            if (c.Insert())
            {
                _objectList.Add(c);
                var enum2 = c.GetChildren();
                WriteLine("Number of children for the part is " + enum2.GetSize());
            }
            else
            {
                WriteLine("Creation of COL8 failed, please check that you have the \"COL8\" in your model.");
            }
        }

        private void TaperedBeamTest()
        {
            WriteLine("Starting TaperedBeam Test...");

            for (var i = 0; i < 1; i++)
            {
                var ci = new ComponentInput();
                ci.AddOneInputPosition(new Point(500 * i, 0, 0));
                ci.AddOneInputPosition(new Point(500 * i, 6000, 0));

                var c = new Component(ci);

                c.Name = "TaperedBeam";
                c.Number = 1000045;
                c.LoadAttributesFromFile("standard");

                c.SetAttribute("end_height", 500.0);
                c.SetAttribute("stp_height", 800.0);

                if (c.Insert())
                    _objectList.Add(c);
                else
                    WriteLine("Creation of TaperedBeam failed!");
            }
            WriteLine("Completed TaperedBeam Test!");
        }

        private void DetailTest()
        {
            WriteLine("Starting Detail Test...");

            var b = new Beam(new Point(13000, 3000, -500), new Point(13000, 3000, 6000));
            b.Profile.ProfileString = "HI400-15-20*400";
            b.Material.MaterialString = "Steel_Undefined";

            if (b.Insert())
            {
                _objectList.Add(b);

                var d = new Detail();
                d.Name = "Test End Plate Detail";
                d.Number = 1002;
                d.LoadAttributesFromFile("standard");
                d.UpVector = new Vector(0, 0, 0);
                d.PositionType = PositionTypeEnum.MIDDLE_PLANE;
                d.AutoDirectionType = AutoDirectionTypeEnum.AUTODIR_DETAIL;
                d.DetailType = DetailTypeEnum.END;

                d.SetPrimaryObject(b);
                d.SetReferencePoint(new Point(13000, 3000, 3000));

                d.SetAttribute("el", 33.0);
                d.SetAttribute("er", 33.0);

                if (!d.Insert())
                    WriteLine("Detail Insert failed");
                else
                    _objectList.Add(d);
                WriteLine(d.Identifier.ID.ToString());

                var dValue = 0.0;
                if (!d.GetAttribute("er", ref dValue) || dValue != 33)
                    WriteLine("Detail GetAttribute failed");

                var dSel = new Detail();
                dSel.Identifier.ID = d.Identifier.ID;
                if (!dSel.Select() || dSel.AutoDirectionType != AutoDirectionTypeEnum.AUTODIR_DETAIL)
                    WriteLine("Detail Select failed");
            }
            else
            {
                WriteLine("Unable to perform the Detail test, creation of the primary part failed");
            }

            WriteLine("Detail Test complete!");
        }

        private void DeleteAll(Model model)
        {
            var counter = 0;

            WriteLine("Starting DeleteAll...");

            var @enum = _objectList.GetEnumerator();
            while (@enum.MoveNext())
            {
                WriteLine("Deleting object number " + counter++);
                var mo = @enum.Current as ModelObject;
                if (mo != null)
                    mo.Delete();
                else
                    WriteLine("Object already deleted or unsupported!");
            }
            WriteLine("All created objects deleted, have fun!");
        }

        /// <summary>
        /// Executes a specific test or a set of tests based on the provided argument.
        /// </summary>
        /// <param name="test">The name of the test(s) to run.</param>
        private void RunTest(string test)
        {
            // Check if the provided test argument is "All" or a specific test and execute the corresponding test methods.

            // BeamTest
            if (test.Equals("All") || test.Equals("BeamTest"))
                BeamTest();

            // PolyBeamTest
            if (test.Equals("All") || test.Equals("PolyBeamTest"))
                PolyBeamTest();

            // ContourPlateTest
            if (test.Equals("All") || test.Equals("ContourPlateTest"))
                ContourPlateTest();

            // BooleanTests
            if (test.Equals("All") || test.Equals("BooleanTests"))
            {
                BooleanPartTest();
                CutTest();
                FittingTest();
            }

            // WeldTest
            if (test.Equals("All") || test.Equals("WeldTest"))
                WeldTest();

            // CastUnitTest
            if (test.Equals("All") || test.Equals("CastUnitTest"))
                CastUnitTest();

            // PlaneTests
            if (test.Equals("All") || test.Equals("PlaneTests"))
            {
                ControlPlaneTest();
                TransformationPlaneTest(_model);
                var grid = GridTest();
            }

            // RebarTests
            if (test.Equals("All") || test.Equals("RebarTests"))
            {
                SingleRebarTest();
                RebarGroupTest1();
                RebarGroupTest2();
                RebarGroupTest3();
                RebarGroupTest4();
                RebarSpliceTest();
            }

            // AssemblyTest
            if (test.Equals("All") || test.Equals("AssemblyTest"))
            {
                AssemblyTest();
                GetPartMarkTest();
                GetAndSetPropertyTest();
            }

            // SurfaceTreatmentTest
            if (test.Equals("All") || test.Equals("SurfaceTreatmentTest"))
                SurfaceTreatmentTest();

            // BoltTests
            if (test.Equals("All") || test.Equals("BoltTests"))
            {
                BoltArrayTest();
                BoltXyListTest();
                BoltCircleTest();
            }

            // LoadTests
            if (test.Equals("All") || test.Equals("LoadTests"))
            {
                LoadPointTest();
                LoadAreaTest();
                LoadLineTest();
                LoadUniformTest();
            }

            // EnumerationTest
            if (test.Equals("All") || test.Equals("EnumerationTest"))
            {
                EnumTest(_model);
                FilterTest(_model);
            }

            // SolidTests
            if (test.Equals("All") || test.Equals("SolidTests"))
            {
                SolidTest(_model);
                SolidLineIntersectionTest(_model);
                SolidPlaneIntersectionTest();
            }

            // ComponentTests
            if (test.Equals("All") || test.Equals("ComponentTests"))
            {
                ConnectionTest();
                ComponentTest();
                TaperedBeamTest();
                // TaperedCustomPartTest(); // Uncomment if COL8 custom part is available to insert
                DetailTest();
                // SeamTest(); // Uncomment if the model has seamdm seam connection
            }

            // Commit changes to the model after running tests.
            _model.CommitChanges();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            foreach (var itemChecked in _checkedListBox1.CheckedItems)
                RunTest(itemChecked.ToString());
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            DeleteAll(_model);
            _model.CommitChanges();
            _objectList.Clear();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _checkedListBox1.SetItemChecked(0, true);
        }
    }

    public class Epsilon
    {
        public static double Value = 1E-3;
    }
}
