using Ewaste_Vs2022.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ewaste_Vs2022.Controllers
{
    public class TransporterController : Controller
    {        
        #region default
        private readonly EwasteDbContext ewasteDb;

        private readonly IWebHostEnvironment henv;


        public TransporterController(EwasteDbContext ewasteDB, IWebHostEnvironment henv)
        {
            ewasteDb = ewasteDB;
            this.henv = henv;
        }
        #endregion default        

        public IActionResult TransporterHome(int Pid)
        {
            HttpContext.Session.SetString("drvid", Pid.ToString());
            TempData["trainid"] = HttpContext.Session.GetString("drvid");
            return View();
        }

        #region TransporterComplain
        [HttpGet]
        public IActionResult TransporterComplain()
        {
            var personId = Convert.ToInt32(HttpContext.Session.GetString("drvid"));
            TempData["Pid"] = Convert.ToInt32(HttpContext.Session.GetString("drvid"));
            var personName = ewasteDb.PersonMasters.Where(q => q.Pid == personId).FirstOrDefault();
            if (personName != null)
            {
                TempData["perName"] = personName.Pname;
            }            
            return View();
        }

        [HttpPost]
        public ActionResult TransporterComplain(IFormCollection frm)
        {
            ComplainMaster complainmaster = new ComplainMaster();
            complainmaster.Pid = Convert.ToInt32(HttpContext.Session.GetString("drvid"));
            complainmaster.Cdetails = Convert.ToString(frm["Cdetails"]);
            ewasteDb.ComplainMasters.Add(complainmaster);
            ewasteDb.SaveChanges();
            return RedirectToAction("TransporterComplainPost");
        }
        public ActionResult TransporterComplainPost()
        {
            return View();
        }
        #endregion TransporterComplain

        #region TransporterFeedback

        
        [HttpGet]
        public IActionResult TransporterFeedback()
        {
            var personId = Convert.ToInt32(HttpContext.Session.GetString("drvid"));
            var personName = ewasteDb.PersonMasters.Where(q => q.Pid == personId).FirstOrDefault();
            if (personName != null)
            {
                TempData["perName"] = personName.Pname;
            }
            return View();
        }

        [HttpPost]
        public JsonResult TransporterFeedback(string comments, string rdchk)
        {
            FeedbackMaster userFdbk = new FeedbackMaster();
            var personId = Convert.ToInt32(HttpContext.Session.GetString("drvid"));
            userFdbk.Pid = personId;
            userFdbk.Feedbackdate = DateTime.Now.ToString();
            userFdbk.Feedbackdesc = comments;
            userFdbk.ExperienceRate = rdchk;
            ewasteDb.FeedbackMasters.Add(userFdbk);
            ewasteDb.SaveChanges();
            return Json("Success");
        }

        public ActionResult TransporterFeedbackPost()
        {
            return View();
        }

        #endregion TransporterFeedback
        
        public IActionResult TransporterLogout()
        {        
            return RedirectToAction("Home", "Login");
        }
    }
}
