using AutoMapper;
using GymManagmentBLL.Services.Interfaces;
using GymManagmentBLL.ViewModels;
using GymManagmentBLL.ViewModels;
using GymManagmentDAL.Entities;
using GymManagmentDAL.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace GymManagmentBLL.Services.Classes
{
    public class TrainerService : ITrainerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TrainerService(IUnitOfWork unitOfWork , IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }

        public bool CreateTrainer(CreateTrainerViewModel inputModel)
        {
            try
            {
                if (IsEmailExist(inputModel.Email))
                    return false;
                if (IsPhoneExist(inputModel.Phone))
                    return false;

                
                var trainer = new Trainer()
                {
                    Name = inputModel.Name,
                    Phone = inputModel.Phone,
                    Email = inputModel.Email,
                    DateOfBirth = inputModel.DateOfBirth,
                    Gender = inputModel.Gender,
                    Address = new Address()
                    {
                        BuildingNumber = inputModel.BuildingNumber,
                        Street = inputModel.Street,
                        City = inputModel.City,
                    },
                    Specialities = inputModel.Specialization,

                };

                _unitOfWork.GetRepository<Trainer>().Add(trainer);

                return _unitOfWork.SaveChanges() > 0;
            }
            catch
            {
                Console.WriteLine("Trainer Cannot Added In Database");
                return false;
            }
            
        }

        public IEnumerable<TrainerViewModel>? GetAllTrainers()
        {
            
            var trainers = _unitOfWork.GetRepository<Trainer>().GetAll();

            if(trainers is null || !trainers.Any())
            {
                return null;
            }

            return trainers.Select(t => new TrainerViewModel()
                    {
                        Name = t.Name,
                        Phone = t.Phone,
                        Specialities = t.Specialities.ToString(),
                        Email = t.Email,
                    });

        }

        public TrainerViewModel? GetTrainerById(int id)
        {
            
            var trainer = _unitOfWork.GetRepository<Trainer>().GetById(id);

            if(trainer is null)
                return null;

            
            var mappedTrainer = _mapper.Map<Trainer , TrainerViewModel>(trainer);

            return mappedTrainer;
        }

        public TrainerToUpdateViewModel? GetTrainerToUpdate(int id)
        {
            var trainer = _unitOfWork.GetRepository<Trainer>().GetById(id);

            if(trainer is null) return null;

            return _mapper.Map<Trainer , TrainerToUpdateViewModel>(trainer);
        }

        public bool UpdateTrainer(int Id, TrainerToUpdateViewModel inputModel)
        {
            var trainer = _unitOfWork.GetRepository<Trainer>().GetById(Id);
            if(trainer is null) return false;


            if (IsEmailExist(inputModel.Email) || IsPhoneExist(inputModel.Phone))
            {
                return false;
            }


            _mapper.Map(inputModel, trainer);

            _unitOfWork.GetRepository<Trainer>().Update(trainer);

            return _unitOfWork.SaveChanges() > 0;
        }


        public bool RemoveTrainer(int Id)
        {
            if(IsRemoveTrainerIsAvilable(Id))
                return false;

            var trainer = _unitOfWork.GetRepository<Trainer>().GetById(Id);

            if(trainer is null) return false;

            _unitOfWork.GetRepository<Trainer>().Delete(trainer);

            return _unitOfWork.SaveChanges() > 0;
        }



        #region Helper Method

        private bool IsEmailExist(string email)
        {
            var trainer = _unitOfWork.GetRepository<Trainer>().GetAll(x => x.Email == email);

            if (trainer is null || !trainer.Any())
            {
                return false;
            }
            return true;
        }
        private bool IsPhoneExist(string phone)
        {
            var trainer = _unitOfWork.GetRepository<Trainer>().GetAll(x => x.Phone == phone);

            if (trainer is null || !phone.Any())
            {
                return false;
            }
            return true;
        }

        private bool IsRemoveTrainerIsAvilable(int Id)
        {
            var trainersSessions =  _unitOfWork.GetRepository<Session>().GetAll(x => x.TrainerId == Id).ToList();

            if (trainersSessions is null || !trainersSessions.Any())
                return true;
               

            foreach (var session in trainersSessions)
            {
                if (session.StartDate >= DateTime.Now)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion

    }
}
