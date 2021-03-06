﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace WebApplication1
{
    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "OpenId", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "OpenId", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                "OauthGrant", // Route name
                "{controller}", // URL with parameters
                new { controller = "OauthGrant" } // Parameter defaults
            );

            routes.MapRoute(
                "OauthResponse", // Route name
                "{controller}", // URL with parameters
                new { controller = "OauthResponse" } // Parameter defaults
            );

            routes.MapRoute(
                "MenuProxy", // Route name
                "{controller}", // URL with parameters
                new { controller = "MenuProxy" } // Parameter defaults
            );

            routes.MapRoute(
                "Disconnect", // Route name
                "{controller}", // URL with parameters
                new { controller = "Disconnect", action = "Index" } // Parameter defaults
            );

            routes.MapRoute(
                "DirectConnectToIntuit", // Route name
                "{controller}", // URL with parameters
                new { controller = "DirectConnectToIntuit", action = "Index" } // Parameter defaults
            );

            routes.MapRoute(
                "CleanupOnDisconnect", // Route name
                "{controller}", // URL with parameters
                new { controller = "CleanupOnDisconnect", action = "Index" } // Parameter defaults
            );

            routes.MapRoute(
    "Logout", // Route name
    "{controller}", // URL with parameters
    new { controller = "Logout", action = "Index" } // Parameter defaults
);

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }

        protected void Session_Start()
        {
            Response.Redirect("Home");
        }
    }
}
