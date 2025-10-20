using GymManagmentDAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repositories.Interfaces
{
    public interface ISessionRepository : IGenericRepository<Session>
    {
        IEnumerable<Session> GetAllSessionsWithAllTrainerAndCategory();
        Session? GetSessionsWithTrainerAndCategory(int sessionId);

        int GetCountBookedSlots(int sessionId);
    }
}
