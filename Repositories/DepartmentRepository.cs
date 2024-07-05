using BusinessObjects;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        public void DeleteDepartment(Department department) => DepartmentDAO.DeleteDepartment(department);

        public Department? GetDepartmentById(int id) => DepartmentDAO.GetDepartmentById(id);

        public List<Department> GetDepartments() => DepartmentDAO.GetDepartments();

        public void InsertDepartment(Department department) => DepartmentDAO.InsertDepartment(department);

        public void UpdateDepartment(Department department) => DepartmentDAO.UpdateDepartment(department);
    }
}
