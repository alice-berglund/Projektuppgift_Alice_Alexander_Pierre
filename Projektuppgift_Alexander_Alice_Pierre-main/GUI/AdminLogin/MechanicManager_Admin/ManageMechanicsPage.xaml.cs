
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using GUI.HomePage;
using Logic.Entities;
using Logic.Services;

namespace GUI.AdminLogin.MechanicManager_Admin
{
    /// <summary>
    /// Interaction logic for ManageMechanicsPage.xaml
    /// </summary>
    public partial class ManageMechanicsPage : Page
    {
        ManageMechanics ManageMechanics = new ManageMechanics();
        List<Mechanic> Mechanics { get; set; }
        public ManageMechanicsPage()
        {
            InitializeComponent();
            UpdateMechanicList();
        }

        private void UpdateMechanicList()
        {
            mechanicList.Items.Clear();
            InfoAboutSelectedMechanicBox.Clear();

            Mechanics = new List<Mechanic>();

            try
            {
                Mechanics.AddRange(ManageMechanics.AllMechanics());

                foreach (Mechanic mechanic in Mechanics)
                {
                    string mechanicInfo = $"{mechanic.Name}";
                    mechanicList.Items.Add(mechanicInfo);
                }
            }
            catch
            {

            }
        }

        private void AddMechanicButton_Click(object sender, RoutedEventArgs e)
        {
            AddMechanicPage addMechanicPage = new AddMechanicPage();

            NavigationService.Navigate(addMechanicPage);

            UpdateMechanicList();
        }

        private void mechanicList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (mechanicList.SelectedIndex == -1)
                return;

            StringBuilder selectedMechanicInfo = new StringBuilder();

            selectedMechanicInfo.Append($"Namn: {Mechanics[mechanicList.SelectedIndex].Name}" +
                $"\nFödelsedatum: {Mechanics[mechanicList.SelectedIndex].BirthDay.Date}" +
                $"\nAnställningsdatum: {Mechanics[mechanicList.SelectedIndex].EmploymentDate.Date}" +
                $"\nMechanicID: {Mechanics[mechanicList.SelectedIndex].IdNumber}");

            if (Mechanics[mechanicList.SelectedIndex].LastDay != null)
                selectedMechanicInfo.Append($"\nSista dag: {Mechanics[mechanicList.SelectedIndex].LastDay}");

            selectedMechanicInfo.Append($"\n Qualifications:");
            foreach (Component qualification in Mechanics[mechanicList.SelectedIndex].Qualifications)
            {
                selectedMechanicInfo.Append($"\n{qualification}");
            }

            InfoAboutSelectedMechanicBox.Text = selectedMechanicInfo.ToString();
        }

        private async void RemoveMechanicButton_Click(object sender, RoutedEventArgs e)
        {
            if (mechanicList.SelectedItem == null)
            {
                MessageBox.Show("Ingen användare är vald!");
                return;
            }
            else
            {
                Mechanic choosenMechanic = Mechanics[mechanicList.SelectedIndex];

                ManageUsers manageUsers = new ManageUsers();

                List<User> users = manageUsers.AllUsers();

                int connectedToUser = users
                    .FindIndex(user => user.MechanicId == choosenMechanic.IdNumber);

                if (connectedToUser > 0)
                {
                    var result = MessageBox.Show("Användaren som är kopplad till den här mekanikern kommer också att raderas. Vill du fortsätta?", "Varning!", MessageBoxButton.YesNo);

                    if(result == MessageBoxResult.Yes)
                    {
                        await manageUsers.DeleteUser(users[connectedToUser], users);
                        await ManageMechanics.DeleteMechanic(choosenMechanic, Mechanics);
                    }
                }
                else
                {
                    await ManageMechanics.DeleteMechanic(choosenMechanic, Mechanics);
                }
            }

            UpdateMechanicList();
        }

        private void ChangeMechanicButton_Click(object sender, RoutedEventArgs e)
        {
            if(mechanicList.SelectedItem == null)
            {
                MessageBox.Show("Ingen användare är vald!");
                return;
            }
            else
            {
                ChangeMechanicPage changeMechanicPage = new ChangeMechanicPage(Mechanics[mechanicList.SelectedIndex], Mechanics);
                NavigationService.Navigate(changeMechanicPage);
            }

            UpdateMechanicList();
        }

        private void goBackButton_Click(object sender, RoutedEventArgs e)
        {
            AdminHome adminHome = new AdminHome();

            this.NavigationService.Navigate(adminHome);
        }
    }
}
