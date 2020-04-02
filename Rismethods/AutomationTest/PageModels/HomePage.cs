using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using TestFlightReservation.UserInputModels;

namespace TestFlightReservation
{
    public class HomePage
    {
        
        public IWebElement FlightLink => _driver.FindElement(By.XPath(HomePageConstants.FlightLinkPath));
        public DestinationBox FromInput { get; set; }
        public DestinationBox ToInput { get; set; }
        public LoginFacility LoginFacility { get; set; }
        public DatePicker OneWayDatePicker { get; set; }

        public PassengersBox AdultsPassengers { get; set; }
        public PassengersBox ChildPassengers { get; set; }
        public IWebElement SearchButton => _driver.FindElement(By.XPath(HomePageConstants.SearchButton));
        private IWebDriver _driver;
        public HomePage(IWebDriver driver)
        {
            _driver = driver;
            FromInput = new DestinationBox(_driver, "from");
            ToInput = new DestinationBox(_driver, "to");
            AdultsPassengers = new PassengersBox(_driver, "Adults");
            ChildPassengers = new PassengersBox(_driver, "Child");
            OneWayDatePicker = new DatePicker(_driver, "Depart");
            LoginFacility = new LoginFacility(_driver);
        }
        public void ClickOnFlightLing()
        {
            FlightLink.Click();
        }
        public void FillInformation(SearchInformatoin searchInfromation)
        {
            FromInput.ChooseDestination(searchInfromation.StartPoint);
            ToInput.ChooseDestination(searchInfromation.Destination);
            AdultsPassengers.SetNumberOfPassengers(searchInfromation.AdultPassengerNumber);
            ChildPassengers.SetNumberOfPassengers(searchInfromation.ChildsPassngerNumber);
            OneWayDatePicker.SetDate(searchInfromation.DepartureAfterSpecificDays);
        }
        public void ClickSearch()
        {
            SearchButton.Click();
        }

    }
    public class DestinationBox
    {
        
        private string AnchorPath = "a[href*='javascript:void(0)']";
        
        private IWebDriver _driver;        
        private IWebElement AnchorWithJSVoid => 
            _driver.FindElement(By.CssSelector(AnchorPath));
        private IWebElement InputSearchBox =>
            _driver.FindElement(By.XPath(HomePageConstants.SearchBoxPath));
        private IWebElement SelectableListItem =>
            _driver.FindElement(By.XPath(HomePageConstants.ListItemPath));
        private IWebElement SelectableListItemHighlighted =>
            _driver.FindElement(By.XPath(HomePageConstants.HighlightedPath));
        public DestinationBox(IWebDriver driver, string dest)
        {
            _driver = driver;
            if (dest == "to")
            {
                AnchorPath = HomePageConstants.AnchorPathTo + AnchorPath;
            }
            else
            {
                AnchorPath = HomePageConstants.AnchorPathFrom + AnchorPath;
            }
        }
        public void ChooseDestination(string city)
        {
            AnchorWithJSVoid.Click();
            WebDriverWait wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10))
            {
                PollingInterval = TimeSpan.FromSeconds(0.5)
            };
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(HomePageConstants.SearchBoxPath)));
            InputSearchBox.Click();
            InputSearchBox.SendKeys(city);
            wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(HomePageConstants.HighlightedPath)));
            SelectableListItemHighlighted.Click();

        }
    }
    public class PassengersBox
    {
        private IWebDriver _driver;
        public PassengersBox(IWebDriver driver, string passengerType)
        {
            _driver = driver;
            TargetedPassengersPath = "//div[@class='row no-gutters']//div//div[label[contains(text(), '"+ passengerType + " ')]]";
            CurrentNumberReserved = TargetedPassengersPath + "//div[@class='form-icon-left']//div[@class='input-group  bootstrap-touchspin bootstrap-touchspin-injected']//input[@class='form-control touch-spin-03 form-readonly-control']";
            IncreaseButtonPath = TargetedPassengersPath + "//span[@class='input-group-btn-vertical']//button[@class='btn btn-white bootstrap-touchspin-up ']";
            DecreaseButtonPath = TargetedPassengersPath + "//span[@class='input-group-btn-vertical']//button[@class='btn btn-white bootstrap-touchspin-down ']";

        }
        private string TargetedPassengersPath;
        private string CurrentNumberReserved;
        private string IncreaseButtonPath;
        private string DecreaseButtonPath;
        public IWebElement IncreaseButton => _driver.FindElement(By.XPath(IncreaseButtonPath));
        public IWebElement DecreaseButton => _driver.FindElement(By.XPath(DecreaseButtonPath));

        public IWebElement ReservedNumber => _driver.FindElement(By.XPath(CurrentNumberReserved));
        public void SetNumberOfPassengers(int desiredNumber)
        {
            int currnetNumber = int.Parse(ReservedNumber.GetAttribute("value").Trim());
            if(desiredNumber == currnetNumber)
            {
                return;
            }
            int loopTimes = Math.Abs(currnetNumber - desiredNumber);
            if(desiredNumber < currnetNumber)
            {
                for(int i =0; i < loopTimes; i++)
                {
                    DecreaseButton.Click();
                }
            }
            else
            {
                for (int i = 0; i < loopTimes; i++)
                {
                    IncreaseButton.Click();
                }
            }
        }

    }
    public class DatePicker
    {
        private IWebDriver _driver;
        public DatePicker(IWebDriver driver, string desiredDateBox)
        {
            _driver = driver;
            DataPickerPath = "//div[@id='airDatepickerRange-flight']//div[@class='form-group'][label[contains(text(),'" + desiredDateBox + "')]]//input[@id='FlightsDateStart']";
            NextDivSiblings = HomePageConstants.DaysContentPath + "//div[contains(text(),'" + DateTime.Today.Day + "')]//following-sibling::div";
        }
        private string DataPickerPath;
        
        private string NextDivSiblings;
        public IWebElement DatePickerIcon => _driver.FindElement(By.XPath(DataPickerPath));
        public IWebElement DaysContent => _driver.FindElement(By.XPath(HomePageConstants.DaysContentPath));
        public IWebElement NextAction => _driver.FindElement(By.XPath(HomePageConstants.NextActionPath));  
        public void SetDate(int addedDays)
        {
            DatePickerIcon.Click();
            var nextSiblings = _driver.FindElements(By.XPath(NextDivSiblings));
            if (addedDays <= nextSiblings.Count)
            {
                ClickSelectedDate(addedDays);
            }
            else
            {
                NextAction.Click();
                ClickSelectedDate(addedDays);

            }
        }

        private void ClickSelectedDate(int addedDays)
        {
            int selectedDay = DateTime.Today.AddDays(addedDays).Day;
            var elem = _driver.FindElement(By.XPath(HomePageConstants.DaysContentPath + "//div[contains(text(),'" + selectedDay + "')]"));
            IJavaScriptExecutor jse = (IJavaScriptExecutor)_driver;
            jse.ExecuteScript("arguments[0].click()", elem);
        }
    }

    public class LoginFacility
    {
        

        public IWebElement LoginButton => _driver.FindElement(By.XPath(HomePageConstants.LoginAnchorPath));
        public IWebElement LoginMenuButton => _driver.FindElement(By.XPath(HomePageConstants.LoginMenuButtonPath));
        public IWebElement PasswordField => _driver.FindElement(By.XPath(HomePageConstants.PasswordFieldPath));
        public IWebElement EmailInput => _driver.FindElement(By.XPath(HomePageConstants.EmailInputPath));
        public IWebElement FormButton => _driver.FindElement(By.XPath(HomePageConstants.FormButtonPath));


        private IWebDriver _driver;
        private WebDriverWait _wait;
        public LoginFacility(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10))
            {
                PollingInterval = TimeSpan.FromSeconds(0.5)
            };
        }
        public void Login(UserCredentials user)
        {
            NavigateToLoginPage();
            EnterCredentials(user);
            SubmitLogin();
            CheckIfUserIsLoggedInAndGoHome();            
            CheckIfCurrentlyHomePage();

        }
        public void CheckIfUserIsLoggedInAndGoHome()
        {
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible((By.XPath(HomePageConstants.CheckLoginPath))));
            var homeButton = _driver.FindElement(By.XPath(HomePageConstants.HomeBackButtonPath));
            homeButton.Click();
        }
        public void SubmitLogin()
        {
            FormButton.Submit();
        }
        public void EnterCredentials(UserCredentials user)
        {
            EmailInput.SendKeys(user.UserName);
            PasswordField.SendKeys(user.Password);
        }
        public void NavigateToLoginPage()
        {
            LoginButton.Click();
            LoginMenuButton.Click();
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible((By.XPath(HomePageConstants.CheckBeforeLoginPath))));
        }
        public void CheckIfCurrentlyHomePage()
        {
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible((By.XPath(HomePageConstants.FlightLinkPath))));
        }
    }
    public static class HomePageConstants
    {
        public const string CheckBeforeLoginPath = "//div[@class='container']//h3[contains(text(),'Login')]";
        public const string CheckLoginPath = "//nav//ul[@class='menu-vertical-01']";
        public const string HomeBackButtonPath = "//*[@id='mobileMenuMain']/nav/ul[1]/li/a";
        public const string SearchButton = "//div[@id='flights']/div/div/form/div/div[3]/div[4]/button";
        public const string FlightLinkPath = "//a[@href='#flights']";
        public const string LoginAnchorPath = "//header[@id='header-waypoint-sticky']//div[@class='header-top']//div[@class='row align-items-center no-gutters']/div[2]//ul/li[3]//a[@id='dropdownCurrency']";
        public const string LoginMenuButtonPath = "//div[@class='dropdown-menu dropdown-menu-right show']//div//a[@href='https://www.phptravels.net/login']";
        public const string PasswordFieldPath = "//input[@type='password']";
        public const string EmailInputPath = "//input[@type='email']";
        public const string FormButtonPath = "//button[@type='submit']";
        public const string AnchorPathFrom = "#s2id_location_from>";
        public const string AnchorPathTo = "#s2id_location_to>";
        public const string DaysContentPath = "//div[@id='datepickers-container']//div[@class='datepicker -bottom-left- -from-bottom- active']//div[@class='datepicker--content']//div[@class='datepicker--cells datepicker--cells-days']";
        public const string NextActionPath = "//div[@id='datepickers-container']//div[@class='datepicker -bottom-left- -from-bottom- active']//nav[@class='datepicker--nav']//div[@class='datepicker--nav-action'][@data-action='next']";
        public const string SearchBoxPath = "//div[@class='select2-search']//input[@class='select2-input select2-focused']";
        public const string ListItemPath = "//ul[@class='select2-results']//li[@class='select2-results-dept-0 select2-result select2-result-selectable']";
        public const string HighlightedPath = "//ul[@class='select2-results']//li[@class='select2-results-dept-0 select2-result select2-result-selectable select2-highlighted']//div[@class='select2-result-label']";
    }
}
