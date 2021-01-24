using GUI.AdminLogin.MechanicManager_Admin;
using GUI.AdminLogin.Stock_Admin;
using GUI.AdminLoginPage.AddNewUser_Admin;
using GUI.AdminLoginPage.ErrandsPage_Admin;
using System.Windows;
using System.Windows.Controls;

namespace GUI.HomePage
{
    /// <summary>
    /// Interaction logic for AdminHome.xaml
    /// </summary>
    public partial class AdminHome : Page
    {
        public AdminHome()
        {
            InitializeComponent();
        }

        private void NewUserButton_Click(object sender, RoutedEventArgs e)
        {

            AddNewUserPage newUser = new AddNewUserPage();

            this.NavigationService.Navigate(newUser);
        }

        private void MechanicsButton_Click(object sender, RoutedEventArgs e)
        {

            ManageMechanicsPage manageMechanics = new ManageMechanicsPage();

            this.NavigationService.Navigate(manageMechanics);
        }

        private void ErrandsButton_Click(object sender, RoutedEventArgs e)
        {

            ManageErrandsPage manageErrands = new ManageErrandsPage();

            this.NavigationService.Navigate(manageErrands);
        }

        private void StockButton_Click(object sender, RoutedEventArgs e)
        {
            ComponentsPage componentsPage = new ComponentsPage();
            this.NavigationService.Navigate(componentsPage);
        }
    }
}
