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
    /// Interaction logic for CountryWindow.xaml
    /// </summary>
    public partial class CountryWindow : Window
    {
        private readonly ICountryService iCountryService;
        private readonly IRegionService iRegionService;

        public int? CurrentUserRole { get; set; } // Store current user's role
        public CountryWindow()
        {
            InitializeComponent();
            iCountryService = new CountryService();
            iRegionService = new RegionService();
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

        private void ResetInput()
        {
            txtCountryId.Text = "";
            txtCountryName.Text = "";
            cboRegionId.SelectedValue = false;
        }

        private void LoadCountry()
        {
            try
            {
                dgData.ItemsSource = null;
                var countries = iCountryService.GetCountries();
                dgData.ItemsSource = countries;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Can not load countries");
            }
            finally
            {
                ResetInput();
            }
        }

        private void LoadRegion()
        {
            try
            {
                var regions = iRegionService.GetRegions();
                cboRegionId.ItemsSource = regions;
                cboRegionId.SelectedValuePath = "RegionId";
                cboRegionId.DisplayMemberPath = "RegionName";

                var regionsFilter = iRegionService.GetRegions();
                regionsFilter.Add(new Region() { RegionId = 0, RegionName = "ALL" });
                cboSeachRegion.ItemsSource = regionsFilter;
                cboSeachRegion.SelectedValuePath = "RegionId";
                cboSeachRegion.DisplayMemberPath = "RegionName";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Can not load regions");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ApplyAuthorization();
            LoadCountry();
            LoadRegion();
        }

        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            if (dataGrid.ItemsSource != null)
            {
                DataGridRow row = dataGrid.ItemContainerGenerator
                    .ContainerFromIndex(dataGrid.SelectedIndex) as DataGridRow;
                DataGridCell cell = dataGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;

                string countryId = ((TextBlock)cell.Content).Text;
                if (!countryId.Equals(""))
                {
                    Country country = iCountryService.GetCountryById(countryId);
                    txtCountryId.Text = country.CountryId.ToString();
                    txtCountryName.Text = country.CountryName.ToString();
                    cboRegionId.SelectedValue = country.RegionId;
                }
            }
        }

        private void txtSeachText_TextChanged(object sender, TextChangedEventArgs e)
        {
            /*
            try
            {
                string search = txtSeachText.Text;
                dgData.ItemsSource = null;
                var filterCountries = iCountryService.GetCountriesByName(search);
                dgData.ItemsSource = filterCountries;
            } 
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Can not load country by name");
            }
            */
            FilterCountries();
        }

        private void cboSeachRegion_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            /*
            try
            {
                int idRegion = int.Parse(cboSeachRegion.SelectedValue.ToString());
                dgData.ItemsSource = null;
                if (idRegion == 0)
                {
                    var country = iCountryService.GetCountries();
                    dgData.ItemsSource = country;
                }
                else
                {
                    var filterCountry = iCountryService.GetCountriesByRegionId(idRegion);
                    dgData.ItemsSource = filterCountry;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Can not load country by region Id");
            }
            */
            FilterCountries();
        }

        private void FilterCountries()
        {
            string? search = txtSeachText.Text.Trim();
            int? regionId = cboSeachRegion.SelectedValue != null ? int.Parse(cboSeachRegion.SelectedValue.ToString()) : null;

            dgData.ItemsSource = null;
            var filterCountries = iCountryService.FilterCountries(search, regionId);
            dgData.ItemsSource = filterCountries;
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentUserRole != 1)
            {
                MessageBox.Show("Only admin has role to create country.", "Permission Denied");
                return;
            }
            try
            {
                
                string countryId = txtCountryId.Text.ToString();
                var checkIdExist = iCountryService.checkIdExist(countryId);
                if (checkIdExist)
                {
                    MessageBox.Show($"ID: {countryId} is duplicated! \nPlease enter another ID");
                    return;
                }
                if (txtCountryName.Text.Trim().Length <= 0 ||
                    txtCountryId.Text.Trim().Length <= 0)
                {
                    MessageBox.Show("Please enter char not white space");
                    return;
                }

                Country country = new Country()
                {
                    CountryId = countryId,
                    CountryName = txtCountryName.Text.ToString(),
                    RegionId = int.Parse(cboRegionId.SelectedValue.ToString()),
                };
                iCountryService.InsertCountry(country);
                MessageBox.Show("Create Successfully");
                LoadCountry();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Can not create new country");
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentUserRole != 1)
            {
                MessageBox.Show("Only admin has role to update country.", "Permission Denied");
                return;
            }
            try
            {
                if (txtCountryId.Text.Length > 0)
                {
                    string countryId = txtCountryId.Text.ToString();
                    var country = iCountryService.GetCountryById(countryId);

                    if (txtCountryName.Text.ToString().Trim().Length <= 0 ||
                    txtCountryId.Text.ToString().Trim().Length <= 0)
                    {
                        MessageBox.Show("Please enter char not white space");
                        return;
                    }

                    if (country != null)
                    {
                        //country.CountryId = txtCountryId.Text.ToString();
                        country.CountryName = txtCountryName.Text.ToString();
                        country.RegionId = int.Parse(cboRegionId.SelectedValue.ToString());

                        iCountryService.UpdateCountry(country);
                        MessageBox.Show("Update Successfully");
                        LoadCountry();
                    }
                    else
                    {
                        MessageBox.Show("Can not found country");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Please select a country");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Can not update country");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentUserRole != 1)
            {
                MessageBox.Show("Only admin has role to delete country.", "Permission Denied");
                return;
            }  
            try
            {
                if (txtCountryId.Text.Length > 0)
                {
                    string countryId = txtCountryId.Text.ToString();
                    var country = iCountryService.GetCountryById(countryId);

                    if (country != null)
                    {
                        iCountryService.DeleteCountry(country);
                        MessageBox.Show("Delete Successfully");
                        LoadCountry();
                    }
                    else
                    {
                        MessageBox.Show("Can not found country");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Please select a country");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Can not delete country");
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
