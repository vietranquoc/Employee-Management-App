using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public static class EmployeeDAO
    {
        public static EmployeeManagementContext context = new EmployeeManagementContext();

        public static List<Employee> GetEmployees()
        {
            return context.Employees.ToList();
        }

        public static void InsertEmployee(Employee employee)
        {
            context.Employees.Add(employee);
            context.SaveChanges();
        }

        public static void UpdateEmployee(Employee employee)
        {
            context.Employees.Update(employee);
            context.SaveChanges();
        }

        public static void DeleteEmployee(Employee employee)
        {
            context.Employees.Remove(employee);
            context.SaveChanges();
        }

        public static Employee? GetEmployeeById(int id)
        {
            return context.Employees
                        .Where(s => s.EmployeeId == id)
                        .FirstOrDefault();
        }
    }
}
