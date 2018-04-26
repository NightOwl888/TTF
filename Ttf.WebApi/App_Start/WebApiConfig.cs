using Microsoft.Web.Http.Routing;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.Routing;
using Ttf.WebApi.DependencyInjection;
using Ttf.WebApi.Filters;

namespace Ttf.WebApi
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            // Web API configuration and services

            // Dependency Injection for WebApi controllers via StructureMap
            config.Services.Replace(typeof(IHttpControllerActivator), new ServiceActivator(config));

            // API Versioning (adding version to URL)
            config.AddApiVersioning();

            // make a constraint for API version
            var constraintResolver = new DefaultInlineConstraintResolver()
            {
                ConstraintMap =
                {
                    ["apiVersion"] = typeof( ApiVersionRouteConstraint )
                }
            };

            // Web API routes
            config.MapHttpAttributeRoutes(constraintResolver);

            // Add a filter to handle common exceptions and convert them into
            // appropriate status codes.
            config.Filters.Add(new WebApiExceptionFilter());

            // Add converter to serialize enum types as their symbol name rather than their value
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());

            // Add JSON support for output (instead of XML) when text/html is requested
            var appXmlType = config.Formatters.XmlFormatter.SupportedMediaTypes.FirstOrDefault(t => t.MediaType == "application/xml");
            config.Formatters.XmlFormatter.SupportedMediaTypes.Remove(appXmlType);
        }
    }
}
