using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace BulkyBook.Tests.UI.Utilities
{
    public class SeleniumHelper
    {
        public static void WaitForElementToBeVisible(IWebDriver driver, By element, int waitTime)
        {
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(waitTime));
            wait.Until(d => d.FindElement(element));
        }

        public static void ClickElement(IWebDriver driver, By element, int waitTime)
        {
            WaitForElementToBeVisible(driver, element, waitTime);
            driver.FindElement(element).Click();
        }

        public static void EnterText(IWebDriver driver, By element, int waitTime, string text)
        {
            WaitForElementToBeVisible(driver, element, waitTime);
            driver.FindElement(element).SendKeys(text);
        }

        public static void ClearField(IWebDriver driver, By element, int waitTime)
        {
            WaitForElementToBeVisible(driver, element, waitTime);
            driver.FindElement(element).Clear();
        }

        public static void ScrollToElementByXPath(IWebDriver driver, string xpath)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript($"document.evaluate(\"{xpath}\", document, null, XPathResult.FIRST_ORDERED_NODE_TYPE, null).singleNodeValue.scrollIntoView();");
        }
    }
}
