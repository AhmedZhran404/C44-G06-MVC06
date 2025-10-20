using AutoMapper;
using GymManagmentBLL.Services.Interfaces;
using GymManagmentBLL.ViewModels;
using GymManagmentDAL.Entities;
using GymManagmentDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Services.Classes
{
    public class SessionService : ISessionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper mapper;

        public SessionService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public bool CreateSession(CreateSessionViewModel input)
        {
            if (!IsTrainerExist(input.TrainerId))
                return false;
            if(!IsCategoryExist(input.CategoryId))
                return false;

            if(!IsValidDateRange(input.StartDate, input.EndDate)) 
                return false;
            

            var session = mapper.Map<CreateSessionViewModel , Session>(input);

            _unitOfWork.GetRepository<Session>().Add(session);

            return _unitOfWork.SaveChanges() > 0;
            
        }

        public IEnumerable<SessionViewModel>? GetAllSessions()
        {
            var sessions = _unitOfWork.SessionRepository.GetAllSessionsWithAllTrainerAndCategory().OrderBy(x => x.StartDate);
            
            if(sessions is null || !sessions.Any())
            {
                return [];
            }

            var mappedSession = mapper.Map<IEnumerable<Session> , IEnumerable<SessionViewModel>>(sessions);
            
            foreach(var session in mappedSession)
            {
                session.AvailableSlots = session.Capacity - _unitOfWork.SessionRepository.GetCountBookedSlots(session.Id);
            }
            return mappedSession;
        }

        public SessionViewModel? GetSessionById(int sessionId)
        {

            var session = _unitOfWork.SessionRepository.GetSessionsWithTrainerAndCategory(sessionId);

            if (session is null)
            {
                return null;
            }

            var mappedSession = mapper.Map<Session, SessionViewModel>(session);

            mappedSession.AvailableSlots = session.Capacity - _unitOfWork.SessionRepository.GetCountBookedSlots(session.Id);

            return mappedSession;

        }

        public bool UpdateSession(int id, UpdateSessionViewModel input)
        {

            var session = _unitOfWork.GetRepository<Session>().GetById(id);
            
            if(IsSessionAvaliableForUpdate(session))
                return false;

            if (!IsTrainerExist(input.TrainerId))
                return false;

            if (!IsValidDateRange(input.StartDate, input.EndDate))
                return false;

            mapper.Map(input , session);

            session.UpdatedAt = DateTime.Now;

            _unitOfWork.GetRepository<Session>().Update(session);

            return _unitOfWork.SaveChanges() > 0;
        }

        public bool DeleteSession(int id)
        {
            var session = _unitOfWork.GetRepository<Session>().GetById(id);

            if(!IsSessionAvaliableForRemove(session)) 
                return false;

            _unitOfWork.GetRepository<Session>().Delete(session);
         
            
            return _unitOfWork.SaveChanges() > 0;
        }

        public UpdateSessionViewModel? GetSessionToUpdate(int Id)
        {
            var session = _unitOfWork.GetRepository<Session>().GetById(Id);

            if (session is null) return null;

            return mapper.Map<UpdateSessionViewModel>(session);
        }

        #region Helper Method

        private bool IsTrainerExist(int trainerId)
        {
            var trainer = _unitOfWork.GetRepository<Trainer>().GetById(trainerId);

            return trainer is null ? false : true;
        }

        private bool IsCategoryExist(int categoryId)
        {
            var category = _unitOfWork.GetRepository<Category>().GetById(categoryId);

            return category is null ? false : true;
        }

        private bool IsValidDateRange(DateTime startData , DateTime endData)
        {

            return startData < endData && startData > DateTime.UtcNow;
        }

        private bool IsSessionAvaliableForUpdate(Session session)
        {
            if(session is null)
                return false;

            if (session.EndDate < DateTime.Now)
                return false;

            if (session.StartDate < DateTime.Now)
                return false;

            var hasActiveBookings = _unitOfWork.SessionRepository.GetCountBookedSlots(session.Id) > 0;

            if(hasActiveBookings) 
                return false;

            return true;
        }
        private bool IsSessionAvaliableForRemove(Session session)
        {
            if(session is null)
                return false;

            if (session.StartDate > DateTime.UtcNow)
                return false;

            if (session.StartDate <= DateTime.UtcNow && session.EndDate > DateTime.Now)
                return false;

            var hasActiveBookings = _unitOfWork.SessionRepository.GetCountBookedSlots(session.Id) > 0;

            if(hasActiveBookings) 
                return false;

            return true;
        }

        #endregion
    }
}
