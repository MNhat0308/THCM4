using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using THCM4.Models;
using CaptchaMvc.HtmlHelpers;
using CaptchaMvc;
namespace THCM4.Controllers
{
    public class HomeController : Controller
    {
        WebSite1Entities dt = new WebSite1Entities();
        public ActionResult Index()
        {
            var lstDTM = dt.SanPham.Where(n => n.MaLoaiSP == 1 && n.Loai == 0);
            ViewBag.ListDMT = lstDTM;

            var lstMTBM = dt.SanPham.Where(n => n.MaLoaiSP == 2 && n.Loai == 0);
            ViewBag.ListMTBM = lstMTBM;

          

            return View();
        }
        public ActionResult MenuPartial()
        {
            var lstSp = dt.SanPham;
            return PartialView(lstSp);
        }
        [HttpGet]
        public ActionResult DangKy()
        {
            return View();
        }
        [HttpPost]
        public ActionResult DangKy(ThanhVien TV)
        {
            if (this.IsCaptchaValid("Captcha Không hợp lệ"))
            {
                ViewBag.ThongBao = "Thành Công";
                dt.ThanhVien.Add(TV);
                dt.SaveChanges();
                return View();
            }
            ViewBag.ThongBao = "Nhập lại captcha";
            return View();

        }
        [HttpGet]
        public ActionResult DangKy1()
        {
            return View();
        }
     
        public ActionResult Login(FormCollection f)

        {
            string Tk = f["txtTK"].ToString();
            string Pw = f["txtMK"].ToString();
            ThanhVien TV = dt.ThanhVien.SingleOrDefault(n => n.TaiKhoan == Tk && n.MatKhau == Pw);
            if(TV!=null)
            {
                Session["TaiKhoan"] = TV;
                return RedirectToAction("Index");
            }
            

            return Content("Tài Khoản Hoặc Mật Khẩu Sai");
        }
        public ActionResult DangXuat()
        {
            Session["TaiKhoan"] = null;
            return RedirectToAction("Index");
        }
      


    }
}