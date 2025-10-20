using GymManagmentBLL.Services.Interfaces;
using GymManagmentBLL.ViewModels;
using GymManagmentDAL.Entities;
using GymManagmentDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Services.Classes
{
    public class PlanService : IPlanService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PlanService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public bool Activate(int planId)
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetById(planId);

            if (plan is null || HasActiveMembership(planId))
                return false;

            plan.IsActive = !plan.IsActive;
            plan.UpdatedAt = DateTime.UtcNow;
            _unitOfWork.GetRepository<Plan>().Update(plan);

            return _unitOfWork.SaveChanges() > 0;

        }

        public PlanViewModel? GetPlanById(int id)
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetById(id);

            if (plan is null)
                return null;

            return new PlanViewModel
            {
                Id = plan.Id,
                Name = plan.Name,
                Description = plan.Description,
                Price = plan.Price,
                IsActive = plan.IsActive,
            };
        }

        public IEnumerable<PlanViewModel>? GetPlans()
        {
            var plans = _unitOfWork.GetRepository<Plan>().GetAll();

            if (plans is null || !plans.Any()) 
                return [];

            return plans.Select(p => new PlanViewModel
            {

                Id = p.Id,
                Name = p.Name,
                Description = p.Description,
                Price = p.Price,
                IsActive = p.IsActive,

            });
        }

        public UpdatePlanViewModel? GetPlanToUpdate(int planId)
        {
            var plan = _unitOfWork.GetRepository<Plan>().GetById(planId);

            if (plan is null || plan.IsActive == false)
                return null;

            return new UpdatePlanViewModel
            {
                PlanName = plan.Name,
                Description = plan.Description,
                DurationDays = plan.DurationDays,
                Price = plan.Price,
            };

        }

        public bool UpdatePlan(int Id, UpdatePlanViewModel input)
        {

            try
            {
                var plan = _unitOfWork.GetRepository<Plan>().GetById(Id);

                if (plan is null || HasActiveMembership(Id))
                    return false;

                plan.Description = input.Description;
                plan.Price = input.Price;
                plan.Name = input.PlanName;
                plan.DurationDays = input.DurationDays;
        
                // (plan.DurationDays , plan.Name , plan.Description , plan.Price) = (input.DurationDays , input.PlanName , input.Description , plan.Price)

                _unitOfWork.GetRepository<Plan>().Update(plan);

                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }

        }

        #region Helper Methods

        private bool HasActiveMembership(int planId)
        {
            return _unitOfWork.GetRepository<Membership>()
                              .GetAll(m => m.PlanId == planId && m.Status == "Active").Any();
        }


        #endregion
    }
}
