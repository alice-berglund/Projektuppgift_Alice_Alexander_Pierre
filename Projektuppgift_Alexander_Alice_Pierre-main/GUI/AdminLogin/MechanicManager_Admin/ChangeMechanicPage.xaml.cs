using GUI.HomePage;
using Logic.Entities;
using Logic.Services;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace GUI.AdminLogin.MechanicManager_Admin
{
    /// <summary>
    /// Interaction logic for ChangeMechanicPage.xaml
    /// </summary>
    public partial class ChangeMechanicPage : Page
    {
        Mechanic Mechanic;

        List<Mechanic> Mechanics;
        public ChangeMechanicPage(Mechanic mechanic, List<Mechanic> mechanics)
        {
            InitializeComponent();

            Mechanic = mechanic;

            Mechanics = mechanics;

            this.mechanicToChangeNamelabel.Content = mechanic.Name;

            foreach(Component qualification in mechanic.Qualifications)
            {
                if (qualification == Component.Bromsar)
                    this.brakesCheckBox.IsChecked = true;

                if (qualification == Component.Däck)
                    this.tiresCheckBox.IsChecked = true;

                if (qualification == Component.Kaross)
                    this.carrieageBodyCheckBox.IsChecked = true;

                if (qualification == Component.Motor)
                    this.engineCheckBox.IsChecked = true;

                if (qualification == Component.Vindrutor)
                    this.windShieldCheckBox.IsChecked = true;
            }
        }

        private async void changeButton_Click(object sender, RoutedEventArgs e)
        {
            List<Component> qualifications = new List<Component>();

            if (this.brakesCheckBox.IsChecked == true)
                qualifications.Add(Component.Bromsar);

            if (this.engineCheckBox.IsChecked == true)
                qualifications.Add(Component.Motor);

            if (this.carrieageBodyCheckBox.IsChecked == true)
                qualifications.Add(Component.Kaross);

            if (this.tiresCheckBox.IsChecked == true)
                qualifications.Add(Component.Däck);

            if (this.windShieldCheckBox.IsChecked == true)
                qualifications.Add(Component.Vindrutor);

            ManageMechanics manageMechanics = new ManageMechanics();

            await manageMechanics.ChangeMechanic(Mechanic.IdNumber.ToString(), qualifications, Mechanics);

            ManageMechanicsPage manageMechanicsPage = new ManageMechanicsPage();
            this.NavigationService.Navigate(manageMechanicsPage);
        }

        private void goBackButton_Click(object sender, RoutedEventArgs e)
        {
            AdminHome adminHome = new AdminHome();
            this.NavigationService.Navigate(adminHome);
        }
    }
}
