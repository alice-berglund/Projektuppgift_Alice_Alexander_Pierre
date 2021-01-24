using GUI.Home;
using GUI.HomePage;
using Logic.Entities;
using Logic.Services;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace GUI.Login
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        private const string _errorMsg = "Inloggningen misslyckades";

        private LoginService _loginService;

        public LoginPage()
        {
            InitializeComponent();

            _loginService = new LoginService();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string username = this.tbUsernam.Text.ToLower();
            string password = this.pbPassword.Password;

            List<User> users = new List<User>();
            try
            {
                users = _loginService.Login(username, password);
                User user = users.Single(s => s.Username.ToLower() == username);
                if (user.IsAdmin == false)
                {
                    ManageMechanics manageMechanics = new ManageMechanics();
                    List<Mechanic> mechanics = manageMechanics.AllMechanics();
                    Mechanic mechanic = mechanics.Single(x => x.IdNumber == user.MechanicId);

                    MechanicHome mechanicHomePage = new MechanicHome(mechanic);

                    this.NavigationService.Navigate(mechanicHomePage);
                }
                else
                {
                    AdminHome adminHomePage = new AdminHome();

                    this.NavigationService.Navigate(adminHomePage);
                }
            }
            catch
            {
                MessageBox.Show(_errorMsg);
                this.tbUsernam.Clear();
                this.pbPassword.Clear();
            }
        }
    }
}
