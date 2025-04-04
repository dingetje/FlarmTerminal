using ScottPlot;
using ScottPlot.Plottables;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FontStyle = System.Drawing.FontStyle;

namespace FlarmTerminal.GUI
{
    [SupportedOSPlatform("windows")]
    public partial class CARPRadarPlot : Form
    {
        private MainForm? _mainForm = null!;
        private Radar? _radar = null;

        private bool _belowSafeRange = false;

        public CARPRadarPlot(MainForm? m)
        {
            _mainForm = m;
            InitializeComponent();
            double[,] values = {
                { 3000,3000,3000,3000,3000,3000,3000,3000,3000,3000,3000,3000,3000,3000,3000,3000,3000,3000,3000,3000 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 },
                { 0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0 }
                };

            var arrayIndex = 1;
            foreach (var antenna in new char[] { 'A', 'B' })
            {
                var rangeDoubles = _mainForm?.GetCARPData(antenna);
                if (rangeDoubles != null && rangeDoubles.Count == 20)
                {
                    for (int i = 0; i < 20; i++)
                    {
                        values[arrayIndex, i] = rangeDoubles[i];
                        if (rangeDoubles[i] < 3000)
                        {
                            _belowSafeRange = true;
                        }
                    }
                    arrayIndex++;
                }
            }

            _radar = formsPlot1.Plot.Add.Radar(values);

            _radar.Series[0].FillColor = Colors.Transparent;
            _radar.Series[0].LineColor = Colors.Blue;
            _radar.Series[0].LineWidth = 3;
            _radar.Series[0].LinePattern = LinePattern.DenselyDashed;

            _radar.Series[0].LegendText = "Minimum";
            _radar.Series[1].LegendText = "Antenna A";
            _radar.Series[2].LegendText = "Antenna B";

            _radar.PolarAxis.StraightLines = true;

            double[] tickPositions = { 1000, 2000, 3000, 5000, 7000, 10000 };
            string[] tickLabels = tickPositions.Select(x => (x/1000.0).ToString() + " km").ToArray();
            _radar.PolarAxis.SetCircles(tickPositions, tickLabels);

            formsPlot1.Refresh();

            // Initialize print components
            printDocument1 = new PrintDocument();
            printDocument1.PrintPage += PrintDocument1_PrintPage;

            printDialog1 = new PrintDialog();
            printDialog1.Document = printDocument1;

            printPreviewDialog1 = new PrintPreviewDialog();
            printPreviewDialog1.Document = printDocument1;

            groupBoxRemarks.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBoxRemarks_Paint);
            groupBoxDevice.Paint += new System.Windows.Forms.PaintEventHandler(this.groupBoxDevice_Paint);
        }

        private void groupBoxDevice_Paint(object sender, PaintEventArgs e)
        {
            var deviceText = "Flight recorder:      ";
            if (_mainForm != null && _mainForm.DeviceProperties.ContainsKey("Device Type"))
            {
                deviceText += _mainForm?.DeviceProperties["Device Type"];
            }
            // Define the font and brush
            var font = new Font("Segoe UI", 10);
            var brush = Brushes.Black;

            // Define the location to draw the string
            var point = new PointF(10, 25); // Adjust the coordinates as needed

            // Draw the string
            e.Graphics.DrawString(deviceText, font, brush, point);
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void groupBoxRemarks_Paint(object sender, PaintEventArgs e)
        {
            if (_belowSafeRange)
            {
                var remarksText = "Recorded range of the combined antennas is lower than the minimum safe range. Check your installation!";

                // Define the font and brush
                var font = new Font("Segoe UI", 10, FontStyle.Bold);
                var brush = Brushes.Red;

                // Define the location to draw the string
                var point = new PointF(10, 25); // Adjust the coordinates as needed

                // Draw the string
                e.Graphics.DrawString(remarksText, font, brush, point);
            }

            var carpTime = _mainForm?.CarpDateTime;
            if (carpTime.startTime != DateTime.MinValue && carpTime.endTime != DateTime.MaxValue)
            {
                var startTimeUtc = carpTime.startTime.ToUniversalTime().ToString("u");
                var endTimeUtc = carpTime.endTime.ToUniversalTime().ToString("u");
                var carpTimeLabel = $"Plot is based on CARP data collected between {startTimeUtc} UTC and {endTimeUtc} UTC";
                var font = new Font("Segoe UI", 10);
                var brush = Brushes.Black;

                // Define the location to draw the string
                var point = new PointF(10, 50); // Adjust the coordinates as needed

                // Draw the string
                e.Graphics.DrawString(carpTimeLabel, font, brush, point);
            }

            if (_mainForm?.CarpPoints != 0)
            {
                var carpPointText = $"{_mainForm?.CarpPoints.ToString()} points saved: ";
                if (_mainForm.CarpPoints < 1000)
                {
                    carpPointText += "this is a low number of points. Range may not be representative";
                }
                else
                {
                    carpPointText += "range should be representative";
                }
                var font = new Font("Segoe UI", 10);
                var brush = Brushes.Black;

                // Define the location to draw the string
                var point = new PointF(10, 80); // Adjust the coordinates as needed

                // Draw the string
                e.Graphics.DrawString(carpPointText, font, brush, point);
            }
        }

        private void PrintDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Define the margins (in hundredths of an inch)
            int marginLeft = 50; // .5 inch
            int marginTop = 50; // .5 inch
            int marginRight = 50; // .5 inch
            int marginBottom = 50; // .5 inch

            // Calculate the printable area
            int printableWidth = e.PageBounds.Width - marginLeft - marginRight;
            int printableHeight = e.PageBounds.Height - marginTop - marginBottom;

            // Calculate the scaling factor to fit the bitmap to the printable area
            float scaleX = (float)printableWidth / memoryImage.Width;
            float scaleY = (float)printableHeight / memoryImage.Height;
            float scale = Math.Min(scaleX, scaleY);

            // Calculate the new width and height based on the scaling factor
            int newWidth = (int)(memoryImage.Width * scale);
            int newHeight = (int)(memoryImage.Height * scale);

            // Calculate the position to center the image within the printable area
            int posX = marginLeft + (printableWidth - newWidth) / 2;
            int posY = marginTop + (printableHeight - newHeight) / 2;

            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            e.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
            e.Graphics.DrawImage(memoryImage, posX, posY, newWidth, newHeight);
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            // Create a high-resolution bitmap with the size of the form
            int width = this.Width;
            int height = this.Height;
            int dpi = 600; // Set the desired DPI for printing
            memoryImage = new Bitmap(width, height);
            memoryImage.SetResolution(dpi, dpi);

            // Draw the form to the high-resolution bitmap
            using (Graphics g = Graphics.FromImage(memoryImage))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                this.DrawToBitmap(memoryImage, new System.Drawing.Rectangle(0, 0, width, height));
            }

            // Show the print dialog
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                // Show the print preview dialog
                printPreviewDialog1.ShowDialog();
            }
        }
        private Bitmap memoryImage;
        private PrintDocument printDocument1;
        private PrintDialog printDialog1;
        private PrintPreviewDialog printPreviewDialog1;

    }
}
