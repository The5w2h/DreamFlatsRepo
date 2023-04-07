using System;
using DreamFlats.Models.DTO;

namespace DreamFlats.Data
{
    public static class FlatStore
    {
       public static List<FlatDTO> flatList = new List<FlatDTO>
            {
                new FlatDTO {Id = 1, Name = "Lake View", SquareFeet = 100, Occupancy = 4},
                new FlatDTO {Id = 2, Name = "City View", SquareFeet = 400, Occupancy= 10}
            };
    }
}

