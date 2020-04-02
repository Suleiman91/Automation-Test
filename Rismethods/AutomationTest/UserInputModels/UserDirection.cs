using System;
using System.Collections.Generic;
using System.Text;

namespace TestFlightReservation.UserInputModels
{
    public class UserDirection
    {
        public string StartPoint { get; set; }
        public string Destination { get; set; }
        public UserDirection(string startPoint, string destination)
        {
            StartPoint = startPoint;
            Destination = destination;
        }
    }
}
