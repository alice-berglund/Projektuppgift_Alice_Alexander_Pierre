using System;
using System.Collections.Generic;

namespace Logic.Entities
{
    public class Mechanic
    {
        public string Name { get; set; }
        public DateTime BirthDay { get; set; }
        public DateTime EmploymentDate { get; set; }
        public DateTime? LastDay { get; set; }
        public Guid IdNumber { get; set; }
        public List<Component> Qualifications { get; set; }

        private List<Errand> _Errands;
        public List<Errand> Errands
        {
            get
            {
                if (_Errands == null)
                {
                    _Errands = new List<Errand>();
                }
                return _Errands;
            }
            set
            {
                _Errands = value;
            }
        }

        public Mechanic()
        {

        }
      
        public override string ToString()
        {
            return $"Namn: {Name}" +
                   $"\nFödelsedag: {BirthDay}" +
                   $"\nAnställningsdatum: {EmploymentDate}" +
                   $"\nTotalt antal ärenden: {_Errands.Count}";
        }
    }
}
