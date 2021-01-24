using GUI.HomePage;
using Logic.Entities;
using Logic.Exceptions;
using Logic.Services;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace GUI.AdminLogin.MechanicManager_Admin
{
    /// <summary>
    /// Interaction logic for AddMechanicPage.xaml
    /// </summary>
    public partial class AddMechanicPage : Page
    {
        private bool untilFuther;

        ManageMechanics ManageMechanics = new ManageMechanics();

        private List<Mechanic> Mechanics = new List<Mechanic>();

        public AddMechanicPage()
        {
            InitializeComponent();

            try
            {
                Mechanics = ManageMechanics.AllMechanics();
            }
            catch
            {

            } 

            UpDatePage();
        }
        private void UpDatePage()
        {
            brakesCheckBox.IsChecked = false;
            engineCheckBox.IsChecked = false;
            carrieageBodyCheckBox.IsChecked = false;
            tireCheckBox.IsChecked = false;
            windShieldCheckBox.IsChecked = false;
            mechanicNameInput.Text = null;
            birthDayPicker.SelectedDate = DateTime.Now;
            dateOfEmployPicker.SelectedDate = DateTime.Now;
            untilFurtherButton.Content = "Inte tillsvidareanställd";
            endOfEmployLabel.Visibility = Visibility.Hidden;
            endOfEmployPicker.Visibility = Visibility.Hidden;
            untilFuther = true;
        }

        private void untilFurtherButton_Click(object sender, RoutedEventArgs e)
        {
            if (untilFurtherButton.Content.ToString() == "Tillsvidareanställd")
            {
                untilFurtherButton.Content = "Inte tillsvidareanställd";

                endOfEmployLabel.Visibility = Visibility.Hidden;
                endOfEmployPicker.Visibility = Visibility.Hidden;

                untilFuther = true;
            }
            else
            {
                untilFurtherButton.Content = "Tillsvidareanställd";

                endOfEmployLabel.Visibility = Visibility.Visible;
                endOfEmployPicker.Visibility = Visibility.Visible;

                untilFuther = false;
            }
        }

        private async void addMechanicButton_Click(object sender, RoutedEventArgs e)
        {
            if (mechanicNameInput.Text == "")
            {
                MessageBox.Show("Du måste fylla i alla värden!");
            }
            else
            {
                try
                {
                    ManageMechanics.SetQualifications(brakesCheckBox.IsChecked.Value, engineCheckBox.IsChecked.Value, carrieageBodyCheckBox.IsChecked.Value, tireCheckBox.IsChecked.Value, windShieldCheckBox.IsChecked.Value);
                    if (untilFuther)
                    {
                        await ManageMechanics.AddMechanic(Mechanics, mechanicNameInput.Text, birthDayPicker.SelectedDate.Value, dateOfEmployPicker.SelectedDate.Value);
                    }
                    else
                    {
                        await ManageMechanics.AddMechanic(Mechanics, mechanicNameInput.Text, birthDayPicker.SelectedDate.Value, dateOfEmployPicker.SelectedDate.Value, endOfEmployPicker.SelectedDate.Value);
                    }

                    ManageMechanicsPage manageMechanicsPage = new ManageMechanicsPage();

                    this.NavigationService.Navigate(manageMechanicsPage);
                }
                catch (DateOutOfReachException)
                {
                    MessageBox.Show("Någon av datumen är över eller under vad som är tillåtet");
                }
            }
        }

        private void goBackButton_Click(object sender, RoutedEventArgs e)
        {
           AdminHome adminHome = new AdminHome();

           this.NavigationService.Navigate(adminHome);
        }
    }
}
