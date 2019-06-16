using System;
using System.Collections.Generic;
using System.Text;
using EFMovie.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using EFMovie.Data.Models;
using System.Linq;

namespace EFMovie.Data.Services
{
    public class MovieService : IMovieService
    {
        private readonly MovieDbContext db;


        public MovieService()
        {
            db = new MovieDbContext();
        }

        public void Initialise()
        {
            //call to Initialise() method from MovieDbContext:
            db.Initialise();
        }

        //method to get movies & order by Title, Director or Year:
        public List<Movie> GetAllMovies(string orderBy=null)
        {
            List<Movie> output=null;
            if (orderBy==null)
            {
                output = db.Movies.ToList();
            }
            if (orderBy == "Title")
            {
                output = db.Movies.OrderBy(s => s.Title).ToList();
            }

            if (orderBy=="Director")
            {
                output = db.Movies.OrderBy(s => s.Director).ToList();
            }

            if (orderBy=="Year")
            {
                output = db.Movies.OrderBy(s => s.Year).ToList();
            }

            return output;
        }

        public Movie GetMovieById(int id)
        {
            var movie = db.Movies.Include(s => s.Reviews).FirstOrDefault(s => s.Id == id);
            if (movie == null)
                return null;
            return movie;
        }
            
        public bool DeleteMovie(int id)
        {
            var movie = GetMovieById(id);
            if (movie == null)
                return false;
            db.Movies.Remove(movie);
            db.SaveChanges();

            if (GetMovieById(id) == null)
                return true;
            return false;
        }

        public bool UpdateMovie(Movie m)
        {
            var movie = GetMovieById(m.Id);
            if (movie == null)
                return false;

            // ** disconnect entity from EF Change tracking so we **
            // ** can update the new entity without a conflict    **
            db.Entry(movie).State = EntityState.Detached;

            // tell EF that this entity has changed
            db.Update(m);

            // save the changes
            db.SaveChanges();

            if (GetMovieById(m.Id).Equals(m))
                return true;

            return false;
        }

        public Movie AddMovie(Movie m)
        {
            if (m == null)
                return null;
            db.Movies.Add(m);
            db.SaveChanges();
            if (GetMovieById(m.Id).Equals(m))
                return m;
            return null;                
        }

        public Review GetReviewById(int id)
        {
            var review = db.Reviews.Where(s => s.Id == id).SingleOrDefault();
            if (review == null)
                return null;

            return review;
        }

        public Review AddReview(Review r)
        {
            if (r == null)
                return null;
           
            db.Reviews.Add(r);
            db.SaveChanges();

            //    return review;
            var review = GetReviewById(r.Id);
            if (review==r)
                return r;
            return null;           
        }

        public bool DeleteReview(int id)
        {
            var review = GetReviewById(id);
            if (review == null)
                return false;
            db.Reviews.Remove(review);
            db.SaveChanges();
            if (GetReviewById(id) == null)
                return true;
            return false;
        }
    }
}


