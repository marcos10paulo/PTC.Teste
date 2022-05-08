using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System.Collections.Concurrent;

namespace PTC.Teste.Api.Logger
{
    public class CustomLoggerProvider : ILoggerProvider
    {
        readonly CustomLoggerProviderConfiguration loggerConfig;
        readonly ConcurrentDictionary<string, CustomLogger> logger = new();
        readonly IWebHostEnvironment webHostEnvironment;

        public CustomLoggerProvider(CustomLoggerProviderConfiguration _loggerConfig, IWebHostEnvironment _webHostEnvoriment)
        {
            loggerConfig = _loggerConfig;
            this.webHostEnvironment = _webHostEnvoriment;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return logger.GetOrAdd(categoryName, name => new CustomLogger(name, loggerConfig, webHostEnvironment));
        }

        public void Dispose()
        {
            logger.Clear();
        }
    }
}
