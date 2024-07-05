using BusinessObjects;

namespace DataAccessLayer
{
    public static class RegionDAO
    {
        public static EmployeeManagementContext context = new EmployeeManagementContext();
        public static List<Region> GetRegions()
        {
            return context.Regions.ToList();
        }

        public static void InsertRegion(Region region)
        {
            context.Regions.Add(region);
            context.SaveChanges();
        }

        public static void UpdateRegion(Region region)
        {
            context.Regions.Update(region);
            context.SaveChanges();
        }

        public static void DeleteRegion(Region region)
        {
            context.Regions.Remove(region);
            context.SaveChanges();
        }

        public static Region? GetRegionById(int id)
        {
            return context.Regions.Find(id);
        }
    }
}
