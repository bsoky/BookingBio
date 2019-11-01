using BookingBio.Managers;
using BookingBio.Models;
using BookingBio.Models.DTOs;
using BookingBio;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using System.Data.Entity.Validation;
using System.Web.Http.Cors;

namespace BookingBio.Controllers
{
 
    public class UserAccountsController : ApiController
    {
        private BookingDBEntities db = new BookingDBEntities();
        private HttpRequestMessage msg = new HttpRequestMessage();

        // GET: api/UserAccounts
        public IQueryable<UserAccountsDTO> GetUserAccounts() // Get all user accounts
        {
            var userAccounts = from b in db.UserAccounts
                               select new UserAccountsDTO()
                               {
                                   UserAccountId = b.userAccountId,
                                   AccountName = b.accountName,
                                   AccountPassword = b.accountPassword,
                                   Salt = b.salt,
                                   Customers = b.Customers,
                                   CustomerId = b.customerId,
                                   PhoneNumber = b.phoneNumber
                               };

            return userAccounts;
        }
       
        // GET: 
        [Route("UserAccounts/FindAccount/{accountName?}")]
        [HttpGet]
        [ResponseType(typeof(UserAccounts))]
        public IHttpActionResult GetUserAccountByName(AccountNameDTO accountName) // Gets UserAccount entity from DB with accountname; accountName is found in json body, not the url!
        {        
            UserAccountsManager umgr = new UserAccountsManager();
            UserAccounts user = new UserAccounts();
            user = umgr.GetUserAccountByName(accountName.AccountName); // function thats gets user entity from DB

            if (user is null) // if user entity is null
            {
                TextResult failedToFindAccount = new TextResult("Failed to find account", msg);
                return failedToFindAccount;
            }           
            return Ok(user);
        }

        // PUT: 
        [Route("UserAccounts/Update")]
        [ResponseType(typeof(UpdateAccountDTO))]
        [HttpPut]
        public IHttpActionResult PutUserAccounts(UpdateAccountDTO updateAccount) // UPDATE USER ACCOUNT 
        {
            TextResult httpResponse = new TextResult("Account has been updated!", msg); // Http response

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            UserAccountsManager umgr = new UserAccountsManager();
            UserAccounts updatedUserAcc = umgr.UpdateAccountDetails(updateAccount); // Function to update account details
            if (updatedUserAcc is null)
            {
                httpResponse.ChangeHTTPMessage("Failed to update account!", msg); // Http response if user entity is null
                return httpResponse;
            }
            bool entityIsUpdated = umgr.UpdateEntityInDb(updatedUserAcc); // Updating entity in DB
            if (entityIsUpdated.Equals(true))
            {              
                return httpResponse;
            }           

            httpResponse.ChangeHTTPMessage("Failed to update account!", msg);
            return httpResponse;
        }

        // PUT: 
        [HttpPut]
        [Route("UserAccounts/ChangePassword")]
        [ResponseType(typeof(ChangePasswordDTO))]        
        public IHttpActionResult PutUserAccounts(ChangePasswordDTO passwordInput) // CHANGE PASSWORD
        {
            TextResult httpResponse = new TextResult("Failed to change password!", msg); // Http response

            string salt;
            string hashedOldPassword = null;
            string hashedNewPassword;
            string hashedPasswordFromDb = null;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            UserAccountsManager umgr = new UserAccountsManager();
            bool passwordIsNotOk = umgr.CheckIfPasswordIsOk(passwordInput.NewPassword); // Check if password is valid
            if (passwordIsNotOk.Equals(true))
            {
                httpResponse.ChangeHTTPMessage("Password must contain atleast six characters, one digit and one uppercase!", msg); // If password is not ok, HTTP response
                return httpResponse;
            }
            try
            {
                salt = umgr.GetUserSalt(passwordInput.AccountName); // Gets salt from DB
                hashedOldPassword = umgr.HashPassword(salt, passwordInput.OldPassword); // Hashing old password with user salt
                hashedNewPassword = umgr.HashPassword(salt, passwordInput.NewPassword); // Hashing new password with user salt
                hashedPasswordFromDb = umgr.GetPassword(passwordInput.AccountName); // Gets old hashed password from DB
            } catch
            {              
                return httpResponse; // http response if above operations failed
            }
            
            if (hashedOldPassword.Equals(hashedPasswordFromDb)) // Compares new password vs old
            {
                UserAccounts updatedUser = new UserAccounts();
                updatedUser.accountPassword = hashedNewPassword; // Adds new hashed password to user entity
                updatedUser.accountName = passwordInput.AccountName; // User entity account name
                bool entityIsUpdated = umgr.UpdateEntityInDb(updatedUser); // Updating entity in DB
                if (entityIsUpdated.Equals(true))
                {
                    httpResponse.ChangeHTTPMessage("Password changed!", msg);
                    return httpResponse;
                }                            
                return httpResponse;
            }
            httpResponse.ChangeHTTPMessage("Password not correct!", msg); // if input password and password from DB do not match
            return httpResponse;
        }
        // GET: api/UserAccounts/5
        [ResponseType(typeof(UserAccounts))]
        public IHttpActionResult GetUserAccounts(int id)
        {

            UserAccounts userAccounts = db.UserAccounts.Find(id);
            if (userAccounts == null)
            {
                return NotFound();
            }

            return Ok(userAccounts);
        }
       
        [Route("UserAccounts/deleteaccount")]
        [ResponseType(typeof(UserAccounts))]
        public IHttpActionResult DeleteUserAccounts(AccountNameDTO accName)
        {
            UserAccountsManager umgr = new UserAccountsManager();
            CustomerManager cmgr = new CustomerManager();
            var user = umgr.GetUserAccountByName(accName.AccountName);
            var customer = cmgr.GetCustomerEntityFromId(user.customerId);

            try
            {
                db.UserAccounts.Remove(user);
                db.Customers.Remove(customer);
                db.SaveChanges();
            }
            catch
            {
                TextResult httpResponse = new TextResult("Failed to delete account!", msg); // Http response
                return httpResponse;
            }

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool UserAccountsExists(string accountName)
        {
            return db.UserAccounts.Count(e => e.accountName == accountName) > 0;
        }

        private bool FindUserAccountId(string accountName)
        {
            
            return db.UserAccounts.Count(e => e.accountName == accountName) > 0;
        }

        public IHttpActionResult FailedRequest()
        {
            return BadRequest();
        }


    }
}
