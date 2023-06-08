using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace UI.Pages
{
    public class LoginPage
    {
        // Locators
        private By emailField = By.Id("Input_Email");
        private By passwordField = By.Id("Input_Password");
        private By loginButton = By.Id("login-submit");

        public void EnterLoginCredentialsAndLogin(IWebDriver driver, string email, string password)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.FindElement(emailField));
            driver.FindElement(emailField).SendKeys(email);
            wait.Until(d => d.FindElement(passwordField));
            driver.FindElement(passwordField).SendKeys(password);
            wait.Until(d => d.FindElement(loginButton));
            driver.FindElement(loginButton).Click();
        }
    }
}
