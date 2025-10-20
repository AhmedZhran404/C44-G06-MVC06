using GymManagmentBLL.ViewModels;
using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Services.Interfaces
{
    public interface ITrainerService
    {
        IEnumerable<TrainerViewModel>? GetAllTrainers();

        bool CreateTrainer(CreateTrainerViewModel inputModel);

        TrainerViewModel? GetTrainerById(int id);

        TrainerToUpdateViewModel? GetTrainerToUpdate(int id);

        bool UpdateTrainer(int Id , TrainerToUpdateViewModel inputModel);
        
        bool RemoveTrainer(int Id);
    }
}
