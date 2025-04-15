using FlarmTerminal.GUI;
using FlarmTerminal.Properties;
using System;
using System.Runtime.Versioning;
using System.Threading;
using System.Windows.Forms;
using System.Drawing;
using System.Configuration;
using RJCP.IO.Ports;
using Parity = RJCP.IO.Ports.Parity;
using StopBits = RJCP.IO.Ports.StopBits;
using System.Diagnostics;
using Microsoft.VisualBasic.ApplicationServices;
using System.Numerics;
using System.IO;
using System.Timers;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Reflection;
using System.IO.Ports;
using System.Collections;
using System.Resources;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using UCNLNMEA;
using VPKSoft.WinFormsRtfPrint;
using Timer = System.Threading.Timer;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Windows.Controls;
using System.Windows.Media; // Add this namespace for Brushes and FontFamily
using FontAwesome.Sharp;
using Color = System.Drawing.Color;
using Svg;

namespace FlarmTerminal
{
#nullable enable
    [SupportedOSPlatform("windows")]
    public partial class MainForm : Form
    {
        private ISerialPortInfo? _serialPortInfo = null;
        private FlarmMessagesParser? _flarmMessagesParser = null;
        private COMPortHandler? _comPortHandler = null;
        private string _lastCommand = string.Empty;
        private string _rawFlarmDataFile = string.Empty;
        private List<string>? _lines = null!;
        private int _lineIndex = 0;
        private TaskQueue<string>? _taskQueue = null!;
        private System.Timers.Timer _rawFileTimer;

        private bool _isClosing = false;
        private bool _isPaused = false;
        private bool _isStopped = false;
        private bool _isRecording = false;
        private bool _isInitialized = false;
        private bool _recordImageOn = true;
        private bool _requestSelfTestResultsMode = false;
        private FileStream? _fsRecording = null!;
        private FlarmDisplay? _flarmDisplay = null;
        private Serilog.ILogger _log;

        private Dictionary<char, List<double>> _carpData = new Dictionary<char, List<double>>();
        private Dictionary<char, List<double>> _carpMaxData = new Dictionary<char, List<double>>();
        private Dictionary<string, string> _properties = new Dictionary<string, string>();

        private CarpDateTime _carpDateTime = new();
        private System.Threading.Timer? _carpDataRequestTimer;
        private CARPRadarPlot? _carpRadarPlotDialog = null;

        public CarpDateTime CarpDateTime
        {
            get { return _carpDateTime; }
        }

        public Dictionary<string, string> DeviceProperties
        {
            get { return _properties; }
        }

        public long CarpPoints { get; set; } = 0;

        private enum DataSource
        {
            SOURCE_FLARM,
            SOURCE_FILE,
        };

        private DataSource _dataSource = DataSource.SOURCE_FLARM;

        public bool IsClosing
        {
            get => _isClosing;
        }

        public bool IsPaused
        {
            get => _isPaused;
        }

        public MainForm(Serilog.ILogger log)
        {
            _log = log;
            log.Information("MainForm started");
            InitializeComponent();
            this.FormClosing += new FormClosingEventHandler(MainForm_FormClosing);
            toolStripStatusComPortProperties.Text = COMPortSettingsToString();
            // add flarm parser and event handler for new data
            _flarmMessagesParser = new FlarmMessagesParser();
            _flarmMessagesParser.NewPropertiesData += HandleNewPropertiesData;
            _flarmMessagesParser.IGCEnabledDetected += HandleIGCEnabled;
            _flarmMessagesParser.DualPortDetected += HandleDualPortEnabled;
            // init the hanging indent for the properties window
            SetSelectionHangingIndent(richTextBoxProperties, 20);
        }

        private void PrintPropertiesRichTextBox(object? sender, EventArgs e)
        {
            RtfPrint.RichTextBox = richTextBoxProperties;
            RtfPrint.PrintRichTextContents(true, true);
        }

        private void PrintTerminalRichTextBox(object? sender, EventArgs e)
        {
            RtfPrint.RichTextBox = textBoxTerminal;
            RtfPrint.PrintRichTextContents(true, true);
        }

        private string COMPortSettingsToString()
        {
            var settings = Properties.Settings.Default.COMPort;
            settings += $", {Properties.Settings.Default.BaudRate}";

            settings += $", {Properties.Settings.Default.DataBits}";
            switch (Properties.Settings.Default.Parity)
            {
                default:
                case "None":
                    settings += "N";
                    break;
                case "Even":
                    settings += "E";
                    break;
                case "Odd":
                    settings += "O";
                    break;
            }

            settings += $"{Properties.Settings.Default.StopBits}";
            return settings;
        }

        private void MainForm_FormClosing(object? sender, FormClosingEventArgs e)
        {
            _isClosing = true;
            disconnectToolStripMenuItem_Click(this, null);
            Thread.Sleep(100);
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var about = new AboutBox();
            _ = about.ShowDialog();
        }

        private void cOMPortToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var comportconfig = new SerialPortConfig(this);
            if (comportconfig.ShowDialog() == DialogResult.OK && comportconfig.SelectedPort != null)
            {
                try
                {
                    _serialPortInfo = comportconfig.SelectedPort;
                    toolStripStatusLabelPort.Text = _serialPortInfo.Caption;
                    // save in app.config
                    Properties.Settings.Default.COMPort = _serialPortInfo.PortName;
                    Properties.Settings.Default.BaudRate = comportconfig.BaudRate;
                    Properties.Settings.Default.DataBits = comportconfig.DataBits;
                    Properties.Settings.Default.Handshake = comportconfig.HandShake.ToString();
                    switch (comportconfig.StopBits)
                    {
                        default:
                        case System.IO.Ports.StopBits.One:
                            Properties.Settings.Default.StopBits = 1;
                            break;
                        case System.IO.Ports.StopBits.Two:
                            Properties.Settings.Default.StopBits = 2;
                            break;
                    }

                    Properties.Settings.Default.Parity = comportconfig.Parity.ToString();
                    _log.Information(
                        $"COM port properties: Name: '{_serialPortInfo.PortName}', Baud Rate: {comportconfig.BaudRate}, Data Bits: {comportconfig.DataBits}, Stop Bits: {Properties.Settings.Default.StopBits}, Parity: {comportconfig.Parity}, Hand Shake: {comportconfig.HandShake}");
                    var path = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.PerUserRoamingAndLocal)
                        .FilePath;
                    _log.Debug($"COM port configuration path: {path}");
                    Properties.Settings.Default.Save();
                }
                catch (Exception ex)
                {
                    using (new CenterWinDialog(this))
                    {
                        MessageBox.Show($"Failed to save COM port '{_serialPortInfo.PortName}', Error: {ex.Message}",
                            Program.ApplicationName,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (_serialPortInfo != null)
            {
                try
                {
                    if (_comPortHandler == null && !string.IsNullOrEmpty(_serialPortInfo.PortName))
                    {
                        Parity pout = Parity.None;
                        Parity ptmp = Parity.None;
                        if (Enum.TryParse(Properties.Settings.Default.Parity, out ptmp))
                        {
                            pout = ptmp;
                        }

                        StopBits sb = StopBits.One;
                        switch (Properties.Settings.Default.StopBits)
                        {
                            default:
                            case 1:
                                sb = StopBits.One;
                                break;
                            case 2:
                                sb = StopBits.Two;
                                break;
                        }

                        _comPortHandler = new COMPortHandler(this,
                            _log,
                            _serialPortInfo.PortName,
                            Properties.Settings.Default.BaudRate,
                            Properties.Settings.Default.DataBits,
                            ptmp,
                            sb);
                        _log.Information($"Connecting with COM port: {_serialPortInfo.PortName}...");
                        _comPortHandler.Connect();
                        // we're connected!
                        _log.Information($"Connected with COM port: {_serialPortInfo.PortName}!");
                        commandToolStripMenuItem.Enabled = true;
                        toolStripDropDownConnectButton.Image = Resources.green_led;
                        toolStripStatusComPortProperties.Text = _comPortHandler.GetPortProperties();
                        // don't allow to change COM port while connected
                        COMPortToolStripMenuItem.Enabled = false;
                        Application.DoEvents();
                    }
                }
                catch (Exception ex)
                {
                    using (new CenterWinDialog(this))
                    {
                        var msg = $"Failed to connect to COM port '{_serialPortInfo.PortName}', Error: {ex.Message}";
                        _log.Error(msg);
                        MessageBox.Show(msg,
                            Program.ApplicationName,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                using (new CenterWinDialog(this))
                {
                    var msg = $"Select COM port first in settings dialog";
                    _log.Warning(msg);
                    MessageBox.Show(msg,
                        Program.ApplicationName,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
            }
        }

        public void WriteToTerminal(string serialData)
        {
            if (_isClosing)
            {
                return;
            }

            if (textBoxTerminal.InvokeRequired)
            {
                textBoxTerminal.Invoke(new EventHandler(delegate
                {
                    if (_isInitialized &&
                        serialData.Contains("Selftest start") ||
                        serialData.Contains("Initializing Device Info"))
                    {
                        _isInitialized = false;
                    }

                    // selftest status?
                    if (!_isInitialized && serialData.Contains("$PFLAE,A,"))
                    {
                        _isInitialized = true;
                        _flarmDisplay?.SetLED(FlarmDisplay.LEDNames.Power, FlarmDisplay.LEDColor.Green, false);

                        if (_dataSource == DataSource.SOURCE_FLARM && !_requestSelfTestResultsMode)
                        {
                            // send commands to read device properties
                            ReadProperties();
                        }
                    }

                    if (_requestSelfTestResultsMode)
                    {
                        _lastCommand = "$PFLAE";
                        if (serialData.Contains("$PFLAE,A*33"))
                        {
                            _requestSelfTestResultsMode = false;
                        }
                    }

                    if (serialData.StartsWith("$PFLAC,A,CLEARMEM,"))
                    {
                        var items = serialData.Split(',');
                        if (items.Length > 3)
                        {
                            int percent = 0;
                            if (Int32.TryParse(items[3], out percent))
                            {
                                toolStripStatusLabelClearMemory.Visible = true;
                                toolStripProgressBar.Visible = true;
                                toolStripProgressBar.Value = percent;
                                if (percent == 100)
                                {
                                    toolStripProgressBar.Visible = false;
                                    toolStripStatusLabelClearMemory.Visible = false;
                                    using (new CenterWinDialog(this))
                                    {
                                        MessageBox.Show("FLARM memory cleared", ProductName, MessageBoxButtons.OK,
                                            MessageBoxIcon.Information);
                                    }
                                }
                            }
                        }
                    }

                    // properties?
                    if (serialData.StartsWith("$PFLAC,A,") && _flarmMessagesParser != null)
                    {
                        _flarmMessagesParser.ParseProperties(serialData);
                        textBoxTerminal.SelectionColor = System.Drawing.Color.Blue;
                    }

                    if (_isRecording && _fsRecording != null)
                    {
                        var bytes = System.Text.Encoding.ASCII.GetBytes(serialData);
                        _fsRecording.Write(bytes, 0, bytes.Length);
                    }

                    if (_lastCommand != string.Empty && serialData.StartsWith(_lastCommand + ",A"))
                    {
                        textBoxTerminal.SelectionColor = System.Drawing.Color.Blue;
                        textBoxTerminal.AppendText(serialData);
                        _lastCommand = string.Empty;
                    }
                    else
                    {
                        textBoxTerminal.AppendText(serialData);
                    }

                    Application.DoEvents();
                }));
            }
            else
            {
                Debug.WriteLine("InvokeRequired is false: " + serialData);
            }
        }

        private bool IsConnected()
        {
            if (_comPortHandler != null)
            {
                return _comPortHandler.IsConnected;
            }

            return false;
        }

        private void disconnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (IsConnected())
            {
                try
                {
                    _comPortHandler.Dispose();
                    _comPortHandler = null;
                    toolStripDropDownConnectButton.Image = Resources.red_led;
                    commandToolStripMenuItem.Enabled = false;
                    COMPortToolStripMenuItem.Enabled = true;
                    _isInitialized = false;

                }
                catch (Exception ex)
                {
                    using (new CenterWinDialog(this))
                    {
                        MessageBox.Show($"Failed to close COM port '{_serialPortInfo.PortName}', Error: {ex.Message}",
                            Program.ApplicationName,
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Properties.Settings.Default.COMPort))
            {
                _serialPortInfo = SerialPortSearcher.GetPortInfo(Properties.Settings.Default.COMPort);
                if (_serialPortInfo != null)
                {
                    toolStripStatusLabelPort.Text = _serialPortInfo.Caption;
                }

                autoConnectToolStripMenuItem.Checked = Properties.Settings.Default.AutoConnect;
            }

            DoubleBuffered = true;

            _flarmDisplay = new FlarmDisplay(this);
            _flarmDisplay.Visible = false;
        }

        //        public void ShowPosition(int X, int Y)
        //        {
        //            toolStripStatusLabelPosDisplay.Text = $"X: {X}, Y: {Y}";
        //        }

        private void MainForm_Shown(object sender, EventArgs e)
        {
            if (Properties.Settings.Default.AutoConnect)
            {
                connectToolStripMenuItem_Click(this, null);
            }
        }

        private void WriteCommand(string command)
        {
            if (_comPortHandler != null && _comPortHandler.IsConnected)
            {
                textBoxTerminal.SelectionColor = System.Drawing.Color.Red;
                textBoxTerminal.SelectedText = command + Environment.NewLine;
                _log.Debug($"Sending command: 'command'");
                _comPortHandler.Send(command + "\r\n");
                var pos = command.IndexOf(",");
                if (pos > 0)
                {
                    _lastCommand = command.Substring(0, pos);
                }
            }
        }

        private void PopupCarpWindow(object? state)
        {
            if (InvokeRequired)
            {
                Invoke(new Action<object?>(PopupCarpWindow), state);
                return;
            }

            _carpDataRequestTimer?.Dispose();
            _carpDataRequestTimer = null;

            // allow to get response
            if (_carpData.ContainsKey('A'))
            {
                if (_carpRadarPlotDialog != null)
                {
                    _carpRadarPlotDialog.RefreshCarpData();
                }

                if (_carpRadarPlotDialog == null)
                {
                    _carpRadarPlotDialog ??= new CARPRadarPlot(this);
                    _carpRadarPlotDialog.FormClosed += CarpRadarPlotDialog_FormClosed; // Add event handler
                    _carpRadarPlotDialog.Show();
                }
            }
            else
            {
                MessageBox.Show("Sorry, no CARP data available", ProductName, MessageBoxButtons.OK,
                    MessageBoxIcon.Exclamation);
            }
        }

        private void CarpRadarPlotDialog_FormClosed(object? sender, FormClosedEventArgs e)
        {
            _carpRadarPlotDialog = null;
        }

        private void requestDebugToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WriteCommand("$PFLAS,R");
        }

        private void requestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // request CARP data, only PowerFlarms will respond!
            WriteCommand("$PFLAN,R,RANGE");
            _carpDataRequestTimer = new Timer(PopupCarpWindow);
            _carpDataRequestTimer.Change(1500, 0);
            _carpDateTime.startTime = DateTime.Now;
        }

        private void requestSelftestResultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _requestSelfTestResultsMode = true;
            // Request Self Test Result
            // In that case, all errors are returned one by one, with one sentence per error.The
            // terminating $PFLAE,A sentence is used to denote the end of the command.
            WriteCommand("$PFLAE,R");
        }

        private void textBoxTerminal_MouseDown(object sender, MouseEventArgs e)
        {
            // Context Menu on right click
            if (e.Button == MouseButtons.Right)
            {
                var contextMenu = new System.Windows.Forms.ContextMenuStrip();
                var menuItem = new ToolStripMenuItem("Save As...");
                menuItem.Image = IconChar.Save.ToBitmap(IconFont.Solid, 32, Color.Black);
                menuItem.Click += new EventHandler(SaveTerminalToFile);
                contextMenu.Items.Add(menuItem);
                menuItem = new ToolStripMenuItem("Print...");
                menuItem.Image = IconChar.Print.ToBitmap(IconFont.Solid, 32, Color.Black);
                menuItem.Click += new EventHandler(PrintTerminalRichTextBox);
                contextMenu.Items.Add(menuItem);
                menuItem = new ToolStripMenuItem("Clear");
                menuItem.Image = IconChar.Eraser.ToBitmap(IconFont.Solid, 32, Color.Black);
                menuItem.Click += new EventHandler(ClearAction);
                contextMenu.Items.Add(menuItem);
                menuItem = new ToolStripMenuItem("Cut");
                menuItem.Image = IconChar.Cut.ToBitmap(IconFont.Solid, 32, Color.Black);
                menuItem.Click += new EventHandler(CutAction);
                contextMenu.Items.Add(menuItem);
                menuItem = new ToolStripMenuItem("Copy Selection");
                menuItem.Image = IconChar.Copy.ToBitmap(IconFont.Solid, 32, Color.Black);
                menuItem.Click += new EventHandler(CopyAction);
                contextMenu.Items.Add(menuItem);
                menuItem = new ToolStripMenuItem("Copy All (Rich Text)");
                menuItem.Image = IconChar.Copy.ToBitmap(IconFont.Solid, 32, Color.Black);
                menuItem.Click += new EventHandler(CopyAllRichText);
                contextMenu.Items.Add(menuItem);
                menuItem = new ToolStripMenuItem("Copy All (Plain Text)");
                menuItem.Image = IconChar.Copy.ToBitmap(IconFont.Solid, 32, Color.Black);
                menuItem.Click += new EventHandler(CopyAllPlainText);
                contextMenu.Items.Add(menuItem);

                //                menuItem = new ToolStripMenuItem("Paste");
                //                menuItem.Click += new EventHandler(PasteAction);
                //                contextMenu.Items.Add(menuItem);

                textBoxTerminal.ContextMenuStrip = contextMenu;
            }
        }

        private void SaveTerminalToFile(object? sender, EventArgs e)
        {
            try
            {
                using (var saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                    saveFileDialog.Title = "Save Terminal Output";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllText(saveFileDialog.FileName, textBoxTerminal.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                using (new CenterWinDialog(this))
                {
                    MessageBox.Show($"Failed to save file, Error: {ex.Message}",
                        Program.ApplicationName,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }

        private void CopyAllPlainText(object sender, EventArgs e)
        {
            textBoxTerminal.Focus();
            textBoxTerminal.SelectAll();
            Clipboard.Clear();
            Clipboard.SetText(textBoxTerminal.Text, TextDataFormat.Text);
        }

        private void CopyAllRichText(object sender, EventArgs e)
        {
            textBoxTerminal.Focus();
            textBoxTerminal.SelectAll();
            CopyAction(sender, e);
        }

        private void ClearAction(object sender, EventArgs e)
        {
            textBoxTerminal.Clear();
        }

        void CutAction(object sender, EventArgs e)
        {
            textBoxTerminal.Cut();
        }

        void CopyAction(object sender, EventArgs e)
        {
            Clipboard.Clear();
            Clipboard.SetData(DataFormats.Text, textBoxTerminal.SelectedText);
        }

        void PasteAction(object sender, EventArgs e)
        {
            if (Clipboard.ContainsText(TextDataFormat.Rtf))
            {
                textBoxTerminal.SelectedRtf
                    = Clipboard.GetData(DataFormats.Rtf).ToString();
            }
        }

        private void readIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WriteCommand("$PFLAC,R,RADIOID");
            WriteCommand("$PFLAC,R,ID");
            WriteCommand("$PFLAC,R,DEVICEID");
        }

        private void ReadProperties()
        {
            if (_flarmMessagesParser != null)
            {
                richTextBoxProperties.Clear();
                // issue all commands to read common properties
                foreach (var item in Enum.GetValues(typeof(FlarmProperties.ConfigurationItems)))
                {
                    // read below as last item
                    if (item.ToString() == "RADIOID")
                    {
                        continue;
                    }
                    WriteCommand($"$PFLAC,R,{item}");
                    Application.DoEvents();
                }

                Thread.Sleep(100);
                Application.DoEvents();
                // read radio ID and stop properties reading mode
                WriteCommand($"$PFLAC,R,RADIOID");
            }
        }

        private void HandleIGCEnabled(object? sender, bool e)
        {
            foreach (var item in Enum.GetValues(typeof(FlarmProperties.IGCSpecific)))
            {
                WriteCommand($"$PFLAC,R,{item}");
            }
        }

        private void HandleDualPortEnabled(object? sender, bool e)
        {
            foreach (var item in Enum.GetValues(typeof(FlarmProperties.PowerFlarmSpecific)))
            {
                WriteCommand($"$PFLAC,R,{item}");
            }
        }

        public void WriteProperties(string key, string value)
        {
            _properties[key] = value;
            var keyPadRight = key;
            while (keyPadRight.Length < 20)
            {
                keyPadRight += " ";
            }

            WriteToProperties($"{keyPadRight}: {value}\r\n");
        }

        private void HandleNewPropertiesData(object? sender, FlarmMessagesParser.FlarmPropData e)
        {
            if (e.Key != null && e.Value != null)
            {
                WriteProperties(e.Key, e.Value);
            }
        }

        /// <summary>
        /// Helper to set the hanging indent for the RichTextBox
        /// </summary>
        /// <param name="richTextBox"></param>
        /// <param name="characterCount"></param>
        public void SetSelectionHangingIndent(System.Windows.Forms.RichTextBox richTextBox, int characterCount)
        {
            int fudgeValue = 6; // Adjust this value to increase or decrease the indent

            // Get the font used in the RichTextBox
            Font font = richTextBox.SelectionFont ?? richTextBox.Font;

            // Create a Graphics object to measure the width of the character
            using (Graphics g = richTextBox.CreateGraphics())
            {
                // Measure the width of a string of multiple characters
                string sampleText = new string('x', 10);
                SizeF size = g.MeasureString(sampleText, font);

                // Calculate the average width of a single character
                float averageCharWidth = size.Width / 10;

                // Calculate the hanging indent based on the character count
                int hangingIndent = (int)(averageCharWidth * characterCount) + fudgeValue;

                // Set the SelectionHangingIndent property width in pixels
                richTextBox.SelectionHangingIndent = hangingIndent;
            }
        }

        public void WriteToProperties(string strLogMessage)
        {
            richTextBoxProperties.Invoke(new EventHandler(delegate
            {
                richTextBoxProperties.SelectionColor = System.Drawing.Color.Blue;
                richTextBoxProperties.SelectedText = strLogMessage;
            }));
        }

        private void autoConnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Properties.Settings.Default.AutoConnect = autoConnectToolStripMenuItem.Checked;
            Properties.Settings.Default.Save();
        }

        private void readPropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ReadProperties();
        }

        internal void UpdateGPSData(DateTime date, string gPSQuality, int usedSatellitesNumber,
            double precisionHorizontalDilution, double antennaAltitude, string altitudeUnits, double geoidalSeparation,
            string geoidalSeparationUnits, int differentialReferenceStation)
        {
            toolStripStatusLabelGPSUTC.Text = "UTC: " + date.ToString("HH:mm:ss");
        }

        private void requestVersionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WriteCommand("$PFLAV,R");
            WriteCommand("$PFLAC,R,FLARMVER");
        }

        private void EnableFileTimer(bool state)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<bool>(EnableFileTimer), state);
            }
            else
            {
                _rawFileTimer.Enabled = state;
            }
        }

        private void readFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var opnDlg = new OpenFileDialog())
            {
                opnDlg.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                opnDlg.CheckFileExists = true;
                opnDlg.CheckPathExists = true;
                opnDlg.ReadOnlyChecked = true;
                opnDlg.Title = "Open FLARM Raw NMEA Data file";
                if (opnDlg.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _rawFlarmDataFile = File.ReadAllText(opnDlg.FileName);
                        _lines = _rawFlarmDataFile.Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None)
                            .ToList();
                        _lineIndex = 0;
                        _dataSource = DataSource.SOURCE_FILE;
                        _rawFileTimer = new System.Timers.Timer();
                        _rawFileTimer.Interval = 250;
                        _rawFileTimer.AutoReset = false;
                        _rawFileTimer.Enabled = true;
                        _isStopped = false;
                        _rawFileTimer.Elapsed += OnElapsedRawFileTimer;
                        _taskQueue = new TaskQueue<string>(1, this);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void OnElapsedRawFileTimer(object? sender, ElapsedEventArgs e)
        {
            Debug.WriteLine($"_isStopped = {_isStopped}");
            if (_isStopped)
            {
                EnableFileTimer(false);
                _taskQueue?.Dispose();
                _taskQueue = null;
                return;
            }

            if (_lineIndex < _lines.Count && _dataSource == DataSource.SOURCE_FILE)
            {
                var line = _lines[_lineIndex++];
                _taskQueue?.EnqueueTask(line);
                EnableFileTimer(true);
            }
            else
            {
                EnableFileTimer(false);
            }
        }

        private void toolStripButtonPlay_Click(object sender, EventArgs e)
        {
            if (_rawFileTimer != null)
            {
                _isStopped = false;
                EnableFileTimer(true);
            }

            _isPaused = false;
        }

        private void toolStripButtonPause_Click(object sender, EventArgs e)
        {
            if (_rawFileTimer != null)
            {
                EnableFileTimer(false);
            }

            _isPaused = true;
        }

        public bool IsStopped()
        {
            return _isStopped;
        }

        private void toolStripButtonStop_Click(object sender, EventArgs e)
        {
            if (_dataSource == DataSource.SOURCE_FILE)
            {
                if (_rawFileTimer != null)
                {
                    _isStopped = true;
                    _lineIndex = 0;
                    EnableFileTimer(false);
                    _dataSource = DataSource.SOURCE_FLARM;
                }
            }
            else
            {
                _isPaused = true;
                _isStopped = true;
            }
        }

        private void toolStripButtonRecord_Click(object sender, EventArgs e)
        {
            if (_isRecording)
            {
                if (_fsRecording != null)
                {
                    _fsRecording.Close();
                    _fsRecording = null;
                }

                _isRecording = false;
                recordTtimer.Enabled = false;
                toolStripButtonRecord.Image = Resources.record;
            }
            else
            {
                // Save NMEA to file (text file)
                var saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Filter = "Text File|*.txt|All Files|*.*";
                saveFileDialog1.Title = "Save NMEA to File";

                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _fsRecording = File.Open(saveFileDialog1.FileName, FileMode.Create);
                        _isRecording = true;
                        recordTtimer.Enabled = true;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Failed to open file for recording: Error: " + ex.ToString(),
                            ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void recordTtimer_Tick(object sender, EventArgs e)
        {
            _recordImageOn = !_recordImageOn;
            if (_recordImageOn == false)
            {
                toolStripButtonRecord.Image = Resources.record_off;
            }
            else
            {
                toolStripButtonRecord.Image = Resources.record;
            }
        }

        private void fLARMDisplayToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!_flarmDisplay!.Visible)
            {
                _flarmDisplay.Visible = true;
                _flarmDisplay.BringToFront();
                if (!_isInitialized)
                {
                    _flarmDisplay.SetLED(FlarmDisplay.LEDNames.Power, FlarmDisplay.LEDColor.Red, true);
                }
                else
                {
                    _flarmDisplay.SetLED(FlarmDisplay.LEDNames.Power, FlarmDisplay.LEDColor.Green, false);
                }
            }
            else
            {
                _flarmDisplay.Visible = false;
            }
        }

        private void scenario1CollissionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WriteCommand("$PFLAF,S,1");
        }

        private void requestRunningScenarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            WriteCommand("$PFLAF,R");
        }

        internal void UpdateFLARMLeftLED(bool[] on)
        {
            if (_flarmDisplay != null)
            {
                _flarmDisplay.SetLED(FlarmDisplay.LEDNames.RX,
                    on[0] ? FlarmDisplay.LEDColor.Green : FlarmDisplay.LEDColor.Off, true);
                _flarmDisplay.SetLED(FlarmDisplay.LEDNames.TX,
                    on[1] ? FlarmDisplay.LEDColor.Green : FlarmDisplay.LEDColor.Off, true);
                if (on[3])
                {
                    // if Power is on, GPS is either green or blinking red (no lock)
                    if (on[2])
                    {
                        _flarmDisplay.SetLED(FlarmDisplay.LEDNames.GPS, FlarmDisplay.LEDColor.Green, false);
                    }
                    else
                    {
                        _flarmDisplay.SetLED(FlarmDisplay.LEDNames.GPS, FlarmDisplay.LEDColor.Red, true);
                    }
                }
                else
                {
                    // if Power is off, GPS is also off
                    _flarmDisplay.SetLED(FlarmDisplay.LEDNames.GPS, FlarmDisplay.LEDColor.Off, false);
                }

                _flarmDisplay.SetLED(FlarmDisplay.LEDNames.Power,
                    on[3] ? FlarmDisplay.LEDColor.Green : FlarmDisplay.LEDColor.Red, false);
            }
        }

        internal void UpdateFLARMAboveBelowLED(bool[] on, bool Alarm)
        {
            if (_flarmDisplay != null)
            {
                var color = Alarm ? FlarmDisplay.LEDColor.Red : FlarmDisplay.LEDColor.Green;
                _flarmDisplay.SetLED(FlarmDisplay.LEDNames.Above, on[0] ? color : FlarmDisplay.LEDColor.Off, false);
                _flarmDisplay.SetLED(FlarmDisplay.LEDNames.Below, on[1] ? color : FlarmDisplay.LEDColor.Off, false);
            }
        }

        internal async void UpdateDirectionLEDs(bool[] on, int alarmLevel)
        {
            if (_flarmDisplay != null)
            {
                var color = alarmLevel > 0 ? FlarmDisplay.LEDColor.Red : FlarmDisplay.LEDColor.Green;
                int index = 0;
                foreach (var b in on)
                {
                    var led = FlarmDisplay.DirectionLedFromIndex(index++);
                    if (led != null && led.HasValue)
                    {
                        _flarmDisplay.SetLED(led.Value, b ? color : FlarmDisplay.LEDColor.Off, alarmLevel > 0);
                        if (alarmLevel > 0)
                        {
                            await _flarmDisplay.PlayBeep(alarmLevel);
                        }
                    }
                }
            }
        }

        private void resetToFactorySettingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to reset this FLARM device to factory settings?", ProductName,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                WriteCommand("$PFLAR,99");
            }
        }

        private void clearMemoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to clear the memory of this FLARM device?", ProductName,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                WriteCommand("$PFLAC,S,CLEARMEM");
            }
        }

        private void clearAllFlightLogsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to clear the flight logs of this FLARM device?", ProductName,
                    MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                WriteCommand("$PFLAC,CLEARLOGS");
            }
        }

        private void statusStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            _log.Information("Exiting application");
            Environment.Exit(0);
        }

        private void setDeviceIDToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DeviceIDDialog dlg = new DeviceIDDialog(GetDeviceID());
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                if (dlg.getDeviceIDType() == DeviceIDDialog.DeviceIDType.ICAO)
                {
                    // ICAO Address
                    WriteCommand($"$PFLAC,S,ID,{dlg.getICAOAddress()}");
                }
                else
                {
                    // Standard Serial Number
                    WriteCommand($"$PFLAC,S,ID,0xffffff");
                }
            }
        }

        public string GetDeviceID()
        {
            if (_properties.ContainsKey("ID") && _properties.ContainsKey("RADIOID"))
            {
                if (_properties["RADIOID"].Contains("ICAO"))
                {
                    return _properties["ID"];
                }
            }

            return "";
        }

        private void printPropertiesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintPropertiesRichTextBox(sender, e);
        }

        private void printRawSerialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PrintTerminalRichTextBox(sender, e);
        }

        internal void UpdateCARPRadar(char antenna, double[] rangeDoubles)
        {
            switch (antenna)
            {
                case 'A':
                    _carpData['A'] = rangeDoubles.ToList();
                    break;
                case 'B':
                    _carpData['B'] = rangeDoubles.ToList();
                    break;
                default:
                    break;
            }
        }
        public List<double>? GetCARPData(char antenna)
        {
            if (_carpData.TryGetValue(antenna, out var data))
            {
                return data;
            }
            return null;
        }
        public List<double>? GetCARPMaxData(char antenna)
        {
            if (_carpMaxData.TryGetValue(antenna, out var data))
            {
                return data;
            }
            return null;
        }

        internal void UpdateCARPTimeSpan(DateTime start, DateTime end)
        {
            _carpDateTime.startTime = start;
            _carpDateTime.endTime = end;
        }

        internal void UpdateCARPMaxRadar(char antenna, double[] rangeDoubles)
        {
            switch (antenna)
            {
                case 'A':
                    _carpMaxData['A'] = rangeDoubles.ToList();
                    break;
                case 'B':
                    _carpMaxData['B'] = rangeDoubles.ToList();
                    break;
                default:
                    break;
            }
        }

        private void richTextBoxProperties_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                var contextMenu = new System.Windows.Forms.ContextMenuStrip();
                var menuItem = new ToolStripMenuItem("Save As...");
                menuItem.Image = IconChar.Save.ToBitmap(IconFont.Solid, 32, Color.Black);
                menuItem.Click += new EventHandler(SavePropertiesToFile);
                contextMenu.Items.Add(menuItem);
                menuItem = new ToolStripMenuItem("Print...");
                menuItem.Image = IconChar.Print.ToBitmap(IconFont.Solid, 32, Color.Black);
                menuItem.Click += new EventHandler(PrintPropertiesRichTextBox);
                contextMenu.Items.Add(menuItem);
                richTextBoxProperties.ContextMenuStrip = contextMenu;
            }
        }

        private void SavePropertiesToFile(object? sender, EventArgs e)
        {
            try
            {
                using (var saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
                    saveFileDialog.Title = "Save Properties Output";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllText(saveFileDialog.FileName, richTextBoxProperties.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            var aboutDialog = new AboutBox();
            aboutDialog.ShowDialog();
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            var bitmap = LogoLoader.LoadLogo();
            if (bitmap != null)
            {
                // Draw the Bitmap onto the PictureBox using PaintEventArgs e
                e.Graphics.DrawImage(bitmap, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));
            }
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void cutToolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void copyToolStripButton_Click(object sender, EventArgs e)
        {

        }
    }
}