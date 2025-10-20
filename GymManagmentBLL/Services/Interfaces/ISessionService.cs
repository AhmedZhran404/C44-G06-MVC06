using GymManagmentBLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Services.Interfaces
{
    public interface ISessionService
    {
        IEnumerable<SessionViewModel>? GetAllSessions();

        SessionViewModel? GetSessionById(int sessionId);

        bool CreateSession(CreateSessionViewModel input);

        bool UpdateSession(int id , UpdateSessionViewModel input);

        bool DeleteSession(int id);

        UpdateSessionViewModel? GetSessionToUpdate(int Id);

    }
}
