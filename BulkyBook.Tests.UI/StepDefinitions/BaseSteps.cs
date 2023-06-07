using BulkyBook.Tests.UI.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;

namespace BulkyBook.Tests.UI.StepDefinitions
{
    public abstract class BaseSteps
    {
        protected ILoggerFactory loggerFactory;
        protected ILogger<BaseSteps> logger;
        protected IWebDriver? driver;
        protected string baseUrl;

        public void GetConfiguration()
        {
            CreateServices();
            ConfigureBrowser();
        }

        private void CreateServices()
        {
            // create service provider
            var services = new ServiceCollection();
            DependencyInjectionHelper.Configure(services);
            var serviceProvider = services.BuildServiceProvider();

            // create the services 
            loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            logger = loggerFactory.CreateLogger<BaseSteps>();  
        }

        private void ConfigureBrowser()
        {
            driver = DriverFactoryHelper.ConfigureDriver();
            baseUrl = ConfigurationHelper.GetTargetUrl();
        }
    }
}
