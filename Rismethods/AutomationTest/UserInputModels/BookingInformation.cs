using System;
using System.Collections.Generic;
using System.Text;

namespace TestFlightReservation
{
    public class BookingInformation
    {
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public string From { get; set; }
        public string To { get; set; }
        public float TotalAmount { get; set; }
        public BookingInformation(DateTime departureDate, DateTime arrivalTime, string from, string to, float totalAmount)
        {
            DepartureDate = departureDate;
            ArrivalDate = arrivalTime;
            From = from;
            To = to;
            TotalAmount = totalAmount;
        }
    }
}
