using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace BulkyBook.Tests.UI.StepDefinitions
{
    public abstract class BaseSteps
    {

        private static Dictionary<string, string> config;
        protected ILoggerFactory loggerFactory;
        protected ILogger<BaseSteps> logger;
        protected WebDriver? driver;
        protected string browser;
        protected string baseUrl;

        public void GetConfiguration()
        {
            InitializeServiceProvider();
            LoadConfigFromFile();
            ConfigureBrowser();
        }

        private void InitializeServiceProvider()
        {
            var serviceProvider = CreateServiceProvider();
            CreateServices(serviceProvider);
        }

        private static ServiceProvider CreateServiceProvider()
        {
            var services = new ServiceCollection();
            DependencyInjectionConfig.Configure(services);
            return services.BuildServiceProvider();
        }

        private void CreateServices(ServiceProvider serviceProvider)
        {
            loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            logger = loggerFactory.CreateLogger<BaseSteps>();
        }

        private void LoadConfigFromFile()
        {
            var configPath = HelperMethods.GetProjectRootPath() + "\\config.json";
            var configJson = File.ReadAllText(configPath);
            if (configJson != null)
            {
                config = JsonConvert.DeserializeObject<Dictionary<string, string>>(configJson);
                if (config == null)
                {
                    logger.LogError("Error creating config object from config file");
                }
            }
            else
            {
                logger.LogError("Error reading config.json");
            }
        }

        private void ConfigureBrowser()
        {
            if (config != null)
            {
                browser = config["browser"];
                baseUrl = config["baseUrl"];
                if (string.Equals(browser, "CHROME", StringComparison.OrdinalIgnoreCase))
                {
                    driver = new ChromeDriver();
                    driver.Manage().Window.Maximize();
                }
                else
                {
                    logger.LogError($"Invalid browser {browser} name read from config file");
                }
            }
        }
    }
}
