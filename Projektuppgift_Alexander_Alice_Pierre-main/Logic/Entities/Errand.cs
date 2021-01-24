using System;
using System.Threading;

namespace Logic.Entities
{
    public enum WorkState
    {
        Obemannad,
        Pågående,
        Avslutat
    }

    public enum Component
    {
        Bromsar,
        Motor,
        Kaross,
        Vindrutor,
        Däck
    }
    public class Errand
    {
        public string Description { get; set; }
        public Guid VehicleId { get; set; }
        public Guid MechanicId { get; set; }
        public Component Component { get; set; }
        public WorkState WorkState { get; set; }
        public int ErrandId { get; set; }

        public static int nextId;

        public Errand()
        {
            ErrandId = Interlocked.Increment(ref nextId);
        }
        public override string ToString()
        {
            return $"Beskrivning: {Description}" +
                   $"\n\nStatus: {WorkState}" +
                   $"\n\nProblem: {Component}" +
                   $"\n\nMekanikerID: {MechanicId}" +
                   $"\n\nFordonID: {VehicleId}";
        }
    }
}

