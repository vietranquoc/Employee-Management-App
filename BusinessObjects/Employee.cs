using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace BusinessObjects;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string? FirstName { get; set; }

    public string? LastName { get; set; }

    public string? Email { get; set; }

    public string? Phone { get; set; }

    public DateOnly? HireDate { get; set; }

    public string? JobId { get; set; }

    public double? Salary { get; set; }

    public double? CommissionPct { get; set; }

    public int? ManagerId { get; set; }

    public int? DepartmentId { get; set; }

    public virtual Department? Department { get; set; }

    public virtual ICollection<Department> Departments { get; set; } = new List<Department>();

    public virtual ICollection<Employee> InverseManager { get; set; } = new List<Employee>();

    public virtual Job? Job { get; set; }

    public virtual Employee? Manager { get; set; }

    [NotMapped] //lưu trữ giá trị tạm thời này khi tải dữ liệu
    public int Status { get; set; } = 1;
}
