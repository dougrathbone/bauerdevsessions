using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using NServiceBus;
using NServiceBus.ObjectBuilder;

namespace _2013.April.NServiceBus.ClientWeb
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
            Configure.With()
                .DefaultBuilder()
                .ForMvc()
                .Log4Net()
                .MsmqTransport()
                .IsTransactional(false)
                .PurgeOnStartup(true)
                .UnicastBus()
                .ImpersonateSender(false)
                .CreateBus()
                .Start(() => Configure.Instance.ForInstallationOn<global::NServiceBus.Installation.Environments.Windows>().Install());
        }
    }

    public static class NServiceBusConfigurationExtensions
    {
        public static Configure ForMvc(this Configure configure)
        {
            // Register our controller activator with NSB
            configure.Configurer.RegisterSingleton(typeof(IControllerActivator),
                new NServiceBusControllerActivator());

            // Find every controller class so that we can register it
            var controllers = Configure.TypesToScan
                .Where(t => typeof(IController).IsAssignableFrom(t));

            // Register each controller class with the NServiceBus container
            foreach (Type type in controllers)
                configure.Configurer.ConfigureComponent(type, DependencyLifecycle.InstancePerCall);

            // Set the MVC dependency resolver to use our resolver
            DependencyResolver.SetResolver(new NServiceBusDependencyResolverAdapter(configure.Builder));

            // Required by the fluent configuration semantics
            return configure;
        }
    }

    public class NServiceBusControllerActivator : IControllerActivator
    {
        public IController Create(RequestContext requestContext, Type controllerType)
        {
            return DependencyResolver.Current.GetService(controllerType) as IController;
        }
    }

    public class NServiceBusDependencyResolverAdapter : IDependencyResolver
    {
        private IBuilder builder;

        public NServiceBusDependencyResolverAdapter(IBuilder builder)
        {
            this.builder = builder;
        }

        public object GetService(Type serviceType)
        {
            if (Configure.Instance.Configurer.HasComponent(serviceType))
                return builder.Build(serviceType);
            return null;
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return builder.BuildAll(serviceType);
        }
    }
}