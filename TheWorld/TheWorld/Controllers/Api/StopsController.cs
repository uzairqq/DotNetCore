using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using TheWorld.Models;
using TheWorld.Services;

namespace TheWorld.Controllers.Api
{
    [Route("api/trip/{tripsName}/stops")]
    public class StopsController:Controller
    {
        private readonly IWorldRepository _repository;
        private readonly ILogger<StopsController> _logger;
        private readonly GeoCoordsService _coordsService;

        public StopsController(IWorldRepository repository,ILogger<StopsController> logger,GeoCoordsService coordsService)
        {
            _repository = repository;
            _logger = logger;
            _coordsService = coordsService;
        }

        [HttpGet("")]
        public IActionResult Get(string tripsName)
        {
            try
            {
                var trip = _repository.GetTripByName(tripsName);
                return Ok(Mapper.Map<IEnumerable<StopsViewModel>>(trip.Stops.OrderBy(s=>s.Order)).ToList());
            }
            catch (Exception ex)
            {
                _logger.LogError("Failed To Get Stops:{0}",ex);
            }
            return BadRequest("Failed To Return Stops");
        }

        [HttpPost("")]
        public async Task<IActionResult> Get(string tripsName,[FromBody] StopsViewModel stopsViewModel)
        {
            try
            {
                if (!ModelState.IsValid) return BadRequest("Model Not Valid In post");

                var newStop=Mapper.Map<Stop>(stopsViewModel);

                var result = await _coordsService.GetCoordsAsync(newStop.Name);
                if (!result.Success)
                {
                    _logger.LogError(result.Message);
                }
                else
                {
                    newStop.Latitude = result.Latitude;
                    newStop.Longitude = result.Longitude;

                    _repository.AddStops(tripsName, newStop);

                    if (await _repository.SaveChangesAsync())
                    {

                        return Created($"api/trips/{tripsName}/stops/{newStop.Name}",
                            Mapper.Map<StopsViewModel>(newStop));
                    }
                }

            }
            catch (Exception e)
            {
                _logger.LogError("Failed to Save Stop:{0}",e);
                
            }
            return BadRequest("Failed To save New Stop");
        }

    }
}
