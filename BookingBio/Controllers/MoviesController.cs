﻿using System;
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

        // GET: api/Movies/5
        [ResponseType(typeof(Movies))]
        public IHttpActionResult GetMovies(int id)
        {
            Movies movies = db.Movies.Find(id);
            if (movies == null)
            {
                return NotFound();
            }

            return Ok(movies);
        }

        // PUT: api/Movies/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMovies(int id, Movies movies)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != movies.movieId)
            {
                return BadRequest();
            }

            db.Entry(movies).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MoviesExists(id))
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

        // POST: api/Movies
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
        [ResponseType(typeof(Movies))]
        public IHttpActionResult DeleteMovies(int id)
        {
            Movies movies = db.Movies.Find(id);
            if (movies == null)
            {
                return NotFound();
            }

            db.Movies.Remove(movies);
            db.SaveChanges();

            return Ok(movies);
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