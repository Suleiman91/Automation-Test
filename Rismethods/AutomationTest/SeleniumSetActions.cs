using System;
using System.Collections.Generic;
using System.Text;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace TestFlightReservation
{
    
    public static class SeleniumSetActions
    {
        public static void SendKeys(TypesOfElementsPaths pathType, string pathValue, string value, IWebDriver driver)
        {
            switch (pathType)
            {
                case TypesOfElementsPaths.Name:
                {
                    driver.FindElement(By.Name(pathValue)).SendKeys(value);
                    break;
                }
                case TypesOfElementsPaths.Id:
                {
                    driver.FindElement(By.Id(pathValue)).SendKeys(value);
                    break;
                }
                case TypesOfElementsPaths.XPath:
                {
                    driver.FindElement(By.XPath(pathValue)).SendKeys(value);
                    break;
                }
                case TypesOfElementsPaths.CssSelector:
                {
                    driver.FindElement(By.CssSelector(pathValue)).SendKeys(value);
                    break;
                }
            }
        }
        public static void Click(TypesOfElementsPaths pathType, string pathValue, IWebDriver driver)
        {
            switch (pathType)
            {
                case TypesOfElementsPaths.Name:
                {
                    driver.FindElement(By.Name(pathValue)).Click();
                    break;
                }
                case TypesOfElementsPaths.Id:
                {
                    driver.FindElement(By.Id(pathValue)).Click();
                    break;
                }
                case TypesOfElementsPaths.XPath:
                {
                    driver.FindElement(By.XPath(pathValue)).Click();
                    break;
                }
                case TypesOfElementsPaths.CssSelector:
                {
                    driver.FindElement(By.CssSelector(pathValue)).Click();
                    break;
                }
            }
        }

        public static void ClickPOM(this IWebElement element)
        {
            element.Click();
        }
        public static void Submit(TypesOfElementsPaths pathType, string pathValue, IWebDriver driver)
        {
            switch (pathType)
            {
                case TypesOfElementsPaths.Name:
                {
                    driver.FindElement(By.Name(pathValue)).Submit();
                    break;
                }
                case TypesOfElementsPaths.Id:
                {
                    driver.FindElement(By.Id(pathValue)).Submit();
                    break;
                }
                case TypesOfElementsPaths.XPath:
                {
                    driver.FindElement(By.XPath(pathValue)).Submit();
                    break;
                }
                case TypesOfElementsPaths.CssSelector:
                {
                    driver.FindElement(By.CssSelector(pathValue)).Submit();
                    break;
                }
            }
        }

        public static void SubmitPOM(this IWebElement element)
        {
            element.Submit();
        }
        public static void SelectOption(TypesOfElementsPaths pathType, string pathValue, string value, IWebDriver driver)
        {
            switch (pathType)
            {
                case TypesOfElementsPaths.Name:
                {
                    new SelectElement(driver.FindElement(By.Name(pathValue))).SelectByText(value);
                    break;
                }
                case TypesOfElementsPaths.Id:
                {
                    new SelectElement(driver.FindElement(By.Id(pathValue))).SelectByText(value);
                    break;
                }
                case TypesOfElementsPaths.XPath:
                {
                    new SelectElement(driver.FindElement(By.XPath(pathValue))).SelectByText(value);
                    break;
                    }
                case TypesOfElementsPaths.CssSelector:
                {
                    new SelectElement(driver.FindElement(By.CssSelector(pathValue))).SelectByText(value);
                    break;
                }
            }            
        }

        public static void SelectOptionPOM(this IWebElement element, string value)
        {
            new SelectElement(element).SelectByText(value);
        }
    }
}
