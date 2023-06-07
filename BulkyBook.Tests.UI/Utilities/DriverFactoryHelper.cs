using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace BulkyBook.Tests.UI.Utilities
{
    public class DriverFactoryHelper
    {
        public static IWebDriver ConfigureDriver()
        {
            var driverToUse = ConfigurationHelper.Get<DriverToUse>("DriverToUse");
            IWebDriver driver;

            switch (driverToUse)
            {
                case DriverToUse.InternetExplorer:
                    driver = new InternetExplorerDriver();
                    break;
                case DriverToUse.Firefox:
                    driver = new FirefoxDriver();
                    break;
                case DriverToUse.Chrome:
                    driver = new ChromeDriver();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            driver.Manage().Window.Maximize();

            return driver;
        }
    }

    public enum DriverToUse
    {
        InternetExplorer,
        Chrome,
        Firefox
    }
}
