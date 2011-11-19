using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using TaskReminder.Core.EntityFramework;
using TaskReminder.Web.Core;
using TaskReminder.Web.Mvc;

namespace TaskReminder.Web
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

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
                null,
                "",
                new { controller = "task", action = "list" }
            );

            routes.MapRoute(
                null, // Route name
                "company", // URL with parameters
                new { controller = "company", action = "list", id = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                null, // Route name
                "company-{companyId}/{action}", // URL with parameters
                new { controller = "company", action = "edit", id = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                null, // Route name
                "company-{companyId}/office/{action}/{id}", // URL with parameters
                new { controller = "office", action = "detail", id = UrlParameter.Optional } // Parameter defaults
            );

            routes.MapRoute(
                null,
                "task-{taskId}/{action}",
                new {  controller = "task", action = "detail"}
            );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "task", action = "list", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            Database.SetInitializer<DataContext>(new DataContextInitializer());
            DependencyResolver.SetResolver(new NinjectDependencyResolver());

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new TaskReminder.Web.Mvc.RazorViewEngine());

            //ModelBinders.Binders.Add(typeof(DateTime), new DateTimeModelBinder());
            //ModelBinders.Binders.Add(typeof(DateTime?), new DateTimeModelBinder());

            WebViewPageHelper.RegisterTabs();
        }
    }
}