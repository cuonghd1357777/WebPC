using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEBPC.Models;
using WEBPC.Controllers;

namespace WEBPC.Controllers
{
    public class GioHangController : Controller
    {
       
        WEBPCDataContext db = new WEBPCDataContext();
        // GET: GioHang
        public ActionResult GioHang()
        {
           


            List<GioHang> list = listgiohang();


            ViewBag.tongtien = tonggiatien();
            return View(list);
        }
        public List<GioHang> listgiohang()
        {
            List<GioHang> list = new List<GioHang>();
            list = Session["giohang"] as List<GioHang>;
            if (list == null)
            {
                list = new List<GioHang>();
                Session["giohang"] = new List<GioHang>();

            }
            return Session["giohang"] as List<GioHang>;
        }
        public int soluongsanpham()
        {
            List<GioHang> list = listgiohang();
            int i = 0;
            foreach(var item in list)
            {
                i++;
            }
            return i;
        }
        public double  tonggiatien()
        {
            List<GioHang> list = listgiohang();
            double s = 0;
            foreach (var item in list)
            {
                s = s + item.Thanhtien;
            }
            return s;
        }
        public ActionResult Add(int id,FormCollection f)
        {
            List<GioHang> list = listgiohang();
            double s = 0;
            GioHang sp = list.SingleOrDefault(n => n.Masp == id);
            if (sp != null)
            {
                sp.Soluong = sp.Soluong + 1;
                
            }
            return RedirectToAction("GioHang");

        }
        public ActionResult reduce(int id, FormCollection f)
        {
            List<GioHang> list = listgiohang();
            double s = 0;
            GioHang sp = list.SingleOrDefault(n => n.Masp == id);
            if (sp != null &&sp.Soluong!=1)
            {
               
                sp.Soluong = sp.Soluong - 1;
                
                

            }
            return RedirectToAction("GioHang");

        }
        public ActionResult themgiohang(int id, string Url)
        {
            sanpham sp = db.sanphams.SingleOrDefault(n => n.masp == id);
            List<GioHang> list = listgiohang();
            int i = 0;
           foreach(var item in list)
            {
                if (item.Masp == sp.masp)
                {
                    item.Soluong++;
                    i++;
                    break;

                }
            }
            if (i == 0)
            {
                GioHang gh = new GioHang(sp.masp, sp.tensanpham, sp.hinhanh, sp.dongia, 1);
                list.Add(gh);
            }
          
            
         
            return Redirect(Url);
           

        }
        public ActionResult GioHangPartial()
        {
            ViewBag.soluong = soluongsanpham();
           
            return PartialView();
        }
        
        public ActionResult xoagiohang(int id)
        {
            List<GioHang> list = listgiohang();
            list.RemoveAll(n => n.Masp == id);
            if (list.Count == 0)
            {
                return RedirectToAction("Index", "WEBPC");
            }
           
            return RedirectToAction("GioHang");
           
        }
        public ActionResult xoahetgiohang()
        {
            List<GioHang> list = listgiohang();
            list.Clear();
           
            return RedirectToAction("Index", "WEBPC");
            

          

        }
        [HttpGet]
         public ActionResult donhang()
        {
          
            List<GioHang> list = listgiohang();
            if (Session["taikhoankh"] == null || Session["taikhoankh"].ToString()=="")
            {
                Session["come"] = 1;
                return RedirectToAction("loginn", "WEBPC");
            }else if (list.Count == 0)
            {
                 return RedirectToAction("Index", "WEBPC");
            }
            else
            {
                khachhang kh = Session["taikhoankh"] as khachhang;
                ViewBag.hoten = kh.tenkh;
                ViewBag.diachi = kh.diachi;
                ViewBag.sodienthoai = kh.sodienthoai;
                var ngaylap = DateTime.Now.ToString("dd/MM/yyyy");
               
            }
           
            return View();
        }
        [HttpPost]
        public ActionResult donhang(FormCollection f)
        {
            khachhang kh1 = Session["taikhoankh"] as khachhang;
            ViewBag.hoten = kh1.tenkh;
            ViewBag.diachi = kh1.diachi;
            ViewBag.sodienthoai = kh1.sodienthoai;
           
            if (ModelState.IsValid)
            {


               
                var ngaygiao = String.Format("{0:MM/dd/yyyy}", f["ngaygiao"]);
                if (!string.IsNullOrEmpty(ngaygiao))
                {
                    DateTime ngaylap = Convert.ToDateTime(ngaygiao).Date;
                    ViewBag.ngaylap = ngaylap.ToString("yyyy-MM-dd");
                }
               
                if (string.IsNullOrEmpty(ngaygiao))
                {
                  
                    ViewBag.err1 = "Ngày giao hàng không được rỗng";
                   

                }
                else if (string.IsNullOrEmpty(f.Get("httt")))
                {
                    ViewBag.err2 = "Vui lòng chọn Hình thức thanh toán ";
                   
                }
                else if (string.IsNullOrEmpty(f.Get("vc")))
                {
                    ViewBag.err3 = "Vui lòng chọn Hình thức giao hàng ";
                    
                }
                else
                {
                    khachhang kh = Session["taikhoankh"] as khachhang;
                    donhang dh = new donhang();
                    dh.makh = kh.makh;
                    List<GioHang> list = listgiohang();

                    dh.ngaylap = DateTime.Now;
                    dh.tinhtrang = "Đơn hàng mới";

                    dh.NgayGiaoHang = DateTime.Parse(Convert.ToDateTime(f.Get("ngaygiao")).ToString("MM/dd/yyyy"));
                    dh.HTThanhToan = f.Get("httt");
                    dh.HTGiaoHang = f.Get("vc");

                    dh.tongtien = tonggiatien();
                    db.donhangs.InsertOnSubmit(dh);
                    db.SubmitChanges();
                    foreach (var item in list)
                    {
                        chitietdonhang ct = new chitietdonhang();
                        ct.madonhang = dh.madonhang;
                        ct.masp = item.Masp;
                        ct.soluong = item.Soluong;
                        ct.dongia = (int)Convert.ToDouble(item.Dongia);
                        ct.thanhtien = (int)Convert.ToDouble(item.Thanhtien);
                        db.chitietdonhangs.InsertOnSubmit(ct);
                    }
                    db.SubmitChanges();






                    list.Clear();

                    return RedirectToAction("xacnhandonhang", "GioHang");

                }
            }
            
            
                return View();
            
        }
        public ActionResult xacnhandonhang()
        {
            return View();
        }

    }
}