﻿namespace FlarmTerminal.GUI
{
    partial class CARPRadarPlot
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CARPRadarPlot));
            formsPlot1 = new ScottPlot.WinForms.FormsPlot();
            buttonOK = new System.Windows.Forms.Button();
            groupBoxPlot = new System.Windows.Forms.GroupBox();
            groupBoxRemarks = new System.Windows.Forms.GroupBox();
            groupBoxDevice = new System.Windows.Forms.GroupBox();
            buttonPrint = new System.Windows.Forms.Button();
            groupBoxPlot.SuspendLayout();
            SuspendLayout();
            // 
            // formsPlot1
            // 
            formsPlot1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            formsPlot1.DisplayScale = 1F;
            formsPlot1.Location = new System.Drawing.Point(18, 22);
            formsPlot1.Name = "formsPlot1";
            formsPlot1.Size = new System.Drawing.Size(512, 383);
            formsPlot1.TabIndex = 0;
            // 
            // buttonOK
            // 
            buttonOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            buttonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            buttonOK.Location = new System.Drawing.Point(505, 868);
            buttonOK.Name = "buttonOK";
            buttonOK.Size = new System.Drawing.Size(56, 31);
            buttonOK.TabIndex = 1;
            buttonOK.Text = "OK";
            buttonOK.UseVisualStyleBackColor = true;
            buttonOK.Click += buttonOK_Click;
            // 
            // groupBoxPlot
            // 
            groupBoxPlot.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            groupBoxPlot.Controls.Add(formsPlot1);
            groupBoxPlot.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            groupBoxPlot.Location = new System.Drawing.Point(12, 12);
            groupBoxPlot.MinimumSize = new System.Drawing.Size(530, 420);
            groupBoxPlot.Name = "groupBoxPlot";
            groupBoxPlot.Size = new System.Drawing.Size(549, 420);
            groupBoxPlot.TabIndex = 2;
            groupBoxPlot.TabStop = false;
            groupBoxPlot.Text = "Range Plot";
            // 
            // groupBoxRemarks
            // 
            groupBoxRemarks.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            groupBoxRemarks.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            groupBoxRemarks.Location = new System.Drawing.Point(11, 438);
            groupBoxRemarks.MaximumSize = new System.Drawing.Size(0, 150);
            groupBoxRemarks.MinimumSize = new System.Drawing.Size(530, 150);
            groupBoxRemarks.Name = "groupBoxRemarks";
            groupBoxRemarks.Size = new System.Drawing.Size(549, 150);
            groupBoxRemarks.TabIndex = 3;
            groupBoxRemarks.TabStop = false;
            groupBoxRemarks.Text = "Remarks";
            // 
            // groupBoxDevice
            // 
            groupBoxDevice.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            groupBoxDevice.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            groupBoxDevice.Location = new System.Drawing.Point(11, 598);
            groupBoxDevice.MaximumSize = new System.Drawing.Size(0, 264);
            groupBoxDevice.MinimumSize = new System.Drawing.Size(530, 264);
            groupBoxDevice.Name = "groupBoxDevice";
            groupBoxDevice.Size = new System.Drawing.Size(549, 264);
            groupBoxDevice.TabIndex = 4;
            groupBoxDevice.TabStop = false;
            groupBoxDevice.Text = "Device Info";
            // 
            // buttonPrint
            // 
            buttonPrint.Location = new System.Drawing.Point(438, 868);
            buttonPrint.Name = "buttonPrint";
            buttonPrint.Size = new System.Drawing.Size(61, 31);
            buttonPrint.TabIndex = 5;
            buttonPrint.Text = "Print...";
            buttonPrint.UseVisualStyleBackColor = true;
            buttonPrint.Click += buttonPrint_Click;
            // 
            // CARPRadarPlot
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(573, 911);
            Controls.Add(buttonPrint);
            Controls.Add(groupBoxDevice);
            Controls.Add(groupBoxRemarks);
            Controls.Add(groupBoxPlot);
            Controls.Add(buttonOK);
            MinimumSize = new System.Drawing.Size(570, 950);
            Name = "CARPRadarPlot";
            Text = "CARP Range Radar Plot";
            groupBoxPlot.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private ScottPlot.WinForms.FormsPlot formsPlot1;
        private System.Windows.Forms.Button buttonOK;
        private System.Windows.Forms.GroupBox groupBoxPlot;
        private System.Windows.Forms.GroupBox groupBoxRemarks;
        private System.Windows.Forms.GroupBox groupBoxDevice;
        private System.Windows.Forms.Button buttonPrint;
    }
}