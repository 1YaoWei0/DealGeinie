using DealGeinieCrmService.Filters;
using System.Web.Http;
using System.Web.Http.Cors;

namespace DealGeinieCrmService
{
    /// <summary>
    /// Web API configuration
    /// Willie Yao - 03/27/2025
    /// </summary>
    public static class WebApiConfig
    {
        /// <summary>
        /// Register the Web API configuration
        /// Willie Yao - 03/27/2025
        /// </summary>
        /// <param name="config">Http configuration</param>
        public static void Register(HttpConfiguration config)
        {
            var cors = new EnableCorsAttribute("*", "*", "*", "Authorization");
            config.EnableCors(cors);
            config.Filters.Add(new GlobalExceptionFilter()); // Add the global exception filter
            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
