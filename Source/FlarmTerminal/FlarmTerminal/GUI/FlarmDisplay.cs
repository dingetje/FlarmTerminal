using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.Versioning;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;
using System.IO;
using System.Threading.Tasks;
using System.Media;
using System.Reflection;
using System.Threading;
using Microsoft.VisualBasic.Logging;
using NAudio.Wave;

namespace FlarmTerminal.GUI
{
    [SupportedOSPlatform("windows")]
    public partial class FlarmDisplay : Form
    {
        private Mutex _mutex = new Mutex();
        private WaveOutEvent _outputDevice = new WaveOutEvent();
        private object _lockOutputDevice = new();

        public enum LEDColor
        {
            Off,
            Green,
            Red,
        };
        public enum LEDNames
        {
            RX,
            GPS,
            TX,
            Power,
            Degree10,
            Degree45,
            Degree90,
            Degree135,
            Degree170,
            Degree190,
            Degree225,
            Degree270,
            Degree315,
            Degree350,
            Above,
            Below,
        };

        public class LED
        {
            public LEDNames Type { get; set; }
            public LEDColor Color { get; set; }
            public bool Blink { get; set; }

            public LED(LEDNames type, LEDColor color, bool blink)
            {
                Type = type;
                Color = color;
                Blink = blink;
            }
        }
        private MainForm _mainForm = null;

        private Dictionary<LEDNames, LED> _leds = new();
        private bool _dragging = false;
        private Point _offset;
        private Point _start_point = new Point(0, 0);

        // Create rectangle for RX LED
        private Rectangle rectRX = new Rectangle(29, 77, 13, 13);
        // Create rectangle for GPS LED
        private Rectangle rectGPS = new Rectangle(29, 121, 13, 13);
        // Create rectangle for TX LED
        private Rectangle rectTX = new Rectangle(29, 99, 13, 13);
        // Create rectangle for Power LED
        private Rectangle rectPower = new Rectangle(29, 143, 13, 13);

        private Rectangle rectAbove = new Rectangle(176, 22, 13, 13);

        private Rectangle rectBelow = new Rectangle(176, 147, 13, 13);

        private Rectangle rectDegree10 = new Rectangle(283, 21, 13, 13);
        private Rectangle rectDegree45 = new Rectangle(318, 46, 13, 13);
        private Rectangle rectDegree90 = new Rectangle(331, 84, 13, 13);
        private Rectangle rectDegree135 = new Rectangle(317, 123, 13, 13);
        private Rectangle rectDegree170 = new Rectangle(284, 147, 13, 13);
        private Rectangle rectDegree190 = new Rectangle(242, 147, 13, 13);
        private Rectangle rectDegree225 = new Rectangle(209, 123, 13, 13);
        private Rectangle rectDegree270 = new Rectangle(196, 84, 13, 13);
        private Rectangle rectDegree315 = new Rectangle(209, 46, 13, 13);
        private Rectangle rectDegree350 = new Rectangle(243, 22, 13, 13);

        private SolidBrush redBrush = new SolidBrush(Color.OrangeRed);
        private SolidBrush greenBrush = new SolidBrush(Color.LimeGreen);
        private SolidBrush blackBrush = new SolidBrush(Color.Black);

        private Timer _blinkTimer = new Timer();
        private bool _blink = false;
        private int _blinkInterval = 400;

        private Rectangle GetRect(LEDNames led)
        {
            switch (led)
            {
                case LEDNames.RX: return rectRX;
                case LEDNames.GPS: return rectGPS;
                case LEDNames.TX: return rectTX;
                case LEDNames.Power: return rectPower;
                case LEDNames.Degree10: return rectDegree10;
                case LEDNames.Degree45: return rectDegree45;
                case LEDNames.Degree90: return rectDegree90;
                case LEDNames.Degree135: return rectDegree135;
                case LEDNames.Degree170: return rectDegree170;
                case LEDNames.Degree190: return rectDegree190;
                case LEDNames.Degree225: return rectDegree225;
                case LEDNames.Degree270: return rectDegree270;
                case LEDNames.Degree315: return rectDegree315;
                case LEDNames.Degree350: return rectDegree350;
                case LEDNames.Above: return rectAbove;
                case LEDNames.Below: return rectBelow;
                default: return rectRX;
            }
        }

        internal static LEDNames? DirectionLedFromIndex(int index)
        {
            //    [9] *    * [0]
            //  [8] *    ^   * [1]
            // [7] *  ---+--- * [2]
            //  [6] *    +   * [3]
            //    [5] *    * [4]

            switch (index)
            {
                case 0: return LEDNames.Degree10;
                case 1: return LEDNames.Degree45;
                case 2: return LEDNames.Degree90;
                case 3: return LEDNames.Degree135;
                case 4: return LEDNames.Degree170;
                case 5: return LEDNames.Degree190;
                case 6: return LEDNames.Degree225;
                case 7: return LEDNames.Degree270;
                case 8: return LEDNames.Degree315;
                case 9: return LEDNames.Degree350;
            }
            return null;
        }

        public FlarmDisplay(MainForm mf)
        {
            _mainForm = mf;
            InitializeComponent();
            this.BackColor = Color.Black;
            this.TransparencyKey = Color.Black;

            foreach (LEDNames led in Enum.GetValues(typeof(LEDNames)))
            {
                _leds[led] = new LED(led, LEDColor.Off, false);
            }
            _blinkTimer = new Timer();
            _blinkTimer.Interval = 400;
            _blinkTimer.Tick += BlinkTimer_Tick;
            _blinkTimer.Enabled = false;
        }

        private void BlinkTimer_Tick(object sender, EventArgs e)
        {
            _blinkTimer.Interval = _blinkInterval;
            _blink = !_blink;
            pictureBox1.Invalidate();
            pictureBox1.Refresh();
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            // don't draw background
        }

        private void FlarmDisplay_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Hide();
            }
        }

        private void FlarmDisplay_MouseUp(object sender, MouseEventArgs e)
        {
        }

        private void FlarmDisplay_MouseMove(object sender, MouseEventArgs e)
        {
        }

        private void FlarmDisplay_MouseDown(object sender, MouseEventArgs e)
        {
        }

        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _dragging = true;
                _start_point = new Point(e.X, e.Y);
            }
        }

        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_dragging)
            {
                Point p = PointToScreen(e.Location);
                Location = new Point(p.X - this._start_point.X, p.Y - this._start_point.Y);
            }
//            else
//            {
//                _mainForm.ShowPosition(e.X, e.Y);
//            }
        }

        private void pictureBox1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                _dragging = false;
            }
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            foreach (KeyValuePair<LEDNames, LED> led in _leds)
            {
                switch (led.Value.Color)
                {
                    case LEDColor.Off:
                        break;
                    case LEDColor.Green:
                    case LEDColor.Red:
                    {
                        var draw = false;
                        if (led.Value.Blink)
                        {
                            if (_blink)
                            {
                                draw = true;
                            }
                        }
                        else
                        {
                            draw = true;
                        }

                        if (draw)
                        {
                            var rect = GetRect(led.Key);
                            var brush = led.Value.Color == LEDColor.Green ? greenBrush : redBrush;
                            e.Graphics.FillEllipse(brush, rect);
                        }
                    }
                    break;
                }
            }
        }

        internal void SetLED(LEDNames led, LEDColor col, bool blink)
        {
            _mutex.WaitOne();
            _leds[led].Blink = blink;
            _leds[led].Color = col;
            _mutex.ReleaseMutex();
        }

        internal async Task PlayBeep(int alarmLevel)
        {
            if (_mainForm != null && _mainForm.IsStopped())
            {
                return;
            }

            string fileName = "2000.wav";
            switch (alarmLevel)
            {
                default:
                case 1:
                    _blinkInterval = 400;
                    fileName = "2000.wav";
                    break;
                case 2:
                    _blinkInterval = 250;
                    fileName = "2000.wav";
                    break;
                case 3:
                    _blinkInterval = 150;
                    fileName = "2500.wav";
                    break;
            }
            try
            {
                if (File.Exists(fileName))
                {
                        //                        var player = new System.Media.SoundPlayer();
                        //                        player.Stream = File.OpenRead(fileName);
                        //                        player.Load();
                        //                        player.Play();
                        //                        Task.Delay(125).Wait();
                    var audioFile = new AudioFileReader(fileName);
                    if (_outputDevice.PlaybackState != PlaybackState.Stopped)
                    {
                        _outputDevice.Stop();
                    }

                    lock (_lockOutputDevice)
                    {
                        _outputDevice.Init(audioFile);
                        _outputDevice.Play();
                    }
                    while(_outputDevice.PlaybackState == PlaybackState.Playing)
                    {
                        Thread.Sleep(20);
                    }
                }
            }
            catch (Exception)
            {

            }
        }
        public void SetVolume(float volume)
        {
            // Ensure volume is between 0.0 (mute) and 1.0 (full volume)
            if (volume < 0.0f || volume > 1.0f)
            {
                return;
            }
            if (_outputDevice != null)
            {
                lock (_lockOutputDevice)
                {
                    _outputDevice.Volume = volume;
                }
            }
        }

        private void FlarmDisplay_VisibleChanged(object sender, EventArgs e)
        {
            if (e != null && e.GetType() == typeof(EventArgs))
            {
                _blinkTimer.Enabled = Visible;
            }
        }
    }
}
