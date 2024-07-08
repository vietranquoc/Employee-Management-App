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


    }
}
