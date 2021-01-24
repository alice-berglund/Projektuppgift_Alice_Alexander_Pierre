using GUI.AdminLogin.Errands_Admin;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Logic.Entities;
using Logic.Services;
using System.Linq;
using System.Threading.Tasks;

namespace GUI.AdminLoginPage.ErrandsPage_Admin
{
    /// <summary>
    /// Interaction logic for ManageErrandsPage.xaml
    /// </summary>
    public partial class ManageErrandsPage : Page
    {
        ManageErrands manageErrands = new ManageErrands();
        ManageMechanics manageMechanics = new ManageMechanics();
        public List<Errand> Errands { get; set; }
        public List<Mechanic> Mechanics { get; set; }

        // --- Meddelanden för MessageBox ---
        private const string _noSelectionErrand = "Inget ärende har valts";
        private const string _noSelectionMechanic = "Du måste välja en mekaniker först";
        private const string _chooseMechanic = "Välj en mekaniker i listan för att tilldela";
        private const string _errandAssigned = "Ärende tilldelat";
        private const string _errandDone = "Ärendet har ändrat status till avslutat";
        private const string _alreadyDone = "Ärendet är redan avslutat";
        private const string _alreadyPending = "Du kan inte ta bort ett pågående ärende";
        private const string _notAssigned = "Ärendet har inte påbörjats av en mekaniker";
        // ----------------------------------

        public ManageErrandsPage()
        {
            InitializeComponent();
            UpdateErrandsList();          
        }

        private void NewErrandButton_Click(object sender, RoutedEventArgs e)
        {
            NewErrandPage newErrand = new NewErrandPage();
            this.NavigationService.Navigate(newErrand);
        }

        private void RemoveErrandButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedErrand = (Errand)ErrandsDataListView.SelectedItem;

            if (selectedErrand == null)
            {
                MessageBox.Show(_noSelectionErrand);
            }
            else if (selectedErrand.WorkState == WorkState.Pågående)
            {
                MessageBox.Show(_alreadyPending);
                return;
            }

            RemoveErrand();
            UpdateErrandsList();
        }

        private void ShowErrandButton_Click(object sender, RoutedEventArgs e)
        {
            ShowSelectedErrand();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
            RefreshPage();
        }

        private async void ErrandDoneButton_Click(object sender, RoutedEventArgs e)
        {
            await ChangeWorkState();
            UpdateErrandsList();
        }

        private async void ChooseMechanicButton_Click(object sender, RoutedEventArgs e)
        {
            await AssignToMechanic();
            UpdateErrandsList();
        }

        #region Metoder
        private void RefreshPage()
        {
            ManageErrandsPage refreshPage = new ManageErrandsPage();

            this.NavigationService.Navigate(refreshPage);
        }
        private void UpdateErrandsList()
        {

            Errands = new List<Errand>();

            Errands.AddRange((manageErrands.GetAllErrands()).OrderBy(x => x.WorkState));

            ErrandsDataListView.ItemsSource = Errands;
        }
        private void ShowSelectedErrand()
        {
            SelectedErrand.Items.Clear();

            if (ErrandsDataListView.SelectedItem == null)
            {
                MessageBox.Show(_noSelectionErrand);
                return;
            }

            var showErrand = (Errand)ErrandsDataListView.SelectedItems[0];
            SelectedErrand.Items.Add(showErrand.ToString());

            List<Mechanic> Mechanics = manageMechanics.AllMechanics();
            var mechanic = Mechanics.FirstOrDefault(x => x.IdNumber == showErrand.MechanicId);

            if (mechanic != null)
            {
                SelectedErrand.Items.Add($"\n");
                SelectedErrand.Items.Add(mechanic.ToString());
            }

            if (showErrand.MechanicId == Guid.Empty)
            {
                ShowAvailableMechanics(showErrand.Component);
            }
            else if (showErrand.MechanicId != Guid.Empty)
            {
                lbSelectMechanic.ItemsSource = String.Empty;
            }
        }
        private void ShowAvailableMechanics(Component component)
        {
            Mechanics = new List<Mechanic>();
            Mechanics.AddRange(manageMechanics.AllMechanics());

            var availableMechanics = Mechanics.Where(x => x.Qualifications.Contains(component)).ToList();
            var filteredMechanics = manageErrands.CheckAvailableMechanics(availableMechanics);

            lbSelectMechanic.ItemsSource = filteredMechanics;

            foreach (Mechanic mechanic in filteredMechanics)
            {
                mechanic.ToString();
            }
        }
        private async Task AssignToMechanic()
        {
           
            if (lbSelectMechanic.SelectedItem == null)
            {
                MessageBox.Show(_noSelectionMechanic);
                return;
            }
            else if (ErrandsDataListView.SelectedItem == null)
            {
                MessageBox.Show(_noSelectionErrand);
                return;
            }
            else
            {
                var selectedMechanic = (Mechanic)lbSelectMechanic.SelectedItem;
                var selectedErrand = (Errand)ErrandsDataListView.SelectedItem;

                await manageErrands.AssignMechanicToErrand(selectedMechanic, selectedErrand);

                MessageBox.Show(_errandAssigned);

                UpdateErrandsList();
            }
        }
        private async Task ChangeWorkState()
        {
            var selectedErrand = (Errand)this.ErrandsDataListView.SelectedItem;

            if (selectedErrand == null)
            {
                MessageBox.Show(_noSelectionErrand);
            }
            else if (selectedErrand.WorkState == WorkState.Pågående)
            {
                RemoveErrand();

                Errands = new List<Errand>();
                Errands.AddRange(manageErrands.GetAllErrands());

                await manageErrands.ChangeWorkStateErrand(selectedErrand, Errands);
                await manageErrands.ChangeWorkStateMechanic(selectedErrand);

                MessageBox.Show(_errandDone);
            }
            else if (selectedErrand.WorkState == WorkState.Obemannad)
            {
                MessageBox.Show(_notAssigned);
            }
            else if (selectedErrand.WorkState == WorkState.Avslutat)
            {
                MessageBox.Show(_alreadyDone);
            }
        }
        private async void RemoveErrand()
        {
            Errand selectedErrand = (Errand)this.ErrandsDataListView.SelectedItem;

            if (this.ErrandsDataListView.SelectedItem == null)
            {
                MessageBox.Show(_noSelectionErrand);
                return;
            }
            else
            {
                await manageErrands.RemoveErrand(selectedErrand, Errands);
                SelectedErrand.Items.Clear();
            }
        }
        #endregion

    }

}
