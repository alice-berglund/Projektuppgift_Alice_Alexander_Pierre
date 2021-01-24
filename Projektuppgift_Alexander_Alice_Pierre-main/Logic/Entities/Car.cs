using System;

namespace Logic.Entities
{
    public enum TypeOfCar
    {
        Sedan,
        Herrgårdsvagn,
        Cabriolet,
        Halvkombi
    }
    public class Car : Vehicle
    {
        public bool Towbar { get; set; }
        public TypeOfCar TypeOfCar { get; set; }

        public Car()
        {

        }
        public Car(string modelName, string regNumber, int odoMeter, DateTime regDate, Fuel typeOfFuel, bool towBar, TypeOfCar typeOfCar)
        {
            this.ModelName = modelName;
            this.RegNumber = regNumber;
            this.Odometer = odoMeter;
            this.RegDate = DateTime.Now;
            this.TypeOfFuel = typeOfFuel;
            this.Towbar = towBar;
            this.TypeOfCar = typeOfCar;
            
        }
    }

}
