using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public static class DepartmentDAO
    {
        public static EmployeeManagementContext context = new EmployeeManagementContext();

        public static List<Department> GetDepartments()
        {
            return context.Departments.ToList();
        }

        public static void InsertDepartment(Department department)
        {
            context.Departments.Add(department);
            context.SaveChanges();
        }

        public static void UpdateDepartment(Department department)
        {
            context.Departments.Update(department);
            context.SaveChanges();
        }

        public static void DeleteDepartment(Department department)
        {
            context.Departments.Remove(department);
            context.SaveChanges();
        }

        public static Department? GetDepartmentById(int id)
        {
            return context.Departments.Find(id);
        }
    }
}
