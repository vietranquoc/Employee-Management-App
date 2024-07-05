using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public static class LocationDAO
    {
        public static EmployeeManagementContext context = new EmployeeManagementContext();

        public static List<Location> GetLocations()
        {
            return context.Locations.ToList();
        }

        public static void InsertLocation(Location location)
        {
            context.Locations.Add(location);
            context.SaveChanges();
        }

        public static void UpdateLocation(Location location)
        {
            context.Locations.Update(location);
            context.SaveChanges();
        }

        public static void DeleteLocation(Location location)
        {
            context.Locations.Remove(location); 
            context.SaveChanges();
        }

        public static Location? GetLocationById(string  id)
        {
            return context.Locations.Find(id);
        }
    }
}
