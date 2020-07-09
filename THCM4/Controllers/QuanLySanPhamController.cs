using System;
using System.Collections.Generic;
using System.IO;

using System.Linq;
using System.Web;
using System.Web.Mvc;
using THCM4.Models;

namespace THCM4.Controllers
{
    public class QuanLySanPhamController : Controller
    {
        // GET: QuanLySanPham
        WebSite1Entities dt = new WebSite1Entities();
        public ActionResult Index()
        {

            return View(dt.SanPham);
        }
        [HttpGet]
        public ActionResult TaoSP()
        {
            ViewBag.MaNCC = new SelectList(dt.NCC.OrderBy(n => n.TenNCC), "MaNCC", "TenNCC");
            ViewBag.MaLoaiSP = new SelectList(dt.LoaiSP.OrderBy(n => n.TenLoai), "MaLoaiSP", "TenLoai");
            ViewBag.MaNSX = new SelectList(dt.NSX.OrderBy(n => n.TenNSX), "MaNSX", "TenNSX");  

            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult  TaoSP(SanPham sp ,HttpPostedFileBase HinhAnh)

        {
            //load combobox ncc,nsx,loaisp
            ViewBag.MaNCC = new SelectList(dt.NCC.OrderBy(n => n.TenNCC), "MaNCC", "TenNCC");
            ViewBag.MaLoaiSP = new SelectList(dt.LoaiSP.OrderBy(n => n.TenLoai), "MaLoaiSP", "TenLoai");
            ViewBag.MaNSX = new SelectList(dt.NSX.OrderBy(n => n.TenNSX), "MaNSX", "TenNSX");
            //kiem tra anh da ton tai chua
            if(HinhAnh.ContentLength>0)
            {
                // dua anh vao duong dan
                var filename = Path.GetFileName(HinhAnh.FileName);
                var path = Path.Combine(Server.MapPath("~/Content/AnhData"), filename);
                // kiem tra anh co ton tai ko
                if (System.IO.File.Exists(path))
                {
                    ViewBag.UpAnh = "Ảnh đã tồn tại";
                    return View();
                }
                else
                {
                    //luu anh
                    HinhAnh.SaveAs(path);
                    sp.HinhAnh = filename;
                }
                
            }
            dt.SanPham.Add(sp);
            dt.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}