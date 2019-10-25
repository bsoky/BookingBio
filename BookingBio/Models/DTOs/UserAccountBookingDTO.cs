using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingBio.Models.DTOs
{
    public class UserAccountBookingDTO
    {
        bool IsUserLoggedIn { get; set; }
        //UserAccounts
        public string AccountName { get; set; }
        public string CustomerName { get; set; }
        //Bookings
        public string BookingForDate { get; set; }
        public string BookingMadeDate { get; set; }
        //Customers
        public string Email { get; set; }
        //Lounges
        public int LoungeId { get; set; }
        //AllSeats
        public string RowNumber { get; set; }
        public int SeatNumber { get; set; }
        //Login
        public string LoginToken { get; set; }
        //Movies
        public string MovieName { get; set; }



    }
}