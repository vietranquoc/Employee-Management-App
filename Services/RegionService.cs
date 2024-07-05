using BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class RegionService : IRegionService
    {
        private readonly IRegionRepository iRegionRepository;

        public RegionService()
        {
            iRegionRepository = new RegionRepository();
        }

        public void DeleteRegion(Region region)
        {
            iRegionRepository.DeleteRegion(region);
        }

        public Region? GetRegionById(int id)
        {
            return iRegionRepository.GetRegionById(id); 
        }

        public List<Region> GetRegions()
        {
            return iRegionRepository.GetRegions();
        }

        public void InsertRegion(Region region)
        {
            iRegionRepository.InsertRegion(region);
        }

        public void UpdateRegion(Region region)
        {
            iRegionRepository.UpdateRegion(region);
        }
    }
}
