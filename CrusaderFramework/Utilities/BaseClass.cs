using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebDriverManager.DriverConfigs.Impl;
#pragma warning disable CS8618


namespace CrusaderFramework.Utilities
{
    public class BaseClass
    {

        protected IWebDriver Driver;

        [SetUp]
        public void Setup()
        {
            // го поврзуваме кодот со App.config file-от во кој е дефинирано кој broweser ќе се користи
            string browsername = ConfigurationManager.AppSettings["browser"] ?? "defaultBrowser";

            InitBrowser(browsername);

            // се додава извршено wait кој има ефекти на сите тестови
            GetDriver().Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);

            GetDriver().Url = "https://crusader.bransys.com/#/";
            GetDriver().Manage().Window.Maximize();
        }

        // метода со која иницираме browser со помош Driver Managera - вредностите се впишани во App.config
        public IWebDriver GetDriver()
        {
            return Driver;
        }

        private void InitBrowser(string browserName)
        {
            switch (browserName)
            {
                case "chrome":
                    new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                    Driver = new ChromeDriver();
                    break;

                case "firefox":
                    new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                    Driver = new FirefoxDriver();
                    break;

                case "edge":
                    new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                    Driver = new EdgeDriver();
                    break; 
            }
        }

        [TearDown]
        public void AfterTest()
        {
            if (Driver != null)
            {
                GetDriver().Quit();
            }
        }
    }
}
