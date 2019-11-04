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
using BookingBio.Models.DTOs;

namespace BookingBio.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class BookingsController : ApiController
    {
        private BookingDBEntities db = new BookingDBEntities();
        private HttpRequestMessage msg = new HttpRequestMessage();

        // GET: api/Bookings
        public IQueryable<Bookings> GetBookings()
        {
            return db.Bookings;
        }

        [Route("GetAvailableSeats")]
        [HttpPost]
        [ResponseType(typeof(SeatAndRowDTO))]
        public IHttpActionResult GetAvailableSeats(DateAndLoungeDTO dateAndLounge) // GETS AVAILABLE SEATS
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            TextResult httpResponse = new TextResult("", msg);
            SeatManager smgr = new SeatManager();
            CustomerManager cmgr = new CustomerManager();
            BookingManager bmgr = new BookingManager();
            UserAccountsManager umgr = new UserAccountsManager();
            SeatAndRowDTO seatAndRowLists = new SeatAndRowDTO();
            DateTime currentDate = DateTime.Now;
            var convertedBookingDate = bmgr.DateTimeConverter(dateAndLounge.BookingForDate); // Conert to datetime object
            var seatsList = smgr.GetAllSeatsInList(dateAndLounge.LoungeId); // get AllSeatsIds from AllSeats and puts in list
            var bookedSeatsList = smgr.GetAllBookedSeatFromDate(convertedBookingDate); // Gets AllseatsIds from Bookings from a date
            var availableSeatsIdList = smgr.CompareAllSeatsAndBookedSeats(seatsList, bookedSeatsList); // compares AllSeats and Booked seat and return difference
            var (rowList, seatList) = smgr.GetUnbookedAllSeatIdsFromAllSeats(availableSeatsIdList); // Gets row and seat from available seats and puts in lists
            seatAndRowLists.Row = rowList;
            seatAndRowLists.SeatNumber = seatList;

            return Ok(seatAndRowLists); // Returns available seats
        }

        [Route("Bookings/UserBooking")]
        [HttpPost]
        [ResponseType(typeof(HttpResponseMessage))]
        public IHttpActionResult UserAccountBooking(UserAccountBookingDTO booking) // BOOKING WITH USER ACCOUNT, NOT FINISHED
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            TextResult httpResponse = new TextResult("", msg);            
            SeatManager smgr = new SeatManager();
            CustomerManager cmgr = new CustomerManager();
            BookingManager bmgr = new BookingManager();
            UserAccountsManager umgr = new UserAccountsManager();
            DateTime currentDate = DateTime.Now;           
            string token = umgr.CreateToken(booking.AccountName);
            var loginOk = umgr.CheckIfUserIsLoggedIn(booking.LoginToken, token); // token passed from frontend to check if user is logged in, token variabel content?
            if (loginOk.Equals(false))
            {
                httpResponse.ChangeHTTPMessage("User is not logged in!", msg);
                return httpResponse;
            };
            var convertedForDate = bmgr.DateTimeConverter(booking.BookingForDate); // Converting dates into DateTime objects          
                if (convertedForDate.Equals(null)) // checking if date input is valid
            {
                httpResponse.ChangeHTTPMessage("Date input is not correct!", msg);
                return httpResponse;
            };
            int? allSeatsId = smgr.GetSeatPlacementId(booking.RowNumber, booking.SeatNumber); // Gets the allSeatsId from AllSeats from row and seatnumber
            int bookingId = smgr.CheckIfSeatIsTaken(convertedForDate, allSeatsId); // checks if seat is taken, returns bookingId    
            if (bookingId != 0)
            {
                httpResponse.ChangeHTTPMessage("That seat is taken!", msg); // http response if seat is taken
                return httpResponse;
            }
            var custId = umgr.GetCustomerIdFromUserAccountName(booking.AccountName);
            var email = cmgr.GetCustomerEmailFromAccountName(custId);
            var bookingEntity = bmgr.UserAccountBooking(allSeatsId, custId, convertedForDate, currentDate);
     
            db.Bookings.Add(bookingEntity);
            db.SaveChanges();

            httpResponse.ChangeHTTPMessage("Booking has been made!", msg); // HTTP response if fails to savechanges to DB
            return httpResponse;
        }

        [Route("Bookings/CustomerBookings")]
        [HttpPost]
        [ResponseType(typeof(CustomerBookingDTO))]
        public IHttpActionResult CustomerBooking(CustomerBookingDTO booking) // BOOKING WITHOUT USER ACCOUNT, ONLY AS CUSTOMER
        {
            if (!ModelState.IsValid)
            {
                var errors = this.ModelState.Keys.SelectMany(key => this.ModelState[key].Errors);
                return BadRequest(ModelState);
            }
            TextResult httpResponse = new TextResult("", msg);
            SeatManager smgr = new SeatManager();
            CustomerManager cmgr = new CustomerManager();
            BookingManager bmgr = new BookingManager();           
            Customers custEntity = new Customers();
            Bookings bookingEntity = new Bookings();          
            int? customerId = null;
            DateTime currentDate = DateTime.Now;
            var convertedForDate = bmgr.DateTimeConverter(booking.BookingForDate); // Convert dates passed from fronted to DateTime objects                        
            int? allSeatsId = smgr.GetSeatPlacementId(booking.RowNumber, booking.SeatNumber); // Gets the allSeatsId from AllSeats from row and seatnumber                     
            try
            {
                custEntity = cmgr.AddCustomer(booking.Email); // try to create new customer entity
                db.Customers.Add(custEntity);
                db.SaveChanges(); // if customer entity exists, trying to insert a new customer will cause exception due to duplicate keys
            } catch 
            {
                customerId = cmgr.FindCustomerId(booking.Email); // if customer entity already exists, get customerID from email input
            }
                if (customerId!=null) // if customer entity already exists, customerId is not null, use customerId instead of entity
                {
                bookingEntity = bmgr.CustomerBooking(convertedForDate, currentDate, allSeatsId, customerId); // creates booking entity with customerId
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
            bookingEntity = bmgr.CustomerBooking(custEntity,convertedForDate,currentDate, allSeatsId); // creates booking entity, with customerEntity
                try
                {
                    db.Bookings.Add(bookingEntity);
                    db.SaveChanges();
                }   catch
                {
                    httpResponse.ChangeHTTPMessage("Could not make booking!", msg);
                    return httpResponse;
                }         
            httpResponse.ChangeHTTPMessage("Booking completed!", msg);
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