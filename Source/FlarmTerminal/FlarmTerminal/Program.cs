using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using FlarmTerminal.GUI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NLog.Targets;

namespace FlarmTerminal
{
    [SupportedOSPlatform("windows")]
    internal static class Program
    {
        public const string ApplicationName = "Flarm Terminal";
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}
