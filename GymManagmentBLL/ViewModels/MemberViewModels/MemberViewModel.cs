using GymManagmentDAL.Entities;
using GymManagmentDAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.ViewModels
{
    public class MemberViewModel
    {

        public int Id { get; set; }

        public string? Photo { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!;

        public string DateOfBirth { get; set; } = null!;

        public string Gender { get; set; } = null!;

        public string Address { get; set; } = null!;

        public string?  PlaneName { get; set; }

        public string? MembershipStartDate { get; set; }

        public string? MembershipEndDate { get; set; }
    }
}
