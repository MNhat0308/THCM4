using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using THCM4.Models;
using THCM4.Controllers;

namespace THCM4.Controllers
{
    public class GioHangController : Controller
    {
        WebSite1Entities dt = new WebSite1Entities();
        //lay gio hang
        public List<GioHang> LayGioHang()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if(lstGioHang==null)
            {
                lstGioHang  = new List<GioHang>();
                Session["GioHang"] = lstGioHang;
               
            }
            return lstGioHang;
        }
        //them gio hang
        public ActionResult ThemGioHang(int MaSP,string Surl)
        {
            //Kiem tra san pham co ton tai trong CSDL hay ko

            SanPham sp = dt.SanPham.SingleOrDefault(n => n.MaSP==MaSP);
            if(sp==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //Lay gio hang
            List<GioHang> lstGioHang = LayGioHang();
            //Truong hop 1:Neu Da Co SP trong gio hang>>>tang SoLuong
            GioHang spCheck = lstGioHang.SingleOrDefault(n => n.MaSP == MaSP);
            if(spCheck!=null)
            {
                if(sp.SoLuongTon<spCheck.SoLuong)
                {
                    return View("ThongBao");
                }
                spCheck.SoLuong++;
                spCheck.ThanhTien = spCheck.SoLuong * spCheck.DonGia;
                return Redirect(Surl);
            }
           
            GioHang iteamGH = new GioHang(MaSP);
            if (sp.SoLuongTon < iteamGH.SoLuong)
            {
                return View("ThongBao");
            }
            lstGioHang.Add(iteamGH);
            return Redirect(Surl);

        }
        public double TongSL()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if(lstGioHang==null)
            {
                return 0;
            }
            return lstGioHang.Sum(n=>n.SoLuong);

        }
        public decimal TongTien()
        {
            List<GioHang> lstGioHang = Session["GioHang"] as List<GioHang>;
            if (lstGioHang == null)
            {
                return 0;
            }
            return lstGioHang.Sum(n => n.ThanhTien);
        }




        // GET: GioHang
        public ActionResult XemGioHang()
        {
            List<GioHang> lstGioHang = LayGioHang();

            return View(lstGioHang);
        }
        public ActionResult GioHangPartial()
        {
            if(TongSL()==0)
            {
                ViewBag.TongSoLuong = 0;
                return PartialView();
            }
            ViewBag.TongSoLuong = TongSL();
            ViewBag.TongTien = TongTien();
            return PartialView();
        }
        public ActionResult ChinhSuaGioHang(int MaSP)
        {
            //Kiem tra
            if (Session["GioHang"]==null)
            {
                return RedirectToAction("Index","Home");
            }
            SanPham sp = dt.SanPham.SingleOrDefault(n => n.MaSP == MaSP);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //lay list san pham trong gio
            List<GioHang> lstGioHang = LayGioHang();
            GioHang spcheck = lstGioHang.SingleOrDefault(n => n.MaSP == MaSP);
            //kiem tra co sp trong list gio hang ko
            if(spcheck==null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.TB = lstGioHang;
            return View(spcheck);
        }
        [HttpPost]
        public ActionResult CapNhatGioHang(GioHang itemGH)
        {   
            //kiem tra so luon ton
            SanPham spcheck = dt.SanPham.Single(n => n.MaSP == itemGH.MaSP);
            if (spcheck.SoLuongTon < itemGH.SoLuong)
            {
                return View("ThongBao");
            }
            List<GioHang> lstGH = LayGioHang();
            //gan sp update vao so luong moi
            GioHang updateGH = lstGH.Find(n => n.MaSP == itemGH.MaSP);
            updateGH.SoLuong = itemGH.SoLuong;
            updateGH.ThanhTien = itemGH.SoLuong * updateGH.DonGia;
            return RedirectToAction("XemGioHang");

        }
        public ActionResult XoaGioHang(int MaSP)
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            SanPham sp = dt.SanPham.SingleOrDefault(n => n.MaSP == MaSP);
            if (sp == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            //lay list san pham trong gio
            List<GioHang> lstGioHang = LayGioHang();
            GioHang spcheck = lstGioHang.SingleOrDefault(n => n.MaSP == MaSP);
            //kiem tra co sp trong list gio hang ko
            if (spcheck == null)
            {
                return RedirectToAction("Index", "Home");
            }
            lstGioHang.Remove(spcheck);
            return RedirectToAction("XemGioHang");
        }

    }
}