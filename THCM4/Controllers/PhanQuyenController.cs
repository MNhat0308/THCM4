using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using THCM4.Models;

namespace THCM4.Controllers
{
    [Authorize(Roles = "AD,QLQ")]
    public class PhanQuyenController : Controller
    {
        WebSite1Entities dt = new WebSite1Entities();
        // GET: PhanQuyen
        public ActionResult Index()
        {
            return View(dt.LoaiTV.OrderBy(n=>n.TenLoai));
        }
        [HttpGet]
        public ActionResult PhanQuyen(int? id)
        {
            if (id == null)
            {
                Response.StatusCode = 404;
                return null;
            }
            var lst = dt.LoaiTV.SingleOrDefault(n => n.MaLoaiTV == id);
            if(lst==null)
            {
                return HttpNotFound();
            }
            ViewBag.DsQuyen = dt.Quyen;
            ViewBag.LoaiTVQuyen = dt.LoaiTV_Quyen.Where(n => n.MaLoaiTV == id);
            return View(lst);

        }
        [HttpPost]
        public ActionResult PhanQuyen(int ? MaLTV,IEnumerable<LoaiTV_Quyen>lstPhanQuyen)
        {
            //Phan lai quyen
            var lstDaPhanQuyen = dt.LoaiTV_Quyen.Where(n => n.MaLoaiTV == MaLTV);
            if (lstDaPhanQuyen.Count() != 0)
            {
                dt.LoaiTV_Quyen.RemoveRange(lstDaPhanQuyen);
                dt.SaveChanges();
            }
            foreach (var item in lstPhanQuyen)
            {

                item.MaLoaiTV = int.Parse(MaLTV.ToString());
                    dt.LoaiTV_Quyen.Add(item);
     
            }
            dt.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}