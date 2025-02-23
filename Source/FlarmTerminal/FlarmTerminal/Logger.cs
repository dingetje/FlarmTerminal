using System;
using System.IO;
using Serilog;
using Serilog.Debugging;
using Serilog.Configuration;
using Serilog.Sinks.File; // Ensure this is included
using Microsoft.Extensions.Configuration;

namespace FlarmTerminal
{
    internal class FlarmTerminalLogger
    {
        private readonly ILogger _logger;

        public ILogger GetLogger()
        {
            return _logger;
        }

        public FlarmTerminalLogger()
        {
            // Enable Serilog self-logging to a file for debugging
#if DEBUG
            // Enable Serilog self-logging to a file for debugging
            var selfLogFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs", "serilog-selflog.txt");
            Directory.CreateDirectory(Path.GetDirectoryName(selfLogFile));
            var selfLogWriter = new StreamWriter(selfLogFile, true) { AutoFlush = true };
            SelfLog.Enable(selfLogWriter);
#endif
            try
            {
                var configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

                _logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration)
                    .CreateLogger();
                _logger.Debug("Logger initialized from configuration.");
            }
            catch (Exception ex)
            {
                // Fallback logger in case of configuration failure
                _logger = new LoggerConfiguration()
                    .MinimumLevel.Debug()
                    .WriteTo.Console()
                    .WriteTo.File("Logs/fallback-log-.txt", rollingInterval: RollingInterval.Day)
                    .CreateLogger();

                _logger.Warning(ex, "Failed to initialize Serilog from configuration. Using fallback logger.");
            }
        }
    }
}
