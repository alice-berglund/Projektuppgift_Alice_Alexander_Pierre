using System;

namespace Logic.Entities
{
    public enum Fuel
    {
        El,
        Bensin,
        Diesel,
        Etanol,
    }

    public enum VehiclesAsEnum
    {
        Bil,
        Buss,
        Lastbil,
        Motorcykel,
    }

    public abstract class Vehicle
    {
        public string ModelName { get; set; }
        public string RegNumber { get; set; }
        public int Odometer { get; set; }
        public DateTime RegDate { get; set; }
        public Fuel TypeOfFuel { get; set; }
        public Guid IdNumber { get; set; } = Guid.NewGuid();

        public override string ToString()
        {
            return $"Modellnamn: {ModelName}" +
                   $"Regnummer: {RegNumber}" +
                   $"Milmätare: {Odometer}" +
                   $"Idnummer: {IdNumber}";
        }
    }
}
