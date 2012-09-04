using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc3;




using System.Web.Security;
//using Adventure.Web.IoC;
using Adventure.Data;
using Adventure.Data.Infrastructure;
using Adventure.Service;
using System.Data.Entity;
using Microsoft.Practices.ServiceLocation;


namespace Adventure.Web
{
    public static class Bootstrapper
    {
        public static void Initialise()
        {
            var container = BuildUnityContainer();

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer()
                //.RegisterType<IControllerActivator, CustomControllerActivator>() // No nned to a controller activator
                //.RegisterType<IFormsAuthenticationService, FormsAuthenticationService>()
                //.RegisterType<IMembershipService, AccountMembershipService>()
                //.RegisterInstance<MembershipProvider>(Membership.Provider)
                //.RegisterType<IDatabaseFactory, DatabaseFactory>(new HttpContextLifetimeManager<IDatabaseFactory>())
            .RegisterType<IUnitOfWork, UnitOfWork>(new HierarchicalLifetimeManager())
            .RegisterType<IDepartmentRepository, DepartmentRepository>(new HierarchicalLifetimeManager())
            .RegisterType<IDepartmentService, DepartmentService>(new HierarchicalLifetimeManager());
            //.RegisterType<ILoggingService, LoggingService>(new HttpContextLifetimeManager<ILoggingService>())
            //.RegisterType<ISecurityService, SecurityService>(new HttpContextLifetimeManager<ISecurityService>());
            return container;


            // register all your components with the container here
            // it is NOT necessary to register your controllers
            
            // e.g. container.RegisterType<ITestService, TestService>();            
        }
    }
}