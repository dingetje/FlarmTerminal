using ScottPlot;
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

namespace FlarmTerminal.GUI
{
    [SupportedOSPlatform("windows")]
    public partial class CARPRadarPlot : Form
    {
        public CARPRadarPlot()
        {
            InitializeComponent();
            double[,] values = {
                { 3000,3000,3000,3000,3000,3000,3000,3000,3000,3000,3000,3000,3000,3000,3000,3000,3000,3000,3000,3000 },
                { 3637,4986,5080,4423,4311,4061,3733,4131,4743,4774,4330,4428,3299,3177,3477,2849,2632,2937,2552,2553 },
                { 2092,2258,1837,1754,2730,2525,3412,3190,3189,2728,1743,2464,2118,1825,1965,1807,1472,1587,1526,1576 }
                };

            var radar = formsPlot1.Plot.Add.Radar(values);

            radar.Series[0].FillColor = Colors.Transparent;
            radar.Series[0].LineColor = Colors.Blue;
            radar.Series[0].LineWidth = 3;
            radar.Series[0].LinePattern = LinePattern.DenselyDashed;

            radar.Series[0].LegendText = "Minimum";
            radar.Series[1].LegendText = "Antenna A";
            radar.Series[2].LegendText = "Antenna B";

            radar.PolarAxis.StraightLines = true;

            double[] tickPositions = { 1000, 2000, 3000, 5000, 7000, 10000 };
            string[] tickLabels = tickPositions.Select(x => x.ToString() + " m").ToArray();
            radar.PolarAxis.SetCircles(tickPositions, tickLabels);

            formsPlot1.Refresh();

            // Initialize print components
            printDocument1 = new PrintDocument();
            printDocument1.PrintPage += PrintDocument1_PrintPage;

            printDialog1 = new PrintDialog();
            printDialog1.Document = printDocument1;

            printPreviewDialog1 = new PrintPreviewDialog();
            printPreviewDialog1.Document = printDocument1;
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void PrintDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Define the margins (in hundredths of an inch)
            int marginLeft = 100; // 1 inch
            int marginTop = 100; // 1 inch
            int marginRight = 100; // 1 inch
            int marginBottom = 100; // 1 inch

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

            // Draw the resized image with margins
            e.Graphics.DrawImage(memoryImage, posX, posY, newWidth, newHeight);
        }

        private void buttonPrint_Click(object sender, EventArgs e)
        {
            // Create a bitmap with the size of the form
            memoryImage = new Bitmap(this.Width, this.Height);

            // Draw the form to the bitmap
            this.DrawToBitmap(memoryImage, new Rectangle(0, 0, this.Width, this.Height));

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
