using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace AutomationTests.UITests.Pages
{
    public class HomePage
    {
        private readonly IWebDriver _driver;
        private readonly ILogger _logger;
        private WebDriverWait _wait;

        // Locators
        private By menuItem(string item) => By.XPath($"//h3[text()='{item}']/..");

        public HomePage(IWebDriver driver)
        {
            _driver = driver;
            _logger = DependencyRegister.GetService<ILogger>();
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        public void NavigateToHomePage(string url)
        {
            _driver.Navigate().GoToUrl(url);
            _logger.Info("Home page was opened.");
        }

        public void ClickMenuItem(string itemName)
        {
            var item = _wait.Until(d => d.FindElement(menuItem(itemName)));
            _logger.Info($"Clicking on {itemName} menu on Home page.");
            item.Click();
        }
    }
}
