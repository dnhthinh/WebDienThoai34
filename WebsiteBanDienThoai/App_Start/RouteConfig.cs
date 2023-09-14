using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebsiteBanDienThoai
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
            name: "AccountLogin",
            url: "Account/Login",
            defaults: new { controller = "Account", action = "Login" }
        );
      
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
            routes.MapRoute(
            name: "Order",
            url: "Order/{action}/{id}",
            defaults: new { controller = "Order", action = "Index", id = UrlParameter.Optional }
        );
            routes.MapHttpRoute(
               name: "API Default",
               routeTemplate: "api/{controller}/{id}",
               defaults: new { id = RouteParameter.Optional }
            );
            routes.MapRoute(
    name: "AdminThongKe",
    url: "Admin/ThongKe_Admin/Index",
    defaults: new { controller = "ThongKe_Admin", action = "Index" }
);



        }
    }

}
