namespace FlarmTerminal.GUI
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
            formsPlot1 = new ScottPlot.WinForms.FormsPlot();
            buttonOK = new System.Windows.Forms.Button();
            SuspendLayout();
            // 
            // formsPlot1
            // 
            formsPlot1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            formsPlot1.DisplayScale = 1F;
            formsPlot1.Location = new System.Drawing.Point(16, 17);
            formsPlot1.Name = "formsPlot1";
            formsPlot1.Size = new System.Drawing.Size(490, 372);
            formsPlot1.TabIndex = 0;
            // 
            // buttonOK
            // 
            buttonOK.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            buttonOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            buttonOK.Location = new System.Drawing.Point(444, 403);
            buttonOK.Name = "buttonOK";
            buttonOK.Size = new System.Drawing.Size(56, 31);
            buttonOK.TabIndex = 1;
            buttonOK.Text = "OK";
            buttonOK.UseVisualStyleBackColor = true;
            buttonOK.Click += buttonOK_Click;
            // 
            // CARPRadarPlot
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(512, 446);
            Controls.Add(buttonOK);
            Controls.Add(formsPlot1);
            Name = "CARPRadarPlot";
            Text = "CARP Range Radar Plot";
            ResumeLayout(false);
        }

        #endregion

        private ScottPlot.WinForms.FormsPlot formsPlot1;
        private System.Windows.Forms.Button buttonOK;
    }
}