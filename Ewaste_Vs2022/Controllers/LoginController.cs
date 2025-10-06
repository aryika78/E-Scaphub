using Ewaste_Vs2022.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ewaste_Vs2022.Controllers
{
    public class LoginController : Controller
    {        
        #region default
        private readonly EwasteDbContext ewasteDb;

        private readonly IWebHostEnvironment henv;


        public LoginController(EwasteDbContext ewasteDB, IWebHostEnvironment henv)
        {
            ewasteDb = ewasteDB;
            this.henv = henv;
        }
        #endregion default     
        public IActionResult Home()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(IFormCollection frm)
        {
            var Email = Convert.ToString(frm["Pemail"]);
            var Passwd = Convert.ToString(frm["Ppassword"]);

            var RdFound = ewasteDb.PersonMasters.Where(pm => pm.Pemail == Email
            && pm.Ppassword == Passwd).FirstOrDefault();

            if (RdFound != null)
            {
                if (RdFound.Proleid == 1) //for admin
                {
                    return RedirectToAction("AdminHome", "Admin");
                }
                else if (RdFound.Proleid == 2) //for user
                {
                    return RedirectToAction("UserHome", "User", new { RdFound.Pid });
                }
                else if (RdFound.Proleid == 3) //for Transporter
                {
                    return RedirectToAction("TransporterHome", "Transporter", new { RdFound.Pid });
                }
            }
            else
            {
                TempData["ErrMsg"] = "Tempdata Invalid Email-Id or Password";
            }
            return View();
        }
        
        public IActionResult Changepassword()
        {
            return View();
        }

        public JsonResult CheckLoginJson(string Pemail, string Ppassword, string Pnpassword)
        {            

            var rdFound = ewasteDb.PersonMasters.Where(pm => pm.Pemail == Pemail && pm.Ppassword == Ppassword).FirstOrDefault();
            if (rdFound != null)
            {
                rdFound.Ppassword = Convert.ToString(Pnpassword);
                ewasteDb.Entry(rdFound).State = EntityState.Modified;                 
                ewasteDb.SaveChanges();
                return Json("Success");
            }
            else
            {
                return Json("Invalid Email-Id or Password");
            }
        }

        [HttpGet]
        public IActionResult SignupAsUser()
        {
            var qList = ewasteDb.QuestionMasters.ToList();
            ViewBag.QuestionList = qList;
            return View();
        }
        [HttpPost]
        public ActionResult SignupAsUser(IFormCollection frm)
        {
            PersonMaster tblPersonRec = new PersonMaster();
            tblPersonRec.Pname = Convert.ToString(frm["Pname"]);
            tblPersonRec.Paddress = Convert.ToString(frm["Paddress"]);
            tblPersonRec.Pdob = Convert.ToString(frm["Pdob"]);
            tblPersonRec.Pgender = Convert.ToString(frm["Pgender"]);
            tblPersonRec.Pphone = Convert.ToString(frm["Pphone"]);
            tblPersonRec.Pemail = Convert.ToString(frm["Pemail"]);
            tblPersonRec.Ppassword = Convert.ToString(frm["Ppassword"]);
            tblPersonRec.Pqid = Convert.ToInt32(frm["Pqlist"]);
            tblPersonRec.Pimage = "No image";
            tblPersonRec.Panswer = Convert.ToString(frm["Panswer"]);
            tblPersonRec.Proleid = 2;
            ewasteDb.PersonMasters.Add(tblPersonRec);
            ewasteDb.SaveChanges();
            return RedirectToAction("Login");
        }


        [HttpGet]
        public IActionResult SignupAsTransporter()
        {
            var qList = ewasteDb.QuestionMasters.ToList();
            ViewBag.QuestionList = qList;
            return View();
        }
        [HttpPost]
        public IActionResult SignupAsTransporter(PersonMaster personmaster, IFormCollection frm, IFormFile file)
        {
            personmaster.Pqid = Convert.ToInt32(frm["Pqlist"]);
            personmaster.Proleid = 3;
            string uniqueImageName = null;
            if (file != null)
            {
                string uploadimgfoldername = Path.Combine(henv.WebRootPath, "images\\TransporterImages");
                uniqueImageName = Guid.NewGuid().ToString() + "_" + file.FileName;
                string finalPath = Path.Combine(uploadimgfoldername, uniqueImageName);
                file.CopyTo(new FileStream(finalPath, FileMode.Create));
            }
            personmaster.Pimage = "images\\TransporterImages\\" + uniqueImageName;

            ewasteDb.PersonMasters.Add(personmaster);
            ewasteDb.SaveChanges();
            return RedirectToAction("Login");
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult Services()
        {
            return View();
        }

    }
}
