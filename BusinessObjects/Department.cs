using System;
using System.Collections.Generic;

namespace BusinessObjects;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string? DepartmentName { get; set; }

    public int? ManagerId { get; set; }

    public string? LocationId { get; set; }

    public virtual ICollection<Employee> Employees { get; set; } = new List<Employee>();

    public virtual Location? Location { get; set; }

    public virtual Employee? Manager { get; set; }
}
