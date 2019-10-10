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

namespace BookingBio.Controllers
{
    public class CustomersController : ApiController
    {
        private BookingDBEntities db = new BookingDBEntities();
        private HttpRequestMessage msg = new HttpRequestMessage();

        // GET: api/Customers
        public IQueryable<Customers> GetCustomers()
        {
            return db.Customers;
        }

        // GET: api/Customers/5
        [ResponseType(typeof(Customers))]
        public IHttpActionResult GetCustomers(int id)
        {
            Customers customers = db.Customers.Find(id);
            if (customers == null)
            {
                return NotFound();
            }

            return Ok(customers);
        }

        // PUT: api/Customers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutCustomers(int id, Customers customers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != customers.customerId)
            {
                return BadRequest();
            }

            db.Entry(customers).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CustomersExists(id))
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

        // POST: api/Customers
        [ResponseType(typeof(Customers))]
        public IHttpActionResult PostCustomers(Customers customers) // Adds new customer
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            CustomerManager cmgr = new CustomerManager();
            bool emailExists = cmgr.CheckIfEmailExists(customers.email); // Check if email exists
            if (emailExists.Equals(true))
            {
                TextResult emailIsExisting = new TextResult("Email already exists!", msg); // Http response
                return emailIsExisting;
            }
            var customerObject = cmgr.AddCustomer(customers.email); // creates new customer entity

            try
            {
                db.Customers.Add(customerObject); // adds customer entity to db
                db.SaveChanges();
            }
            catch
            {
                TextResult FailedToCreateCustomer = new TextResult("Failed to create customer", msg);
                return FailedToCreateCustomer;
            }
            return Ok();
        }

        // DELETE: api/Customers/5
        [ResponseType(typeof(Customers))]
        public IHttpActionResult DeleteCustomers(int id)
        {
            Customers customers = db.Customers.Find(id);
            if (customers == null)
            {
                return NotFound();
            }

            db.Customers.Remove(customers);
            db.SaveChanges();

            return Ok(customers);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CustomersExists(int id)
        {
            return db.Customers.Count(e => e.customerId == id) > 0;
        }
    }
}