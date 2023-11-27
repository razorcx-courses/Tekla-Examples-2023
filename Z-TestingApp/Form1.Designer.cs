namespace Z_TestingApp
{
    partial class Form1
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
            this.checkBoxBoltStandard = new System.Windows.Forms.CheckBox();
            this.checkBoxPlateLength = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.PlateLengthTextBox = new System.Windows.Forms.TextBox();
            this.BoltStandardTextBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // checkBoxBoltStandard
            // 
            this.checkBoxBoltStandard.AutoSize = true;
            this.checkBoxBoltStandard.Checked = true;
            this.checkBoxBoltStandard.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxBoltStandard.Location = new System.Drawing.Point(185, 102);
            this.checkBoxBoltStandard.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxBoltStandard.Name = "checkBoxBoltStandard";
            this.checkBoxBoltStandard.Size = new System.Drawing.Size(22, 21);
            this.checkBoxBoltStandard.TabIndex = 14;
            this.checkBoxBoltStandard.UseVisualStyleBackColor = true;
            // 
            // checkBoxPlateLength
            // 
            this.checkBoxPlateLength.AutoSize = true;
            this.checkBoxPlateLength.Checked = true;
            this.checkBoxPlateLength.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkBoxPlateLength.Location = new System.Drawing.Point(185, 62);
            this.checkBoxPlateLength.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.checkBoxPlateLength.Name = "checkBoxPlateLength";
            this.checkBoxPlateLength.Size = new System.Drawing.Size(22, 21);
            this.checkBoxPlateLength.TabIndex = 13;
            this.checkBoxPlateLength.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(65, 62);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 20);
            this.label1.TabIndex = 10;
            this.label1.Text = "Plate length";
            // 
            // PlateLengthTextBox
            // 
            this.PlateLengthTextBox.Location = new System.Drawing.Point(217, 57);
            this.PlateLengthTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.PlateLengthTextBox.Name = "PlateLengthTextBox";
            this.PlateLengthTextBox.Size = new System.Drawing.Size(133, 26);
            this.PlateLengthTextBox.TabIndex = 9;
            this.PlateLengthTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.anyTextBox_KeyPress);
            // 
            // BoltStandardTextBox
            // 
            this.BoltStandardTextBox.Location = new System.Drawing.Point(217, 97);
            this.BoltStandardTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.BoltStandardTextBox.Name = "BoltStandardTextBox";
            this.BoltStandardTextBox.Size = new System.Drawing.Size(133, 26);
            this.BoltStandardTextBox.TabIndex = 11;
            this.BoltStandardTextBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.anyTextBox_KeyPress);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(65, 102);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(104, 20);
            this.label3.TabIndex = 12;
            this.label3.Text = "Bolt standard";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.checkBoxBoltStandard);
            this.Controls.Add(this.checkBoxPlateLength);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.PlateLengthTextBox);
            this.Controls.Add(this.BoltStandardTextBox);
            this.Controls.Add(this.label3);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBoxBoltStandard;
        private System.Windows.Forms.CheckBox checkBoxPlateLength;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox PlateLengthTextBox;
        private System.Windows.Forms.TextBox BoltStandardTextBox;
        private System.Windows.Forms.Label label3;
    }
}

