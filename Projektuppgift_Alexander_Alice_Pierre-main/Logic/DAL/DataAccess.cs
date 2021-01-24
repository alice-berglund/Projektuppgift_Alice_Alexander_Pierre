using Logic.Entities;
using Logic.Services;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
//using Json.Net;
//using Newtonsoft.Json;

namespace Logic.DAL
{
    public enum FileName
    {
        User,
        Errands,
        Mechanic,
        Car,
        Truck,
        Bus,
        Motorbike,
        CarComponents,
        TruckComponents,
        BusComponents,
        MotorbikeComponents,

    }
    public class DataAccess : IDataAccess
    {
        // Generic metod för att hämta data från JSON.
        // Tar emot en enum för namn på fil.
        public List<T> GetData<T>(FileName fileName)
        {
            string path = $@"Dal\{fileName}.json";
            string jsonString;

            if (fileName == FileName.User)
            {
                if (!File.Exists(path))
                {
                    ManageUsers manageUsers = new ManageUsers();
                    List<User> users = manageUsers.AddAdmin();

                    jsonString = JsonSerializer.Serialize(users);

                   File.WriteAllText(path, jsonString);
                }
            }

            if (!File.Exists(path))
            {
                List<T> defaultList = new List<T>();
                return defaultList;
                
            }
            
            jsonString = File.ReadAllText(path);

            List<T> getData = JsonSerializer.Deserialize<List<T>>(jsonString);

            return getData;
        }

        // Generic metod för att spara data till JSON.
        // Tar emot list som ska skrivas över och en enum för namn på fil.
        public async Task SaveData<T>(List<T> list, FileName fileName)
        {
            string path = $@"Dal\{fileName}.json";
            
            var jsonString = JsonSerializer.Serialize(list);

            await File.WriteAllTextAsync(path, jsonString);
        }

        // Metod för att spara data till Dictionary-JSON.
        // Overload.
        public async Task SaveData(Dictionary<string, int> dictionary, FileName fileName)
        {
            string path = $@"Dal\{fileName}.json";

            JsonSerializerOptions options = new JsonSerializerOptions();

            options.Converters.Add(new CustomDictionaryJsonConverter<Component, int>());

            var json = JsonSerializer.Serialize(dictionary, options);

            await File.WriteAllTextAsync(path, json);

        }

        // Metod för att hämta data från Dictionary-JSON.
        // Overload.
        public Dictionary<string, int> GetData(FileName fileName)
        {
            string path = $@"Dal\{fileName}.json";
            string jsonString;

            CheckIfFileExists(fileName);

            jsonString = File.ReadAllText(path);

            Dictionary<string, int> getData = JsonSerializer.Deserialize<Dictionary<string, int>>(jsonString);

            return getData;
        }
        public void CheckIfFileExists(FileName fileName)
        {
            string path = $@"Dal\{fileName}.json";

            if (fileName == FileName.CarComponents)
            {
                if (!File.Exists(path))
                {
                    ManageStock manageStock = new ManageStock();
                    Dictionary<string, int> CarComponents = manageStock.CarComponentsDefault();

                    _ = SaveData(CarComponents, FileName.CarComponents);
                }
            }
            if (fileName == FileName.BusComponents)
            {
                if (!File.Exists(path))
                {
                    ManageStock manageStock = new ManageStock();
                    Dictionary<string, int> BusComponents = manageStock.BusComponentsDefault();

                    _ = SaveData(BusComponents, FileName.BusComponents);
                }
            }
            if (fileName == FileName.TruckComponents)
            {
                if (!File.Exists(path))
                {
                    ManageStock manageStock = new ManageStock();
                    Dictionary<string, int> TruckComponents = manageStock.TruckComponentsDefault();

                    _ = SaveData(TruckComponents, FileName.TruckComponents);
                }
            }
            if (fileName == FileName.MotorbikeComponents)
            {
                if (!File.Exists(path))
                {
                    ManageStock manageStock = new ManageStock();
                    Dictionary<string, int> MotorbikeComponents = manageStock.MotorbikeComponentsDefault();

                    _ = SaveData(MotorbikeComponents, FileName.MotorbikeComponents);
                }
            }
        }
    }


}
