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
using BookingBio.Models;

namespace BookingBio.Controllers
{

  
    public class LogoutController : ApiController
    {
        private BookingDBEntities db = new BookingDBEntities();

        // POST: api/Logout
        [ResponseType(typeof(UserAccounts))]
        public IHttpActionResult PostUserAccounts(UserAccounts userAccounts)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.UserAccounts.Add(userAccounts);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = userAccounts.userAccountId }, userAccounts);
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