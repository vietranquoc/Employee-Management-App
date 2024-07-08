using BusinessObjects;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : Window
    {
        private readonly IEmployeeService iEmployeeService;
        private readonly IJobService iJobService;
        private readonly IDepartmentService iDepartmentService;

        public int? CurrentUserRole { get; set; } // Store current user's role
        public EmployeeWindow()
        {
            InitializeComponent();
            iEmployeeService = new EmployeeService();
            iJobService = new JobService();
            iDepartmentService = new DepartmentService();
        }

        private void ApplyAuthorization()
        {
            if (CurrentUserRole != 1 && CurrentUserRole != 2)
            {
                btnCreate.IsEnabled = false;
                btnUpdate.IsEnabled = false;
                btnDelete.IsEnabled = false;
            }
        }


        private void LoadEmployees()
        {
            try
            {
                dgData.ItemsSource = null;
                var employees = iEmployeeService.GetEmployees();
                dgData.ItemsSource = employees;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Can not load Employees");
            }
        }

        private void LoadJob()
        {
            try
            {
                var jobs = iJobService.GetJobs();
                cboJob.ItemsSource = jobs;
                cboJob.DisplayMemberPath = "JobTitle";
                cboJob.SelectedValuePath = "JobId";

                var searchJob = iJobService.GetJobs();
                searchJob.Add(new Job { JobId = "ALL", JobTitle = "ALL" });
                cboSeachJob.ItemsSource = searchJob;
                cboSeachJob.DisplayMemberPath = "JobTitle";
                cboSeachJob.SelectedValuePath = "JobId";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Can not load jobs");
            }
        }

        private void LoadDepartment()
        {
            try
            {
                var departments = iDepartmentService.GetDepartments();
                cboDepartment.ItemsSource = departments;
                cboDepartment.DisplayMemberPath = "DepartmentName";
                cboDepartment.SelectedValuePath = "DepartmentId";

                var searchDepart = iDepartmentService.GetDepartments();
                searchDepart.Add(new Department { DepartmentId = 0, DepartmentName = "ALL" });
                cboSeachDepartment.ItemsSource = searchDepart;
                cboSeachDepartment.DisplayMemberPath = "DepartmentName";
                cboSeachDepartment.SelectedValuePath = "DepartmentId";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Can not load jobs");
            }
        }

        private void LoadManager()
        {
            try
            {
                var managers = iEmployeeService.GetManagers();
                cboManager.ItemsSource = managers;
                cboManager.DisplayMemberPath = "EmployeeId";
                cboManager.SelectedValuePath = "EmployeeId";

                var searchManager = iEmployeeService.GetManagers();
                searchManager.Add(new Employee { EmployeeId = 999, ManagerId = 999 });
                cboSeachManager.ItemsSource = searchManager;
                cboSeachManager.DisplayMemberPath = "EmployeeId";
                cboSeachManager.SelectedValuePath = "EmployeeId";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Can not load managers");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ApplyAuthorization();
            LoadEmployees();
            LoadJob();
            LoadManager();
            LoadDepartment();
        }

        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            if (dataGrid.ItemsSource != null)
            {
                DataGridRow row = dataGrid.ItemContainerGenerator
                    .ContainerFromIndex(dataGrid.SelectedIndex) as DataGridRow;
                DataGridCell cell = dataGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;

                string employeeId = ((TextBlock)cell.Content).Text;
                if (!employeeId.Equals(""))
                {
                    Employee employee = iEmployeeService.GetEmployeeById(int.Parse(employeeId));
                    txtEmployeeId.Text = employee.EmployeeId.ToString();
                    txtFirstName.Text = employee.FirstName;
                    txtLastName.Text = employee.LastName;
                    txtEmail.Text = employee.Email;
                    txtPhone.Text = employee.Phone;
                    dpHireDate.Text = employee.HireDate.ToString();
                    cboJob.SelectedValue = employee.JobId;
                    txtSalary.Text = employee.Salary.ToString();
                    txtCommission.Text = employee.CommissionPct.ToString();
                    cboManager.SelectedValue = employee.ManagerId;
                    cboDepartment.SelectedValue = employee.DepartmentId;
                }
            }
        }

        private void txtSeachText_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string text = txtSeachText.Text;
                dgData.ItemsSource = null;
                var employeesFilter = iEmployeeService.GetEmployeesByName(text);
                dgData.ItemsSource = employeesFilter;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Can not load employees by name");
            }
        }

        private void cboSeachJob_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string jobId = cboSeachJob.SelectedValue.ToString();
                dgData.ItemsSource = null;
                if (jobId == "ALL")
                {
                    var employeesFilter = iEmployeeService.GetEmployees();
                    dgData.ItemsSource = employeesFilter;
                }
                else
                {
                    var employeesFilter = iEmployeeService.GetEmployeesByJobId(jobId);
                    dgData.ItemsSource = employeesFilter;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Can not load employees by job");
            }
        }

        private void txtFilterCommission_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                double minCommission = 0, maxCommission = 0;
                if (txtFilterCommissionMin.Text != "") double.TryParse(txtFilterCommissionMin.Text, out minCommission);
                if (txtFilterCommissionMax.Text != "") double.TryParse(txtFilterCommissionMax.Text, out maxCommission);

                dgData.ItemsSource = null;
                if (maxCommission != 0 && minCommission > maxCommission)
                {
                    MessageBox.Show("Min commission must be less than Max commission!");
                }
                else
                {
                    var employeeFilter = iEmployeeService.GetEmployeesByCommission(minCommission, maxCommission);
                    dgData.ItemsSource = employeeFilter;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Can not load employee by commission");
            }
        }

        private void txtFilterSalary_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                double minSalary = 0, maxSalary = 0;
                if (txtFilterSalaryMin.Text != "") double.TryParse(txtFilterSalaryMin.Text, out minSalary);
                if (txtFilterSalaryMax.Text != "") double.TryParse(txtFilterSalaryMax.Text, out maxSalary);

                dgData.ItemsSource = null;
                if (maxSalary != 0 && minSalary > maxSalary)
                {
                    MessageBox.Show("Min salary must be less than Max salary!");
                }
                else
                {
                    var employeeFilter = iEmployeeService.GetEmployeesBySalary(minSalary, maxSalary);
                    dgData.ItemsSource = employeeFilter;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Can not load employee by salary");
            }
        }

        private void cboSeachManager_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int idManager = int.Parse(cboSeachManager.SelectedValue.ToString());
                dgData.ItemsSource = null;
                if (idManager == 999)
                {
                    var employeesFilter = iEmployeeService.GetEmployees();
                    dgData.ItemsSource = employeesFilter;
                }
                else
                {
                    var employeesFilter = iEmployeeService.GetEmployeesByManagerId(idManager);
                    dgData.ItemsSource = employeesFilter;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Can not load employee by manager");
            }
        }

        private void cboSeachDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                int departmentId = int.Parse(cboSeachDepartment.SelectedValue.ToString());
                dgData.ItemsSource = null;
                if (departmentId == 0)
                {
                    var employeesFilter = iEmployeeService.GetEmployees();
                    dgData.ItemsSource = employeesFilter;
                }
                else
                {
                    var employeesFilter = iEmployeeService.GetEmployeesByDepartmentId(departmentId);
                    dgData.ItemsSource = employeesFilter;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Can not load employees by department");
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentUserRole != 1 && CurrentUserRole != 2)
            {
                MessageBox.Show("You do not have permission to create a employee.", "Permission Denied");
                return;
            }
            try
            {
                if (cboJob.SelectedValue == null)
                {
                    MessageBox.Show("Please choose a job.", "Input Error");
                    return;
                }
                if (cboDepartment.SelectedValue == null)
                {
                    MessageBox.Show("Please choose a department.", "Input Error");
                    return;
                }
                if (txtEmployeeId.Text.ToString().Trim().Length <= 0 ||
                    txtFirstName.Text.ToString().Trim().Length <= 0 ||
                    txtLastName.Text.ToString().Trim().Length <= 0 ||
                    txtEmail.Text.ToString().Trim().Length <= 0 ||
                    txtPhone.Text.ToString().Trim().Length <= 0 ||
                    txtSalary.Text.ToString().Trim().Length <= 0 ||
                    txtCommission.Text.ToString().Trim().Length <= 0)
                {
                    MessageBox.Show("Please enter char not white space");
                    return;
                }
                Employee employee = new Employee()
                {
                    EmployeeId = int.Parse(txtEmployeeId.Text.ToString()),
                    FirstName = txtFirstName.Text.ToString(),
                    LastName = txtLastName.Text.ToString(),
                    Email = txtEmail.Text.ToString(),
                    Phone = txtPhone.Text.ToString(),
                    HireDate = DateOnly.FromDateTime(dpHireDate.SelectedDate.Value),
                    JobId = cboJob.SelectedValue.ToString(),
                    Salary = double.Parse(txtSalary.Text.ToString()),
                    CommissionPct = double.Parse(txtCommission.Text.ToString()),
                    ManagerId = cboManager.SelectedValue != null ? int.Parse(cboManager.SelectedValue.ToString()) : (int?)null,
                    DepartmentId = int.Parse(cboDepartment.SelectedValue.ToString())
                };
                iEmployeeService.InsertEmployee(employee);
                MessageBox.Show("Create Successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Can not create new Employee");
            }
            finally
            {
                LoadEmployees();
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentUserRole != 1 && CurrentUserRole != 2)
            {
                MessageBox.Show("You do not have permission to update a employee.", "Permission Denied");
                return;
            }
            try
            {
                if (txtEmployeeId.Text.Length > 0)
                {
                    int id = int.Parse(txtEmployeeId.Text.ToString());
                    var employee = iEmployeeService.GetEmployeeById(id);
                    if (employee != null)
                    {
                        employee.FirstName = txtFirstName.Text;
                        employee.LastName = txtLastName.Text;
                        employee.Email = txtEmail.Text;
                        employee.Phone = txtPhone.Text;
                        employee.HireDate = DateOnly.FromDateTime(dpHireDate.SelectedDate.Value);
                        employee.JobId = cboJob.SelectedValue.ToString();   
                        employee.Salary = double.Parse(txtSalary.Text.ToString());
                        employee.CommissionPct = double.Parse(txtCommission.Text.ToString());
                        employee.ManagerId = cboManager.SelectedValue != null ? int.Parse(cboManager.SelectedValue.ToString()) : (int?)null;
                        employee.DepartmentId = int.Parse(cboDepartment.SelectedValue.ToString());
                        iEmployeeService.UpdateEmployee(employee);
                        MessageBox.Show("Update Successfully");
                    }
                    else
                    {
                        MessageBox.Show("Can not found employee");
                    }
                }
                else
                {
                    MessageBox.Show("Please select a Job");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Can not update Employee");
            }
            finally
            {
                LoadEmployees();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentUserRole != 1 && CurrentUserRole != 2)
            {
                MessageBox.Show("You do not have permission to update a employee.", "Permission Denied");
                return;
            }
            try
            {
                if (txtEmployeeId.Text.Length > 0)
                {
                    int id = int.Parse(txtEmployeeId.Text.ToString());
                    var employee = iEmployeeService.GetEmployeeById(id);
                    iEmployeeService.DeleteEmployee(employee);
                    MessageBox.Show("Delete Successfully");
                }
                else
                {
                    MessageBox.Show("Please select a Job");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Can not delete Employee");
            }
            finally
            {
                LoadEmployees();
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
