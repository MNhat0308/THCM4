using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using THCM4.Models;

namespace THCM4.Controllers
{
    public class NhapHangController : Controller
    {
        // GET: NhapHang
        WebSite1Entities dt = new WebSite1Entities();
        [HttpGet]
        public ActionResult NhapHang()
        {
            ViewBag.MaNCC = dt.NCC;
            ViewBag.lstsp = dt.SanPham;
            ViewBag.lstsp = dt.SanPham;
            return View();
        }
        [HttpPost]
        public ActionResult NhapHang(IEnumerable<CTPN> model)
        {
            return View();
        }

    }
    
}