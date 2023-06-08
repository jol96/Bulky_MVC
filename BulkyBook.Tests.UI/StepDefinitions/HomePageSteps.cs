using BulkyBook.Tests.UI.Pages;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;
using UI.Pages;

namespace BulkyBook.Tests.UI.StepDefinitions
{
    [Binding]
    public class HomePageSteps :BaseSteps
    {
        HomePage HomePage { get; set; }

        public HomePageSteps(Context.ScenarioContext scenarioContext) : base(scenarioContext)
        {
            HomePage = new HomePage();
        }

        [Given(@"I click the login link")]
        public void GivenIClickTheLoginLink()
        {
            if (scenarioContext.driver == null)
            {
                Assert.Fail("Unable to click the login link as driver is null");
            }
            else
            {
                HomePage.ClickLogin(scenarioContext.driver);
            }
        }

        [Then(@"the user has logged in successfully")]
        public void ThenTheUserHasLoggedInSuccessfully()
        {
            if (scenarioContext.driver == null)
            {
                Assert.Fail("Unable to click the login link as driver is null");
            }
            else
            {
                // clicking the logout button as that will only appear if the user has logged in successfully
                HomePage.ClickLogout(scenarioContext.driver);
            }
        }

    }
}
