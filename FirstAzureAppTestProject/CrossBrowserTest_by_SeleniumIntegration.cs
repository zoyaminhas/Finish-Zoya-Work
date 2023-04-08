using System;
using System.Threading;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Safari;
 

namespace MyFirstAzureWebApp.Pages.Tests
{
    /**
    * This class demonstrate running tests against web browsers
    * <p>
    * Notes :
    * Switching between the browsers could be transferred to a Base Class.
    * Please refer to https://git.io/fhqYE  for more concise version
    */
  //  [TestClass]
    public class CrossBrowserTest_by_SeleniumIntegration
    {
        private static String BASE_URL = "http://hrm.pragmatictestlabs.com";
        private static String USERNAME = "Admin";
        private static String PASSWORD = "Ptl@#321";
        private static String WELCOME_MESSAGE = "Welcome Admin";
        private static int TIMEOUT = 30;


        private void PerformTask(IWebDriver driver)
        {

            //Set the implicit wait
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(TIMEOUT);

            driver.Manage().Window.FullScreen();
            driver.Navigate().GoToUrl(BASE_URL);

            driver.FindElement(By.Name("txtUsername")).SendKeys(USERNAME);
            driver.FindElement(By.Name("txtPassword")).SendKeys(PASSWORD);
            driver.FindElement(By.Name("txtPassword")).SendKeys(Keys.Return);

            String welcomeMessage = driver.FindElement(By.Id("welcome")).Text;

            Assert.AreEqual(welcomeMessage, WELCOME_MESSAGE, "Welcome message is incorrect ");
            driver.Quit();
        }


        /**
        * Demonstrate launching Google Chrome web browser
        */
        // [TestMethod]
        public void OpenChrome()
        {
            IWebDriver driver = new ChromeDriver();
            PerformTask(driver);
        }

        /**
        * This method demonstrate launching Chrome headless mode
        * You can create an instance of ChromeOptions,
        * which has convenient methods for setting ChromeDriver-specific capabilities.
        * You can then pass the ChromeOptions object into the ChromeDriver constructor:
        * Please refer to http://chromedriver.chromium.org/capabilities
        */
        // [TestMethod]
        public void OpenChromeHeadless()
        {
            var options = new ChromeOptions();
            options.AddArguments("disable-infobars");
            options.AddArguments("headless");
            IWebDriver driver = new ChromeDriver(options);
            PerformTask(driver);
        }
        /**
       * This method demonstrate launching Firefox web browser
       */
        // [TestMethod]
        public void OpenFirefox()
        {
            IWebDriver driver = new FirefoxDriver();
            PerformTask(driver);
        }

        /**
        * This method demonstrate launching Firefox headless mode
        */
        // [TestMethod]
        public void OpenFirefoxHeadless()
        {
            var options = new FirefoxOptions();
            options.AddArguments("headless");
            IWebDriver driver = new FirefoxDriver(options);
            PerformTask(driver);
        }

        /**
        * This method demonstrate launching IE web browser
        */
        // [TestMethod]
        public void OpenIE()
        {
            InternetExplorerOptions options = new InternetExplorerOptions();
            options.IgnoreZoomLevel = true; //Ignoring the Zoom level setting 
            //options.FlakinessByIgnoringSecurityDomains(); //Ignoring the Security domains settings 
            options.EnableNativeEvents = false; //Disabling the NATIVE_EVENTS capability to ensure the typing speed with 64bit driver 
                                           //Launch IE browser
            var driver = new InternetExplorerDriver(options);
            PerformTask(driver);
        }

        /**
        * This method demonstrate launching Edge web browser
        */
        // [TestMethod]
        public void OpenEdge()
        {
            IWebDriver driver = new EdgeDriver();
            PerformTask(driver);
        }

        ///**
        //* This method demonstrate launching Opera  web browser
        //*/
        //// [TestMethod]
        //public void OpenOpera()
        //{
        //    OperaOptions options = new OperaOptions();
        //    options.BinaryLocation = "/Applications/Opera.app/Contents/MacOS/Opera";

        //    var driver = new OperaDriver(options);
        //    PerformTask(driver);

        //}

        /**
         * Demonstrate launching Safari web browser
         * <p>
         * Note :
         * Browser driver configuration is not required as browser has the driver inbuilt
         */
        // [TestMethod]
        public void OpenSafari()
        {
            var driver = new SafariDriver();
            PerformTask(driver);

        }

        /**
          * Demonstrate launching Chrome Mobile Emulator
          */
        // [TestMethod]
        public void OpenMobileEmulation()
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            chromeOptions.EnableMobileEmulation("iPhone X");
            //chromeOptions.EnableMobileEmulation("Google Nexus 5");
            IWebDriver driver = new ChromeDriver(chromeOptions);
 
            PerformTask(driver);
        }
    }

}