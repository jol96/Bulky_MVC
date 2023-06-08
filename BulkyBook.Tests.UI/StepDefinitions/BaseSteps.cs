using BulkyBook.Tests.UI.Context;
using BulkyBook.Tests.UI.Utilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OpenQA.Selenium;

namespace BulkyBook.Tests.UI.StepDefinitions
{
    public abstract class BaseSteps
    {
        protected ScenarioContext scenarioContext;

        protected BaseSteps(ScenarioContext scenarioContext)
        {
            this.scenarioContext = scenarioContext;
        }

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
            scenarioContext.loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            scenarioContext.logger = scenarioContext.loggerFactory.CreateLogger<BaseSteps>();  
        }

        public void ConfigureBrowser()
        {
            scenarioContext.driver = DriverFactoryHelper.ConfigureDriver();
            scenarioContext.baseUrl = ConfigurationHelper.GetPropertyValue("TargetUrl");
        }

        public void ConfigureUsernamePassword() 
        {
            scenarioContext.username = ConfigurationHelper.GetPropertyValue("UserName");
            scenarioContext.password = ConfigurationHelper.GetPropertyValue("Password");
        }
    }
}
