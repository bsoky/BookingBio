using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingBio.Models.DTOs
{
    public class UserAccountBookingDTO
    {
        public string AccountName { get; set; }
        public string BookingForDate { get; set; }      
        public int LoungeId { get; set; }
        public string RowNumber { get; set; }
        public int SeatNumber { get; set; }       
        public string LoginToken { get; set; }      

    }
}