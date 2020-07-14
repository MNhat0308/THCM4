using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using THCM4.Models;
using PagedList;

namespace THCM4.Controllers
{
    public class TimKiemController : Controller
    {
        WebSite1Entities dt = new WebSite1Entities();
        [HttpGet]
        public ActionResult Search(string stukhoa,int? page)
        {
            if(Request.HttpMethod!="GET")
            {
                page = 1;
            }
            int SanPhamTrang = 2;
            int SoTrang = (page ?? 1);

            var lstSp = dt.SanPham.Where(n => n.TenSP.Contains(stukhoa));
            ViewBag.tukhoa = stukhoa;
            return View(lstSp.OrderBy(n=>n.TenSP).ToPagedList(SoTrang,SanPhamTrang));
        }
        [HttpPost]
        //public ActionResult LayKetQuaTimKiem(string tukhoa)
        //{
        //    return RedirectToAction("Search", new { @stukhoa = tukhoa });
        //}
        public ActionResult Search(string stukhoa, int? page,FormCollection f)
        {
            if (Request.HttpMethod != "GET")
            {
                page = 1;
            }
            int SanPhamTrang = 2;
            int SoTrang = (page ?? 1);

            var lstSp = dt.SanPham.Where(n => n.TenSP.Contains(stukhoa));
            ViewBag.tukhoa = stukhoa;
            return View(lstSp.OrderBy(n => n.TenSP).ToPagedList(SoTrang, SanPhamTrang));
        }
    }
}