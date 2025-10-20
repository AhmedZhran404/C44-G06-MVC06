using GymManagmentDAL.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Entities
{
    // Table["Member"] && [Owned]
    [Owned]
    public class Address
    {
        public int BuildingNumber { get; set; }
        public string City { get; set; } = null!;
        public string Street { get; set; } = null!;
    }
    

    public class Member : GymUser
    {
       
        public string? Photo { get; set; }

        public HealthRecord HealthRecord { get; set; } = null!;

        public ICollection<Membership> MemberPlans { get; set; } = null!;

        public ICollection<Booking> MemberSessions { get; set; } = null!;

    }
}
