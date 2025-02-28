using AutomationTests.UITests.Pages;
using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace AutomationTests.UITests.Tests
{
    public class Tests
    {
        [TestFixture]
        public class DragAndDropTests
        {
            private ILogger _logger;
            private IWebDriver _driver;
            private DragAndDropPage dragAndDropPage;
            private HomePage homePage;

            [SetUp]
            public void Setup()
            {
                _driver = new ChromeDriver();
                _driver.Manage().Window.Maximize();
                _logger = DependencyRegister.GetService<ILogger>();
                dragAndDropPage = new DragAndDropPage(_driver);
                homePage = new HomePage(_driver);

                _logger.Info("****** Drag & Drop test *****");
                homePage.NavigateToHomePage(Config.HomePageUrl);
                homePage.ClickMenuItem("Drag & Drop");
            }

            [Test]
            [TestCase("Fried Chicken")]
            [TestCase("Hamburger")]
            [TestCase("Ice Cream")]
            public void VerifyDragAndDropFunctionality(string itemName)
            {
                _logger.Info($"Testing Drag & Drop for {itemName}.");
                
                dragAndDropPage.DragAndDropMenuItem(itemName);

                // Uncomment the line below to use JavaScript Executor for drag and drop
                // dragAndDropPage.DragAndDropMenuItemJS(itemName);
                dragAndDropPage.VerifyIfItemDropped(itemName);
            }

            [TearDown]
            public void TearDown()
            {
                _driver.Dispose();
                _logger.Info("Closed browser session.");
            }
        }
    }
}