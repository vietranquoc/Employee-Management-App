using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface ICountryService
    {
        List<Country> GetCountries();
        void InsertCountry(Country country);
        void UpdateCountry(Country country);
        void DeleteCountry(Country country);
        Country? GetCountryById(string countryId);
        /*New feature*/
        //List<Country> GetCountriesByName(string name);
        //List<Country> GetCountriesByRegionId(int regionId);
        bool checkIdExist(string id);
        List<Country> FilterCountries(string? name, int? regionId);
    }
}
