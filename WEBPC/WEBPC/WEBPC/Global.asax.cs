using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WEBPC
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            Application["online"] = 0;
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            System.IO.StreamReader read = new System.IO.StreamReader(HttpContext.Current.Server.MapPath("~/Luottruycap.txt"));
            String s = read.ReadLine();
            read.Close();


            Application.Add("hit", s);
            
        }
        void Session_Start(object sender, EventArgs e)
        {
            Application.Lock();
            Application["online"] = int.Parse(Application["online"].ToString()) + 1;
            Application["hit"] = int.Parse(Application["hit"].ToString()) + 1;
            Application.UnLock();
            System.IO.StreamWriter write = new System.IO.StreamWriter(HttpContext.Current.Server.MapPath("~/Luottruycap.txt"));
            write.Write(Application["hit"]);
            write.Close();
        }
        void Session_End(object sender, EventArgs e)
        {
            Application.Lock();
            Application["online"] = int.Parse(Application["online"].ToString()) - 1;
            Application.UnLock();
        }
    }
}
