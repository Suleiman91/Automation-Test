using System;
using System.Collections.Generic;
using System.Text;

namespace TestFlightReservation
{
    public class UserCredentials
    {
        public UserCredentials(string userName, string password)
        {
            UserName = userName;
            Password = password;
        }
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
