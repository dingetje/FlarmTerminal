using System.Drawing;

namespace FlarmTerminal.GUI
{
    partial class SerialPortConfig
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
            panel1 = new System.Windows.Forms.Panel();
            label1 = new System.Windows.Forms.Label();
            pictureBox1 = new System.Windows.Forms.PictureBox();
            lvComPorts = new System.Windows.Forms.ListView();
            columnIcon = new System.Windows.Forms.ColumnHeader();
            columnName = new System.Windows.Forms.ColumnHeader();
            columnLongName = new System.Windows.Forms.ColumnHeader();
            buttonRefresh = new System.Windows.Forms.Button();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            label4 = new System.Windows.Forms.Label();
            label5 = new System.Windows.Forms.Label();
            label6 = new System.Windows.Forms.Label();
            button1 = new System.Windows.Forms.Button();
            button2 = new System.Windows.Forms.Button();
            button3 = new System.Windows.Forms.Button();
            comboBoxBaudRate = new System.Windows.Forms.ComboBox();
            comboBoxDataBits = new System.Windows.Forms.ComboBox();
            comboBoxParity = new System.Windows.Forms.ComboBox();
            comboBoxStopBits = new System.Windows.Forms.ComboBox();
            comboBoxFlowControl = new System.Windows.Forms.ComboBox();
            groupBox1 = new System.Windows.Forms.GroupBox();
            panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            groupBox1.SuspendLayout();
            SuspendLayout();
            // 
            // panel1
            // 
            panel1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            panel1.Controls.Add(label1);
            panel1.Location = new Point(1, 1);
            panel1.Name = "panel1";
            panel1.Size = new Size(436, 84);
            panel1.TabIndex = 0;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(21, 17);
            label1.Name = "label1";
            label1.Size = new Size(317, 15);
            label1.TabIndex = 0;
            label1.Text = "Select the COM port from the list and configure its settings";
            // 
            // pictureBox1
            // 
            pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right;
            pictureBox1.Image = Properties.Resources.serial_port_icon;
            pictureBox1.Location = new Point(443, 1);
            pictureBox1.Margin = new System.Windows.Forms.Padding(5);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(88, 84);
            pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            pictureBox1.TabIndex = 1;
            pictureBox1.TabStop = false;
            // 
            // lvComPorts
            // 
            lvComPorts.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            lvComPorts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] { columnIcon, columnName, columnLongName });
            lvComPorts.FullRowSelect = true;
            lvComPorts.GridLines = true;
            lvComPorts.Location = new Point(6, 91);
            lvComPorts.MultiSelect = false;
            lvComPorts.Name = "lvComPorts";
            lvComPorts.Size = new Size(522, 133);
            lvComPorts.TabIndex = 2;
            lvComPorts.UseCompatibleStateImageBehavior = false;
            lvComPorts.View = System.Windows.Forms.View.Details;
            lvComPorts.SelectedIndexChanged += lvComPorts_SelectedIndexChanged;
            // 
            // columnIcon
            // 
            columnIcon.Text = "";
            // 
            // columnName
            // 
            columnName.Text = "Name";
            // 
            // columnLongName
            // 
            columnLongName.Text = "Description";
            columnLongName.Width = 300;
            // 
            // buttonRefresh
            // 
            buttonRefresh.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            buttonRefresh.Location = new Point(445, 230);
            buttonRefresh.Name = "buttonRefresh";
            buttonRefresh.Size = new Size(75, 23);
            buttonRefresh.TabIndex = 3;
            buttonRefresh.Text = "Refresh";
            buttonRefresh.UseVisualStyleBackColor = true;
            buttonRefresh.Click += buttonRefresh_Click;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(17, 37);
            label2.Name = "label2";
            label2.Size = new Size(60, 15);
            label2.TabIndex = 4;
            label2.Text = "Baud Rate";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(17, 71);
            label3.Name = "label3";
            label3.Size = new Size(53, 15);
            label3.TabIndex = 5;
            label3.Text = "Data Bits";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(17, 105);
            label4.Name = "label4";
            label4.Size = new Size(37, 15);
            label4.TabIndex = 6;
            label4.Text = "Parity";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(17, 139);
            label5.Name = "label5";
            label5.Size = new Size(53, 15);
            label5.TabIndex = 7;
            label5.Text = "Stop Bits";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(17, 173);
            label6.Name = "label6";
            label6.Size = new Size(75, 15);
            label6.TabIndex = 8;
            label6.Text = "Flow Control";
            // 
            // button1
            // 
            button1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            button1.Location = new Point(445, 374);
            button1.Name = "button1";
            button1.Size = new Size(75, 23);
            button1.TabIndex = 9;
            button1.Text = "Defaults";
            button1.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            button2.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            button2.DialogResult = System.Windows.Forms.DialogResult.OK;
            button2.Location = new Point(343, 419);
            button2.Name = "button2";
            button2.Size = new Size(75, 23);
            button2.TabIndex = 10;
            button2.Text = "OK";
            button2.UseVisualStyleBackColor = true;
            // 
            // button3
            // 
            button3.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right;
            button3.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            button3.Location = new Point(445, 419);
            button3.Name = "button3";
            button3.Size = new Size(75, 23);
            button3.TabIndex = 11;
            button3.Text = "Cancel";
            button3.UseVisualStyleBackColor = true;
            // 
            // comboBoxBaudRate
            // 
            comboBoxBaudRate.FormattingEnabled = true;
            comboBoxBaudRate.Items.AddRange(new object[] { "1200", "2400", "4800", "9600", "19200", "38400", "57600", "115200" });
            comboBoxBaudRate.Location = new Point(126, 29);
            comboBoxBaudRate.Name = "comboBoxBaudRate";
            comboBoxBaudRate.Size = new Size(116, 23);
            comboBoxBaudRate.TabIndex = 12;
            comboBoxBaudRate.SelectedIndexChanged += comboBoxBaudRate_SelectedIndexChanged;
            // 
            // comboBoxDataBits
            // 
            comboBoxDataBits.FormattingEnabled = true;
            comboBoxDataBits.Items.AddRange(new object[] { "8", "7" });
            comboBoxDataBits.Location = new Point(126, 63);
            comboBoxDataBits.Name = "comboBoxDataBits";
            comboBoxDataBits.Size = new Size(68, 23);
            comboBoxDataBits.TabIndex = 13;
            comboBoxDataBits.SelectedIndexChanged += comboBoxDataBits_SelectedIndexChanged;
            // 
            // comboBoxParity
            // 
            comboBoxParity.FormattingEnabled = true;
            comboBoxParity.Items.AddRange(new object[] { "None", "Even", "Odd", "Mark", "Space" });
            comboBoxParity.Location = new Point(126, 97);
            comboBoxParity.Name = "comboBoxParity";
            comboBoxParity.Size = new Size(121, 23);
            comboBoxParity.TabIndex = 14;
            comboBoxParity.SelectedIndexChanged += comboBoxParity_SelectedIndexChanged;
            // 
            // comboBoxStopBits
            // 
            comboBoxStopBits.FormattingEnabled = true;
            comboBoxStopBits.Items.AddRange(new object[] { "One", "Two" });
            comboBoxStopBits.Location = new Point(126, 131);
            comboBoxStopBits.Name = "comboBoxStopBits";
            comboBoxStopBits.Size = new Size(121, 23);
            comboBoxStopBits.TabIndex = 15;
            comboBoxStopBits.SelectedIndexChanged += comboBoxStopBits_SelectedIndexChanged;
            // 
            // comboBoxFlowControl
            // 
            comboBoxFlowControl.FormattingEnabled = true;
            comboBoxFlowControl.Items.AddRange(new object[] { "None", "RTS", "RTS/Xon/Xoff", "Xon/Xoff", "" });
            comboBoxFlowControl.Location = new Point(126, 165);
            comboBoxFlowControl.Name = "comboBoxFlowControl";
            comboBoxFlowControl.Size = new Size(121, 23);
            comboBoxFlowControl.TabIndex = 16;
            comboBoxFlowControl.SelectedIndexChanged += comboBoxFlowControl_SelectedIndexChanged;
            // 
            // groupBox1
            // 
            groupBox1.Anchor = System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left;
            groupBox1.Controls.Add(label2);
            groupBox1.Controls.Add(comboBoxFlowControl);
            groupBox1.Controls.Add(label3);
            groupBox1.Controls.Add(comboBoxStopBits);
            groupBox1.Controls.Add(label4);
            groupBox1.Controls.Add(comboBoxParity);
            groupBox1.Controls.Add(label5);
            groupBox1.Controls.Add(comboBoxDataBits);
            groupBox1.Controls.Add(label6);
            groupBox1.Controls.Add(comboBoxBaudRate);
            groupBox1.Location = new Point(15, 235);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(264, 207);
            groupBox1.TabIndex = 17;
            groupBox1.TabStop = false;
            groupBox1.Text = "Properties";
            // 
            // SerialPortConfig
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new Size(534, 449);
            Controls.Add(groupBox1);
            Controls.Add(button3);
            Controls.Add(button2);
            Controls.Add(button1);
            Controls.Add(buttonRefresh);
            Controls.Add(lvComPorts);
            Controls.Add(pictureBox1);
            Controls.Add(panel1);
            MinimizeBox = false;
            MinimumSize = new Size(550, 450);
            Name = "SerialPortConfig";
            Padding = new System.Windows.Forms.Padding(3);
            Text = "Serial Port Configuration";
            Load += SerialPortConfig_Load;
            Shown += SerialPortConfig_Shown;
            ResizeEnd += SerialPortConfig_ResizeEnd;
            panel1.ResumeLayout(false);
            panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            groupBox1.ResumeLayout(false);
            groupBox1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ListView lvComPorts;
        private System.Windows.Forms.ColumnHeader columnIcon;
        private System.Windows.Forms.ColumnHeader columnName;
        private System.Windows.Forms.ColumnHeader columnLongName;
        private System.Windows.Forms.Button buttonRefresh;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ComboBox comboBoxBaudRate;
        private System.Windows.Forms.ComboBox comboBoxDataBits;
        private System.Windows.Forms.ComboBox comboBoxParity;
        private System.Windows.Forms.ComboBox comboBoxStopBits;
        private System.Windows.Forms.ComboBox comboBoxFlowControl;
        private System.Windows.Forms.GroupBox groupBox1;
    }
}