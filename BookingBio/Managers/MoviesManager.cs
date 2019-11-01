using BookingBio.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookingBio.Managers
{
    public class MoviesManager
    {
       public Movies AddNewMovie (string movieNameInput) // Add new movie in db
        {
            Movies movie = new Movies();
            movie.movieName = movieNameInput;
            return movie;
        }
        public int CheckIfMovieExists(string movieNameInput) // check if movie already exists in db
        {
            using (var db = new BookingDBEntities())
            {

                var movieId = (from s in db.Movies
                                 where s.movieName == movieNameInput
                                 select s.movieId).DefaultIfEmpty(0).FirstOrDefault();

                return movieId;
            }
        }
        public int? CheckIfMovieShowingExists (DateTime movieShowing) // check if showing already exists in db
        {
            using (var db = new BookingDBEntities())
            {

                var showingId = (from s in db.MovieShowings
                               where s.movieShowingTime == movieShowing
                               select s.movieId).DefaultIfEmpty(0).FirstOrDefault();

                return showingId;
            }
        }
        public MovieShowings AddNewMovieShowing(DateTime showingDate, int? loungeId, int? movieId) // add new showing to DB
        {
            MovieShowings showing = new MovieShowings();
            showing.movieShowingTime = showingDate;
            showing.loungeId = loungeId;
            showing.movieId = movieId;
            return showing;
        }
        public int GetMovieIdFromName (string movieName)
        {
            using (var db = new BookingDBEntities())
            {

                var movieId = (from s in db.Movies
                                 where s.movieName == movieName
                                 select s.movieId).DefaultIfEmpty(0).FirstOrDefault();

                return movieId;
            }
        }
        public List<DateTime?> GetMovieShowingsFromMovieId(int movieIdInput)
        {
            using (var db = new BookingDBEntities())
            {
                List<DateTime?> movieShowings = new List<DateTime?>();
                try
                {
                    movieShowings = (from s in db.MovieShowings
                                         where s.movieId == movieIdInput
                                         select s.movieShowingTime).ToList();
                    return movieShowings;
                }
                catch
                {
                    return movieShowings;
                }
                
            }
        }
    }

}