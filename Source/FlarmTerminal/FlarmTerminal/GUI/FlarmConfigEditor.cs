using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace FlarmTerminal.GUI
{
    public partial class FlarmConfigEditor : Form
    {
        private MainForm? _mainForm = null;

        public FlarmConfigEditor(MainForm? mainForm = null)
        {
            InitializeComponent();
            _mainForm = mainForm;
            // Only call PopulateFields if not in design mode
            if (!DesignMode)
            {
                PopulateFields();
                UpdateReadFromDeviceButtonState();
            }
        }

        private FlarmConfigModel _model = new FlarmConfigModel();

        private static readonly Dictionary<string, string> AcftTypeDescriptions = new()
        {
            {"1", "1 - Glider/Motor Glider"},
            {"2", "2 - Tow Plane"},
            {"3", "3 - Helicopter/Rotorcraft"},
            {"4", "4 - Parachute"},
            {"5", "5 - Drop plane for skydivers"},
            {"6", "6 - Hang glider"},
            {"7", "7 - Para-glider"},
            {"8", "8 - Reciprocating engine(s)"},
            {"9", "9 - Jet/turboprop engine(s)"},
            {"11", "11 - Balloon"},
            {"12", "12 - Airship"},
            {"13", "13 - UAV"},
            {"15", "15 - Static Object"}
        };

        private static readonly Dictionary<string, string> BaudDescriptions = new()
        {
            {"0", "0 - 4800"},
            {"1", "1 - 9600"},
            {"2", "2 - 19200"},
            {"3", "3 - 28800"},
            {"4", "4 - 38400"},
            {"5", "5 - 57600"},
            {"6", "6 - 115200"},
            {"7", "7 - 230400"}
        };

        private static readonly Dictionary<string, string> PrivacyDescriptions = new()
        {
            {"0", "0 - Disabled"},
            {"1", "1 - Enabled"}
        };

        private static readonly Dictionary<string, string> NoTrackDescriptions = new()
        {
            {"0", "0 - Disabled"},
            {"1", "1 - Enabled"}
        };

        private static readonly Dictionary<string, string> CFlagsDescriptions = new()
        {
            {"0", "0 - No flags set"},
            {"1", "1 - Disable INFO Alerts (Classic only)"},
            {"2", "2 - Competition mode (reduced alarm distances/times)"}
            // 4...255 reserved, do not use
        };

        private static readonly Dictionary<string, string> NmeaOutDescriptions = new()
        {
            {"0", "0 - no output"},
            {"1", "1 - GPRMC, GPGGA, GPGSA + FLARM proprietary (incl. PGRMZ)"},
            {"2", "2 - Only GPRMC, GPGGA, GPGSA (no FLARM proprietary)"},
            {"3", "3 - Only FLARM proprietary (incl. PGRMZ) (no GPRMC, GPGGA, GPGSA)"},
            {"4", "4 - Garmin TIS (binary, license may be required, baud=9600)"},
            {"5", "5 - GDL90 (binary, only on PowerFLARM Fusion NMEAOUT2, baud=38400)"},
            {"40", "40 - Like 0, protocol v4/5"},
            {"41", "41 - Like 1, protocol v4/5"},
            {"42", "42 - Like 2, protocol v4/5"},
            {"43", "43 - Like 3, protocol v4/5"},
            {"44", "44 - Like 4, protocol v4/5"},
            {"60", "60 - Like 0, protocol v6"},
            {"61", "61 - Like 1, protocol v6"},
            {"62", "62 - Like 2, protocol v6"},
            {"63", "63 - Like 3, protocol v6"},
            {"64", "64 - Like 4, protocol v6"},
            {"70", "70 - Like 0, protocol v7"},
            {"71", "71 - Like 1, protocol v7"},
            {"72", "72 - Like 2, protocol v7"},
            {"73", "73 - Like 3, protocol v7"},
            {"74", "74 - Like 4, protocol v7"},
            {"80", "80 - Like 0, protocol v8"},
            {"81", "81 - Like 1, protocol v8"},
            {"82", "82 - Like 2, protocol v8"},
            {"83", "83 - Like 3, protocol v8"},
            {"84", "84 - Like 4, protocol v8"},
            {"90", "90 - Like 0, protocol v9"},
            {"91", "91 - Like 1, protocol v9"},
            {"92", "92 - Like 2, protocol v9"},
            {"93", "93 - Like 3, protocol v9"},
            {"94", "94 - Like 4, protocol v9"}
        };

        private void PopulateFields()
        {
            comboBoxNMEAOUT.Items.AddRange(NmeaOutDescriptions.Values.ToArray());
            comboBoxACFT.Items.AddRange(new object[] {
                "1 - Glider/Motor Glider", "2 - Tow Plane", "3 - Helicopter/Rotorcraft", "4 - Parachute",
                "5 - Drop plane for skydivers", "6 - Hang glider", "7 - Para-glider", "8 - Reciprocating engine(s)",
                "9 - Jet/turboprop engine(s)", "11 - Balloon", "12 - Airship", "13 - UAV", "15 - Static Object"
            });
            comboBoxCFLAGS.Items.AddRange(new object[] {
                "0 - No flags set",
                "1 - Disable INFO Alerts (Classic only)",
                "2 - Competition mode (reduced alarm distances/times)"
            });
            comboBoxPRIV.Items.AddRange(new object[] { "0 - Disabled", "1 - Enabled" });
            comboBoxNOTRACK.Items.AddRange(new object[] { "0 - Disabled", "1 - Enabled" });
            // Remove comboBoxTHRE population, now using numericUpDownTHRE
            comboBoxBAUD.Items.AddRange(new object[] { "0 - 4800", "1 - 9600", "2 - 19200", "3 - 28800", "4 - 38400", "5 - 57600", "6 - 115200", "7 - 230400" });
        }

        public void LoadConfig(string filePath)
        {
            _model = FlarmConfigModel.LoadFromFile(filePath);
            UpdateUIFromModel();
        }

        private void UpdateUIFromModel()
        {
            textBoxID.Text = _model.ID;
            // Show NMEAOUT with description if possible
            if (NmeaOutDescriptions.TryGetValue(_model.NMEAOUT, out var nmeaDesc))
                comboBoxNMEAOUT.Text = nmeaDesc;
            else
                comboBoxNMEAOUT.Text = _model.NMEAOUT;
            // Show ACFT with description if possible
            if (AcftTypeDescriptions.TryGetValue(_model.ACFT, out var acftDesc))
                comboBoxACFT.Text = acftDesc;
            else
                comboBoxACFT.Text = _model.ACFT;
            // Show CFLAGS with description if possible (only for 0, 1, 2)
            if (CFlagsDescriptions.TryGetValue(_model.CFLAGS, out var cflagsDesc))
                comboBoxCFLAGS.Text = cflagsDesc;
            else
                comboBoxCFLAGS.Text = _model.CFLAGS;
            // Show PRIV with description if possible
            if (PrivacyDescriptions.TryGetValue(_model.PRIV, out var privDesc))
                comboBoxPRIV.Text = privDesc;
            else
                comboBoxPRIV.Text = _model.PRIV;
            // Show NOTRACK with description if possible
            if (NoTrackDescriptions.TryGetValue(_model.NOTRACK, out var notrackDesc))
                comboBoxNOTRACK.Text = notrackDesc;
            else
                comboBoxNOTRACK.Text = _model.NOTRACK;
            // Set numericUpDownTHRE for THRE value
            if (int.TryParse(_model.THRE, out int threVal))
                numericUpDownTHRE.Value = Math.Max(numericUpDownTHRE.Minimum, Math.Min(numericUpDownTHRE.Maximum, threVal));
            else
                numericUpDownTHRE.Value = numericUpDownTHRE.Minimum;
            // numericUpDownLOGINT replaces textBoxLOGINT
            if (int.TryParse(_model.LOGINT, out int logintVal))
            {
                numericUpDownLOGINT.Value = Math.Max(numericUpDownLOGINT.Minimum, Math.Min(numericUpDownLOGINT.Maximum, logintVal));
            }
            else
            {
                numericUpDownLOGINT.Value = numericUpDownLOGINT.Minimum;
            }
            textBoxPILOT.Text = _model.PILOT;
            textBoxCOMPCLASS.Text = _model.COMPCLASS;
            textBoxCOMPID.Text = _model.COMPID;
            textBoxGLIDERID.Text = _model.GLIDERID;
            textBoxGLIDERTYPE.Text = _model.GLIDERTYPE;
            // Show BAUD with description if possible
            if (BaudDescriptions.TryGetValue(_model.BAUD, out var baudDesc))
                comboBoxBAUD.Text = baudDesc;
            else
                comboBoxBAUD.Text = _model.BAUD;
        }

        private void UpdateModelFromUI()
        {
            _model.ID = textBoxID.Text.Trim();
            _model.NMEAOUT = comboBoxNMEAOUT.Text.Split(' ')[0];
            _model.ACFT = comboBoxACFT.Text.Split(' ')[0];
            _model.CFLAGS = comboBoxCFLAGS.Text.Split(' ')[0];
            _model.PRIV = comboBoxPRIV.Text.Split(' ')[0];
            _model.NOTRACK = comboBoxNOTRACK.Text.Split(' ')[0];
            _model.THRE = ((int)numericUpDownTHRE.Value).ToString();
            // numericUpDownLOGINT replaces textBoxLOGINT
            _model.LOGINT = ((int)numericUpDownLOGINT.Value).ToString();
            _model.PILOT = textBoxPILOT.Text.Trim();
            _model.COMPCLASS = textBoxCOMPCLASS.Text.Trim();
            _model.COMPID = textBoxCOMPID.Text.Trim();
            _model.GLIDERID = textBoxGLIDERID.Text.Trim();
            _model.GLIDERTYPE = textBoxGLIDERTYPE.Text.Trim();
            _model.BAUD = comboBoxBAUD.Text.Split(' ')[0];
        }

        public void PopulateFromDeviceProperties(Dictionary<string, string> properties)
        {
            if (properties == null) return;
            // Use the raw property item names as keys
            if (properties.TryGetValue("ID", out var id)) _model.ID = id;
            if (properties.TryGetValue("NMEAOUT", out var nmeaout)) _model.NMEAOUT = nmeaout;
            if (properties.TryGetValue("ACFT", out var acft)) _model.ACFT = acft;
            if (properties.TryGetValue("CFLAGS", out var cflags)) _model.CFLAGS = cflags;
            if (properties.TryGetValue("PRIV", out var priv)) _model.PRIV = priv;
            if (properties.TryGetValue("NOTRACK", out var notrack)) _model.NOTRACK = notrack;
            if (properties.TryGetValue("THRE", out var thre)) _model.THRE = thre.Replace("m/s", "");
            if (properties.TryGetValue("LOGINT", out var logint)) _model.LOGINT = logint.Split(' ')[0];
            if (properties.TryGetValue("PILOT", out var pilot)) _model.PILOT = pilot;
            if (properties.TryGetValue("COMPCLASS", out var compclass)) _model.COMPCLASS = compclass;
            if (properties.TryGetValue("COMPID", out var compid)) _model.COMPID = compid;
            if (properties.TryGetValue("GLIDERID", out var gliderid)) _model.GLIDERID = gliderid;
            if (properties.TryGetValue("GLIDERTYPE", out var glidertype)) _model.GLIDERTYPE = glidertype;
            if (properties.TryGetValue("BAUD", out var baud)) _model.BAUD = baud;
            UpdateUIFromModel();
        }

        private void buttonLoad_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Title = "Open FLARM configuration file";
                dlg.FileName = "flarmcfg.txt";
                dlg.Filter = "FLARM Config (*.txt)|*.txt|All Files|*.*";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    LoadConfig(dlg.FileName);
                }
            }
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            UpdateModelFromUI();
            using (var dlg = new SaveFileDialog())
            {
                dlg.Title = "Save FLARM configuration file";
                dlg.Filter = "FLARM Config (*.txt)|*.txt|All Files|*.*";
                dlg.FileName = "flarmcfg.txt";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    File.WriteAllText(dlg.FileName, _model.ToFileContent());
                    using (new CenterWinDialog(this))
                    {
                        MessageBox.Show("Configuration saved.", "FLARM Config", MessageBoxButtons.OK,
                            MessageBoxIcon.Information);
                    }
                }
            }
        }

        private void buttonPreview_Click(object sender, EventArgs e)
        {
            UpdateModelFromUI();
            using (new CenterWinDialog(this))
            {
                MessageBox.Show(_model.ToFileContent(), "Preview flarmcfg.txt", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonHelpID_Click(object sender, EventArgs e)
        {
            Help.ShowPopup(textBoxID, "Enter the ICAO 24-bit aircraft address here or use FFFFFF to use the default factory ID (not recommended).", textBoxID.PointToScreen(new Point(0, textBoxID.Height)));
        }

        private void UpdateReadFromDeviceButtonState()
        {
            buttonReadFromDevice.Enabled = _mainForm != null && _mainForm.IsHandleCreated && _mainForm.InvokeRequired
                ? (bool)_mainForm.Invoke(new Func<bool>(() => _mainForm.IsConnectedPublic()))
                : (_mainForm != null && _mainForm.IsConnectedPublic());
        }

        private void buttonReadFromDevice_Click(object sender, EventArgs e)
        {
            if (_mainForm != null && _mainForm.IsConnectedPublic())
            {
                // Ask MainForm to read properties from device
                _mainForm.Invoke(new Action(() => _mainForm.ReadPropertiesPublic()));
                // Wait for properties to be updated, then populate fields
                // Use a timer to poll for updated properties
                Timer timer = new Timer();
                timer.Interval = 500;
                int pollCount = 0;
                timer.Tick += (s, args) =>
                {
                    pollCount++;
                    var props = _mainForm.DeviceProperties;
                    if (props != null && props.ContainsKey("RADIOID"))
                    {
                        timer.Stop();
                        PopulateFromDeviceProperties(props);
//                        MessageBox.Show("Device properties loaded into the editor.", "Read from Device", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if (pollCount > 10) // Timeout after 5 seconds
                    {
                        timer.Stop();
                        using (new CenterWinDialog(this))
                        {
                            MessageBox.Show("Failed to read properties from device.", "Read from Device", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                };
                timer.Start();
            }
            else
            {
                using (new CenterWinDialog(this))
                {
                    MessageBox.Show("No serial connection established.", "Read from Device", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            UpdateReadFromDeviceButtonState();
        }

        protected override void OnActivated(EventArgs e)
        {
            base.OnActivated(e);
            UpdateReadFromDeviceButtonState();
        }
    }

    public class FlarmConfigModel
    {
        public string ID { get; set; } = "";
        public string NMEAOUT { get; set; } = "";
        public string ACFT { get; set; } = "";
        public string CFLAGS { get; set; } = "";
        public string PRIV { get; set; } = "";
        public string NOTRACK { get; set; } = "";
        public string THRE { get; set; } = "";
        public string LOGINT { get; set; } = "";
        public string PILOT { get; set; } = "";
        public string COMPCLASS { get; set; } = "";
        public string COMPID { get; set; } = "";
        public string GLIDERID { get; set; } = "";
        public string GLIDERTYPE { get; set; } = "";
        public string BAUD { get; set; } = "";

        public static FlarmConfigModel LoadFromFile(string filePath)
        {
            var model = new FlarmConfigModel();
            foreach (var line in File.ReadAllLines(filePath))
            {
                if (line.StartsWith("$PFLAC,S,ID,")) model.ID = line.Split(',')[3];
                if (line.StartsWith("$PFLAC,S,NMEAOUT,")) model.NMEAOUT = line.Split(',')[3];
                if (line.StartsWith("$PFLAC,S,ACFT,")) model.ACFT = line.Split(',')[3];
                if (line.StartsWith("$PFLAC,S,CFLAGS,")) model.CFLAGS = line.Split(',')[3];
                if (line.StartsWith("$PFLAC,S,PRIV,")) model.PRIV = line.Split(',')[3];
                if (line.StartsWith("$PFLAC,S,NOTRACK,")) model.NOTRACK = line.Split(',')[3];
                if (line.StartsWith("$PFLAC,S,THRE,")) model.THRE = line.Split(',')[3];
                if (line.StartsWith("$PFLAC,S,LOGINT,")) model.LOGINT = line.Split(',')[3];
                if (line.StartsWith("$PFLAC,S,PILOT,")) model.PILOT = line.Split(',')[3];
                if (line.StartsWith("$PFLAC,S,COMPCLASS,")) model.COMPCLASS = line.Split(',')[3];
                if (line.StartsWith("$PFLAC,S,COMPID,")) model.COMPID = line.Split(',')[3];
                if (line.StartsWith("$PFLAC,S,GLIDERID,")) model.GLIDERID = line.Split(',')[3];
                if (line.StartsWith("$PFLAC,S,GLIDERTYPE,")) model.GLIDERTYPE = line.Split(',')[3];
                if (line.StartsWith("$PFLAC,S,BAUD,")) model.BAUD = line.Split(',')[3];
            }
            return model;
        }

        public string ToFileContent()
        {
            var lines = new List<string>
            {
                "# Basic Settings",
                "$PFLAC,S,ID," + ID,
                "$PFLAC,S,NMEAOUT," + NMEAOUT,
                "$PFLAC,S,ACFT," + ACFT,
                "$PFLAC,S,CFLAGS," + CFLAGS,
                "$PFLAC,S,PRIV," + PRIV,
                "$PFLAC,S,NOTRACK," + NOTRACK,
                "$PFLAC,S,THRE," + THRE,
                "",
                "# IGC Settings",
                "$PFLAC,S,LOGINT," + LOGINT,
                "$PFLAC,S,PILOT," + PILOT,
                "$PFLAC,S,COMPCLASS," + COMPCLASS,
                "$PFLAC,S,COMPID," + COMPID,
                "$PFLAC,S,GLIDERID," + GLIDERID,
                "$PFLAC,S,GLIDERTYPE," + GLIDERTYPE,
                "",
                "# Baud rate",
                "$PFLAC,S,BAUD," + BAUD
            };
            return string.Join(Environment.NewLine, lines);
        }
    }
}
