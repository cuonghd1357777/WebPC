using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBPC.Library;
using WEBPC.Models;


namespace WEBPC.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        WEBPCDataContext db = new WEBPCDataContext();
       
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        // GET: Admin/Home
        [HttpPost]
        public ActionResult Login(FormCollection f)
        {
            string password = encryption.ToMD5(f.Get("txtmatkhau"));
            ViewBag.pb = password;

            if (ModelState.IsValid)
            {


               adminn kh = db.adminns.SingleOrDefault(a => a.tendn == f.Get("txtdangnhap") && a.matkhau == (password));
                if (kh != null)
                {

                    Session["taikhoankh"] = kh;

                    Session["kt"] = 1;

                   
                    return RedirectToAction("Index","Admin");
                   






                }


                else

                {

                    if (f.Get("txtdangnhap") == "" && f.Get("txtmatkhau") != "")
                    {
                        ViewBag.error = "Tên đăng nhập không được bỏ trống";

                    }
                    else if (f.Get("txtdangnhap") != "" && f.Get("txtmatkhau") == "")
                    {
                        ViewBag.error = "Mật khẩu không được bỏ trống";

                    }
                    else if (f.Get("txtdangnhap") != "" && f.Get("txtmatkhau") != "")
                    {
                        ViewBag.error = "Sai tên đăng nhập hoặc mật khẩu";
                    }
                    else
                    {
                        ViewBag.error = "Tên đăng nhập và mật khẩu không được trống";
                    }
                }
            }

            return View();
        }
    }
}