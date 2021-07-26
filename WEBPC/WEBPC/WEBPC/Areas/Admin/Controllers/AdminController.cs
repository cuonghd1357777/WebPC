using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBPC.Models;
using System.Web.Mvc;
using WEBPC.Library;
using PagedList;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Net.Mime;
using WEBPC.Areas.Admin.Models;
using System.Text;
using System.Data.Entity.Core.Objects;

namespace WEBPC.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        WEBPCDataContext db = new WEBPCDataContext();
        public ActionResult logout()
        {
            Session["taikhoankh"] = null;
            return RedirectToAction("Login", "Home");
        }
        // GET: Admin/Admin
        /*
         
        /*
       [HttpGet]
        public ActionResult Index(int? page)
        {

            var list3 = db.danhmucsanphams.ToList();
            ViewBag.loaidanhmuc = new SelectList(list3, "madm", "tendanhmuc");
            int iPageNum = (page ?? 1);
            int iPageSize = 7;
            
            return View(db.sanphams.ToList().OrderBy(sp=>sp.masp).ToPagedList(iPageNum, iPageSize));
        }
        */
        /*
        public ActionResult Index(int? page, FormCollection f, string chuoi, string ngayg, string ngayc)
        {
            int iPageNum = (page ?? 1);
            int iPageSize = 7;
            var list1 = db.sanphams.ToList().OrderBy(n => n.masp).ToPagedList(iPageNum, iPageSize);
            var list3 = db.danhmucsanphams.ToList();

            ViewBag.loaidanhmuc = new SelectList(list3, "madm", "tendanhmuc");
          if (chuoi != null)
             {
                 ViewBag.a = chuoi;
                 var list = from s in db.sanphams
                            join cd in db.danhmucsanphams on s.madm equals cd.madm
                            where s.tensanpham.Contains(chuoi) || cd.tendanhmuc.Contains(chuoi)
                            orderby s.ngaycapnhat
                            select s;
                 return View(list.ToPagedList(iPageNum, iPageSize));
             }
         




            ViewBag.ngaygiao = String.Format("{0:MM/dd/yyyy}", f["ngaylap"]);
            ViewBag.ngaygiaocuoi = String.Format("{0:MM/dd/yyyy}", f["ngaylapcuoi"]);
            ViewBag.ngaytimkiem = f["ngaytimkiem"];

            if (!string.IsNullOrEmpty(ngayg) && !string.IsNullOrEmpty(ngayc))
            {
                DateTime ngaylap = Convert.ToDateTime(ngayg).Date;
                DateTime ngaylapcuoi = Convert.ToDateTime(ngayc).Date;

                //DateTime ngaylap = Convert.ToDateTime(f["ngaylap"]).Date;
                //DateTime ngaylapcuoi = Convert.ToDateTime(f["ngaylapcuoi"]).Date;
                //ViewBag.ngaylap = ngaylap.ToString("yyyy-MM-dd");
                // ViewBag.ngaylapcuoi = ngaylapcuoi.ToString("yyyy-MM-dd");
                if (!string.IsNullOrEmpty(chuoi))
                {


                    var list = db.sanphams.OrderByDescending(dh => dh.ngaycapnhat).Where(
                    ab => ab.ngaycapnhat.Date >= ngaylap && ab.ngaycapnhat.Date <= ngaylapcuoi && ab.tensanpham == (chuoi)).ToList();
                    if (list != null)
                    {
                        ViewBag.a = list.ToPagedList(iPageNum, iPageSize);
                        return View(list.ToPagedList(iPageNum, iPageSize));
                    }

                }
                else
                {
                    var list = db.sanphams.OrderByDescending(dh => dh.ngaycapnhat).Where(
                     ab => ab.ngaycapnhat.Date >= ngaylap && ab.ngaycapnhat.Date <= ngaylapcuoi).ToList();
                    if (list != null)
                    {
                        ViewBag.a = list.ToPagedList(iPageNum, iPageSize);
                        return View(list.ToPagedList(iPageNum, iPageSize));
                    }

                }

            }
            else if (string.IsNullOrEmpty(ngayg) && string.IsNullOrEmpty(chuoi))
            {
                if (!string.IsNullOrEmpty(chuoi))
                {

                    /*var list = db.sanphams.OrderByDescending(dh => dh.ngaycapnhat).ToList();
                    ViewBag.loaidanhmuc = new SelectList(list3, "madm", "tendanhmuc", f["loaidanhmuc"]);
                    if (list != null)
                    {
                        ViewBag.a = list.ToPagedList(iPageNum, iPageSize);
                        return View(list.ToPagedList(iPageNum, iPageSize));
                    }
                   
                    var list = from s in db.sanphams
                               join cd in db.danhmucsanphams on s.madm equals cd.madm
                               where s.tensanpham.Contains(chuoi) || cd.tendanhmuc.Contains(chuoi)
                               orderby s.ngaycapnhat
                               select s;

                    if (list != null)
                    {
                        ViewBag.a = chuoi;
                        return View(list.ToPagedList(iPageNum, iPageSize));
                    }

                }
                else if (!string.IsNullOrEmpty(chuoi))
                {

                    var list = from s in db.sanphams
                               join cd in db.danhmucsanphams on s.madm equals cd.madm
                               where s.tensanpham.Contains(chuoi) || cd.tendanhmuc.Contains(chuoi)
                               orderby s.ngaycapnhat
                               select s;

                    if (list != null)
                    {
                        ViewBag.a = chuoi;
                        return View(list.ToPagedList(iPageNum, iPageSize));
                    }

                }
            }



            return View(list1);
        }
        */



        public ActionResult Index(int? page, FormCollection f, string chuoi, string ngayg, string ngayc, string ldm,string sapxep)
        {
            int iPageNum = (page ?? 1);
            int iPageSize = 7;
            var list1 =db.sanphams.ToList().OrderBy(n => n.masp).ToPagedList(iPageNum, iPageSize); ;
            if (sapxep == "1" || sapxep == null)
            {
                 list1 = db.sanphams.ToList().OrderByDescending(n => n.ngaycapnhat).ToPagedList(iPageNum, iPageSize);
            }
            else if (sapxep == "2" )
            {
                 list1 = db.sanphams.ToList().OrderBy(n => n.ngaycapnhat).ToPagedList(iPageNum, iPageSize);
               
            }
            else if (sapxep == "3" )
            {
                 list1 = db.sanphams.ToList().OrderByDescending(n => n.ngaycapnhat).ToPagedList(iPageNum, iPageSize);
               
            }
            else if (sapxep == "4")
            {
                 list1 = db.sanphams.ToList().OrderBy(n => n.dongia).ToPagedList(iPageNum, iPageSize);

            }
            else 
            {
                 list1 = db.sanphams.ToList().OrderByDescending(n => n.dongia).ToPagedList(iPageNum, iPageSize);
               
            }
            var list3 = db.danhmucsanphams.ToList();

            ViewBag.loaidanhmuc = new SelectList(list3, "madm", "tendanhmuc");
            if (chuoi != null)
            {

       
                ViewBag.a = chuoi;
                var list = from s in db.sanphams
                           join cd in db.danhmucsanphams on s.madm equals cd.madm
                           where s.tensanpham.Contains(chuoi) || cd.tendanhmuc.Contains(chuoi)
                           orderby s.masp
                           select s;
     
                return View(list.ToPagedList(iPageNum, iPageSize));

            }
            else if (ldm != null)
            {

                ViewBag.ldm = ldm;
                var list = db.sanphams.OrderByDescending(dh => dh.ngaycapnhat).Where(cd => cd.madm == int.Parse(ldm)).ToList();
                ViewBag.loaidanhmuc = new SelectList(list3, "madm", "tendanhmuc", ldm);
                if (list != null)
                {

                    return View(list.ToPagedList(iPageNum, iPageSize));
                }

                return View(list1);

            }
            else if (!string.IsNullOrEmpty(ngayg) && !string.IsNullOrEmpty(ngayc))
            {
                DateTime ngaylap = Convert.ToDateTime(ngayg).Date;
                DateTime ngaylapcuoi = Convert.ToDateTime(ngayc).Date;

                //DateTime ngaylap = Convert.ToDateTime(f["ngaylap"]).Date;
                //DateTime ngaylapcuoi = Convert.ToDateTime(f["ngaylapcuoi"]).Date;
                //ViewBag.ngaylap = ngaylap.ToString("yyyy-MM-dd");
                // ViewBag.ngaylapcuoi = ngaylapcuoi.ToString("yyyy-MM-dd");
                if (!string.IsNullOrEmpty(chuoi))
                {

                    ViewBag.ngaygiao = ngayg;
                    ViewBag.ngaygiaocuoi = ngayc;
                    ViewBag.a = chuoi;
                    var list = db.sanphams.OrderByDescending(dh => dh.ngaycapnhat).Where(
                    ab => ab.ngaycapnhat.Date >= ngaylap && ab.ngaycapnhat.Date <= ngaylapcuoi && ab.tensanpham == (chuoi)).ToList();
                    if (list != null)
                    {

                        return View(list.ToPagedList(iPageNum, iPageSize));
                    }

                }
                else
                {
                    var list = db.sanphams.OrderByDescending(dh => dh.ngaycapnhat).Where(
                     ab => ab.ngaycapnhat.Date >= ngaylap && ab.ngaycapnhat.Date <= ngaylapcuoi).ToList();
                    if (list != null)
                    {
                        ViewBag.ngaygiao = ngayg;
                        ViewBag.ngaygiaocuoi = ngayc;
                        ViewBag.ngaytimkiem = chuoi;

                        return View(list.ToPagedList(iPageNum, iPageSize));
                    }

                }
            }
            else
            {




                var ngaygiao = String.Format("{0:MM/dd/yyyy}", f["ngaylap"]);
                var ngaygiaocuoi = String.Format("{0:MM/dd/yyyy}", f["ngaylapcuoi"]);
                var ngaytimkiem = f["ngaytimkiem"];

                ViewBag.ngaygiao = String.Format("{0:MM/dd/yyyy}", f["ngaylap"]);
                ViewBag.ngaygiaocuoi = String.Format("{0:MM/dd/yyyy}", f["ngaylapcuoi"]);
                if (string.IsNullOrEmpty(ngaygiao) && string.IsNullOrEmpty(ngaygiaocuoi) && string.IsNullOrEmpty(ngaytimkiem))
                {
                    if (!string.IsNullOrEmpty(f["loaidanhmuc"]))
                    {
                        var list = db.sanphams.OrderByDescending(dh => dh.ngaycapnhat).Where(cd => cd.madm == int.Parse(f["loaidanhmuc"])).ToList();
                        ViewBag.loaidanhmuc = new SelectList(list3, "madm", "tendanhmuc", f["loaidanhmuc"]);
                        if (list != null)
                        {
                            ViewBag.ldm = f["loaidanhmuc"];
                            return View(list.ToPagedList(iPageNum, iPageSize));
                        }

                        return View(list1);
                    }

                }

                else if (!string.IsNullOrEmpty(ngaygiao) && !string.IsNullOrEmpty(ngaygiaocuoi))
                {
                    DateTime ngaylap = Convert.ToDateTime(ngaygiao).Date;
                    DateTime ngaylapcuoi = Convert.ToDateTime(ngaygiaocuoi).Date;

                    //DateTime ngaylap = Convert.ToDateTime(f["ngaylap"]).Date;
                    //DateTime ngaylapcuoi = Convert.ToDateTime(f["ngaylapcuoi"]).Date;
                    //ViewBag.ngaylap = ngaylap.ToString("yyyy-MM-dd");
                    // ViewBag.ngaylapcuoi = ngaylapcuoi.ToString("yyyy-MM-dd");
                    if (!string.IsNullOrEmpty(ngaytimkiem))
                    {


                        var list = db.sanphams.OrderByDescending(dh => dh.ngaycapnhat).Where(
                        ab => ab.ngaycapnhat.Date >= ngaylap && ab.ngaycapnhat.Date <= ngaylapcuoi && ab.tensanpham == (ngaytimkiem)).ToList();
                        if (list != null)
                        {

                            return View(list.ToPagedList(iPageNum, iPageSize));
                        }

                    }
                    else
                    {
                        var list = db.sanphams.OrderByDescending(dh => dh.ngaycapnhat).Where(
                         ab => ab.ngaycapnhat.Date >= ngaylap && ab.ngaycapnhat.Date <= ngaylapcuoi).ToList();
                        if (list != null)
                        {

                            return View(list.ToPagedList(iPageNum, iPageSize));
                        }

                    }

                }
                else if (string.IsNullOrEmpty(ngaygiao) && string.IsNullOrEmpty(ngaygiaocuoi))
                {/*
                    if (string.IsNullOrEmpty(f["loaidanhmuc"]))
                    {
                        var list = db.sanphams.OrderByDescending(dh => dh.ngaycapnhat).Where(cd=>cd.madm==int.Parse(f["loaidanhmuc"])).ToList();
                        ViewBag.loaidanhmuc = new SelectList(list3, "madm", "tendanhmuc", f["loaidanhmuc"]);
                        if (list != null)
                        {
                           ViewBag.ldm= f["loaidanhmuc"];
                            return View(list.ToPagedList(iPageNum, iPageSize));
                        }

                        return View(list1);
                    }
                    */
                    if (!string.IsNullOrEmpty(ngaytimkiem))
                    {

                        var list = from s in db.sanphams
                                   join cd in db.danhmucsanphams on s.madm equals cd.madm
                                   where s.tensanpham.Contains(ngaytimkiem) || cd.tendanhmuc.Contains(ngaytimkiem)
                                   orderby s.ngaycapnhat
                                   select s;

                        if (list != null)
                        {
                            ViewBag.a = ngaytimkiem;
                            return View(list.ToPagedList(iPageNum, iPageSize));
                        }

                    }
                }
            }


            return View(list1);
        }

        [HttpGet]
        public ActionResult Create()
        {

            ViewBag.MaDM = new SelectList(db.danhmucsanphams.ToList(), "madm", "tendanhmuc");
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(FormCollection f, HttpPostedFileBase fFileUpload)
        {
            sanpham sp = new sanpham();
            ViewBag.MaDM = new SelectList(db.danhmucsanphams.ToList(), "madm", "tendanhmuc");
            if (fFileUpload == null)
            {
                ViewBag.tensanpham = f["tensanpham"];
                ViewBag.mota = f["mota"];
                ViewBag.dongia = int.Parse(f["dongia"]);
                ViewBag.soluong = int.Parse(f["soluong"]);
                ViewBag.ngaycapnhat = Convert.ToDateTime(f["ngaycapnhat"]);
                ViewBag.madm = int.Parse(f["MaDM"]);
                ViewBag.thongbao = "Vui lòng chọn ảnh";
                ViewBag.MaDM = new SelectList(db.danhmucsanphams.ToList(), "madm", "tendanhmuc", int.Parse(f["MaDM"]));
                ViewBag.baohanh = f["baohanh"];
                return View();
            }
            else if (ModelState.IsValid)
            {
                var sFileName = Path.GetFileName(fFileUpload.FileName);
                var path = Path.Combine(Server.MapPath("~/hinh anh"), sFileName);
                if (!System.IO.File.Exists(path))
                {
                    fFileUpload.SaveAs(path);
                }
                sp.tensanpham = f["tensanpham"];
                sp.mota = f["mota"];
                //sp.mota = f["mota"].Replace("<p>", "").Replace("</p", "\n"); ;
                sp.hinhanh = sFileName;
                sp.dongia = int.Parse(f["dongia"]);
                sp.soluong = int.Parse(f["soluong"]);
                sp.ngaycapnhat = Convert.ToDateTime(f["ngaycapnhat"]);
                sp.madm = int.Parse(f["MaDM"]);
                sp.baohanh = f["baohanh"];
                db.sanphams.InsertOnSubmit(sp);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View();

            /*
            [HttpPost]
            [ValidateInput(false)]
            public ActionResult Create(FormCollection f)
            {
                sanpham sp = new sanpham();
                ViewBag.MaDM = new SelectList(db.danhmucsanphams.ToList(), "madm", "tendanhmuc");
                if (ModelState.IsValid)
                {
                    var a = Request.Files["fFileUpload"];
                    var path = Path.Combine(Server.MapPath("~/hinh anh"), a.FileName);
                    if (!System.IO.File.Exists(path))
                    {
                        a.SaveAs(path);
                    }
                    sp.tensanpham = f["tensanpham"];
                    sp.mota = f["mota"];
                    sp.hinhanh = a.FileName;
                    sp.dongia = int.Parse(f["dongia"]);
                    sp.soluong=Convert.ToInt32(f["soluong"]);
                    sp.ngaycapnhat = Convert.ToDateTime(f["ngaycapnhat"]);
                    sp.madm = int.Parse(f["MaDM"]);
                    db.sanphams.InsertOnSubmit(sp);
                    db.SubmitChanges();
                    return RedirectToAction("Index");
                }
                return View();

            }
            */


        }
        public ActionResult Details(int id)
        {
            var sp = db.sanphams.Where(cp => cp.masp == id).SingleOrDefault();
            return View(sp);
        }
        public ActionResult Detailssptk(int id)
        {
            var sp = db.sanphams.Where(cp => cp.masp == id).SingleOrDefault();
            return View(sp);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            ViewBag.hienthi = "Hình ảnh cũ";
            ViewBag.MaDM = new SelectList(db.danhmucsanphams.ToList(), "madm", "tendanhmuc");
            var sp = db.sanphams.Where(cp => cp.masp == id).SingleOrDefault();
            return View(sp);
        }
        [HttpPost]
        public ActionResult Edit(FormCollection f, int id, HttpPostedFile fFileUpload)
        {
            var sp = db.sanphams.Where(cp => cp.masp == id).SingleOrDefault();
            ViewBag.MaDM = new SelectList(db.danhmucsanphams.ToList(), "madm", "tendanhmuc");
            if (fFileUpload != null)
            {
                var sFileName = Path.GetFileName(fFileUpload.FileName);
                var path = Path.Combine(Server.MapPath("~/hinh anh"), sFileName);
                if (!System.IO.File.Exists(path))
                {
                    fFileUpload.SaveAs(path);
                }
                sp.hinhanh = sFileName;

            }
            if (ModelState.IsValid)
            {

                ViewBag.MaDM = new SelectList(db.danhmucsanphams.ToList(), "madm", "tendanhmuc", int.Parse(f["MaDM"]));
                sp.tensanpham = f["tensanpham"];
                //sp.mota = f["mota"].Replace("<p>", "").Replace("</p", "\n");
                sp.mota = f["mota"];
                sp.dongia = int.Parse(f["dongia"]);
                sp.soluong = int.Parse(f["soluong"]);
                sp.ngaycapnhat = Convert.ToDateTime(f["ngaycapnhat"]);
                sp.madm = int.Parse(f["MaDM"]);
                sp.baohanh = f["baohanh"];
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
            return View(sp);
        }
        [HttpGet]
        public ActionResult Delete(int id)
        {
            var sp = db.sanphams.Where(cp => cp.masp == id).SingleOrDefault();
            return View(sp);
        }
        [HttpPost]
        public ActionResult Delete(int id, FormCollection f)
        {
            var sp = db.sanphams.Where(cp => cp.masp == id).SingleOrDefault();
            var ctdh = db.chitietdonhangs.Where(ct => ct.masp == id).ToList();
            if (ctdh.Count > 0)
            {
                ViewBag.thongbao = "Sản phẩm có trong chi tiết đơn hàng. Vui lòng xóa sản phẩm trong chi tiết đơn hàng";
                return View(sp);
            }
            else
            {
                db.sanphams.DeleteOnSubmit(sp);
                db.SubmitChanges();
                return RedirectToAction("Index");
            }
        }
        public ActionResult lienhe()
        {
            var lienhe = db.lienhes.OrderByDescending(lh => lh.ngaygui).ToList();
            return View(lienhe);
        }
        public ActionResult xoalienhe(int id)
        {
            var list = db.lienhes.Where(lh => lh.idlienhe == id).SingleOrDefault();
            db.lienhes.DeleteOnSubmit(list);
            db.SubmitChanges();
            return RedirectToAction("lienhe");

        }
        [HttpGet]
        public ActionResult SendMail(int id)
        {
            var list = db.lienhes.Where(lh => lh.idlienhe == id).SingleOrDefault();
            ViewBag.tenkh = list.tennguoigui;
            ViewBag.emailgui = list.email.ToString();
            return View();
        }
        [HttpPost]
        public ActionResult SendMail(Mail model, int id)
        {
            var list = db.lienhes.Where(lh => lh.idlienhe == id).SingleOrDefault();
            var mail = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new NetworkCredential("cuonghd135777@gmail.com", "016263194864hd"),
                EnableSsl = true
            };
            var mess = new MailMessage();
            mess.From = new MailAddress(model.From);
            mess.ReplyToList.Add(model.From);
            mess.To.Add(new MailAddress(model.To));
            mess.Subject = model.Subject;
            mess.Body = model.Notes;
            var file = Request.Files["file1"];
            if (file != null)
            {
                var sFileName = Path.GetFileName(file.FileName);
                var path = Path.Combine(Server.MapPath("~/hinh anh"), sFileName);
                if (!System.IO.File.Exists(path))
                {
                    file.SaveAs(path);
                }
                Attachment data = new Attachment(Server.MapPath("~/hinh anh/" + file.FileName), MediaTypeNames.Application.Octet);
                mess.Attachments.Add(data);

            }




            mail.Send(mess);

            return View("SendMail");
        }
        /*
         public ActionResult khachhang(int? page)
         {
             int iPageNum = (page ?? 1);
             int iPageSize = 7;
             var list1 = db.khachhangs.ToList().OrderBy(n => n.makh).ToPagedList(iPageNum, iPageSize);
             return View(list1);

         }
        */

        public ActionResult xemhetsp()
        {
            return RedirectToAction("Index");
        }
        public ActionResult xemhetkh()
        {
            return RedirectToAction("khachhang");
        }

        public ActionResult khachhang(int? page, FormCollection f, string chuoi)
        {
            int iPageNum = (page ?? 1);
            int iPageSize = 6;

            var list1 = db.khachhangs.OrderBy(kh => kh.makh).ToList().ToPagedList(iPageNum, iPageSize);
            if (chuoi != null)
            {


                ViewBag.a = chuoi;
                var list = from s in db.khachhangs

                           where s.tenkh.Contains(chuoi)

                           select s;
                return View(list.ToPagedList(iPageNum, iPageSize));

            }
            else if (!string.IsNullOrEmpty(f["ngaytimkiem"]))
            {
                var ngaytimkiem = f["ngaytimkiem"];
                ViewBag.a = ngaytimkiem;
                var list = from s in db.khachhangs

                           where s.tenkh.Contains(ngaytimkiem)

                           select s;

                if (list != null)
                {
                    return View(list.ToPagedList(iPageNum, iPageSize));
                }

            }
            return View(list1);
        }

        public ActionResult Detailskh(int id)
        {
            var list = db.khachhangs.SingleOrDefault(khachhang => khachhang.makh == id);
            return View(list);
        }
        [HttpGet]
        public ActionResult Editkh(int id)
        {
            var list = db.khachhangs.SingleOrDefault(khachhang => khachhang.makh == id);
            return View(list);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Editkh(int id, FormCollection f)
        {
            if (ModelState.IsValid)
            {
                khachhang kh1 = db.khachhangs.SingleOrDefault(n => n.makh == id);

                kh1.tenkh = f.Get("tenkh");
                kh1.matkhau = encryption.ToMD5(f.Get("matkhau"));
                kh1.gioitinh = f.Get("gioitinh");
                kh1.sodienthoai = f.Get("sodienthoai");
                kh1.email = f.Get("email");
                kh1.diachi = f.Get("diachi");
                db.SubmitChanges();


                return RedirectToAction("khachhang");
            }
            return View();
        }

        public ActionResult Deletekh(int id)
        {
            var list = db.khachhangs.SingleOrDefault(khachhang => khachhang.makh == id);
            db.khachhangs.DeleteOnSubmit(list);
            db.SubmitChanges();
            return RedirectToAction("khachhang");
        }
        public ActionResult xoahetdh()
        {
            var list1 = db.chitietdonhangs;
            db.chitietdonhangs.DeleteAllOnSubmit(list1);
            db.SubmitChanges();
            var list = db.donhangs;
            db.donhangs.DeleteAllOnSubmit(list);
            db.SubmitChanges();
            return RedirectToAction("donhang");
        }
        public ActionResult xemhetdh()
        {



            return RedirectToAction("donhang");
        }
        /*
        [HttpGet]
        public ActionResult donhang()
        {

          

            var list = db.donhangs.OrderByDescending(dh => dh.ngaylap).ToList();
            
            
            return View(list);
        }
        */
        /**
        [HttpPost]
        public ActionResult donhang(FormCollection f)
        {
            var list1 = db.donhangs.OrderByDescending(dh => dh.ngaylap).ToList();
            var ngaygiao = String.Format("{0:MM/dd/yyyy}", f["ngaylap"]);
            var ngaygiaocuoi = String.Format("{0:MM/dd/yyyy}", f["ngaylapcuoi"]);
            var ngaytimkiem = f["ngaytimkiem"];
            ViewBag.ngay = ngaytimkiem;
            //var kh = db.khachhangs.Where(k => k.tenkh.Contains(ngaytimkiem)).Select(k => k.makh);
            var kq = from s in db.donhangs
                     join cd in db.khachhangs on s.makh equals cd.makh
                     where cd.tenkh.Contains(ngaytimkiem)
                     orderby s.ngaylap
                     select s;

                     
            return View(kq);
            
        }
        */


        public ActionResult donhang(int? page, FormCollection f, string chuoi, string ngayg, string ngayc, string ldm,string loaidh)
        {

            int iPageNum = (page ?? 1);
            int iPageSize = 7;
            var list1 = db.donhangs.OrderByDescending(dh => dh.ngaylap).ToList().ToPagedList(iPageNum, iPageSize);


            /*var kq = from s in db.donhangs
                     join cd in db.khachhangs on s.makh equals cd.makh
                     where cd.tenkh.Equals(ngaytimkiem)
                     orderby s.ngaylap
                     group s by new {s.chitietdonhangs,s.HTGiaoHang,s.HTThanhToan,s.khachhang,s.madonhang,s.makh,s.NgayGiaoHang,s.ngaylap } 
                     ;
            */
            if (chuoi != null)
            {

                ViewBag.a = chuoi;
                var list = from s in db.donhangs
                           join cd in db.khachhangs on s.makh equals cd.makh
                           where cd.tenkh.Contains(chuoi)
                           orderby s.ngaylap
                           select s;

                if (list != null)
                {
                    return View(list.ToPagedList(iPageNum, iPageSize));
                }


            }
            else if (ldm != null)
            {

                ViewBag.ldm = ldm;
                var list = db.donhangs.OrderByDescending(dh => dh.ngaylap).Where(ab => ab.tinhtrang == ldm).ToList();

                if (list != null)
                {
                    return View(list.ToPagedList(iPageNum, iPageSize));
                }

            }
            else if (!string.IsNullOrEmpty(ngayg) && !string.IsNullOrEmpty(ngayc))
            {
                DateTime ngaylap = Convert.ToDateTime(ngayg).Date;
                DateTime ngaylapcuoi = Convert.ToDateTime(ngayc).Date;

                //DateTime ngaylap = Convert.ToDateTime(f["ngaylap"]).Date;
                //DateTime ngaylapcuoi = Convert.ToDateTime(f["ngaylapcuoi"]).Date;
                //ViewBag.ngaylap = ngaylap.ToString("yyyy-MM-dd");
                // ViewBag.ngaylapcuoi = ngaylapcuoi.ToString("yyyy-MM-dd");
                if (!string.IsNullOrEmpty(chuoi))
                {

                    ViewBag.ngaygiao = ngayg;
                    ViewBag.ngaygiaocuoi = ngayc;
                    ViewBag.a = chuoi;
                    var list = db.sanphams.OrderByDescending(dh => dh.ngaycapnhat).Where(
                    ab => ab.ngaycapnhat.Date >= ngaylap && ab.ngaycapnhat.Date <= ngaylapcuoi && ab.tensanpham == (chuoi)).ToList();
                    if (list != null)
                    {

                        return View(list.ToPagedList(iPageNum, iPageSize));
                    }

                }
                else
                {
                    var list = db.sanphams.OrderByDescending(dh => dh.ngaycapnhat).Where(
                     ab => ab.ngaycapnhat.Date >= ngaylap && ab.ngaycapnhat.Date <= ngaylapcuoi).ToList();
                    if (list != null)
                    {
                        ViewBag.ngaygiao = ngayg;
                        ViewBag.ngaygiaocuoi = ngayc;
                        ViewBag.ngaytimkiem = chuoi;

                        return View(list.ToPagedList(iPageNum, iPageSize));
                    }

                }
            }
            else
            {
                var ngaygiao = String.Format("{0:MM/dd/yyyy}", f["ngaylap"]);
                var ngaygiaocuoi = String.Format("{0:MM/dd/yyyy}", f["ngaylapcuoi"]);
                var ngaytimkiem = f["ngaytimkiem"];
                ViewBag.ngaygiao = String.Format("{0:MM/dd/yyyy}", f["ngaylap"]);
                ViewBag.ngaygiaocuoi = String.Format("{0:MM/dd/yyyy}", f["ngaylapcuoi"]);
                if (string.IsNullOrEmpty(ngaygiao) && string.IsNullOrEmpty(ngaygiaocuoi) && string.IsNullOrEmpty(ngaytimkiem))
                {
                    if (!string.IsNullOrEmpty(loaidh))
                    {
                        var list = db.donhangs.OrderByDescending(dh => dh.ngaylap).Where(ab => ab.tinhtrang == loaidh).ToList();


                        if (list != null)
                        {
                            ViewBag.ldm = f["loaidonhang"];
                            return View(list.ToPagedList(iPageNum, iPageSize));
                        }
                        return View(list1);


                    }

                }
                else if (!string.IsNullOrEmpty(ngaygiao) && !string.IsNullOrEmpty(ngaygiaocuoi))
                {
                    DateTime ngaylap = Convert.ToDateTime(ngaygiao).Date;
                    DateTime ngaylapcuoi = Convert.ToDateTime(ngaygiaocuoi).Date;

                    //DateTime ngaylap = Convert.ToDateTime(f["ngaylap"]).Date;
                    //DateTime ngaylapcuoi = Convert.ToDateTime(f["ngaylapcuoi"]).Date;
                    // ViewBag.ngaylap = ngaylap.ToString("yyyy-MM-dd");
                    // ViewBag.ngaylapcuoi = ngaylapcuoi.ToString("yyyy-MM-dd");              
                    if (!string.IsNullOrEmpty(ngaytimkiem))
                    {


                        var list = db.donhangs.OrderByDescending(dh => dh.ngaylap).Where(ab =>
                          ab.ngaylap.Date >= ngaylap && ab.ngaylap.Date <= ngaylapcuoi).ToList();
                        if (list != null)
                        {
                            return View(list.ToPagedList(iPageNum, iPageSize));
                        }
                        return View(list1);
                    }
                    else
                    {
                        var list = db.donhangs.OrderByDescending(dh => dh.ngaylap).Where(ab =>
                         ab.ngaylap.Date >= ngaylap && ab.ngaylap.Date <= ngaylapcuoi).ToList();
                        if (list != null)
                        {
                            return View(list.ToPagedList(iPageNum, iPageSize));
                        }
                        return View(list1);
                    }

                }
                else if (string.IsNullOrEmpty(ngaygiao) && string.IsNullOrEmpty(ngaygiaocuoi))
                {
                    if (!string.IsNullOrEmpty(ngaytimkiem))
                    {

                        var list = from s in db.donhangs
                                   join cd in db.khachhangs on s.makh equals cd.makh
                                   where cd.tenkh.Contains(ngaytimkiem)
                                   orderby s.ngaylap
                                   select s;

                        if (list != null)
                        {
                            ViewBag.a = ngaytimkiem;
                            return View(list.ToPagedList(iPageNum, iPageSize));
                        }

                    }
                }
            }

            return View(list1);
        }

        public ActionResult Deletedh(int id)
        {
            var list1 = db.chitietdonhangs.Where(dh => dh.madonhang == id);
            db.chitietdonhangs.DeleteAllOnSubmit(list1);
            db.SubmitChanges();
            var list = db.donhangs.SingleOrDefault(dh => dh.madonhang == id);
            db.donhangs.DeleteOnSubmit(list);
            db.SubmitChanges();


            return RedirectToAction("donhang");
        }
        [HttpGet]
        public ActionResult Detailsdh(int id)
        {
            ViewBag.hienthi = id;
            var list1 = db.donhangs.SingleOrDefault(dh => dh.madonhang == id);
            var list = db.chitietdonhangs.Where(dh => dh.madonhang == id);
            var kh = db.khachhangs.Where(item => item.makh == list1.makh).SingleOrDefault();
            @ViewBag.hoten = kh.tenkh;
            @ViewBag.diachi = kh.diachi;
            @ViewBag.sodienthoai = kh.sodienthoai;
            @ViewBag.email = kh.email;
            ViewBag.ngaydat = Convert.ToDateTime(list1.ngaylap).ToString("dd/MMyyyy");
            ViewBag.ngaygiao = Convert.ToDateTime(list1.NgayGiaoHang).ToString("dd/MMyyyy");
            return View(list);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Detailsdh(int id, FormCollection f)
        {
            if (ModelState.IsValid)
            {
                var list1 = db.chitietdonhangs.Where(dh => dh.madonhang == id);
                db.chitietdonhangs.DeleteAllOnSubmit(list1);
                db.SubmitChanges();
                var list = db.donhangs.SingleOrDefault(dh => dh.madonhang == id);
                db.donhangs.DeleteOnSubmit(list);
                db.SubmitChanges();

                return RedirectToAction("donhang");
            }
            return View();

        }
        [HttpGet]
        public ActionResult capnhatdh(int id)
        {
            tinhtrang aa = new tinhtrang();
            List<tinhtrang> listt = new List<tinhtrang>();

            aa = new tinhtrang();
            aa.Chuoi = "Đơn hàng đang vận chuyển";
            listt.Add(aa);
            aa = new tinhtrang();
            aa.Chuoi = "Đơn hàng đã hoàn tất";
            listt.Add(aa);
            aa = new tinhtrang();
            aa.Chuoi = "Đơn hàng mới";
            listt.Add(aa);
            aa = new tinhtrang();
            aa.Chuoi = "Đơn hàng bị hủy";
            listt.Add(aa);
            var item = db.donhangs.SingleOrDefault(dh => dh.madonhang == id);
            if (item.tinhtrang == "Đơn hàng đang vận chuyển")
            {
                ViewBag.ttrang = new SelectList(listt, "chuoi", "chuoi", "Đơn hàng đang vận chuyển");
            }
            else if (item.tinhtrang == "Đơn hàng đã hoàn tất")
            {
                ViewBag.ttrang = new SelectList(listt, "chuoi", "chuoi", "Đã giao hàng");

            }
            else if (item.tinhtrang == "Đơn hàng mới")
            {
                ViewBag.ttrang = new SelectList(listt, "chuoi", "chuoi", "Đơn hàng mới");

            }
            else
            {
                ViewBag.ttrang = new SelectList(listt, "chuoi", "chuoi", "Đơn hàng bị hủy");
            }
            tinhtrang a1 = new tinhtrang();
            List<tinhtrang> listt1 = new List<tinhtrang>();
            a1.Chuoi = "Ship code";
            listt1.Add(a1);
            a1 = new tinhtrang();
            a1.Chuoi = "Chuyển khoản";
            listt1.Add(a1);
            //
            tinhtrang a2 = new tinhtrang();
            List<tinhtrang> listt2 = new List<tinhtrang>();
            a2.Chuoi = "ViettelPost";
            listt2.Add(a2);
            a2 = new tinhtrang();
            a2.Chuoi = "Giao hàng nhanh";
            listt2.Add(a2);
            a2 = new tinhtrang();
            a2.Chuoi = "Giao Hàng Tiết Kiệm";
            listt2.Add(a2);
            a2 = new tinhtrang();
            a2.Chuoi = "Việt Nam Spot";
            listt2.Add(a2);

            ViewBag.httt = new SelectList(listt1, "chuoi", "chuoi", item.HTThanhToan);
            ViewBag.htgh = new SelectList(listt2, "chuoi", "chuoi", item.HTGiaoHang);
            ViewBag.ngay = Convert.ToDateTime(item.NgayGiaoHang).ToString("MM/dd/yyyy");
            return View(item);
        }
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult capnhatdh(int id, FormCollection f)
        {
            if (ModelState.IsValid)
            {


                var list = db.donhangs.SingleOrDefault(dh => dh.madonhang == id);
                list.HTGiaoHang = f["htgh"];
                list.HTThanhToan = f["httt"];

                list.tinhtrang = f["ttrang"];


                list.NgayGiaoHang = DateTime.Parse(Convert.ToDateTime(f.Get("ngaygiaohang")).ToString("MM/dd/yyyy"));
                db.SubmitChanges();
                return RedirectToAction("donhang");
            }
            return View();
        }
        public ActionResult leftmenu()
        {
            return PartialView();
        }
        public ActionResult formthongke()
        {
            return View();
        }


        public ActionResult thongke(FormCollection f)
        {
            List<tinhtrang> list = new List<tinhtrang>();
            tinhtrang a = new tinhtrang();
            a.Chuoi = "bar";
            list.Add(a);
            a = new tinhtrang();
            a.Chuoi = "horizontalBar";
            list.Add(a);
            a = new tinhtrang();
            a.Chuoi = "pie";
            list.Add(a);
            a = new tinhtrang();
            a.Chuoi = "line";
            list.Add(a);
            a = new tinhtrang();
            a.Chuoi = "doughnut";
            list.Add(a);
            a = new tinhtrang();
            a.Chuoi = "radar";
            list.Add(a);
            a = new tinhtrang();
            a.Chuoi = "polarArea";
            list.Add(a);
            ViewBag.loaibieudo = new SelectList(list, "chuoi", "chuoi");
            if (!string.IsNullOrEmpty(f["ngaylap"]) && !string.IsNullOrEmpty(f["ngaylapcuoi"]))
            {
                var ngaygiao = String.Format("{0:MM/dd/yyyy}", f["ngaylap"]);
                var ngaygiaocuoi = String.Format("{0:MM/dd/yyyy}", f["ngaylapcuoi"]);
                DateTime ngaylap = Convert.ToDateTime(ngaygiao).Date;
                DateTime ngaylapcuoi = Convert.ToDateTime(ngaygiaocuoi).Date;

                //DateTime ngaylap = Convert.ToDateTime(f["ngaylap"]).Date;
                //DateTime ngaylapcuoi = Convert.ToDateTime(f["ngaylapcuoi"]).Date;
                ViewBag.ngaylap = ngaylap.ToString("yyyy-MM-dd");
                ViewBag.ngaylapcuoi = ngaylapcuoi.ToString("yyyy-MM-dd");
                if (!string.IsNullOrEmpty(f["loaibieudo"]))
                {

                    ViewBag.loaibieudo = new SelectList(list, "chuoi", "chuoi", f["loaibieudo"]);
                    ViewBag.loai = f["loaibieudo"];
                }

                var donhang1 = db.donhangs.Where(ab => ab.ngaylap.Date >= ngaylap && ab.ngaylap.Date <= ngaylapcuoi).GroupBy(ab => ab.ngaylap).ToList();
                //List<donhang> list = (List<donhang>)ViewBag.donhang;
                //ViewBag.donhang = donhang;
                /*
                var kq = from s in db.donhangs
                         group s by new { s.ngaylap } into g
                         select new groupin
                         {
                             date = g.Key.ngaylap,

                             Count = g.Count(),
                         };
                */
                var kq = from s in db.donhangs
                             //join cd in db.chitietdonhangs on s.madonhang equals cd.madonhang
                         where s.ngaylap.Date >= ngaylap && s.ngaylap.Date <= ngaylapcuoi
                         orderby s.ngaylap
                         group s by new { s.ngaylap.Date } into g
                         select new groupin
                         {
                             date = g.Key.Date,
                             Count = g.Count(),
                         };
                return View(kq);
            }
            return View();









        }
        public ActionResult thongketien(FormCollection f)
        {


            if (!string.IsNullOrEmpty(f["ngaylap"]) && !string.IsNullOrEmpty(f["ngaylapcuoi"]))
            {
                var ngaygiao = String.Format("{0:MM/dd/yyyy}", f["ngaylap"]);
                var ngaygiaocuoi = String.Format("{0:MM/dd/yyyy}", f["ngaylapcuoi"]);
                DateTime ngaylap = Convert.ToDateTime(ngaygiao).Date;
                DateTime ngaylapcuoi = Convert.ToDateTime(ngaygiaocuoi).Date;

                //DateTime ngaylap = Convert.ToDateTime(f["ngaylap"]).Date;
                //DateTime ngaylapcuoi = Convert.ToDateTime(f["ngaylapcuoi"]).Date;
                ViewBag.ngaylap = ngaylap.ToString("yyyy-MM-dd");
                ViewBag.ngaylapcuoi = ngaylapcuoi.ToString("yyyy-MM-dd");


                var donhang1 = db.donhangs.Where(ab => ab.ngaylap.Date >= ngaylap && ab.ngaylap.Date <= ngaylapcuoi).GroupBy(ab => ab.ngaylap).ToList();
                //List<donhang> list = (List<donhang>)ViewBag.donhang;
                //ViewBag.donhang = donhang;
                /*
                var kq = from s in db.donhangs
                         group s by new { s.ngaylap } into g
                         select new groupin
                         {
                             date = g.Key.ngaylap,

                             Count = g.Count(),
                         };
                */
                var kq = from s in db.donhangs
                             //join cd in db.chitietdonhangs on s.madonhang equals cd.madonhang
                         where s.ngaylap.Date >= ngaylap && s.ngaylap.Date <= ngaylapcuoi
                         orderby s.ngaylap
                         group s by new { s.ngaylap.Date } into g
                         select new groupin
                         {
                             date = g.Key.Date,
                             Sum = (decimal?)g.Sum(s => s.tongtien),
                         };
                return View(kq);
            }
            return View();









        }
        public ActionResult thongketienbdc(FormCollection f)
        {
            List<tinhtrang> list = new List<tinhtrang>();
            tinhtrang a = new tinhtrang();
            a.Chuoi = "bar";
            list.Add(a);
            a = new tinhtrang();
            a.Chuoi = "horizontalBar";
            list.Add(a);
            a = new tinhtrang();
            a.Chuoi = "pie";
            list.Add(a);
            a = new tinhtrang();
            a.Chuoi = "line";
            list.Add(a);
            a = new tinhtrang();
            a.Chuoi = "doughnut";
            list.Add(a);
            a = new tinhtrang();
            a.Chuoi = "radar";
            list.Add(a);
            a = new tinhtrang();
            a.Chuoi = "polarArea";
            list.Add(a);
            ViewBag.loaibieudo = new SelectList(list, "chuoi", "chuoi");
            if (!string.IsNullOrEmpty(f["ngaylap"]) && !string.IsNullOrEmpty(f["ngaylapcuoi"]))
            {
                var ngaygiao = String.Format("{0:MM/dd/yyyy}", f["ngaylap"]);
                var ngaygiaocuoi = String.Format("{0:MM/dd/yyyy}", f["ngaylapcuoi"]);
                DateTime ngaylap = Convert.ToDateTime(ngaygiao).Date;
                DateTime ngaylapcuoi = Convert.ToDateTime(ngaygiaocuoi).Date;

                //DateTime ngaylap = Convert.ToDateTime(f["ngaylap"]).Date;
                //DateTime ngaylapcuoi = Convert.ToDateTime(f["ngaylapcuoi"]).Date;
                ViewBag.ngaylap = ngaylap.ToString("yyyy-MM-dd");
                ViewBag.ngaylapcuoi = ngaylapcuoi.ToString("yyyy-MM-dd");

                if (!string.IsNullOrEmpty(f["loaibieudo"]))
                {

                    ViewBag.loaibieudo = new SelectList(list, "chuoi", "chuoi", f["loaibieudo"]);
                    ViewBag.loai = f["loaibieudo"];
                }


                var donhang1 = db.donhangs.Where(ab => ab.ngaylap.Date >= ngaylap && ab.ngaylap.Date <= ngaylapcuoi).GroupBy(ab => ab.ngaylap).ToList();
                //List<donhang> list = (List<donhang>)ViewBag.donhang;
                //ViewBag.donhang = donhang;
                /*
                var kq = from s in db.donhangs
                         group s by new { s.ngaylap } into g
                         select new groupin
                         {
                             date = g.Key.ngaylap,

                             Count = g.Count(),
                         };
                */
                var kq = from s in db.donhangs
                             //join cd in db.chitietdonhangs on s.madonhang equals cd.madonhang
                         where s.ngaylap.Date >= ngaylap && s.ngaylap.Date <= ngaylapcuoi
                         orderby s.ngaylap
                         group s by new { s.ngaylap.Date } into g
                         select new groupin
                         {
                             date = g.Key.Date,
                             Sum = (decimal?)g.Sum(s => s.tongtien),
                         };
                if (kq != null)
                {

                    return View(kq);
                }
                else
                {
                    ViewBag.loai = "";
                    return View();
                }

            }
            return View();


        }
        public ActionResult thongkesp(FormCollection f)
        {
            List<tinhtrang> list = new List<tinhtrang>();
            tinhtrang a = new tinhtrang();
            a.Chuoi = "bar";
            list.Add(a);
            a = new tinhtrang();
            a.Chuoi = "horizontalBar";
            list.Add(a);
            a = new tinhtrang();
            a.Chuoi = "pie";
            list.Add(a);
            a = new tinhtrang();
            a.Chuoi = "line";
            list.Add(a);
            a = new tinhtrang();
            a.Chuoi = "doughnut";
            list.Add(a);
            a = new tinhtrang();
            a.Chuoi = "radar";
            list.Add(a);
            a = new tinhtrang();
            a.Chuoi = "polarArea";
            list.Add(a);
            ViewBag.loaibieudo = new SelectList(list, "chuoi", "chuoi");
            var list3 = db.danhmucsanphams.ToList();

            ViewBag.loaidanhmuc = new SelectList(list3, "madm", "tendanhmuc");
            if (!string.IsNullOrEmpty(f["ngaylap"]) && !string.IsNullOrEmpty(f["ngaylapcuoi"]))
            {
                var ngaygiao = String.Format("{0:MM/dd/yyyy}", f["ngaylap"]);
                var ngaygiaocuoi = String.Format("{0:MM/dd/yyyy}", f["ngaylapcuoi"]);
                DateTime ngaylap = Convert.ToDateTime(ngaygiao).Date;
                DateTime ngaylapcuoi = Convert.ToDateTime(ngaygiaocuoi).Date;

                //DateTime ngaylap = Convert.ToDateTime(f["ngaylap"]).Date;
                //DateTime ngaylapcuoi = Convert.ToDateTime(f["ngaylapcuoi"]).Date;
                ViewBag.ngaylap = ngaylap.ToString("yyyy-MM-dd");
                ViewBag.ngaylapcuoi = ngaylapcuoi.ToString("yyyy-MM-dd");
                if (!string.IsNullOrEmpty(f["loaibieudo"]))
                {

                    ViewBag.loaibieudo = new SelectList(list, "chuoi", "chuoi", f["loaibieudo"]);
                    ViewBag.loai = f["loaibieudo"];
                }

                var donhang1 = db.donhangs.Where(ab => ab.ngaylap.Date >= ngaylap && ab.ngaylap.Date <= ngaylapcuoi).GroupBy(ab => ab.ngaylap).ToList();
                //List<donhang> list = (List<donhang>)ViewBag.donhang;
                //ViewBag.donhang = donhang;
                /*
                var kq = from s in db.donhangs
                         group s by new { s.ngaylap } into g
                         select new groupin
                         {
                             date = g.Key.ngaylap,

                             Count = g.Count(),
                         };
                */
                var kq = from s in db.sanphams
                             //join cd in db.chitietdonhangs on s.madonhang equals cd.madonhang
                             //where s.ngaycapnhat.Date >= ngaylap && s.ngaycapnhat.Date <= ngaylapcuoi
                             //orderby s.ngaycapnhat
                         orderby s.masp
                         group s by new { s.madm } into g
                         select new groupin
                         {
                             Id = g.Key.madm.ToString(),
                             Count = g.Count(),
                         };
                return View(kq);
            }
            else
            {
                ViewBag.loai = "bar";
                if (!string.IsNullOrEmpty(f["loaibieudo"]))
                {

                    ViewBag.loaibieudo = new SelectList(list, "chuoi", "chuoi", f["loaibieudo"]);
                    ViewBag.loai = f["loaibieudo"];
                }
                var kq = from s in db.sanphams
                         join cd in db.chitietdonhangs on s.masp equals cd.masp
                         //where s.ngaycapnhat.Date >= ngaylap && s.ngaycapnhat.Date <= ngaylapcuoi
                         //orderby s.ngaycapnhat
                         orderby s.masp
                         group cd by new { s.masp, s.tensanpham } into g

                         select new groupin
                         {
                             Id = g.Key.tensanpham.ToString(),
                             Count = g.Count(),
                             Sum = g.Sum(cd => cd.thanhtien),
                         };
                return View(kq);

            }











        }
        public ActionResult thongkesp1(FormCollection f)
        {
            List<tinhtrang> list = new List<tinhtrang>();
            tinhtrang a = new tinhtrang();
            a.Chuoi = "bar";
            list.Add(a);
            a = new tinhtrang();
            a.Chuoi = "horizontalBar";
            list.Add(a);
            a = new tinhtrang();
            a.Chuoi = "pie";
            list.Add(a);
            a = new tinhtrang();
            a.Chuoi = "line";
            list.Add(a);
            a = new tinhtrang();
            a.Chuoi = "doughnut";
            list.Add(a);
            a = new tinhtrang();
            a.Chuoi = "radar";
            list.Add(a);
            a = new tinhtrang();
            a.Chuoi = "polarArea";
            list.Add(a);
            ViewBag.loaibieudo = new SelectList(list, "chuoi", "chuoi");
            var list3 = db.danhmucsanphams.ToList();

            ViewBag.loaidanhmuc = new SelectList(list3, "madm", "tendanhmuc");

            if (!string.IsNullOrEmpty(f["ngaytimkiem"]))
            {
                var kq1 = from s in db.sanphams
                          join cd in db.chitietdonhangs on s.masp equals cd.masp
                          where s.tensanpham.Contains(f["ngaytimkiem"])
                          //orderby s.ngaycapnhat
                          orderby s.masp
                          group cd by new { s.masp, s.tensanpham, s.dongia } into g

                          select new groupin
                          {
                              Id = g.Key.masp.ToString(),
                              dongia = (int)g.Key.dongia,
                              Name = g.Key.tensanpham,
                              Count = g.Count(),
                              Sum = Convert.ToDecimal(g.Sum(cd => cd.thanhtien).ToString()),


                          };
                ViewBag.timkiem = f["ngaytimkiem"];
                return View(kq1);
            }
            else
            {
                var kq = from s in db.sanphams
                         join cd in db.chitietdonhangs on s.masp equals cd.masp
                         //where s.ngaycapnhat.Date >= ngaylap && s.ngaycapnhat.Date <= ngaylapcuoi
                         //orderby s.ngaycapnhat
                         orderby s.masp
                         group cd by new { s.masp, s.tensanpham, s.dongia } into g

                         select new groupin
                         {
                             Id = g.Key.masp.ToString(),
                             dongia = (int)g.Key.dongia,
                             Name = g.Key.tensanpham,
                             Count = g.Count(),
                             Sum = Convert.ToDecimal(g.Sum(cd => cd.thanhtien).ToString()),


                         };
                return View(kq);
            }

            }

        
        public ActionResult xemhetsptk()
        {
            return RedirectToAction("thongkesp1");
        }


    }
}