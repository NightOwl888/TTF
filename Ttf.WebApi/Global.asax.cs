using System.Web.Http;
using Ttf.WebApi.DependencyInjection;

namespace Ttf.WebApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            IoC.Register();
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
