namespace FlarmTerminal.GUI
{
    partial class AboutBox
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutBox));
            tableLayoutPanel = new System.Windows.Forms.TableLayoutPanel();
            labelProductName = new System.Windows.Forms.Label();
            labelVersion = new System.Windows.Forms.Label();
            labelCopyright = new System.Windows.Forms.Label();
            labelCompanyName = new System.Windows.Forms.Label();
            textBoxDescription = new System.Windows.Forms.TextBox();
            okButton = new System.Windows.Forms.Button();
            logoPictureBox = new System.Windows.Forms.PictureBox();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            tableLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)logoPictureBox).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // tableLayoutPanel
            // 
            tableLayoutPanel.BackColor = System.Drawing.Color.White;
            tableLayoutPanel.ColumnCount = 2;
            tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 155F));
            tableLayoutPanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel.Controls.Add(labelProductName, 1, 0);
            tableLayoutPanel.Controls.Add(labelVersion, 1, 1);
            tableLayoutPanel.Controls.Add(labelCopyright, 1, 2);
            tableLayoutPanel.Controls.Add(labelCompanyName, 1, 3);
            tableLayoutPanel.Controls.Add(textBoxDescription, 1, 4);
            tableLayoutPanel.Controls.Add(okButton, 1, 5);
            tableLayoutPanel.Controls.Add(logoPictureBox, 0, 0);
            tableLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            tableLayoutPanel.Location = new System.Drawing.Point(10, 10);
            tableLayoutPanel.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            tableLayoutPanel.Name = "tableLayoutPanel";
            tableLayoutPanel.RowCount = 6;
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            tableLayoutPanel.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 35F));
            tableLayoutPanel.Size = new System.Drawing.Size(714, 464);
            tableLayoutPanel.TabIndex = 0;
            // 
            // labelProductName
            // 
            labelProductName.Location = new System.Drawing.Point(162, 0);
            labelProductName.Margin = new System.Windows.Forms.Padding(7, 0, 4, 0);
            labelProductName.MaximumSize = new System.Drawing.Size(0, 20);
            labelProductName.Name = "labelProductName";
            labelProductName.Size = new System.Drawing.Size(548, 20);
            labelProductName.TabIndex = 19;
            labelProductName.Text = "Product Name";
            labelProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelVersion
            // 
            labelVersion.BackColor = System.Drawing.Color.White;
            labelVersion.Dock = System.Windows.Forms.DockStyle.Fill;
            labelVersion.Location = new System.Drawing.Point(162, 23);
            labelVersion.Margin = new System.Windows.Forms.Padding(7, 0, 4, 0);
            labelVersion.MaximumSize = new System.Drawing.Size(0, 20);
            labelVersion.Name = "labelVersion";
            labelVersion.Size = new System.Drawing.Size(548, 20);
            labelVersion.TabIndex = 0;
            labelVersion.Text = "Version";
            labelVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelCopyright
            // 
            labelCopyright.Dock = System.Windows.Forms.DockStyle.Fill;
            labelCopyright.Location = new System.Drawing.Point(162, 46);
            labelCopyright.Margin = new System.Windows.Forms.Padding(7, 0, 4, 0);
            labelCopyright.MaximumSize = new System.Drawing.Size(0, 20);
            labelCopyright.Name = "labelCopyright";
            labelCopyright.Size = new System.Drawing.Size(548, 20);
            labelCopyright.TabIndex = 21;
            labelCopyright.Text = "Copyright";
            labelCopyright.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // labelCompanyName
            // 
            labelCompanyName.Dock = System.Windows.Forms.DockStyle.Fill;
            labelCompanyName.Location = new System.Drawing.Point(162, 69);
            labelCompanyName.Margin = new System.Windows.Forms.Padding(7, 0, 4, 0);
            labelCompanyName.MaximumSize = new System.Drawing.Size(0, 20);
            labelCompanyName.Name = "labelCompanyName";
            labelCompanyName.Size = new System.Drawing.Size(548, 20);
            labelCompanyName.TabIndex = 22;
            labelCompanyName.Text = "Company Name";
            labelCompanyName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBoxDescription
            // 
            textBoxDescription.BackColor = System.Drawing.SystemColors.Info;
            textBoxDescription.Dock = System.Windows.Forms.DockStyle.Fill;
            textBoxDescription.Location = new System.Drawing.Point(162, 95);
            textBoxDescription.Margin = new System.Windows.Forms.Padding(7, 3, 4, 3);
            textBoxDescription.Multiline = true;
            textBoxDescription.Name = "textBoxDescription";
            textBoxDescription.ReadOnly = true;
            textBoxDescription.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            textBoxDescription.Size = new System.Drawing.Size(548, 331);
            textBoxDescription.TabIndex = 23;
            textBoxDescription.TabStop = false;
            textBoxDescription.Text = "Description";
            // 
            // okButton
            // 
            okButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            okButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            okButton.Location = new System.Drawing.Point(622, 434);
            okButton.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            okButton.Name = "okButton";
            okButton.Size = new System.Drawing.Size(88, 27);
            okButton.TabIndex = 24;
            okButton.Text = "&OK";
            // 
            // logoPictureBox
            // 
            logoPictureBox.Dock = System.Windows.Forms.DockStyle.Left;
            logoPictureBox.Image = Properties.Resources._20607150556_c01d092437_b;
            logoPictureBox.InitialImage = Properties.Resources._20607150556_c01d092437_b;
            logoPictureBox.Location = new System.Drawing.Point(4, 3);
            logoPictureBox.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            logoPictureBox.Name = "logoPictureBox";
            tableLayoutPanel.SetRowSpan(logoPictureBox, 6);
            logoPictureBox.Size = new System.Drawing.Size(147, 458);
            logoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            logoPictureBox.TabIndex = 12;
            logoPictureBox.TabStop = false;
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            pictureBox1.Location = new System.Drawing.Point(616, 15);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new System.Drawing.Size(100, 63);
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            pictureBox1.Paint += pictureBox1_Paint;
            // 
            // AboutBox
            // 
            AcceptButton = okButton;
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            BackColor = System.Drawing.Color.White;
            ClientSize = new System.Drawing.Size(734, 484);
            Controls.Add(pictureBox1);
            Controls.Add(tableLayoutPanel);
            DoubleBuffered = true;
            Icon = (System.Drawing.Icon)resources.GetObject("$this.Icon");
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MaximizeBox = false;
            MinimizeBox = false;
            MinimumSize = new System.Drawing.Size(750, 500);
            Name = "AboutBox";
            Padding = new System.Windows.Forms.Padding(10);
            ShowInTaskbar = false;
            SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            Text = "About";
            tableLayoutPanel.ResumeLayout(false);
            tableLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)logoPictureBox).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel;
        private System.Windows.Forms.Label labelProductName;
        private System.Windows.Forms.Label labelVersion;
        private System.Windows.Forms.Label labelCopyright;
        private System.Windows.Forms.Label labelCompanyName;
        private System.Windows.Forms.TextBox textBoxDescription;
        private System.Windows.Forms.Button okButton;
        private System.Windows.Forms.PictureBox logoPictureBox;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}
