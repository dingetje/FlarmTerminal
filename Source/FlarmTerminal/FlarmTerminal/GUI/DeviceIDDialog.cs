using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FlarmTerminal.GUI
{
    public partial class DeviceIDDialog : Form
    {
        public enum DeviceIDType
        {
            ICAO,
            Serial
        }

        public DeviceIDDialog(string deviceID)
        {
            InitializeComponent();
            if (deviceID.Length == 6)
            {
                radioButtonICAO.Checked = true;
                textBoxICAO.Text = deviceID;
            }
            else
            {
                radioButtonSerial.Checked = true;
            }
        }

        public DeviceIDType getDeviceIDType()
        {
            if (radioButtonSerial.Checked)
            {
                return DeviceIDType.Serial;
            }
            else
            {
                return DeviceIDType.ICAO;
            }
        }

        public string getICAOAddress()
        {
            return textBoxICAO.Text;
        }

        private void buttonOk_Click(object sender, EventArgs e)
        {
            if (radioButtonICAO.Checked && String.IsNullOrEmpty(textBoxICAO.Text))
            {
                MessageBox.Show("Please provide a valid ICAO address", 
                    Program.ApplicationName, 
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error);
                return;
            }
        }
    }
}
