using System.Reflection;
using System.Web.Http;
using Autofac;
using Autofac.Integration.WebApi;
using SharpMind.AppServices;

namespace SharpMind.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            var container = BuildContainer();

            var resolver = new AutofacWebApiDependencyResolver(container);

            GlobalConfiguration.Configuration.DependencyResolver = resolver;
        }

        static IContainer BuildContainer()
        {
            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly()).PropertiesAutowired();

            builder.RegisterType<ActivityResolverFacade>().As<IActivityResolverFacade>();
            builder.RegisterType<SimpleResolver>().As<ISimpleResolver>();
            builder.RegisterType<BotAddedResolver>().As<IBotAddedResolver>();

            return builder.Build();
        }
    }
}
