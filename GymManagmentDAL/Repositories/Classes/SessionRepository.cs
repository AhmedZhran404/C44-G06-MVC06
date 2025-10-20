using GymManagmentDAL.Data.GymDBContext;
using GymManagmentDAL.Entities;
using GymManagmentDAL.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentDAL.Repositories.Classes
{
    public class SessionRepository : GenericRepository<Session>, ISessionRepository
    {
        private readonly GymDbContext context;

        public SessionRepository(GymDbContext context) : base(context) 
        {
            this.context = context;
        }
        public IEnumerable<Session> GetAllSessionsWithAllTrainerAndCategory()
        {
            return context.Sessions
                          .Include(s => s.Trainer)
                          .Include(s => s.Category)
                          .ToList();
        }

        public int GetCountBookedSlots(int sessionId)
        {
            return context.Bookings.Where(x => x.SessionId == sessionId).Count();
        }

        public Session? GetSessionsWithTrainerAndCategory(int sessionId)
        {
            return context.Sessions
                          .Include(s => s.Trainer)
                          .Include(s => s.Category)
                          .FirstOrDefault(x => x.Id == sessionId);
        }
    }
}
