using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Logic.Services;
using Logic.Entities;
using GUI.AdminLoginPage.ErrandsPage_Admin;
using Logic.DAL;

namespace GUI.AdminLogin.Errands_Admin
{
    /// <summary>
    /// Interaction logic for NewErrand.xaml
    /// </summary>
    public partial class NewErrandPage : Page
    {
        ManageErrands manageErrands = new ManageErrands();
        ManageVehicles manageVehicles = new ManageVehicles();
        ManageStock manageStock = new ManageStock();
        public List<Vehicle> Vehicles { get; set; }
        public List<Errand> Errands { get; set; }

        // --- Meddelanden för MessageBox ---
        private const string _errandSuccess = "Ärende skapat";
        private const string _errandFail = "Misslyckades, fyll i alla fält korrekt";
        private const string _notEnoughComponents = "Inte tillräckligt med komponenter på lager";
        // ----------------------------------

        public NewErrandPage()
        {
            InitializeComponent();

            Errands = new List<Errand>();

            PrintAllDropDown();
            NoVehicleVisibility();

            Errands.AddRange(manageErrands.GetAllErrands());    
        }
        private void ClearAll()
        {
            ModelNameBox.Clear();
            RegNumberBox.Clear();
            OdometerBox.Clear();
            PropBlock.Clear();
            DescriptionBox.Clear();
            TypeOfProblemList.Items.Clear();
            VehicleComboBox.Items.Clear();
            FuelComboBox.Items.Clear();
            cbTypeOfCar.Items.Clear();
            TowBarNo.IsChecked = false;
            TowBarYes.IsChecked = false;

        }
        private void NavigateBack()
        {
            ManageErrandsPage manageErrandsPage = new ManageErrandsPage();
            this.NavigationService.Navigate(manageErrandsPage);
        }
        private void PrintAllDropDown()
        {
            PrintTypeOfProblem();
            PrintTypeOfVehicle();
            PrintTypeOfFuel();
            PrintTypeOfCar();
        }
        private void PrintTypeOfProblem()
        {
            List<Component> TypeOfProblem = Enum.GetValues(typeof(Component)).Cast<Component>().ToList();

            foreach (Component problem in TypeOfProblem)
            {
                this.TypeOfProblemList.Items.Add(problem);
            }

        }
        private void PrintTypeOfFuel()
        {
            List<Fuel> FuelType = Enum.GetValues(typeof(Fuel)).Cast<Fuel>().ToList();

            foreach (Fuel fuel in FuelType)
            {
                this.FuelComboBox.Items.Add(fuel);
            }
        }
        private void PrintTypeOfVehicle()
        {
            List<VehiclesAsEnum> TypeVehicle = Enum.GetValues(typeof(VehiclesAsEnum)).Cast<VehiclesAsEnum>().ToList();

            foreach (VehiclesAsEnum vehicle in TypeVehicle)
            {
                this.VehicleComboBox.Items.Add(vehicle);
            }
        }
        private void PrintTypeOfCar()
        {
            List<TypeOfCar> CarType = Enum.GetValues(typeof(TypeOfCar)).Cast<TypeOfCar>().ToList();

            foreach (TypeOfCar type in CarType)
            {
                this.cbTypeOfCar.Items.Add(type);
            }
        }
        private async Task NewErrand()
        {
            Component problem = (Component)TypeOfProblemList.SelectedIndex;
            Fuel fuel = (Fuel)FuelComboBox.SelectedIndex;
            string modelName = ModelNameBox.Text;
            string regNumber = RegNumberBox.Text;
            int.TryParse(OdometerBox.Text, out int odoMeter);

            bool isValid = CheckIfValidInput();

            if (isValid == false)
            {
                MessageBox.Show(_errandFail);
                ClearAll();
                return;
            }

            else if (VehicleComboBox.SelectedIndex == 0)
            {
                await NewErrandCar(problem, fuel, modelName, regNumber, odoMeter);
                NavigateBack();
            }

            else if (VehicleComboBox.SelectedIndex == 1)
            {
                await NewErrandBus(problem, fuel, modelName, regNumber, odoMeter);
                NavigateBack();
            }

            else if (VehicleComboBox.SelectedIndex == 2)
            {
                await NewErrandTruck(problem, fuel, modelName, regNumber, odoMeter);
                NavigateBack();
            }

            else if (VehicleComboBox.SelectedIndex == 3)
            {
                await NewErrandMotorbike(problem, fuel, modelName, regNumber, odoMeter);
                NavigateBack();
            }

            Errands.AddRange(manageErrands.GetAllErrands());
        }
        private async Task NewErrandCar(Component problem, Fuel fuel, string modelName, string regNumber, int odoMeter)
        {
            if (TowBarYes.IsChecked == true)
            {
                Component component = (Component)TypeOfProblemList.SelectedItem;
                VehiclesAsEnum vehicle = (VehiclesAsEnum)VehicleComboBox.SelectedItem;
                TypeOfCar typeOfCar = (TypeOfCar)cbTypeOfCar.SelectedItem;

                Task<bool> result = manageStock.RemoveComponents(component, vehicle);
                bool inStock = result.Result;

                if (inStock == true)
                {
                    bool towBar = false;
                    Guid vehicleId = await manageVehicles.NewCar(modelName, regNumber, odoMeter, fuel, towBar, typeOfCar);
                    await manageErrands.NewErrand(Errands, DescriptionBox.Text, problem, vehicleId);

                    MessageBox.Show(_errandSuccess);
                }
                else if (inStock == false)
                {
                    MessageBox.Show(_notEnoughComponents);
                    ClearAll();
                }
            }
            else if (TowBarNo.IsChecked == true)
            {
                Component component = (Component)TypeOfProblemList.SelectedItem;
                VehiclesAsEnum vehicle = (VehiclesAsEnum)VehicleComboBox.SelectedItem;
                TypeOfCar typeOfCar = (TypeOfCar)cbTypeOfCar.SelectedItem;

                Task<bool> result = manageStock.RemoveComponents(component, vehicle);
                bool inStock = result.Result;

                if (inStock == true)
                {
                    bool towBar = false;
                    Guid vehicleId = await manageVehicles.NewCar(modelName, regNumber, odoMeter, fuel, towBar, typeOfCar);
                    await manageErrands.NewErrand(Errands, DescriptionBox.Text, problem, vehicleId);

                    MessageBox.Show(_errandSuccess);
                }
                else if (inStock == false)
                {
                    MessageBox.Show(_notEnoughComponents);
                    ClearAll();
                }
            }
        }

        private async Task NewErrandTruck(Component problem, Fuel fuel, string modelName, string regNumber, int odoMeter)
        {
            Component component = (Component)TypeOfProblemList.SelectedItem;
            VehiclesAsEnum vehicle = (VehiclesAsEnum)VehicleComboBox.SelectedItem;

            Task<bool> result = manageStock.RemoveComponents(component, vehicle);
            bool inStock = result.Result;

            if (inStock == true)
            {
                int.TryParse(PropText.Text, out int maxLoad);
                Guid vehicleId = await manageVehicles.NewTruck(modelName, regNumber, odoMeter, fuel, maxLoad);

                await manageErrands.NewErrand(Errands, DescriptionBox.Text, problem, vehicleId);
                MessageBox.Show(_errandSuccess);
            }

            else if (inStock == false)
            {
                MessageBox.Show(_notEnoughComponents);
                ClearAll();
            }
        }

        private async Task NewErrandBus(Component problem, Fuel fuel, string modelName, string regNumber, int odoMeter)
        {
            Component component = (Component)TypeOfProblemList.SelectedItem;
            VehiclesAsEnum vehicle = (VehiclesAsEnum)VehicleComboBox.SelectedItem;

            Task<bool> result = manageStock.RemoveComponents(component, vehicle);
            bool inStock = result.Result;

            if (inStock == true)
            {
                int.TryParse(PropText.Text, out int maxPassengers);
                Guid vehicleId = await manageVehicles.NewBus(modelName, regNumber, odoMeter, fuel, maxPassengers);

                await manageErrands.NewErrand(Errands, DescriptionBox.Text, problem, vehicleId);

                MessageBox.Show(_errandSuccess);
            }
            else if (inStock == false)
            {
                MessageBox.Show(_notEnoughComponents);
                ClearAll();
            }
        }

        private async Task NewErrandMotorbike(Component problem, Fuel fuel, string modelName, string regNumber, int odoMeter)
        {
            if (VehicleComboBox.SelectedIndex == 3)
            {
                Component component = (Component)TypeOfProblemList.SelectedItem;
                VehiclesAsEnum vehicle = (VehiclesAsEnum)VehicleComboBox.SelectedItem;

                Task<bool> result = manageStock.RemoveComponents(component, vehicle);
                bool inStock = result.Result;

                if (inStock == true)
                {
                    Guid vehicleId = await manageVehicles.NewMotorbike(modelName, regNumber, odoMeter, fuel);

                    await manageErrands.NewErrand(Errands, DescriptionBox.Text, problem, vehicleId);

                    MessageBox.Show(_errandSuccess);
                }

                else if (inStock == false)
                {
                    MessageBox.Show(_notEnoughComponents);
                    ClearAll();
                }
            }
        }

        private void NoVehicleVisibility()
        {
            PropText.Visibility = Visibility.Hidden;
            PropBlock.Visibility = Visibility.Hidden;
            TowBarTextBox.Visibility = Visibility.Hidden;
            TowBarNo.Visibility = Visibility.Hidden;
            TowBarYes.Visibility = Visibility.Hidden;
            tbTypeOfcar.Visibility = Visibility.Hidden;
            cbTypeOfCar.Visibility = Visibility.Hidden;
        }
        private void CarVisibility()
        {
            PropText.Visibility = Visibility.Hidden;
            PropBlock.Visibility = Visibility.Hidden;
            TowBarTextBox.Visibility = Visibility.Visible;
            TowBarNo.Visibility = Visibility.Visible;
            TowBarYes.Visibility = Visibility.Visible;
            tbTypeOfcar.Visibility = Visibility.Visible;
            cbTypeOfCar.Visibility = Visibility.Visible;
        }
        private void TruckVisibility()
        {
            PropText.Text = "Max last";
            PropText.Visibility = Visibility.Visible;
            PropBlock.Visibility = Visibility.Visible;
            TowBarTextBox.Visibility = Visibility.Hidden;
            TowBarNo.Visibility = Visibility.Hidden;
            TowBarYes.Visibility = Visibility.Hidden;
            tbTypeOfcar.Visibility = Visibility.Hidden;
            cbTypeOfCar.Visibility = Visibility.Hidden;
        }
        private void BusVisibility()
        {
            PropText.Text = "Max passagerare";
            PropText.Visibility = Visibility.Visible;
            PropBlock.Visibility = Visibility.Visible;
            TowBarTextBox.Visibility = Visibility.Hidden;
            TowBarNo.Visibility = Visibility.Hidden;
            TowBarYes.Visibility = Visibility.Hidden;
            tbTypeOfcar.Visibility = Visibility.Hidden;
            cbTypeOfCar.Visibility = Visibility.Hidden;
        }
        private void MotorbikeVisibility()
        {
            PropText.Visibility = Visibility.Hidden;
            PropBlock.Visibility = Visibility.Hidden;
            TowBarTextBox.Visibility = Visibility.Hidden;
            TowBarNo.Visibility = Visibility.Hidden;
            TowBarYes.Visibility = Visibility.Hidden;
            tbTypeOfcar.Visibility = Visibility.Hidden;
            cbTypeOfCar.Visibility = Visibility.Hidden;
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private bool CheckIfValidInput()
        {
            if (
                TypeOfProblemList.SelectedIndex == -1 ||
                FuelComboBox.SelectedIndex == -1 ||
                ModelNameBox.Text == null ||
                RegNumberBox.Text == null )

            {
                return false;
            }
            else
            {
                return true;
            }
        }
        private async void SubmitErrandButton_Click(object sender, RoutedEventArgs e)
        {
            await NewErrand();

            ClearAll();
            PrintAllDropDown();
            NoVehicleVisibility();
        }

        private void VehicleComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (VehicleComboBox.SelectedIndex == 0)
            {
                CarVisibility();
            }
            if (VehicleComboBox.SelectedIndex == 1)
            {
                BusVisibility();
            }
            if (VehicleComboBox.SelectedIndex == 2)
            {
                TruckVisibility();
            }
            if (VehicleComboBox.SelectedIndex == 3)
            {
                MotorbikeVisibility();
            }
        }
        private void TowBarYes_Checked(object sender, RoutedEventArgs e)
        {
            TowBarNo.IsChecked = false;
        }

        private void TowBarNo_Checked(object sender, RoutedEventArgs e)
        {
            TowBarYes.IsChecked = false;
        }
    }
}
