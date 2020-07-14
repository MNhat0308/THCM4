using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using THCM4.Models;

namespace THCM4.Controllers
{
    public class QuanLyQuyenController : Controller
    {
        // GET: QuanLyQuyen
        WebSite1Entities dt = new WebSite1Entities();
        public ActionResult Index()
        {

            return View(dt.Quyen);
        }
        [HttpGet]
        public ActionResult TaoMoi()
        {
            return View();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult TaoMoi(Quyen Q)
        {
            dt.Quyen.Add(Q);
            dt.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult ChinhSua(string Ma)
        {
            if (Ma == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            Quyen Q = dt.Quyen.SingleOrDefault(n => n.MaQuyen == Ma);
            if (Ma == null)
            {
                return HttpNotFound();

            }
            return View(Q);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult ChinhSua(Quyen Q)
        {
            dt.Entry(Q).State = System.Data.Entity.EntityState.Modified;
            dt.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Xoa(string Ma)
        {
            if (Ma == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            Quyen Q = dt.Quyen.SingleOrDefault(n => n.MaQuyen == Ma);
            if (Q == null)
            {
                return HttpNotFound();

            }

            return View(Q);
        }
        [HttpPost]
        public ActionResult Xoa1(string Ma)
        {
            if (Ma == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            Quyen Q = dt.Quyen.SingleOrDefault(n => n.MaQuyen == Ma);
            if (Q == null)
            {
                return HttpNotFound();

            }
            dt.Quyen.Remove(Q);
            dt.SaveChanges();
            return RedirectToAction("Index");

        }

    }
}