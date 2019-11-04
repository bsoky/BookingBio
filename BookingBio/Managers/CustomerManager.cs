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
                    emailExists = true; // returns if linq query returns an email from DB
                };

                return emailExists;
            }

        }
        public int FindCustomerId(string emailInput) // find customer id from email
        {
            using (var db = new BookingDBEntities())
            {

                var custId = (from s in db.Customers
                             where s.email == emailInput
                             select s.customerId).FirstOrDefault();

                return custId;
            }
         
        }
        public Customers GetCustomerEntity(string emailInput) // find customer id from email
        {
            using (var db = new BookingDBEntities())
            {

                var accId = (from s in db.Customers
                             where s.email == emailInput
                             select s).FirstOrDefault<Customers>();

                return accId;
            }

        }
        public int GetCustomerId(string emailInput)
        {
            using (var db = new BookingDBEntities())
            {

                var custId = (from s in db.Customers
                             where s.email == emailInput
                             select s.customerId).FirstOrDefault();

                return custId;
            }
        }

        public Customers GetCustomerEntityFromId(int? custId) // Gets user entity from db
        {
            try
            {
                using (var db = new BookingDBEntities())
                {
                    var customer = (from s in db.Customers
                                   where s.customerId == custId
                                   select s).FirstOrDefault<Customers>();

                    return customer;
                }
            }
            catch
            {
                return null; // returns null if entity is not found
            }


        }


        public bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }
        public string GetCustomerEmailFromAccountName(int custId)
        {
            
            using (var db = new BookingDBEntities())
            {
                var email = (from s in db.Customers
                             where s.customerId == custId
                             select s.email).FirstOrDefault();

                return email;
            }
        }
    }
}