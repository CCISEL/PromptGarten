using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using PromptGarten.Web.Controllers;
using PromptGarten.Domain.Services;
using PromptGarten.Domain.Model;

namespace PromptGarten.Web
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
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );

        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            var diControllerFactory = new DupeDiControllerFactory();
            ControllerBuilder.Current.SetControllerFactory(diControllerFactory);
            //
            // Fill InMemoryRepository
            //
            var rep = diControllerFactory.GetInstance<IRepository>();
            rep.Insert(new Teacher(12563, "Pedro Félix"));
            rep.Insert(new Teacher(13125, "Duarte Nunes"));
            rep.Insert(new Teacher(76152, "Luís Falcão"));
            rep.Insert(new Teacher(10702, "FM"));

        }
    }
}