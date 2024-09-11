using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace TestSearch
{
    public class Tests
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            // Khởi tạo trình điều khiển Chrome
            driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Navigate().GoToUrl("http://localhost:3001/");
        }

        [Test]
        public void SearchDocument_Found_ReturnsCorrectResult()
        {
            IWebElement searchInput = driver.FindElement(By.Id("searchInput"));
            searchInput.SendKeys("Document1");

            IWebElement searchButton = driver.FindElement(By.Id("searchButton"));
            searchButton.Click();

            IWebElement result = driver.FindElement(By.Id("result"));
            Assert.IsTrue(result.Text.Contains("Document1"), "Document1 should be found.");
        }

        [Test]
        public void SearchDocument_NotFound_ReturnsNotFoundMessage()
        {
            // Tìm ô tìm kiếm và nhập từ khóa không hợp lệ
            IWebElement searchInput = driver.FindElement(By.Id("searchInput"));
            searchInput.SendKeys("UnknownDocument");

            // Tìm và nhấn nút tìm kiếm
            IWebElement searchButton = driver.FindElement(By.Id("searchButton"));
            searchButton.Click();

            // Đợi kết quả và kiểm tra nội dung
            IWebElement result = driver.FindElement(By.Id("result"));
            Assert.AreEqual("Not Found", result.Text, "The document should not be found.");
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }
    }
}
