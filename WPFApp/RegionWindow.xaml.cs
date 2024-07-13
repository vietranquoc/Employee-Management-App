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
    /// Interaction logic for RegionWindow.xaml
    /// </summary>
    public partial class RegionWindow : Window
    {
        private readonly IRegionService iRegionService;

        public int? CurrentUserRole { get; set; } // Store current user's role
        public RegionWindow()
        {
            InitializeComponent();
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

        private void LoadRegion()
        {
            try
            {
                dgData.ItemsSource = null;
                var regions = iRegionService.GetRegions();
                dgData.ItemsSource = regions;
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Can not load regions");
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ApplyAuthorization();
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

                string regionId = ((TextBlock)cell.Content).Text;
                if (!regionId.Equals(""))
                {
                    Region region = iRegionService.GetRegionById(int.Parse(regionId));
                    txtRegionId.Text = region.RegionId.ToString();
                    txtRegionName.Text = region.RegionName.ToString();
                }
            }
        }

        private void txtSeachText_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                string name = txtSeachText.Text.ToString();
                dgData.ItemsSource = null;
                var regionFilter = iRegionService.GetRegionsByName(name);
                dgData.ItemsSource = regionFilter;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Can not load regions by name");
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentUserRole != 1)
            {
                MessageBox.Show("Only admin has role to create region.", "Permission Denied");
                return;
            }
            try
            {
                if (txtRegionName.Text.Trim().Length <= 0 ||
                    txtRegionId.Text.Trim().Length <= 0)
                {
                    MessageBox.Show("Please enter char not white space");
                    return;
                }
                var regionId = int.Parse(txtRegionId.Text.ToString());
                var checkIdExist = iRegionService.CheckIdExist(regionId);
                if (checkIdExist)
                {
                    MessageBox.Show($"ID: {regionId} is duplicated! \nPlease enter another ID");
                    return;
                }
                Region region = new Region()
                {
                    RegionId = regionId,
                    RegionName = txtRegionName.Text.ToString()
                };
                iRegionService.InsertRegion(region);
                MessageBox.Show("Create Successfully");
                LoadRegion();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Can not create new region");
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentUserRole != 1)
            {
                MessageBox.Show("Only admin has role to update region.", "Permission Denied");
                return;
            }
            try
            {
                if (txtRegionId.Text.Length > 0)
                {
                    int regionId = int.Parse(txtRegionId.Text.ToString());
                    var region = iRegionService.GetRegionById(regionId);

                    if (txtRegionName.Text.Trim().Length <= 0 ||
                    txtRegionId.Text.Trim().Length <= 0)
                    {
                        MessageBox.Show("Please enter char not white space");
                        return;
                    }

                    if (region != null)
                    {
                        //region.RegionId = int.Parse(txtRegionId.Text.ToString());
                        region.RegionName = txtRegionName.Text.ToString();

                        iRegionService.UpdateRegion(region);
                        MessageBox.Show("Update Successfully");
                        LoadRegion();
                    }
                    else
                    {
                        MessageBox.Show("Can not found region");
                    }
                } 
                else
                {
                    MessageBox.Show("Please select a region");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Can not update region");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentUserRole != 1)
            {
                MessageBox.Show("Only admin has role to delete region.", "Permission Denied");
                return;
            }
            try
            {
                if (txtRegionId.Text.Length > 0)
                {
                    int regionId = int.Parse(txtRegionId.Text.ToString());
                    var region = iRegionService.GetRegionById(regionId);

                    if (region != null)
                    {
                        iRegionService.DeleteRegion(region);
                        MessageBox.Show("Delete Successfully");
                        LoadRegion();
                    }
                    else
                    {
                        MessageBox.Show("Region not found");
                    }
                }
                else
                {
                    MessageBox.Show("Please select a region");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Can not delete region");
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
