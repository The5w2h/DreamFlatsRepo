using System;
using System.ComponentModel.DataAnnotations;

namespace DreamFlats.Models.DTO
{
    // DTOs are created to ensure there is wrapper between Model and Controller
    public class FlatDTO
    {
        public FlatDTO()
        {
        }

        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Name { get; set; }

        public string Details { get; set; }

        [Required]
        public double Rate { get; set; }

        public string ImageUrl { get; set; }

        public string Amenity { get; set; }

        public int Occupancy { get; set; }

        public int SquareFeet { get; set; }
    }
}

