partial class SpliceConnection
{
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
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
            this.PlateLengthTextBox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.BoltStandardTextBox = new System.Windows.Forms.TextBox();
            this.okApplyModifyGetOnOffCancel1 = new Tekla.Structures.Dialog.UIControls.OkApplyModifyGetOnOffCancel();
            this.saveLoad1 = new Tekla.Structures.Dialog.UIControls.SaveLoad();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.checkBoxBoltStandard = new System.Windows.Forms.CheckBox();
            this.checkBoxPlateLength = new System.Windows.Forms.CheckBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.ConnectionCodeTextBox = new System.Windows.Forms.TextBox();
            this.ClassTextBox = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.comboBox4 = new System.Windows.Forms.ComboBox();
            this.checkBoxLocked = new System.Windows.Forms.CheckBox();
            this.UpDirectionComboBox = new System.Windows.Forms.ComboBox();
            this.checkBoxAutoConnection = new System.Windows.Forms.CheckBox();
            this.checkBoxConnectionCode = new System.Windows.Forms.CheckBox();
            this.checkBoxRotationAngleX = new System.Windows.Forms.CheckBox();
            this.checkBoxClass = new System.Windows.Forms.CheckBox();
            this.checkBoxAutoDefault = new System.Windows.Forms.CheckBox();
            this.checkBoxRotationAngleY = new System.Windows.Forms.CheckBox();
            this.checkBoxUpDirection = new System.Windows.Forms.CheckBox();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // PlateLengthTextBox
            // 
            this.structuresExtender.SetAttributeName(this.PlateLengthTextBox, "PlateLength");
            this.structuresExtender.SetAttributeTypeName(this.PlateLengthTextBox, "Distance");
            this.structuresExtender.SetBindPropertyName(this.PlateLengthTextBox, null);
            this.PlateLengthTextBox.Location = new System.Drawing.Point(192, 32);
            this.PlateLengthTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.PlateLengthTextBox.Name = "PlateLengthTextBox";
            this.PlateLengthTextBox.Size = new System.Drawing.Size(133, 26);
            this.PlateLengthTextBox.TabIndex = 1;
            this.PlateLengthTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.anyTextBox_KeyPress);
            // 
            // label1
            // 
            this.structuresExtender.SetAttributeName(this.label1, null);
            this.structuresExtender.SetAttributeTypeName(this.label1, null);
            this.label1.AutoSize = true;
            this.structuresExtender.SetBindPropertyName(this.label1, null);
            this.label1.Location = new System.Drawing.Point(40, 37);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "Plate length";
            // 
            // label3
            // 
            this.structuresExtender.SetAttributeName(this.label3, null);
            this.structuresExtender.SetAttributeTypeName(this.label3, null);
            this.label3.AutoSize = true;
            this.structuresExtender.SetBindPropertyName(this.label3, null);
            this.label3.Location = new System.Drawing.Point(40, 77);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Bolt standard";
            // 
            // BoltStandardTextBox
            // 
            this.structuresExtender.SetAttributeName(this.BoltStandardTextBox, "BoltStandard");
            this.structuresExtender.SetAttributeTypeName(this.BoltStandardTextBox, "String");
            this.structuresExtender.SetBindPropertyName(this.BoltStandardTextBox, null);
            this.BoltStandardTextBox.Location = new System.Drawing.Point(192, 72);
            this.BoltStandardTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BoltStandardTextBox.Name = "BoltStandardTextBox";
            this.BoltStandardTextBox.Size = new System.Drawing.Size(133, 26);
            this.BoltStandardTextBox.TabIndex = 5;
            this.BoltStandardTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.anyTextBox_KeyPress);
            // 
            // okApplyModifyGetOnOffCancel1
            // 
            this.structuresExtender.SetAttributeName(this.okApplyModifyGetOnOffCancel1, null);
            this.structuresExtender.SetAttributeTypeName(this.okApplyModifyGetOnOffCancel1, null);
            this.structuresExtender.SetBindPropertyName(this.okApplyModifyGetOnOffCancel1, null);
            this.okApplyModifyGetOnOffCancel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.okApplyModifyGetOnOffCancel1.Location = new System.Drawing.Point(0, 518);
            this.okApplyModifyGetOnOffCancel1.Margin = new System.Windows.Forms.Padding(6);
            this.okApplyModifyGetOnOffCancel1.Name = "okApplyModifyGetOnOffCancel1";
            this.okApplyModifyGetOnOffCancel1.Size = new System.Drawing.Size(784, 45);
            this.okApplyModifyGetOnOffCancel1.TabIndex = 8;
            this.okApplyModifyGetOnOffCancel1.OkClicked += new System.EventHandler(this.okApplyModifyGetOnOffCancel1_OkClicked);
            this.okApplyModifyGetOnOffCancel1.ApplyClicked += new System.EventHandler(this.okApplyModifyGetOnOffCancel1_ApplyClicked);
            this.okApplyModifyGetOnOffCancel1.ModifyClicked += new System.EventHandler(this.okApplyModifyGetOnOffCancel1_ModifyClicked);
            this.okApplyModifyGetOnOffCancel1.GetClicked += new System.EventHandler(this.okApplyModifyGetOnOffCancel1_GetClicked);
            this.okApplyModifyGetOnOffCancel1.OnOffClicked += new System.EventHandler(this.okApplyModifyGetOnOffCancel1_OnOffClicked);
            this.okApplyModifyGetOnOffCancel1.CancelClicked += new System.EventHandler(this.okApplyModifyGetOnOffCancel1_CancelClicked);
            // 
            // saveLoad1
            // 
            this.structuresExtender.SetAttributeName(this.saveLoad1, null);
            this.structuresExtender.SetAttributeTypeName(this.saveLoad1, null);
            this.saveLoad1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.structuresExtender.SetBindPropertyName(this.saveLoad1, null);
            this.saveLoad1.Dock = System.Windows.Forms.DockStyle.Top;
            this.saveLoad1.HelpFileType = Tekla.Structures.Dialog.UIControls.SaveLoad.HelpFileTypeEnum.General;
            this.saveLoad1.HelpKeyword = "";
            this.saveLoad1.HelpUrl = "";
            this.saveLoad1.Location = new System.Drawing.Point(0, 0);
            this.saveLoad1.Margin = new System.Windows.Forms.Padding(6);
            this.saveLoad1.Name = "saveLoad1";
            this.saveLoad1.SaveAsText = "";
            this.saveLoad1.Size = new System.Drawing.Size(784, 66);
            this.saveLoad1.TabIndex = 9;
            this.saveLoad1.UserDefinedHelpFilePath = null;
            // 
            // tabControl1
            // 
            this.structuresExtender.SetAttributeName(this.tabControl1, null);
            this.structuresExtender.SetAttributeTypeName(this.tabControl1, null);
            this.structuresExtender.SetBindPropertyName(this.tabControl1, null);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 66);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(784, 452);
            this.tabControl1.TabIndex = 10;
            // 
            // tabPage1
            // 
            this.structuresExtender.SetAttributeName(this.tabPage1, null);
            this.structuresExtender.SetAttributeTypeName(this.tabPage1, null);
            this.tabPage1.BackColor = System.Drawing.Color.WhiteSmoke;
            this.structuresExtender.SetBindPropertyName(this.tabPage1, null);
            this.tabPage1.Controls.Add(this.checkBoxBoltStandard);
            this.tabPage1.Controls.Add(this.checkBoxPlateLength);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.PlateLengthTextBox);
            this.tabPage1.Controls.Add(this.BoltStandardTextBox);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage1.Size = new System.Drawing.Size(776, 175);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "albl_Parameters";
            // 
            // checkBoxBoltStandard
            // 
            this.structuresExtender.SetAttributeName(this.checkBoxBoltStandard, "BoltStandard");
            this.structuresExtender.SetAttributeTypeName(this.checkBoxBoltStandard, null);
            this.checkBoxBoltStandard.AutoSize = true;
            this.structuresExtender.SetBindPropertyName(this.checkBoxBoltStandard, "Checked");
            this.checkBoxBoltStandard.Checked = true;
            this.checkBoxBoltStandard.CheckState = System.Windows.Forms.CheckState.Checked;
            this.structuresExtender.SetIsFilter(this.checkBoxBoltStandard, true);
            this.checkBoxBoltStandard.Location = new System.Drawing.Point(160, 77);
            this.checkBoxBoltStandard.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxBoltStandard.Name = "checkBoxBoltStandard";
            this.checkBoxBoltStandard.Size = new System.Drawing.Size(22, 21);
            this.checkBoxBoltStandard.TabIndex = 8;
            this.checkBoxBoltStandard.UseVisualStyleBackColor = true;
            // 
            // checkBoxPlateLength
            // 
            this.structuresExtender.SetAttributeName(this.checkBoxPlateLength, "PlateLength");
            this.structuresExtender.SetAttributeTypeName(this.checkBoxPlateLength, null);
            this.checkBoxPlateLength.AutoSize = true;
            this.structuresExtender.SetBindPropertyName(this.checkBoxPlateLength, "Checked");
            this.checkBoxPlateLength.Checked = true;
            this.checkBoxPlateLength.CheckState = System.Windows.Forms.CheckState.Checked;
            this.structuresExtender.SetIsFilter(this.checkBoxPlateLength, true);
            this.checkBoxPlateLength.Location = new System.Drawing.Point(160, 37);
            this.checkBoxPlateLength.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxPlateLength.Name = "checkBoxPlateLength";
            this.checkBoxPlateLength.Size = new System.Drawing.Size(22, 21);
            this.checkBoxPlateLength.TabIndex = 7;
            this.checkBoxPlateLength.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.structuresExtender.SetAttributeName(this.tabPage2, null);
            this.structuresExtender.SetAttributeTypeName(this.tabPage2, null);
            this.tabPage2.BackColor = System.Drawing.Color.WhiteSmoke;
            this.structuresExtender.SetBindPropertyName(this.tabPage2, null);
            this.tabPage2.Controls.Add(this.textBox6);
            this.tabPage2.Controls.Add(this.textBox5);
            this.tabPage2.Controls.Add(this.ConnectionCodeTextBox);
            this.tabPage2.Controls.Add(this.ClassTextBox);
            this.tabPage2.Controls.Add(this.textBox3);
            this.tabPage2.Controls.Add(this.pictureBox1);
            this.tabPage2.Controls.Add(this.textBox2);
            this.tabPage2.Controls.Add(this.comboBox4);
            this.tabPage2.Controls.Add(this.checkBoxLocked);
            this.tabPage2.Controls.Add(this.UpDirectionComboBox);
            this.tabPage2.Controls.Add(this.checkBoxAutoConnection);
            this.tabPage2.Controls.Add(this.checkBoxConnectionCode);
            this.tabPage2.Controls.Add(this.checkBoxRotationAngleX);
            this.tabPage2.Controls.Add(this.checkBoxClass);
            this.tabPage2.Controls.Add(this.checkBoxAutoDefault);
            this.tabPage2.Controls.Add(this.checkBoxRotationAngleY);
            this.tabPage2.Controls.Add(this.checkBoxUpDirection);
            this.tabPage2.Controls.Add(this.label8);
            this.tabPage2.Controls.Add(this.label7);
            this.tabPage2.Controls.Add(this.label6);
            this.tabPage2.Controls.Add(this.label5);
            this.tabPage2.Controls.Add(this.label4);
            this.tabPage2.Controls.Add(this.label2);
            this.tabPage2.Location = new System.Drawing.Point(4, 29);
            this.tabPage2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tabPage2.Size = new System.Drawing.Size(776, 419);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "General";
            // 
            // textBox6
            // 
            this.structuresExtender.SetAttributeName(this.textBox6, "ac_root");
            this.structuresExtender.SetAttributeTypeName(this.textBox6, "String");
            this.structuresExtender.SetBindPropertyName(this.textBox6, "Text");
            this.textBox6.Location = new System.Drawing.Point(417, 348);
            this.textBox6.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox6.Name = "textBox6";
            this.textBox6.Size = new System.Drawing.Size(148, 26);
            this.textBox6.TabIndex = 7;
            this.textBox6.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.anyTextBox_KeyPress);
            // 
            // textBox5
            // 
            this.structuresExtender.SetAttributeName(this.textBox5, "ad_root");
            this.structuresExtender.SetAttributeTypeName(this.textBox5, "String");
            this.structuresExtender.SetBindPropertyName(this.textBox5, "Text");
            this.textBox5.Location = new System.Drawing.Point(417, 306);
            this.textBox5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox5.Name = "textBox5";
            this.textBox5.Size = new System.Drawing.Size(148, 26);
            this.textBox5.TabIndex = 6;
            this.textBox5.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.anyTextBox_KeyPress);
            // 
            // ConnectionCodeTextBox
            // 
            this.structuresExtender.SetAttributeName(this.ConnectionCodeTextBox, "joint_code");
            this.structuresExtender.SetAttributeTypeName(this.ConnectionCodeTextBox, "String");
            this.structuresExtender.SetBindPropertyName(this.ConnectionCodeTextBox, "Text");
            this.ConnectionCodeTextBox.Location = new System.Drawing.Point(417, 263);
            this.ConnectionCodeTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ConnectionCodeTextBox.Name = "ConnectionCodeTextBox";
            this.ConnectionCodeTextBox.Size = new System.Drawing.Size(148, 26);
            this.ConnectionCodeTextBox.TabIndex = 5;
            this.ConnectionCodeTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.anyTextBox_KeyPress);
            // 
            // ClassTextBox
            // 
            this.structuresExtender.SetAttributeName(this.ClassTextBox, "group_no");
            this.structuresExtender.SetAttributeTypeName(this.ClassTextBox, "Integer");
            this.structuresExtender.SetBindPropertyName(this.ClassTextBox, "Text");
            this.ClassTextBox.Location = new System.Drawing.Point(417, 223);
            this.ClassTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ClassTextBox.Name = "ClassTextBox";
            this.ClassTextBox.Size = new System.Drawing.Size(148, 26);
            this.ClassTextBox.TabIndex = 5;
            this.ClassTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.anyTextBox_KeyPress);
            // 
            // textBox3
            // 
            this.structuresExtender.SetAttributeName(this.textBox3, "zang1");
            this.structuresExtender.SetAttributeTypeName(this.textBox3, "Double");
            this.structuresExtender.SetBindPropertyName(this.textBox3, "Text");
            this.textBox3.Location = new System.Drawing.Point(417, 75);
            this.textBox3.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(148, 26);
            this.textBox3.TabIndex = 5;
            this.textBox3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.anyTextBox_KeyPress);
            // 
            // pictureBox1
            // 
            this.structuresExtender.SetAttributeName(this.pictureBox1, null);
            this.structuresExtender.SetAttributeTypeName(this.pictureBox1, null);
            this.structuresExtender.SetBindPropertyName(this.pictureBox1, null);
            this.pictureBox1.Image = global::SpliceConn.Properties.Resources.UpDirection;
            this.pictureBox1.Location = new System.Drawing.Point(248, 63);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(128, 117);
            this.pictureBox1.TabIndex = 4;
            this.pictureBox1.TabStop = false;
            // 
            // textBox2
            // 
            this.structuresExtender.SetAttributeName(this.textBox2, "zang2");
            this.structuresExtender.SetAttributeTypeName(this.textBox2, "Double");
            this.structuresExtender.SetBindPropertyName(this.textBox2, "Text");
            this.textBox2.Location = new System.Drawing.Point(417, 114);
            this.textBox2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(148, 26);
            this.textBox2.TabIndex = 3;
            this.textBox2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.anyTextBox_KeyPress);
            // 
            // comboBox4
            // 
            this.structuresExtender.SetAttributeName(this.comboBox4, "OBJECT_LOCKED");
            this.structuresExtender.SetAttributeTypeName(this.comboBox4, "Integer");
            this.structuresExtender.SetBindPropertyName(this.comboBox4, "SelectedIndex");
            this.comboBox4.FormattingEnabled = true;
            this.comboBox4.Items.AddRange(new object[] {
            "No",
            "Yes"});
            this.comboBox4.Location = new System.Drawing.Point(417, 186);
            this.comboBox4.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.comboBox4.Name = "comboBox4";
            this.comboBox4.Size = new System.Drawing.Size(148, 28);
            this.comboBox4.TabIndex = 2;
            this.comboBox4.SelectedIndexChanged += new System.EventHandler(this.anyComboBox_SelectedIndexChanged);
            // 
            // checkBoxLocked
            // 
            this.structuresExtender.SetAttributeName(this.checkBoxLocked, "OBJECT_LOCKED");
            this.structuresExtender.SetAttributeTypeName(this.checkBoxLocked, null);
            this.checkBoxLocked.AutoSize = true;
            this.structuresExtender.SetBindPropertyName(this.checkBoxLocked, "Checked");
            this.checkBoxLocked.Checked = true;
            this.checkBoxLocked.CheckState = System.Windows.Forms.CheckState.Checked;
            this.structuresExtender.SetIsFilter(this.checkBoxLocked, true);
            this.checkBoxLocked.Location = new System.Drawing.Point(386, 191);
            this.checkBoxLocked.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxLocked.Name = "checkBoxLocked";
            this.checkBoxLocked.Size = new System.Drawing.Size(22, 21);
            this.checkBoxLocked.TabIndex = 1;
            this.checkBoxLocked.UseVisualStyleBackColor = true;
            // 
            // UpDirectionComboBox
            // 
            this.structuresExtender.SetAttributeName(this.UpDirectionComboBox, "zsuunta");
            this.structuresExtender.SetAttributeTypeName(this.UpDirectionComboBox, "Integer");
            this.structuresExtender.SetBindPropertyName(this.UpDirectionComboBox, "SelectedIndex");
            this.UpDirectionComboBox.FormattingEnabled = true;
            this.UpDirectionComboBox.Items.AddRange(new object[] {
            "none",
            "-z",
            "+z",
            "+x",
            "-x",
            "-y",
            "+y",
            "auto"});
            this.UpDirectionComboBox.Location = new System.Drawing.Point(417, 35);
            this.UpDirectionComboBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.UpDirectionComboBox.Name = "UpDirectionComboBox";
            this.UpDirectionComboBox.Size = new System.Drawing.Size(148, 28);
            this.UpDirectionComboBox.TabIndex = 2;
            this.UpDirectionComboBox.SelectedIndexChanged += new System.EventHandler(this.anyComboBox_SelectedIndexChanged);
            // 
            // checkBoxAutoConnection
            // 
            this.structuresExtender.SetAttributeName(this.checkBoxAutoConnection, "ac_root");
            this.structuresExtender.SetAttributeTypeName(this.checkBoxAutoConnection, null);
            this.checkBoxAutoConnection.AutoSize = true;
            this.structuresExtender.SetBindPropertyName(this.checkBoxAutoConnection, "Checked");
            this.checkBoxAutoConnection.Checked = true;
            this.checkBoxAutoConnection.CheckState = System.Windows.Forms.CheckState.Checked;
            this.structuresExtender.SetIsFilter(this.checkBoxAutoConnection, true);
            this.checkBoxAutoConnection.Location = new System.Drawing.Point(386, 352);
            this.checkBoxAutoConnection.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxAutoConnection.Name = "checkBoxAutoConnection";
            this.checkBoxAutoConnection.Size = new System.Drawing.Size(22, 21);
            this.checkBoxAutoConnection.TabIndex = 1;
            this.checkBoxAutoConnection.UseVisualStyleBackColor = true;
            // 
            // checkBoxConnectionCode
            // 
            this.structuresExtender.SetAttributeName(this.checkBoxConnectionCode, "joint_code");
            this.structuresExtender.SetAttributeTypeName(this.checkBoxConnectionCode, null);
            this.checkBoxConnectionCode.AutoSize = true;
            this.structuresExtender.SetBindPropertyName(this.checkBoxConnectionCode, "Checked");
            this.checkBoxConnectionCode.Checked = true;
            this.checkBoxConnectionCode.CheckState = System.Windows.Forms.CheckState.Checked;
            this.structuresExtender.SetIsFilter(this.checkBoxConnectionCode, true);
            this.checkBoxConnectionCode.Location = new System.Drawing.Point(386, 266);
            this.checkBoxConnectionCode.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxConnectionCode.Name = "checkBoxConnectionCode";
            this.checkBoxConnectionCode.Size = new System.Drawing.Size(22, 21);
            this.checkBoxConnectionCode.TabIndex = 1;
            this.checkBoxConnectionCode.UseVisualStyleBackColor = true;
            // 
            // checkBoxRotationAngleX
            // 
            this.structuresExtender.SetAttributeName(this.checkBoxRotationAngleX, "zang2");
            this.structuresExtender.SetAttributeTypeName(this.checkBoxRotationAngleX, null);
            this.checkBoxRotationAngleX.AutoSize = true;
            this.structuresExtender.SetBindPropertyName(this.checkBoxRotationAngleX, "Checked");
            this.checkBoxRotationAngleX.Checked = true;
            this.checkBoxRotationAngleX.CheckState = System.Windows.Forms.CheckState.Checked;
            this.structuresExtender.SetIsFilter(this.checkBoxRotationAngleX, true);
            this.checkBoxRotationAngleX.Location = new System.Drawing.Point(386, 118);
            this.checkBoxRotationAngleX.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxRotationAngleX.Name = "checkBoxRotationAngleX";
            this.checkBoxRotationAngleX.Size = new System.Drawing.Size(22, 21);
            this.checkBoxRotationAngleX.TabIndex = 1;
            this.checkBoxRotationAngleX.UseVisualStyleBackColor = true;
            // 
            // checkBoxClass
            // 
            this.structuresExtender.SetAttributeName(this.checkBoxClass, "group_no");
            this.structuresExtender.SetAttributeTypeName(this.checkBoxClass, null);
            this.checkBoxClass.AutoSize = true;
            this.structuresExtender.SetBindPropertyName(this.checkBoxClass, "Checked");
            this.checkBoxClass.Checked = true;
            this.checkBoxClass.CheckState = System.Windows.Forms.CheckState.Checked;
            this.structuresExtender.SetIsFilter(this.checkBoxClass, true);
            this.checkBoxClass.Location = new System.Drawing.Point(386, 226);
            this.checkBoxClass.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxClass.Name = "checkBoxClass";
            this.checkBoxClass.Size = new System.Drawing.Size(22, 21);
            this.checkBoxClass.TabIndex = 1;
            this.checkBoxClass.UseVisualStyleBackColor = true;
            // 
            // checkBoxAutoDefault
            // 
            this.structuresExtender.SetAttributeName(this.checkBoxAutoDefault, "ad_root");
            this.structuresExtender.SetAttributeTypeName(this.checkBoxAutoDefault, null);
            this.checkBoxAutoDefault.AutoSize = true;
            this.structuresExtender.SetBindPropertyName(this.checkBoxAutoDefault, "Checked");
            this.checkBoxAutoDefault.Checked = true;
            this.checkBoxAutoDefault.CheckState = System.Windows.Forms.CheckState.Checked;
            this.structuresExtender.SetIsFilter(this.checkBoxAutoDefault, true);
            this.checkBoxAutoDefault.Location = new System.Drawing.Point(386, 311);
            this.checkBoxAutoDefault.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxAutoDefault.Name = "checkBoxAutoDefault";
            this.checkBoxAutoDefault.Size = new System.Drawing.Size(22, 21);
            this.checkBoxAutoDefault.TabIndex = 1;
            this.checkBoxAutoDefault.UseVisualStyleBackColor = true;
            // 
            // checkBoxRotationAngleY
            // 
            this.structuresExtender.SetAttributeName(this.checkBoxRotationAngleY, "zang1");
            this.structuresExtender.SetAttributeTypeName(this.checkBoxRotationAngleY, null);
            this.checkBoxRotationAngleY.AutoSize = true;
            this.structuresExtender.SetBindPropertyName(this.checkBoxRotationAngleY, "Checked");
            this.checkBoxRotationAngleY.Checked = true;
            this.checkBoxRotationAngleY.CheckState = System.Windows.Forms.CheckState.Checked;
            this.structuresExtender.SetIsFilter(this.checkBoxRotationAngleY, true);
            this.checkBoxRotationAngleY.Location = new System.Drawing.Point(386, 78);
            this.checkBoxRotationAngleY.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxRotationAngleY.Name = "checkBoxRotationAngleY";
            this.checkBoxRotationAngleY.Size = new System.Drawing.Size(22, 21);
            this.checkBoxRotationAngleY.TabIndex = 1;
            this.checkBoxRotationAngleY.UseVisualStyleBackColor = true;
            // 
            // checkBoxUpDirection
            // 
            this.structuresExtender.SetAttributeName(this.checkBoxUpDirection, "zsuunta");
            this.structuresExtender.SetAttributeTypeName(this.checkBoxUpDirection, null);
            this.checkBoxUpDirection.AutoSize = true;
            this.structuresExtender.SetBindPropertyName(this.checkBoxUpDirection, "Checked");
            this.checkBoxUpDirection.Checked = true;
            this.checkBoxUpDirection.CheckState = System.Windows.Forms.CheckState.Checked;
            this.structuresExtender.SetIsFilter(this.checkBoxUpDirection, true);
            this.checkBoxUpDirection.Location = new System.Drawing.Point(386, 40);
            this.checkBoxUpDirection.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxUpDirection.Name = "checkBoxUpDirection";
            this.checkBoxUpDirection.Size = new System.Drawing.Size(22, 21);
            this.checkBoxUpDirection.TabIndex = 1;
            this.checkBoxUpDirection.UseVisualStyleBackColor = true;
            // 
            // label8
            // 
            this.structuresExtender.SetAttributeName(this.label8, null);
            this.structuresExtender.SetAttributeTypeName(this.label8, null);
            this.label8.AutoSize = true;
            this.structuresExtender.SetBindPropertyName(this.label8, null);
            this.label8.Location = new System.Drawing.Point(42, 360);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(199, 20);
            this.label8.TabIndex = 0;
            this.label8.Text = "AutoConnection rule group";
            // 
            // label7
            // 
            this.structuresExtender.SetAttributeName(this.label7, null);
            this.structuresExtender.SetAttributeTypeName(this.label7, null);
            this.label7.AutoSize = true;
            this.structuresExtender.SetBindPropertyName(this.label7, null);
            this.label7.Location = new System.Drawing.Point(42, 318);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(178, 20);
            this.label7.TabIndex = 0;
            this.label7.Text = "AutoDefaults rule group";
            // 
            // label6
            // 
            this.structuresExtender.SetAttributeName(this.label6, null);
            this.structuresExtender.SetAttributeTypeName(this.label6, null);
            this.label6.AutoSize = true;
            this.structuresExtender.SetBindPropertyName(this.label6, null);
            this.label6.Location = new System.Drawing.Point(42, 275);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(129, 20);
            this.label6.TabIndex = 0;
            this.label6.Text = "Connection code";
            // 
            // label5
            // 
            this.structuresExtender.SetAttributeName(this.label5, null);
            this.structuresExtender.SetAttributeTypeName(this.label5, null);
            this.label5.AutoSize = true;
            this.structuresExtender.SetBindPropertyName(this.label5, null);
            this.label5.Location = new System.Drawing.Point(42, 234);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 20);
            this.label5.TabIndex = 0;
            this.label5.Text = "Class";
            // 
            // label4
            // 
            this.structuresExtender.SetAttributeName(this.label4, null);
            this.structuresExtender.SetAttributeTypeName(this.label4, null);
            this.label4.AutoSize = true;
            this.structuresExtender.SetBindPropertyName(this.label4, null);
            this.label4.Location = new System.Drawing.Point(42, 191);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "Locked";
            // 
            // label2
            // 
            this.structuresExtender.SetAttributeName(this.label2, null);
            this.structuresExtender.SetAttributeTypeName(this.label2, null);
            this.label2.AutoSize = true;
            this.structuresExtender.SetBindPropertyName(this.label2, null);
            this.label2.Location = new System.Drawing.Point(42, 40);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(94, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Up direction";
            // 
            // SpliceConnection
            // 
            this.structuresExtender.SetAttributeName(this, null);
            this.structuresExtender.SetAttributeTypeName(this, null);
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.structuresExtender.SetBindPropertyName(this, null);
            this.ClientSize = new System.Drawing.Size(784, 563);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.saveLoad1);
            this.Controls.Add(this.okApplyModifyGetOnOffCancel1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "SpliceConnection";
            this.Text = "Splice Connection";
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

    }

    #endregion

    private System.Windows.Forms.TextBox PlateLengthTextBox;
    private System.Windows.Forms.Label label1;
    private System.Windows.Forms.Label label3;
    private System.Windows.Forms.TextBox BoltStandardTextBox;
    private Tekla.Structures.Dialog.UIControls.OkApplyModifyGetOnOffCancel okApplyModifyGetOnOffCancel1;
    private Tekla.Structures.Dialog.UIControls.SaveLoad saveLoad1;
    private System.Windows.Forms.TabControl tabControl1;
    private System.Windows.Forms.TabPage tabPage1;
    private System.Windows.Forms.TabPage tabPage2;
    private System.Windows.Forms.ComboBox UpDirectionComboBox;
    private System.Windows.Forms.CheckBox checkBoxUpDirection;
    private System.Windows.Forms.Label label8;
    private System.Windows.Forms.Label label7;
    private System.Windows.Forms.Label label6;
    private System.Windows.Forms.Label label5;
    private System.Windows.Forms.Label label4;
    private System.Windows.Forms.Label label2;
    private System.Windows.Forms.PictureBox pictureBox1;
    private System.Windows.Forms.TextBox textBox2;
    private System.Windows.Forms.TextBox textBox3;
    private System.Windows.Forms.CheckBox checkBoxRotationAngleX;
    private System.Windows.Forms.CheckBox checkBoxRotationAngleY;
    private System.Windows.Forms.TextBox ConnectionCodeTextBox;
    private System.Windows.Forms.TextBox ClassTextBox;
    private System.Windows.Forms.ComboBox comboBox4;
    private System.Windows.Forms.CheckBox checkBoxLocked;
    private System.Windows.Forms.CheckBox checkBoxAutoConnection;
    private System.Windows.Forms.CheckBox checkBoxConnectionCode;
    private System.Windows.Forms.CheckBox checkBoxClass;
    private System.Windows.Forms.CheckBox checkBoxAutoDefault;
    private System.Windows.Forms.TextBox textBox6;
    private System.Windows.Forms.TextBox textBox5;
    private System.Windows.Forms.CheckBox checkBoxBoltStandard;
    private System.Windows.Forms.CheckBox checkBoxPlateLength;
}
