using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ILocationService
    {
        List<Location> GetLocations();
        void InsertLocation(Location location);
        void UpdateLocation(Location location);
        void DeleteLocation(Location location);
        Location? GetLocationById(string id);

        /* New Features */
        List<Location> GetLocaionsByCountryId(string countryId);
        List<Location> GetLocationByCity(string searchText);
        List<Location> GetLocationByStateProvince(string searchText);
        bool CheckIdExist(string id);
        List<Location> FilterLocations(string? countryId, string? city, string? stateProvince); 

    }
}
