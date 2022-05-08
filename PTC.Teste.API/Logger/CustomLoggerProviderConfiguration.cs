using Microsoft.Extensions.Logging;

namespace PTC.Teste.Api.Logger
{
    public class CustomLoggerProviderConfiguration
    {
        public LogLevel LogLevel { get; set; } = LogLevel.Warning;
        public int EventId { get; set; }
    }
}
