using System.Web.Http;

namespace MyWallet.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Web API routes
            config.MapHttpAttributeRoutes();
            //resolver for 404 error

            //config.Routes.MapHttpRoute(
            //    name: "ResourceNotFound",
            //    routeTemplate: "{*uri}",
            //    defaults: new { controller = "Default", id = RouteParameter.Optional });

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
