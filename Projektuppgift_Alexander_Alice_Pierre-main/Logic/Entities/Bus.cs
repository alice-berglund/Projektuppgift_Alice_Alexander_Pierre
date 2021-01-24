using System;

namespace Logic.Entities
{
    public class Bus : Vehicle
    {
        public int MaxPassengers { get; set; }

        public Bus()
        {

        }
        public Bus(string modelName, string regNumber, int odoMeter, DateTime regDate, Fuel typeOfFuel, int maxPassengers)
        {
            this.ModelName = modelName;
            this.RegNumber = regNumber;
            this.Odometer = odoMeter;
            this.RegDate = DateTime.Now;
            this.TypeOfFuel = typeOfFuel;
            this.MaxPassengers = maxPassengers;
        }
    }
}
