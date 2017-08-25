using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace TheWorld.Models
{
    public class WorldRepository : IWorldRepository
    {
        private readonly WorldContext _context;
        private readonly ILogger<WorldRepository> _logger;

        public WorldRepository(WorldContext context,ILogger<WorldRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IEnumerable<Trip> GetAllTrips()
        {
            _logger.LogInformation("Getting All Information From The Database");
            return _context.Trips.ToList();
        }

        public void AddTrips(Trip trip)
        {
            try
            {
                _context.Add(trip);  
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void AddStops(string tripsName, Stop newStop)
        {
            var trip = GetTripByName(tripsName);
            if (trip != null)
            {
                trip.Stops.Add(newStop);
                _context.Stops.Add(newStop);
            }
        }

        public async Task<bool> SaveChangesAsync()
        {
            return (await _context.SaveChangesAsync()) > 0;
        }

        public Trip GetTripByName(string tripName)
        {
            try
            {
                return 
                _context.Trips
                .Include(i=>i.Stops)
                .Where(i => i.Name == tripName)
                .FirstOrDefault();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public IEnumerable<Trip> GetTripsByUserName(string name)
        {
            return _context.Trips
                .Include(i=>i.Stops)
                .Where(i => i.UserName == name)
                .ToList();
        }
    }
}
