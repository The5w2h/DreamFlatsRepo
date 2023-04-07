using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DreamFlats.Models
{
    public class Flat
    {
        public Flat()
        {
        }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)] // For making sure this is an Identity col.
        public int Id { get; set; }

        public string Name { get; set; }

        public string Details { get; set; }

        public double Rate { get; set; }

        public string ImageUrl { get; set; }

        public string Amenity { get; set; }

        public int Occupancy { get; set; }

        public int SquareFeet { get; set; }

        public DateTime CreatedDate { get; set; }

        public DateTime ModifiedDate { get; set; }
    }
}

