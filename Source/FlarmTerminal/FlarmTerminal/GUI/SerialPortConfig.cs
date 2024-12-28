using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Reflection;
using System.Runtime.Versioning;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

#nullable enable
namespace FlarmTerminal.GUI
{
    [SupportedOSPlatform("windows")]
    public partial class SerialPortConfig : Form
    {
        private IEnumerable<ISerialPortInfo> _serialPorts;
        private ISerialPortInfo _serialPortInfo = null;
        private MainForm _MainForm = null;
        // defaults settings, 19K2, 8N1, None
        private int _baudRate = 19200;
        private Parity _parity = Parity.None;
        private StopBits _stopBits = StopBits.One;
        private int _dataBits = 8;
        private Handshake _handShake = Handshake.None;

        // selected properties
        public ISerialPortInfo? SelectedPort => _serialPortInfo;
        public int BaudRate => _baudRate;
        public int DataBits => _dataBits;
        public Parity Parity => _parity;
        public StopBits StopBits => _stopBits;
        public Handshake HandShake => _handShake;

        public SerialPortConfig(MainForm f)
        {
            _MainForm = f;
            InitializeComponent();
        }

        private void SerialPortConfig_Load(object sender, EventArgs e)
        {
        }

        private void SerialPortConfig_ResizeEnd(object sender, EventArgs e)
        {
            ResizeColumnHeader();
        }

        private void ResizeColumnHeader()
        {
            for (int i = 0; i < lvComPorts.Columns.Count - 1; i++)
            {
                lvComPorts.AutoResizeColumn(i, ColumnHeaderAutoResizeStyle.ColumnContent);
            }
            lvComPorts.Columns[lvComPorts.Columns.Count - 1].Width = -2;
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            Cursor? previousCursor = Cursor.Current;
            try
            {
                if (Cursor.Current != null)
                {
                    Cursor.Current = Cursors.WaitCursor;
                }
                Application.DoEvents();
                _serialPorts = SerialPortSearcher.Search().ToArray();
                lvComPorts.Items.Clear();
                foreach (var port in _serialPorts)
                {
                    ListViewItem value = new ListViewItem(new[] { "", port.PortName, port.Description });
                    lvComPorts.Items.Add(value);
                }
            }
            catch (Exception ex)
            {
                using (new CenterWinDialog(_MainForm))
                {
                    MessageBox.Show("Oops: " + ex.Message,
                        Program.ApplicationName,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Exclamation);
                }
            }
            finally
            {
                if (Cursor.Current == Cursors.WaitCursor)
                {
                    Cursor.Current = previousCursor;
                }
            }
        }

        private void lvComPorts_SelectedIndexChanged(object sender, EventArgs e)
        {
            var items = lvComPorts.SelectedItems;
            if (items != null && items.Count == 1)
            {
                var item = items[0];
                var COMPortName = item.SubItems[1];
                if (COMPortName != null && !string.IsNullOrEmpty(COMPortName.Text))
                {
                    var port = _serialPorts.Where(a => a.PortName == COMPortName.Text).FirstOrDefault();
                    if (port != null)
                    {
                        try
                        {
                            // try to open the port
                            var prt = new SerialPort(port.PortName);

                            // we need to open the port to get its properties
                            prt.Open();
                            var i = comboBoxBaudRate.Items.IndexOf(prt.BaudRate.ToString());
                            if (i != -1)
                            {
                                comboBoxBaudRate.SelectedIndex = i;
                            }
                            i = comboBoxStopBits.Items.IndexOf(prt.StopBits.ToString());
                            if (i != -1)
                            {
                                comboBoxStopBits.SelectedIndex = i;
                            }
                            i = comboBoxParity.Items.IndexOf(prt.Parity.ToString());
                            if (i != -1)
                            {
                                comboBoxParity.SelectedIndex = i;
                            }
                            i = comboBoxDataBits.Items.IndexOf(prt.DataBits.ToString());
                            if (i != -1)
                            {
                                comboBoxDataBits.SelectedIndex = i;
                            }
                            i = comboBoxFlowControl.Items.IndexOf(prt.Handshake.ToString());
                            if (i != -1)
                            {
                                comboBoxFlowControl.SelectedIndex = i;
                            }
                            _serialPortInfo = port;
                            prt.Close();
                        }
                        catch (Exception ex)
                        {
                            using (new CenterWinDialog(_MainForm))
                            {
                                MessageBox.Show("Oops: " + ex.Message,
                                Program.ApplicationName,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                            }
                        }
                    }
                }
            }
        }

        private void comboBoxBaudRate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxBaudRate.SelectedItem != null)
            {
                int tmp = 19200;
                if (Int32.TryParse(comboBoxBaudRate.SelectedItem.ToString(), out tmp))
                {
                    _baudRate = tmp;
                }
            }
        }

        private void comboBoxDataBits_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxDataBits.SelectedItem != null)
            {
                int tmp = 8;
                if (Int32.TryParse(comboBoxDataBits.SelectedItem.ToString(), out tmp))
                {
                    _dataBits = tmp;
                }
            }
        }

        private void comboBoxParity_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxParity.SelectedItem != null)
            {
                Parity p = Parity.None;
                if (Enum.TryParse(comboBoxParity.SelectedItem.ToString(), out p))
                {
                    _parity = p;
                }
            }
        }

        private void comboBoxStopBits_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxStopBits.SelectedItem != null)
            {
                StopBits sb = StopBits.One;
                if (Enum.TryParse(comboBoxStopBits.SelectedItem.ToString(), out sb))
                {
                    _stopBits = sb;
                }
            }
        }

        private void comboBoxFlowControl_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBoxFlowControl.SelectedItem != null)
            {
                Handshake hs = Handshake.None;
                if (Enum.TryParse(comboBoxFlowControl.SelectedItem.ToString(), out hs))
                {
                    _handShake = hs;
                }
            }
        }

        private void SerialPortConfig_Shown(object sender, EventArgs e)
        {
            Cursor? currentCursor = _MainForm.Cursor;
            _MainForm.Cursor = Cursors.WaitCursor;
            Application.DoEvents();
            // search for COM ports
            buttonRefresh_Click(this, EventArgs.Empty);
            ResizeColumnHeader();
            // found any COM ports?
            if (lvComPorts.Items.Count > 0)
            {
                // If we have a COM port in Settings select that one
                if (!string.IsNullOrEmpty(Properties.Settings.Default.COMPort))
                {
                    bool bFound = false;
                    for (int i = 0; i < lvComPorts.Items.Count; i++)
                    {
                        if (lvComPorts.Items[i].SubItems.Count > 0)
                        {
                            var COMPortName = lvComPorts.Items[i].SubItems[1].Text;
                            if (COMPortName == FlarmTerminal.Properties.Settings.Default.COMPort)
                            {
                                bFound = true;
                                lvComPorts.Items[i].Selected = true;
                                break;
                            }
                        }
                    }
                    // if not found select first on in the list
                    if (!bFound)
                    {
                        lvComPorts.Items[0].Selected = true;
                    }
                }
            }
            else
            {
                using (new CenterWinDialog(_MainForm))
                {
                    MessageBox.Show("No COM Ports found on this system!",
                    Program.ApplicationName,
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Stop);
                }
            }
            _MainForm.Cursor = currentCursor;
        }
    }
}
