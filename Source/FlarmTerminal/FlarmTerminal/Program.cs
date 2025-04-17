using System;
using System.Reflection;
using System.Runtime.Versioning;
using System.Windows.Forms;

namespace FlarmTerminal
{
    [SupportedOSPlatform("windows")]
    internal static class Program
    {
        public const string ApplicationName = "FLARM Terminal";
        public static FlarmTerminalLogger logger = new FlarmTerminalLogger();
        
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            var log = logger.GetLogger();
            try
            {
                var version = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "1.0.0.0";
                log.Information($"{ApplicationName} V{version}");
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm(log));
            }
            catch(Exception ex)
            {
                log.Error(ex, "An error occurred in the main application");
                MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
