using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using EFMovie.Data.Models;
using EFMovie.Data.Services;
using EFMovie.Web.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EFMovie.Web.Controllers
{
    public class MovieController : BaseController
    {
        private readonly MovieService svc;

        public MovieController()
        {
            svc = new MovieService();
        }

        // GET: Movies
        public ViewResult Index(string order=null)
        {
            var movies = svc.GetAllMovies(order);

            //display the list of movies:
            return View(movies);
        }

        // GET: Movie/Details/5
        public IActionResult Details(int id)
        {
            var movie = svc.GetMovieById(id);
            if (movie == null)
            {
                //display alert if movie not found:
                Alert("Movie not found", AlertType.warning);

                //if movie not found, go back to index:
                return RedirectToAction(nameof(Index));
            }

            //display movie:
            return View(movie);
        }

        //// GET: Movie/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movie/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Title,Director,Budget,Actors,Year,Duration,Genre,PosterUrl,Plot")] Movie m)
        {
            if (ModelState.IsValid)
            {
                svc.AddMovie(m);
                //display alert to confirm movie was added:
                Alert("Movie added to list", AlertType.success);

                //once movie is added, return to index
                return RedirectToAction(nameof(Index));
            }

            // redisplay the form for editing
            return View(m);
        }

        //// GET: Movie/Edit/5
        public IActionResult Edit(int id)
        { 
            //retrieve movie by id:
            var m = svc.GetMovieById(id);

            //if movie not found return to index:
            if (m == null)
            {
                Alert("Movie not found", AlertType.warning);
                return RedirectToAction(nameof(Index));
            }
            //return view of movie for editing:
           return View(m);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Title,Director,Budget,Cast,Year,Duration,Genre,PosterUrl,Plot")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                svc.UpdateMovie(movie);
                //display alert to confirm movie edited:
                Alert("Movie edited successfully", AlertType.success);

                //once movie is updated, go back to index:
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }
 
        //// GET: Movie/Delete/5
        public IActionResult Delete(int id)
        {
            //retrieve movie by id:
            var m = svc.GetMovieById(id);

            //if movie not found redirect to index:
            if (m == null)
            {
                Alert("Movie not found", AlertType.warning);
                return RedirectToAction(nameof(Index));
            }
            //return view of movie for editing:
            return View(m);
        }

        //// POST: Movie/Delete/5
        [HttpPost] //, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            svc.DeleteMovie(id);

            //display alert to confirm movie deleted:
            Alert("Movie deleted successfully", AlertType.success);

            //once movie is deleted, return to index:
            return RedirectToAction(nameof(Index));
        }    
  
        /// GET: Movie/AddReview/1 
        [HttpGet]
        public IActionResult AddReview(int id) 
        {
            var r = new Review();
            r.MovieId = id;
            r.On = DateTime.Now;
            return View(r);
        }
 
        /// POST: Movie/AddReview/1 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddReview([Bind("On,MovieId,Name,Comment,Rating")] Review review)
        {
            if (ModelState.IsValid)
            {
                svc.AddReview(review);

                //display alert to confirm review added:
                Alert("Review added successfully", AlertType.success);

                //once review is added, redirect to the details of the same movie:
                return RedirectToAction(nameof(Details), new { Id = review.MovieId } );
            }

            return View(review);
        }
        
        /// GET: Movie/DeleteReview/1 
        public IActionResult DeleteReview(int id)
        {
            var r = svc.GetReviewById(id);

            //if review not found, redirect to index:
            if (r == null)
            {
                Alert("Review not found", AlertType.warning);
                return RedirectToAction(nameof(Index));
            }
             return View(r);
        }
        
        /// POST: Movie/DeleteReview/1 
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteReviewConfirmed(int id)
        {
            var review = svc.GetReviewById(id);
            svc.DeleteReview(id);

            //display alert confirming review deleted:
            Alert("Review deleted successfully", AlertType.success);

            //once review is deleted, redirect to details of the same movie:
            return RedirectToAction(nameof(Details), new { Id = review.MovieId });
        }
    }
}