using System.Web.Configuration;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using AdventureWorks.Infrastructure;
using AdventureWorks.Services;
using Microsoft.Azure.KeyVault;

namespace AdventureWorks.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AutofacConfig.ConfigureContainerForMvc(typeof(MvcApplication));

            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var kv = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(KeyVaultService.GetToken));
            var sec1 = kv.GetSecretAsync(WebConfigurationManager.AppSettings["AccountConnectionStringSecretUri"]).Result;
            var sec2 = kv.GetSecretAsync(WebConfigurationManager.AppSettings["StorageConnectionStringSecretUri"]).Result;
            var sec3 = kv.GetSecretAsync(WebConfigurationManager.AppSettings["ServiceSearchKeySecretUri"]).Result;
            var sec4 = kv.GetSecretAsync(WebConfigurationManager.AppSettings["ServiceSearchNameSecretUri"]).Result;
            KeyVaultService.AccountConnectionString = sec1.Value;
            KeyVaultService.StorageConnectionString = sec2.Value;
            KeyVaultService.ServiceSearchClientKey = sec3.Value;
            KeyVaultService.ServiceSearchClientName = sec4.Value;
        }
    }
}
