using Microsoft.Extensions.Logging;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace BulkyBook.Tests.UI.StepDefinitions
{
    [Binding]
    public class BrowserManagerSteps : BaseSteps
    {
        

        [Before]
        public void SetUp()
        {
            GetConfiguration();
        }

        [Given(@"I open the Book store web app")]
        public void GivenIOpenTheBookStoreWebApp()
        {
            if (driver == null)
            {
                Assert.Fail("Cannot open the book store web app as driver is null");              
            }
            else
            {
                driver.Navigate().GoToUrl(baseUrl);
            }
        }

        [After]
        public void CloseBrowser()
        {
            if (driver == null)
            {
                Assert.Fail("Cannot quit driver as its equal to null");
            }
            else
            {
                driver.Quit();
                driver.Dispose();
            }
        }

    }
}