using BusinessObjects;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class CountryRepository : ICountryRepository
    {
        public void DeleteCountry(Country country) => CountryDAO.DeleteCountry(country);

        public List<Country> GetCountries() => CountryDAO.GetCountries();

        public Country? GetCountryById(string countryId) => CountryDAO.GetCountryById(countryId);

        public void InsertCountry(Country country) => CountryDAO.InsertCountry(country);

        public void UpdateCountry(Country country) => CountryDAO.UpdateCountry(country);
    }
}
