using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Entities
{
    public class Membership : BaseEnitity
    {
        public DateTime EndDate { get; set; }

        // ef Core Ignore This Propert
        public string Status 
        {
            get
            {
                if(EndDate >= DateTime.Now)
                {
                    return "Active";
                }
                else
                {
                    return "Expired";
                }
            }
        }

        public int MemberId { get; set; }
        public Member Member { get; set; } = null!;
        public int PlanId { get; set; }
        public Plan Plan { get; set; } = null!;

    }
}
