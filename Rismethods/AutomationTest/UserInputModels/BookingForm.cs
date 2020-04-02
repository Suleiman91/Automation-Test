using System;
using System.Collections.Generic;
using System.Text;

namespace TestFlightReservation
{
    public class BookingForm
    {
        public string Title { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PassportNumber { get; set; }
        public string Birthday { get; set; }
        public string ExpirationDate { get; set; }
        public string Nationality { get; set; }
        public string CardType { get; set; }
        public string CardNumber { get; set; }
        public string CardExpiryYear { get; set; }
        public string CVV { get; set; }
        public BookingForm(string title, string name, string surname, string email, string phone, string birthday, string expirationdate,
            string nationality, string cardtype, string cardNumber, string cardExpiry, string cvv, string passportNumber)
        {
            Title = title;
            Name = name;
            Surname = surname;
            Email = email;
            Phone = phone;
            Birthday = birthday;
            ExpirationDate = expirationdate;
            Nationality = nationality;
            CardType = cardtype;
            CardNumber = cardNumber;
            CardExpiryYear = cardExpiry;
            CVV = cvv;
            PassportNumber = passportNumber;
        }
    }
}
