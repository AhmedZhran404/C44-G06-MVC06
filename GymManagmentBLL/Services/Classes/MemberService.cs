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
    public class MemberService : IMemberService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MemberService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public bool CreateMember(CreateMemberViewModel model)
        {
            try
            {
                if(IsExistingEmail(model.Email) && IsExistingPhone(model.Phone))
                {
                    return false;
                }
                var member = new Member
                {
                    Name = model.Name,
                    Email = model.Email,
                    Phone = model.Phone,
                    Gender = model.Gender,
                    DateOfBirth = model.DateOfBirth,
                    Address = new Address
                    {
                        BuildingNumber = model.BuildingNumber,
                        Street = model.Street,
                        City = model.City,
                    },
                    HealthRecord = new HealthRecord
                    {
                        Height = model.HealthRecordViewModel.Height,
                        Weight = model.HealthRecordViewModel.Weight,
                        BloodType = model.HealthRecordViewModel.BloodType,
                        Note = model.HealthRecordViewModel.Note,
                    }

                };

                _unitOfWork.GetRepository<Member>().Add(member);

                return _unitOfWork.SaveChanges() > 0;
            }catch
            {
                return false;
            }
        }

        public IEnumerable<MemberViewModel> GetAllMembers()
        {

            var members = _unitOfWork.GetRepository<Member>().GetAll() ?? [];

            if(members is null || !members.Any())
            {
                return [];
            }

            var memberViewModels = members.Select(x => new MemberViewModel
            {

                Id = x.Id,
                Name = x.Name,
                Phone = x.Phone,
                Email = x.Email,
                DateOfBirth = x.DateOfBirth.ToShortDateString(),
                Gender = x.Gender.ToString(),
                
            }); 

            return memberViewModels;
            
        }



        public MemberViewModel? GetMemberDetails(int MemberId)
        {
            
            var member = _unitOfWork.GetRepository<Member>().GetById(MemberId);

            if(member is null)
            {
                return null;
            }

            var memberViewModel = new MemberViewModel
            {
                Id = member.Id,
                Name = member.Name,
                Phone = member.Phone,
                Email = member.Email,
                Gender = member.Gender.ToString(),
                Photo = member.Photo,
                Address = FormatAddress(member.Address),
                DateOfBirth = member.DateOfBirth.ToShortDateString(),
            };


            var activeMembership = _unitOfWork.GetRepository<Membership>()
                                   .GetAll(x => x.MemberId == MemberId && x.Status == "Active")
                                   .FirstOrDefault();

            if(activeMembership is not null)
            {
                var activePlan = _unitOfWork.GetRepository<Plan>().GetById(activeMembership.PlanId);

                memberViewModel.PlaneName = activePlan?.Name;
                memberViewModel.MembershipStartDate = activeMembership?.CreatedAt.ToShortDateString();
                memberViewModel.MembershipStartDate = activeMembership?.EndDate.ToShortDateString();


            }


            return memberViewModel;
        }


        public HealthRecordViewModel? GetMemberHealthRecord(int MemberId)
        {
            var memberHealthRecord = _unitOfWork.GetRepository<HealthRecord>().GetById(MemberId);

            if(memberHealthRecord is null)
            {
                return null;
            }

            return new HealthRecordViewModel
            {
                Height = memberHealthRecord.Height,
                Weight = memberHealthRecord.Weight,
                BloodType = memberHealthRecord.BloodType,
                Note = memberHealthRecord.Note
            };

        }



        public bool UpdateMemberDetails(int memberId, MemberToUpdateViewModel model)
        {
           

            var emailExists = _unitOfWork.GetRepository<Member>()
                .GetAll(condition: X => X.Email == model.Email && X.Id != memberId);

            var PhoneExists = _unitOfWork.GetRepository<Member>()
                .GetAll(condition: X => X.Phone == model.Phone && X.Id != memberId);

            if (emailExists.Any() || PhoneExists.Any()) return false;

            
            var member = _unitOfWork.GetRepository<Member>().GetById(memberId);
            if (member is null) return false;

            member.Email = model.Email;
            member.Phone = model.Phone;
            member.Address.BuildingNumber = model.BuildingNumber; 
            member.Address.Street = model.Street; 
            member.Address.City = model.City;
            member.UpdatedAt = DateTime.Now;


            _unitOfWork.GetRepository<Member>().Update(member);

            return _unitOfWork.SaveChanges() > 0;

        }


        public MemberToUpdateViewModel? GetMemberToUpdate(int memberId)
        {


            var member = _unitOfWork.GetRepository<Member>().GetById(memberId);

            if (member is null)
            {
                return null;
            }

            var memberDetailsView = new MemberToUpdateViewModel
            {
                Name = member.Name,
                photo = member.Photo,
                Phone = member.Phone,
                Email = member.Email,
                BuildingNumber = member.Address.BuildingNumber,
                Street = member.Address.Street,
                City = member.Address.City,
               
            };

            return memberDetailsView;
        }



        public bool ReomveMember(int MemberId)
        {
            var member = _unitOfWork.GetRepository<Member>().GetById(MemberId);
            if (member is null)
                return false;

            var ActiveBooking = _unitOfWork.GetRepository<Booking>()
                                .GetAll(x => x.MemberId == MemberId && x.Session.StartDate > DateTime.UtcNow);

            if (ActiveBooking.Any())
                return false;

            var ActiveMemberships = _unitOfWork.GetRepository<Membership>().GetAll(x => x.MemberId == MemberId);

            try
            {
                if (ActiveMemberships.Any())
                {
                    foreach (var Membership in ActiveMemberships)
                    {
                        _unitOfWork.GetRepository<Membership>().Delete(Membership);
                    }
                }

                _unitOfWork.GetRepository<Member>().Delete(member);
                return _unitOfWork.SaveChanges() > 0;
            }
            catch 
            {
                return false;
            }



        }


        #region Helper Methods

        private bool IsExistingEmail(string Email)
        {
            var existingMember = _unitOfWork.GetRepository<Member>().GetAll(x => x.Email == Email);

            return existingMember is not null && existingMember.Any();
        }
        private bool IsExistingPhone(string Phone)
        {
            var existingMember = _unitOfWork.GetRepository<Member>().GetAll(x => x.Phone == Phone);

            return existingMember is not null && existingMember.Any();
        }


        private string FormatAddress(Address address)
        {
            if(address is null)
            {
                return "N/A";
            }

            return $"{address.BuildingNumber} , {address.Street} , {address.City}";

        }



        #endregion


    }
}
