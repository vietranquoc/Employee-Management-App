using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IRegionService
    {
        List<Region> GetRegions();
        void InsertRegion(Region region);
        void UpdateRegion(Region region);
        void DeleteRegion(Region region);
        Region? GetRegionById(int id);
        /*New feature*/
        List<Region> GetRegionsByName(string name);
        bool CheckIdExist(int id);
    }
}
