using Newtonsoft.Json.Linq;
using UCNLNMEA;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
using System.Security.Claims;
using System.Security.Policy;
using FlarmTerminal.GUI;
using System.Windows.Forms;
using System.Diagnostics;

namespace FlarmTerminal
{
    public struct SatelliteData
    {
        public int PRNNumber;
        public int Elevation;
        public int Azimuth;
        public int SNR;

        public override string ToString()
        {
            return string.Format("PRN: {0:00}, Elevation: {1:00}°, Azimuth: {2:000}°, SNR: {3:00} dB", PRNNumber, Elevation, Azimuth, SNR);
        }

        public SatelliteData(int prn, int elevation, int azimuth, int snr)
        {
            PRNNumber = prn;
            Elevation = elevation;
            Azimuth = azimuth;
            SNR = snr;
        }
    }

    internal class ProcessMessages
    {
        private delegate void ProcessCommandDelegate(object[] parameters);

        private Dictionary<UCNLNMEA.SentenceIdentifiers, ProcessCommandDelegate> commandProcessor;

        public delegate void NewFixHandler(DateTime fixTime, GeographicDimension latitude,
            GeographicDimension longitude);

        public NewFixHandler NewFix;

        public delegate void GroundSpeedMeasurementHandler(double trackTrue, double trackMagnetic, double speedKnots,
            string skUnits, double speedKmh, string sKmUnits);

        public GroundSpeedMeasurementHandler GroundSpeedMeashurement;

        public delegate void SatellitesInfoUpdatedHandler(SatelliteData[] satellites);

        public SatellitesInfoUpdatedHandler SatellitesInfoUpdated;

        public delegate void GPSTextReceivedHandler(string text);

        public GPSTextReceivedHandler GPSTextReceived = null;

        delegate T NullChecker<T>(object parameter);

        NullChecker<int> intNullChecker = (x => x == null ? -1 : (int)x);
        NullChecker<double> dobleNullChecker = (x => x == null ? double.NaN : (double)x);
        NullChecker<string> stringNullChecker = (x => x == null ? string.Empty : (string)x);

        public delegate void GlobalGPSDataHandler(DateTime date,
            string GPSQuality, int usedSatellitesNumber,
            double precisionHorizontalDilution, double antennaAltitude, string altitudeUnits,
            double geoidalSeparation, string geoidalSeparationUnits,
            int differentialReferenceStation);

        // update four LEDs on the FLARM device for RX,TX, GPS, Power
        public delegate void LedUpdate(bool[] on);

        public LedUpdate FLARMLedStatusRecieved = null;

        // update for upper and lower LEDs
        public delegate void LedUpdateAboveLower(bool[] on, bool Alarm);

        public LedUpdateAboveLower FLARMLedAboveBelowStatusRecieved;

        public delegate void LedDirection(bool[] on, int AlarmLevel);

        public LedDirection FLARMLedDirectionRecieved = null;

        public GlobalGPSDataHandler GlobalGPSData = null;
        List<SatelliteData> satellites;

        private Dictionary<char,List<Int32>> _carpCnt = new Dictionary<char, List<Int32>>()
        {
            { 'A', new List<Int32>() },
            { 'B', new List<Int32>() }
        };
        private Dictionary<char, List<double>> _carpRange = new Dictionary<char, List<Double>>()
        {
            { 'A', new List<Double>() },
            { 'B', new List<Double>() }
        };

        public delegate void CARPDataReceived(char antenna, double[] rangeDoubles);
        public CARPDataReceived FLARMCARPDataReceived = null;

        public delegate void CARPTimeSpan(DateTime startTime, DateTime endTime);
        public CARPTimeSpan FLARMCARPTimeSpanReceived = null;

        public delegate void CARPPointStats(long pointCount);
        public CARPPointStats FLARMCARPPoints = null;

        public ProcessMessages()
        {
            commandProcessor = new Dictionary<SentenceIdentifiers, ProcessCommandDelegate>()
            {
                { UCNLNMEA.SentenceIdentifiers.GGA, new ProcessCommandDelegate(ProcessGGA) },
                { UCNLNMEA.SentenceIdentifiers.GSV, new ProcessCommandDelegate(ProcessGSV) },
                { UCNLNMEA.SentenceIdentifiers.GLL, new ProcessCommandDelegate(ProcessGLL) },
                { UCNLNMEA.SentenceIdentifiers.RMC, new ProcessCommandDelegate(ProcessRMC) },
//                { NMEA.SentenceIdentifiers.VTG, new ProcessCommandDelegate(ProcessVTG)},
//                { NMEA.SentenceIdentifiers.TXT, new ProcessCommandDelegate(ProcessTXT)}
            };
        }

        public void Process(NMEAStandartSentence nmea)
        {
            try
            {
                if (commandProcessor.ContainsKey(nmea.SentenceID))
                {
                    commandProcessor[nmea.SentenceID](nmea.parameters);
                }
            }
            catch
            {
                //
            }
        }

        public void Process(NMEAProprietarySentence nmea)
        {
            try
            {
                switch (nmea.Manufacturer)
                {
                    case ManufacturerCodes.FLA:
                        switch (nmea.SentenceIDString)
                        {
                            case "U":
                                ProcessFLAU(nmea.parameters);
                                break;
                            case "N":
                                ProcessFLAN(nmea.parameters);
                                break;
                        }

                        break;
                }

            }
            catch
            {
                //
            }
        }

        private void ProcessGGA(object[] parameters)
        {
            try
            {
                DateTime date = DateTime.UtcNow;
                if (parameters[0] != null)
                {
                    date = (DateTime)parameters[0];
                }

                var gpsQualityIndicator = (string)parameters[5];
                var satellitesInUse = intNullChecker(parameters[6]);
                var precisionHorizontalDilution = dobleNullChecker(parameters[7]);
                var antennaAltitude = dobleNullChecker(parameters[8]);
                var antennaAltitudeUnits = (string)parameters[9];
                var geoidalSeparation = dobleNullChecker(parameters[10]);
                var geoidalSeparationUnits = (string)parameters[11];
                var differentialReferenceStation = intNullChecker(parameters[12]);

                if (GlobalGPSData != null)
                {
                    GlobalGPSData(date, gpsQualityIndicator, satellitesInUse, precisionHorizontalDilution,
                        antennaAltitude, antennaAltitudeUnits,
                        geoidalSeparation, geoidalSeparationUnits, differentialReferenceStation);
                }
            }
            catch
            {
                //
            }
        }

        private void ProcessRMC(object[] parameters)
        {
            // not yet used
        }

        private void ProcessGSV(object[] paramters)
        {
            try
            {
                int totalMessages = (int)paramters[0];
                int currentMessageNumber = (int)paramters[1];

                if (currentMessageNumber == 1)
                {
                    satellites.Clear();
                }

                int satellitesDataItemsCount = (paramters.Length - 3) / 4;

                for (int i = 0; i < satellitesDataItemsCount; i++)
                {
                    satellites.Add(
                        new SatelliteData(
                            intNullChecker(paramters[3 + 4 * i]),
                            intNullChecker(paramters[4 + 4 * i]),
                            intNullChecker(paramters[5 + 4 * i]),
                            intNullChecker(paramters[6 + 4 * i])));
                }

                if (currentMessageNumber == totalMessages)
                {
                    if (SatellitesInfoUpdated != null)
                    {
                        SatellitesInfoUpdated(satellites.ToArray());
                    }
                }
            }
            catch
            {
                //
            }
        }

        private void ProcessGLL(object[] parameters)
        {
            try
            {
                if (parameters[5].ToString() == "Valid")
                {
                    if (NewFix != null)
                    {
                        NewFix((DateTime)parameters[4],
                            new GeographicDimension((double)parameters[0],
                                (Cardinals)Enum.Parse(typeof(Cardinals), (string)parameters[1])),
                            new GeographicDimension((double)parameters[2],
                                (Cardinals)Enum.Parse(typeof(Cardinals), (string)parameters[3])));
                    }
                }
            }
            catch
            {
                //
            }
        }

        /// <summary>
        /// PFLAU,<RX>,<TX>,<GPS>,<Power>,<AlarmLevel>,<RelativeBearing>,<AlarmType>,<RelativeVertical>,<RelativeDistance>[,<ID>]
        /// [0] RX    0..99                  number of planes in range
        /// [1] TX    0..1                   sending or not
        /// [2] Power 0..1                   on or off
        /// [3] GPS   0..3                   0 = no GPS, 1 = GPS, 2 = DGPS, 3 = GPS+DGPS
        /// [4] AlarmLevel   0..3            0 = no alarm, 1 = alarm, 2 = warning, 3 = emergency
        /// [5] RelativeBearing -180..180    Decimal integer value. Range: -180 to 180.
        ///                                     Relative bearing in degrees from true ground track to the
        ///                                     intruder’s position.Positive values are clockwise. 
        ///                                     0° indicates that the object is exactly ahead. The field is
        ///                                     empty for non-directional targets or when no aircraft are
        ///                                     within range.For obstacle alarm and Alert Zone alarm,
        ///                                     this field is 0
        /// [6] AlarmType 00..FF             Type of alarm as assessed by FLARM
        ///                                     0 = no aircraft within range or no-alarm traffic
        ///                                         information
        ///                                     2 = aircraft alarm
        ///                                     3 = obstacle/Alert Zone alarm(if data port version
        ///                                         < 7, otherwise only obstacle alarms are indicated by
        ///                                         <AlarmType> = 3)
        ///                                     4 = traffic advisory(sent once each time an aircraft
        ///                                         enters within distance 1.5 km and vertical distance
        ///                                         300 m from own ship; when data port version >=4)
        ///                                     xx = Alert Zone alarm(see comment below)
        ///                                          When data port version >=7, the type of Alert Zone is
        ///                                          sent as <AlarmType> in the range 10..FF.Refer to the
        ///                                          <ZoneType> parameter in the PFLAO sentence for a
        ///                                          description.
        /// [7] RelativeVertical            Decimal integer value. Range: from -32768 to 32767.
        ///                                 Relative vertical separation in meters above own
        ///                                 position.Negative values indicate that the other aircraft
        ///                                 or obstacle is lower.The field is empty when no aircraft
        ///                                 are within range.
        ///                                 For Alert Zone and obstacle warnings, this field is 0
        /// [8] RelativeDistance            Decimal integer value. Range: from 0 to 2147483647.
        ///                                 Relative horizontal distance in meters to the target or
        ///                                 obstacle.For non-directional targets, this value is
        ///                                 estimated based on signal strength.
        ///                                 The field is empty when no aircraft are within range and
        ///                                 no alarms are generated.
        ///                                 For Alert Zone, this field is 0
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        private void ProcessFLAU(object[] parameters)
        {
            try
            {
                bool Alarm = (int)parameters[4] > 0;
                // delegate used?
                if (FLARMLedStatusRecieved != null)
                {
                    bool[] ledStatus = new bool[4];
                    for (int i = 0; i < 4; i++)
                    {
                        ledStatus[i] = (int)parameters[i] != 0;
                    }

                    FLARMLedStatusRecieved?.Invoke(ledStatus);
                }

                // all required data and delegate defined?
                if (FLARMLedAboveBelowStatusRecieved != null && parameters[6] != null && parameters[7] != null)
                {
                    bool[] ledStatus = new bool[2];
                    // Above
                    ledStatus[0] = (int)parameters[7] >= 0;
                    // Below
                    ledStatus[1] = !ledStatus[0];
                    FLARMLedAboveBelowStatusRecieved?.Invoke(ledStatus, Alarm);
                }

                if (FLARMLedDirectionRecieved != null)
                {
                    if (parameters[5] != null)
                    {
                        // determine which LEDs to light up
                        //          0°
                        //    [9] *    * [0]
                        //  [8] *    ^   * [1]
                        // [7] *  ---+--- * [2]
                        //  [6] *    +   * [3]
                        //    [5] *    * [4]
                        //
                        // bearing 0° is straight ahead
                        // but displayed on the FLARM display with the
                        // 0° LED on the left side, experimentally determined
                        // like this:
                        //
                        //     [< 0]  [< 37]
                        //  [< -35]      [< 73]
                        //[< -71]          [< 109]
                        //  [< -107]      [< 145]
                        //    [< -143] [> 145]


                        int RelativeBearing = (int)parameters[5];
                        int AlarmLevel = (int)parameters[4];
                        var ledStatus = new bool[10]; // default all false
                        if (RelativeBearing >= 0)
                        {
                            if (RelativeBearing < 37)
                            {
                                ledStatus[0] = true;
                            }
                            else if (RelativeBearing < 73)
                            {
                                ledStatus[1] = true;
                            }
                            else if (RelativeBearing < 109)
                            {
                                ledStatus[2] = true;
                            }
                            else if (RelativeBearing < 145)
                            {
                                ledStatus[3] = true;
                            }
                            else
                            {
                                ledStatus[4] = true;
                            }
                        }
                        else
                        {
                            if (RelativeBearing < -143)
                            {
                                ledStatus[5] = true;
                            }
                            else if (RelativeBearing < -107)
                            {
                                ledStatus[6] = true;
                            }
                            else if (RelativeBearing < -71)
                            {
                                ledStatus[7] = true;
                            }
                            else if (RelativeBearing < -35)
                            {
                                ledStatus[8] = true;
                            }
                            else
                            {
                                ledStatus[9] = true;
                            }
                        }

                        Debug.WriteLine("Relative Bearing: " + RelativeBearing);
                        Debug.WriteLine("LED Status: " + string.Join(",", ledStatus));
                        FLARMLedDirectionRecieved?.Invoke(ledStatus, AlarmLevel);
                    }
                }
            }
            catch
            {
                //
            }
        }

        /// <summary>
        /// PFLAN,A,RANGE
        ///
        /// This is the CARP range data for PowerFlarm only
        /// </summary>
        /// <param name="parameters"></param>
        /// <remarks>
        /// Example:
        /// $PFLAN,R,RANGE
        /// $PFLAN,A,RANGE,RFTOP,A,3637,4986,5080,4423,4311,4061,3733,4131,4743,4774,4330,4428,3299,3177,3477,2849,2632,2937,2552,2553*5E
        /// $PFLAN,A,RANGE,RFCNT,A,2460,2277,2119,1777,1571,1824,2438,2807,3365,4405,3134,2397,2118,1927,1957,1997,2070,1955,1796,2581*4A
        /// $PFLAN,A,RANGE,RFDEV,A,3342,4797,4638,3907,16704,13749,2910,3640,4232,4103,11965,16312,3058,3549,3640,2821,2587,15203,2561,2305*79
        /// $PFLAN,A,RANGE,RFTOP,B,2092,2258,1837,1754,2730,2525,3412,3190,3189,2728,1743,2464,2118,1825,1965,1807,1472,1587,1526,1576*57
        /// $PFLAN,A,RANGE,RFCNT,B,793,511,392,409,633,807,830,695,676,1184,1228,1339,1456,1697,1506,1347,1227,1149,1023,1172*79
        /// $PFLAN,A,RANGE,RFDEV,B,2465,2731,2246,2136,2737,2398,18519,3477,3991,3477,1813,2644,1909,1557,1886,1430,1287,1494,1676,1536*71
        /// $PFLAN,A,RANGE,STATS,67049*36
        /// $PFLAN,A,RANGE,TIMESPAN,1620905845,1632663140*7E
        /// $PFLAN,A,RANGE*4B
        ///
        /// RFTOP = mean range in meters per sector and channel.
        /// RFCNT = number of data points used to compute the mean range for each sector and channel.
        /// RFDEV = standard deviation of the data points in each sector per channel.
        /// Note: Each sector should be checked to have a sufficiently large RFCNT value to be significant
        /// </remarks>
        private void ProcessFLAN(object[] parameters)
        {
            try
            {
                if (parameters[0] != null)
                {
                    if (parameters.Length == 24 && (string)parameters[0] == "A" && (string)parameters[1] == "RANGE")
                    {
                        if ((string)parameters[2] == "RFTOP" || (string)parameters[2] == "RFCNT" || (string)parameters[2] == "RFDEV")
                        {
                            // A or B antenna
                            char antenna = parameters[3].ToString()[0];
                            switch ((string)parameters[2])
                            {
                                case "RFCNT":
                                    _carpCnt[antenna].Clear();
                                    var rangeDoubles = new double[20];
                                    for (int i = 0; i < 20; i++)
                                    {
                                        _carpCnt[antenna].Add(Convert.ToInt32(parameters[4 + i]));
                                        if(_carpCnt[antenna][i] > 20)
                                        {
                                            rangeDoubles[i] = _carpRange[antenna][i];
                                        }
                                        else
                                        {
                                            rangeDoubles[i] = 0;
                                        }
                                    }
                                    if (FLARMCARPDataReceived != null)
                                    {
                                        FLARMCARPDataReceived?.Invoke(antenna, rangeDoubles);
                                    }
                                    break;
                                case "RFTOP":
                                    _carpRange[antenna].Clear();
                                    for (int i = 0; i < 20; i++)
                                    {
                                        _carpRange[antenna].Add(Convert.ToDouble(parameters[4 + i]));
                                    }
                                    break;
                                case "RFDEV":
                                    break;
                            }
                        }
                    }
                    if (parameters.Length == 4 && (string)parameters[2] == "STATS")
                    {
                        if (FLARMCARPPoints != null)
                        {
                            var points = Convert.ToInt64(parameters[3]);
                            FLARMCARPPoints?.Invoke(points);
                        }
                    }
                    if (parameters.Length == 5 && (string)parameters[2] == "TIMESPAN")
                    {
                        if (FLARMCARPTimeSpanReceived != null)
                        {
                            // Convert UNIX timestamps to DateTime
                            var timestamp1 = Convert.ToInt64(parameters[3]);
                            var timestamp2 = Convert.ToInt64(parameters[4]);
                            var dateTimeStart = DateTimeOffset.FromUnixTimeSeconds(timestamp1).UtcDateTime;
                            var dateTimeEnd = DateTimeOffset.FromUnixTimeSeconds(timestamp2).UtcDateTime;

                            // Invoke the delegate with the times
                            FLARMCARPTimeSpanReceived?.Invoke(dateTimeStart,dateTimeEnd);
                        }
                    }
                }
            }
            catch
            {
                //
            }
        }
    }
}
