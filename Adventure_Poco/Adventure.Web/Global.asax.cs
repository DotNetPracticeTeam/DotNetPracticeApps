using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
//using Adventure.Web.Models;
using System.Web.Security;
using Adventure.Web.IoC;
using Adventure.Data;
using Adventure.Data.Interface;
using Adventure.Data.Base;
using Adventure.Service;
//using Adventure.Web.Helpers;
using System.Data.Entity;
using Microsoft.Practices.ServiceLocation;
using System.Configuration;
//using Adventure.Web.Log;



namespace Adventure.Web
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

/*            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }, // Parameter defaults
                new { controller = @"[^\.]*" }                          // Parameter constraints

            );
            */


            routes.MapRoute(
            "WithParameter",                                              // Route name
            "{controller}/{action}/{Id}",                                         // URL with parameters
            new { controller = "Purchase", action = "Index", Id = "" }  // Parameter defaults
            );


            routes.MapRoute(
                "Default",                                              // Route name
                "{controller}/{action}",                                         // URL with parameters
                new { controller = "Purchase", action = "Index", id = "" }  // Parameter defaults
                );

            routes.MapRoute(
                "Home",                                              // Route name
                "{controller}",                                         // URL with parameters
                new { controller = "Home", action = "Index", id = "" }  // Parameter defaults
                );



        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);

            IUnityContainer container = new UnityContainer();
            IControllerFactory controllerFactory = new UnityControllerFactory(container);
            ControllerBuilder.Current.SetControllerFactory(controllerFactory);

            UnityConfigurationSection section = ConfigurationManager.GetSection("unity") as UnityConfigurationSection;
            section.Configure(container, "Default");

            EFUnitOfWorkFactory.SetObjectContext(() => new AdventureEntities());

            UnitOfWork.UnitOfWorkFactory = container.Resolve<IUnitOfWorkFactory>();
        }

    }
}