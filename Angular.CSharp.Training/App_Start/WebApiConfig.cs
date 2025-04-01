using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Angular.CSharp.Training.App_Start
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Enable CORS for all origins, headers, and methods
            var cors = new EnableCorsAttribute("*", "*", "*"); // You can specify domains here instead of "*"
            config.EnableCors(cors);

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always;
        }
    }
}