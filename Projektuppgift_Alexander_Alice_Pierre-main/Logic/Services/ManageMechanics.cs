using Logic.DAL;
using Logic.Entities;
using Logic.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logic.Services
{
    public class ManageMechanics
    {
        IDataAccess DataAccess = new DataAccess();

        private List<Component> qualifications = new List<Component>();

        public List<Mechanic> AllMechanics()
        {
            List<Mechanic> mechanics = DataAccess.GetData<Mechanic>(FileName.Mechanic);

            return mechanics;
        }
        
        public void SetQualifications(bool brakes, bool engine, bool carriageBody, bool tire, bool windShield)
        {
            qualifications.Clear();

           if(brakes == true)
                qualifications.Add(Component.Bromsar);          

           if(engine == true)
                qualifications.Add(Component.Motor);
            
           if(carriageBody == true)
                qualifications.Add(Component.Kaross);

           if(tire == true)
                qualifications.Add(Component.Däck);

           if(windShield == true)
                qualifications.Add(Component.Vindrutor);
        }

        public async Task AddMechanic(List<Mechanic> mechanics ,string name, DateTime birthday, DateTime employmentDate)
        {
            if (birthday > DateTime.Now || birthday < DateTime.Parse("1900-01-01"))
            {
                throw new DateOutOfReachException();
            }
            else if(employmentDate > DateTime.Now || employmentDate < DateTime.Parse("1900-01-01"))
            {
                throw new DateOutOfReachException();
            }

            Mechanic mechanic = new Mechanic();
            mechanic.Name = name;
            mechanic.BirthDay = birthday;
            mechanic.EmploymentDate = employmentDate;
            mechanic.Qualifications = qualifications;
            mechanic.IdNumber = Guid.NewGuid();

            mechanics.Add(mechanic);

            await DataAccess.SaveData<Mechanic>(mechanics, FileName.Mechanic);
        }

        public async Task AddMechanic(List<Mechanic> mechanics ,string name, DateTime birthday, DateTime employmentDate, DateTime endDate)
        {
            if (birthday > DateTime.Now || birthday < DateTime.Parse("1900-01-01"))
            {
                throw new DateOutOfReachException();
            }
            else if (employmentDate > DateTime.Now || employmentDate < DateTime.Parse("1900-01-01"))
            {
                throw new DateOutOfReachException();
            }
            else if(endDate < DateTime.Now || endDate < DateTime.Parse("1900-01-01"))
            {
                throw new DateOutOfReachException();
            }

            Mechanic mechanic = new Mechanic();
            mechanic.Name = name;
            mechanic.BirthDay = birthday;
            mechanic.EmploymentDate = employmentDate;
            mechanic.Qualifications = qualifications;
            mechanic.IdNumber = Guid.NewGuid();
            mechanic.LastDay = endDate;

            mechanics.Add(mechanic);

            await DataAccess.SaveData<Mechanic>(mechanics, FileName.Mechanic);

            qualifications.Clear();
        }

        public async Task DeleteMechanic(Mechanic choosenMechanic, List<Mechanic> mechanics)
        {
            mechanics = mechanics
                .Where(mechanic => mechanic.IdNumber != choosenMechanic.IdNumber)
                .ToList();

            await DataAccess.SaveData<Mechanic>(mechanics, FileName.Mechanic);
        }

        public async Task ChangeMechanic(string mechanicID, List<Component> qualifications, List<Mechanic> mechanics)
        {
            var mechanic = mechanics.FirstOrDefault(x => x.IdNumber.ToString() == mechanicID);

            mechanics = mechanics
              .Where(mechanic => mechanic.IdNumber.ToString() != mechanicID)
              .ToList();

            mechanic.Qualifications = qualifications;

            mechanics.Add(mechanic);

            await DataAccess.SaveData<Mechanic>(mechanics, FileName.Mechanic);
        }
    }
}
