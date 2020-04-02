using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestFlightReservation
{
    public static class ExpectedConditionCustom
    {
        public static Func<IWebDriver, bool> ElementIsVisible(IWebElement element)
        {
            return (driver) =>
            {
                try
                {
                    return element.Displayed;
                }
                catch (Exception)
                {
                    return false;
                }
            };
        }
    }
}
