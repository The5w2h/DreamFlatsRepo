using System;
using DreamFlats.Data;
using DreamFlats.Models;
using DreamFlats.Models.DTO;
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
        public DreamFlatsAPIController()
        {
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
                return Ok(FlatStore.flatList);
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
                    return BadRequest();
                }

                var flat = FlatStore.flatList.FirstOrDefault(u => u.Id == id);
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
            if (FlatStore.flatList.FirstOrDefault(u => u.Name.ToLower() == flatDTO.Name.ToLower()) != null)
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

            flatDTO.Id = FlatStore.flatList.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            FlatStore.flatList.Add(flatDTO);

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

            var flat = FlatStore.flatList.FirstOrDefault(u => u.Id == id);

            if (flat == null)
            {
                return NotFound();
            }

            FlatStore.flatList.Remove(flat);
            return NoContent();
        }


        [HttpPut("{id:int}", Name = "UpdateFlat")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateFlat(int id, [FromBody]FlatDTO flatDTO) // In IActionResult there is not need to define return type i.e. <x> is not required as in ActionResult<x>
        {
            if (flatDTO == null || id != flatDTO.Id)
            {
                return BadRequest();
            }

            var flat = FlatStore.flatList.FirstOrDefault(u => u.Id == id);

            flat.Name = flatDTO.Name;
            flat.Occupancy = flatDTO.Occupancy;
            flat.SquareFeet = flatDTO.SquareFeet;

            return NoContent();
        }
    }
}

