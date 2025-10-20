using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Entities
{
    public class Plan : BaseEnitity
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public int DurationDays { get; set; }

        public decimal Price { get; set; }

        public bool IsActive { get; set; }

        public ICollection<Membership> PlanMembers { get; set; }

    }
}
