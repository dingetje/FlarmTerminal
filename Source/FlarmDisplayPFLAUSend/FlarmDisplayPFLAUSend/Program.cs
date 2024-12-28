using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RJCP.IO.Ports;

namespace FlarmDisplayPFLAUSend
{
    internal class Program
    {
        static byte CalcChecksum(string line)
        {
            byte checksum = 0;
            int startIndex = line.IndexOf('$') + 1;
            int endIndex = line.IndexOf('*');
            for (int i = startIndex; i < endIndex; i++)
            {
                checksum ^= (byte)line[i];
            }
            return checksum;
        }

        static void SendPFLAU(SerialPortStream sps, int degrees,int alarmlevel, int alarmtype, FileStream f)
        {
            // RX,TX,GPS,Power,AlarmLevel,Degrees,AlarmType,Above/Below,Distance,Checksum
            //
            // RX
            // Decimal integer value.Range: from 0 to 99.
            // Number of devices with unique IDs currently received
            // regardless of the horizontal or vertical separation.
            // Because the processing might be based on extrapolated
            // historical data, < Rx > might be lower than the number of
            // aircraft in range, i.e.there might be other traffic around
            // (even if the number is zero).
            // Do not expect to receive<Rx> PFLAA sentences, because
            // the number of aircraft being processed might be higher or lower.
            //
            // TX
            // Decimal integer value.Range: from 0 to 1.
            //
            // GPS status:
            // 0 = no GPS reception
            // 1 = 3d-fix on ground, i.e. not airborne
            // 2 = 3d-fix when airborne
            //
            // Power
            // Decimal integer value.Range: from 0 to 1.
            //
            // Alarm level
            // Decimal integer value. Range: from 0 to 3.
            //
            // Alarm level as assessed by FLARM:
            // 0 = no alarm (also used for no-alarm traffic information)
            // 1 = aircraft or obstacle alarm, 15-20 seconds to impact,
            // Alert Zone alarm, or traffic advisory (<AlarmType> = 4)
            // 2 = aircraft or obstacle alarm, 10-15 seconds to impact
            // 3 = aircraft or obstacle alarm, 0-10 seconds to impact
            // Note: Alert Zone: If inside the zone, alarm level is 1 for 4 seconds,
            // then 0 for 12 seconds, then repeats.
            //
            //Degrees
            // Decimal integer value. Range: -180 to 180.
            // Relative bearing in degrees from true ground track to the
            // intruder’s position. Positive values are clockwise.
            // 0° indicates that the object is exactly ahead. The field is
            // empty for non - directional targets or when no aircraft are
            // within range.For obstacle alarm and Alert Zone alarm,
            // this field is 0.
            //
            //AlarmType
            //Hexadecimal value.Range: from 0 to FF.
            //Type of alarm as assessed by FLARM
            // 0 = no aircraft within range or no - alarm traffic
            //    information
            // 2 = aircraft alarm
            // 3 = obstacle / Alert Zone alarm (if data port version
            //    < 7, otherwise only obstacle alarms are indicated by
            //    <AlarmType> = 3)
            // 4 = traffic advisory(sent once each time an aircraft
            //     enters within distance 1.5 km and vertical distance
            //     300 m from own ship; when data port version >= 4)
            // xx = Alert Zone alarm(see comment below)
            // When data port version >= 7, the type of Alert Zone is
            // sent as <AlarmType> in the range 10..FF.Refer to the
            // <ZoneType> parameter in the PFLAO sentence for a description.
            //
            // Above/Below
            // Decimal integer value.Range: from -32768 to 32767
            // Vertical distance in meters to the intruder. Positive
            // values indicate that the intruder is above own ship.
            //
            // Distance
            // Decimal integer value.Range: from 0 to 2147483647
            // Horizontal distance in meters to the intruder.

            // if both alarmlevel and alarmtype are 0 then display shows direction
            // with green led and no alarm sound

            //     [< 0]  [< 37]
            //  [< -35]      [< 73]
            //[< -71]          [< 109]
            //  [< -107]      [< 145]
            //    [< -143] [> 145]

            var line = $"$PFLAU,1,0,2,1,{alarmlevel},{degrees},{alarmtype},-1,1284*";
            var checksum = CalcChecksum(line);
            line += checksum.ToString("X2");
            Console.Write(line + "\r");
            byte[] lineBytes = Encoding.ASCII.GetBytes(line);
            sps.Write(lineBytes, 0, lineBytes.Length);
            f.Write(lineBytes, 0, lineBytes.Length);
            f.Write(new byte[] { 13, 10 }, 0, 2);
        }

        static void Main(string[] args)
        {
            try
            {
                var sps = new RJCP.IO.Ports.SerialPortStream("COM3", 19200, 8, Parity.None, StopBits.One);
//                var sps = new RJCP.IO.Ports.SerialPortStream("COM3", 4800, 8, Parity.None, StopBits.One);
                sps.Open();
                var file = System.IO.File.Open("C:\\Users\\antoinem\\Dropbox\\Projects\\FLARM\\Data\\Clockwise.txt", System.IO.FileMode.OpenOrCreate);
                var alarmlevel = 2;
                var alarmtype = 2;
                for (var degrees = -180; degrees < 180; degrees += 1)
                {
                    while (!Console.KeyAvailable)
                    {
                        SendPFLAU(sps, degrees, alarmlevel, alarmtype, file);
                        System.Threading.Thread.Sleep(500);
                    }
                    Console.ReadKey();
                }
                sps.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
