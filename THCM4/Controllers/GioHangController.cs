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
        public List<ItemGioHang> LayGioHang()
        {
            List<ItemGioHang> lstGioHang = Session["GioHang"] as List<ItemGioHang>;
            if(lstGioHang==null)
            {
                lstGioHang  = new List<ItemGioHang>();
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
            List<ItemGioHang> lstGioHang = LayGioHang();
            //Truong hop 1:Neu Da Co SP trong gio hang>>>tang SoLuong
            ItemGioHang spCheck = lstGioHang.SingleOrDefault(n => n.MaSP == MaSP);
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
           
            ItemGioHang iteamGH = new ItemGioHang(MaSP);
            if (sp.SoLuongTon < iteamGH.SoLuong)
            {
                return View("ThongBao");
            }
            lstGioHang.Add(iteamGH);
            return Redirect(Surl);

        }
        public double TongSL()
        {
            List<ItemGioHang> lstGioHang = Session["GioHang"] as List<ItemGioHang>;
            if(lstGioHang==null)
            {
                return 0;
            }
            return lstGioHang.Sum(n=>n.SoLuong);

        }
        public decimal TongTien()
        {
            List<ItemGioHang> lstGioHang = Session["GioHang"] as List<ItemGioHang>;
            if (lstGioHang == null)
            {
                return 0;
            }
            return lstGioHang.Sum(n => n.ThanhTien);
        }




        // GET: GioHang
        public ActionResult XemGioHang()
        {
            List<ItemGioHang> lstGioHang = LayGioHang();

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
            List<ItemGioHang> lstGioHang = LayGioHang();
            ItemGioHang spcheck = lstGioHang.SingleOrDefault(n => n.MaSP == MaSP);
            //kiem tra co sp trong list gio hang ko
            if(spcheck==null)
            {
                return RedirectToAction("Index", "Home");
            }
            ViewBag.TB = lstGioHang;
            return View(spcheck);
        }
        [HttpPost]
        public ActionResult CapNhatGioHang(ItemGioHang itemGH)
        {   
            //kiem tra so luon ton
            SanPham spcheck = dt.SanPham.Single(n => n.MaSP == itemGH.MaSP);
            if (spcheck.SoLuongTon < itemGH.SoLuong)
            {
                return View("ThongBao");
            }
            List<ItemGioHang> lstGH = LayGioHang();
            //gan sp update vao so luong moi
            ItemGioHang updateGH = lstGH.Find(n => n.MaSP == itemGH.MaSP);
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
            List<ItemGioHang> lstGioHang = LayGioHang();
            ItemGioHang spcheck = lstGioHang.SingleOrDefault(n => n.MaSP == MaSP);
            //kiem tra co sp trong list gio hang ko
            if (spcheck == null)
            {
                return RedirectToAction("Index", "Home");
            }
            lstGioHang.Remove(spcheck);
            return RedirectToAction("XemGioHang");
        }
        public ActionResult DatHang(KhachHang kh)
        {
            if (Session["GioHang"] == null)
            {
                return RedirectToAction("Index", "Home");
            }
            KhachHang Khach = new KhachHang();
            if(Session["TaiKhoan"]==null)
            {
                //khach chua dang ky
                Khach = kh;
                dt.KhachHang.Add(Khach);
                dt.SaveChanges();
            }
            else
            {
                // khach da dang ky la thanh vien
                ThanhVien tv =  Session["TaiKhoan"] as ThanhVien;
                Khach.HoTenKH = tv.HoTen;
                Khach.SdtKH = tv.Sdt;
                Khach.EmailKH = tv.Email;
                Khach.DiaChiKH = tv.DiaChi;
                Khach.MaTV = tv.MaTV;
                dt.KhachHang.Add(Khach);
                dt.SaveChanges();
            }
            //Them Don Hang
            DonDatHang ddh = new DonDatHang();
            ddh.MaKH = Khach.MaKH; 
            ddh.NgayDat = DateTime.Now;
            ddh.NgayGiao = DateTime.Now.AddDays(1);
            ddh.TinhTrangGiaoHang = false;
            ddh.UuDai = 0;
            ddh.Dagiao = false;
            ddh.DaThanhToan = false;
            dt.DonDatHang.Add(ddh);
            dt.SaveChanges();
            //chi tiet
            List<ItemGioHang> lstGioHang = LayGioHang();
            foreach (var item in lstGioHang)
            {
                CTDH ctdh = new CTDH();
                ctdh.MaDDH = ddh.MaDDH;
                ctdh.MaSP = item.MaSP;
                ctdh.TenSP = item.TenSP;
                ctdh.Soluong = item.SoLuong;
                ctdh.Dongia = item.DonGia;
                dt.CTDH.Add(ctdh);
                
            }
            dt.SaveChanges();
            Session["GioHang"] = null;


            return RedirectToAction("XemGioHang");
        }

    }
}