using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using System.Web.Http.Description;
using BookingBio.Managers;
using BookingBio.Models;
using BookingBio.Models.DTOs;

namespace BookingBio.Controllers
{

 
    public class RegistrationController : ApiController
    {
        private BookingDBEntities db = new BookingDBEntities();
        private HttpRequestMessage msg = new HttpRequestMessage();

        // POST:
        [Route("Register")]
        [HttpPost]
        [ResponseType(typeof(RegistrationDTO))]
        public IHttpActionResult CreateUserAccount(RegistrationDTO userInput) // CREATE ACCOUNT
        {
            TextResult httpResponse = new TextResult("There is already an account with that name!", msg);
            UserAccountsManager umgr = new UserAccountsManager();
            CustomerManager cmgr = new CustomerManager();
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }
            bool EmailIsOk = cmgr.IsValidEmail(userInput.Email);
            if (EmailIsOk.Equals(false))
            {
                httpResponse.ChangeHTTPMessage("Enter valid email!", msg);
                return httpResponse; // HTTP response if accountname already exists
            };
            bool accNameExist = umgr.CheckIfAccountNameExists(userInput.AccountName); // Check if username already exists, returns bool
            if (accNameExist.Equals(true))
            {
                return httpResponse; // HTTP response if accountname already exists
            };
            bool emailExists = cmgr.CheckIfEmailExists(userInput.Email); // check if email already exists, returns bool
            if (emailExists.Equals(true))
            {
                httpResponse.ChangeHTTPMessage("Email already exists!", msg); // If email exists, HTTP response
                return httpResponse;
            };
            bool passwordIsNotOk = umgr.CheckIfPasswordIsOk(userInput.AccountPassword); // checks if password is ok
            if (passwordIsNotOk.Equals(true))
            {
                httpResponse.ChangeHTTPMessage("Password must contain atleast six characters, one digit and one uppercase!", msg); // If password is not ok, HTTP response
                return httpResponse;
            };
            var customerObject = cmgr.AddCustomer(userInput.Email); // Creates customer entity
            var userObject = umgr.CreateUserAccount(userInput.AccountName, userInput.AccountPassword, userInput.PhoneNumber, userInput.CustomerName, customerObject); // creates useraccount entity            
            try
            {
                db.Customers.Add(customerObject); // adds customer entity to DB
                db.UserAccounts.Add(userObject); // adds useraccount to DB
                db.SaveChanges();
            }
            catch
            {
                httpResponse.ChangeHTTPMessage("Failed to create account!", msg); // HTTP response if fails to savechanges to DB
                return httpResponse;
            }           
            
            return Ok(); // returns login token if registration succesfull
        }



        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserAccountsExists(int id)
        {
            return db.UserAccounts.Count(e => e.userAccountId == id) > 0;
        }
    }
}