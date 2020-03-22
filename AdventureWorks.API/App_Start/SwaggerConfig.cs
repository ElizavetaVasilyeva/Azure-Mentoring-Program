using System.Web.Http;
using Swashbuckle.Application;
using WebActivatorEx;
using AdventureWorks.API;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace AdventureWorks.API
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>c.SingleApiVersion("v1", "AdventureWorks.API"))
                        .EnableSwaggerUi();
        }
    }

}