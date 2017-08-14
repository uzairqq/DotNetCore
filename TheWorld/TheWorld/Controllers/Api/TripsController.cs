using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TheWorld.Models;

namespace TheWorld.Controllers.Api
{
    public class TripsController:Controller
    {
        private readonly IWorldRepository _worldRepository;

        public TripsController(IWorldRepository worldRepository)
        {
            _worldRepository = worldRepository;
        }

        [HttpGet("api/trips")]
        public IActionResult Get()
        {
            return Ok(_worldRepository.GetAllTrips());
        }
    }
}
