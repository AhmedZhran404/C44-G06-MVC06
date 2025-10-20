using GymManagmentBLL.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymManagmentBLL.Services.Interfaces
{
    public interface IMemberService
    {
        IEnumerable<MemberViewModel> GetAllMembers();

        bool CreateMember(CreateMemberViewModel model);

        MemberViewModel? GetMemberDetails(int MemberId);

        HealthRecordViewModel? GetMemberHealthRecord(int MemberId);

        bool UpdateMemberDetails(int memberId , MemberToUpdateViewModel model);

        MemberToUpdateViewModel? GetMemberToUpdate(int MemberId);


        bool ReomveMember(int MemberId);


    }
}
