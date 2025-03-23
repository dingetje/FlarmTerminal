using ScottPlot;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            radar.Series[1].LegendText = "Antenne A";
            radar.Series[2].LegendText = "Antenne B";

            radar.PolarAxis.StraightLines = true;

            double[] tickPositions = { 1000, 2000, 3000, 5000 };
            string[] tickLabels = tickPositions.Select(x => x.ToString() + " m").ToArray();
            radar.PolarAxis.SetCircles(tickPositions, tickLabels);

            formsPlot1.Refresh();
        }

        private void buttonOK_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
