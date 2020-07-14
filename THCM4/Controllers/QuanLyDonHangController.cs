using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using THCM4.Models;

namespace THCM4.Controllers
{
    public class QuanLyDonHangController : Controller
    {
        // GET: QuanLyDonHang
        WebSite1Entities dt = new WebSite1Entities();
        public ActionResult ChuaThanhToan()
        {
            var lstChuaThanhToan = dt.DonDatHang.Where(n => n.DaThanhToan == false);

            return View(lstChuaThanhToan);
        }
        public ActionResult DaThanhToan()
        {
            var lstDaThanhToan = dt.DonDatHang.Where(n => n.DaThanhToan == true);

            return View(lstDaThanhToan);
        }
        public ActionResult DaGiaoThanhToan()
        {
            var lstThanhToan = dt.DonDatHang.Where(n => n.DaThanhToan == true &&n.Dagiao==true);

            return View(lstThanhToan);
        }
        [HttpGet]
        public ActionResult DuyetDonHang(int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DonDatHang ddh = dt.DonDatHang.SingleOrDefault(n=>n.MaDDH==id);
            if(ddh==null)
            {
                return HttpNotFound();
            }
            var lstChiTietDonHang = dt.CTDH.Where(n => n.MaDDH == id);
            ViewBag.lstCTDH = lstChiTietDonHang;
            return View(ddh);
        }
        [HttpPost]
        public ActionResult DuyetDonHang(DonDatHang ddh)
        {
            DonDatHang ddhUpdate = dt.DonDatHang.SingleOrDefault(n => n.MaDDH == ddh.MaDDH);
            ddhUpdate.DaThanhToan = ddh.DaThanhToan;
            ddhUpdate.Dagiao = ddh.Dagiao;
            dt.SaveChanges();
            var lstChiTietDonHang = dt.CTDH.Where(n => n.MaDDH == ddh.MaDDH);
            ViewBag.lstCTDH = lstChiTietDonHang;
            return View(ddhUpdate);
        }
    }
}