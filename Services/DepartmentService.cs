using BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository iDepartmentRepository;

        public DepartmentService()
        {
            iDepartmentRepository = new DepartmentRepository();
        }

        public void DeleteDepartment(Department department)
        {
            iDepartmentRepository.DeleteDepartment(department);
        }

        public Department? GetDepartmentById(int id)
        {
            return iDepartmentRepository.GetDepartmentById(id); 
        }

        public List<Department> GetDepartments()
        {
            return iDepartmentRepository.GetDepartments();
        }

        public void InsertDepartment(Department department)
        {
            iDepartmentRepository.InsertDepartment(department);
        }

        public void UpdateDepartment(Department department)
        {
            iDepartmentRepository.UpdateDepartment(department);
        }

        /* New features */
        public List<Department> GetDepartmentByName(string name)
        {
            var departments = 
                GetDepartments()
                .Where(d => d.DepartmentName.Contains(name))
                .ToList();
            return departments;
        }

        public List<Department> GetDepartmentsByManagerId(int managerId)
        {
            var departments =
                GetDepartments()
                .Where(d => d.ManagerId == managerId)
                .ToList();
            return departments;
        }

        public List<Department> GetDepartmentsByLocationId(string locationId)
        {
            var departments =
                GetDepartments()
                .Where(d => d.LocationId == locationId)
                .ToList();
            return departments;
        }
    }
}
