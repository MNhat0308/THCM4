using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using THCM4.Models;

namespace THCM4.Controllers
{
    //[Authorize(Roles = "AD,TK")]
    public class ThongKeController : Controller
    {
        // GET: ThongKe
        WebSite1Entities dt = new WebSite1Entities();
        public ActionResult Index()
        {
            ViewBag.SoNguoiTruyCap = HttpContext.Application["SoNguoiTruyCap"].ToString();
            ViewBag.SoNguoiOnline = HttpContext.Application["Online"].ToString();
            ViewBag.TongDoanhThu = TongDoanhThu().ToString("#,##");
            ViewBag.DH = DonHang();
            ViewBag.TV = SoLuongThanhVien();
          
            return View();
        }
        public decimal TongDoanhThu()
        {
            decimal tong = dt.CTDH.Sum(n => n.Soluong * n.Dongia).Value;
            tong = Convert.ToInt32(tong / 1000000);
            return tong;
        }
        public decimal TongDoanhThuTheoNăm(int Thang,int Nam)
        {
            var lstDDH = dt.DonDatHang.Where(n => n.NgayDat.Value.Month == Thang && n.NgayDat.Value.Year == Nam);
            decimal tong = 0;
            foreach (var item in lstDDH)
            {
                tong += Convert.ToDecimal( item.CTDH.Sum(n => n.Soluong * n.Dongia));
            }
            return tong;
        }
        public double DonHang()
        {
            double dh = dt.DonDatHang.Count();
            return dh;
        }
        public double SoLuongThanhVien()
        {
            double tv = dt.ThanhVien.Count();
            return tv;
        }

    }
}