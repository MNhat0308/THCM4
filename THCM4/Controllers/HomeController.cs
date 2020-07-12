using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using THCM4.Models;
using CaptchaMvc.HtmlHelpers;
using CaptchaMvc;
using System.Web.Security;
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
            //string Tk = f["txtTK"].ToString();
            //string Pw = f["txtMK"].ToString();
            //ThanhVien TV = dt.ThanhVien.SingleOrDefault(n => n.TaiKhoan == Tk && n.MatKhau == Pw);
            //if(TV!=null)
            //{
            //    Session["TaiKhoan"] = TV;
            //    return RedirectToAction("Index");
            //}


            //return Content("Tài Khoản Hoặc Mật Khẩu Sai");

            string Tk = f["txtTK"].ToString();
            string Pw = f["txtMK"].ToString();
            ThanhVien TV = dt.ThanhVien.SingleOrDefault(n => n.TaiKhoan == Tk && n.MatKhau == Pw);
            if (TV != null)
            {
                var lstQuyen = dt.LoaiTV_Quyen.Where(n => n.MaLoaiTV == TV.MaLoaiTV);
                string Quyen = "";
                if(lstQuyen.Count()!=0)
                {
                    foreach (var item in lstQuyen)
                    {
                        Quyen += item.Quyen.MaQuyen + ",";
                    }
                    Quyen = Quyen.Substring(0, Quyen.Length - 1);
                    PhanQuyen(TV.TaiKhoan.ToString(), Quyen);
                    Session["TaiKhoan"] = TV;
                    return RedirectToAction("Index");
                }
               
            }
            return Content("Tài Khoản Hoặc Mật Khẩu Sai");
        }
        public void PhanQuyen(string TaiKhoan ,string Quyen)
        {
            FormsAuthentication.Initialize();
            var PQ = new FormsAuthenticationTicket(
                1,
                TaiKhoan, //user
                DateTime.Now, //bat dau
                DateTime.Now.AddHours(2),//ket thuc
                false,
                Quyen,
                FormsAuthentication.FormsCookieName);
            var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, FormsAuthentication.Encrypt(PQ));
            if(PQ.IsPersistent)
            {
                cookie.Expires = PQ.Expiration;
            }
            Response.Cookies.Add(cookie);
        }
        public ActionResult DangXuat()
        {
            Session["TaiKhoan"] = null;
            FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }
        public ActionResult LoiPQ()
        {
            return View();
        }
      


    }
}