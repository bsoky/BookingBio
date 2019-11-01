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
using BookingBio.Models;

namespace BookingBio.Controllers
{
    
    public class LoungesController : ApiController
    {
        private BookingDBEntities db = new BookingDBEntities();

        // GET: api/Lounges
        public IQueryable<Lounges> GetLounges()
        {
            return db.Lounges;
        }

        // GET: api/Lounges/5
        [ResponseType(typeof(Lounges))]
        public IHttpActionResult GetLounges(int id)
        {
            Lounges lounges = db.Lounges.Find(id);
            if (lounges == null)
            {
                return NotFound();
            }

            return Ok(lounges);
        }

        // PUT: api/Lounges/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutLounges(int id, Lounges lounges)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != lounges.loungeId)
            {
                return BadRequest();
            }

            db.Entry(lounges).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LoungesExists(id))
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

        // POST: api/Lounges
        [ResponseType(typeof(Lounges))]
        public IHttpActionResult PostLounges(Lounges lounges)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Lounges.Add(lounges);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = lounges.loungeId }, lounges);
        }

        // DELETE: api/Lounges/5
        [ResponseType(typeof(Lounges))]
        public IHttpActionResult DeleteLounges(int id)
        {
            Lounges lounges = db.Lounges.Find(id);
            if (lounges == null)
            {
                return NotFound();
            }

            db.Lounges.Remove(lounges);
            db.SaveChanges();

            return Ok(lounges);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LoungesExists(int id)
        {
            return db.Lounges.Count(e => e.loungeId == id) > 0;
        }
    }
}