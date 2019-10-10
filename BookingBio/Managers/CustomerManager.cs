using BookingBio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingBio.Managers
{
    public class CustomerManager
    {
        public Customers AddCustomer(string emailInput) // create new customer entity
        {
            Customers cust = new Customers();
            cust.email = emailInput;

            return cust;
        }

        public bool CheckIfEmailExists(string emailInput) // check if email exists in db
        {
            bool emailExists = false;
            using (var db = new BookingDBEntities())
            {
                var email = (from s in db.Customers
                               where s.email == emailInput
                               select s.email).DefaultIfEmpty(String.Empty).First().ToString();

                if (email!=(String.Empty)) 
                {
                    emailExists = true; // returns if linq query returns and email from DB
                };

                return emailExists;
            }

        }
        private int FindCustomerId(string emailInput) // find customer id from email
        {
            using (var db = new BookingDBEntities())
            {

                var accId = (from s in db.Customers
                             where s.email == emailInput
                             select s.customerId).FirstOrDefault();

                return accId;
            }

            
        }
    }
}