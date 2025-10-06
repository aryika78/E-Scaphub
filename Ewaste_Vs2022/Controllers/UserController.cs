using Ewaste_Vs2022.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Ewaste_Vs2022.Controllers
{
    public class UserController : Controller
    {     
        #region default
        private readonly EwasteDbContext ewasteDb;

        private readonly IWebHostEnvironment henv;


        public UserController(EwasteDbContext ewasteDB, IWebHostEnvironment henv)
        {
            ewasteDb = ewasteDB;
            this.henv = henv;
        }
        #endregion default     

        public IActionResult UserHome(int Pid)
        {
            HttpContext.Session.SetString("PerId", Convert.ToString(Pid));
            TempData["Pid"] = Convert.ToInt32(HttpContext.Session.GetString("PerId"));
            return View();
        }


        #region UserComplain
        [HttpGet]
        public IActionResult UserComplain()
        {
            var personId = Convert.ToInt32(HttpContext.Session.GetString("PerId"));
            TempData["Pid"] = Convert.ToInt32(HttpContext.Session.GetString("PerId"));
            var personName = ewasteDb.PersonMasters.Where(q => q.Pid == personId).FirstOrDefault();
            if (personName != null)
            {
                TempData["perName"] = personName.Pname;
            }            
            return View();
        }

        [HttpPost]
        public ActionResult UserComplain(IFormCollection frm)
        {
            ComplainMaster complainmaster = new ComplainMaster();
            complainmaster.Pid = Convert.ToInt32(HttpContext.Session.GetString("PerId"));
            complainmaster.Cdetails = Convert.ToString(frm["Cdetails"]);
            ewasteDb.ComplainMasters.Add(complainmaster);
            ewasteDb.SaveChanges();
            return RedirectToAction("UserComplainPost");
        }

        public ActionResult UserComplainPost()
        {
            return View();
        }
        #endregion UserComplain

        #region UserFeedback
        [HttpGet]
        public IActionResult UserFeedback()
        {
            var personId = Convert.ToInt32(HttpContext.Session.GetString("PerId"));
            var personName = ewasteDb.PersonMasters.Where(q => q.Pid == personId).FirstOrDefault();
            if (personName != null)
            {
                TempData["perName"] = personName.Pname;
            }            
            return View();
        }

        [HttpPost]
        public JsonResult UserFeedback(string comments, string rdchk)
        {
            FeedbackMaster userFdbk = new FeedbackMaster();
            var personId = Convert.ToInt32(HttpContext.Session.GetString("PerId"));
            userFdbk.Pid = personId;
            userFdbk.Feedbackdate = DateTime.Now.ToString();
            userFdbk.Feedbackdesc = comments;
            userFdbk.ExperienceRate = rdchk;
            ewasteDb.FeedbackMasters.Add(userFdbk);
            ewasteDb.SaveChanges();
            return Json("Success");
        }

        public ActionResult UserFeedbackPost()
        {
            return View();
        }

        #endregion UserFeedback

        #region SellEwaste
        public IActionResult UserViewMainCategory()
        {
            var maincatList = ewasteDb.ProductCategoryMasters.ToList();
            TempData["usrId"] = HttpContext.Session.GetString("PerId");
            return View(maincatList);
        }   

        public IActionResult UserSubCateogryDetail(int CatId)
        {
            var scList = ewasteDb.ProductSubCategories.Where(sc => sc.Catid == CatId).ToList();
            TempData["usrId"] = HttpContext.Session.GetString("PerId");
            var maincatRecord = ewasteDb.ProductCategoryMasters.Where(cat => cat.Catid == CatId).FirstOrDefault();
            if (maincatRecord!=null)
            {
                TempData["catName"] = maincatRecord.Catname;
            }            
            return View(scList);
        }

        public IActionResult UserSCItemAddtocart(int SCid)
        {
            var scRec = ewasteDb.ProductSubCategories.Where(sc => sc.SCid == SCid).FirstOrDefault();
            var maincatRec = ewasteDb.ProductCategoryMasters.Where(cat => cat.Catid == scRec.Catid).FirstOrDefault();
            if (maincatRec!=null)
            {
                TempData["catName"] = maincatRec.Catname;
            }            
            TempData["usrId"] = HttpContext.Session.GetString("PerId");
            return View(scRec);
        }

        public IActionResult UserSCItemAddtocartfinal(IFormCollection frm)
        {
            var scid = Convert.ToInt32(frm["SCid"]);
            var CtId = ewasteDb.ProductSubCategories.Where(q => q.SCid == scid).FirstOrDefault();
            CartMaster tblCartmaster = new CartMaster();
            tblCartmaster.Pid = Convert.ToInt32(HttpContext.Session.GetString("PerId"));
            tblCartmaster.SCid = Convert.ToInt32(frm["SCid"]);
            tblCartmaster.SCQty = Convert.ToInt32(frm["txtQty"]);
            ewasteDb.CartMasters.Add(tblCartmaster);
            ewasteDb.SaveChanges();            
            return RedirectToAction("UserSubCateogryDetail", new { CatId = CtId.Catid });
        }

        public IActionResult ViewSellingCartDetails()
        {
            var personId = Convert.ToInt32(HttpContext.Session.GetString("PerId"));
            var cartdetail = ewasteDb.CartMasters.Where(q => q.Pid == personId).ToList();
            var categorydetails = ewasteDb.ProductCategoryMasters.ToList();
            var itemdetails = ewasteDb.ProductSubCategories.ToList();
            ViewBag.catdetails = categorydetails;
            ViewBag.itmdetails = itemdetails;
            TempData["usrId"] = HttpContext.Session.GetString("PerId");
            return View(cartdetail);
        }

        [HttpGet]
        public IActionResult UserEditCartDetails(int Cartid)
        {
            var cartFound = ewasteDb.CartMasters.Where(cm => cm.Cartid == Cartid).FirstOrDefault();
            return View(cartFound);
        }

        [HttpPost]
        public IActionResult UserEditCartDetails(IFormCollection frm)
        {
            var crtid = Convert.ToInt32(frm["Cartid"]);
            var editCartQty = ewasteDb.CartMasters.Where(q => q.Cartid == crtid).FirstOrDefault();
            if (editCartQty != null)
            {
                editCartQty.SCQty = Convert.ToInt32(frm["SCQty"]);
            }            
            ewasteDb.Entry(editCartQty).State = EntityState.Modified;
            ewasteDb.SaveChanges();
            return RedirectToAction("ViewSellingCartDetails");
        }

        public IActionResult UserDeleteCartDetails(int Cartid)
        {
            var delCartdetails = ewasteDb.CartMasters.Where(q => q.Cartid == Cartid).FirstOrDefault();
            ewasteDb.Entry(delCartdetails).State = EntityState.Deleted;
            ewasteDb.SaveChanges();
            return RedirectToAction("ViewSellingCartDetails");
        }

        //following method may not be used...
        public JsonResult UserGetSCQty(string Cartid)
        {
            //TempData["imgPath"] = null;
            var cartId = Convert.ToInt32(Cartid);
            var tblcartRec = ewasteDb.CartMasters.Where(q => q.Cartid == cartId).FirstOrDefault();
            //TempData["imgName"] = tblcategoryRec.Catimage;
            var scqty = tblcartRec.SCQty;
            //var igPath = tblcategoryRec.Catimage;
            var result = new { ctId = cartId, qty = scqty };
            return Json(result);
        }

        [HttpGet]
        public ActionResult UserSellingFinalizeArea()
        {
            var areaList = ewasteDb.AreaMasters.ToList();
            return View(areaList);
        }

        [HttpPost]
        public ActionResult UserSellingFinalizeArea(IFormCollection frm)
        {
            HttpContext.Session.SetString("areaname", Convert.ToString(frm["Arealist"].ToString()));
            return RedirectToAction("UserSellingPaymentMode");
        }

        [HttpGet]
        public ActionResult UserSellingPaymentMode()
        {
            return View();
        }

        [HttpPost]
        public ActionResult UserSellingPaymentMode(IFormCollection frm)
        {
            if (Convert.ToString(frm["Method"]) == "online")
            {
                HttpContext.Session.SetString("PaymentMode", "Online");
            }
            if (Convert.ToString(frm["Method"]) == "cash")
            {
                HttpContext.Session.SetString("PaymentMode", "COD");
            }
            return RedirectToAction("UserInvoiceDetails");
        }

        [HttpGet]
        public IActionResult UserInvoiceDetails()
        {
            TempData["SQdate"] = DateTime.Now;
            var perId = Convert.ToInt32(HttpContext.Session.GetString("PerId"));
            var pDetails = ewasteDb.PersonMasters.Where(q => q.Pid == perId).FirstOrDefault();
            if (pDetails != null)
            {
                TempData["PersonName"] = pDetails.Pname;
                TempData["PersonAddr"] = pDetails.Paddress;
                TempData["PersonEmail"] = pDetails.Pemail;
            }                       
            TempData["date"] = Convert.ToString(DateTime.Now.Day) + " - " + Convert.ToString(DateTime.Now.Month) + " - " + Convert.ToString(DateTime.Now.Year);
            int orderId;
            if (ewasteDb.OrderMasters.Count() > 0)
            {
                var maxOrdId = ewasteDb.OrderMasters.ToList().OrderByDescending(q => q.Ordid).First();
                orderId = maxOrdId.Ordid;
            }
            else
            {
                orderId = 1;
            }
            var ordId = Convert.ToInt32(orderId);
            if (ordId == 0)
            {
                ordId = Convert.ToInt32(0);
            }
            else
            {
                ordId = ordId + 1;
            }
            TempData["ordId"] = ordId;
            var cartList = ewasteDb.CartMasters.Where(q => q.Pid == perId).ToList();
            ViewBag.cartList = cartList;
            var productSCList = ewasteDb.ProductSubCategories.ToList();
            ViewBag.productSCList = productSCList;

            return View();
        }


        public ActionResult UserInvoiceDetailsSubmit(decimal Total, decimal GrandTotal)
        {            
            OrderMaster ordermaster = new OrderMaster();
            ordermaster.Orddate = DateTime.Now;
            ordermaster.Pid = Convert.ToInt32(HttpContext.Session.GetString("PerId"));
            var areaRec = ewasteDb.AreaMasters.Where(am => am.Areaname == HttpContext.Session.GetString("areaname").Trim()).FirstOrDefault();
            ordermaster.Areaid = areaRec.Aid;
            ordermaster.Ordstatus = "Pending";
            ordermaster.Ordtotal = Total;
            ordermaster.Ordgrandtotal = GrandTotal;
            ordermaster.Ordpaymentmode = Convert.ToString(HttpContext.Session.GetString("PaymentMode"));
            ewasteDb.OrderMasters.Add(ordermaster);
            ewasteDb.SaveChanges();
            //Empting current cart from cartmaster
            var removecurrentCart = ewasteDb.CartMasters.Where(cm => cm.Pid == ordermaster.Pid).ToList();
            ewasteDb.CartMasters.RemoveRange(removecurrentCart); //to remove more than one record...            
            ewasteDb.SaveChanges();

            return RedirectToAction("UserThanksPage");
        }


        public ActionResult UserThanksPage()
        {            
            return View();
        }
        #endregion SellEwaste
        public IActionResult UserHelp()
        {
            return View();
        }

       
        public IActionResult UserLogout()
        {       
            return RedirectToAction("Home", "Login");
        }        
    }
}
