using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WEBPC.Models;

namespace WEBPC.Models
{
    public class GioHang
    {
        WEBPCDataContext db = new WEBPCDataContext();
        private int masp;
        private string tensp;
        private string hinhanh;
        private double dongia;
        private int soluong;
        private double thanhtien;

        public int Masp { get => masp; set => masp = value; }
        public string Tensp { get => tensp; set => tensp = value; }
        public string Hinhanh { get => hinhanh; set => hinhanh = value; }
        public double Dongia { get => dongia; set => dongia = value; }
        public int Soluong { get => soluong; set => soluong = value; }
        public double Thanhtien { get => Dongia * Soluong; set => thanhtien = value; }

        public GioHang( int masp, string tensp, string hinhanh, double dongia, int soluong)
        {
            
            this.Masp = masp;
            this.Tensp = tensp;
            this.Hinhanh = hinhanh;
            this.Dongia = dongia;
            this.Soluong = soluong;
          

        }
        public GioHang()
        {

        }
   
    }
}