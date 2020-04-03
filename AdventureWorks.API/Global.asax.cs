using System.Web;
using System.Web.Http;
using AdventureWorks.Infrastructure;

namespace AdventureWorks.API
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AutofacConfig.ConfigureContainerForWebApi(GlobalConfiguration.Configuration, typeof(WebApiApplication));
            GlobalConfiguration.Configure(WebApiConfig.Register);
        }
    }
}
