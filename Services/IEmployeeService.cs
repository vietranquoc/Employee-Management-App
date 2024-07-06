using BusinessObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IEmployeeService
    {
        List<Employee> GetEmployees();
        void InsertEmployee(Employee employee);
        void UpdateEmployee(Employee employee);
        void DeleteEmployee(Employee employee);
        Employee? GetEmployeeById(int id);
        /* New features */
        List<Employee> GetManagers();
        List<Employee> GetEmployeesByName(string name);
        List<Employee> GetEmployeesBySalary(double minSalary, double maxSalary);
        List<Employee> GetEmployeesByCommission(double minCommission, double maxCommission);
        List<Employee> GetEmployeesByJobId(string jobId);
        List<Employee> GetEmployeesByManagerId(int managerId);
        List<Employee> GetEmployeesByDepartmentId(int departmentId);
        List<Employee> GetEmployeesByYearOfHireDate(int yearOfHireDate);
    }
}
