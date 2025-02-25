using Autofac.Integration.WebApi;
using Autofac;
using System.Reflection;
using System.Web.Http;
using DealGeinieCrmService.Modules;

namespace DealGeinieCrmService
{
    /// <summary>
    /// ApiApplication class is the entry point of the application.
    /// Willie Yao - 02/21/2025
    /// </summary>
    public class ApiApplication : System.Web.HttpApplication
    {
        /// <summary>
        /// Application_Start method is called when the application starts registering the Web API configuration.
        /// Willie Yao - 02/21/2025
        /// </summary>
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);

            var builder = new ContainerBuilder();

            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

            builder.RegisterModule(new ServiceModule());

            var container = builder.Build();
            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
        }
    }
}