using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingBio.Models.DTOs
{
    public class UserLoginDTO
    {
        public string AccountName { get; set; }
        public string AccountPassword { get; set; }
    }
}