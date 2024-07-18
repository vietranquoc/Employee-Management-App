using BusinessObjects;
using Services;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : Window, INotifyPropertyChanged
    {
        private readonly IEmployeeService iEmployeeService;
        private readonly IJobService iJobService;
        private readonly IDepartmentService iDepartmentService;
        private PagingCollectionView _cView;

        public int? CurrentUserRole { get; set; } // Store current user's role

        // Properties for current page and total pages
        public int CurrentPageNumber => _cView.CurrentPage; // Displayed page numbers should start from 1
        public int TotalPages => _cView.PageCount;
        public EmployeeWindow()
        {
            InitializeComponent();
            iEmployeeService = new EmployeeService();
            iJobService = new JobService();
            iDepartmentService = new DepartmentService();
            //this.DataContext = this._cView;
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
                _cView = new PagingCollectionView(employees, 20);
                dgData.ItemsSource = _cView;

                this.DataContext = this; // Set DataContext after initializing _cView
                OnPropertyChanged(nameof(CurrentPageNumber));
                OnPropertyChanged(nameof(TotalPages));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Can not load Employees");
            }
            finally
            {
                ResetInput();
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
                searchManager.Add(new Employee { EmployeeId = 0, ManagerId = 0 });
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
            /*
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
            }*/
            DataGrid dataGrid = sender as DataGrid;
            if (dataGrid != null && dataGrid.SelectedIndex >= 0)
            {
                var selectedEmployee = dataGrid.SelectedItem as Employee;
                if (selectedEmployee != null)
                {
                    txtEmployeeId.Text = selectedEmployee.EmployeeId.ToString();
                    txtFirstName.Text = selectedEmployee.FirstName;
                    txtLastName.Text = selectedEmployee.LastName;
                    txtEmail.Text = selectedEmployee.Email;
                    txtPhone.Text = selectedEmployee.Phone;
                    dpHireDate.Text = selectedEmployee.HireDate.ToString();
                    cboJob.SelectedValue = selectedEmployee.JobId;
                    txtSalary.Text = selectedEmployee.Salary.ToString();
                    txtCommission.Text = selectedEmployee.CommissionPct.ToString();
                    cboManager.SelectedValue = selectedEmployee.ManagerId;
                    cboDepartment.SelectedValue = selectedEmployee.DepartmentId;
                }
            }
        }

        private void ResetInput()
        {
            txtEmployeeId.Text = "";
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
            dpHireDate.SelectedDate = null;
            cboJob.SelectedValue = null;
            txtSalary.Text = "";
            txtCommission.Text = "";
            cboManager.SelectedValue = null;
            cboDepartment.SelectedValue= null;
        }


        /*
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
                double minCommission = 0, maxCommission = double.MaxValue;
                if (txtFilterCommissionMin.Text != "") double.TryParse(txtFilterCommissionMin.Text, out minCommission);
                if (txtFilterCommissionMax.Text != "") double.TryParse(txtFilterCommissionMax.Text, out maxCommission);

                dgData.ItemsSource = null;
                if (maxCommission != double.MaxValue && minCommission > maxCommission)
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
                double minSalary = 0, maxSalary = double.MaxValue;
                if (txtFilterSalaryMin.Text != "") double.TryParse(txtFilterSalaryMin.Text, out minSalary);
                if (txtFilterSalaryMax.Text != "") double.TryParse(txtFilterSalaryMax.Text, out maxSalary);

                dgData.ItemsSource = null;
                if (maxSalary != double.MaxValue && minSalary > maxSalary)
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

        private void txtFilterHireDate_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (txtFilterHireDate.Text == "")
                {
                    dgData.ItemsSource = iEmployeeService.GetEmployees();
                    return;
                }
                if (!int.TryParse(txtFilterHireDate.Text, out int year))
                {
                    MessageBox.Show("Please enter a valid year.");
                    return;
                }
                dgData.ItemsSource = null;
                var employeeFilter = iEmployeeService.GetEmployeesByYearOfHireDate(year);
                dgData.ItemsSource = employeeFilter;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Can not load employees by year of hire date");
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

        */

        private void FilterEmployees()
        {
            try
            {
                string? name = txtSeachText.Text.Trim();
                string? jobId = cboSeachJob.SelectedValue != null ? cboSeachJob.SelectedValue.ToString() : null;
                int? managerId = cboSeachManager.SelectedValue != null ? int.Parse(cboSeachManager.SelectedValue.ToString()) : null;
                int? departmentId = cboSeachDepartment.SelectedValue != null ? int.Parse(cboSeachDepartment.SelectedValue.ToString()) : null;

                double? minSalary = 0, maxSalary = double.MaxValue;
                if (!string.IsNullOrEmpty(txtFilterSalaryMin.Text)) 
                {
                    if (!double.TryParse(txtFilterSalaryMin.Text, out double minSal))
                    {
                        MessageBox.Show("Please enter a valid min salary.");
                        return;
                    }
                    minSalary = minSal;
                }
                if (!string.IsNullOrEmpty(txtFilterSalaryMax.Text))
                {
                    if (!double.TryParse(txtFilterSalaryMax.Text, out double maxSal))
                    {
                        MessageBox.Show("Please enter a valid max salary.");
                        return;
                    }
                    maxSalary = maxSal;
                }

                double? minCommission = 0, maxCommission = double.MaxValue;
                if (!string.IsNullOrEmpty(txtFilterCommissionMin.Text))
                {
                    if (!double.TryParse(txtFilterCommissionMin.Text, out double minCom))
                    {
                        MessageBox.Show("Please enter a valid min Commsission.");
                        return;
                    }
                    minCommission = minCom;
                }
                if (!string.IsNullOrEmpty(txtFilterCommissionMax.Text))
                {
                    if (!double.TryParse(txtFilterCommissionMax.Text, out double maxCom))
                    {
                        MessageBox.Show("Please enter a valid max Commsission.");
                        return;
                    }
                    maxCommission = maxCom;
                }
                
                int? yearOfHireDate = null;
                if (!string.IsNullOrEmpty(txtFilterHireDate.Text))
                {
                    if (!int.TryParse(txtFilterHireDate.Text, out int year))
                    {
                        MessageBox.Show("Please enter a valid year.");
                        return;
                    }
                    yearOfHireDate = year;
                }
                
                dgData.ItemsSource = null;
                var employeeFilter = iEmployeeService.FilterEmployees(
                    name, minSalary, maxSalary, minCommission, maxCommission, jobId, managerId, departmentId, yearOfHireDate);
                dgData.ItemsSource = employeeFilter;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Can not filter employees");
            }
        }

        private void txtSeachText_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterEmployees();
        }

        private void cboSeachJob_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterEmployees();
        }

        private void cboSeachManager_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterEmployees();
        }

        private void cboSeachDepartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            FilterEmployees();
        }

        private void txtFilterCommission_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterEmployees();
        }

        private void txtFilterSalary_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterEmployees();
        }

        private void txtFilterHireDate_TextChanged(object sender, TextChangedEventArgs e)
        {
            FilterEmployees();
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
                int id = int.Parse(txtEmployeeId.Text.ToString());
                string phone = txtPhone.Text.ToString();
                string email = txtEmail.Text.ToString();
                var checkIdExist = iEmployeeService.checkIdExist(id);
                var checkPhoneExist = iEmployeeService.checkPhoneExist(phone);
                var checkEmailExist = iEmployeeService.checkEmailExist(email);
                if (checkIdExist)
                {
                    MessageBox.Show($"ID: {id} is duplicated! \nPlease enter another ID");
                    return;
                }
                if (checkPhoneExist)
                {
                    MessageBox.Show($"Phone number: {phone} is duplicated! \nPlease enter another Phone number");
                    return;
                }
                if (checkEmailExist)
                {
                    MessageBox.Show($"Email: {email} is duplicated! \nPlease enter another Email");
                    return;
                }
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
                if (txtEmployeeId.Text.Trim().Length <= 0 ||
                    txtFirstName.Text.Trim().Length <= 0 ||
                    txtLastName.Text.Trim().Length <= 0 ||
                    txtEmail.Text.Trim().Length <= 0 ||
                    txtPhone.Text.Trim().Length <= 0 ||
                    txtSalary.Text.Trim().Length <= 0 ||
                    txtCommission.Text.Trim().Length <= 0)
                {
                    MessageBox.Show("Please enter char not white space");
                    return;
                }

                string phoneFormatted = FormatPhoneNumber(phone);
                Employee employee = new Employee()
                {
                    EmployeeId = id,
                    FirstName = txtFirstName.Text.ToString(),
                    LastName = txtLastName.Text.ToString(),
                    Email = email,
                    Phone = phoneFormatted,
                    HireDate = DateOnly.FromDateTime(dpHireDate.SelectedDate.Value),
                    JobId = cboJob.SelectedValue.ToString(),
                    Salary = double.Parse(txtSalary.Text.ToString()),
                    CommissionPct = double.Parse(txtCommission.Text.ToString()),
                    ManagerId = cboManager.SelectedValue != null ? int.Parse(cboManager.SelectedValue.ToString()) : (int?)null,
                    DepartmentId = int.Parse(cboDepartment.SelectedValue.ToString())
                };
                iEmployeeService.InsertEmployee(employee);
                MessageBox.Show("Create Successfully");
                LoadEmployees();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Can not create new Employee");
            }
        }

        private string FormatPhoneNumber(string phoneNumber)
        {
            phoneNumber = phoneNumber.Replace(".", "");

            if (phoneNumber.Length != 10 || !phoneNumber.All(char.IsDigit))
            {
                throw new ArgumentException("Invalid phone number. It should contain exactly 10 digits.");
            }
            return $"{phoneNumber.Substring(0, 3)}.{phoneNumber.Substring(3, 3)}.{phoneNumber.Substring(6, 4)}";
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

                    string phone = txtPhone.Text.ToString();
                    var checkPhoneExist = iEmployeeService.checkPhoneExist(phone);
                    string email = txtEmail.Text.ToString();
                    var checkEmailExist = iEmployeeService.checkEmailExist(email);

                    string phoneFormatted = FormatPhoneNumber(phone);
                    if (checkPhoneExist 
                        && FormatPhoneNumber(employee.Phone.ToString()) != phoneFormatted)
                    {
                        MessageBox.Show($"Phone number: {phone} is duplicated! \nPlease enter another Phone number");
                        return;
                    }
                    if (checkEmailExist && employee.Email != email)
                    {
                        MessageBox.Show($"Email: {email} is duplicated! \nPlease enter another Email");
                        return;
                    }
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
                    if (txtEmployeeId.Text.Trim().Length <= 0 ||
                        txtFirstName.Text.Trim().Length <= 0 ||
                        txtLastName.Text.Trim().Length <= 0 ||
                        txtEmail.Text.Trim().Length <= 0 ||
                        txtPhone.Text.Trim().Length <= 0 ||
                        txtSalary.Text.Trim().Length <= 0 ||
                        txtCommission.Text.Trim().Length <= 0)
                    {
                        MessageBox.Show("Please enter char not white space");
                        return;
                    }

                    if (employee != null)
                    {
                        employee.FirstName = txtFirstName.Text;
                        employee.LastName = txtLastName.Text;
                        employee.Email = email;
                        employee.Phone = phoneFormatted;
                        employee.HireDate = DateOnly.FromDateTime(dpHireDate.SelectedDate.Value);
                        employee.JobId = cboJob.SelectedValue.ToString();   
                        employee.Salary = double.Parse(txtSalary.Text.ToString());
                        employee.CommissionPct = double.Parse(txtCommission.Text.ToString());
                        employee.ManagerId = cboManager.SelectedValue != null ? int.Parse(cboManager.SelectedValue.ToString()) : (int?)null;
                        employee.DepartmentId = int.Parse(cboDepartment.SelectedValue.ToString());
                        iEmployeeService.UpdateEmployee(employee);
                        MessageBox.Show("Update Successfully");
                        LoadEmployees();
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
                    
                    if (employee != null)
                    {
                        //iEmployeeService.DeleteEmployee(employee);
                        employee.Status = 0;
                        MessageBox.Show("Remove Successfully");
                        LoadEmployees();
                    }
                    else
                    {
                        MessageBox.Show("Can not found employee");
                    }
                }
                else
                {
                    MessageBox.Show("Please select a Employee");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Can not delete Employee");
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OnPreviousClicked(object sender, RoutedEventArgs e)
        {
            _cView.MoveToPreviousPage();
            OnPropertyChanged(nameof(CurrentPageNumber));
        }

        private void OnNextClicked(object sender, RoutedEventArgs e)
        {
            _cView.MoveToNextPage();
            OnPropertyChanged(nameof(CurrentPageNumber));
        }
        private void OnFirstPageClicked(object sender, RoutedEventArgs e)
        {
            _cView.MoveToFirstPage();
            OnPropertyChanged(nameof(CurrentPageNumber));
        }

        private void OnLastPageClicked(object sender, RoutedEventArgs e)
        {
            _cView.MoveToLastPage();
            OnPropertyChanged(nameof(CurrentPageNumber));
        }

        // Implement INotifyPropertyChanged interface for updating UI bindings
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }

    public class PagingCollectionView : CollectionView
    {
        private readonly IList _innerList;
        private readonly int _itemsPerPage;
        private int _currentPage = 1;

        public PagingCollectionView(IList innerList, int itemsPerPage) : base(innerList)
        {
            this._innerList = innerList;
            this._itemsPerPage = itemsPerPage;
        }

        public override int Count
        {
            get
            {
                if (this._innerList.Count == 0) return 0;
                if (this._currentPage < this.PageCount) // page 1..n-1
                {
                    return this._itemsPerPage;
                }
                else // page n
                {
                    var itemsLeft = this._innerList.Count % this._itemsPerPage;
                    if (0 == itemsLeft)
                    {
                        return this._itemsPerPage; // exactly itemsPerPage left
                    }
                    else
                    {
                        // return the remaining items
                        return itemsLeft;
                    }
                }
            }
        }

        public int CurrentPage
        {
            get { return this._currentPage; }
            set
            {
                if (value < 1 || value > this.PageCount)
                {
                    throw new ArgumentOutOfRangeException("value");
                }

                this._currentPage = value;
                this.OnPropertyChanged(new PropertyChangedEventArgs(nameof(CurrentPage)));
                this.Refresh();
            }
        }

        public int ItemsPerPage { get { return this._itemsPerPage; } }

        public int PageCount
        {
            get
            {
                return (int)Math.Ceiling((double)this._innerList.Count / this._itemsPerPage);
            }
        }

        private int EndIndex
        {
            get
            {
                var end = this._currentPage * this._itemsPerPage;
                return (end > this._innerList.Count) ? this._innerList.Count : end;
            }
        }

        private int StartIndex
        {
            get { return (this._currentPage - 1) * this._itemsPerPage; }
        }

        public override object GetItemAt(int index)
        {
            var offset = index % this._itemsPerPage;
            return this._innerList[this.StartIndex + offset];
        }

        public void MoveToNextPage()
        {
            if (this._currentPage < this.PageCount)
            {
                this.CurrentPage += 1;
            }
        }

        public void MoveToPreviousPage()
        {
            if (this._currentPage > 1)
            {
                this.CurrentPage -= 1;
            }
        }

        public void MoveToFirstPage()
        {
            this.CurrentPage = 1;
        }

        public void MoveToLastPage()
        {
            this.CurrentPage = this.PageCount;
        }
    }

}
