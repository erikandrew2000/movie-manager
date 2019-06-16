using System;
using System.ComponentModel.DataAnnotations;

namespace EFMovie.Data.Models
{
   
    public class Review {

        //primary key named according to convention:
        public int Id { set; get; }

        // foreign key named according to convention:
        public int MovieId { set; get; }

        [Required]
        [MaxLength(100)]
        public string Name { set; get; }

        [Required]
        public DateTime On { set; get; }

        [Required]
        [MaxLength(250)]
        public string Comment { set; get; }

        [Required]
        [Range(1,10)]
        public int Rating { set; get; }

        //navigation property defining 1:N relationship:
        public Movie Movie { set; get; }

    }
}