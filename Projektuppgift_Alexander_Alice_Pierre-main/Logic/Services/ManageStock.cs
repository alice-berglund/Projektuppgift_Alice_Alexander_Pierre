using System.Collections.Generic;
using Logic.Entities;
using Logic.DAL;
using System.Threading.Tasks;

namespace Logic.Services
{

    public class ManageStock
    {
        DataAccess dataAccess = new DataAccess();

        public Dictionary<string, int> GetAllComponents(FileName fileName)
        {
            Dictionary<string, int> components = dataAccess.GetData(fileName);

            return components;
        }
        public async Task<bool> RemoveComponents(Component component, VehiclesAsEnum vehicle)
        {
                switch (vehicle)
                {
                    case VehiclesAsEnum.Bil:

                    Dictionary<string, int> CarComponents = new Dictionary<string, int>();

                    CarComponents = dataAccess.GetData(FileName.CarComponents);

                    if (
                    component == Component.Bromsar && CarComponents[component.ToString()] >= 4 ||
                    component == Component.Däck && CarComponents[component.ToString()] >= 4 )
                    
                    {
                        CarComponents[component.ToString()] -= 4;

                        await dataAccess.SaveData(CarComponents, FileName.CarComponents);
                        return true;
                    }

                    else if (
                        component == Component.Kaross && CarComponents[component.ToString()] >= 1
                        || component == Component.Motor && CarComponents[component.ToString()] >= 1
                        || component == Component.Vindrutor && CarComponents[component.ToString()] >= 1 )
                        
                    {
                        CarComponents[component.ToString()] -= 1;
                        await dataAccess.SaveData(CarComponents, FileName.CarComponents);
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case VehiclesAsEnum.Buss:

                    Dictionary<string, int> BusComponents = new Dictionary<string, int>();

                    BusComponents = dataAccess.GetData(FileName.BusComponents);

                    if (
                    component == Component.Bromsar && BusComponents[component.ToString()] >= 6 ||
                    component == Component.Däck && BusComponents[component.ToString()] >= 6)

                    {
                        BusComponents[component.ToString()] -= 6;

                        await dataAccess.SaveData(BusComponents, FileName.BusComponents);
                        return true;
                    }

                    else if (
                        component == Component.Kaross && BusComponents[component.ToString()] >= 1
                        || component == Component.Motor && BusComponents[component.ToString()] >= 1
                        || component == Component.Vindrutor && BusComponents[component.ToString()] >= 1)

                    {
                        BusComponents[component.ToString()] -= 1;
                        await dataAccess.SaveData(BusComponents, FileName.BusComponents);
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case VehiclesAsEnum.Lastbil:

                    Dictionary<string, int> TruckComponents = new Dictionary<string, int>();

                    TruckComponents = dataAccess.GetData(FileName.TruckComponents);

                    if (
                    component == Component.Bromsar && TruckComponents[component.ToString()] >= 6 ||
                    component == Component.Däck && TruckComponents[component.ToString()] >= 6)

                    {
                        TruckComponents[component.ToString()] -= 6;

                        await dataAccess.SaveData(TruckComponents, FileName.TruckComponents);
                        return true;
                    }

                    else if (
                        component == Component.Kaross && TruckComponents[component.ToString()] >= 1
                        || component == Component.Motor && TruckComponents[component.ToString()] >= 1
                        || component == Component.Vindrutor && TruckComponents[component.ToString()] >= 1)

                    {
                        TruckComponents[component.ToString()] -= 1;
                        await dataAccess.SaveData(TruckComponents, FileName.TruckComponents);
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case VehiclesAsEnum.Motorcykel:

                    Dictionary<string, int> MotorbikeComponents = new Dictionary<string, int>();

                    MotorbikeComponents = dataAccess.GetData(FileName.MotorbikeComponents);

                    if (
                    component == Component.Bromsar && MotorbikeComponents[component.ToString()] >= 2 ||
                    component == Component.Däck && MotorbikeComponents[component.ToString()] >= 2)

                    {
                        MotorbikeComponents[component.ToString()] -= 2;

                        await dataAccess.SaveData(MotorbikeComponents, FileName.MotorbikeComponents);
                        return true;
                    }

                    else if (
                        component == Component.Kaross && MotorbikeComponents[component.ToString()] >= 1
                        || component == Component.Motor && MotorbikeComponents[component.ToString()] >= 1
                        || component == Component.Vindrutor && MotorbikeComponents[component.ToString()] >= 1)

                    {
                        MotorbikeComponents[component.ToString()] -= 1;
                        await dataAccess.SaveData(MotorbikeComponents, FileName.MotorbikeComponents);
                        return true;
                    }
                    else
                    {
                        return false;
                    }                   
            }

            return false;
        }
        public async void AddComponents(Component component, VehiclesAsEnum vehicle, int amount)
        {
            switch (vehicle)
            {
                case VehiclesAsEnum.Bil:

                    Dictionary<string, int> CarComponents = new Dictionary<string, int>();
                    CarComponents = dataAccess.GetData(FileName.CarComponents);

                    CarComponents[component.ToString()] += amount;

                    await dataAccess.SaveData(CarComponents, FileName.CarComponents);

                    break;

                case VehiclesAsEnum.Buss:

                    Dictionary<string, int> BusComponents = new Dictionary<string, int>();
                    BusComponents = dataAccess.GetData(FileName.BusComponents);

                    BusComponents[component.ToString()] += amount;

                    await dataAccess.SaveData(BusComponents, FileName.BusComponents);

                    break;

                case VehiclesAsEnum.Lastbil:

                    Dictionary<string, int> TruckComponents = new Dictionary<string, int>();
                    TruckComponents = dataAccess.GetData(FileName.TruckComponents);

                    TruckComponents[component.ToString()] += amount;

                    await dataAccess.SaveData(TruckComponents, FileName.TruckComponents);

                    break;

                case VehiclesAsEnum.Motorcykel:

                    Dictionary<string, int> MotorbikeComponents = new Dictionary<string, int>();
                    MotorbikeComponents = dataAccess.GetData(FileName.MotorbikeComponents);

                    MotorbikeComponents[component.ToString()] += amount;

                    await dataAccess.SaveData(MotorbikeComponents, FileName.MotorbikeComponents);

                    break;

                default:

                    break;
            }
        }
        public Dictionary<string, int> CarComponentsDefault()
        {
            Dictionary<string, int> CarComponents = new Dictionary<string, int>()
            { {"Bromsar", 0}, {"Däck", 0}, {"Kaross", 0}, {"Motor", 0}, {"Vindrutor", 0} };

            return CarComponents;
        }
        public Dictionary<string, int> BusComponentsDefault()
        {
            Dictionary<string, int> BusComponents = new Dictionary<string, int>()
            { {"Bromsar", 0}, {"Däck", 0}, {"Kaross", 0}, {"Motor", 0}, {"Vindrutor", 0} };

            return BusComponents;
        }
        public Dictionary<string, int> TruckComponentsDefault()
        {
            Dictionary<string, int> TruckComponents = new Dictionary<string, int>()
            { {"Bromsar", 0}, {"Däck", 0}, {"Kaross", 0}, {"Motor", 0}, {"Vindrutor", 0} };

            return TruckComponents;
        }
        public Dictionary<string, int> MotorbikeComponentsDefault()
        {
            Dictionary<string, int> MotorbikeComponents = new Dictionary<string, int>()
            { {"Bromsar", 0}, {"Däck", 0}, {"Kaross", 0}, {"Motor", 0}, {"Vindrutor", 0} };

            return MotorbikeComponents;
        }
    }
} 
