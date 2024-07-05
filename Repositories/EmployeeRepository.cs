using BusinessObjects;
using DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public void DeleteEmployee(Employee employee) => EmployeeDAO.DeleteEmployee(employee);

        public Employee? GetEmployeeById(int id) => EmployeeDAO.GetEmployeeById(id);

        public List<Employee> GetEmployees() => EmployeeDAO.GetEmployees();

        public void InsertEmployee(Employee employee) => EmployeeDAO.InsertEmployee(employee);

        public void UpdateEmployee(Employee employee) => EmployeeDAO.UpdateEmployee(employee);
    }
}
