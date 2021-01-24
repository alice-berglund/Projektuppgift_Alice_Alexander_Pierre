using Logic.DAL;
using Logic.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Logic.Services
{
    public class ManageErrands
    {
        IDataAccess dataAccess = new DataAccess();
        ManageMechanics manageMechanics = new ManageMechanics();

        public List<Errand> GetAllErrands()
        {
            List<Errand> errands = (List<Errand>)dataAccess.GetData<Errand>(FileName.Errands);

            return errands;
        }
        public List<string> GetAllErrandDescription(List<Errand> errands)
        {
            var errandDescriptions = errands.Select(x => x.Description).ToList();

            return errandDescriptions;
        }
        public List<Component> GetAllErrandProblem(List<Errand> errands)
        {
            var errandProblems = errands.Select(x => x.Component).ToList();

            return errandProblems;
        }
        public List<WorkState> GetAllErrandWorkState(List<Errand> errands)
        {
            var errandWorkStates = errands.Select(x => x.WorkState).ToList();

            return errandWorkStates;
        }

        public async Task NewErrand(List<Errand> errands, string description, Component component, Guid vehicleId)

        {
            Errand errand = new Errand();
            {
                errand.VehicleId = vehicleId;
                errand.Description = description;
                errand.Component = component;
                errand.WorkState = 0;
            }

            errands.Add(errand);
            await dataAccess.SaveData<Errand>(errands, FileName.Errands);
        }

        public async Task ChangeWorkStateErrand(Errand selected, List<Errand> Errands)
        {          
            Errand errand = selected;

            if (errand.WorkState == WorkState.Pågående)
            {
                errand.WorkState = WorkState.Avslutat;
                List<Errand> newList = Errands.Where(x => x.ErrandId != errand.ErrandId).ToList();

                newList.Add(errand);
                await dataAccess.SaveData<Errand>(newList, FileName.Errands);
            }

            else if (errand.WorkState == WorkState.Obemannad)
            {
                throw new Exception("Errand hasn't been assigned"); 
            }

        }

        public async Task ChangeWorkStateMechanic(Errand selected)
        {
            List<Mechanic> Mechanics = manageMechanics.AllMechanics();

            var mechanic = Mechanics.FirstOrDefault(x => x.IdNumber == selected.MechanicId);
            
            Mechanics = Mechanics.Where(x => x.IdNumber != selected.MechanicId).ToList();

            for (int i = 0; i < mechanic.Errands.Count; i++)
            {
                if (mechanic.Errands[i].ErrandId == selected.ErrandId)
                {
                    mechanic.Errands[i].WorkState = WorkState.Avslutat;
                    Mechanics.Add(mechanic);
                    break;
                }
            }

            await dataAccess.SaveData<Mechanic>(Mechanics, FileName.Mechanic);
        }

        public List<Mechanic> CheckAvailableMechanics(List<Mechanic> Mechanics)
        {
            int workStatePending;

            foreach (Mechanic mechanic in Mechanics.ToList())
            {
                workStatePending = 0;

                foreach (Errand errand in mechanic.Errands)
                {
                    if (errand.WorkState == WorkState.Pågående)
                    {
                        workStatePending++;

                        if (workStatePending == 2)
                        {
                            Mechanics.Remove(mechanic);
                            break;
                        }
                    }
                }
            }

                return Mechanics;
        }

        public async Task AssignMechanicToErrand(Mechanic mechanic, Errand errand)
        {
            List<Errand> Errands = GetAllErrands();
            List<Mechanic> Mechanics = manageMechanics.AllMechanics();

            Mechanics = Mechanics.Where(x => x.IdNumber != mechanic.IdNumber).ToList();

            if (errand.MechanicId != Guid.Empty)
            {
                throw new Exception("This errand is already assigned to a mechanic");
            }

            else if (errand.MechanicId == Guid.Empty)
            {
                Errands.Where(x => x.ErrandId == errand.ErrandId)
                    .ToList()
                    .ForEach(s => s.WorkState = WorkState.Pågående);

                Errands.Where(x => x.ErrandId == errand.ErrandId)
                    .ToList()
                    .ForEach(s => s.MechanicId = mechanic.IdNumber);

                errand.MechanicId = mechanic.IdNumber;
                errand.WorkState = WorkState.Pågående;

                mechanic.Errands.Add(errand);
                
                Mechanics.Add(mechanic);

                await dataAccess.SaveData<Mechanic>(Mechanics, FileName.Mechanic);

                await dataAccess.SaveData<Errand>(Errands, FileName.Errands);
            }
        }

        public async Task RemoveErrand(Errand selected, List<Errand> errands)
        {
           
            errands = errands.Where(x => x != selected).ToList();

            await dataAccess.SaveData<Errand>(errands, FileName.Errands);
        }

    }
}
