using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingBio.Models.DTOs
{
    public class MovieShowingDTO
    {
        public string movieShowingTime { get; set; }
        public Nullable<int> loungeId { get; set; }
        public Nullable<int> movieId { get; set; }
    }
}