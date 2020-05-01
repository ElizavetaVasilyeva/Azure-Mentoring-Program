using System.Web;
using System.Web.Configuration;
using System.Web.Http;
using AdventureWorks.Infrastructure;
using AdventureWorks.Services;
using Microsoft.Azure.KeyVault;

namespace AdventureWorks.API
{
    public class WebApiApplication : HttpApplication
    {
        protected void Application_Start()
        {
            AutofacConfig.ConfigureContainerForWebApi(GlobalConfiguration.Configuration, typeof(WebApiApplication));
            GlobalConfiguration.Configure(WebApiConfig.Register);

            var kv = new KeyVaultClient(new KeyVaultClient.AuthenticationCallback(KeyVaultService.GetToken));
            var sec1 = kv.GetSecretAsync(WebConfigurationManager.AppSettings["AccountConnectionStringSecretUri"]).Result;
            var sec2 = kv.GetSecretAsync(WebConfigurationManager.AppSettings["StorageConnectionStringSecretUri"]).Result;
            KeyVaultService.AccountConnectionString = sec1.Value;
            KeyVaultService.StorageConnectionString = sec2.Value;
        }
    }
}
