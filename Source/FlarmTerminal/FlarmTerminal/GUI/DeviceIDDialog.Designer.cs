namespace FlarmTerminal.GUI
{
    partial class DeviceIDDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DeviceIDDialog));
            textBoxICAO = new System.Windows.Forms.TextBox();
            groupBox1 = new System.Windows.Forms.GroupBox();
            radioButtonICAO = new System.Windows.Forms.RadioButton();
            radioButtonSerial = new System.Windows.Forms.RadioButton();
            buttonOk = new System.Windows.Forms.Button();
            buttonCancel = new System.Windows.Forms.Button();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // textBoxICAO
            // 
            textBoxICAO.Location = new System.Drawing.Point(37, 81);
            textBoxICAO.Name = "textBoxICAO";
            textBoxICAO.Size = new System.Drawing.Size(110, 23);
            textBoxICAO.TabIndex = 1;
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(radioButtonICAO);
            groupBox1.Controls.Add(radioButtonSerial);
            groupBox1.Controls.Add(textBoxICAO);
            groupBox1.Location = new System.Drawing.Point(12, 12);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new System.Drawing.Size(211, 125);
            groupBox1.TabIndex = 2;
            groupBox1.TabStop = false;
            groupBox1.Text = "Device ID";
            // 
            // radioButtonICAO
            // 
            radioButtonICAO.AutoSize = true;
            radioButtonICAO.Location = new System.Drawing.Point(21, 56);
            radioButtonICAO.Name = "radioButtonICAO";
            radioButtonICAO.Size = new System.Drawing.Size(126, 19);
            radioButtonICAO.TabIndex = 3;
            radioButtonICAO.TabStop = true;
            radioButtonICAO.Text = "ICAO (hex address)";
            radioButtonICAO.UseVisualStyleBackColor = true;
            // 
            // radioButtonSerial
            // 
            radioButtonSerial.AutoSize = true;
            radioButtonSerial.Location = new System.Drawing.Point(22, 26);
            radioButtonSerial.Name = "radioButtonSerial";
            radioButtonSerial.Size = new System.Drawing.Size(143, 19);
            radioButtonSerial.TabIndex = 2;
            radioButtonSerial.TabStop = true;
            radioButtonSerial.Text = "Factory ID (automatic)";
            radioButtonSerial.UseVisualStyleBackColor = true;
            // 
            // buttonOk
            // 
            buttonOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            buttonOk.Location = new System.Drawing.Point(96, 146);
            buttonOk.Name = "buttonOk";
            buttonOk.Size = new System.Drawing.Size(75, 23);
            buttonOk.TabIndex = 3;
            buttonOk.Text = "OK";
            buttonOk.UseVisualStyleBackColor = true;
            buttonOk.Click += buttonOk_Click;
            // 
            // buttonCancel
            // 
            buttonCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            buttonCancel.Location = new System.Drawing.Point(178, 146);
            buttonCancel.Name = "buttonCancel";
            buttonCancel.Size = new System.Drawing.Size(75, 23);
            buttonCancel.TabIndex = 4;
            buttonCancel.Text = "Cancel";
            buttonCancel.UseVisualStyleBackColor = true;
            // 
            // DeviceIDDialog
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(265, 181);
            Controls.Add(buttonCancel);
            Controls.Add(buttonOk);
            Controls.Add(groupBox1);
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            MinimumSize = new System.Drawing.Size(280, 220);
            Name = "DeviceIDDialog";
            Text = "Set Device ID";
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TextBox textBoxICAO;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButtonICAO;
        private System.Windows.Forms.RadioButton radioButtonSerial;
        private System.Windows.Forms.Button buttonOk;
        private System.Windows.Forms.Button buttonCancel;
    }
}