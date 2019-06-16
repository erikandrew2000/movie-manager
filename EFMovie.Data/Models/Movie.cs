using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace EFMovie.Data.Models
{
    // genre enumeration goes here
    public enum Genre { Action, Comedy, Family, Horror, Romance, SciFi, Thriller, Western,  War};

    public class Movie
    {
        //constructor; when Movie object is created, constructor creates an empty list of reviews
        public Movie()
        {
            Reviews = new List<Review>();
        }

        //primary key named according to convention:
        public int Id { set; get; }

        public Genre Genre { set; get; }

        [Required]
        [MaxLength(100)]
        public string Title { set; get; }

        public int Year { set; get; }

        [Required]
        [MaxLength(100)]
        public string Director { set; get; }

        [Range(1,400)]
        public int Duration { set; get; }

        public string Cast { set; get; }

        [Range(0.0,500.0)]
        public double Budget { set; get; }

        [Url]
        public string PosterUrl { set; get; }

        [MaxLength(500)]
        public string Plot { set; get; }

        //navigation property defining 1:N relationship:
        public ICollection<Review> Reviews { get; set; }

        //rating attribute calculating average from all ratings of the movie:
        public int Rating
        {
            get
            {
                if (ReviewsCount == 0)
                    return 0;
                return (int)(Reviews.Average(s => s.Rating)*10);
            }
        }

        //review count attribute counting all the reviews of the movie:
        public int ReviewsCount
        {
            get
            {
                return Reviews.Count;
            }
        }
    }


}