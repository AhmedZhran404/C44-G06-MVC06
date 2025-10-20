using GymManagmentBLL.Services.Interfaces;
using GymManagmentDAL.Entities;
using Microsoft.AspNetCore.Mvc;

namespace GymManagmentPL.Controllers
{
    public class HomeController : Controller
    {
        private readonly IAnalyticsService _analyticsService;

        public HomeController(IAnalyticsService analyticsService)
        {
            this._analyticsService = analyticsService;
        }

        // ViewResult Action
        public ViewResult Index()
        {
            var analytics = _analyticsService.GetAnalyticsData();
            
            return View(analytics);

        }
        
        
        
        #region RetunTypeOfActions

        /// JsonResult Action
        ///public JsonResult Trainers()
        ///{
        ///    var Trainers = new List<Trainer>()
        ///    {
        ///        new Trainer() { Name = "Ahmed" , Phone = "01092837465" },
        ///        new Trainer() { Name = "Mohamed" , Phone = "01092837465" },
        ///        new Trainer() { Name = "Hamza" , Phone = "01092837465" },
        ///    };
        ///    return Json(Trainers);
        ///}

        /// RedirectResult Action
        ///public RedirectResult Redirect()
        ///{
        ///    return Redirect("https://learn.microsoft.com/en-us/aspnet/mvc/overview/older-versions-1/controllers-and-routing/creating-an-action-cs");
        ///}

        /// ContentResult Action
        ///public ContentResult Content()
        ///{
        ///    return Content("<h1>Hello From GymManagment System</h1>" , "text/html");
        ///}

        /// Dowenload File Action
        ///public FileResult Dowenloadfile()
        ///{
        ///    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "css", "Site.css");          
        ///    var FileBytes = System.IO.File.ReadAllBytes(filePath);
        ///    return File(FileBytes , "text/css" , "DowenloadedFile");
        ///}

        /// Empty Page 
        ///public EmptyResult EmptyResult()
        ///{
        ///    return new EmptyResult();
        ///}
        /// We Use ActionResult ==> This Parent For All DateTypes [ViewResult , JsonResult , RedirectResult , ContentResult , FileResult , ..] 
        #endregion
    }
}
 