namespace FlarmTerminal.GUI
{
    partial class FlarmConfigEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.TextBox textBoxID;
        private System.Windows.Forms.ComboBox comboBoxNMEAOUT;
        private System.Windows.Forms.ComboBox comboBoxACFT;
        private System.Windows.Forms.ComboBox comboBoxCFLAGS;
        private System.Windows.Forms.ComboBox comboBoxPRIV;
        private System.Windows.Forms.ComboBox comboBoxNOTRACK;
        private System.Windows.Forms.NumericUpDown numericUpDownTHRE;
        private System.Windows.Forms.Label labelThreUnit;
        private System.Windows.Forms.TextBox textBoxPILOT;
        private System.Windows.Forms.TextBox textBoxCOMPCLASS;
        private System.Windows.Forms.TextBox textBoxCOMPID;
        private System.Windows.Forms.TextBox textBoxGLIDERID;
        private System.Windows.Forms.TextBox textBoxGLIDERTYPE;
        private System.Windows.Forms.ComboBox comboBoxBAUD;
        private System.Windows.Forms.Button buttonLoad;
        private System.Windows.Forms.Button buttonSave;
        private System.Windows.Forms.Button buttonPreview;
        private System.Windows.Forms.Button buttonCancel;
        private System.Windows.Forms.TabControl tabControlMain;
        private System.Windows.Forms.TabPage tabPageBasic;
        private System.Windows.Forms.TabPage tabPageIGC;
        private System.Windows.Forms.Label labelID;
        private System.Windows.Forms.Label labelNMEAOUT;
        private System.Windows.Forms.Label labelACFT;
        private System.Windows.Forms.Label labelCFLAGS;
        private System.Windows.Forms.Label labelPRIV;
        private System.Windows.Forms.Label labelNOTRACK;
        private System.Windows.Forms.Label labelTHRE;
        private System.Windows.Forms.Label labelLOGINT;
        private System.Windows.Forms.NumericUpDown numericUpDownLOGINT;
        private System.Windows.Forms.Label labelPILOT;
        private System.Windows.Forms.Label labelCOMPCLASS;
        private System.Windows.Forms.Label labelCOMPID;
        private System.Windows.Forms.Label labelGLIDERID;
        private System.Windows.Forms.Label labelGLIDERTYPE;
        private System.Windows.Forms.Label labelBAUD;
        private System.Windows.Forms.ToolTip toolTip1;

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

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FlarmConfigEditor));
            tabControlMain = new System.Windows.Forms.TabControl();
            tabPageIGC = new System.Windows.Forms.TabPage();
            label1 = new System.Windows.Forms.Label();
            labelLOGINT = new System.Windows.Forms.Label();
            numericUpDownLOGINT = new System.Windows.Forms.NumericUpDown();
            labelPILOT = new System.Windows.Forms.Label();
            textBoxPILOT = new System.Windows.Forms.TextBox();
            labelCOMPCLASS = new System.Windows.Forms.Label();
            textBoxCOMPCLASS = new System.Windows.Forms.TextBox();
            labelCOMPID = new System.Windows.Forms.Label();
            textBoxCOMPID = new System.Windows.Forms.TextBox();
            labelGLIDERID = new System.Windows.Forms.Label();
            textBoxGLIDERID = new System.Windows.Forms.TextBox();
            labelGLIDERTYPE = new System.Windows.Forms.Label();
            textBoxGLIDERTYPE = new System.Windows.Forms.TextBox();
            tabPageBasic = new System.Windows.Forms.TabPage();
            labelID = new System.Windows.Forms.Label();
            textBoxID = new System.Windows.Forms.TextBox();
            labelNMEAOUT = new System.Windows.Forms.Label();
            comboBoxNMEAOUT = new System.Windows.Forms.ComboBox();
            labelACFT = new System.Windows.Forms.Label();
            comboBoxACFT = new System.Windows.Forms.ComboBox();
            labelCFLAGS = new System.Windows.Forms.Label();
            comboBoxCFLAGS = new System.Windows.Forms.ComboBox();
            labelPRIV = new System.Windows.Forms.Label();
            comboBoxPRIV = new System.Windows.Forms.ComboBox();
            labelNOTRACK = new System.Windows.Forms.Label();
            comboBoxNOTRACK = new System.Windows.Forms.ComboBox();
            labelTHRE = new System.Windows.Forms.Label();
            numericUpDownTHRE = new System.Windows.Forms.NumericUpDown();
            labelThreUnit = new System.Windows.Forms.Label();
            labelBAUD = new System.Windows.Forms.Label();
            comboBoxBAUD = new System.Windows.Forms.ComboBox();
            buttonLoad = new System.Windows.Forms.Button();
            buttonSave = new System.Windows.Forms.Button();
            buttonPreview = new System.Windows.Forms.Button();
            buttonCancel = new System.Windows.Forms.Button();
            toolTip1 = new System.Windows.Forms.ToolTip(components);
            pictureBox1 = new System.Windows.Forms.PictureBox();
            tabControlMain.SuspendLayout();
            tabPageIGC.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownLOGINT).BeginInit();
            tabPageBasic.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownTHRE).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // tabControlMain
            // 
            tabControlMain.Controls.Add(tabPageIGC);
            tabControlMain.Controls.Add(tabPageBasic);
            tabControlMain.Location = new System.Drawing.Point(12, 12);
            tabControlMain.Name = "tabControlMain";
            tabControlMain.SelectedIndex = 0;
            tabControlMain.Size = new System.Drawing.Size(577, 370);
            tabControlMain.TabIndex = 0;
            // 
            // tabPageIGC
            // 
            tabPageIGC.Controls.Add(pictureBox1);
            tabPageIGC.Controls.Add(label1);
            tabPageIGC.Controls.Add(labelLOGINT);
            tabPageIGC.Controls.Add(numericUpDownLOGINT);
            tabPageIGC.Controls.Add(labelPILOT);
            tabPageIGC.Controls.Add(textBoxPILOT);
            tabPageIGC.Controls.Add(labelCOMPCLASS);
            tabPageIGC.Controls.Add(textBoxCOMPCLASS);
            tabPageIGC.Controls.Add(labelCOMPID);
            tabPageIGC.Controls.Add(textBoxCOMPID);
            tabPageIGC.Controls.Add(labelGLIDERID);
            tabPageIGC.Controls.Add(textBoxGLIDERID);
            tabPageIGC.Controls.Add(labelGLIDERTYPE);
            tabPageIGC.Controls.Add(textBoxGLIDERTYPE);
            tabPageIGC.Location = new System.Drawing.Point(4, 24);
            tabPageIGC.Name = "tabPageIGC";
            tabPageIGC.Padding = new System.Windows.Forms.Padding(3);
            tabPageIGC.Size = new System.Drawing.Size(569, 342);
            tabPageIGC.TabIndex = 1;
            tabPageIGC.Text = "Flight Recorder";
            tabPageIGC.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(230, 20);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(58, 15);
            label1.TabIndex = 14;
            label1.Text = "second(s)";
            // 
            // labelLOGINT
            // 
            labelLOGINT.AutoSize = true;
            labelLOGINT.Location = new System.Drawing.Point(20, 20);
            labelLOGINT.Name = "labelLOGINT";
            labelLOGINT.Size = new System.Drawing.Size(89, 15);
            labelLOGINT.TabIndex = 0;
            labelLOGINT.Text = "Logger Interval:";
            // 
            // numericUpDownLOGINT
            // 
            numericUpDownLOGINT.Location = new System.Drawing.Point(160, 17);
            numericUpDownLOGINT.Maximum = new decimal(new int[] { 5, 0, 0, 0 });
            numericUpDownLOGINT.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            numericUpDownLOGINT.Name = "numericUpDownLOGINT";
            numericUpDownLOGINT.Size = new System.Drawing.Size(60, 23);
            numericUpDownLOGINT.TabIndex = 1;
            numericUpDownLOGINT.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // labelPILOT
            // 
            labelPILOT.AutoSize = true;
            labelPILOT.Location = new System.Drawing.Point(20, 55);
            labelPILOT.Name = "labelPILOT";
            labelPILOT.Size = new System.Drawing.Size(69, 15);
            labelPILOT.TabIndex = 2;
            labelPILOT.Text = "Pilot Name:";
            // 
            // textBoxPILOT
            // 
            textBoxPILOT.Location = new System.Drawing.Point(160, 52);
            textBoxPILOT.Name = "textBoxPILOT";
            textBoxPILOT.Size = new System.Drawing.Size(311, 23);
            textBoxPILOT.TabIndex = 3;
            // 
            // labelCOMPCLASS
            // 
            labelCOMPCLASS.AutoSize = true;
            labelCOMPCLASS.Location = new System.Drawing.Point(20, 90);
            labelCOMPCLASS.Name = "labelCOMPCLASS";
            labelCOMPCLASS.Size = new System.Drawing.Size(107, 15);
            labelCOMPCLASS.TabIndex = 4;
            labelCOMPCLASS.Text = "Competition Class:";
            // 
            // textBoxCOMPCLASS
            // 
            textBoxCOMPCLASS.Location = new System.Drawing.Point(160, 87);
            textBoxCOMPCLASS.Name = "textBoxCOMPCLASS";
            textBoxCOMPCLASS.Size = new System.Drawing.Size(311, 23);
            textBoxCOMPCLASS.TabIndex = 5;
            // 
            // labelCOMPID
            // 
            labelCOMPID.AutoSize = true;
            labelCOMPID.Location = new System.Drawing.Point(20, 125);
            labelCOMPID.Name = "labelCOMPID";
            labelCOMPID.Size = new System.Drawing.Size(91, 15);
            labelCOMPID.TabIndex = 6;
            labelCOMPID.Text = "Competition ID:";
            // 
            // textBoxCOMPID
            // 
            textBoxCOMPID.Location = new System.Drawing.Point(160, 122);
            textBoxCOMPID.Name = "textBoxCOMPID";
            textBoxCOMPID.Size = new System.Drawing.Size(311, 23);
            textBoxCOMPID.TabIndex = 7;
            // 
            // labelGLIDERID
            // 
            labelGLIDERID.AutoSize = true;
            labelGLIDERID.Location = new System.Drawing.Point(20, 160);
            labelGLIDERID.Name = "labelGLIDERID";
            labelGLIDERID.Size = new System.Drawing.Size(115, 15);
            labelGLIDERID.TabIndex = 8;
            labelGLIDERID.Text = "Aircraft Registration:";
            // 
            // textBoxGLIDERID
            // 
            textBoxGLIDERID.Location = new System.Drawing.Point(160, 157);
            textBoxGLIDERID.Name = "textBoxGLIDERID";
            textBoxGLIDERID.Size = new System.Drawing.Size(311, 23);
            textBoxGLIDERID.TabIndex = 9;
            // 
            // labelGLIDERTYPE
            // 
            labelGLIDERTYPE.AutoSize = true;
            labelGLIDERTYPE.Location = new System.Drawing.Point(20, 195);
            labelGLIDERTYPE.Name = "labelGLIDERTYPE";
            labelGLIDERTYPE.Size = new System.Drawing.Size(68, 15);
            labelGLIDERTYPE.TabIndex = 10;
            labelGLIDERTYPE.Text = "Glider Type:";
            // 
            // textBoxGLIDERTYPE
            // 
            textBoxGLIDERTYPE.Location = new System.Drawing.Point(160, 192);
            textBoxGLIDERTYPE.Name = "textBoxGLIDERTYPE";
            textBoxGLIDERTYPE.Size = new System.Drawing.Size(311, 23);
            textBoxGLIDERTYPE.TabIndex = 11;
            // 
            // tabPageBasic
            // 
            tabPageBasic.Controls.Add(labelID);
            tabPageBasic.Controls.Add(textBoxID);
            tabPageBasic.Controls.Add(labelNMEAOUT);
            tabPageBasic.Controls.Add(comboBoxNMEAOUT);
            tabPageBasic.Controls.Add(labelACFT);
            tabPageBasic.Controls.Add(comboBoxACFT);
            tabPageBasic.Controls.Add(labelCFLAGS);
            tabPageBasic.Controls.Add(comboBoxCFLAGS);
            tabPageBasic.Controls.Add(labelPRIV);
            tabPageBasic.Controls.Add(comboBoxPRIV);
            tabPageBasic.Controls.Add(labelNOTRACK);
            tabPageBasic.Controls.Add(comboBoxNOTRACK);
            tabPageBasic.Controls.Add(labelTHRE);
            tabPageBasic.Controls.Add(numericUpDownTHRE);
            tabPageBasic.Controls.Add(labelThreUnit);
            tabPageBasic.Controls.Add(labelBAUD);
            tabPageBasic.Controls.Add(comboBoxBAUD);
            tabPageBasic.Location = new System.Drawing.Point(4, 24);
            tabPageBasic.Name = "tabPageBasic";
            tabPageBasic.Padding = new System.Windows.Forms.Padding(3);
            tabPageBasic.Size = new System.Drawing.Size(569, 342);
            tabPageBasic.TabIndex = 0;
            tabPageBasic.Text = "Basic Settings";
            tabPageBasic.UseVisualStyleBackColor = true;
            // 
            // labelID
            // 
            labelID.AutoSize = true;
            labelID.Location = new System.Drawing.Point(20, 20);
            labelID.Name = "labelID";
            labelID.Size = new System.Drawing.Size(21, 15);
            labelID.TabIndex = 0;
            labelID.Text = "ID:";
            // 
            // textBoxID
            // 
            textBoxID.Location = new System.Drawing.Point(160, 17);
            textBoxID.Name = "textBoxID";
            textBoxID.Size = new System.Drawing.Size(94, 23);
            textBoxID.TabIndex = 1;
            toolTip1.SetToolTip(textBoxID, resources.GetString("textBoxID.ToolTip"));
            // 
            // labelNMEAOUT
            // 
            labelNMEAOUT.AutoSize = true;
            labelNMEAOUT.Location = new System.Drawing.Point(20, 55);
            labelNMEAOUT.Name = "labelNMEAOUT";
            labelNMEAOUT.Size = new System.Drawing.Size(85, 15);
            labelNMEAOUT.TabIndex = 2;
            labelNMEAOUT.Text = "NMEA Output:";
            // 
            // comboBoxNMEAOUT
            // 
            comboBoxNMEAOUT.Location = new System.Drawing.Point(160, 52);
            comboBoxNMEAOUT.Name = "comboBoxNMEAOUT";
            comboBoxNMEAOUT.Size = new System.Drawing.Size(380, 23);
            comboBoxNMEAOUT.TabIndex = 3;
            // 
            // labelACFT
            // 
            labelACFT.AutoSize = true;
            labelACFT.Location = new System.Drawing.Point(20, 90);
            labelACFT.Name = "labelACFT";
            labelACFT.Size = new System.Drawing.Size(76, 15);
            labelACFT.TabIndex = 4;
            labelACFT.Text = "Aircraft Type:";
            // 
            // comboBoxACFT
            // 
            comboBoxACFT.Location = new System.Drawing.Point(160, 87);
            comboBoxACFT.Name = "comboBoxACFT";
            comboBoxACFT.Size = new System.Drawing.Size(380, 23);
            comboBoxACFT.TabIndex = 5;
            // 
            // labelCFLAGS
            // 
            labelCFLAGS.AutoSize = true;
            labelCFLAGS.Location = new System.Drawing.Point(20, 125);
            labelCFLAGS.Name = "labelCFLAGS";
            labelCFLAGS.Size = new System.Drawing.Size(107, 15);
            labelCFLAGS.TabIndex = 6;
            labelCFLAGS.Text = "Competition Flags:";
            // 
            // comboBoxCFLAGS
            // 
            comboBoxCFLAGS.Location = new System.Drawing.Point(160, 122);
            comboBoxCFLAGS.Name = "comboBoxCFLAGS";
            comboBoxCFLAGS.Size = new System.Drawing.Size(380, 23);
            comboBoxCFLAGS.TabIndex = 7;
            // 
            // labelPRIV
            // 
            labelPRIV.AutoSize = true;
            labelPRIV.Location = new System.Drawing.Point(20, 160);
            labelPRIV.Name = "labelPRIV";
            labelPRIV.Size = new System.Drawing.Size(82, 15);
            labelPRIV.TabIndex = 8;
            labelPRIV.Text = "Privacy Mode:";
            // 
            // comboBoxPRIV
            // 
            comboBoxPRIV.Location = new System.Drawing.Point(160, 157);
            comboBoxPRIV.Name = "comboBoxPRIV";
            comboBoxPRIV.Size = new System.Drawing.Size(380, 23);
            comboBoxPRIV.TabIndex = 9;
            // 
            // labelNOTRACK
            // 
            labelNOTRACK.AutoSize = true;
            labelNOTRACK.Location = new System.Drawing.Point(20, 195);
            labelNOTRACK.Name = "labelNOTRACK";
            labelNOTRACK.Size = new System.Drawing.Size(56, 15);
            labelNOTRACK.TabIndex = 10;
            labelNOTRACK.Text = "No Track:";
            // 
            // comboBoxNOTRACK
            // 
            comboBoxNOTRACK.Location = new System.Drawing.Point(160, 192);
            comboBoxNOTRACK.Name = "comboBoxNOTRACK";
            comboBoxNOTRACK.Size = new System.Drawing.Size(380, 23);
            comboBoxNOTRACK.TabIndex = 11;
            // 
            // labelTHRE
            // 
            labelTHRE.AutoSize = true;
            labelTHRE.Location = new System.Drawing.Point(20, 230);
            labelTHRE.Name = "labelTHRE";
            labelTHRE.Size = new System.Drawing.Size(97, 15);
            labelTHRE.TabIndex = 12;
            labelTHRE.Text = "Alarm Threshold:";
            // 
            // numericUpDownTHRE
            // 
            numericUpDownTHRE.Location = new System.Drawing.Point(160, 227);
            numericUpDownTHRE.Maximum = new decimal(new int[] { 20, 0, 0, 0 });
            numericUpDownTHRE.Name = "numericUpDownTHRE";
            numericUpDownTHRE.Size = new System.Drawing.Size(60, 23);
            numericUpDownTHRE.TabIndex = 13;
            toolTip1.SetToolTip(numericUpDownTHRE, resources.GetString("numericUpDownTHRE.ToolTip"));
            // 
            // labelThreUnit
            // 
            labelThreUnit.AutoSize = true;
            labelThreUnit.Location = new System.Drawing.Point(230, 230);
            labelThreUnit.Name = "labelThreUnit";
            labelThreUnit.Size = new System.Drawing.Size(28, 15);
            labelThreUnit.TabIndex = 14;
            labelThreUnit.Text = "m/s";
            // 
            // labelBAUD
            // 
            labelBAUD.AutoSize = true;
            labelBAUD.Location = new System.Drawing.Point(20, 265);
            labelBAUD.Name = "labelBAUD";
            labelBAUD.Size = new System.Drawing.Size(63, 15);
            labelBAUD.TabIndex = 15;
            labelBAUD.Text = "Baud Rate:";
            // 
            // comboBoxBAUD
            // 
            comboBoxBAUD.Location = new System.Drawing.Point(160, 262);
            comboBoxBAUD.Name = "comboBoxBAUD";
            comboBoxBAUD.Size = new System.Drawing.Size(380, 23);
            comboBoxBAUD.TabIndex = 16;
            // 
            // buttonLoad
            // 
            buttonLoad.Location = new System.Drawing.Point(30, 400);
            buttonLoad.Name = "buttonLoad";
            buttonLoad.Size = new System.Drawing.Size(100, 30);
            buttonLoad.TabIndex = 1;
            buttonLoad.Text = "Load";
            buttonLoad.UseVisualStyleBackColor = true;
            buttonLoad.Click += buttonLoad_Click;
            // 
            // buttonSave
            // 
            buttonSave.Location = new System.Drawing.Point(172, 400);
            buttonSave.Name = "buttonSave";
            buttonSave.Size = new System.Drawing.Size(100, 30);
            buttonSave.TabIndex = 2;
            buttonSave.Text = "Save";
            buttonSave.UseVisualStyleBackColor = true;
            buttonSave.Click += buttonSave_Click;
            // 
            // buttonPreview
            // 
            buttonPreview.Location = new System.Drawing.Point(314, 400);
            buttonPreview.Name = "buttonPreview";
            buttonPreview.Size = new System.Drawing.Size(100, 30);
            buttonPreview.TabIndex = 3;
            buttonPreview.Text = "Preview";
            buttonPreview.UseVisualStyleBackColor = true;
            buttonPreview.Click += buttonPreview_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.Location = new System.Drawing.Point(456, 400);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new System.Drawing.Size(100, 30);
            buttonCancel.TabIndex = 4;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            buttonCancel.Click += buttonCancel_Click;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            pictureBox1.Image = Properties.Resources.text_icon;
            pictureBox1.InitialImage = (System.Drawing.Image)resources.GetObject("pictureBox1.InitialImage");
            pictureBox1.Location = new System.Drawing.Point(492, 272);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(64, 64);
            pictureBox1.TabIndex = 15;
            pictureBox1.TabStop = false;
            // 
            // FlarmConfigEditor
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(601, 441);
            Controls.Add(tabControlMain);
            Controls.Add(buttonLoad);
            Controls.Add(buttonSave);
            Controls.Add(buttonPreview);
            Controls.Add(buttonCancel);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MinimumSize = new System.Drawing.Size(400, 350);
            Name = "FlarmConfigEditor";
            Text = "FLARM Config Editor";
            tabControlMain.ResumeLayout(false);
            tabPageIGC.ResumeLayout(false);
            tabPageIGC.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownLOGINT).EndInit();
            tabPageBasic.ResumeLayout(false);
            tabPageBasic.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)numericUpDownTHRE).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }
        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
