namespace BasicViews
{
    partial class BasicViews
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
            this.button1 = new System.Windows.Forms.Button();
            this.createTopView = new System.Windows.Forms.CheckBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.create3dView = new System.Windows.Forms.CheckBox();
            this.createEndView = new System.Windows.Forms.CheckBox();
            this.createFrontView = new System.Windows.Forms.CheckBox();
            this.openDrawings = new System.Windows.Forms.CheckBox();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.trackBar2 = new System.Windows.Forms.TrackBar();
            this.label3 = new System.Windows.Forms.Label();
            this.trackBar3 = new System.Windows.Forms.TrackBar();
            this.label4 = new System.Windows.Forms.Label();
            this.trackBar5 = new System.Windows.Forms.TrackBar();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.panel1 = new System.Windows.Forms.Panel();
            this.domainUpDown1 = new System.Windows.Forms.DomainUpDown();
            this.domainUpDown2 = new System.Windows.Forms.DomainUpDown();
            this.domainUpDown3 = new System.Windows.Forms.DomainUpDown();
            this.domainUpDown4 = new System.Windows.Forms.DomainUpDown();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar5)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(18, 18);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(86, 37);
            this.button1.TabIndex = 0;
            this.button1.Text = "Create";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.Create_click);
            // 
            // createTopView
            // 
            this.createTopView.AutoSize = true;
            this.createTopView.Location = new System.Drawing.Point(9, 29);
            this.createTopView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.createTopView.Name = "createTopView";
            this.createTopView.Size = new System.Drawing.Size(96, 24);
            this.createTopView.TabIndex = 1;
            this.createTopView.Text = "Top view";
            this.createTopView.UseVisualStyleBackColor = true;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.create3dView);
            this.groupBox1.Controls.Add(this.createEndView);
            this.groupBox1.Controls.Add(this.createFrontView);
            this.groupBox1.Controls.Add(this.createTopView);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Right;
            this.groupBox1.Location = new System.Drawing.Point(617, 0);
            this.groupBox1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.groupBox1.Size = new System.Drawing.Size(225, 604);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Views to be created";
            // 
            // create3dView
            // 
            this.create3dView.AutoSize = true;
            this.create3dView.Location = new System.Drawing.Point(9, 135);
            this.create3dView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.create3dView.Name = "create3dView";
            this.create3dView.Size = new System.Drawing.Size(87, 24);
            this.create3dView.TabIndex = 4;
            this.create3dView.Text = "3d view";
            this.create3dView.UseVisualStyleBackColor = true;
            // 
            // createEndView
            // 
            this.createEndView.AutoSize = true;
            this.createEndView.Location = new System.Drawing.Point(9, 100);
            this.createEndView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.createEndView.Name = "createEndView";
            this.createEndView.Size = new System.Drawing.Size(98, 24);
            this.createEndView.TabIndex = 3;
            this.createEndView.Text = "End view";
            this.createEndView.UseVisualStyleBackColor = true;
            // 
            // createFrontView
            // 
            this.createFrontView.AutoSize = true;
            this.createFrontView.Location = new System.Drawing.Point(9, 65);
            this.createFrontView.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.createFrontView.Name = "createFrontView";
            this.createFrontView.Size = new System.Drawing.Size(107, 24);
            this.createFrontView.TabIndex = 2;
            this.createFrontView.Text = "Front view";
            this.createFrontView.UseVisualStyleBackColor = true;
            // 
            // openDrawings
            // 
            this.openDrawings.AutoSize = true;
            this.openDrawings.Location = new System.Drawing.Point(18, 65);
            this.openDrawings.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.openDrawings.Name = "openDrawings";
            this.openDrawings.Size = new System.Drawing.Size(141, 24);
            this.openDrawings.TabIndex = 3;
            this.openDrawings.Text = "Open drawings";
            this.openDrawings.UseVisualStyleBackColor = true;
            // 
            // trackBar1
            // 
            this.trackBar1.Location = new System.Drawing.Point(18, 331);
            this.trackBar1.Maximum = 20;
            this.trackBar1.Minimum = 2;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Size = new System.Drawing.Size(422, 69);
            this.trackBar1.TabIndex = 4;
            this.trackBar1.Value = 8;
            this.trackBar1.Scroll += new System.EventHandler(this.anyTrackBar_Scroll);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 302);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 20);
            this.label1.TabIndex = 5;
            this.label1.Text = "Drawing Scale (2-20)";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 117);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(206, 20);
            this.label2.TabIndex = 7;
            this.label2.Text = "Drawing Position X (0-1000)";
            // 
            // trackBar2
            // 
            this.trackBar2.Location = new System.Drawing.Point(18, 145);
            this.trackBar2.Maximum = 1000;
            this.trackBar2.Name = "trackBar2";
            this.trackBar2.Size = new System.Drawing.Size(422, 69);
            this.trackBar2.TabIndex = 6;
            this.trackBar2.Value = 40;
            this.trackBar2.Scroll += new System.EventHandler(this.anyTrackBar_Scroll);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 205);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(206, 20);
            this.label3.TabIndex = 9;
            this.label3.Text = "Drawing Position Y (0-1000)";
            // 
            // trackBar3
            // 
            this.trackBar3.Location = new System.Drawing.Point(12, 233);
            this.trackBar3.Maximum = 1000;
            this.trackBar3.Name = "trackBar3";
            this.trackBar3.Size = new System.Drawing.Size(422, 69);
            this.trackBar3.TabIndex = 8;
            this.trackBar3.Value = 40;
            this.trackBar3.Scroll += new System.EventHandler(this.anyTrackBar_Scroll);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(6, 403);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(161, 20);
            this.label4.TabIndex = 11;
            this.label4.Text = "Min Length (50-1500)";
            // 
            // trackBar5
            // 
            this.trackBar5.Location = new System.Drawing.Point(18, 432);
            this.trackBar5.Maximum = 1500;
            this.trackBar5.Minimum = 50;
            this.trackBar5.Name = "trackBar5";
            this.trackBar5.Size = new System.Drawing.Size(422, 69);
            this.trackBar5.TabIndex = 200;
            this.trackBar5.Value = 200;
            this.trackBar5.Scroll += new System.EventHandler(this.anyTrackBar_Scroll);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(18, 550);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(416, 26);
            this.textBox1.TabIndex = 201;
            this.textBox1.Text = "This is the BEAM name which can be changed with API";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 517);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 20);
            this.label5.TabIndex = 202;
            this.label5.Text = "Title Text";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(131, 18);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(159, 37);
            this.button2.TabIndex = 203;
            this.button2.Text = "Test";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(296, 18);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(159, 37);
            this.button3.TabIndex = 204;
            this.button3.Text = "Views";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.domainUpDown4);
            this.panel1.Controls.Add(this.domainUpDown3);
            this.panel1.Controls.Add(this.domainUpDown2);
            this.panel1.Controls.Add(this.domainUpDown1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(474, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(143, 604);
            this.panel1.TabIndex = 205;
            // 
            // domainUpDown1
            // 
            this.domainUpDown1.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.domainUpDown1.Items.Add("2");
            this.domainUpDown1.Items.Add("3");
            this.domainUpDown1.Items.Add("4");
            this.domainUpDown1.Items.Add("5");
            this.domainUpDown1.Items.Add("6");
            this.domainUpDown1.Items.Add("7");
            this.domainUpDown1.Items.Add("8");
            this.domainUpDown1.Items.Add("9");
            this.domainUpDown1.Items.Add("10");
            this.domainUpDown1.Items.Add("11");
            this.domainUpDown1.Items.Add("12");
            this.domainUpDown1.Items.Add("13");
            this.domainUpDown1.Items.Add("14");
            this.domainUpDown1.Items.Add("15");
            this.domainUpDown1.Items.Add("16");
            this.domainUpDown1.Items.Add("17");
            this.domainUpDown1.Items.Add("18");
            this.domainUpDown1.Items.Add("19");
            this.domainUpDown1.Items.Add("20");
            this.domainUpDown1.Location = new System.Drawing.Point(21, 331);
            this.domainUpDown1.Name = "domainUpDown1";
            this.domainUpDown1.Size = new System.Drawing.Size(87, 39);
            this.domainUpDown1.TabIndex = 0;
            this.domainUpDown1.Text = "8";
            this.domainUpDown1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.domainUpDown1.SelectedItemChanged += new System.EventHandler(this.domainUpDown1_SelectedItemChanged);
            // 
            // domainUpDown2
            // 
            this.domainUpDown2.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.domainUpDown2.Items.Add("50");
            this.domainUpDown2.Items.Add("75");
            this.domainUpDown2.Items.Add("100");
            this.domainUpDown2.Items.Add("125");
            this.domainUpDown2.Items.Add("150");
            this.domainUpDown2.Items.Add("175");
            this.domainUpDown2.Items.Add("200");
            this.domainUpDown2.Items.Add("225");
            this.domainUpDown2.Items.Add("250");
            this.domainUpDown2.Items.Add("275");
            this.domainUpDown2.Items.Add("300");
            this.domainUpDown2.Items.Add("325");
            this.domainUpDown2.Items.Add("350");
            this.domainUpDown2.Items.Add("375");
            this.domainUpDown2.Items.Add("450");
            this.domainUpDown2.Items.Add("425");
            this.domainUpDown2.Items.Add("450");
            this.domainUpDown2.Items.Add("475");
            this.domainUpDown2.Items.Add("500");
            this.domainUpDown2.Items.Add("550");
            this.domainUpDown2.Items.Add("600");
            this.domainUpDown2.Items.Add("650");
            this.domainUpDown2.Items.Add("700");
            this.domainUpDown2.Items.Add("750");
            this.domainUpDown2.Items.Add("800");
            this.domainUpDown2.Items.Add("850");
            this.domainUpDown2.Items.Add("900");
            this.domainUpDown2.Items.Add("1000");
            this.domainUpDown2.Items.Add("1100");
            this.domainUpDown2.Items.Add("1200");
            this.domainUpDown2.Items.Add("1300");
            this.domainUpDown2.Items.Add("1400");
            this.domainUpDown2.Items.Add("1500");
            this.domainUpDown2.Location = new System.Drawing.Point(21, 432);
            this.domainUpDown2.Name = "domainUpDown2";
            this.domainUpDown2.Size = new System.Drawing.Size(87, 39);
            this.domainUpDown2.TabIndex = 1;
            this.domainUpDown2.Text = "200";
            this.domainUpDown2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.domainUpDown2.SelectedItemChanged += new System.EventHandler(this.domainUpDown2_SelectedItemChanged);
            // 
            // domainUpDown3
            // 
            this.domainUpDown3.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.domainUpDown3.Items.Add("0");
            this.domainUpDown3.Items.Add("50");
            this.domainUpDown3.Items.Add("75");
            this.domainUpDown3.Items.Add("100");
            this.domainUpDown3.Items.Add("125");
            this.domainUpDown3.Items.Add("150");
            this.domainUpDown3.Items.Add("175");
            this.domainUpDown3.Items.Add("200");
            this.domainUpDown3.Items.Add("225");
            this.domainUpDown3.Items.Add("250");
            this.domainUpDown3.Items.Add("275");
            this.domainUpDown3.Items.Add("300");
            this.domainUpDown3.Items.Add("325");
            this.domainUpDown3.Items.Add("350");
            this.domainUpDown3.Items.Add("375");
            this.domainUpDown3.Items.Add("450");
            this.domainUpDown3.Items.Add("425");
            this.domainUpDown3.Items.Add("450");
            this.domainUpDown3.Items.Add("475");
            this.domainUpDown3.Items.Add("500");
            this.domainUpDown3.Items.Add("550");
            this.domainUpDown3.Items.Add("600");
            this.domainUpDown3.Items.Add("650");
            this.domainUpDown3.Items.Add("700");
            this.domainUpDown3.Items.Add("750");
            this.domainUpDown3.Items.Add("800");
            this.domainUpDown3.Items.Add("850");
            this.domainUpDown3.Items.Add("900");
            this.domainUpDown3.Items.Add("1000");
            this.domainUpDown3.Location = new System.Drawing.Point(21, 233);
            this.domainUpDown3.Name = "domainUpDown3";
            this.domainUpDown3.Size = new System.Drawing.Size(87, 39);
            this.domainUpDown3.TabIndex = 2;
            this.domainUpDown3.Text = "50";
            this.domainUpDown3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.domainUpDown3.SelectedItemChanged += new System.EventHandler(this.domainUpDown3_SelectedItemChanged);
            // 
            // domainUpDown4
            // 
            this.domainUpDown4.Font = new System.Drawing.Font("Nirmala UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.domainUpDown4.Items.Add("0");
            this.domainUpDown4.Items.Add("50");
            this.domainUpDown4.Items.Add("75");
            this.domainUpDown4.Items.Add("100");
            this.domainUpDown4.Items.Add("125");
            this.domainUpDown4.Items.Add("150");
            this.domainUpDown4.Items.Add("175");
            this.domainUpDown4.Items.Add("200");
            this.domainUpDown4.Items.Add("225");
            this.domainUpDown4.Items.Add("250");
            this.domainUpDown4.Items.Add("275");
            this.domainUpDown4.Items.Add("300");
            this.domainUpDown4.Items.Add("325");
            this.domainUpDown4.Items.Add("350");
            this.domainUpDown4.Items.Add("375");
            this.domainUpDown4.Items.Add("450");
            this.domainUpDown4.Items.Add("425");
            this.domainUpDown4.Items.Add("450");
            this.domainUpDown4.Items.Add("475");
            this.domainUpDown4.Items.Add("500");
            this.domainUpDown4.Items.Add("550");
            this.domainUpDown4.Items.Add("600");
            this.domainUpDown4.Items.Add("650");
            this.domainUpDown4.Items.Add("700");
            this.domainUpDown4.Items.Add("750");
            this.domainUpDown4.Items.Add("800");
            this.domainUpDown4.Items.Add("850");
            this.domainUpDown4.Items.Add("900");
            this.domainUpDown4.Items.Add("1000");
            this.domainUpDown4.Location = new System.Drawing.Point(21, 145);
            this.domainUpDown4.Name = "domainUpDown4";
            this.domainUpDown4.Size = new System.Drawing.Size(87, 39);
            this.domainUpDown4.TabIndex = 3;
            this.domainUpDown4.Text = "50";
            this.domainUpDown4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.domainUpDown4.SelectedItemChanged += new System.EventHandler(this.domainUpDown4_SelectedItemChanged);
            // 
            // BasicViews
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(842, 604);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.trackBar5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.trackBar3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.trackBar2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.openDrawings);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(409, 216);
            this.Name = "BasicViews";
            this.Text = "Create basic views";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.BasicViews_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar5)).EndInit();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.CheckBox createTopView;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox create3dView;
        private System.Windows.Forms.CheckBox createEndView;
        private System.Windows.Forms.CheckBox createFrontView;
        private System.Windows.Forms.CheckBox openDrawings;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TrackBar trackBar2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TrackBar trackBar3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TrackBar trackBar5;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DomainUpDown domainUpDown1;
        private System.Windows.Forms.DomainUpDown domainUpDown2;
        private System.Windows.Forms.DomainUpDown domainUpDown4;
        private System.Windows.Forms.DomainUpDown domainUpDown3;
    }
}

