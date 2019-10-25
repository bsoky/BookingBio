using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingBio.Models.DTOs
{
    public class CustomerBookingDTO
    {
        public string BookingForDate { get; set; }
        public string BookingMadeDate { get; set; }
        public string Email { get; set; }
        public string RowNumber { get; set; }
        public int SeatNumber { get; set; }
        public int LoungeId { get; set; }

    }
}