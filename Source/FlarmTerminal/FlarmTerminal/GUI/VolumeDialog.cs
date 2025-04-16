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
    public partial class VolumeDialog : Form
    {
        private int _volumeLevel = 50; // Default volume level

        // Event to notify when the volume level changes
        public event EventHandler<int>? VolumeLevelChanged;

        public VolumeDialog(int volumeLevel)
        {
            InitializeComponent();
            this._volumeLevel = volumeLevel;
            // Initialize controls
            numericUpDown1.Value = volumeLevel;
            trackBar1.Value = volumeLevel;
        }

        public int GetVolumeLevel()
        {
            return numericUpDown1.Value > 0 ? (int)numericUpDown1.Value : 0;
        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            // Synchronize TrackBar with NumericUpDown
            trackBar1.Value = (int)numericUpDown1.Value;
            _volumeLevel = (int)numericUpDown1.Value;
            pictureBox1.Invalidate(); // Redraw the PictureBox to show the cross if volume is 0
            // Raise the VolumeLevelChanged event
            VolumeLevelChanged?.Invoke(this, _volumeLevel);
        }

        private void trackBar1_ValueChanged(object sender, EventArgs e)
        {
            // Synchronize NumericUpDown with TrackBar
            numericUpDown1.Value = trackBar1.Value;
            _volumeLevel = (int)trackBar1.Value; 
            pictureBox1.Invalidate(); // Redraw the PictureBox to show the cross if volume is 0
            // Notify listeners
            VolumeLevelChanged?.Invoke(this, trackBar1.Value);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            // Load the default bitmap
            Bitmap defaultBitmap = Properties.Resources.icons8_volume_48;

            // Draw the default bitmap
            e.Graphics.DrawImage(defaultBitmap, 0, 0, pictureBox1.Width, pictureBox1.Height);

            // If volume is 0, draw a red cross over the bitmap
            if (_volumeLevel == 0)
            {
                using (Pen redPen = new Pen(Color.Red, 5))
                {
                    e.Graphics.DrawLine(redPen, 5, 5, pictureBox1.Width - 5, pictureBox1.Height - 5); // Diagonal line
                    e.Graphics.DrawLine(redPen, 5, pictureBox1.Height - 5, pictureBox1.Width - 5, 5); // Opposite diagonal line
                }
            }
        }
    }
}
