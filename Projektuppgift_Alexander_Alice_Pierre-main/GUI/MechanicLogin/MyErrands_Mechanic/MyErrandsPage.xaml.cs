using GUI.Home;
using Logic.Entities;
using Logic.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace GUI.MechanicLoginPage.MyErrands_Mechanic
{
    /// <summary>
    /// Interaction logic for MyErradsPage.xaml
    /// </summary>
    public partial class MyErrandsPage : Page
    {
        ManageErrands ManageErrands = new ManageErrands();
        bool ShowCurrentErrends = true;
        List<Errand> Errands;
        List<Errand> MyErrands;
        Mechanic Mechanic;

        public MyErrandsPage(Mechanic mechanic)
        {
            InitializeComponent();
            Mechanic = mechanic;
            UpdateErrendList();
        }

        public void UpdateErrendList()
        {
            this.errendsListBox.Items.Clear();
            this.errendInfoTextBox.Clear();

            Errands = new List<Errand>();

            try
            {
                Errands.AddRange(ManageErrands.GetAllErrands());

                MyErrands = Errands.Where(x => x.MechanicId == Mechanic.IdNumber).ToList();

                foreach (Errand errand in MyErrands)
                {
                    if(ShowCurrentErrends)
                    {
                        if (errand.WorkState == WorkState.Pågående)
                        {
                            string mechanicInfo = $"{errand.Component} + {errand.ErrandId}";
                            errendsListBox.Items.Add(mechanicInfo);
                        }
                    }
                    else
                    {
                        if (errand.WorkState == WorkState.Avslutat)
                        {
                            string mechanicInfo = $"{errand.Component} + {errand.ErrandId}";
                            errendsListBox.Items.Add(mechanicInfo);
                        }
                    }       
                }
            }
            catch
            {

            }
        }

        private void currentErrandsButton_Click(object sender, RoutedEventArgs e)
        {
            if (currentErrandsButton.Content.ToString() == "Avslutade ärenden")
            {
                currentErrandsButton.Content = "Pågående ärenden";
                errendDoneButton.Visibility = Visibility.Hidden;
                ShowCurrentErrends = false;
            }
            else
            {
                currentErrandsButton.Content = "Avslutade ärenden";
                errendDoneButton.Visibility = Visibility.Visible;
                ShowCurrentErrends = true;
            }

            UpdateErrendList();
        }

        private void errendsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (errendsListBox.SelectedIndex == -1)
                return;

            StringBuilder selectedMechanicInfo = new StringBuilder();

            selectedMechanicInfo.Append($"Typ: {MyErrands[errendsListBox.SelectedIndex].Component}" +
                $"\nBeskrivning: {MyErrands[errendsListBox.SelectedIndex].Description}" +
                $"\nÄrendeID: {MyErrands[errendsListBox.SelectedIndex].ErrandId}" +
                $"\nFordonsID: {MyErrands[errendsListBox.SelectedIndex].VehicleId}" +
                $"\nMekanikerID: {MyErrands[errendsListBox.SelectedIndex].MechanicId}" +
                $"\nArbetsstatus: {MyErrands[errendsListBox.SelectedIndex].WorkState}");

            this.errendInfoTextBox.Text = selectedMechanicInfo.ToString();
        }

        private async void errendDoneButton_Click(object sender, RoutedEventArgs e)
        {
            if(errendsListBox.SelectedItem == null)
            {
                MessageBox.Show("Inget ärende är valt");
                return;
            }
            else
            {
                await ManageErrands.ChangeWorkStateErrand(MyErrands[errendsListBox.SelectedIndex], Errands);
                await ManageErrands.ChangeWorkStateMechanic(MyErrands[errendsListBox.SelectedIndex]);
            }

            UpdateErrendList();
        }

        private void goBackButton_Click(object sender, RoutedEventArgs e)
        {
            MechanicHome mechanicHome = new MechanicHome(Mechanic);

            this.NavigationService.Navigate(mechanicHome);
        }
    }
}
