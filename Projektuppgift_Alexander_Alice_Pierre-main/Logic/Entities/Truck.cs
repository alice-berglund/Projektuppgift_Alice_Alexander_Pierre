using System;

namespace Logic.Entities
{
    public class Truck : Vehicle
    {
        public int MaxLoad { get; set; }

        public Truck()
        {

        }
        public Truck(string modelName, string regNumber, int odoMeter, DateTime regDate, Fuel typeOfFuel, int maxLoad)
        {
            this.ModelName = modelName;
            this.RegNumber = regNumber;
            this.Odometer = odoMeter;
            this.RegDate = DateTime.Now;
            this.TypeOfFuel = typeOfFuel;
            this.MaxLoad = maxLoad;
        }
    }
}
