using System;
using System.Reflection;
using System.Runtime.Versioning;
using System.Windows.Forms;
using FlarmTerminal.GUI;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace FlarmTerminal
{
    [SupportedOSPlatform("windows")]
    internal static class Program
    {
        public const string ApplicationName = "Flarm Terminal";
        public static FlarmTerminalLogger logger = new FlarmTerminalLogger();
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var log = logger.GetLogger();
            var version = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "1.0.0.0";
            log.Information($"{ApplicationName} V{version}" );
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(log));
        }
    }
}
