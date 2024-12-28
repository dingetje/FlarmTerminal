using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Runtime.Versioning;
using System.Text;
using System.Threading.Tasks;

namespace FlarmTerminal.GUI
{
    [SupportedOSPlatform("windows")]
    public class Beeper
    {
        public static void Beep(int Frequency, int durationInMiliSeconds)
        {
            var frequency = Frequency; // Frequency in Hz
            var sampleRate = 24100; // Sample Rate in Hz
            var amplitude = 0.40 * short.MaxValue; // Amplitude
            var samplesPerWaveLength = sampleRate / frequency;
            var waveLengthInBytes = samplesPerWaveLength * 2; // 16 bit sound = 2 bytes per sample
            var totalWaves = (sampleRate / samplesPerWaveLength * durationInMiliSeconds)/600; // Total number of waves to generate

            var memoryStream = new MemoryStream();
            var binaryWriter = new BinaryWriter(memoryStream);

            // Write the header of the WAV file
            binaryWriter.Write(new char[4] { 'R', 'I', 'F', 'F' });
            binaryWriter.Write(36 + totalWaves * waveLengthInBytes);
            binaryWriter.Write(new char[8] { 'W', 'A', 'V', 'E', 'f', 'm', 't', ' ' });
            binaryWriter.Write(16);
            binaryWriter.Write((short)1);
            binaryWriter.Write((short)1);
            binaryWriter.Write(sampleRate);
            binaryWriter.Write(sampleRate * 2);
            binaryWriter.Write((short)2);
            binaryWriter.Write((short)16);
            binaryWriter.Write(new char[4] { 'd', 'a', 't', 'a' });
            binaryWriter.Write(totalWaves * waveLengthInBytes);

            // Generate and write the samples
            for (int j = 0; j < totalWaves; j++)
            {
                for (int i = 0; i < samplesPerWaveLength; i++)
                {
                    var value = i < samplesPerWaveLength / 2 ? amplitude : -amplitude;
                    var valueInBytes = BitConverter.GetBytes((short)value);
                    binaryWriter.Write(valueInBytes);
                }
            }

            binaryWriter.Flush();
            memoryStream.Seek(0, SeekOrigin.Begin);

            // Play the sound
            var soundPlayer = new SoundPlayer(memoryStream);
            soundPlayer.PlaySync();
        }
    }
}
