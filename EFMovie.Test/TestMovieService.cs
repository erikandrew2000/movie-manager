using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using EFMovie.Data.Models;
using EFMovie.Data.Services;
using Xunit;

namespace EFMovie.Test
{
    public class TestMovieService
    {
        private readonly IMovieService svc;

        public TestMovieService()
        {
            // general arrangement
            svc = new MovieService();
            svc.Initialise();
        }

        [Fact] //GetAllMovies
        public void GetAllMovies_EmptyDb_ShouldHaveZeroCount()
        {
            //act 
            var list = svc.GetAllMovies("Title");
            var count = list.Count;

            //assert
            Assert.Equal(0, count);
        }

        [Fact] //GetAllMovies
        public void GetAllMovies_SeededDbSortByDirector_ShouldReturnOrderedList()
        {
            //act
            MovieDbServiceSeeder.Seed(svc);
            var listOfTitles = svc.GetAllMovies("Director").Select(s => new { s.Director }).ToList();
            var director1 = new { Director = "Brian DePalma" };
            var director2 = new { Director = "Francis Ford Coppola" };
            var director3 = new { Director = "Gus Van Sant" };
            var director4 = new { Director = "Martin Scorsese" };
            var expectedOrder = new List<object> { director1, director2, director3, director4 };

            //assert:
            Assert.Equal(expectedOrder,listOfTitles );
        }

        [Fact] //GetAllMovies
        public void GetAllMovies_With2Movies_ShouldReturnTwo()
        {

            // act     
            svc.AddMovie(new Movie { Title = "TestTitle1", Director = "TestDirector1" });
            svc.AddMovie(new Movie { Title = "TestTitle2", Director = "TestDirector2" });
            var movies = svc.GetAllMovies("Title");
            var count = movies.Count;

            // assert
            Assert.Equal(2, count);
        }

        [Fact] //GetMovieById
        public void GetMovieById_WithNoMovies_ShouldReturnNull()
        {
            //act
            var movie = svc.GetMovieById(1);

            //assert
            Assert.Null(movie);
        }

        [Fact] //GetMovieById
        public void GetMovieById_WithThreeMovies_ShouldReturnSecondMovie()
        {
            //act
            var movie1 = new Movie { Title = "TestTitle1", Director = "TestDirector1" };
            var movie2 = new Movie { Title = "TestTitle2", Director = "TestDirector2" };
            var movie3 = new Movie { Title = "TestTitle3", Director = "TestDirector3" };
            svc.AddMovie(movie1);
            svc.AddMovie(movie2);
            svc.AddMovie(movie3);
            var test = svc.GetMovieById(2);

            //assert
            Assert.Equal(movie2, test);
        }

        [Fact] //DeleteMovie
        public void DeleteMovie_WithThreeMovies_ShouldReturnNull()
        {
            //act
            var movie1 = new Movie { Title = "TestTitle1", Director = "TestDirector1" };
            var movie2 = new Movie { Title = "TestTitle2", Director = "TestDirector2" };
            var movie3 = new Movie { Title = "TestTitle3", Director = "TestDirector3" };
            svc.AddMovie(movie1);
            svc.AddMovie(movie2);
            svc.AddMovie(movie3);
            var id = movie2.Id;
            var test = svc.DeleteMovie(id);
            var deleted = svc.GetMovieById(id);

            //assert
            Assert.Null(deleted);
        }

        [Fact] //DeleteMovie
        public void DeleteMovie_WithTreeMovies_ShouldReturnTrue()
        {
            //act
            var movie1 = new Movie { Title = "TestTitle1", Director = "TestDirector1" };
            var movie2 = new Movie { Title = "TestTitle2", Director = "TestDirector2" };
            var movie3 = new Movie { Title = "TestTitle3", Director = "TestDirector3" };
            svc.AddMovie(movie1);
            svc.AddMovie(movie2);
            svc.AddMovie(movie3);
            var deleted = svc.DeleteMovie(movie2.Id);

            //assert
            Assert.True(deleted);
        }

        [Fact] //DeleteMovie
        public void DeleteMovie_WithNonExistentId_ShouldReturnFalse()
        {
            //act
            var deleted = svc.DeleteMovie(4);

            Assert.False(deleted);
        }

        [Fact] //UpdateMovie
        public void UpdateMovie_WithCopy_ShouldReturnEqual()
        {
            //act
            var movie = new Movie { Title = "TestTitle1", Director = "TestDirector1", Year=1990 };
            svc.AddMovie(movie);
            var copy = new Movie { Id = movie.Id, Title = movie.Title, Director = movie.Director, Year=movie.Year };
            copy.Year = 2000;
            svc.UpdateMovie(copy);
            var copy2 = svc.GetMovieById(movie.Id);

            //assert
            Assert.Equal(2000,copy2.Year);
        }

        [Fact] //UpdateMovie
        public void UpdateMovie_WithIncorrectId_ShouldReturnNotEqual()
        {
            //act 
            var movie = new Movie { Title = "TestTitle1", Director = "TestDirector1", Year = 2000 };
            svc.AddMovie(movie);
            var copy = new Movie { Id = movie.Id, Title = movie.Title, Director = movie.Director, Year = movie.Year };
            copy.Id = 5;
            copy.Year = 2005;
            svc.UpdateMovie(copy);
            var copy2 = svc.GetMovieById(movie.Id);

            //assert
            Assert.NotEqual(2005, copy2.Year);
        }

        [Fact] //AddMovie
        public void AddMovie_WithMovieObject_ShouldReturnObject()
        {       
            //act
            var movie = new Movie { Title = "TestTitle1", Director = "TestDirector1" };
            var output = svc.AddMovie(movie);

            //assert
            Assert.Equal(movie,output);
        }

        [Fact] //AddMovie
        public void AddMovie_WithNullObject_ShouldReturnNull()
        {
            //act
            Movie movie = null;
            var output = svc.AddMovie(movie);

            //assert
            Assert.Null(output);
        }

        [Fact] //AddMovie
        public void AddMovie_With2Objects_ShouldBeTwoMoviesInDatabase()
        {
            //act
            svc.AddMovie(new Movie { Title = "TestTitle1", Director = "TestDirector1" });
            svc.AddMovie(new Movie { Title = "TestTitle2", Director = "TestDirector2" });
            var count = svc.GetAllMovies("Title").Count();

            //assert
            Assert.Equal(2, count);
        }

        [Fact] //GetReviewById
        public void GetReviewById_WithNonExistentId_ShouldReturnNull()
        {
            //act 
            var output = svc.GetReviewById(3);

            //assert
            Assert.Null(output);
        }

        [Fact] //getreviewbyid
        public void GetReviewbyId_WithReviewObject_ShouldReturnSecondObject()
        {
            //act
            MovieDbServiceSeeder.Seed(svc);
            var output = svc.GetReviewById(2);

            //assert
            Assert.Equal("Julie", output.Name);
        }

        [Fact] //AddReview
        public void AddReview_WithReviewObjects_ShouldReturnObject()
        {
            //act
            var m = new Movie { Title = "TestTitle", Director = "TestDirector" };
            var r = new Review { MovieId = 1, Comment = "Test", Name="Random" };
            var r1 = new Review { MovieId = 1, Comment = "Test2", Name = "Random2" };
            svc.AddMovie(m);
            svc.AddReview(r);
            var output = svc.AddReview(r1);
            
            Assert.Equal(r1, output);
            //Assert.Null(output);
        }

        [Fact] //AddReview
        public void AddReview_WithNullObject_ShouldReturnNull()
        {
            //act
            Review review = null;
            var output = svc.AddReview(review);

            //assert
            Assert.Null(output);
        }

        [Fact]
        public void DeleteReview_WithExistingReview_ShouldReturnTrue()
        {
            //arrange
            MovieDbServiceSeeder.Seed(svc);

            //act
            svc.DeleteReview(1);
            var output = svc.GetReviewById(1);

            //assert
            Assert.Null(output);
        }

        [Fact]
        public void DeleteReview_WithNullReview_ShouldReturnFalse()
        {
            //act
            var output = svc.DeleteReview(1);

            //assert
            Assert.False(output);
        }

    }
}