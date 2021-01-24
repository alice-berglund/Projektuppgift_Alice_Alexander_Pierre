using GUI.MechanicLoginPage.MyErrands_Mechanic;
using GUI.MechanicLoginPage.MyExpertise_Mechanic;
using Logic.Entities;
using System.Windows;
using System.Windows.Controls;

namespace GUI.Home
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class MechanicHome : Page
    {
        Mechanic Mechanic;
        public MechanicHome(Mechanic mechanic)
        {
            InitializeComponent();
            Mechanic = mechanic;
        }

        private void ExpertiseButton_Click(object sender, RoutedEventArgs e)
        {
            MyExpertisePage expertisePage = new MyExpertisePage(Mechanic);
            this.NavigationService.Navigate(expertisePage);
        }

        private void ErrandButton_Click(object sender, RoutedEventArgs e)
        {
            MyErrandsPage errandsPage = new MyErrandsPage(Mechanic);
            this.NavigationService.Navigate(errandsPage);
        }

        private void LogoutButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.NavigationService.Navigate(mainWindow);
        }
    }
}
