using GymManagmentBLL.Services.Interfaces;
using GymManagmentBLL.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GymManagmentPL.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberService _memberService;

        // Inject MemberSevice
        public MemberController(IMemberService memberService)
        {
            this._memberService = memberService;
        }

        public ActionResult Index()
        {
            var members = _memberService.GetAllMembers();

            return View(members);
        }

        public ActionResult MemberDetails(int Id)
        {
            if(Id <= 0)
            {
                TempData["ErorrMassage"] = "Id Of Member Can Not Be 0 Or Negative Number";
                return RedirectToAction(nameof(Index));

            }

            var member = _memberService.GetMemberDetails(Id);

            if(member == null)
            {
                TempData["ErorrMassage"] = "Member Not Found";
                return RedirectToAction(nameof(Index));
            }

            return View(member);

        }

        public ActionResult HealthRecordDetails(int Id)
        {
            if(Id <= 0)
            {
                TempData["ErorrMassage"] = "Id Of Member Can Not Be 0 Or Negative Number";
                return RedirectToAction(nameof(Index));

            }

            var healthRecord = _memberService.GetMemberHealthRecord(Id);

            if(healthRecord == null)
            {
                TempData["ErorrMassage"] = "Member Not Found";
                return RedirectToAction(nameof(Index));
            }


            return View(healthRecord);

        }


        public ActionResult Create()
        {

            return View();

        }

        [HttpPost]
        public ActionResult CreateMember(CreateMemberViewModel createdMember)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("DataInValid", "Check Data And Missing Fields");
                return View(nameof(Create), createdMember);
            }

            bool Result =  _memberService.CreateMember(createdMember);

            if(Result)
            {
                TempData["SeccessMessage"] = "Member Created Successfully";
            }
            else
            {
                TempData["ErrorMessage"] = "Member Failed To Create , Check Phone And Email";

            }

            return RedirectToAction(nameof(Index));
        }

        public ActionResult MemberEdit(int id)
        {
            if(id <= 0)
            {
                TempData["ErorrMassage"] = "Id Of Member Can Not Be 0 Or Negative Number";
                return RedirectToAction(nameof(Index));
            }

            var member = _memberService.GetMemberToUpdate(id);

            if(member is null)
            {
                TempData["ErorrMassage"] = "Member Not Found";
                return RedirectToAction(nameof(Index));
            }

            return View(member);
        }

        [HttpPost]
        public ActionResult MemberEdit([FromRoute] int id , MemberToUpdateViewModel memberToEdit)
        {
            if (!ModelState.IsValid)
            {
                return View(memberToEdit);
            }
            
            var Result = _memberService.UpdateMemberDetails(id, memberToEdit);
            if (Result)
            {
                TempData["SeccessMessage"] = "Member Updated Successfully";
            }
            else
            {

                TempData["ErrorMessage"] = "Member Falied To Updated";
            }

            return RedirectToAction(nameof(Index));
        }


        public ActionResult Delete(int id)
        {
            if (id <= 0)
            {
                TempData["ErorrMassage"] = "Id Of Member Can Not Be 0 Or Negative Number";
                return RedirectToAction(nameof(Index));
            }

            var Member = _memberService.GetMemberDetails(id);

            if(Member is null)
            {
                TempData["ErrorMessage"] = "Member Not Found";
                return RedirectToAction(nameof(Index));
            }
            ViewBag.MemberId = id;
            ViewBag.MemberName = Member.Name;
            return View();
        }

        [HttpPost]
        public ActionResult DeleteConfirmed(int id)
        {
            var Result = _memberService.ReomveMember(id);

            if(Result)
            {
                TempData["SeccessMessage"] = "Member Deleted Succefully";

            }
            else
            {
                TempData["ErrorMessage"] = "Member Can Not Deleted";

            }

            return RedirectToAction(nameof(Index));

        }


    }
}
