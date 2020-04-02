using BoDi;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using TechTalk.SpecFlow;

namespace TestFlightReservation
{
    [Binding]
    public sealed class Hooks 
    {
        private readonly IObjectContainer container;
        private const string _baseUrl = "https://www.phptravels.net/home";

        public Hooks(IObjectContainer container)
        {
            this.container = container;
        }
        [BeforeScenario]
        public void BeforeScenario()
        {
            ChromeDriver driver = new ChromeDriver();
            container.RegisterInstanceAs<IWebDriver>(driver);
            driver.Navigate().GoToUrl(_baseUrl);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            var driver = container.Resolve<IWebDriver>();

            if (driver != null)
            {
                driver.Quit();
                driver.Dispose();
            }
        }
    }
}
