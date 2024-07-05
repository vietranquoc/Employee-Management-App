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

        public static List<Deparment> GetCountries()
        {
            return context.Countries.ToList();
        }

        public static void InsertCountry(Deparment country)
        {
            context.Countries.Add(country);
            context.SaveChanges();
        }

        public static void UpdateCountry(Deparment country)
        {
            context.Countries.Update(country);
            context.SaveChanges();
        }

        public static void DeleteCountry(Deparment country)
        {
            context.Countries.Remove(country);
            context.SaveChanges();
        }

        public static Deparment? GetCountryById(string countryId)
        {
            return context.Countries.Find(countryId);
        }
    }
}
