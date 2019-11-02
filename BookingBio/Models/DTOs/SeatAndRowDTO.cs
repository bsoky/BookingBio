using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingBio.Models.DTOs
{
    public class SeatAndRowDTO
    {
        public List<int?> SeatNumber { get; set; }
        public List<string> Row { get; set; }
    }
}