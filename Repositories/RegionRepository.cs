using BusinessObjects;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class RegionRepository : IRegionRepository
    {
        public void DeleteRegion(Region region) => RegionDAO.DeleteRegion(region);

        public Region? GetRegionById(int id) => RegionDAO.GetRegionById(id);

        public List<Region> GetRegions() => RegionDAO.GetRegions();

        public void InsertRegion(Region region) => RegionDAO.InsertRegion(region);

        public void UpdateRegion(Region region) => RegionDAO.UpdateRegion(region);
    }
}
