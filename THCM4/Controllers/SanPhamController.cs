using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using THCM4.Controllers;
using THCM4.Models;
using System.Net;
using PagedList;

namespace THCM4.Controllers
{
    public class SanPhamController : Controller
    {
        // GET: SanPham
        WebSite1Entities dt =new WebSite1Entities();
        public ActionResult SanPhamPartial ()
        {
            return PartialView();
        }
        //detail
        public ActionResult XemChiTiet (int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sp = dt.SanPham.SingleOrDefault(n => n.MaSP == id);
            if(sp==null)
            {
                return HttpNotFound();
            }
            return View(sp);

        }
        public ActionResult LoadSanPham(int? MaLoaiSP,int? MaNSX,int? SoTrang)
        {
            if (MaLoaiSP == null||MaNSX==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var lstSP = dt.SanPham.Where(n => n.MaLoaiSP == MaLoaiSP && n.MaNSX == MaNSX);
            if(lstSP.Count()==0)
            {
                return HttpNotFound();
            }
            //phan trang
            int SanPhamPage = 2;
            int pageHienTai = (SoTrang ?? 1);
            ViewBag.MaLoaiSP = MaLoaiSP;
            ViewBag.MaNSX = MaNSX;
            return View(lstSP.OrderBy(n => n.NgayCapNhat).ToPagedList(pageHienTai, SanPhamPage));
            //return View(lstSP);
        }
    }
}