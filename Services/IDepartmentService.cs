using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IDepartmentService
    {
        List<Department> GetDepartments();
        void InsertDepartment(Department department);
        void UpdateDepartment(Department department);
        void DeleteDepartment(Department department);
        Department? GetDepartmentById(int id);

        /* New features */
        //List<Department> GetDepartmentByName(string name);
        //List<Department> GetDepartmentsByManagerId(int managerId);
        //List<Department> GetDepartmentsByLocationId(string locationid);
        List<Department> FilterDepartment(string? searchName, int? searchManager, string? searchLocation);
    }
}
