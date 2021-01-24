using Logic.DAL;
using Logic.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Logic.Services
{
    public class ManageVehicles
    {
        DataAccess dataAccess = new DataAccess();

        public List<T> GetVehicle<T>(FileName fileName)
        {
            List<T> vehicles = (List<T>)dataAccess.GetData<T>(fileName);

            return vehicles;
        }

        public async Task<Guid> NewCar(string modelName, string regNumber, int odoMeter, Fuel typeOfFuel, bool towBar, TypeOfCar typeOfCar)
        {          
            Car car = new Car();

            car.ModelName = modelName;
            car.RegNumber = regNumber;
            car.Odometer = odoMeter;
            car.RegDate = DateTime.Now;
            car.TypeOfFuel = typeOfFuel;
            car.IdNumber = Guid.NewGuid();
            car.Towbar = towBar;
            car.TypeOfCar = typeOfCar;

            List<Car> Cars = dataAccess.GetData<Car>(FileName.Car);

            Cars.Add(car);
            
            await dataAccess.SaveData<Car>(Cars, FileName.Car);

            return car.IdNumber;
        }
        public async Task<Guid> NewBus(string modelName, string regNumber, int odoMeter, Fuel typeOfFuel, int maxPassengers)
        {
            Bus bus = new Bus();

            bus.ModelName = modelName;
            bus.RegNumber = regNumber;
            bus.Odometer = odoMeter;
            bus.RegDate = DateTime.Now;
            bus.TypeOfFuel = typeOfFuel;
            bus.IdNumber = Guid.NewGuid();
            bus.MaxPassengers = maxPassengers;

            List<Bus> Buses = dataAccess.GetData<Bus>(FileName.Bus);

            Buses.Add(bus);

            await dataAccess.SaveData<Bus>(Buses, FileName.Bus);

            return bus.IdNumber;
        }
        public async Task<Guid> NewTruck(string modelName, string regNumber, int odoMeter, Fuel typeOfFuel, int maxLoad)
        {
            Truck truck = new Truck();

            truck.ModelName = modelName;
            truck.RegNumber = regNumber;
            truck.Odometer = odoMeter;
            truck.RegDate = DateTime.Now;
            truck.TypeOfFuel = typeOfFuel;
            truck.IdNumber = Guid.NewGuid();
            truck.MaxLoad = maxLoad;

            List<Truck> Trucks = dataAccess.GetData<Truck>(FileName.Truck);

            Trucks.Add(truck);

            await dataAccess.SaveData<Truck>(Trucks, FileName.Truck);

            return truck.IdNumber;
        }
        public async Task<Guid> NewMotorbike(string modelName, string regNumber, int odoMeter, Fuel typeOfFuel)
        {
            MotorBike motorbike = new MotorBike();

            motorbike.ModelName = modelName;
            motorbike.RegNumber = regNumber;
            motorbike.Odometer = odoMeter;
            motorbike.RegDate = DateTime.Now;
            motorbike.TypeOfFuel = typeOfFuel;
            motorbike.IdNumber = Guid.NewGuid();


            List<MotorBike> Motorbikes = dataAccess.GetData<MotorBike>(FileName.Motorbike);

            Motorbikes.Add(motorbike);

            await dataAccess.SaveData<MotorBike>(Motorbikes, FileName.Motorbike);

            return motorbike.IdNumber;
        }
        public List<Car> GetAllCars()
        {
            List<Car> cars = (List<Car>)dataAccess.GetData<Car>(FileName.Car);

            return cars;
        }
        public List<Truck> GetAllTrucks()
        {
            List<Truck> trucks = (List<Truck>)dataAccess.GetData<Truck>(FileName.Truck);

            return trucks;
        }
        public List<Bus> GetAllBuses()
        {
            List<Bus> buses = (List<Bus>)dataAccess.GetData<Bus>(FileName.Bus);

            return buses;
        }
        public List<MotorBike> GetAllMotorbikes()
        {
            List<MotorBike> motorBikes = (List<MotorBike>)dataAccess.GetData<MotorBike>(FileName.Motorbike);

            return motorBikes;
        }

    }
}
