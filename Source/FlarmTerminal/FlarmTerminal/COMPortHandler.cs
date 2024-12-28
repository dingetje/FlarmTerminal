using RJCP.IO.Ports;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Parity = RJCP.IO.Ports.Parity;
using StopBits = RJCP.IO.Ports.StopBits;
using static SerialPortSearcher;
using System.Diagnostics;

namespace FlarmTerminal
{
#nullable enable
    [SupportedOSPlatform("windows")]
    internal class COMPortHandler : IDisposable
    {
        SerialPortStream? _serialPortStream = null!;
        private TaskQueue<string>? _taskQueue = null!;
        private string _portName = "COM1";
        private int _baudRate = 19200;
        private int _dataBits = 8;
        private Parity _parity = Parity.None;
        private StopBits _stopBits = StopBits.One;
        private bool _isClosing = false;
        private MainForm? _mainForm = null!;
        private string _lastData = string.Empty;

        public bool IsConnected { 
            get 
            {
                if (_serialPortStream != null)
                {
                    return _serialPortStream.IsOpen;
                }
                return false;
            } 
        }

        public void Dispose()
        {
            Disconnect();
        }

        public COMPortHandler(MainForm form, string PortName, int BaudRate = 19200, int DataBits = 8, Parity p = Parity.None, StopBits sb = StopBits.One)
        {
            _mainForm = form;
            _portName = PortName;
            _baudRate = BaudRate;
            _dataBits = DataBits;
            _parity = p;
            _stopBits = sb;
        }

        public void Connect()
        {
            if (_serialPortStream == null)
            {
                _serialPortStream = new SerialPortStream(_portName,
                                Properties.Settings.Default.BaudRate,
                                Properties.Settings.Default.DataBits,
                                _parity,
                                _stopBits);
                // FLARM data is terminated with \r\n
                _serialPortStream.NewLine = "\r\n";
                _serialPortStream.Open();
                _serialPortStream.DataReceived += _serialPort_DataReceived;
                _isClosing = false;
                _taskQueue = new TaskQueue<string>(1, _mainForm);
            }
        }

        public void Disconnect()
        {
            if (_serialPortStream != null)
            {
                _isClosing = true;
                _serialPortStream.DataReceived -= _serialPort_DataReceived;
                Thread.Sleep(100);
                _taskQueue?.Dispose();
                _serialPortStream.Close();
                _serialPortStream.Dispose();
                _serialPortStream = null;
            }
        }

        public void Send(string data)
        {
            if (_serialPortStream != null)
            {
                _serialPortStream.Write(data);
            }
        }

        private void _serialPort_DataReceived(object sender,RJCP.IO.Ports.SerialDataReceivedEventArgs e)
        {
            /*
             * Typical IGC Red Box startup:
             * 
             * 
             * Selftest start
             * 
             * Startup: normal
             * o.k. 32 Mbit internal FLASH memory
             * o.k.Obstacles Init
             * o.k.Logging Init
             * o.k.RF subsystem
             * o.k.Pressure sensor
             * o.k.GPS configuration
             * o.k.Serial number
             * no SD Card detected
             * $PFLAE,A,0,0*33
             * HW LXN-Flarm-IGC, #0000003500, 0xDDD7E4, SW v7.22/1.23 Build 6076cde9d
            */
            if (_isClosing)
            {
                return;
            }
            if (_serialPortStream == null)
            {
                return;
            }
            if (_serialPortStream.BytesToRead > 0)
            {
                try
                {
                    // read all available date
                    var newData = _serialPortStream?.ReadExisting();
                    _lastData += newData;
                    if (_lastData.EndsWith("\r\n"))
                    {
                        if (_mainForm!.IsPaused)
                        {
                            return;
                        }
                        _taskQueue?.EnqueueTask(_lastData);
                        _lastData = string.Empty;
                    }
                }
                catch (Exception ex)
                {
                    _mainForm.WriteToTerminal(ex.Message);
                }
            }
        }
        
        private string ParityToChar()
        {
            switch (_parity)
            {
                case Parity.None:
                    return "N";
                case Parity.Odd:
                    return "O";
                case Parity.Even:
                    return "E";
                case Parity.Mark:
                    return "M";
                case Parity.Space:
                    return "S";
                default:
                    return "N";
            }
        }

        private string StopBitsToChar()
        {
            switch (_stopBits)
            {
                case StopBits.One:
                    return "1";
                case StopBits.Two:
                    return "2";
                default:
                    return "1";
            }
        }
        internal string? GetPortProperties()
        {
            return _baudRate + "," + _dataBits + ParityToChar() + StopBitsToChar();
        }
    }
}
