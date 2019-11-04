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
using System.Web.Http.Cors;
using System.Web.Http.Description;
using BookingBio.Managers;
using BookingBio.Models;
using BookingBio.Models.DTOs;

namespace BookingBio.Controllers
{
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    public class MovieShowingsController : ApiController
    {
        
        private BookingDBEntities db = new BookingDBEntities();
        private HttpRequestMessage msg = new HttpRequestMessage();

        // GET: api/MovieShowings
        public IQueryable<MovieShowings> GetMovieShowings()
        {
            return db.MovieShowings;
        }

        
        [Route("movieshowings")]
        [HttpPost]
        [ResponseType(typeof(MovieShowingTimeDTO))]
        public IHttpActionResult GetMovieShowings(MovieNameDTO movie) // GETS MOVIESHOWINGS
        {
            MoviesManager mmgr = new MoviesManager();
            int movieId = mmgr.GetMovieIdFromName(movie.MovieName);
            if (movieId == 0)
            {
                TextResult failedToFindMovieShowing = new TextResult("Movieshowing does not exist!", msg);
                return failedToFindMovieShowing;
            }
            var movieList = mmgr.GetMovieShowingsFromMovieId(movieId); // Gets movieshowingtimes and puts in list                    
            return Ok(movieList); 
        }

        // PUT: api/MovieShowings/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMovieShowings(int id, MovieShowings movieShowings)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != movieShowings.movieShowingsId)
            {
                return BadRequest();
            }

            db.Entry(movieShowings).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieShowingsExists(id))
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

        // POST: api/MovieShowings
        [ResponseType(typeof(MovieShowingDTO))]
        public IHttpActionResult PostMovieShowings(MovieShowingDTO movieShowings) // ADD NEW MOVIE SHOWING
        {
            MoviesManager mvmgr = new MoviesManager();
            BookingManager bmgr = new BookingManager();
            TextResult httpResponse = new TextResult("", msg);
            DateTime convertedShowingDate = new DateTime();


            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            convertedShowingDate = bmgr.DateTimeConverter(movieShowings.movieShowingTime);
            int? showingExists = mvmgr.CheckIfMovieShowingExists(convertedShowingDate); // Checks if movieshowing already exists
            if (showingExists!= 0)
            {
                httpResponse.ChangeHTTPMessage("Showing already exists on that date!", msg); // http response
                return httpResponse;
            }
            var movieShowingEntity = mvmgr.AddNewMovieShowing(convertedShowingDate, movieShowings.loungeId, movieShowings.movieId); // creates moieshowing entity
            try
            {
                db.MovieShowings.Add(movieShowingEntity);
                db.SaveChanges();
            } catch
            {
                httpResponse.ChangeHTTPMessage("Movieshowing could not be added!", msg);
                return httpResponse;
            }
            
            httpResponse.ChangeHTTPMessage("Movieshowing added!", msg);
            return httpResponse;
        }

        // DELETE: api/MovieShowings/5
        [Route("deletemovieshowings")]
        [ResponseType(typeof(MovieShowings))]
        public IHttpActionResult DeleteMovieShowings(MovieNameDTO movie)
        {
            TextResult httpResponse = new TextResult("Could not delete movieshowing!", msg);
            MoviesManager mmgr = new MoviesManager();
            int movieId = mmgr.GetMovieIdFromName(movie.MovieName);       
            if (movieId == 0)
            {
                return NotFound();
            }
            try
            {
                db.MovieShowings.RemoveRange(db.MovieShowings.Where(x => x.movieId == movieId));
                db.SaveChanges();
            } catch
            {
                return httpResponse;
            }
            

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MovieShowingsExists(int id)
        {
            return db.MovieShowings.Count(e => e.movieShowingsId == id) > 0;
        }
    }
}