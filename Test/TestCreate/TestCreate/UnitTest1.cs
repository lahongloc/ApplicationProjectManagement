using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;

namespace TestCreate
{
    public class PostTests
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
         
            driver = new ChromeDriver();
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
            driver.Navigate().GoToUrl("http://localhost:3001/");
        }

        [Test]
        public void CreatePost_ShouldDisplayPostOnPage_WhenPostIsValid()
        {
            
            var usernameField = driver.FindElement(By.Id("username"));
            var passwordField = driver.FindElement(By.Id("password"));
            var loginButton = driver.FindElement(By.Id("loginButton"));

            usernameField.SendKeys("your_username");
            passwordField.SendKeys("your_password");
            loginButton.Click();

            // Điều hướng đến trang đăng bài
            driver.Navigate().GoToUrl("http://localhost:3001/");

            // Điền thông tin bài viết
            var titleField = driver.FindElement(By.Id("postTitle"));
            var contentField = driver.FindElement(By.Id("postContent"));
            var submitButton = driver.FindElement(By.Id("submitPost"));

            titleField.SendKeys("Bài viết mới từ Selenium");
            contentField.SendKeys("Đây là nội dung của bài viết mới.");
            submitButton.Click();

            var confirmationMessage = driver.FindElement(By.ClassName("post-success-message"));

            Assert.IsTrue(confirmationMessage.Displayed, "Bài viết đã được đăng thành công.");
        }

        [TearDown]
        public void TearDown()
        {
            // Đóng trình duyệt sau khi test kết thúc
            driver.Quit();
        }
    }
}
