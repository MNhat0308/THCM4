using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace THCM4
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            Application["SoNguoiTruyCap"] = 0;
            Application["Online"] = 0;
        }
        protected void Session_start()
        {
            Application.Lock();
            Application["SoNguoiTruyCap"] = (int)Application["SoNguoiTruyCap"] + 1;
            Application["Online"] = (int)Application["Online"] + 1;
            Application.UnLock();

        }
        protected void Session_end()
        {
            Application.Lock();
     
            Application["Online"] = (int)Application["Online"] - 1;
            Application.UnLock();
        }

        protected void Application_AuthenticateRequest(Object sender,EventArgs e)
        {
            var TkCookie = Context.Request.Cookies[FormsAuthentication.FormsCookieName];
            if(TkCookie!=null)
           {
                var XacNhanPQ = FormsAuthentication.Decrypt(TkCookie.Value);
                var Quyen = XacNhanPQ.UserData.Split(new Char[] { ',' });
                var userPrincipal = new GenericPrincipal(new GenericIdentity(XacNhanPQ.Name), Quyen);
                Context.User = userPrincipal;
            }   
        }
    }
}
