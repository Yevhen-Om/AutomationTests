using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace AutomationTests.UITests.Pages
{
    public class DragAndDropPage
    {
        private readonly IWebDriver _driver;
        private readonly ILogger _logger;
        private WebDriverWait _wait;

        // Locators
        private By menuItem(string item) => By.XPath($"//li[text()='{item}']");
        private By orderTicket = By.XPath("//*[@id='plate-items']");

        public DragAndDropPage(IWebDriver driver)
        {
            _driver = driver;
            _logger = DependencyRegister.GetService<ILogger>();
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10));
        }

        public void DragAndDropMenuItem(string itemName)
        {
            var item = _wait.Until(d => d.FindElement(menuItem(itemName)));
            var orderArea = _wait.Until(d => d.FindElement(orderTicket));

            var actions = new Actions(_driver);
            _logger.Info($"Dragging {itemName} to Order Area.");
            actions.DragAndDrop(item, orderArea).Perform();
        }

        public void DragAndDropMenuItemJS(string itemName)
        {
            var item = _wait.Until(d => d.FindElement(menuItem(itemName)));
            var orderArea = _wait.Until(d => d.FindElement(orderTicket));
            IJavaScriptExecutor js = (IJavaScriptExecutor)_driver;
            string script = File.ReadAllText(Path.Combine(TestContext.CurrentContext.WorkDirectory, "../../../UITests/Resources/" + "dragAndDrop.js"));
            var result = js.ExecuteScript(script + "; return simulateDragAndDrop(arguments[0], arguments[1]);",
                                          $"#{item.GetAttribute("id")}", $"#{orderArea.GetAttribute("id")}");

            _logger.Info($"Performed drag and drop via JavaScript Executor for {itemName}.");
        }

        public bool IsItemDropped(string itemName)
        {
            var result = _wait.Until(d => d.FindElement(orderTicket).Text.Contains(itemName));
            _logger.Info($"Verification result for {itemName}: {result}");
            return result;
        }

        public void VerifyIfItemDropped(string itemName)
        {
            Assert.IsTrue(IsItemDropped(itemName), $"{itemName} was not successfully dropped.");
            _logger.Info($"Verified that {itemName} item was dragged and dropped.");
        }
    }
}
