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
    public class BookingsController : ApiController
    {
        private BookingDBEntities db = new BookingDBEntities();
        private HttpRequestMessage msg = new HttpRequestMessage();

        // GET: api/Bookings
        public IQueryable<Bookings> GetBookings()
        {
            return db.Bookings;
        }

        // GET: api/Bookings/5
        [ResponseType(typeof(Bookings))]
        public IHttpActionResult GetBookings(int id)
        {
            Bookings bookings = db.Bookings.Find(id);
            if (bookings == null)
            {
                return NotFound();
            }

            return Ok(bookings);
        }

        [Route("Bookings/UserBooking")]
        [HttpPost]
        [ResponseType(typeof(UserAccountBookingDTO))]
        public IHttpActionResult UserAccountBooking(UserAccountBookingDTO booking) // BOOKING WITH USER ACCOUNT, TODO
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            TextResult httpResponse = new TextResult("", msg);
            UserAccounts user = new UserAccounts();
            SeatManager smgr = new SeatManager();
            CustomerManager cmgr = new CustomerManager();
            BookingManager bmgr = new BookingManager();
            UserAccountsManager umgr = new UserAccountsManager();

            var loginOk = umgr.CheckIfUserIsLoggedIn(booking.LoginToken); // token passed from frontend to check if user is logged in, token variabel content?
                if (loginOk.Equals(false))
            {
                httpResponse.ChangeHTTPMessage("User is not logged in!", msg);
                return httpResponse;
            };
            var convertedForDate = bmgr.DateTimeConverter(booking.BookingForDate); // Converting dates into DateTime objects
            var convertedMadeDate = bmgr.DateTimeConverter(booking.BookingMadeDate);
                if (convertedForDate.Equals(null)||convertedMadeDate.Equals(null)) // checking if date input is valid
            {
                httpResponse.ChangeHTTPMessage("Date input is not correct!", msg);
                return httpResponse;
            };
            var allSeatsEntity = smgr.CreateSeatEntity(booking.SeatNumber, booking.RowNumber, booking.LoungeId); // creates seat entity
            var custEntity = cmgr.GetCustomerEntity(booking.Email); // gets customer entity from email input
            var bookingEntity = bmgr.CreateUserAccountBooking(allSeatsEntity, custEntity, convertedForDate, convertedMadeDate);

            db.AllSeats.Add(allSeatsEntity);
            db.Bookings.Add(bookingEntity);
            db.SaveChanges();

            httpResponse.ChangeHTTPMessage("Booking has been made!", msg); // HTTP response if fails to savechanges to DB
            return httpResponse;
        }

        [Route("Bookings/CustomerBookings")]
        [HttpPost]
        [ResponseType(typeof(CustomerBookingDTO))]
        public IHttpActionResult CustomerBooking(CustomerBookingDTO bookings) // BOOKING WITHOUT USER ACCOUNT, ONLY AS CUSTOMER
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            TextResult httpResponse = new TextResult("", msg);
            SeatManager smgr = new SeatManager();
            CustomerManager cmgr = new CustomerManager();
            BookingManager bmgr = new BookingManager();           
            Customers custEntity = new Customers();
            Bookings bookingEntity = new Bookings();
            int? customerId = null;
            var convertedForDate = bmgr.DateTimeConverter(bookings.BookingForDate); // Convert dates passed from fronted to DateTime objects
            var convertedMadeDate = bmgr.DateTimeConverter(bookings.BookingMadeDate); // Dateformat example: 2009-06-20T11:40:30

            int? allSeatsId = smgr.GetSeatPlacementId(bookings.RowNumber, bookings.SeatNumber); // Gets the allSeatsId from AllSeats from row and seatnumber
            int bookingId= smgr.CheckIfSeatIsTaken(convertedForDate, allSeatsId); // checks if seat is taken, returns bookingId                   
            if (bookingId!=0)
            {
                httpResponse.ChangeHTTPMessage("That seat is taken!", msg); // http response if seat is taken
                return httpResponse;
            }
            
            try
            {
                custEntity = cmgr.AddCustomer(bookings.Email); // try to create new customer entity
                db.Customers.Add(custEntity);
                db.SaveChanges(); // if customer entity exists, trying to insert a new customer will cause exception due to duplicate keys
            } catch 
            {
                customerId = cmgr.FindCustomerId(bookings.Email); // if customer entity already exists, get customerID from email input
            }

                if (customerId!=null) // if customer entity already exists, customerId is not null, use customerId instead of entity
                {
                bookingEntity = bmgr.CustomerBooking(convertedForDate, convertedMadeDate, allSeatsId, customerId); // creates booking entity with customerId
                    try
                    {
                        db.Bookings.Add(bookingEntity); // creates booking enitity, with customerId
                        db.SaveChanges();
                    }
                    catch
                    {
                        httpResponse.ChangeHTTPMessage("Could not make booking!", msg);
                        return httpResponse;
                    }
                }  
               
            bookingEntity = bmgr.CustomerBooking(custEntity,convertedForDate,convertedMadeDate, allSeatsId); // creates booking entity, with customerEntity
                try
                {
                    db.Bookings.Add(bookingEntity);
                    db.SaveChanges();
                }   catch
                {
                    httpResponse.ChangeHTTPMessage("Could not make booking!", msg);
                    return httpResponse;
                }
           
            httpResponse.ChangeHTTPMessage("Booking made!", msg);
            return httpResponse;
        }

        // DELETE: api/Bookings/5
        [ResponseType(typeof(Bookings))]
        public IHttpActionResult DeleteBookings(int id)
        {
            Bookings bookings = db.Bookings.Find(id);
            if (bookings == null)
            {
                return NotFound();
            }

            db.Bookings.Remove(bookings);
            db.SaveChanges();

            return Ok(bookings);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool BookingsExists(int id)
        {
            return db.Bookings.Count(e => e.bookingId == id) > 0;
        }
    }
}