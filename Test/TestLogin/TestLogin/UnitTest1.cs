using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace TestLogin
{
    public class Tests
    {
        private IWebDriver webDriver;

        [SetUp]
        public void Setup()
        {
            webDriver = new ChromeDriver();
            webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);  // Cấu hình chờ ngầm định
            webDriver.Navigate().GoToUrl("http://localhost:3001/");  // Điều hướng đến URL trang login
        }

        [Test]
        public void TC_Login_01()
        {
            // Điền thông tin đăng nhập đúng
            IWebElement usernameField = webDriver.FindElement(By.Id("username"));
            usernameField.SendKeys("TrucLy");

            IWebElement passwordField = webDriver.FindElement(By.Id("password"));
            passwordField.SendKeys("123456");

            IWebElement btn_login = webDriver.FindElement(By.XPath("/html/body/div/main/div/form/button"));
            btn_login.Click();

            // Sử dụng WebDriverWait để chờ kết quả sau khi đăng nhập
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.UrlContains("/dashboard"));  // Chờ đến khi URL chứa "/dashboard" hoặc trang bạn muốn

            // Kiểm tra xem có chuyển hướng đến dashboard sau khi đăng nhập thành công không
            Assert.IsTrue(webDriver.Url.Contains("/dashboard"), "Đăng nhập thành công và chuyển hướng đến trang Dashboard.");
        }

        [Test]
        public void TC_Login_02_InvalidPassword()
        {
            // Điền thông tin đăng nhập với mật khẩu không hợp lệ
            IWebElement usernameField = webDriver.FindElement(By.Id("username"));
            usernameField.SendKeys("TrucLy");

            IWebElement passwordField = webDriver.FindElement(By.Id("password"));
            passwordField.SendKeys("12345");  // Mật khẩu sai

            IWebElement btn_login = webDriver.FindElement(By.XPath("/html/body/div/main/div/form/button"));
            btn_login.Click();

            // Chờ xuất hiện thông báo lỗi đăng nhập
            WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
            IWebElement errorMessage = wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("error-message")));  // Giả sử thẻ chứa thông báo lỗi có class là "error-message"

            // Kiểm tra xem thông báo lỗi có xuất hiện không
            Assert.IsTrue(errorMessage.Displayed, "Thông báo lỗi phải xuất hiện khi mật khẩu không hợp lệ.");
            Assert.AreEqual("Sai tên đăng nhập hoặc mật khẩu.", errorMessage.Text);  // Kiểm tra nội dung thông báo lỗi (thay thế bằng nội dung thực tế trên trang của bạn)
        }

        [TearDown]
        public void TearDown()
        {
            // Đóng trình duyệt sau khi test hoàn thành
            if (webDriver != null)
            {
                webDriver.Quit();
            }
        }
    }
}
}