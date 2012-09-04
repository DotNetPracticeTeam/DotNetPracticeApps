using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Microsoft.Practices.Unity;
using Microsoft.Practices.Unity.Configuration;
using Pubs.Data.Base;
using Pubs.Data;
using Pubs.Data.Interface;
using System.Configuration;
using Pubs.Web.IoC;


namespace AutoMapperSampler
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

            //UMI route 
            routes.MapRoute(
            "resolvetype",
            "Home/ResolveUsing/{resolvetype}", new { controller = "Home", action = "ResolveUsing", resolvetype = UrlParameter.Optional }, // Parameter defaults,
            new { controller = @"[^\.]*"}  
          );

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional }, // Parameter defaults
                new { controller = @"[^\.]*" }                          // Parameter constraints
            );

          

            routes.MapRoute(
                "ResolveUsing1", // Route name
                "Home/ResolveUsing/{resolvetype}",
                new { resolvetype = UrlParameter.Optional }
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

            EFUnitOfWorkFactory.SetObjectContext(() => new pubsEntities());

            UnitOfWork.UnitOfWorkFactory = container.Resolve<IUnitOfWorkFactory>();

        }
    }
}