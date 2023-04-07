using System;
using DreamFlats.Data;
using DreamFlats.Logging;
using DreamFlats.Models;
using DreamFlats.Models.DTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace DreamFlats.Controllers
{
    /*
    "X" in "api/X" is the name of the API Controller. So in the Route attr, DreamFlats API is name of API - Controller text
    Alt: Just use "[controller]" i.e. 'controller' in square brackets
     */
    //[Route("api/[controller]")]
    [Route("api/DreamFlatsAPI")] // Shows "Route" to the Controller. Route is needed to remove the error related to "AttributeRoute"
    [ApiController] //notifies that the below class is an API Controller.
    public class DreamFlatsAPIController : ControllerBase // ControllerBase: contains common methods for data and users.
    {
        // For using SeriLog use below way
        //private readonly ILogger<DreamFlatsAPIController> _logger; // For logging.

        //public DreamFlatsAPIController(ILogger<DreamFlatsAPIController> logger) // DI is used for logging
        //{
        //    _logger = logger;
        //}

        // For custom logging, use below

        private readonly ILogging _logger;
        private readonly ApplicationDbContext _db;

        public DreamFlatsAPIController(ILogging logger)
        {
            _logger = logger;
        }

        public DreamFlatsAPIController(ApplicationDbContext db)
        {
            _db = db;
        }
        /*
        Below method is called an "EndPoint". "HTTPVerb" is required for the "EndPoint" or "Action" method.
        HttpVerb is defined NOT at controller level, but at ACTION method level i.e., EndPoint level
         */
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<FlatDTO>> GetFlats()
        {
            try
            {
                _logger.Log("Getting all flats", "success");
                return Ok(_db.Flats.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }

        [HttpGet("{id:int}", Name = "GetFlat")]
        //[ProducesResponseType(200, Type = typeof(FlatDTO))]
        //[ProducesResponseType(404)]
        //[ProducesResponseType(400)]
        //[ProducesResponseType(200)]
        //[ProducesResponseType(404)]
        //[ProducesResponseType(400)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<FlatDTO> GetFlats(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.Log("Get Villa Error with ID" + id, "error");
                    return BadRequest();
                }

                var flat = _db.Flats.FirstOrDefault(u => u.Id == id);
                if (flat == null)
                {
                    return NotFound();
                }
                return Ok(flat); // returns only 1 FlatDTO, that's why return type should be changed
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }

        }


        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<FlatDTO> CreateFlat([FromBody] FlatDTO flatDTO)
        {
            if (_db.Flats.FirstOrDefault(u => u.Name.ToLower() == flatDTO.Name.ToLower()) != null)
            {
                ModelState.AddModelError("", "Flat already exists");
                return BadRequest(ModelState);
            }
            if (flatDTO == null)
            {
                return BadRequest(flatDTO);
            }

            if (flatDTO.Id < 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            // Only a model or an object can be added to _db.Table.Add(x) method; x= Model
            // model is created like below
            Flat model = new()
            {
                Id = flatDTO.Id,
                Name = flatDTO.Name,
                Details = flatDTO.Details,
                SquareFeet = flatDTO.SquareFeet,
                Amenity = flatDTO.Amenity,
                ImageUrl = flatDTO.ImageUrl,
                Occupancy = flatDTO.Occupancy,
                Rate = flatDTO.Rate
            };

            //flatDTO.Id = _db.Flats.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            _db.Flats.Add(model);

            /* Save changes is required to persist the changes*/
            _db.SaveChanges();
            return CreatedAtRoute("GetFlat", new { id = flatDTO.Id }, flatDTO); //Used to route to a different Action Verb
        }


        [HttpDelete("{id:int}", Name = "DeleteFlat")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult DeleteFlat(int id) // In IActionResult there is not need to define return type i.e. <x> is not required as in ActionResult<x>
        {
            if (id == 0)
            {
                return BadRequest();
            }

            var flat = _db.Flats.FirstOrDefault(u => u.Id == id);

            if (flat == null)
            {
                return NotFound();
            }

            _db.Flats.Remove(flat);

            /*SaveChanges() is required to persist the changes*/
            _db.SaveChanges();
            return NoContent();
        }


        [HttpPut("{id:int}", Name = "UpdateFlat")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateFlat(int id, [FromBody]FlatDTO flatDTO) // In IActionResult there is not need to define return type i.e. <x> is not required as in ActionResult<x>
        {
            if (flatDTO == null || id != flatDTO.Id)
            {
                return BadRequest();
            }

            //var flat = _db.Flats.FirstOrDefault(u => u.Id == id);

            Flat model = new()
            {
                Id = flatDTO.Id,
                Name = flatDTO.Name,
                Details = flatDTO.Details,
                SquareFeet = flatDTO.SquareFeet,
                Amenity = flatDTO.Amenity,
                ImageUrl = flatDTO.ImageUrl,
                Occupancy = flatDTO.Occupancy,
                Rate = flatDTO.Rate
            };
            _db.Flats.Update(model);
            _db.SaveChanges();
            return NoContent();
        }

        [HttpPatch("{id:int}", Name = "UpdatePartialFlat")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialFlat(int id, JsonPatchDocument<FlatDTO> patchDTO) // In IActionResult there is not need to define return type i.e. <x> is not required as in ActionResult<x>
        {
            if (patchDTO == null || id == 0)
            {
                return BadRequest();
            }

            var flat = _db.Flats.FirstOrDefault(u => u.Id == id);

            if (flat == null)
            {
                return BadRequest();
            }

            FlatDTO flatDTO = new()
            {
                Id = flat.Id,
                Name = flat.Name,
                Details = flat.Details,
                SquareFeet = flat.SquareFeet,
                Amenity = flat.Amenity,
                ImageUrl = flat.ImageUrl,
                Occupancy = flat.Occupancy,
                Rate = flat.Rate
            };

            Flat flatModel = new Flat()
            {
                Id = flatDTO.Id,
                Name = flatDTO.Name,
                Details = flatDTO.Details,
                SquareFeet = flatDTO.SquareFeet,
                Amenity = flatDTO.Amenity,
                ImageUrl = flatDTO.ImageUrl,
                Occupancy = flatDTO.Occupancy,
                Rate = flatDTO.Rate
            };

            _db.Flats.Update(flatModel);
            _db.SaveChanges();

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return NoContent();
        }
    }
}

