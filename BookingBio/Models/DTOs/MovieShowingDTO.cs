using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingBio.Models.DTOs
{
    public class MovieShowingDTO
    {
        public string MovieShowingTime { get; set; }
        public string MovieName { get; set; }
        public int? LoungeId { get; set; }
    }
}