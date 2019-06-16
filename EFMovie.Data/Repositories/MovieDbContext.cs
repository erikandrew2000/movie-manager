using System;
using Microsoft.EntityFrameworkCore;
using EFMovie.Data.Models;

namespace EFMovie.Data.Repositories
{
    public class MovieDbContext : DbContext
    {
        // create DbSets 
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Review> Reviews { get; set; }

        //set up DB:
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .EnableSensitiveDataLogging(true)
                .UseSqlite("Filename=MovieDatabase.db");
        }

        public void Initialise()
        {
            Database.EnsureDeleted();
            Database.EnsureCreated();
        }

    }
}