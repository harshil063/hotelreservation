using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using hotelreservation.Models;
using System.Web.Security;

namespace hotelreservation.Controllers
{
    public class AccountController : Controller
    {
        HotelDBEntities1 db = new HotelDBEntities1();
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(tbl_customer cst)
        {
            

            tbl_customer cs = db.tbl_customer.Where(x => x.email == cst.email && x.password == cst.password).SingleOrDefault();
            if (cs != null)
            {
                FormsAuthentication.SetAuthCookie(cst.email.ToString(), false);
                Session["cust_id"] = cs.cust_id.ToString();
                Session["cust_fname"] = cs.fname.ToString();
                Session["cust_lname"] = cs.lname.ToString();
                return RedirectToAction("Home", "Customer");
            }
            else
            {
                ViewBag.err = "Invalid user name or password";
            }
            return View();
        }


        public ActionResult Signup()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signup(tbl_customer model)
        {
            using (var context = new HotelDBEntities1())
            {
                context.tbl_customer.Add(model);
                if(model.password == model.cpassword)
                {
                    context.SaveChanges();
                    return RedirectToAction("Login");
                }
                else
                {
                    ViewBag.passerr = "Password does not match..!!";
                    
                }

            }
            return View();
            
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}