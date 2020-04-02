using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestFlightReservation
{
    public class BookingCompletionPage
    {
        

        private IWebDriver _driver;
        public SelectElement SelectTitle => new SelectElement(_driver.FindElement(By.XPath(BookingCompletionPageConstants.SelectTitlePath)));
        public IWebElement NameInput => _driver.FindElement(By.XPath(BookingCompletionPageConstants.NameInputPath));
        public IWebElement SurName => _driver.FindElement(By.XPath(BookingCompletionPageConstants.SurNamePath));
        public IWebElement Email => _driver.FindElement(By.XPath(BookingCompletionPageConstants.EmailPath));
        public IWebElement Phone => _driver.FindElement(By.XPath(BookingCompletionPageConstants.PhonePath));
        public IWebElement Birthday => _driver.FindElement(By.XPath(BookingCompletionPageConstants.BirthdayPath));
        public IWebElement PassportNumber => _driver.FindElement(By.XPath(BookingCompletionPageConstants.PassportNumberPath));
        public IWebElement ExpirationDate => _driver.FindElement(By.XPath(BookingCompletionPageConstants.ExpirationDatePath));
        public IWebElement NationalityAnchor => _driver.FindElement(By.XPath(BookingCompletionPageConstants.NationalityAnchorPath));
        public IWebElement NationalityItemSelect => _driver.FindElement(By.XPath(BookingCompletionPageConstants.NationalityItemSelectPath1));
        public SelectElement SelectCardType => new SelectElement(_driver.FindElement(By.XPath(BookingCompletionPageConstants.SelectCardTypePath)));
        public IWebElement CardNumber => _driver.FindElement(By.XPath(BookingCompletionPageConstants.CardNumberPath));
        public SelectElement CardExpiryYear => new SelectElement(_driver.FindElement(By.XPath(BookingCompletionPageConstants.CardExpiryYearPath)));
        public IWebElement CVV => _driver.FindElement(By.XPath(BookingCompletionPageConstants.CVVPath));
        public IWebElement CheckBox => _driver.FindElement(By.XPath(BookingCompletionPageConstants.CheckBoxPath));
        public IWebElement SubmitButton => _driver.FindElement(By.XPath(BookingCompletionPageConstants.SubmitButtonPath));




        private WebDriverWait _wait { get; set; }
        public BookingCompletionPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10))
            {
                PollingInterval = TimeSpan.FromSeconds(0.5)
            };
        }
        public void FillingBillingInformation(BookingForm bookingForm)
        {
            SelectTitle.SelectByValue(bookingForm.Title);
            NameInput.SendKeys(bookingForm.Name);
            SurName.SendKeys(bookingForm.Surname);
            Email.SendKeys(bookingForm.Email);
            Phone.SendKeys(bookingForm.Phone);
            Birthday.SendKeys(bookingForm.Birthday.ToString());
            PassportNumber.SendKeys(bookingForm.PassportNumber);
            ExpirationDate.SendKeys(bookingForm.ExpirationDate);
            NationalityAnchor.Click();
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(BookingCompletionPageConstants.NationalityItemSelectPath)));
            NationalityItemSelect.Click();
            SelectCardType.SelectByValue(bookingForm.CardType);
            CardNumber.SendKeys(bookingForm.CardNumber);
            CardExpiryYear.SelectByValue(bookingForm.CardExpiryYear);
            CVV.SendKeys(bookingForm.CVV);
            

        }
        public void AcceptTermsAndCheckThebox()
        {
            Actions action = new Actions(_driver);
            action.MoveToElement(CheckBox).Click().Perform();
        }
        public void SubmitTheForm()
        {
            SubmitButton.Submit();
        }
    }

    public static class BookingCompletionPageConstants
    {
        public const string SelectTitlePath = "//form[@name='ticketBookingForm']//section//div[@class='row row-reverse']//div[@class='form-group'][label='Title']//select[@id='title']";
        public const string NameInputPath = "//form[@name='ticketBookingForm']//section//div[@class='row row-reverse']//div[@class='form-group'][label='Name']//input[@id='name']";
        public const string SurNamePath = "//form[@name='ticketBookingForm']//section//div[@class='row row-reverse']//div[@class='form-group'][label='Surname']//input[@id='surname']";
        public const string EmailPath = "//form[@name='ticketBookingForm']//section//div[@class='row']//div[@class='form-group'][label='Email']//input[@id='email']";
        public const string PhonePath = "//form[@name='ticketBookingForm']//section//div[@class='row']//div[@class='form-group'][label='Phone']//input[@id='phone']";
        public const string BirthdayPath = "//form[@name='ticketBookingForm']//section//div[@class='row row-reverse']//div[@class='form-group'][label='Birthday']//input[@id='birthday']";
        public const string PassportNumberPath = "//form[@name='ticketBookingForm']//section//div[@class='row row-reverse']//div[@class='form-group'][label='Passport No.']//input[@id='cardno']";
        public const string ExpirationDatePath = "//form[@name='ticketBookingForm']//section//div[@class='row row-reverse']//div[@class='form-group'][label='Expiration Date']//input[@id='expiration']";
        public const string NationalityAnchorPath = "//form[@name='ticketBookingForm']//section//div[@class='row row-reverse']//div[@class='form-group'][label='Nationality']//a[@href='javascript:void(0)']";
        public const string NationalityItemSelectPath = "//div[@class='select2-drop select2-display-none select2-with-searchbox select2-drop-active'][@id='select2-drop']//ul[@class='select2-results']//li[1]//div[@class='select2-result-label']//span[@class='select2-match']";
        public const string NationalityItemSelectPath1 = "//div[@class='select2-drop select2-display-none select2-with-searchbox select2-drop-active'][@id='select2-drop']//ul[@class='select2-results']//li[1]";
        public const string SelectCardTypePath = "//div[contains(@class,'bg-white-shadow')][h6='Payment Types']//div[@class='payment-desc']//div[contains(@class,'row')]//div[label='Card Type']//select[@id='cardtype']";
        public const string CardNumberPath = "//div[contains(@class,'bg-white-shadow')][h6='Payment Types']//div[@class='payment-desc']//div[contains(@class,'row')]//div[label='Card Number']//input[@id='card-number']";
        public const string CardExpiryYearPath = "//div[contains(@class,'bg-white-shadow')][h6='Payment Types']//div[@class='payment-desc']//div[@class='row row-reverse']//select[@id='expiry-year']";
        public const string CVVPath = "//div[contains(@class,'bg-white-shadow')][h6='Payment Types']//div[@class='payment-desc']//div[@class='row row-reverse']//div[label='Card CVV']//input[@id='cvv']";
        public const string CheckBoxPath = "//button[@id='confirmBooking']";
        public const string SubmitButtonPath = "//input[@id='acceptTerm']";
    }
}
