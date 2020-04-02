using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestFlightReservation
{
    public static class SeleniumGetActions
    {

        public static string GetTextFromValueAttribute(TypesOfElementsPaths pathType, string pathValue, IWebDriver driver)
        {
            switch (pathType)
            {
                case TypesOfElementsPaths.Name:
                {
                    return driver.FindElement(By.Name(pathValue)).GetAttribute("value");
                }
                case TypesOfElementsPaths.Id:
                {
                    return driver.FindElement(By.Id(pathValue)).GetAttribute("value");
                    }
                case TypesOfElementsPaths.XPath:
                {
                    return driver.FindElement(By.XPath(pathValue)).GetAttribute("value");
                    }
                case TypesOfElementsPaths.CssSelector:
                {
                    return driver.FindElement(By.CssSelector(pathValue)).GetAttribute("value");
                }                    
            }
            throw new Exception($"No Element With Identifer Called {pathValue} was Found!");
        }

        public static string GetTextFromValueAttributePOM(this IWebElement element)
        {
            return element.GetAttribute("value");
        }
        public static string GetTextFromDDL(TypesOfElementsPaths pathType, string pathValue, IWebDriver driver)
        {
            switch (pathType)
            {
                case TypesOfElementsPaths.Name:
                {
                    return new SelectElement(driver.FindElement(By.Name(pathValue)))
                        .AllSelectedOptions.SingleOrDefault()?.Text;
                }
                case TypesOfElementsPaths.Id:
                {
                    return new SelectElement(driver.FindElement(By.Id(pathValue)))
                        .AllSelectedOptions.SingleOrDefault()?.Text;
                    }
                case TypesOfElementsPaths.XPath:
                {
                    return new SelectElement(driver.FindElement(By.XPath(pathValue)))
                        .AllSelectedOptions.SingleOrDefault()?.Text;
                }
                case TypesOfElementsPaths.CssSelector:
                {
                    return new SelectElement(driver.FindElement(By.CssSelector(pathValue)))
                        .AllSelectedOptions.SingleOrDefault()?.Text;
                }
            }
            throw new NoSuchElementException($"No Element With Identifer Called {pathValue} was Found!");
        }
    }
}
