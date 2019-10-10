using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingBio.Models.DTOs
{
    public class UpdateAccountDTO
    {
        public string AccountName { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
    }
}