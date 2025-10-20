using GymManagmentBLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Services.Interfaces
{
    public interface IPlanService
    {
        bool UpdatePlan(int Id, UpdatePlanViewModel input);


        UpdatePlanViewModel? GetPlanToUpdate(int planId);


        IEnumerable<PlanViewModel>? GetPlans();

        PlanViewModel? GetPlanById(int id);

        bool Activate(int planId);

    }
}