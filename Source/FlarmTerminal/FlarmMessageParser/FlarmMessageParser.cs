﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Runtime.Versioning;

namespace FlarmTerminal
{
    [SupportedOSPlatform("windows")]
    public class FlarmMessagesParser
    {
        private object locker = new object();
        private bool _isIGC = false;
        private bool _isDualPort = false;

        // IGC capable?
        public bool IsIGC => _isIGC;
        public bool IsDualPort => _isDualPort;

        public class FlarmPropData : EventArgs
        {
            public string? Key { get; set; }
            public string? Value { get; set; }
        }

        public event EventHandler<FlarmPropData> NewPropertiesData;
        public event EventHandler<bool> IGCEnabledDetected;
        public event EventHandler<bool> DualPortDetected;

        private string StripChecksum(string newData)
        {
            if (newData.Contains("*"))
            {
                var items = newData.Split('*');
                if (items != null && items.Length > 0)
                {
                    return items[0];
                }
            }
            return newData;
        }

        private void WriteProperties(string key, string value)
        {
            OnNewPropertiesData(new FlarmPropData { Key = key, Value = value });
        }

        private void OnNewPropertiesData(FlarmPropData e)
        {
            // emit event to GUI
            NewPropertiesData?.Invoke(this, e);
        }

        private void OnIsIGCEnabled(bool e)
        {
            // emit event to GUI
            IGCEnabledDetected?.Invoke(this, e);
        }

        private void OnIsDualPort(bool e)
        {
            // emit event to GUI
            DualPortDetected?.Invoke(this, e);
        }

        public void ParseProperties(string newData)
        {
            // answer to a property request?
            if (newData.StartsWith("$PFLAC,A,"))
            {
                lock (locker)
                {
                    string tmp = newData.Clone().ToString();
                    var items = tmp.Split(',');
                    if (items != null && items.Length > 3)
                    {
                        try
                        {
                            var key = items[2];
                            var value = StripChecksum(items[3].Replace("\r\n", ""));
                            // default use raw key as property name
                            var propertyName = key;
                            // but if found in the enum, use the human friendly name
                            if (Enum.TryParse(key, out FlarmProperties.ConfigurationItems configItem))
                            {
                                propertyName = FlarmProperties.GetConfigName(configItem);
                            }
                            switch (key)
                            {
                                case "ACFT":
                                    switch (value)
                                    {
                                        case "1":
                                            WriteProperties(propertyName, "Glider/Motor Glider");
                                            break;
                                        case "2":
                                            WriteProperties(propertyName, "Tow Plane");
                                            break;
                                        case "3":
                                            WriteProperties(propertyName, "Helicopter/Rotorcraft");
                                            break;
                                        case "4":
                                            WriteProperties(propertyName, "Parachute");
                                            break;
                                        case "5":
                                            WriteProperties(propertyName, "Drop plane for skydivers");
                                            break;
                                        case "6":
                                            WriteProperties(propertyName, "Hang glider");
                                            break;
                                        case "7":
                                            WriteProperties(propertyName, "Para-glider");
                                            break;
                                        case "8":
                                            WriteProperties(propertyName, "aircraft with reciprocating engine(s)");
                                            break;
                                        case "9":
                                            WriteProperties(propertyName, "aircraft with jet/turboprop engine(s)");
                                            break;
                                        case "11":
                                            WriteProperties(propertyName, "Balloon");
                                            break;
                                        case "12":
                                            WriteProperties(propertyName, "Airship");
                                            break;
                                        case "13":
                                            WriteProperties(propertyName, "unmanned aerial vehicle (UAV)");
                                            break;
                                        case "10":
                                        case "14":
                                            WriteProperties(propertyName, "Reserved, do not use!");
                                            break;
                                        case "15":
                                            WriteProperties(propertyName, "Static Object");
                                            break;
                                        default:
                                            WriteProperties(propertyName, $"Not supported (type={value})");
                                            break;
                                    }
                                    break;
                                case "CAP": // device capabilities
                                    if (value.Contains(";"))
                                    {
                                        var capabilities = value.Split(';');
                                        var capList = new List<string>();
                                        foreach (var cap in capabilities)
                                        {
                                            switch (cap)
                                            {
                                                case "ADSR":
                                                    capList.Add("ADS-R/TIS-B reception functionality)");
                                                    break;
                                                case "AUD":
                                                    capList.Add("Audio output connection");
                                                    break;
                                                case "AZN":
                                                    capList.Add("Alert Zone Generator");
                                                    break;
                                                case "BARO":
                                                    capList.Add("Barometric sensor");
                                                    break;
                                                case "BAT":
                                                    capList.Add("Battery compartment or built-in batteries");
                                                    break;
                                                case "DLED":
                                                    capList.Add("The device has one or more LEDs");
                                                    break;
                                                case "DP2":
                                                    _isDualPort = true;
                                                    OnIsDualPort(true);
                                                    capList.Add("Second Data Port");
                                                    break;
                                                case "ENL":
                                                    capList.Add("Engine noise level sensor");
                                                    break;
                                                case "GND":
                                                    capList.Add("The device operates as a receive-only ground station");
                                                    break;
                                                case "IGC":
                                                    _isIGC = true;
                                                    capList.Add("The device is or can be IGC approved");
                                                    OnIsIGCEnabled(true);
                                                    break;
                                                case "OBST":
                                                    capList.Add("The device can issue obstacle collision warnings if a database is installed and the license is valid");
                                                    break;
                                                case "RFB":
                                                    capList.Add("Second radio transceiver for antenna diversity");
                                                    break;
                                                case "SD":
                                                    capList.Add("Slot for SD card");
                                                    break;
                                                case "TIS":
                                                    capList.Add("Interface for Garmin TIS protocol");
                                                    break;
                                                case "UI":
                                                    capList.Add("Built-in user interface (display, possibly button/knob)");
                                                    break;
                                                case "USBH":
                                                    capList.Add("Slot for USB stick");
                                                    break;
                                                case "XPDR":
                                                    capList.Add("SSR transponder/ADS-B receiver");
                                                    break;
                                            }
                                        }
                                        if (capList.Count > 0)
                                        {
                                            var capKey = "Capabilities";
                                            foreach (var cap in capList)
                                            {
                                                WriteProperties(capKey, cap);
                                                capKey = "";
                                            }
                                        }
                                        else
                                        {
                                            WriteProperties(propertyName, value);
                                        }
                                    }
                                    else
                                    {
                                        WriteProperties(propertyName, value);
                                    }
                                    break;
                                case "LOGINT":
                                    WriteProperties(propertyName, value + " SEC.");
                                    break;
                                case "DEVTYPE":
                                    WriteProperties(propertyName, value);
                                    break;
                                case "REGION":
                                    WriteProperties(propertyName, value);
                                    break;
                                case "DEVICEID":
                                case "ID":
                                    WriteProperties(propertyName, value);
                                    break;
                                case "NOTRACK":
                                case "PRIV":
                                    WriteProperties(propertyName, value == "0" ? "Disabled" : "Enabled");
                                    break;
                                case "SWVER":
                                    WriteProperties(propertyName, value);
                                    break;
                                case "FLARMVER":
                                    WriteProperties(propertyName, value);
                                    break;
                                case "BUILD":
                                    WriteProperties(propertyName, value);
                                    break;
                                case "SER":
                                    WriteProperties(propertyName, value);
                                    break;
                                case "SWEXP":
                                    WriteProperties(propertyName, value);
                                    break;
                                case "RADIOID":
                                    if (items.Length > 4)
                                    {
                                        var radioId = StripChecksum(items[4].Replace("\r\n", ""));
                                        switch (value)
                                        {
                                            case "1":
                                                WriteProperties(propertyName, "ICAO=" + radioId);
                                                break;
                                            default:
                                            case "2":
                                                WriteProperties(propertyName, "FLARMID=" + radioId);
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        WriteProperties(propertyName, value);
                                    }
                                    break;
                                case "BAUD":
                                case "BAUD1":
                                case "BAUD2":
                                    switch (value)
                                    {
                                        case "0":
                                            WriteProperties(propertyName, "4800");
                                            break;
                                        case "1":
                                            WriteProperties(propertyName, "9600");
                                            break;
                                        case "2":
                                            WriteProperties(propertyName, "19200");
                                            break;
                                        case "3":
                                            WriteProperties(propertyName, "28800");
                                            break;
                                        case "4":
                                            WriteProperties(propertyName, "38400");
                                            break;
                                        case "5":
                                            WriteProperties(propertyName, "57600");
                                            break;
                                        case "6":
                                            WriteProperties(propertyName, "115200");
                                            break;
                                        case "7":
                                            WriteProperties(propertyName, "230400");
                                            break;
                                        default:
                                            WriteProperties(propertyName, value);
                                            break;
                                    }
                                    break;
                                case "NMEAOUT":
                                case "NMEAOUT1":
                                case "NMEAOUT2":
                                    {
                                        int protocol = 0;
                                        string protocolVersion = "";
                                        if (int.TryParse(value, out protocol))
                                        {
                                            if (protocol > 5)
                                            {
                                                if (protocol >= 40 && protocol <= 44)
                                                {
                                                    protocolVersion = "Protocol Version 4, ";
                                                    protocol -= 40;
                                                }
                                                else if (protocol >= 60 && protocol <= 64)
                                                {
                                                    protocolVersion = "Protocol Version 6, ";
                                                    protocol -= 60;
                                                }
                                                else if (protocol >= 70 && protocol <= 74)
                                                {
                                                    protocolVersion = "Protocol Version 7, ";
                                                    protocol -= 70;
                                                }
                                                else if (protocol >= 80 && protocol <= 84)
                                                {
                                                    protocolVersion = "Protocol Version 8, ";
                                                    protocol -= 80;
                                                }
                                                else if (protocol >= 90 && protocol <= 94)
                                                {
                                                    protocolVersion = "Protocol Version 9, ";
                                                    protocol -= 90;
                                                }
                                            }
                                            WriteProperties(propertyName, protocolVersion);
                                            switch (protocol)
                                            {
                                                case 0:
                                                    WriteProperties("", "No Output");
                                                    break;
                                                case 1:
                                                    WriteProperties("", "GPRMC, GPGGA, GPGSA, plus FLARM proprietary (incl. PGRMZ)");
                                                    break;
                                                case 2:
                                                    WriteProperties("", "Only GPRMC, GPGGA, GPGSA");
                                                    break;
                                                case 3:
                                                    WriteProperties("", "Only FLARM-proprietary (incl. PGRMZ), no GPRMC, GPGGA, GPGSA");

                                                    break;
                                                case 4:
                                                    WriteProperties("", "Garmin TIS");
                                                    break;
                                                case 5:
                                                    WriteProperties("", "GDL90 Protocol");
                                                    break;
                                                default:
                                                    WriteProperties("", $"Unknown {value}");
                                                    break;
                                            }
                                        }
                                        else
                                        {
                                            WriteProperties(propertyName, value);
                                        }
                                    }
                                    break;
                                case "THRE":
                                    WriteProperties(propertyName, value + " m/s");
                                    break;
                                default:
                                    WriteProperties(propertyName, value);
                                    break;
                            }
                        }
                        catch (Exception)
                        {
                        }
                    }
                }
            }
        }
    }
}
