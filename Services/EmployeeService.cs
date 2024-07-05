﻿using BusinessObjects;
using Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository iEmployeeRepository;

        public EmployeeService()
        {
            iEmployeeRepository = new EmployeeRepository();
        }

        public void DeleteEmployee(Employee employee)
        {
            iEmployeeRepository.DeleteEmployee(employee);
        }

        public Employee? GetEmployeeById(int id)
        {
            return iEmployeeRepository.GetEmployeeById(id);
        }

        public List<Employee> GetEmployees()
        {
            return iEmployeeRepository.GetEmployees();
        }

        public void InsertEmployee(Employee employee)
        {
            iEmployeeRepository.InsertEmployee(employee);
        }

        public void UpdateEmployee(Employee employee)
        {
            iEmployeeRepository.UpdateEmployee(employee);
        }

        /* New features */
        public List<Employee> GetManagers()
        {
            var employees = GetEmployees();
            var managers =
                employees
                .Where(e => e.ManagerId.HasValue)
                .Join(
                    employees,
                    e => e.ManagerId,
                    m => m.EmployeeId,
                    (e, m) => m)
                .Distinct()
                .GroupBy(m => m.EmployeeId)
                .Select(g => g.First())
                .ToList();

            return managers;
        }
    }
}
