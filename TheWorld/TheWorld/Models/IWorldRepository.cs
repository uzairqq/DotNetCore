using System.Collections.Generic;
using System.Threading.Tasks;
using TheWorld.Controllers.Api;

namespace TheWorld.Models
{
    public interface IWorldRepository
    {
        IEnumerable<Trip> GetAllTrips();

        void AddTrips(Trip trip);
        void AddStops(string tripsName, Stop newStop);
        Task<bool> SaveChangesAsync();
        Trip GetTripByName(string tripName);
        
    }
}