using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Linq;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using TestFlightReservation.UserInputModels;

namespace TestFlightReservation
{
    [Binding]
    public class BookingFeatureSteps
    {
        private readonly IWebDriver driver;
        private HomePage _homePage;
        private SearchResultPage _searchResultPage;
        private CheckoutPage _checkoutPage;
        private BookingCompletionPage _bookingCompletionPage;
        private ScenarioContext _scenarioContext;
        public BookingFeatureSteps(IWebDriver driver, ScenarioContext scenarioContext)
        {
            this.driver = driver;
            _scenarioContext = scenarioContext;
            _homePage = new HomePage(driver);
            _searchResultPage = new SearchResultPage(driver);
            _checkoutPage = new CheckoutPage(driver);
            _bookingCompletionPage = new BookingCompletionPage(driver);
            _scenarioContext = scenarioContext;

        }
        [Given(@"A user navigate to login page")]
        public void GivenAUserNavigateToLoginPage()
        {
            _homePage.LoginFacility.NavigateToLoginPage();
        }
        
        [Given(@"I am on the home page")]
        public void GivenIAmOnTheHomePage()
        {
            _homePage.LoginFacility.CheckIfCurrentlyHomePage();
        }

        [When(@"Enter credentials")]
        public void WhenEnterCredentials(Table table)
        {
            dynamic userCreds = table.CreateDynamicInstance();
            _homePage.LoginFacility.EnterCredentials(new UserCredentials(userCreds.UserName, userCreds.Password));
        }
        
        [When(@"and submit the login")]
        public void WhenAndSubmitTheLogin()
        {
            _homePage.LoginFacility.SubmitLogin();
        }
        
        [When(@"I select flight type")]
        public void WhenISelectFlightType()
        {
            _homePage.ClickOnFlightLing();
        }
        
        [When(@"I enter the required information")]
        public void WhenIEnterTheRequiredInformation(Table table)
        {
            dynamic searchInfo = table.CreateDynamicInstance();
            _homePage.FillInformation(new SearchInformatoin(searchInfo.StartPoint, searchInfo.Destination, searchInfo.DepartureAfterSpecificDays
                , searchInfo.AdultPassengerNumber, searchInfo.ChildsPassngerNumber));
        }
                
        
        [When(@"I click search and wait to search result to be loaded")]
        public void WhenIClickSearch()
        {
            _homePage.ClickSearch();
            _searchResultPage.CheckIfPageLoaded();
        }
        
        [When(@"I select the desired flight and click book now, for example number (.*)")]
        public void WhenISelectTheDesiredFlightAndClickBookNow(int desiredIndexToBeChecked)
        {
            _searchResultPage.SelectSearchResult(desiredIndexToBeChecked);
        }
        
        [When(@"Now i will select my ticket based own my algorithm, so i'll choose shortest one")]
        public void WhenISelectTheDesiredFlightAndClickBuyNow()
        {
            int fastestRouteIndex = _searchResultPage.CheckFastestRoute();
            _searchResultPage.SelectSearchResult(fastestRouteIndex);
            _scenarioContext.Add("fastestRoute", fastestRouteIndex);
        }
        
        [When(@"I fill billing infomation and payment information as below")]
        public void WhenIFillBillingInfomationAndPaymentInformationAsBelow(Table table)
        {
            dynamic info = table.CreateDynamicInstance();
            BookingForm bookingForm = new BookingForm(info.Title.ToString(), info.Name.ToString(), info.Surname.ToString(), info.Email.ToString(), info.Phone.ToString(), info.Birthday.ToString(), info.ExpirationDate.ToString()
                , info.Nationality.ToString(), info.CardType.ToString(), info.CardNumber.ToString(), info.CardExpiryYear.ToString(), info.CVV.ToString(), info.PassportNumber.ToString());
            _bookingCompletionPage.FillingBillingInformation(bookingForm);
        }
        
        [When(@"check about accepting the rules")]
        public void WhenCheckAboutAcceptingTheRules()
        {
            _bookingCompletionPage.AcceptTermsAndCheckThebox();
        }
        
        [When(@"submitting the form")]
        public void WhenSubmittingTheForm()
        {
            _bookingCompletionPage.SubmitTheForm();
        }
        
        [Then(@"user should be logged in then return home")]
        public void ThenUserShouldBeLoggedIn()
        {
            _homePage.LoginFacility.CheckIfUserIsLoggedInAndGoHome();
        }
        
        [Then(@"The list of available flights should be ascendingly ordered")]
        public void ThenTheListOfAvailableFlightsShouldBeAscendinglyOrdered()
        {
            var acualPriceList = _searchResultPage.GetPricesList();
            var expectedPriceList = acualPriceList.OrderBy(x => x);
            Assert.IsTrue(expectedPriceList.SequenceEqual(acualPriceList), "Seems that results are not ordered asccendingly!");
        }
        
        [Then(@"flights directions must match user input")]
        public void ThenFlightsDirectionsMustMatchUserInput(Table table)
        {
            dynamic userDirection = table.CreateDynamicInstance();
            Assert.That(_searchResultPage.CheckIfDestinationAsRequired(new UserDirection(userDirection.StartPoint, userDirection.Destination)), "Seems there's a difference in the destinations between results and your request");
        }

        [Then(@"I should be directed to the summary page")]
        public void ThenIShouldBeDirectedToTheSummaryPage()
        {
            _checkoutPage.CheckIfPageLoaded();
        }
        
        [Then(@"All information must match user choice including the below")]
        public void ThenAllInformationMustMatchUserChoiceIncludingTheBelow(Table table)
        {
            dynamic userBookingInformation = table.CreateDynamicInstance();
            var bookInfo = _checkoutPage.GetBookingInformatoin();
            Assert.That(bookInfo.ArrivalDate == userBookingInformation.ArrivalDate
                && bookInfo.DepartureDate == userBookingInformation.DepartureDate &&
                bookInfo.From == userBookingInformation.From &&
                bookInfo.To == userBookingInformation.To &&
                bookInfo.TotalAmount == userBookingInformation.TotalAmount, "Seems the values in the summary not correct");
        }      
        
        [Then(@"result will be printed out about amount of ours")]
        public void ThenResultWillBePrintedOutAboutAmountOfOurs()
        {
            TestContext.WriteLine(_scenarioContext.Get<int>("fastestRoute"));
        }
    }
}
