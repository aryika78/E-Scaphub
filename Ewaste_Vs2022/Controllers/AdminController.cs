using Ewaste_Vs2022.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ewaste_Vs2022.Controllers
{
    public class AdminController : Controller
    {
        
        #region default
        private readonly EwasteDbContext ewasteDb;

        private readonly IWebHostEnvironment henv;


        public AdminController(EwasteDbContext ewasteDB, IWebHostEnvironment henv)
        {
            ewasteDb = ewasteDB;
            this.henv = henv;
        }
        #endregion default
        
        public IActionResult AdminHome()
        {
            return View();
        }
        
        #region AreaDetails
        public IActionResult AdminAreaDetails()
        {
            var areaList = ewasteDb.AreaMasters.ToList();
            return View(areaList);            
        }


        [HttpGet]
        public IActionResult AdminAddorEditArea(int Aid = 0)
        {
            if (Aid == 0)
            {
                return View(new AreaMaster());
            }
            else
            {
                return View(ewasteDb.AreaMasters.Where(am => am.Aid == Aid).FirstOrDefault<AreaMaster>());
            }
        }

        [HttpPost]
        public IActionResult AdminAddorEditArea(AreaMaster areamaster)
        {
            if (areamaster.Aid == 0)
            {
                var areaExists = ewasteDb.AreaMasters.Where(am => am.Areaname == areamaster.Areaname.Trim()).FirstOrDefault();
                if (areaExists == null)
                {
                    ewasteDb.AreaMasters.Add(areamaster);
                    ewasteDb.SaveChanges();
                }
                else
                {
                    TempData["areaname"] = areamaster.Areaname;
                    TempData["areaexists"] = "Area Already Exists...";
                    return View(new AreaMaster());
                }
            }
            else
            {
                var areaModify = ewasteDb.AreaMasters.Where(am => am.Aid == areamaster.Aid).FirstOrDefault();
                if (areaModify != null)
                {
                    areaModify.Areaname = areamaster.Areaname;
                    ewasteDb.Entry(areaModify).State = EntityState.Modified;
                    ewasteDb.SaveChanges();
                }
            }
            return RedirectToAction("AdminAreaDetails");            
        }

        public IActionResult AdminDeleteArea(int Aid)
        {
            var delArea = ewasteDb.AreaMasters.Where(q => q.Aid == Aid).FirstOrDefault();
            if (delArea != null)
            {
                ewasteDb.Entry(delArea).State = EntityState.Deleted;
                ewasteDb.SaveChanges();                
            }
            return RedirectToAction("AdminAreaDetails");
        }

        #endregion AreaDetails

        #region MainCategory
        public IActionResult AdminViewMainCategory()
        {
            var mainCatList = ewasteDb.ProductCategoryMasters.ToList();
            return View(mainCatList);
        }

        [HttpGet]
        public IActionResult AdminAddorEditMainCategory(int Catid)
        {
            if (Catid == 0)
            {
                TempData["btn"] = "Add";
                return View(new ProductCategoryMaster());
            }
            else
            {
                TempData["btn"] = "Edit";
                var mainCatRec = ewasteDb.ProductCategoryMasters.Where(cat => cat.Catid == Catid).FirstOrDefault();
                return View(mainCatRec);
            }
        }

        [HttpPost]
        public IActionResult AdminAddorEditMainCategory(ProductCategoryMaster productcategorymaster, IFormFile Catimage)
        {
            string uniqueImageName;
            if (Catimage != null)
            {
                string uploadimgfoldername = Path.Combine(henv.WebRootPath, "images\\MainCategory");
                uniqueImageName = Guid.NewGuid().ToString() + "_" + Catimage.FileName;
                string finalPath = Path.Combine(uploadimgfoldername, uniqueImageName);
                Catimage.CopyTo(new FileStream(finalPath, FileMode.Create));
                productcategorymaster.Catimage = "images\\MainCategory\\" + uniqueImageName;
            }
            else
            {
                var imgdetails = ewasteDb.ProductCategoryMasters.Where(q => q.Catid == productcategorymaster.Catid).FirstOrDefault();
                if (imgdetails != null)
                {
                    productcategorymaster.Catimage = imgdetails.Catimage;
                    ewasteDb.Entry(imgdetails).State = EntityState.Detached; //comment it and run then you will understand
                }                
            }


            if (productcategorymaster.Catid == 0)
            {
                ewasteDb.ProductCategoryMasters.Add(productcategorymaster);
                ewasteDb.SaveChanges();
                return RedirectToAction("AdminViewMainCategory");
            }
            else
            {
                ewasteDb.Entry(productcategorymaster).State = EntityState.Modified;
                ewasteDb.SaveChanges();
                return RedirectToAction("AdminViewMainCategory");
            }
        }

        public IActionResult AdminDeleteMainCategory(int Catid)
        {
            var delMainCategory = ewasteDb.ProductCategoryMasters.Where(c => c.Catid == Catid).FirstOrDefault();
            if (delMainCategory != null)
            {
                ewasteDb.Entry(delMainCategory).State = EntityState.Deleted;
                ewasteDb.SaveChanges();
            }
            
            return RedirectToAction("AdminViewMainCategory");
        }

        #endregion MainCategory


        #region SubCategory
        public IActionResult AdminViewSubcategory(int CatId)
        {
            var catRec = ewasteDb.ProductCategoryMasters.Where(c => c.Catid == CatId).FirstOrDefault();
            if (catRec != null)
            {
                TempData["catname"] = catRec.Catname;
            }            
            HttpContext.Session.SetString("currentmainCat", CatId.ToString());
            var scList = ewasteDb.ProductSubCategories.Where(sc => sc.Catid == CatId).ToList();
            return View(scList);
        }

        [HttpGet]
        public IActionResult AdminAddorEditSubCategory(int Scid)
        {
            if (Scid == 0)
            {
                TempData["btn"] = "Add";
                return View(new ProductSubCategory());
            }
            else
            {
                TempData["btn"] = "Edit";
                var scRec = ewasteDb.ProductSubCategories.Where(sc => sc.SCid == Scid).FirstOrDefault();
                return View(scRec);
            }
        }

        [HttpPost]
        public IActionResult AdminAddorEditSubCategory(ProductSubCategory productsubcategory, IFormFile SCimage)
        {
            var curcatId = Convert.ToInt32(HttpContext.Session.GetString("currentmainCat"));
            string uniqueImageName;
            if (SCimage != null)
            {
                string uploadimgfoldername = Path.Combine(henv.WebRootPath, "images\\SubCategory");
                uniqueImageName = Guid.NewGuid().ToString() + "_" + SCimage.FileName;
                string finalPath = Path.Combine(uploadimgfoldername, uniqueImageName);
                SCimage.CopyTo(new FileStream(finalPath, FileMode.Create));
                productsubcategory.SCimage = "images\\SubCategory\\" + uniqueImageName;
            }
            else
            {
                var imgdetails = ewasteDb.ProductSubCategories.Where(q => q.SCid == productsubcategory.SCid).FirstOrDefault();
                if (imgdetails != null)
                {
                    productsubcategory.SCimage = imgdetails.SCimage;
                    ewasteDb.Entry(imgdetails).State = EntityState.Detached; //comment it and run then you will understand
                }                
            }


            if (productsubcategory.SCid == 0)
            {
                productsubcategory.Catid = Convert.ToInt32(curcatId);
                ewasteDb.ProductSubCategories.Add(productsubcategory);
                ewasteDb.SaveChanges();
                return RedirectToAction("AdminViewSubcategory", new { Catid = Convert.ToInt32(curcatId) });
            }
            else
            {
                ewasteDb.Entry(productsubcategory).State = EntityState.Modified;
                ewasteDb.SaveChanges();
                return RedirectToAction("AdminViewSubcategory", new { Catid = Convert.ToInt32(curcatId) });
            }
        }

        public IActionResult AdminDeleteSubCategory(int Scid)
        {
            var curcatId = Convert.ToInt32(HttpContext.Session.GetString("currentmainCat"));
            var delSubCategory = ewasteDb.ProductSubCategories.Where(c => c.SCid == Scid).FirstOrDefault();
            if (delSubCategory != null)
            {
                ewasteDb.Entry(delSubCategory).State = EntityState.Deleted;
                ewasteDb.SaveChanges();
            }            
            return RedirectToAction("AdminViewSubcategory", new { Catid = Convert.ToInt32(curcatId) });
        }

        #endregion SubCategory

        #region Details
        public IActionResult AdminViewUserDetails()
        {
            var userList = ewasteDb.PersonMasters.Where(q => q.Proleid == 2).ToList();
            return View(userList);
        }

        public IActionResult AdminViewTransporterDetails()
        {
            var transporterList = ewasteDb.PersonMasters.Where(q => q.Proleid == 3).ToList();
            return View(transporterList);
        }

        public IActionResult AdminViewComplainDetails()
        {
            var complainList = ewasteDb.ComplainMasters.ToList();
            var personList = ewasteDb.PersonMasters.Where(pm => pm.Proleid == 2).ToList();
            ViewBag.PersonList = personList;
            return View(complainList);
        }

        
        public IActionResult AdminViewFeedbackDetails()
        {
            var feedbackList = ewasteDb.FeedbackMasters.ToList();
            var personList = ewasteDb.PersonMasters.Where(pm => pm.Proleid == 2).ToList();
            ViewBag.PersonList = personList;
            return View(feedbackList);
        }

        public IActionResult AdminViewTransporterComplain()
        {
            var complainList = ewasteDb.ComplainMasters.ToList();
            var personList = ewasteDb.PersonMasters.Where(pm => pm.Proleid == 3).ToList();
            ViewBag.PersonList = personList;
            return View(complainList);
        }


        public IActionResult AdminViewTransporterFeedback()
        {
            var feedbackList = ewasteDb.FeedbackMasters.ToList();
            var personList = ewasteDb.PersonMasters.Where(pm => pm.Proleid == 3).ToList();
            ViewBag.PersonList = personList;
            return View(feedbackList);
        }

        public IActionResult AdminViewOrderDetails()
        {
            var sellordmst = ewasteDb.OrderMasters.ToList();
            return View(sellordmst);
        }

        //following method may not be used...
        public JsonResult AdminGetUserDetails(string Pid)
        {
            TempData["imgPath"] = null;
            var pid = Convert.ToInt32(Pid.ToString().Trim());
            var tblUserRec = ewasteDb.PersonMasters.Where(q => q.Pid == pid).FirstOrDefault();
            if (tblUserRec != null)
            {
                TempData["imgName"] = tblUserRec.Pimage;
            }            
            var pname = tblUserRec.Pname;
            var igPath = tblUserRec.Pimage;
            var paddr = tblUserRec.Paddress;
            var pemail = tblUserRec.Pemail;
            var pphone = tblUserRec.Pphone;
            var result = new { pId = pid, pName = pname, pImage = igPath, pAddr = paddr, pEmail = pemail, pPhone = pphone };
            return Json(result);
        }
        #endregion Details

        public IActionResult AdminLogout()
        {         
            return RedirectToAction("Home", "Login");
        }
    }
}
