using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BulkyBook.Tests.UI.StepDefinitions
{
    public static class DependencyInjectionConfig
    {
        public static void Configure(IServiceCollection services)
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile(HelperMethods.GetProjectRootPath() + "\\appsettings.json")
                .Build();

            services.AddSingleton<ILoggerFactory, LoggerFactory>();
            services.AddLogging(loggingBuilder =>
            {
                var logLevelConfiguration = configuration.GetSection("Logging:LogLevel");
                loggingBuilder.AddConfiguration(logLevelConfiguration);
                loggingBuilder.AddConsole();    
            });
        }
    }
}