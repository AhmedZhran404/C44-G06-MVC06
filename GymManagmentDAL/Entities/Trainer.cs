using GymManagmentDAL.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Entities
{
    public class Trainer : GymUser
    {
      
        public Specialities Specialities { get; set; }

        public ICollection<Session> Sessions { get; set; } = null!;
    }
}
