using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;

namespace PTC.Teste.Api.Logger
{
    public class CustomLogger : ILogger
    {
        private readonly string _loggerName;
        private readonly CustomLoggerProviderConfiguration _loggerConfig;
        private readonly IWebHostEnvironment _webHostEnvoriment;

        public CustomLogger(string loggerName, CustomLoggerProviderConfiguration loggerConfig, IWebHostEnvironment webHostEnvoriment)
        {
            _loggerConfig = loggerConfig;
            _loggerName = loggerName;
            _webHostEnvoriment = webHostEnvoriment;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
        public bool IsEnabled(LogLevel logLevel)
        {
            throw new NotImplementedException();
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            string message = string.Format("{0}: {1} - {2}", logLevel.ToString(), eventId.Id, formatter(state, exception));
            WriteTextOnFile(message);
        }

        public void WriteTextOnFile(string message)
        {
            string path = Path.Combine(_webHostEnvoriment.ContentRootPath, "log");

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            string fileName = Path.Combine(path, "ptc.Teste.log"); 
            using StreamWriter streamWriter = new(fileName, true);
            streamWriter.WriteLine($"{DateTime.Now:dd/MM/yyyy HH:mm:ss} -> {message}");
            streamWriter.Close();
        }
    }
}
