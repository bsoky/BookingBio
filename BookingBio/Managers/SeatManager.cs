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
       
        public List<int?> GetAllSeatsInList(int loungeId) // Gets allSeatIds from AllSeats in a list
        {           
            using (var db = new BookingDBEntities())
            {
                var list = (from s in db.AllSeats
                            where s.loungeId == loungeId
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

        public List<int?> GetAllAvailableSeats (List<int?> allSeatsList)
        {
            List<int?> availableSeats = new List<int?>();


            return availableSeats;
        }

        public List<int?> GetAllBookedSeatFromDate (DateTime showingDate) // Gets allSeatsIds from Bookings and puts in list
        {
            List<int?> bookedSeatsList = new List<int?>();

            using (var db = new BookingDBEntities())
            {
                bookedSeatsList = (from s in db.Bookings
                                   where s.bookingForDate == showingDate
                                   select (int?)s.allSeatsId).ToList();
                return bookedSeatsList;
            }

        }
        public List<int?> CompareAllSeatsAndBookedSeats (List<int?> allSeats, List<int?> bookedSeats) // Compares allSeatsIds and bookedSeatsIds and return the difference
        {
            List<int?> availableSeatsId = new List<int?>();
            using (var db = new BookingDBEntities())
            {
                availableSeatsId = allSeats.Except(bookedSeats).ToList();
                return availableSeatsId;
            }
        }
        public (List<string> rowList, List<int?> seatList) GetUnbookedAllSeatIdsFromAllSeats (List<int?> availableSeatsIdList) // Gets the seat and row from Allseats, from the availableSeatIds
        {
            List<string> rowList = new List<string>();
            List<int?> seatList = new List<int?>();

            using (var db = new BookingDBEntities())
            {
                foreach (int? element in availableSeatsIdList) // gets the row from AllSeats and puts in list
                {
                    var row = (from s in db.AllSeats
                                       where s.allSeatsId == element
                                       select s.rowNumber).ToList();
                    string convRow = row.First<string>();
                    rowList.Add(convRow);
                }
                foreach (int? element in availableSeatsIdList) // gets seatNumber from ALlSeats and puts in list
                {
                    var seatNumber = (from s in db.AllSeats
                               where s.allSeatsId == element
                               select (int?)s.seatNumber).ToList();
                    int? convSeat = seatNumber.First<int?>();
                    seatList.Add(convSeat);
                }

                return (rowList, seatList);
            }

        }
        public static int? ParseToNullableInt(string value)
        {
            return String.IsNullOrEmpty(value) ? null : (int.Parse(value) as int?);
        }


    }

}