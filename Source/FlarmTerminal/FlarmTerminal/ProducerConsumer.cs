using UCNLNMEA;
using System;
using System.Collections.Generic;
using System.Runtime.Versioning;
using System.Threading;

namespace FlarmTerminal
{
    [SupportedOSPlatform("windows")]
    public class TaskQueue<T> : IDisposable where T : class
    {
        object locker = new object();
        Thread[] workers;
        Queue<T> taskQ = new Queue<T>();
        private MainForm? _mainForm = null!;
        private ProcessMessages? _processMessages = null!;

        public TaskQueue(int workerCount, MainForm form)
        {
            _mainForm = form;
            workers = new Thread[workerCount];

            // Create and start a separate thread for each worker
            for (int i = 0; i < workerCount; i++)
            {
                (workers[i] = new Thread(Consume)).Start();
            }
            _processMessages = new ProcessMessages();
            // Subscribe to GPS data
            _processMessages.GlobalGPSData += (date, GPSQuality, usedSatellitesNumber, precisionHorizontalDilution, antennaAltitude, altitudeUnits, geoidalSeparation, geoidalSeparationUnits, differentialReferenceStation) =>
            {
                _mainForm?.UpdateGPSData(date, GPSQuality, usedSatellitesNumber, precisionHorizontalDilution, antennaAltitude, altitudeUnits, geoidalSeparation, geoidalSeparationUnits, differentialReferenceStation);
            };
            _processMessages.FLARMLedStatusRecieved += (bool [] on) =>
            {
                _mainForm?.UpdateFLARMLeftLED(on);
            };
            _processMessages.FLARMLedAboveBelowStatusRecieved += (bool [] on, bool Alarm) =>
            {
                _mainForm?.UpdateFLARMAboveBelowLED(on, Alarm);
            };
            _processMessages.FLARMLedDirectionRecieved += (bool[] on, int AlarmLevel) =>
            {
                _mainForm?.UpdateDirectionLEDs(on, AlarmLevel);
            };
            _processMessages.FLARMCARPDataReceived += (char antenna, double[] rangeDoubles) =>
            {
                _mainForm?.UpdateCARPRadar(antenna, rangeDoubles);
            };
            _processMessages.FLARMCARPTimeSpanReceived += (DateTime start, DateTime end) =>
            {
                _mainForm?.UpdateCARPTimeSpan(start,end);
            };
            _processMessages.FLARMCARPPoints += (long points) =>
            {
                if (_mainForm != null)
                {
                    _mainForm.CarpPoints = points;
                }
            };
        }

        public void Dispose()
        {
            // Enqueue one null task per worker to make each exit.
            foreach (Thread worker in workers) EnqueueTask(null);
            foreach (Thread worker in workers) worker.Join(TimeSpan.FromMilliseconds(500));
        }

        public void EnqueueTask(T task)
        {
            lock (locker)
            {
                taskQ.Enqueue(task);
                Monitor.PulseAll(locker);
            }
        }

        void Consume()
        {
            while (!_mainForm.IsClosing)
            {
                T newData;
                lock (locker)
                {
                    while (taskQ.Count == 0) Monitor.Wait(locker);
                    newData = taskQ.Dequeue();
                }
                if (newData == null)
                {
                    return;         // This signals our exit
                }
                // Execute task
                var lines = newData.ToString().Split(new string[] {"\r\n"}, StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries);
                foreach (var line in lines)
                {
                    var tmp = new string(line);
                    if (!line.Contains("\r\n"))
                    {
                        tmp += "\r\n";
                    }
                    // NMEA sentence?
                    if (tmp.StartsWith("$") && tmp.EndsWith("\r\n"))
                    {
                        try
                        {
                            var result = NMEAParser.Parse(tmp);
                            if (result != null)
                            {
                                var proprietary = result as NMEAProprietarySentence;
                                if (proprietary != null)
                                {
                                    if (proprietary.Manufacturer == ManufacturerCodes.FLA)
                                    {
                                        // FLARM specific
                                        _processMessages?.Process(proprietary);
                                    }
                                }
                                else
                                {
                                    var nmea = result as NMEAStandartSentence;
                                    if (nmea != null && _processMessages != null)
                                    {
                                        // Standard NMEA
                                        _processMessages?.Process(nmea);
                                    }
                                }
                            }
                        }
                        catch(Exception)
                        {
                        }
                    }
                    _mainForm.WriteToTerminal(tmp);
                }
            }
        }
    }
}
