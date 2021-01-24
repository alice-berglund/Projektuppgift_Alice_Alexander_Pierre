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

namespace GUI.MechanicLoginPage.MyExpertise_Mechanic
{
    /// <summary>
    /// Interaction logic for MyExpertisePage.xaml
    /// </summary>
    public partial class MyExpertisePage : Page
    {
        Mechanic Mechanic;
        ManageMechanics ManageMechanics;
        List<Component> MyQualites;
        List<Component> UnCheckedQualites;
        public MyExpertisePage(Mechanic mechanic)
        {
            InitializeComponent();
            Mechanic = mechanic;
            UpDatePage();
        }

        public void UpDatePage()
        {
            qualitiesListBox.Items.Clear();

            MyQualites = Mechanic.Qualifications;

            foreach(Component component in MyQualites)
            {
                qualitiesListBox.Items.Add(component);
            }

            List<Component> qualities = new List<Component> { Component.Bromsar, Component.Däck, Component.Kaross, Component.Motor, Component.Vindrutor };

            UnCheckedQualites = qualities.Where(item => !MyQualites.Contains(item)).ToList();

            qualitieDropDown.Items.Clear();

            foreach(Component component in UnCheckedQualites)
            {
                qualitieDropDown.Items.Add(component);
            }
  
        }

        private async void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            if(qualitiesListBox.SelectedItem == null)
            {
                MessageBox.Show("Ingen kompetens är vald!");
            }
            else
            {
                MyQualites.RemoveAt(qualitiesListBox.SelectedIndex);

                ManageMechanics = new ManageMechanics();

                await ManageMechanics.ChangeMechanic(Mechanic.IdNumber.ToString(), MyQualites, ManageMechanics.AllMechanics());
                UpDatePage();
            }
        }

        private async void addNewQualiti_Click(object sender, RoutedEventArgs e)
        {
            if(qualitieDropDown.SelectedItem == null)
            {
                MessageBox.Show("Ingen kompetens är vald!");
            }
            else
            {
                ManageMechanics = new ManageMechanics();
                MyQualites.Add(UnCheckedQualites[qualitieDropDown.SelectedIndex]);
                await ManageMechanics.ChangeMechanic(Mechanic.IdNumber.ToString(), MyQualites, ManageMechanics.AllMechanics());
                UpDatePage();
            }
        }

        private void goBackButton_Click(object sender, RoutedEventArgs e)
        {
            MechanicHome mechanicHome = new MechanicHome(Mechanic);

            this.NavigationService.Navigate(mechanicHome);
        }
    }
}
