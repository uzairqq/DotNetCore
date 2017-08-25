using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using TheWorld.Models;

namespace TheWorld.Controllers.Api
{
    [Route("api/trips")]
    [Authorize]
    public class TripsController:Controller
    {
        private readonly IWorldRepository _worldRepository;
        private readonly ILogger<TripsController> _logger;

        public TripsController(IWorldRepository worldRepository,ILogger<TripsController> logger )
        {
            _worldRepository = worldRepository;
            _logger = logger;
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
                _logger.LogError($"Error in Get Call in Trips Controller:{e.Message}");
                return BadRequest("Error Occured");
                
            }
            
        }

        [HttpPost]
        public IActionResult Post([FromBody] TripViewModel thetrips)
        {
            if (ModelState.IsValid)
            {
                var newTrip = Mapper.Map<Trip>(thetrips);
                _worldRepository.AddTrips(newTrip);
                

                return Created($"api/trips:{thetrips.Name}",newTrip);
            }
            return BadRequest(ModelState);
        }

        
    }
}
