using System;
namespace DreamFlats.Models
{
    public class Flat
    {
        public Flat()
        {
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime CreatedDate { get; set; }

        public int Occupancy { get; set; }

        public int SquareFeet { get; set; }
    }
}

