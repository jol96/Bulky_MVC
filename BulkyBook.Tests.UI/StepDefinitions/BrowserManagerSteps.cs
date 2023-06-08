using Microsoft.Extensions.Logging;
using NUnit.Framework;
using TechTalk.SpecFlow;

namespace BulkyBook.Tests.UI.StepDefinitions
{
    [Binding]
    public class BrowserManagerSteps : BaseSteps
    {
        public BrowserManagerSteps(Context.ScenarioContext scenarioContext) : base(scenarioContext)
        {
        }

        [Before]
        public void SetUp()
        {
            GetConfiguration();
        }

        [Given(@"I open the Book store web app")]
        public void GivenIOpenTheBookStoreWebApp()
        {
            if (scenarioContext.driver == null)
            {
                Assert.Fail("Cannot open the book store web app as driver is null");              
            }
            else
            {
                scenarioContext.driver.Navigate().GoToUrl(scenarioContext.baseUrl);
            }
        }

        [After]
        public void CloseBrowser()
        {
            if (scenarioContext.driver == null)
            {
                Assert.Fail("Cannot quit driver as its equal to null");
            }
            else
            {
                scenarioContext.driver.Quit();
                scenarioContext.driver.Dispose();
            }
        }

    }
}