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
        [HttpGet("api/trips")]
        public IActionResult Get()
        {
            return Ok(new Trip() {Name = "UzairTrip",CreatedOn = DateTime.Now,Id = 777,UserName = "what the F..."});
        }
    }
}
