using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace THCM4.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        //[Authorize(Roles ="AD,QLDH,QLSP,QLQ")]
        [Authorize(Roles = "AD,QLDH,QLSP,QLQ")]
        public ActionResult Index()
        {
            return View();
        }
        
    }
}