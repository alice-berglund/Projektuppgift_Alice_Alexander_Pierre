using GUI.HomePage;
using Logic.Entities;
using Logic.Services;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace GUI.AdminLoginPage.AddNewUser_Admin
{
    /// <summary>
    /// Interaction logic for AddNewUserPage.xaml
    /// </summary>
    public partial class AddNewUserPage : Page
    {
        ManageUsers manageUsers = new ManageUsers();
        ManageMechanics ManageMechanics = new ManageMechanics();

        List<User> Users { get; set; }
        List<Mechanic> Mechanics { get; set; }

        public AddNewUserPage()
        {
            InitializeComponent();
            Mechanics = new List<Mechanic>();
            UpdateUserList();
        }

        private void UpdateUserList()
        {
            try
            {
                mechanicDropDown.Items.Clear();
                Mechanics.Clear();
                Mechanics.AddRange(ManageMechanics.AllMechanics());

                foreach (Mechanic mechanic in Mechanics)
                {
                    mechanicDropDown.Items.Add(mechanic.Name + mechanic.IdNumber);
                }
            }
            catch
            {
                MessageBox.Show("Du måste lägga till mekaniker innan du kan lägga till användare!");
            }

            this.userListBox.Items.Clear();

            Users = new List<User>();
            Users.AddRange(manageUsers.AllUsers());

            foreach (User user in Users)
            {
                string userInfo = $"{user.Username} + {user.Password}";
                this.userListBox.Items.Add(userInfo);
            }           
        }

        private void addNewUser_Click(object sender, RoutedEventArgs e)
        {
            foreach(User user in Users)
            {
                if(user.Username == UserNameInput.Text)
                {
                    MessageBox.Show("Användarnamnet finns redan!");
                    UserNameInput.Clear();
                    return;
                }
            }

            try
            {
                manageUsers.AddUser(UserNameInput.Text, passWordInput.Password, mechanicDropDown.SelectedIndex, Users, Mechanics);
            }
            catch
            {
                MessageBox.Show("Felaktigt användarnamn eller lösenord." +
                    "Lösenordet ska innehålla mellan 5 & 10 tecken och användarnamnet ska bestå av en giltig mejladress");
            }

            UserNameInput.Clear();
            passWordInput.Clear();

            UpdateUserList();    
        }

        private async void deleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            if(this.userListBox.SelectedItem == null)
            {
                MessageBox.Show("Ingen användare är vald!");
                return;
            }
            else
            {
                User choosenUser = Users[userListBox.SelectedIndex];

                await manageUsers.DeleteUser(choosenUser, Users);
            }

            UpdateUserList();
        }

        private void goBackButton_Click(object sender, RoutedEventArgs e)
        {
            AdminHome adminHome = new AdminHome();
            this.NavigationService.Navigate(adminHome);
        }
    }
}
