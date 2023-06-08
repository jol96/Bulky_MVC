using BulkyBook.Tests.UI.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;

namespace BulkyBook.Tests.UI.StepDefinitions
{
    public abstract class BaseSteps
    {
        public ILoggerFactory loggerFactory { get; set; }
        public ILogger<BaseSteps> logger { get; set; }
        public IWebDriver driver { get; set; }
        public string baseUrl { get; set; }
        public string username { get; set; }
        public string password { get; set; }

        public void GetConfiguration()
        {
            ConfigureServices();
            ConfigureBrowser();
            ConfigureUsernamePassword();
        }

        public void ConfigureServices()
        {
            // create service provider
            var services = new ServiceCollection();
            DependencyInjectionHelper.Configure(services);
            var serviceProvider = services.BuildServiceProvider();

            // create the services 
            loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            logger = loggerFactory.CreateLogger<BaseSteps>();  
        }

        public void ConfigureBrowser()
        {
            driver = DriverFactoryHelper.ConfigureDriver();
            baseUrl = ConfigurationHelper.GetPropertyValue("TargetUrl");
        }

        public void ConfigureUsernamePassword() 
        {
            username = ConfigurationHelper.GetPropertyValue("UserName");
            password = ConfigurationHelper.GetPropertyValue("Password");
        }
    }
}
