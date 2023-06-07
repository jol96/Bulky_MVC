using Microsoft.Extensions.Configuration;

namespace BulkyBook.Tests.UI.Utilities
{
    public static class ConfigurationHelper
    {
        private static readonly string appSettings = "appsettings.json";

        public static T Get<T>(string name)
        {
            var value = CreateConfigurationBuilder(appSettings).GetSection(name).Value;
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value), $"Value {value} could not be found in appsetting.json");
            }

            if (typeof(T).IsEnum)
                return (T)Enum.Parse(typeof(T), value);
            return (T)Convert.ChangeType(value, typeof(T));
        }

        public static IConfigurationSection GetLoggingLevel() 
        {
            var logginSection = CreateConfigurationBuilder(appSettings).GetSection("Logging:LogLevel");
            if (logginSection is null)
            {
                throw new ArgumentNullException(nameof(logginSection), $"{logginSection} could not be found in appsetting.json");
            }
            return logginSection;
        }

        public static string GetTargetUrl()
        {
            var value = CreateConfigurationBuilder(appSettings).GetSection("TargetUrl").Value;
            if (value is null)
            {
                throw new ArgumentNullException(nameof(value), $"Value {value} could not be found in appsetting.json");
            }
            return value;
        }

        private static IConfigurationRoot CreateConfigurationBuilder(string configFile) 
        {
            return new ConfigurationBuilder()
                .AddJsonFile(HelperMethods.GetProjectRootPath() + "\\" + configFile)
                .Build();
        }
    }
}
