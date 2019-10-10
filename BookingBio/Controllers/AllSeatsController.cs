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
    public class AllSeatsController : ApiController
    {
        private BookingDBEntities db = new BookingDBEntities();

        // GET: api/AllSeats
        public IQueryable<AllSeats> GetAllSeats()
        {
            return db.AllSeats;
        }

        // GET: api/AllSeats/5
        [ResponseType(typeof(AllSeats))]
        public IHttpActionResult GetAllSeats(int id)
        {
            AllSeats allSeats = db.AllSeats.Find(id);
            if (allSeats == null)
            {
                return NotFound();
            }

            return Ok(allSeats);
        }

        // PUT: api/AllSeats/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAllSeats(int id, AllSeats allSeats)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != allSeats.allSeatsId)
            {
                return BadRequest();
            }

            db.Entry(allSeats).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AllSeatsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/AllSeats
        [ResponseType(typeof(AllSeats))]
        public IHttpActionResult PostAllSeats(AllSeats allSeats)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.AllSeats.Add(allSeats);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = allSeats.allSeatsId }, allSeats);
        }

        // DELETE: api/AllSeats/5
        [ResponseType(typeof(AllSeats))]
        public IHttpActionResult DeleteAllSeats(int id)
        {
            AllSeats allSeats = db.AllSeats.Find(id);
            if (allSeats == null)
            {
                return NotFound();
            }

            db.AllSeats.Remove(allSeats);
            db.SaveChanges();

            return Ok(allSeats);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AllSeatsExists(int id)
        {
            return db.AllSeats.Count(e => e.allSeatsId == id) > 0;
        }
    }
}