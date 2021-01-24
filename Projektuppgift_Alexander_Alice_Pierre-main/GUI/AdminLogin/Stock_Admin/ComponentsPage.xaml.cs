using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Logic.Entities;
using Logic.Services;
using Logic.DAL;

namespace GUI.AdminLogin.Stock_Admin
{
    /// <summary>
    /// Interaction logic for ComponentsPage.xaml
    /// </summary>
    public partial class ComponentsPage : Page
    {
        ManageStock manageStock = new ManageStock();

        // --- Meddelanden för MessageBox ---
        private const string _purchaseFail = "Inköp misslyckades";
        private const string _purchaseSuccess = "Inköp lyckades";
        //

        public ComponentsPage()
        {
            InitializeComponent();
            PrintAllDropDown();
            PrintStockStatus();
        }

        private void PrintTypeOfProblem()
        {
            List<Component> TypeOfProblem = Enum.GetValues(typeof(Component)).Cast<Component>().ToList();

            foreach (Component problem in TypeOfProblem)
            {
                this.TypeOfProblemList.Items.Add(problem);
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
        private void PrintAllDropDown()
        {
            PrintTypeOfProblem();
            PrintTypeOfVehicle();
        }
        private void PrintStockStatus()
        {
            StockList.Items.Clear();

            Dictionary<string, int> InStockCar = manageStock.GetAllComponents(FileName.CarComponents);
            Dictionary<string, int> InStockBus = manageStock.GetAllComponents(FileName.BusComponents);
            Dictionary<string, int> InStockTruck = manageStock.GetAllComponents(FileName.TruckComponents);
            Dictionary<string, int> InStockMotorbike = manageStock.GetAllComponents(FileName.MotorbikeComponents);
            PrintComponent(InStockCar, "Bildelar på lager");
            PrintComponent(InStockBus, "Bussdelar på lager");
            PrintComponent(InStockTruck, "Lastbilsdelar på lager");
            PrintComponent(InStockMotorbike, "Motorcykeldelar på lager");
        }
        private void PrintComponent(Dictionary<string, int> dictionary, string vehicleType)
        {
            StockList.Items.Add($"{vehicleType}\n");

            foreach (KeyValuePair<string, int> keyValuePair in dictionary)
            {
                string stockComponents = $"{keyValuePair.Key} = {keyValuePair.Value}";
                StockList.Items.Add(stockComponents);
            }

            StockList.Items.Add("\n");
        }
        private void ClearAll()
        {
            TypeOfProblemList.Items.Clear();
            VehicleComboBox.Items.Clear();
            AmountBox.Clear();
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            int.TryParse(AmountBox.Text, out int amount);

            if (TypeOfProblemList.SelectedIndex == -1 || VehicleComboBox.SelectedIndex == -1 || amount <= 0)
            {
                MessageBox.Show(_purchaseFail);
                ClearAll();
                PrintAllDropDown();
                return;
            }

            else
            {
                Component component = (Component)TypeOfProblemList.SelectedItem;
                VehiclesAsEnum vehicle = (VehiclesAsEnum)VehicleComboBox.SelectedItem;

                manageStock.AddComponents(component, vehicle, amount);
                MessageBox.Show(_purchaseSuccess);
                ClearAll();
                PrintAllDropDown();
                PrintStockStatus();
                return;
            }
        }
    }
}
