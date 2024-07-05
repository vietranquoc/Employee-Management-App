using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public static class CountryDAO
    {
        public static EmployeeManagementContext context = new EmployeeManagementContext();

        public static List<Country> GetCountries()
        {
            return context.Countries.ToList();
        }

        public static void InsertCountry(Country country)
        {
            context.Countries.Add(country);
            context.SaveChanges();
        }

        public static void UpdateCountry(Country country)
        {
            context.Countries.Update(country);
            context.SaveChanges();
        }

        public static void DeleteCountry(Country country)
        {
            context.Countries.Remove(country);
            context.SaveChanges();
        }

        public static Country? GetCountryById(string countryId)
        {
            return context.Countries.Find(countryId);
        }
    }
}
