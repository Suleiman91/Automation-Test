using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using TestFlightReservation.UserInputModels;

namespace TestFlightReservation
{
    public class SearchResultPage
    {
        private IWebDriver _driver;
        
        
        public System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> ListOfPriceResults => _driver.FindElements(By.XPath(SearchResultPageConstatnts.PricesListResultsPath));
        public System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> ListOfResults => _driver.FindElements(By.XPath(SearchResultPageConstatnts.ListResPath));
        public IWebElement FilterSearchTitle => _driver.FindElement(By.XPath(SearchResultPageConstatnts.FilterSearchTitlePath));
        private WebDriverWait _wait { get; set; }
        public SearchResultPage(IWebDriver driver)
        {
            _driver = driver;
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(10))
            {
                PollingInterval = TimeSpan.FromSeconds(0.5)
            };
        }
        public List<int> GetPricesList()
        {
            List<int> tempList = new List<int>();
            for(int i = 0; i < ListOfPriceResults.Count; i++)
            {
                tempList.Add(int.Parse(ListOfPriceResults[i].Text.Split(' ')[1]));
            }
            return tempList;
        }
        public bool CheckIfDestinationAsRequired(UserDirection userDirection)
        {
            for(int i = 1; i < ListOfResults.Count; i++)
            {
                IJavaScriptExecutor jse = (IJavaScriptExecutor)_driver;
                Actions actions = new Actions(_driver);
                _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(SearchResultPageConstatnts.ClickableDivPath + (i - 1).ToString() + "']")));
                var elem = ListOfResults[i].FindElement(By.XPath(SearchResultPageConstatnts.ClickableDivPath + (i - 1).ToString() + "']"));
                actions.MoveToElement(elem).Perform();
                jse.ExecuteScript("arguments[0].click()", elem);
                _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(SearchResultPageConstatnts.ListResPath + "["+ i +"]" + SearchResultPageConstatnts.ListOfDetails)));
                var numOfStations = ListOfResults[i].FindElements(By.XPath(SearchResultPageConstatnts.ListResPath + "[" + i + "]" + SearchResultPageConstatnts.ListOfDetails)).Count;
                var source = GetStringNameForFlight(SearchResultPageConstatnts.SourceStringPath, i, 1);
                var dest = GetStringNameForFlight(SearchResultPageConstatnts.DestinationStringPath, i, numOfStations);

                if (source != userDirection.StartPoint || dest != userDirection.Destination) return false;
            }


            return true;
        }

        private string GetStringNameForFlight(string destOrSource, int index, int detailsIndex)
        {
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(SearchResultPageConstatnts.ListResPath + "[" + index + "]" + destOrSource)));
            return ListOfResults[index].FindElement(By.XPath(SearchResultPageConstatnts.ListResPath + SearchResultPageConstatnts.ListOfDetails + "[" + detailsIndex + "]" + destOrSource )).Text;
        }
        public int CheckFastestRoute()
        {
            int chosenIndex = 1;
            double currentBestRoute = 1000;
            for (int i = 1; i < ListOfResults.Count; i++)
            {
                IJavaScriptExecutor jse = (IJavaScriptExecutor)_driver;
                Actions actions = new Actions(_driver);
                _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(SearchResultPageConstatnts.ClickableDivPath + (i - 1).ToString() + "']")));
                var elem = ListOfResults[i].FindElement(By.XPath(SearchResultPageConstatnts.ClickableDivPath + (i - 1).ToString() + "']"));
                actions.MoveToElement(elem).Perform();
                jse.ExecuteScript("arguments[0].click()", elem);
                _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(SearchResultPageConstatnts.ListResPath + "[" + i + "]" + SearchResultPageConstatnts.ListOfDetails)));
                var numOfStations = ListOfResults[i].FindElements(By.XPath(SearchResultPageConstatnts.ListResPath + "[" + i + "]" + SearchResultPageConstatnts.ListOfDetails)).Count - 1;
                double tempAccumlator = 0;
                for (int j = 0; j < numOfStations; j++)
                {
                    var departure = GetTimeForFlight(SearchResultPageConstatnts.DepartureTimePath, i,j+1);
                    var arrival = GetTimeForFlight(SearchResultPageConstatnts.ArrivalTimePath, i,j+1);
                    DateTime dateTimeDep = DateTime.ParseExact(departure+":00", "HH:mm:ss", CultureInfo.InvariantCulture);
                    DateTime dateTimeArr = DateTime.ParseExact(arrival + ":00", "HH:mm:ss", CultureInfo.InvariantCulture);

                    tempAccumlator = tempAccumlator + Math.Abs((dateTimeArr - dateTimeDep).TotalHours);

                }
                if (tempAccumlator < currentBestRoute)
                {
                    currentBestRoute = tempAccumlator;
                    chosenIndex = i;
                }
                
            }
            return chosenIndex;
        }
        private string GetTimeForFlight(string destOrSource, int index, int indexDetails)
        {
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(SearchResultPageConstatnts.ListResPath + "[" + index + "]" + destOrSource)));
            return ListOfResults[index].FindElement(By.XPath(SearchResultPageConstatnts.ListResPath + "[" + index + "]" + SearchResultPageConstatnts.ListOfDetails + "[" + indexDetails +"]" + destOrSource)).Text;
        }
        public void SelectSearchResult(int index)
        {
            _driver.FindElement(By.XPath(SearchResultPageConstatnts.ListResPath + "["+index+"]"+ "//form[@class='row']")).Submit();
        }
        public void CheckIfPageLoaded()
        {
            _wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(By.XPath(SearchResultPageConstatnts.SearchPagePointOfMark)));
        }
    }


    public static class SearchResultPageConstatnts
    {
        public static string ListResPath = "//ul[@id='LIST']//li[contains(@class,'item')]";
        public static string ClickableDivPath = "//div[@class='theme-search-results-item-preview']//div[@href='#searchResultsItem-";
        public static string SourceStringPath = "//ul[@class='theme-search-results-item-flight-details-schedule-list']//li//div[@class='theme-search-results-item-flight-details-schedule-destination']/div[1]/p/b";
        public static string DestinationStringPath = "//ul[@class='theme-search-results-item-flight-details-schedule-list']//li//div[@class='theme-search-results-item-flight-details-schedule-destination']/div[3]/p/b";
        public static string FilterSearchTitlePath = "//h4[contains(text(),'Filter Search')]";
        public static string ListOfDetails = "/div[contains(@class,'theme-search-results-item')]//div[@class='theme-search-results-item-collapse collapse show']//div[@class='theme-search-results-item-extend']//div[@class='theme-search-results-item-extend-inner']";
        public static string PricesListResultsPath = "//ul[@id='LIST']//li//p[@class='theme-search-results-item-price-tag']//strong";
        public static string DepartureTimePath = "//ul[@class='theme-search-results-item-flight-details-schedule-list']//li//div[@class='theme-search-results-item-flight-details-schedule-time']/span[1]";
        public static string ArrivalTimePath = "//ul[@class='theme-search-results-item-flight-details-schedule-list']//li//div[@class='theme-search-results-item-flight-details-schedule-time']/span[3]";
        public static string SearchPagePointOfMark = "//h4[contains(text(),'Filter Search')]";
    }
}
