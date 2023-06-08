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

        [Given(@"I login to the application")]
        public void GivenILoginToTheApplication()
        {
            if (driver == null)
            {
                Assert.Fail("Unable to enter login details as driver is null");            
            }
            else 
            {
                LoginPage.driver = driver;
                LoginPage.EnterLoginCredentialsAndLogin(username, password);
            }
        }
    }
}
