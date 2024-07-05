using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WPFApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int? CurrentUserRole { get; set; }

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnDepartment_Click(object sender, RoutedEventArgs e)
        {
            DepartmentWindow departmentWindow = new DepartmentWindow();
            departmentWindow.Owner = this; // Set the owner of the DepartmentWindow
            departmentWindow.CurrentUserRole = CurrentUserRole; // Pass role to DepartmentWindow
            //MessageBox.Show("Current role: "+  CurrentUserRole);
            //this.Hide();
            departmentWindow.ShowDialog();

        }

        private void btnEmployee_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnJob_Click(object sender, RoutedEventArgs e)
        {
            JobWindow jobWindow = new JobWindow();
            jobWindow.Owner = this; // Set the owner of the DepartmentWindow
            jobWindow.CurrentUserRole = CurrentUserRole; // Pass role to DepartmentWindow
            jobWindow.ShowDialog();
        }

        private void btnRegion_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnCountry_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnLocation_Click(object sender, RoutedEventArgs e)
        {

        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}