using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBPC.Models;
using WEBPC.Library;
using WEBPC.Controllers;
using PagedList;
using PagedList.Mvc;
using WEBPC.Areas.Admin.Models;

namespace WEBPC.Controllers
{
    public class WEBPCController : Controller
    {
        WEBPCDataContext db = new WEBPCDataContext();
        
        // GET: WEBPC
        [HttpGet]
        public ActionResult Index1()
        {
            var list = db.sanphams.OrderByDescending(n => n.ngaycapnhat).Take(6).ToList();
           
            return View(list);
        }
        public ActionResult Index(int? page,string chuoi,FormCollection f)
        {
           
            int iPageSize = 6;
            int iPageNum = (page ?? 1);
            var list1 = db.sanphams.OrderByDescending(n => n.ngaycapnhat).ToPagedList(iPageNum, iPageSize);
            
            

          
            if (chuoi != null)
            {


                ViewBag.a = chuoi;
                var list = from s in db.sanphams
                           join cd in db.danhmucsanphams on s.madm equals cd.madm
                           where s.tensanpham.Contains(chuoi) || cd.tendanhmuc.Contains(chuoi)

                           select s;
                return View(list.ToPagedList(iPageNum, iPageSize));

            }
            else if (!string.IsNullOrEmpty(f["ngaytimkiem"]))
            {
                var ngaytimkiem = f["ngaytimkiem"];
                ViewBag.a = ngaytimkiem;
                var list = from s in db.sanphams
                           join cd in db.danhmucsanphams on s.madm equals cd.madm
                           where s.tensanpham.Contains(ngaytimkiem) || cd.tendanhmuc.Contains(ngaytimkiem)

                           select s;

                if (list != null)
                {
                    return View(list.ToPagedList(iPageNum, iPageSize));
                }

            }
            return View(list1);
        }
        public ActionResult Loginn()
        {
            Session["quay"] = 0;
           
            ViewBag.error = "";
            return View();
        }
        public string tentaikhoan(string s)
        {
            return s;
        }
       
    
        [HttpPost]
        public ActionResult Loginn(FormCollection f)
        {
            Session["quay"] = 0;

            string password = encryption.ToMD5(f.Get("txtmatkhau"));
            ViewBag.pb = password;
          
            if (ModelState.IsValid)
            {


                khachhang kh = db.khachhangs.SingleOrDefault(a => a.tendn == f.Get("txtdangnhap") && a.matkhau == (password));
                if(kh!=null)
                {
                   
                    Session["taikhoankh"]= kh;
                   
                    Session["kt"] = 1;
                    if (f["remember"].Contains("true"))
                    {
                        Response.Cookies["txtdangnhap"].Value = f.Get("txtdangnhap");
                        Response.Cookies["txtmatkhau"].Value = f.Get("txtmatkhau");
                        Response.Cookies["txtdangnha"].Expires = DateTime.Now.AddDays(1);
                        Response.Cookies["txtmatkhau"].Expires = DateTime.Now.AddDays(1);

                    }
                    else
                    {
                        Response.Cookies["txtdangnha"].Expires = DateTime.Now.AddDays(-1);
                        Response.Cookies["txtmatkhau"].Expires = DateTime.Now.AddDays(-1);
                    }
                    List<GioHang> list = new List<GioHang>();
                    list = Session["giohang"] as List<GioHang>;
                    if (list.Count()>0)
                    {
                        return RedirectToAction("GioHang", "GioHang");
                       
                      
                    }
                    else
                    {
                        return RedirectToAction("Index");
                    }


                 

                  

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
        public ActionResult Header()
        {
            ViewBag.bo = Session["kt"];

            if (ViewBag.bo == 1)
            {
                khachhang kh = Session["taikhoankh"] as khachhang;
                ViewBag.mkh = kh.makh;

                ViewBag.tenhienthi = kh.tenkh;
            }
          
            return PartialView();
        }
        public ActionResult Headersanpham()
        {
            ViewBag.bo = Session["kt"];

            if (ViewBag.bo == 1)
            {
                khachhang kh = Session["taikhoankh"] as khachhang;
                ViewBag.mkh = kh.makh;

                ViewBag.tenhienthi = kh.tenkh;
            }

            return PartialView();
        }

        public ActionResult HeaderAcount()
        {
            
           
                return PartialView();
        }
        public ActionResult LogOutt()
        {


            Session.Remove("taikhoankh");
            Session.Remove("kt");
            Session["come"]=0;
            if (int.Parse(Session["quay"].ToString()) == 1)
            {
                Session["quay"] = 0;
                return RedirectToAction("loginn");

            }
           
            return RedirectToAction("Index");
            
        }
      
        public ActionResult thongtintaikhoan(int id)
        {
            khachhang kh = db.khachhangs.SingleOrDefault(n => n.makh == id);
            return View(kh);
        }
    
        [HttpGet]
        public ActionResult thaydoimatkhau()
        {
            khachhang kh = Session["taikhoankh"] as khachhang;
            ViewBag.makh = kh.makh;
            return View();
        }
        [HttpPost]
        public ActionResult thaydoimatkhau(FormCollection f)
        {
            string password = encryption.ToMD5(f.Get("txtmatkhaucu"));
            string password1 = encryption.ToMD5(f.Get("txtmatkhaumoi"));
            khachhang kh = Session["taikhoankh"] as khachhang;
            ViewBag.makh = kh.makh;
            if (string.IsNullOrEmpty(f.Get("txtmatkhaucu")))
            {
                ViewBag.error = "Vui lòng nhập mật khẩu mới";
            }else if (string.IsNullOrEmpty(f.Get("txtmatkhaumoi"))){
                ViewBag.error = "Vui lòng nhập lại mật khẩu mới";
            }
            else
            {
                
                if(password!= password1)
                {
                    ViewBag.error = "Mật khẩu nhập lại không khớp với mật khẩu cũ";
                }
                else
                {
                    khachhang kh1 = db.khachhangs.SingleOrDefault(n => n.makh == kh.makh && n.matkhau == kh.matkhau);
                    kh1.matkhau = password1;
                    db.SubmitChanges();
                    Session["quay"] = 1;
                    return RedirectToAction("LogOutt", "WEBPC");

                }
            }
            return View();
        }
        [HttpGet]
        public ActionResult Editkh(int id)
        {
            var list = db.khachhangs.SingleOrDefault(n => n.makh == id);
            return View(list);
        }
        [HttpPost]
        public ActionResult Editkh(FormCollection f,int id)
        {
            if (ModelState.IsValid)
            {
                khachhang kh1 = db.khachhangs.SingleOrDefault(n => n.makh == id);

                kh1.tenkh = f.Get("tenkh");
                kh1.tendn = f.Get("tendn");
                kh1.gioitinh = f.Get("gioitinh");
                kh1.sodienthoai = f.Get("sodienthoai");
                kh1.email = f.Get("email");
                kh1.diachi = f.Get("diachi");
                db.SubmitChanges();
                Session["quay"] = 1;

                return RedirectToAction("LogOutt", "WEBPC");
            }
            else
            {
                ViewBag.a = "Cập nhật không thành công";
                return RedirectToAction("Editkh", "WEBPC");
            }

            
           
        }
        public ActionResult dangki(FormCollection f)
        {
            var listkh =(from kh in db.khachhangs select kh).ToList();
            var listad= (from ad in db.adminns select ad).ToList();
            if (ModelState.IsValid)
            {

                if (f.Get("tenkh") == "")
                {
                    ViewBag.error = "Tên khách hàng không được bỏ trống";
                }
               if (f.Get("tendn") == "")
                {
                    ViewBag.error1 = "Tên đăng nhập không được bỏ trống";
                }
                if (f.Get("matkhau") == "")
                {
                    ViewBag.error2 = "Mật khẩu không được bỏ trống";
                }
                if (f.Get("nhaplaimatkhau") == "")
                {
                    ViewBag.error3 = "Nhập lại mật khẩu không được bỏ trống";
                }
                if (f.Get("gioitinh") == "")
                {
                    ViewBag.error4 = "Giới tính không được bỏ trống";
                }
                if (f.Get("sodienthoai") == "")
                {
                    ViewBag.error5 = "Số điện thoại không được bỏ trống";
                }
                if(f.Get("email") == "")
                {
                    ViewBag.error6 = "Email không được bỏ trống";
                }
                if (f.Get("diachi") == "")
                {
                    ViewBag.error7 = "Địa chỉ không được bỏ trống";
                }
                if (f.Get("tenkh") != null && f.Get("tendn") != null && f.Get("nhaplaimatkhau") != null && f.Get("matkhau") != null && f.Get("gioitinh") != null && f.Get("sodienthoai") != null && f.Get("email") != null && f.Get("diachi") != null)
                {
                    int i = 0;
                    foreach (var item in listkh)
                    {
                        if (f.Get("tendn").Equals(item.tendn))
                        {
                            ViewBag.error1 = "Tên đăng nhập đã được sử dụng";
                            i++;
                        }
                        if (f.Get("email").Equals(item.email))
                        {
                            ViewBag.error6 = "Email đã được sử dụng";
                            i++;
                        }
                        

                    }

                    if (f.Get("matkhau").Equals(f.Get("nhaplaimatkhau"))&& i==0)
                    {

                        khachhang kh = new khachhang();


                        kh.tenkh = f.Get("tenkh");
                        kh.tendn = f.Get("tendn");

                        kh.matkhau = encryption.ToMD5(f.Get("matkhau"));
                        kh.gioitinh = f.Get("gioitinh");
                        kh.sodienthoai = f.Get("sodienthoai");
                        kh.email = f.Get("email");
                        kh.diachi = f.Get("diachi");

                        db.khachhangs.InsertOnSubmit(kh);
                        db.SubmitChanges();
                        return RedirectToAction("Loginn");
                    }
                    else
                    {
                        ViewBag.error3 = "Mật khẩu nhập lại không đúng";
                    }
                    
                    
                }
            }

            return View();
        }
        public ActionResult sanpham(int? page,string chuoi,FormCollection f)
        {
            int iPageSize = 6;
            int iPageNum = (page ?? 1);
            var list1 = db.sanphams.OrderByDescending(n => n.ngaycapnhat).ToPagedList(iPageNum, iPageSize);




            if (chuoi != null)
            {


                ViewBag.a = chuoi;
                var list = from s in db.sanphams
                           join cd in db.danhmucsanphams on s.madm equals cd.madm
                           where s.tensanpham.Contains(chuoi) || cd.tendanhmuc.Contains(chuoi)

                           select s;
                return View(list.ToPagedList(iPageNum, iPageSize));

            }
            else if (!string.IsNullOrEmpty(f["ngaytimkiem"]))
            {
                var ngaytimkiem = f["ngaytimkiem"];
                ViewBag.a = ngaytimkiem;
                var list = from s in db.sanphams
                           join cd in db.danhmucsanphams on s.madm equals cd.madm
                           where s.tensanpham.Contains(ngaytimkiem) || cd.tendanhmuc.Contains(ngaytimkiem)

                           select s;

                if (list != null)
                {
                    return View(list.ToPagedList(iPageNum, iPageSize));
                }

            }
            return View(list1);
           
        }
        public ActionResult leftdanhmuc()
        {
            var list = db.danhmucsanphams.Select(n => n);
            return PartialView(list);
        }
        public ActionResult sanphamtheodanhmuc(int id, string tendanhmuc,int? page)
        {
            ViewBag.tendanhmuc = tendanhmuc;
            ViewBag.id = id;
            int isize = 6;
            int ipage = (page ?? 1);
            var listsachtheochude = db.sanphams.OrderByDescending(n => n.ngaycapnhat).Where(sp=>sp.madm==id);
            return View(listsachtheochude.ToPagedList(ipage, isize));
        }
        public ActionResult chinhsachbaohanh()
        {
            return View();
        }
        [HttpGet]
        public ActionResult lienhe()
        {
            return View();
        }
        [HttpPost]
        public ActionResult lienhe(FormCollection f)
        {
         
            var hoten = f.Get("tennguoigui");
            var diachi= f.Get("diachi");
            var sdt= f.Get("sodienthoai");
            var email= f.Get("email");
            var chude= f.Get("chude");
            var noidung= f.Get("Noidung");
            int i = 0;
            if (String.IsNullOrEmpty(hoten))
            {
                ViewBag.error1 = "Vui lòng nhập họ tên";
                i++;
            }
            if (String.IsNullOrEmpty(diachi))
            {
                ViewBag.error2 = "Vui lòng nhập địa chỉ";
                i++;
            }
          
            if (String.IsNullOrEmpty(sdt))
            {
                ViewBag.error3 = "Vui lòng nhập số điện thoại";
                i++;
            }
            if (String.IsNullOrEmpty(email))
            {
                ViewBag.error4 = "Vui lòng nhập email";
                i++;
            }
            if (String.IsNullOrEmpty(chude))
            {
                ViewBag.error5 = "Vui lòng nhập chủ đề";
                i++;
            }
            if (String.IsNullOrEmpty(noidung))
            {
                ViewBag.error6 = "Vui lòng nhập nội dung";
                i++;
            }
            if (i == 0)
            {
                lienhe lh = new lienhe();
                lh.tennguoigui = hoten;
                lh.diachi = diachi;
                lh.email = email;
                lh.sodienthoai = sdt;
                lh.chude = chude;
                lh.Noidung = noidung;
                lh.ngaygui = DateTime.Now;
                db.lienhes.InsertOnSubmit(lh);

                db.SubmitChanges();
                ViewBag.booll = 1;


            }
            

            return View();
        }
        

        public ActionResult chitietsanpham(int id)
        {
            var list = db.sanphams.Where(n => n.masp == id).Single();
            return View(list);
        }
        public ActionResult gt730()
        {
            return View();
        }
        public ActionResult steam()
        {
            return View();
        }


    }
}