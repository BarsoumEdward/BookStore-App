using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BookStore.WebUI
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
               name: null,
               url: "",
               defaults: new { controller = "Book", action = "List", specialization = (string)null,pageno=1, id = UrlParameter.Optional }
           );

            routes.MapRoute(
                name: null,
                url: "BookListPage{pageno}",
                defaults: new { controller = "Book", action = "List",specialization=(string)null, id = UrlParameter.Optional }
            );


            routes.MapRoute(
               name: null,
               url: "{specialization}",
               defaults: new { controller = "Book", action = "List",  id = UrlParameter.Optional }
           );

            routes.MapRoute(
               null,
            "{specialization}/Page{pageno}",
             new { controller = "Book", action = "List"},
             new { pageno=@"\d+"}
          );

            routes.MapRoute(
               name: null,
               url: "BookListPage{pageno}",
               defaults: new { controller = "Book", action = "List", specialization = (string)null, id = UrlParameter.Optional }
           );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Book", action = "List", id = UrlParameter.Optional }
            );
        }
    }
}
