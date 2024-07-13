using BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository iCountryRepository;

        public CountryService()
        {
            iCountryRepository = new CountryRepository();
        }

        public void DeleteCountry(Country country)
        {
            iCountryRepository.DeleteCountry(country);
        }

        public List<Country> GetCountries()
        {
            return iCountryRepository.GetCountries();
        }

        public Country? GetCountryById(string countryId)
        {
            return iCountryRepository.GetCountryById(countryId);
        }

        public void InsertCountry(Country country)
        {
            iCountryRepository.InsertCountry(country);
        }

        public void UpdateCountry(Country country)
        {
            iCountryRepository.UpdateCountry(country);
        }

        /*New feature*/
        /*
       public List<Country> GetCountriesByName(string name)
       {
           var countries =
               GetCountries()
               .Where(c => c.CountryName.ToLower().Contains(name))
               .ToList();
           return countries;
       }

       public List<Country> GetCountriesByRegionId(int regionId)
       {
           var countries =
               GetCountries()
               .Where(c => c.RegionId == regionId)
               .ToList();
           return countries;
       }
        */


        public bool checkIdExist(string id)
        {
            var check =
               GetCountries()
               .Any(c => c.CountryId == id);
            return check;
        }

        public List<Country> FilterCountries(string? name, int? regionId)
        {
            var allCountrys = GetCountries().AsQueryable();

            if (!string.IsNullOrEmpty(name))
            {
                allCountrys = allCountrys.Where(c => c.CountryName.Contains(name, StringComparison.OrdinalIgnoreCase));
            }
            if (regionId.HasValue && regionId != 0)
            {
                allCountrys = allCountrys.Where(c => c.RegionId == regionId);
            }

            return allCountrys.ToList();
        }

    }
}
