namespace FlarmTerminal
{
    partial class MainForm
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
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            textBoxTerminal = new System.Windows.Forms.RichTextBox();
            richTextBoxProperties = new System.Windows.Forms.RichTextBox();
            statusStrip = new System.Windows.Forms.StatusStrip();
            toolStripDropDownConnectButton = new System.Windows.Forms.ToolStripDropDownButton();
            disconnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            toolStripStatusLabelPort = new System.Windows.Forms.ToolStripStatusLabel();
            toolStripStatusComPortProperties = new System.Windows.Forms.ToolStripStatusLabel();
            toolStripStatusLabelGPSUTC = new System.Windows.Forms.ToolStripStatusLabel();
            toolStripStatusLabelPosition = new System.Windows.Forms.ToolStripStatusLabel();
            toolStripStatusLabelClearMemory = new System.Windows.Forms.ToolStripStatusLabel();
            toolStripProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            menuStrip1 = new System.Windows.Forms.MenuStrip();
            fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            readFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            printToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            printPropertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            printRawSerialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            COMPortToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            autoConnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            fLARMDisplayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            commandToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            requestVersionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            requestIDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            requestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            requestSelftestResultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            readIDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            readPropertiesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            simulateScenarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            scenario1CollissionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            requestRunningScenarioToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            dANGERToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            resetToFactorySettingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            clearMemoryToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            clearAllFlightLogsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            setDeviceIDToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            splitContainer1 = new System.Windows.Forms.SplitContainer();
            label1Terminal = new System.Windows.Forms.Label();
            labelProperties = new System.Windows.Forms.Label();
            toolStrip1 = new System.Windows.Forms.ToolStrip();
            toolStripButtonPlay = new System.Windows.Forms.ToolStripButton();
            toolStripButtonPause = new System.Windows.Forms.ToolStripButton();
            toolStripButtonStop = new System.Windows.Forms.ToolStripButton();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            toolStripButtonRecord = new System.Windows.Forms.ToolStripButton();
            recordTtimer = new System.Windows.Forms.Timer(components);
            statusStrip.SuspendLayout();
            menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            toolStrip1.SuspendLayout();
            SuspendLayout();
            // 
            // textBoxTerminal
            // 
            textBoxTerminal.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            textBoxTerminal.BackColor = System.Drawing.Color.LightYellow;
            textBoxTerminal.Font = new System.Drawing.Font("Consolas", 9.75F);
            textBoxTerminal.Location = new System.Drawing.Point(0, 24);
            textBoxTerminal.MinimumSize = new System.Drawing.Size(400, 300);
            textBoxTerminal.Name = "textBoxTerminal";
            textBoxTerminal.ReadOnly = true;
            textBoxTerminal.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            textBoxTerminal.ShowSelectionMargin = true;
            textBoxTerminal.Size = new System.Drawing.Size(590, 587);
            textBoxTerminal.TabIndex = 2;
            textBoxTerminal.Text = "";
            textBoxTerminal.MouseDown += textBoxTerminal_MouseDown;
            // 
            // richTextBoxProperties
            // 
            richTextBoxProperties.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            richTextBoxProperties.BackColor = System.Drawing.Color.LightSteelBlue;
            richTextBoxProperties.Font = new System.Drawing.Font("Courier New", 9.75F);
            richTextBoxProperties.Location = new System.Drawing.Point(2, 24);
            richTextBoxProperties.Name = "richTextBoxProperties";
            richTextBoxProperties.ReadOnly = true;
            richTextBoxProperties.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.ForcedVertical;
            richTextBoxProperties.Size = new System.Drawing.Size(536, 584);
            richTextBoxProperties.TabIndex = 0;
            richTextBoxProperties.Text = "";
            richTextBoxProperties.MouseDown += richTextBoxProperties_MouseDown;
            // 
            // statusStrip
            // 
            statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripDropDownConnectButton, toolStripStatusLabelPort, toolStripStatusComPortProperties, toolStripStatusLabelGPSUTC, toolStripStatusLabelPosition, toolStripStatusLabelClearMemory, toolStripProgressBar });
            statusStrip.Location = new System.Drawing.Point(2, 679);
            statusStrip.Name = "statusStrip";
            statusStrip.Size = new System.Drawing.Size(1132, 22);
            statusStrip.TabIndex = 0;
            statusStrip.Text = "statusStrip2";
            statusStrip.ItemClicked += statusStrip_ItemClicked;
            // 
            // toolStripDropDownConnectButton
            // 
            toolStripDropDownConnectButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripDropDownConnectButton.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { disconnectToolStripMenuItem, connectToolStripMenuItem });
            toolStripDropDownConnectButton.Image = (System.Drawing.Image)resources.GetObject("toolStripDropDownConnectButton.Image");
            toolStripDropDownConnectButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripDropDownConnectButton.Name = "toolStripDropDownConnectButton";
            toolStripDropDownConnectButton.Size = new System.Drawing.Size(29, 20);
            toolStripDropDownConnectButton.Text = "toolStripDropDownButton1";
            // 
            // disconnectToolStripMenuItem
            // 
            disconnectToolStripMenuItem.Name = "disconnectToolStripMenuItem";
            disconnectToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            disconnectToolStripMenuItem.Text = "&Disconnect";
            disconnectToolStripMenuItem.Click += disconnectToolStripMenuItem_Click;
            // 
            // connectToolStripMenuItem
            // 
            connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            connectToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            connectToolStripMenuItem.Text = "&Connect";
            connectToolStripMenuItem.Click += connectToolStripMenuItem_Click;
            // 
            // toolStripStatusLabelPort
            // 
            toolStripStatusLabelPort.BackColor = System.Drawing.SystemColors.Control;
            toolStripStatusLabelPort.Name = "toolStripStatusLabelPort";
            toolStripStatusLabelPort.Size = new System.Drawing.Size(86, 17);
            toolStripStatusLabelPort.Text = "Not connected";
            toolStripStatusLabelPort.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripStatusComPortProperties
            // 
            toolStripStatusComPortProperties.Name = "toolStripStatusComPortProperties";
            toolStripStatusComPortProperties.Size = new System.Drawing.Size(61, 17);
            toolStripStatusComPortProperties.Text = "19200,8N1";
            // 
            // toolStripStatusLabelGPSUTC
            // 
            toolStripStatusLabelGPSUTC.Name = "toolStripStatusLabelGPSUTC";
            toolStripStatusLabelGPSUTC.Size = new System.Drawing.Size(34, 17);
            toolStripStatusLabelGPSUTC.Text = "UTC: ";
            toolStripStatusLabelGPSUTC.ToolTipText = "UTC Time";
            // 
            // toolStripStatusLabelPosition
            // 
            toolStripStatusLabelPosition.Name = "toolStripStatusLabelPosition";
            toolStripStatusLabelPosition.Size = new System.Drawing.Size(0, 17);
            // 
            // toolStripStatusLabelClearMemory
            // 
            toolStripStatusLabelClearMemory.Name = "toolStripStatusLabelClearMemory";
            toolStripStatusLabelClearMemory.Size = new System.Drawing.Size(99, 17);
            toolStripStatusLabelClearMemory.Text = "Clearing Memory";
            toolStripStatusLabelClearMemory.Visible = false;
            // 
            // toolStripProgressBar
            // 
            toolStripProgressBar.Name = "toolStripProgressBar";
            toolStripProgressBar.Size = new System.Drawing.Size(200, 16);
            // 
            // menuStrip1
            // 
            menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { fileToolStripMenuItem, settingsToolStripMenuItem, viewToolStripMenuItem, commandToolStripMenuItem, helpToolStripMenuItem });
            menuStrip1.Location = new System.Drawing.Point(2, 2);
            menuStrip1.Name = "menuStrip1";
            menuStrip1.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            menuStrip1.Size = new System.Drawing.Size(1132, 24);
            menuStrip1.TabIndex = 1;
            menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { readFileToolStripMenuItem, printToolStripMenuItem1, exitToolStripMenuItem });
            fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            fileToolStripMenuItem.Text = "&File";
            // 
            // readFileToolStripMenuItem
            // 
            readFileToolStripMenuItem.Image = Properties.Resources.file;
            readFileToolStripMenuItem.Name = "readFileToolStripMenuItem";
            readFileToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            readFileToolStripMenuItem.Text = "Playback &File...";
            readFileToolStripMenuItem.Click += readFileToolStripMenuItem_Click;
            // 
            // printToolStripMenuItem1
            // 
            printToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { printPropertiesToolStripMenuItem, printRawSerialToolStripMenuItem });
            printToolStripMenuItem1.Name = "printToolStripMenuItem1";
            printToolStripMenuItem1.Size = new System.Drawing.Size(151, 22);
            printToolStripMenuItem1.Text = "Print";
            // 
            // printPropertiesToolStripMenuItem
            // 
            printPropertiesToolStripMenuItem.Name = "printPropertiesToolStripMenuItem";
            printPropertiesToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            printPropertiesToolStripMenuItem.Text = "Print Properties...";
            printPropertiesToolStripMenuItem.Click += printPropertiesToolStripMenuItem_Click;
            // 
            // printRawSerialToolStripMenuItem
            // 
            printRawSerialToolStripMenuItem.Name = "printRawSerialToolStripMenuItem";
            printRawSerialToolStripMenuItem.Size = new System.Drawing.Size(164, 22);
            printRawSerialToolStripMenuItem.Text = "Print Raw Serial...";
            printRawSerialToolStripMenuItem.Click += printRawSerialToolStripMenuItem_Click;
            // 
            // exitToolStripMenuItem
            // 
            exitToolStripMenuItem.Image = Properties.Resources.exit_PNG35;
            exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            exitToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4;
            exitToolStripMenuItem.Size = new System.Drawing.Size(151, 22);
            exitToolStripMenuItem.Text = "E&xit";
            exitToolStripMenuItem.Click += exitToolStripMenuItem_Click;
            // 
            // settingsToolStripMenuItem
            // 
            settingsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { COMPortToolStripMenuItem, autoConnectToolStripMenuItem });
            settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            settingsToolStripMenuItem.Size = new System.Drawing.Size(70, 20);
            settingsToolStripMenuItem.Text = "&Settings...";
            // 
            // COMPortToolStripMenuItem
            // 
            COMPortToolStripMenuItem.Image = Properties.Resources.serial_port_icon;
            COMPortToolStripMenuItem.Name = "COMPortToolStripMenuItem";
            COMPortToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            COMPortToolStripMenuItem.Text = "&COM Port...";
            COMPortToolStripMenuItem.Click += cOMPortToolStripMenuItem_Click;
            // 
            // autoConnectToolStripMenuItem
            // 
            autoConnectToolStripMenuItem.CheckOnClick = true;
            autoConnectToolStripMenuItem.Name = "autoConnectToolStripMenuItem";
            autoConnectToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            autoConnectToolStripMenuItem.Text = "&Auto Connect";
            autoConnectToolStripMenuItem.Click += autoConnectToolStripMenuItem_Click;
            // 
            // viewToolStripMenuItem
            // 
            viewToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { fLARMDisplayToolStripMenuItem });
            viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            viewToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            viewToolStripMenuItem.Text = "&View";
            // 
            // fLARMDisplayToolStripMenuItem
            // 
            fLARMDisplayToolStripMenuItem.Name = "fLARMDisplayToolStripMenuItem";
            fLARMDisplayToolStripMenuItem.Size = new System.Drawing.Size(153, 22);
            fLARMDisplayToolStripMenuItem.Text = "FLARM Display";
            fLARMDisplayToolStripMenuItem.Click += fLARMDisplayToolStripMenuItem_Click;
            // 
            // commandToolStripMenuItem
            // 
            commandToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            commandToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { requestVersionsToolStripMenuItem, requestIDToolStripMenuItem, requestToolStripMenuItem, requestSelftestResultToolStripMenuItem, readIDToolStripMenuItem, readPropertiesToolStripMenuItem, simulateScenarioToolStripMenuItem, dANGERToolStripMenuItem, setDeviceIDToolStripMenuItem });
            commandToolStripMenuItem.Enabled = false;
            commandToolStripMenuItem.Name = "commandToolStripMenuItem";
            commandToolStripMenuItem.Size = new System.Drawing.Size(76, 20);
            commandToolStripMenuItem.Text = "&Command";
            // 
            // requestVersionsToolStripMenuItem
            // 
            requestVersionsToolStripMenuItem.Name = "requestVersionsToolStripMenuItem";
            requestVersionsToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            requestVersionsToolStripMenuItem.Text = "Request &Versions";
            requestVersionsToolStripMenuItem.Click += requestVersionsToolStripMenuItem_Click;
            // 
            // requestIDToolStripMenuItem
            // 
            requestIDToolStripMenuItem.Name = "requestIDToolStripMenuItem";
            requestIDToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            requestIDToolStripMenuItem.Text = "Request &Debug Info";
            requestIDToolStripMenuItem.Click += requestDebugToolStripMenuItem_Click;
            // 
            // requestToolStripMenuItem
            // 
            requestToolStripMenuItem.Name = "requestToolStripMenuItem";
            requestToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            requestToolStripMenuItem.Text = "Request CARP &Range";
            requestToolStripMenuItem.Click += requestToolStripMenuItem_Click;
            // 
            // requestSelftestResultToolStripMenuItem
            // 
            requestSelftestResultToolStripMenuItem.Name = "requestSelftestResultToolStripMenuItem";
            requestSelftestResultToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            requestSelftestResultToolStripMenuItem.Text = "Request &Selftest Result";
            requestSelftestResultToolStripMenuItem.Click += requestSelftestResultToolStripMenuItem_Click;
            // 
            // readIDToolStripMenuItem
            // 
            readIDToolStripMenuItem.Name = "readIDToolStripMenuItem";
            readIDToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            readIDToolStripMenuItem.Text = "Read &ID";
            readIDToolStripMenuItem.Click += readIDToolStripMenuItem_Click;
            // 
            // readPropertiesToolStripMenuItem
            // 
            readPropertiesToolStripMenuItem.Name = "readPropertiesToolStripMenuItem";
            readPropertiesToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            readPropertiesToolStripMenuItem.Text = "Read &Properties";
            readPropertiesToolStripMenuItem.Click += readPropertiesToolStripMenuItem_Click;
            // 
            // simulateScenarioToolStripMenuItem
            // 
            simulateScenarioToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { scenario1CollissionToolStripMenuItem, requestRunningScenarioToolStripMenuItem });
            simulateScenarioToolStripMenuItem.Name = "simulateScenarioToolStripMenuItem";
            simulateScenarioToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            simulateScenarioToolStripMenuItem.Text = "Simulate Scenario";
            // 
            // scenario1CollissionToolStripMenuItem
            // 
            scenario1CollissionToolStripMenuItem.Name = "scenario1CollissionToolStripMenuItem";
            scenario1CollissionToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            scenario1CollissionToolStripMenuItem.Text = "Scenario 1 (collision)";
            scenario1CollissionToolStripMenuItem.Click += scenario1CollissionToolStripMenuItem_Click;
            // 
            // requestRunningScenarioToolStripMenuItem
            // 
            requestRunningScenarioToolStripMenuItem.Name = "requestRunningScenarioToolStripMenuItem";
            requestRunningScenarioToolStripMenuItem.Size = new System.Drawing.Size(212, 22);
            requestRunningScenarioToolStripMenuItem.Text = "Request &Running Scenario";
            requestRunningScenarioToolStripMenuItem.Click += requestRunningScenarioToolStripMenuItem_Click;
            // 
            // dANGERToolStripMenuItem
            // 
            dANGERToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { resetToFactorySettingsToolStripMenuItem, clearMemoryToolStripMenuItem, clearAllFlightLogsToolStripMenuItem });
            dANGERToolStripMenuItem.Image = (System.Drawing.Image)resources.GetObject("dANGERToolStripMenuItem.Image");
            dANGERToolStripMenuItem.Name = "dANGERToolStripMenuItem";
            dANGERToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            dANGERToolStripMenuItem.Text = "DANGER";
            // 
            // resetToFactorySettingsToolStripMenuItem
            // 
            resetToFactorySettingsToolStripMenuItem.Image = Properties.Resources.danger_32x32;
            resetToFactorySettingsToolStripMenuItem.Name = "resetToFactorySettingsToolStripMenuItem";
            resetToFactorySettingsToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            resetToFactorySettingsToolStripMenuItem.Text = "Reset to factory settings";
            resetToFactorySettingsToolStripMenuItem.Click += resetToFactorySettingsToolStripMenuItem_Click;
            // 
            // clearMemoryToolStripMenuItem
            // 
            clearMemoryToolStripMenuItem.Image = Properties.Resources.danger_32x32;
            clearMemoryToolStripMenuItem.Name = "clearMemoryToolStripMenuItem";
            clearMemoryToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            clearMemoryToolStripMenuItem.Text = "Clear Memory";
            clearMemoryToolStripMenuItem.Click += clearMemoryToolStripMenuItem_Click;
            // 
            // clearAllFlightLogsToolStripMenuItem
            // 
            clearAllFlightLogsToolStripMenuItem.Image = Properties.Resources.danger_32x32;
            clearAllFlightLogsToolStripMenuItem.Name = "clearAllFlightLogsToolStripMenuItem";
            clearAllFlightLogsToolStripMenuItem.Size = new System.Drawing.Size(200, 22);
            clearAllFlightLogsToolStripMenuItem.Text = "Clear All Flight Logs";
            clearAllFlightLogsToolStripMenuItem.Click += clearAllFlightLogsToolStripMenuItem_Click;
            // 
            // setDeviceIDToolStripMenuItem
            // 
            setDeviceIDToolStripMenuItem.Name = "setDeviceIDToolStripMenuItem";
            setDeviceIDToolStripMenuItem.Size = new System.Drawing.Size(192, 22);
            setDeviceIDToolStripMenuItem.Text = "&Set Device ID...";
            setDeviceIDToolStripMenuItem.Click += setDeviceIDToolStripMenuItem_Click;
            // 
            // helpToolStripMenuItem
            // 
            helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] { aboutToolStripMenuItem });
            helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            helpToolStripMenuItem.Text = "&Help";
            // 
            // aboutToolStripMenuItem
            // 
            aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            aboutToolStripMenuItem.Size = new System.Drawing.Size(116, 22);
            aboutToolStripMenuItem.Text = "&About...";
            aboutToolStripMenuItem.Click += aboutToolStripMenuItem_Click;
            // 
            // splitContainer1
            // 
            splitContainer1.Anchor = System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right;
            splitContainer1.Location = new System.Drawing.Point(2, 68);
            splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(label1Terminal);
            splitContainer1.Panel1.Controls.Add(textBoxTerminal);
            splitContainer1.Panel1MinSize = 350;
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(labelProperties);
            splitContainer1.Panel2.Controls.Add(richTextBoxProperties);
            splitContainer1.Panel2MinSize = 400;
            splitContainer1.Size = new System.Drawing.Size(1132, 611);
            splitContainer1.SplitterDistance = 590;
            splitContainer1.TabIndex = 2;
            // 
            // label1Terminal
            // 
            label1Terminal.AutoSize = true;
            label1Terminal.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            label1Terminal.Location = new System.Drawing.Point(1, 3);
            label1Terminal.Name = "label1Terminal";
            label1Terminal.Size = new System.Drawing.Size(118, 20);
            label1Terminal.TabIndex = 3;
            label1Terminal.Text = "Raw Serial Data";
            // 
            // labelProperties
            // 
            labelProperties.AutoSize = true;
            labelProperties.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold);
            labelProperties.Location = new System.Drawing.Point(6, 5);
            labelProperties.Name = "labelProperties";
            labelProperties.Size = new System.Drawing.Size(81, 20);
            labelProperties.TabIndex = 1;
            labelProperties.Text = "Properties";
            // 
            // toolStrip1
            // 
            toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            toolStrip1.ImageScalingSize = new System.Drawing.Size(32, 32);
            toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] { toolStripButtonPlay, toolStripButtonPause, toolStripButtonStop, toolStripSeparator1, toolStripButtonRecord });
            toolStrip1.Location = new System.Drawing.Point(2, 26);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new System.Drawing.Size(1132, 39);
            toolStrip1.TabIndex = 3;
            toolStrip1.Text = "toolStrip1";
            // 
            // toolStripButtonPlay
            // 
            toolStripButtonPlay.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButtonPlay.Image = Properties.Resources.Play;
            toolStripButtonPlay.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButtonPlay.Name = "toolStripButtonPlay";
            toolStripButtonPlay.Size = new System.Drawing.Size(36, 36);
            toolStripButtonPlay.Text = "Play";
            toolStripButtonPlay.Click += toolStripButtonPlay_Click;
            // 
            // toolStripButtonPause
            // 
            toolStripButtonPause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButtonPause.Image = Properties.Resources.pause;
            toolStripButtonPause.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButtonPause.Name = "toolStripButtonPause";
            toolStripButtonPause.Size = new System.Drawing.Size(36, 36);
            toolStripButtonPause.Text = "toolStripButton2";
            toolStripButtonPause.ToolTipText = "Pause";
            toolStripButtonPause.Click += toolStripButtonPause_Click;
            // 
            // toolStripButtonStop
            // 
            toolStripButtonStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButtonStop.Image = Properties.Resources.stop;
            toolStripButtonStop.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButtonStop.Name = "toolStripButtonStop";
            toolStripButtonStop.Size = new System.Drawing.Size(36, 36);
            toolStripButtonStop.Text = "Stop";
            toolStripButtonStop.Click += toolStripButtonStop_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(6, 39);
            // 
            // toolStripButtonRecord
            // 
            toolStripButtonRecord.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            toolStripButtonRecord.Image = Properties.Resources.record;
            toolStripButtonRecord.ImageTransparentColor = System.Drawing.Color.Magenta;
            toolStripButtonRecord.Name = "toolStripButtonRecord";
            toolStripButtonRecord.Size = new System.Drawing.Size(36, 36);
            toolStripButtonRecord.Text = "toolStripButton1";
            toolStripButtonRecord.ToolTipText = "Start/Stop Recording";
            toolStripButtonRecord.Click += toolStripButtonRecord_Click;
            // 
            // recordTtimer
            // 
            recordTtimer.Interval = 400;
            recordTtimer.Tick += recordTtimer_Tick;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            ClientSize = new System.Drawing.Size(1136, 703);
            Controls.Add(toolStrip1);
            Controls.Add(splitContainer1);
            Controls.Add(statusStrip);
            Controls.Add(menuStrip1);
            DoubleBuffered = true;
            MainMenuStrip = menuStrip1;
            Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            MinimumSize = new System.Drawing.Size(800, 500);
            Name = "MainForm";
            Padding = new System.Windows.Forms.Padding(2);
            Text = "FLARM Terminal";
            FormClosed += MainForm_FormClosed;
            Load += MainForm_Load;
            Shown += MainForm_Shown;
            statusStrip.ResumeLayout(false);
            statusStrip.PerformLayout();
            menuStrip1.ResumeLayout(false);
            menuStrip1.PerformLayout();
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel1.PerformLayout();
            splitContainer1.Panel2.ResumeLayout(false);
            splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem COMPortToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelPort;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownConnectButton;
        private System.Windows.Forms.ToolStripMenuItem disconnectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem commandToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem requestIDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem requestToolStripMenuItem;
        private System.Windows.Forms.RichTextBox textBoxTerminal;
        private System.Windows.Forms.ToolStripMenuItem requestSelftestResultToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem readIDToolStripMenuItem;
        private System.Windows.Forms.RichTextBox richTextBoxProperties;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusComPortProperties;
        private System.Windows.Forms.ToolStripMenuItem autoConnectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem readPropertiesToolStripMenuItem;
        private System.Windows.Forms.Label label1Terminal;
        private System.Windows.Forms.Label labelProperties;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelGPSUTC;
        private System.Windows.Forms.ToolStripMenuItem requestVersionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem readFileToolStripMenuItem;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton toolStripButtonPlay;
        private System.Windows.Forms.ToolStripButton toolStripButtonPause;
        private System.Windows.Forms.ToolStripButton toolStripButtonStop;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton toolStripButtonRecord;
        private System.Windows.Forms.Timer recordTtimer;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fLARMDisplayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem simulateScenarioToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem scenario1CollissionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem requestRunningScenarioToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelPosition;
        private System.Windows.Forms.ToolStripMenuItem dANGERToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem resetToFactorySettingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearMemoryToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearAllFlightLogsToolStripMenuItem;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabelClearMemory;
        private System.Windows.Forms.ToolStripMenuItem setDeviceIDToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem printPropertiesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem printRawSerialToolStripMenuItem;
    }
}

