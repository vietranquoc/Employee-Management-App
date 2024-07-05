using BusinessObjects;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class LocationRepository : ILocationRepository
    {
        public void DeleteLocation(Location location) => LocationDAO.DeleteLocation(location);
        public Location? GetLocationById(string id) => LocationDAO.GetLocationById(id);

        public List<Location> GetLocations() => LocationDAO.GetLocations();

        public void InsertLocation(Location location) => LocationDAO.InsertLocation(location);

        public void UpdateLocation(Location location) => LocationDAO.UpdateLocation(location);
    }
}
