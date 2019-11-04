using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

using System.Web.Http.Description;
using BookingBio.Managers;
using BookingBio.Models;
using BookingBio.Models.DTOs;

namespace BookingBio.Controllers
{

    
    public class MoviesController : ApiController
    {
        
        private BookingDBEntities db = new BookingDBEntities();
        private HttpRequestMessage msg = new HttpRequestMessage();
        // GET: api/Movies
        public IQueryable<Movies> GetMovies()
        {
            return db.Movies;
        }

        // POST: api/Movies
        [Route("addmovie")]
        [ResponseType(typeof(Movies))]
        public IHttpActionResult PostMovies(Movies movies) // ADD NEW MOVIE TO DATABASE
        {
            TextResult httpResponse = new TextResult("Movie added!", msg);
            MoviesManager mvmgr = new MoviesManager();
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }          
            int movieExists = mvmgr.CheckIfMovieExists(movies.movieName); // returns 0 if movie exists
                if (movieExists!=0) 
                {
                    httpResponse.ChangeHTTPMessage("Movie already exists!", msg);
                    return httpResponse;
                }
            var movieEntity = mvmgr.AddNewMovie(movies.movieName);
            db.Movies.Add(movieEntity);
            db.SaveChanges();

            return httpResponse;
        }

        // DELETE: api/Movies/5
        [HttpPost]
        [Route("deletemovie")]
        [ResponseType(typeof(Movies))]
        public IHttpActionResult DeleteMovies(MovieNameDTO movie) // Deletes movie from db and any related movieshowings
        {

            MoviesManager mmgr = new MoviesManager();
            int movieId = mmgr.CheckIfMovieExists(movie.MovieName);
            Movies movieEntity = db.Movies.Find(movieId);
            if (movieEntity is null)
            {
                return NotFound();
            }
            try
            {
                db.MovieShowings.RemoveRange(db.MovieShowings.Where(x => x.movieId == movieId));
                db.Movies.Remove(movieEntity);
                db.SaveChanges();
            } catch
            {
                return Ok();
            }
            TextResult couldNotDeleteMovieAndShowings = new TextResult("Could not delete movie!", msg);
            return couldNotDeleteMovieAndShowings;


        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MoviesExists(int id)
        {
            return db.Movies.Count(e => e.movieId == id) > 0;
        }
    }
}