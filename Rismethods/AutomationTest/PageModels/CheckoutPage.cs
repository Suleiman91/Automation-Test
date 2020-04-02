using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestFlightReservation
{
    public class CheckoutPage
    {
        
        private IWebDriver _driver;
        private WebDriverWait _wait { get; set; }
        public IWebElement DepartureDate => _driver.FindElement(By.XPath(CheckoutPageConstants.DepartureDatePath));
        public IWebElement ArrivalDate => _driver.FindElement(By.XPath(CheckoutPageConstants.ArrivalDatePath));
        public IWebElement From => _driver.FindElement(By.XPath(CheckoutPageConstants.FromPath));
        public IWebElement To => _driver.FindElement(By.XPath(CheckoutPageConstants.ToPath));
        public IWebElement TotalAmount => _driver.FindElement(By.XPath(CheckoutPageConstants.TotalAmountPath));

        public CheckoutPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10))
            {
                PollingInterval = TimeSpan.FromSeconds(0.5)
            };
        }
        public BookingInformation GetBookingInformatoin()
        {
            DateTime departure = DateTime.Parse(DepartureDate.Text);
            DateTime arrival = DateTime.Parse(ArrivalDate.Text);
            string from = From.Text.Split(' ')[1];
            string to = To.Text.Split(' ')[1];
            var amountList = TotalAmount.Text.Split(' ');
            float amount = float.Parse(amountList[amountList.Length-1]);
            return new BookingInformation(departure,arrival,from,to,amount);
        }
        public void CheckIfPageLoaded()
        {
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(CheckoutPageConstants.PagerMarker)));
        }
    }
    public static class CheckoutPageConstants
    {
        public const string DepartureDatePath = "//div[@class='booking-selection-box']//div[@class='content']//ul[contains(@class,'booking-amount-list')]//li[1]/span";
        public const string ArrivalDatePath = "//div[@class='booking-selection-box']//div[@class='content']//ul[contains(@class,'booking-amount-list')]//li[2]/span";
        public const string ToPath = "//div[@class='booking-selection-box']//div[@class='content']//div[contains(@class,'hotel-room-sm-item')]//div[@class='the-room-item'][2]//div[@class='clear']//h6";
        public const string FromPath = "//div[@class='booking-selection-box']//div[@class='content']//div[contains(@class,'hotel-room-sm-item')]//div[@class='the-room-item'][2]//h6";
        public const string TotalAmountPath = "//div[@class='booking-selection-box']//div[@class='content']//ul[@class='summary-price-list']//li[@class='total']";
        public const string PagerMarker = "//div[@class='booking-selection-box']//div[@class='content']//ul[@class='summary-price-list']//li[@class='total']";
    }
}
