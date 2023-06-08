using BulkyBook.Tests.UI.Utilities;
using OpenQA.Selenium;

namespace BulkyBook.Tests.UI.Pages
{
    public class HomePage
    {
        // Locators
        private readonly By LoginBtn = By.Id("login");
        private readonly By LogoutBtn = By.Id("logout");
        private readonly By ContentManagementDD = By.XPath("//*[contains(text(),'Content')]");
        private readonly By Category = By.XPath("//*[contains(text(),'Category')]");
        private readonly By CartIcon = By.XPath("//a[contains(@href, 'Cart')]");
        private By DetailsBtn;

        public void ClickLogin(IWebDriver driver)
        {
            SeleniumHelper.ClickElement(driver, LoginBtn, 10);
        }
        public void ClickLogout(IWebDriver driver)
        {
            SeleniumHelper.ClickElement(driver, LogoutBtn, 10);
        }

        public void ClickContentManagementDD(IWebDriver driver)
        {
            SeleniumHelper.ClickElement(driver, ContentManagementDD, 10);
        }

        public void ClickCategory(IWebDriver driver)
        {
            SeleniumHelper.ClickElement(driver, Category, 10);
        }

        public void ClickDetailsBtn(IWebDriver driver, String productName)
        {
            DetailsBtn = By.XPath("//*[contains(text(),'" + productName + "')]//..//../following-sibling::div[*]//*[contains(text(),'Details')]");
            SeleniumHelper.ClickElement(driver, DetailsBtn, 20);
        }

        public void ClickCartIcon(IWebDriver driver)
        {
            SeleniumHelper.ClickElement(driver, CartIcon, 10);
        }
    }
}
