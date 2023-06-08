using Microsoft.Extensions.Logging;
using NUnit.Framework;
using System;
using TechTalk.SpecFlow;
using UI.Pages;

namespace BulkyBook.Tests.UI.StepDefinitions
{
    [Binding]
    public class LoginPageSteps : BaseSteps
    {
        public LoginPage LoginPage { get; set; }

        public LoginPageSteps(Context.ScenarioContext scenarioContext) : base(scenarioContext)
        {
            LoginPage = new LoginPage();
        }

        [Given(@"I enter the username and password credentials")]
        public void GivenIEnterTheUsernameAndPasswordCredentials()
        {
            if (scenarioContext.driver == null)
            {
                Assert.Fail("Unable to enter login details as driver is null");
            }
            else
            {
                LoginPage.EnterLoginCredentialsAndLogin(scenarioContext.driver, scenarioContext.username, scenarioContext.password);
            }
        }
    }
}
