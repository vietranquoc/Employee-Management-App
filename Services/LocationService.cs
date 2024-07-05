using BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class LocationService : ILocationService
    {
        private readonly ILocationRepository iLocationRepository;

        public LocationService()
        {
            iLocationRepository = new LocationRepository();
        }

        public void DeleteLocation(Location location)
        {
            iLocationRepository.DeleteLocation(location);
        }

        public Location? GetLocationById(string id)
        {
            return iLocationRepository.GetLocationById(id);
        }

        public List<Location> GetLocations()
        {
            return iLocationRepository.GetLocations();
        }

        public void InsertLocation(Location location)
        {
            iLocationRepository.InsertLocation(location);
        }

        public void UpdateLocation(Location location)
        {
            iLocationRepository.UpdateLocation(location);
        }
    }
}
