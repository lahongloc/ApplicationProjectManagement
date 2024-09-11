using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;

namespace testRegist
{
    public class Tests
    {

            private IWebDriver webDriver;

            [SetUp]
            public void Setup()
            {
                webDriver = new ChromeDriver();
                webDriver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
                webDriver.Navigate().GoToUrl("http://your-website.com/register"); // Điều hướng đến URL trang đăng ký
            }

            [Test]
            public void TC_Register_01_SuccessfulRegistration()
            {
                // Điền thông tin đăng ký hợp lệ
                webDriver.FindElement(By.Id("name")).SendKeys("Nguyen Van A");
                webDriver.FindElement(By.Id("email")).SendKeys("testemail@example.com");
                webDriver.FindElement(By.Id("password")).SendKeys("ValidPassword123");
                webDriver.FindElement(By.Id("confirmPassword")).SendKeys("ValidPassword123");

                // Nhấn nút Đăng ký
                IWebElement registerButton = webDriver.FindElement(By.XPath("//button[contains(text(), 'Đăng ký')]"));
                registerButton.Click();

                // Chờ thông báo xác nhận xuất hiện
                WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
                IWebElement successMessage = wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("success-message")));

                // Kiểm tra thông báo thành công
                Assert.IsTrue(successMessage.Displayed, "Thông báo thành công phải hiển thị sau khi đăng ký.");
                Assert.AreEqual("Tài khoản của bạn đã được tạo thành công. Vui lòng kiểm tra email để xác nhận.", successMessage.Text);
            }

            [Test]
            public void TC_Register_02_EmailAlreadyExists()
            {
                // Điền thông tin đăng ký với email đã tồn tại
                webDriver.FindElement(By.Id("name")).SendKeys("Nguyen Van B");
                webDriver.FindElement(By.Id("email")).SendKeys("existingemail@example.com"); // Email đã tồn tại
                webDriver.FindElement(By.Id("password")).SendKeys("Password123");
                webDriver.FindElement(By.Id("confirmPassword")).SendKeys("Password123");

                // Nhấn nút Đăng ký
                IWebElement registerButton = webDriver.FindElement(By.XPath("//button[contains(text(), 'Đăng ký')]"));
                registerButton.Click();

                // Chờ thông báo lỗi xuất hiện
                WebDriverWait wait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(10));
                IWebElement errorMessage = wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName("error-message")));

                // Kiểm tra thông báo lỗi "Email đã tồn tại"
                Assert.IsTrue(errorMessage.Displayed, "Thông báo lỗi phải hiển thị khi email đã tồn tại.");
                Assert.AreEqual("Email đã tồn tại", errorMessage.Text);
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
