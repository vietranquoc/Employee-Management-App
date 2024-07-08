using BusinessObjects;
using Microsoft.IdentityModel.Tokens;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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
    /// Interaction logic for LocationWindow.xaml
    /// </summary>
    public partial class LocationWindow : Window
    {
        private readonly ILocationService iLocationService;
        private readonly ICountryService iCountryService;

        public int? CurrentUserRole { get; set; } // Store current user's role
        public LocationWindow()
        {
            InitializeComponent();
            iLocationService = new LocationService();
            iCountryService = new CountryService();
        }

        private void ApplyAuthorization()
        {
            if (CurrentUserRole != 1)
            {
                btnCreate.IsEnabled = false;
                btnUpdate.IsEnabled = false;
                btnDelete.IsEnabled = false;
            }
        }

        private void LoadLocation()
        {
            try
            {
                dgData.ItemsSource = null;
                var locations = iLocationService.GetLocations();
                dgData.ItemsSource = locations;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Can not load location");
            }
        }

        private void LoadCountry()
        {
            try
            {
                var countries = iCountryService.GetCountries();
                cboCountry.ItemsSource = countries;
                cboCountry.DisplayMemberPath = "CountryName";
                cboCountry.SelectedValuePath = "CountryId";

                var countriesFilter = iCountryService.GetCountries();
                countriesFilter.Add(new BusinessObjects.Country() { CountryId = "ALL", CountryName = "ALL" });
                cboSearchCountry.ItemsSource = countriesFilter;
                cboSearchCountry.DisplayMemberPath = "CountryName";
                cboSearchCountry.SelectedValuePath = "CountryId";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Can not load country");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ApplyAuthorization();
            LoadLocation();
            LoadCountry();
        }

        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            if (dataGrid.ItemsSource != null)
            {
                DataGridRow row = dataGrid.ItemContainerGenerator
                    .ContainerFromIndex(dataGrid.SelectedIndex) as DataGridRow;
                DataGridCell cell = dataGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;

                string locationId = ((TextBlock)cell.Content).Text;
                if (!locationId.Equals(""))
                {
                    Location location = iLocationService.GetLocationById(locationId);
                    txtLocationId.Text = location.LocationId.ToString();
                    txtStressAddress.Text = location.StreetAddress.ToString();
                    txtPostalCode.Text = location.PostalCode != null ? location.PostalCode.ToString() : "null";
                    txtCity.Text = location.City.ToString();
                    txtStateProvince.Text = location.StateProvince != null ? location.StateProvince.ToString() : "null";
                    cboCountry.SelectedValue = location.CountryId;
                }
            }
        }

        private void txtSeachCity_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string search = txtSeachCity.Text;
                dgData.ItemsSource = null;
                var filterLocation = iLocationService.GetLocationByCity(search);
                dgData.ItemsSource = filterLocation;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Can not load location by city");
            }
        }

        private void txtSeachStateProvince_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string search = txtSeachStateProvince.Text;
                dgData.ItemsSource = null;
                var filterLocation = iLocationService.GetLocationByStateProvince(search);
                dgData.ItemsSource = filterLocation;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Can not load location by state province");
            }
        }

        private void cboSeachCountry_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                string idCountry = cboSearchCountry.SelectedValue.ToString();
                dgData.ItemsSource = null;
                if (idCountry == "ALL")
                {
                    var location = iLocationService.GetLocations();
                    dgData.ItemsSource = location;
                }
                else
                {
                    var filterLocation = iLocationService.GetLocaionsByCountryId(idCountry);
                    dgData.ItemsSource = filterLocation;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Can not load location by country Id");
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentUserRole != 1)
            {
                MessageBox.Show("Only admin has role to create location.", "Permission Denied");
                return;
            }
            try
            {
                if (txtLocationId.Text.Trim().Length <= 0 ||
                    txtStressAddress.Text.Trim().Length <= 0 ||
                    txtPostalCode.Text.Trim().Length <= 0 ||
                    txtCity.Text.Trim().Length <= 0 ||
                    txtStateProvince.Text.Trim().Length <= 0)
                {
                    MessageBox.Show("Please enter char not white space");
                    return;
                }
                Location loaction = new Location()
                {
                    LocationId = txtLocationId.Text.ToString(),
                    StreetAddress = txtStressAddress.Text.ToString(),
                    PostalCode = txtPostalCode.Text.ToString(),
                    City = txtCity.Text.ToString(),
                    StateProvince = txtStateProvince.Text.ToString(),
                    CountryId = cboCountry.SelectedValue.ToString(),
                };
                iLocationService.InsertLocation(loaction);
                MessageBox.Show("Create successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Can not create new location");
            }
            finally
            {
                LoadLocation();
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentUserRole != 1)
            {
                MessageBox.Show("Only admin has role to update location.", "Permission Denied");
                return;
            }
            try
            {
                if (txtLocationId.Text.Length >0)
                {
                    string loactionId = txtLocationId.Text.ToString();
                    var location = iLocationService.GetLocationById(loactionId);
                    if (location != null)
                    {
                        location.LocationId = txtLocationId.Text.ToString();
                        location.StreetAddress = txtStressAddress.Text.ToString();
                        location.PostalCode = txtPostalCode.Text.ToString();
                        location.City = txtCity.Text.ToString();
                        location.StateProvince = txtStateProvince.Text.ToString();
                        location.CountryId = cboCountry.SelectedValue.ToString();

                        iLocationService.UpdateLocation(location);
                        MessageBox.Show("Update successfully");
                    }
                    else
                    {
                        MessageBox.Show("Can not found location");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Please select a location");
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Can not update location");
            }
            finally
            {
                LoadLocation();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentUserRole != 1)
            {
                MessageBox.Show("Only admin has role to update location.", "Permission Denied");
                return;
            }
            try
            {
                if (txtLocationId.Text.Length > 0)
                {
                    string loactionId = txtLocationId.Text.ToString();
                    var location = iLocationService.GetLocationById(loactionId);
                    
                    iLocationService.DeleteLocation(location);
                    MessageBox.Show("Delete Successfully");
                }
                else
                {
                    MessageBox.Show("Please select a location");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Can not Delete location");
            }
            finally
            {
                LoadLocation();
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
