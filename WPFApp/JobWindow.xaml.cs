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
    /// Interaction logic for JobWindow.xaml
    /// </summary>
    public partial class JobWindow : Window
    {
        private readonly IJobService iJobService;

        public int? CurrentUserRole { get; set; } // Store current user's role 
        public JobWindow()
        {
            InitializeComponent();
            iJobService = new JobService();
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

        private void LoadJob()
        {
            try
            {
                dgData.ItemsSource = null;
                var jobs = iJobService.GetJobs();
                dgData.ItemsSource = jobs;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Can not load Jobs");
            }
            finally
            {
                ResetInput();
            }
        }

        private void ResetInput()
        {
            txtJobId.Text = "";
            txtJobTitle.Text = "";
            txtMinSalary.Text = "";
            txtMaxSalary.Text = "";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ApplyAuthorization();
            LoadJob();
        }

        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = sender as DataGrid;
            if (dataGrid.ItemsSource != null)
            {
                DataGridRow row = dataGrid.ItemContainerGenerator
                    .ContainerFromIndex(dataGrid.SelectedIndex) as DataGridRow;
                DataGridCell cell = dataGrid.Columns[0].GetCellContent(row).Parent as DataGridCell;

                string jobId = ((TextBlock)cell.Content).Text;
                if (!jobId.Equals(""))
                {
                    Job job = iJobService.GetJobById(jobId);
                    txtJobId.Text = job.JobId.ToString();
                    txtJobTitle.Text = job.JobTitle.ToString();
                    txtMinSalary.Text = job.MinSalary.ToString();
                    txtMaxSalary.Text = job.MaxSalary.ToString();
                }
            }
        }

        
        private void txtSeachText_TextChanged(object sender, TextChangedEventArgs e)
        {
            /*
            try
            {
                string title = txtSeachText.Text;
                dgData.ItemsSource = null;
                var jobFilter = iJobService.GetJobByTitle(title);
                dgData.ItemsSource = jobFilter;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Can not load job by title");
            }
            */
            FilterJobs();
        }

        private void txtFilterSalary_TextChanged(object sender, TextChangedEventArgs e)
        {
            /*
            try
            {
                int minSalary = 0, maxSalary = int .MaxValue;
                if (txtFilterMin.Text != "") int.TryParse(txtFilterMin.Text, out minSalary);
                if (txtFilterMax.Text != "") int.TryParse(txtFilterMax.Text, out maxSalary);

                dgData.ItemsSource = null;
                if (maxSalary != int.MaxValue && minSalary > maxSalary)
                {
                    MessageBox.Show("Min salary must be less than Max salary!");
                }
                else
                {
                    var jobFilter = iJobService.GetJobBySalary(minSalary, maxSalary);
                    dgData.ItemsSource = jobFilter;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Can not load job by salary");
            }
            */
            FilterJobs();
        }

        private void FilterJobs()
        {
            try
            {
                string? title = txtSeachText.Text.Trim();
                int? minSalary = 0, maxSalary = int.MaxValue;
                if (!string.IsNullOrEmpty(txtFilterMin.Text))
                {
                    if (!int.TryParse(txtFilterMin.Text, out int minSal))
                    {
                        MessageBox.Show("Please enter a valid min salary.");
                        return;
                    }
                    minSalary = minSal;
                }
                if (!string.IsNullOrEmpty(txtFilterMax.Text))
                {
                    if (!int.TryParse(txtFilterMax.Text, out int maxSal))
                    {
                        MessageBox.Show("Please enter a valid max salary.");
                        return;
                    }
                    maxSalary = maxSal;
                }

                dgData.ItemsSource = null;
                var filterJobs = iJobService.FilterJob(title, minSalary, maxSalary);

                dgData.ItemsSource = filterJobs;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Can not filter jobs");
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentUserRole != 1 && CurrentUserRole != 2)
            {
                MessageBox.Show("You do not have permission to create a job.", "Permission Denied");
                return;
            }
            try
            {
                string id = txtJobId.Text;
                var checkIdExist = iJobService.checkIdExist(id);
                if (checkIdExist)
                {
                    MessageBox.Show($"ID: {id} is duplicated! \nPlease enter another ID");
                    return;
                }
                if (txtJobId.Text.Trim().Length <= 0 ||
                    txtJobTitle.Text.Trim().Length <= 0 ||
                    txtMinSalary.Text.Trim().Length <= 0 ||
                    txtMaxSalary.Text.Trim().Length <= 0)
                {
                    MessageBox.Show("Please enter char not white space");
                    return;
                }
                Job job = new Job()
                {
                    JobId = id,
                    JobTitle = txtJobTitle.Text,
                    MinSalary = int.Parse(txtMinSalary.Text),
                    MaxSalary = int.Parse(txtMaxSalary.Text)
                };
                iJobService.InsertJob(job);
                MessageBox.Show("Create Successfully");
                LoadJob();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Can not create new Job");
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentUserRole != 1 && CurrentUserRole != 2)
            {
                MessageBox.Show("You do not have permission to update a job.", "Permission Denied");
                return;
            }
            try
            {
                if (txtJobId.Text.Length > 0)
                {
                    string jobId = txtJobId.Text;
                    var job = iJobService.GetJobById(jobId);

                    if (txtJobId.Text.Trim().Length <= 0 ||
                    txtJobTitle.Text.Trim().Length <= 0 ||
                    txtMinSalary.Text.Trim().Length <= 0 ||
                    txtMaxSalary.Text.Trim().Length <= 0)
                    {
                        MessageBox.Show("Please enter char not white space");
                        return;
                    } 
                    if (job != null)
                    {
                        //job.JobId = txtJobId.Text;
                        job.JobTitle = txtJobTitle.Text;
                        job.MinSalary = int.Parse(txtMinSalary.Text);
                        job.MaxSalary = int.Parse(txtMaxSalary.Text);
                        iJobService.UpdateJob(job);
                        MessageBox.Show("Update Successfully");
                        LoadJob();
                    }
                    else
                    {
                        MessageBox.Show("Can not found job");
                    }
                }
                else
                {
                    MessageBox.Show("Please select a Job");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Can not update Job");
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentUserRole != 1 && CurrentUserRole != 2)
            {
                MessageBox.Show("You do not have permission to delete a job.", "Permission Denied");
                return;
            }
            try
            {
                if (txtJobId.Text.Length > 0)
                {
                    string jobId = txtJobId.Text;
                    var job = iJobService.GetJobById(jobId);
                    if (job != null)
                    {
                        iJobService.DeleteJob(job);
                        MessageBox.Show("Delete Successfully");
                        LoadJob();
                    }
                    else
                    {
                        MessageBox.Show("Job not found");
                    }
                }
                else
                {
                    MessageBox.Show("Please select a Job");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error: Can not delete Job");
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
