using System;
using DreamFlats.Models;
using Microsoft.EntityFrameworkCore;

namespace DreamFlats.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            :base(options)
        {

        }

        /*
        1. For querying DbSets use LINQ, which will be converted into SQL statements.
         */
        public DbSet<Flat> Flats { get; set; }

        // To insert some data by default use:
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Flat>().HasData(

                new Flat()
                {
                    Id = 1,
                    Name = "Royal View",
                    Details = "Lorem epsum",
                    ImageUrl="",
                    Occupancy = 5,
                    Rate = 200,
                    SquareFeet = 550,
                    Amenity = "",
                    CreatedDate = DateTime.Now,
                    //ModifiedDate = DateTime.Now
                });
        }
    }
}

