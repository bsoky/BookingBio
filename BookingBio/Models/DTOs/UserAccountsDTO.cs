using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingBio.Models.DTOs
{
    public class UserAccountsDTO
    {
        public int UserAccountId { get; set; }
        public string AccountName { get; set; }
        public string AccountPassword { get; set; }
        public Nullable<int> CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNumber { get; set; }
        public string Salt { get; set; }

        public virtual Customers Customers { get; set; }
    }
}