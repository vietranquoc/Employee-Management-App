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
using static System.Runtime.CompilerServices.RuntimeHelpers;
using System.Xml.Linq;
using BusinessObjects;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for DepartmentWindow.xaml
    /// </summary>
    public partial class DepartmentWindow : Window
    {
        private readonly IDepartmentService iDepartmentService;
        private readonly IEmployeeService iEmployeeService;
        private readonly ILocationService iLocationService;

        public int? CurrentUserRole { get; set; } // Store current user's role
        public DepartmentWindow()
        {
            InitializeComponent();
            iDepartmentService = new DepartmentService();
            iEmployeeService = new EmployeeService();
            iLocationService = new LocationService();
        }

        private void ApplyAuthorization()
        {
            //MessageBox.Show($"CurrentUserRole: {CurrentUserRole}", "Debug Info");
            if (CurrentUserRole != 1 && CurrentUserRole != 2)
            {
                btnCreate.IsEnabled = false;
                btnUpdate.IsEnabled = false;
                btnDelete.IsEnabled = false;
            }
            //MessageBox.Show($"btnCreate IsEnabled: {btnCreate.IsEnabled}\nbtnUpdate IsEnabled: {btnUpdate.IsEnabled}\nbtnDelete IsEnabled: {btnDelete.IsEnabled}", "Debug Info");
        }


        private void LoadDepartments()
        {
            try
            {
                dgData.ItemsSource = null;
                var departments = iDepartmentService.GetDepartments();
                dgData.ItemsSource = departments;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error: Can not load Departments!");
            }
            finally
            {
                ResetInput();
            }
        }

        private void LoadManager()
        {
            try
            {
                var managers = iEmployeeService.GetManagers();
                cboManagerId.ItemsSource = managers;
                cboManagerId.DisplayMemberPath = "EmployeeId";
                cboManagerId.SelectedValuePath = "EmployeeId";

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

        private void LoadLocation()
        {
            try
            {
                var locations = iLocationService.GetLocations();
                cboLocationId.ItemsSource = locations;
                cboLocationId.DisplayMemberPath = "LocationId";
                cboLocationId.SelectedValuePath = "LocationId";

                var searchLocation = iLocationService.GetLocations();
                searchLocation.Add(new Location { LocationId = "9999"});
                cboSeachLocation.ItemsSource = searchLocation;
                cboSeachLocation.DisplayMemberPath = "LocationId";
                cboSeachLocation.SelectedValuePath = "LocationId";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Can not load locations");
            }
        }

        private void ResetInput()
        {
            txtDepartmentId.Text = "";
            txtDepartmentName.Text = "";
            cboManagerId.SelectedValue = 0;
            cboLocationId.SelectedValue = 0;
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ApplyAuthorization();
            LoadDepartments();
            LoadManager();
            LoadLocation();
        }

        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            if (dataGrid.ItemsSource != null)
            {
                DataGridRow row = dataGrid.ItemContainerGenerator
                    .ContainerFromIndex(dataGrid.SelectedIndex) as DataGridRow;
                DataGridCell cell = dataGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;

                string departmentId = ((TextBlock)cell.Content).Text;
                if (!departmentId.Equals(""))
                {
                    Department department = iDepartmentService.GetDepartmentById(int.Parse(departmentId));
                    txtDepartmentId.Text = department.DepartmentId.ToString();
                    txtDepartmentName.Text = department.DepartmentName.ToString();
                    cboManagerId.SelectedValue = department.ManagerId;
                    cboLocationId.SelectedValue = department.LocationId;
                }
            }

        }


        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentUserRole != 1 && CurrentUserRole != 2)
            {
                MessageBox.Show("You do not have permission to create a department.", "Permission Denied");
                return;
            }
            try
            {
                if (cboLocationId.SelectedValue == null)
                {
                    MessageBox.Show("Please choose a location.", "Input Error");
                    return;
                }
                if (txtDepartmentId.Text.ToString().Trim().Length <= 0 ||
                    txtDepartmentName.Text.ToString().Trim().Length <= 0)
                {
                    MessageBox.Show("Please enter char not white space");
                    return;
                }
                Department department = new Department();
                department.DepartmentId = int.Parse(txtDepartmentId.Text);
                department.DepartmentName = txtDepartmentName.Text;
                department.ManagerId = cboManagerId.SelectedValue != null ? int.Parse(cboManagerId.SelectedValue.ToString()) : (int?)null;
                department.LocationId = cboLocationId.SelectedValue.ToString();

                iDepartmentService.InsertDepartment(department);
                MessageBox.Show("Create Successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Can not create new Department");
            }
            finally
            {
                LoadDepartments();
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentUserRole != 1 && CurrentUserRole != 2)
            {
                MessageBox.Show("You do not have permission to update a department.", "Permission Denied");
                return;
            }
            try
            {
                if (txtDepartmentId.Text.Length > 0)
                {
                    int departmentId = int.Parse(txtDepartmentId.Text);
                    var department = iDepartmentService.GetDepartmentById(departmentId);
                    department.DepartmentId = int.Parse(txtDepartmentId.Text);
                    department.DepartmentName = txtDepartmentName.Text;
                    department.ManagerId = cboManagerId.SelectedValue != null ? int.Parse(cboManagerId.SelectedValue.ToString()) : (int?)null;
                    department.LocationId = cboLocationId.SelectedValue != null ? cboLocationId.SelectedValue.ToString() : null;
                    iDepartmentService.UpdateDepartment(department);
                    MessageBox.Show("Update Successfully");
                }
                else
                {
                    MessageBox.Show("Please select a Department");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Can not update Department");
            }
            finally
            {
                LoadDepartments();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentUserRole != 1 && CurrentUserRole != 2)
            {
                MessageBox.Show("You do not have permission to delete a department.", "Permission Denied");
                return;
            }
            try
            {
                if (txtDepartmentId.Text.Length > 0)
                {
                    int departmentId = int.Parse(txtDepartmentId.Text);
                    var department = iDepartmentService.GetDepartmentById(departmentId);
                    if (department != null)
                    {
                        iDepartmentService.DeleteDepartment(department);
                        MessageBox.Show("Delete Successfully");
                    }
                    else
                    {
                        MessageBox.Show("Department not found");
                    }
                }
                else
                {
                    MessageBox.Show("Please select a Department");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Can not delete Department");
            }
            finally
            {
                LoadDepartments();
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtSeachText_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string search = txtSeachText.Text;
                dgData.ItemsSource = null;
                var filterDeparment = iDepartmentService.GetDepartmentByName(search);
                dgData.ItemsSource = filterDeparment;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Can not load department by name");
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
                    var department = iDepartmentService.GetDepartments();
                    dgData.ItemsSource = department;
                }
                else
                {
                    var filterDeparment = iDepartmentService.GetDepartmentsByManagerId(idManager);
                    dgData.ItemsSource = filterDeparment;
                }
            }
            catch ( Exception ex )
            {
                MessageBox.Show(ex.Message, "Error: Can not load department by manager Id");
            }
        }

        private void cboSeachLocation_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string idLocation = cboSeachLocation.SelectedValue.ToString();
                dgData.ItemsSource = null;
                if (idLocation.Equals("9999"))
                {
                    var department = iDepartmentService.GetDepartments();
                    dgData.ItemsSource = department;
                }
                else
                {
                    var filterDeparment = iDepartmentService.GetDepartmentsByLocationId(idLocation);
                    dgData.ItemsSource = filterDeparment;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Can not load department by manager Id");
            }
        }
    }
}
