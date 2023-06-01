using Microsoft.Extensions.Logging;

namespace BulkyBook.Tests.UI.StepDefinitions
{
    [Binding]
    public sealed class ContextSteps : BaseSteps
    {
        [Before]
        public void SetUp()
        {
            GetConfiguration();
        }

        [Given(@"I open the Book store web app")]
        public void GivenIOpenTheBookStoreWebApp()
        {
            if (driver != null)
            {
                driver.Navigate().GoToUrl(baseUrl);
            }
            else
            {
                logger.LogError("Driver is equal to null");
            }
        }

        [After]
        public void After() 
        {
            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
            }
            else 
            {
                logger.LogError("Cannot quit driver as its equal to null");
            }
        }

    }
}