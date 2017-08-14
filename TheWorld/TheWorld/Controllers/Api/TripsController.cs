using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using TheWorld.Models;

namespace TheWorld.Controllers.Api
{
    [Route("api/trips")]
    public class TripsController:Controller
    {
        private readonly IWorldRepository _worldRepository;

        public TripsController(IWorldRepository worldRepository)
        {
            _worldRepository = worldRepository;
        }

        [HttpGet]        //[HttpGet("/foo")]
        public IActionResult Get()
        {
            try
            {
                var results = _worldRepository.GetAllTrips();

                return Ok(Mapper.Map<IEnumerable<TripViewModel>>(results));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return BadRequest("Error Occured");
                
            }
            
        }

        [HttpPost]
        public IActionResult Post([FromBody] TripViewModel thetrips)
        {
            if (ModelState.IsValid)
            {
                var newTrip = Mapper.Map<Trip>(thetrips);
                

                return Created($"api/trips:{thetrips.Name}",newTrip);
            }
            return BadRequest(ModelState);
        }
    }
}
