using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;

namespace MyFirstAzureWebApp.Pages.Tests
{
   // [TestClass]
    public class LoginTest_by_SeleniumIntegration
    {

        private IWebDriver driver;
        private static String BASE_URL = "http://hrm.pragmatictestlabs.com";

        [TestInitialize()]
        public void BeforeMethod()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments("disable-infobars");
            driver = new ChromeDriver(options);
            driver.Manage().Window.FullScreen();
            driver.Navigate().GoToUrl(BASE_URL);

            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        [TestCleanup()]
        public void afterMethod()
        {
            driver.Close();
        }


        // [TestMethod]
        public void testLoginWithValidCredentials()
        {

            //<input name="txtUsername" id="txtUsername" type="text" value="Admin">
            driver.FindElement(By.Id("txtUsername")).SendKeys("Admin");


            //Check if password input is of type="password"
            Assert.AreEqual(driver.FindElement(By.Id("txtPassword")).GetAttribute("type"), "password");
            driver.FindElement(By.Id("txtPassword")).SendKeys("Ptl@#321");


            Assert.AreEqual(driver.FindElement(By.Id("txtUsername")).GetAttribute("value"), "Admin");

            driver.FindElement(By.Id("btnLogin")).Click();
            String strWelcomeMessage = driver.FindElement(By.Id("welcome")).Text;
            Assert.AreEqual(strWelcomeMessage, "Welcome Admin");
        }


        // [TestMethod]
        public void testValidUserLoginUsingKeyboardKeys()
        {
            driver.FindElement(By.Id("txtUsername")).SendKeys("Admin");
            driver.FindElement(By.Id("txtUsername")).SendKeys(Keys.Tab);

            driver.FindElement(By.Id("txtPassword")).SendKeys("Ptl@#321");
            driver.FindElement(By.Id("txtPassword")).SendKeys(Keys.Return);


            String strWelcomeMessage = driver.FindElement(By.Id("welcome")).Text;
            Assert.AreEqual(strWelcomeMessage, "Welcome Admin");
        }

        // [TestMethod]
        public void testLoginWithInvalidPassword()
        {
            driver.FindElement(By.Id("txtUsername")).SendKeys("Admin");
            driver.FindElement(By.Id("txtPassword")).SendKeys("InvalidPWd");
            driver.FindElement(By.Id("btnLogin")).Click();

            ///<span id="spanMessage">Invalid credentials</span>

            String msgError = driver.FindElement(By.Id("spanMessage")).Text;
            Assert.AreEqual(msgError, "Invalid credentials");
        }

        // [TestMethod]
        public void testLoginWithBlankUsername()
        {
            driver.FindElement(By.Id("txtUsername")).SendKeys("");
            driver.FindElement(By.Id("txtPassword")).SendKeys("Ptl@#321");
            driver.FindElement(By.Id("btnLogin")).Click();

            String msgError = driver.FindElement(By.Id("spanMessage")).Text;
            Assert.AreEqual(msgError, "Username cannot be empty");
        }


        // [TestMethod]
        public void testLoginWithBlankUsernameAndPassword()
        {
            driver.FindElement(By.Id("txtUsername")).SendKeys("");
            driver.FindElement(By.Id("txtPassword")).SendKeys("");
            driver.FindElement(By.Id("btnLogin")).Click();

            String msgError = driver.FindElement(By.Id("spanMessage")).Text;
            Assert.AreEqual(msgError, "Username cannot be empty");

        }

        // [TestMethod]
        public void testLoginWithBlankPassword()
        {
            driver.FindElement(By.Id("txtUsername")).SendKeys("Admin");
            driver.FindElement(By.Id("txtPassword")).SendKeys("");
            driver.FindElement(By.Id("btnLogin")).Click();

            String msgError = driver.FindElement(By.Id("spanMessage")).Text;
            Assert.AreEqual(msgError, "Password cannot be empty");

        }


        // [TestMethod]
        public void testCaseSensitivityOfPassword()
        {
            driver.FindElement(By.Id("txtUsername")).SendKeys("Admin");
            driver.FindElement(By.Id("txtPassword")).SendKeys("ptl@#321");
            driver.FindElement(By.Id("btnLogin")).Click();

            String msgError = driver.FindElement(By.Id("spanMessage")).Text;
            Assert.AreEqual(msgError, "Invalid credentials");
        }


        // [TestMethod]
        public void testLogoutFromSystem()
        {
            driver.FindElement(By.Id("txtUsername")).SendKeys("Admin");
            driver.FindElement(By.Id("txtPassword")).SendKeys("Ptl@#321");
            driver.FindElement(By.Id("btnLogin")).Click();
            driver.FindElement(By.Id("welcome")).Click();
            driver.FindElement(By.LinkText("Logout")).Click();

            String currentURL = driver.Url;
            Assert.AreEqual(currentURL, "http://hrm.pragmatictestlabs.com/symfony/web/index.php/auth/login");
            driver.Navigate().Back();
            Assert.IsFalse(driver.FindElement(By.Id("welcome")).Text.Length > 0);

        }
    }

}