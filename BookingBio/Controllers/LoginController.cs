using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using BookingBio.Managers;
using BookingBio.Models;

namespace BookingBio.Controllers
{
    
    public class LoginController : ApiController
    {
        private BookingDBEntities db = new BookingDBEntities();
        private HttpRequestMessage msg = new HttpRequestMessage();

        [Route("login")]
        [HttpPost]
        [ResponseType(typeof(HttpResponseMessage))]
        public IHttpActionResult LoginUser(UserAccounts userInput) // LOGIN ACCOUNT
        {

            if (!ModelState.IsValid)
            {
                return BadRequest();
            };

            UserAccountsManager umgr = new UserAccountsManager();
            bool loginOk = umgr.Login(userInput.accountName, userInput.accountPassword); // Login function, returns bool
            string token = umgr.CreateToken(userInput.accountName);
            if (loginOk.Equals(true)) // If bool is true, return Ok(200) http response
            {
                return Ok(token);
            }

            return Unauthorized(); // Returns if failed to verify account
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