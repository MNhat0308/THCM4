using System;
using System.Collections.Generic;
using System.IO;

using System.Linq;
using System.Web;
using System.Web.Mvc;
using THCM4.Models;

namespace THCM4.Controllers
{
    //[Authorize(Roles = "AD,QLSP")]
 
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
        public ActionResult TaoSP(SanPham sp, HttpPostedFileBase HinhAnh)

        {
            //load combobox ncc,nsx,loaisp
            ViewBag.MaNCC = new SelectList(dt.NCC.OrderBy(n => n.TenNCC), "MaNCC", "TenNCC");
            ViewBag.MaLoaiSP = new SelectList(dt.LoaiSP.OrderBy(n => n.TenLoai), "MaLoaiSP", "TenLoai");
            ViewBag.MaNSX = new SelectList(dt.NSX.OrderBy(n => n.TenNSX), "MaNSX", "TenNSX");
            
            
            if (HinhAnh == null)
            {
                ViewBag.UpAnhLoi1 = "Phải Chọn Ảnh";
                return View();
            }
            //kiem tra anh da ton tai chua
            if (HinhAnh.ContentLength > 0)
            {
                if (HinhAnh.ContentType != "image/jpeg" && HinhAnh.ContentType != "image/png" && HinhAnh.ContentType != "image/jpg" && HinhAnh.ContentType != "image/gif")
                {
                    ViewBag.UpAnhloi = "Ảnh không hợp lệ";
                }
                else
                {
                    // dua anh vao duong dan
                    var filename = Path.GetFileName(HinhAnh.FileName);
                
                    var path = Path.Combine(Server.MapPath("~/Content/AnhData"), filename  );
                    
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


            }
            dt.SanPham.Add(sp);
            dt.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult ChinhSua(int ? id)
        {
            if(id==null)
            {
                Response.StatusCode = 404;
                return null;
            }
            SanPham sp = dt.SanPham.SingleOrDefault(n => n.MaSP == id);
            if (sp == null)
            {
                return HttpNotFound();

            }   
            ViewBag.MaNCC = new SelectList(dt.NCC.OrderBy(n => n.TenNCC), "MaNCC", "TenNCC",sp.MaNCC);
            ViewBag.MaLoaiSP = new SelectList(dt.LoaiSP.OrderBy(n => n.TenLoai), "MaLoaiSP", "TenLoai",sp.MaLoaiSP);
            ViewBag.MaNSX = new SelectList(dt.NSX.OrderBy(n => n.TenNSX), "MaNSX", "TenNSX",sp.MaNSX);
            return View(sp);
        }
        [ValidateInput(false)]
        [HttpPost]
       
        public ActionResult ChinhSua(SanPham sp)
        {
            ViewBag.MaNCC = new SelectList(dt.NCC.OrderBy(n => n.TenNCC), "MaNCC", "TenNCC", sp.MaNCC);
            ViewBag.MaLoaiSP = new SelectList(dt.LoaiSP.OrderBy(n => n.TenLoai), "MaLoaiSP", "TenLoai", sp.MaLoaiSP);
            ViewBag.MaNSX = new SelectList(dt.NSX.OrderBy(n => n.TenNSX), "MaNSX", "TenNSX", sp.MaNSX);

            //dt.Entry(sp).State = System.Data.Entity.EntityState.Modified;
            //dt.SaveChanges();
            //return RedirectToAction("Index");

            if (ModelState.IsValid)
            {
                dt.Entry(sp).State = System.Data.Entity.EntityState.Modified;
                dt.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(sp);
        }
        [HttpGet]
        public ActionResult Xoa(int ?id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            SanPham sp = dt.SanPham.SingleOrDefault(n => n.MaSP == id);
            if (sp == null)
            {
                return HttpNotFound();

            }
            ViewBag.MaNCC = new SelectList(dt.NCC.OrderBy(n => n.TenNCC), "MaNCC", "TenNCC", sp.MaNCC);
            ViewBag.MaLoaiSP = new SelectList(dt.LoaiSP.OrderBy(n => n.TenLoai), "MaLoaiSP", "TenLoai", sp.MaLoaiSP);
            ViewBag.MaNSX = new SelectList(dt.NSX.OrderBy(n => n.TenNSX), "MaNSX", "TenNSX", sp.MaNSX);
            return View(sp);
        }
        [HttpPost]
        public ActionResult Xoa(int id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            SanPham sp = dt.SanPham.SingleOrDefault(n => n.MaSP == id);
            if (sp == null)
            {
                return HttpNotFound();

            }
            dt.SanPham.Remove(sp);
            dt.SaveChanges();
            return RedirectToAction("Index");

        }

    }
}
