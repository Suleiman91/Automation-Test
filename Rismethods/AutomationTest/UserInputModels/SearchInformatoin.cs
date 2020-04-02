using System;
using System.Collections.Generic;
using System.Text;

namespace TestFlightReservation.UserInputModels
{
    public class SearchInformatoin
    {
        public string StartPoint { get; set; }
        public string Destination { get; set; }
        public int DepartureAfterSpecificDays { get; set; }
        public int AdultPassengerNumber { get; set; }
        public int ChildsPassngerNumber { get; set; }
        public SearchInformatoin(string startPoint, string destination, int afterDays, int adultNum, int childNum)
        {
            StartPoint = startPoint;
            Destination = destination;
            DepartureAfterSpecificDays = afterDays;
            AdultPassengerNumber = adultNum;
            ChildsPassngerNumber = childNum;
        }

    }
}
