using BookingBio.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Web;

namespace BookingBio.Managers
{
    public class BookingManager
    {
        public Bookings UserAccountBooking(int? seatId, int custId, DateTime madeForDate, DateTime madeDate) // Makes booking entity with for useraccount, TODO
        {
            Bookings booking = new Bookings();
            
            booking.bookingMadeDate = madeDate;
            booking.bookingForDate = madeForDate;
            booking.allSeatsId = seatId;
            booking.customerId = custId;                      

            return booking;
        }

        public Bookings CustomerBooking(Customers cust, DateTime madeForDate, DateTime madeDate, int? seatId) // with customer entity
        {
            Bookings booking = new Bookings();

            booking.bookingMadeDate = madeDate;
            booking.bookingForDate = madeForDate;
            booking.allSeatsId = seatId;
            booking.Customers = cust;

            return booking;
        }
        public Bookings CustomerBooking(DateTime madeForDate, DateTime madeDate, int? seatId, int? customerId) // without cust entity, using ID instead
        {
            Bookings booking = new Bookings();

            booking.bookingMadeDate = madeDate;
            booking.bookingForDate = madeForDate;
            booking.allSeatsId = seatId;
            booking.customerId = customerId;

            return booking;
        }


        public int GetBookingIdFromCustomerId (int? custId)
        {
            using (var db = new BookingDBEntities())
            {

                var bookingId = (from s in db.Bookings
                              where s.customerId == custId
                              select s.bookingId).FirstOrDefault();

                return bookingId;
            }
        }


        public DateTime DateTimeConverter(string dateString) // Datetime converter, string into Datetime object
        {

            CultureInfo provider = CultureInfo.InvariantCulture;           
            provider = new CultureInfo("sv-SV");
            DateTime result = new DateTime();

            try
            {
                result = DateTime.Parse(dateString);
                return result;
            }
            catch (FormatException)
            {
                return result;
            }
        }

        public List<int?> GetBookedSeatsInList(DateTime bookingDate) 
        {
            
            using (var db = new BookingDBEntities())
            {
                var list = (from s in db.Bookings
                            where s.bookingForDate == bookingDate
                            select s.allSeatsId).ToList();              

                return list;
            }
        }
        
    }
}