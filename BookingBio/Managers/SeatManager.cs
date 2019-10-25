using BookingBio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingBio.Managers
{
    public class SeatManager
    {
        public AllSeats CreateSeatEntity(int seatNumber, string rowNumber, int loungeId)
        {
            AllSeats seat = new AllSeats();
            seat.seatNumber = seatNumber;
            seat.rowNumber = rowNumber;
            seat.loungeId = loungeId;

            return seat;

        }
       
        public List<int?> GetAllSeatsInList() // Gets alleatIds from AllSeats in a list
        {           
            using (var db = new BookingDBEntities())
            {
                var list = (from s in db.AllSeats                           
                            select (int?)s.allSeatsId).ToList();

                return list;
            }
        }
       
        public int CheckIfSeatIsTaken(DateTime forDate, int? seatId) // Checks if a booking has been made on a date from allSeatId and date, returns bookings id
        {
            int bookingId;
            using (var db = new BookingDBEntities())
            {
                bookingId = (from s in db.Bookings
                             where s.bookingForDate == forDate && s.allSeatsId == seatId
                             select s.bookingId).DefaultIfEmpty(0).FirstOrDefault();
            }

            return bookingId; // returns booking id, 0 if no booking has been found
            
        }
        public int GetSeatPlacementId(string row, int seatNumber) // Gets seat id from row and seatNumber
        {
            int allSeatId;
            using (var db = new BookingDBEntities())
            {
                allSeatId = (from s in db.AllSeats
                                 where s.rowNumber == row && s.seatNumber == seatNumber
                                 select s.allSeatsId).FirstOrDefault();
            }
            return allSeatId;
        }
    }
}