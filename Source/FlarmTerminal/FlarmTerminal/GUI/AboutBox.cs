using System;
using System.Drawing;
using System.Reflection;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace FlarmTerminal.GUI
{
    [SupportedOSPlatform("windows")]
    partial class AboutBox : Form
    {
        public AboutBox()
        {
            InitializeComponent();

            this.Text = String.Format("About {0}", AssemblyTitle);
            this.labelProductName.Text = AssemblyProduct;
            this.labelVersion.Text = String.Format("Version {0}", AssemblyVersion);
            this.labelCopyright.Text = AssemblyCopyright;
            this.labelCompanyName.Text = "Written by " + AssemblyCompany;
            this.textBoxDescription.Text = AssemblyDescription;
            this.textBoxDescription.Text += Environment.NewLine + Environment.NewLine;
            this.textBoxDescription.Text += "FlarmTerminal is using the following 3rd party packages and libraries:";
            this.textBoxDescription.Text += Environment.NewLine + Environment.NewLine;
            this.textBoxDescription.Text += "* NAudio" + Environment.NewLine;
            this.textBoxDescription.Text += "* ScottPlot.WinForms" + Environment.NewLine;
            this.textBoxDescription.Text += "* SerialPortStream" + Environment.NewLine;
            this.textBoxDescription.Text += "* Serilog" + Environment.NewLine;
            this.textBoxDescription.Text += "* Svg.NET" + Environment.NewLine;
            this.textBoxDescription.Text += "* FontAwesome.Sharp" + Environment.NewLine;
            this.textBoxDescription.Text += "* UCNLNMEA, NMEA 0183 protocol support library" + Environment.NewLine;
            this.textBoxDescription.Text += Environment.NewLine + Environment.NewLine;
            this.textBoxDescription.Text += "FlarmTerminal is a free software project. It is licensed under the MIT License";
            this.textBoxDescription.Text += Environment.NewLine + Environment.NewLine;
            this.textBoxDescription.Text += "--------------------------------------------------------------------" +Environment.NewLine;
            this.textBoxDescription.Text += "MIT License\r\n\r\nCopyright (c) 2024 dingetje\r\n\r\nPermission is hereby granted, free of charge, to any person obtaining a copy\r\nof this software and associated documentation files (the \"Software\"), to deal\r\nin the Software without restriction, including without limitation the rights\r\nto use, copy, modify, merge, publish, distribute, sublicense, and/or sell\r\ncopies of the Software, and to permit persons to whom the Software is\r\nfurnished to do so, subject to the following conditions:\r\n\r\nThe above copyright notice and this permission notice shall be included in all\r\ncopies or substantial portions of the Software.\r\n\r\nTHE SOFTWARE IS PROVIDED \"AS IS\", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR\r\nIMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,\r\nFITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE\r\nAUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER\r\nLIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,\r\nOUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE\r\nSOFTWARE.";
        }

        #region Assembly Attribute Accessors

        public string AssemblyTitle
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyTitleAttribute), false);
                if (attributes.Length > 0)
                {
                    AssemblyTitleAttribute titleAttribute = (AssemblyTitleAttribute)attributes[0];
                    if (titleAttribute.Title != "")
                    {
                        return titleAttribute.Title;
                    }
                }
                return System.IO.Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().Location);
            }
        }

        public string AssemblyVersion
        {
            get
            {
                return Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }
        }

        public string AssemblyDescription
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                object[] attributes = Assembly.GetExecutingAssembly().GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                if (attributes.Length == 0)
                {
                    return "";
                }
                return ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }
        #endregion

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            var bitmap = LogoLoader.LoadLogo();
            if (bitmap != null)
            {
                // Draw the Bitmap onto the PictureBox using PaintEventArgs e
                e.Graphics.DrawImage(bitmap, new Rectangle(0, 0, pictureBox1.Width, pictureBox1.Height));
            }
        }
    }
}
