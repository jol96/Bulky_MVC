using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BulkyBook.Tests.UI.Utilities
{
    public static class DependencyInjectionHelper
    {
        public static void Configure(IServiceCollection services)
        {
            services.AddSingleton<ILoggerFactory, LoggerFactory>();
            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddConfiguration(ConfigurationHelper.GetLoggingLevel());
                loggingBuilder.AddConsole();
            });
        }
    }
}