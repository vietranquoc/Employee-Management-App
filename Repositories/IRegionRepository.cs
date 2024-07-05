using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IRegionRepository
    {
        List<Region> GetRegions();
        void InsertRegion(Region region);
        void UpdateRegion(Region region);
        void DeleteRegion(Region region);
        Region? GetRegionById(int id);
    }
}
