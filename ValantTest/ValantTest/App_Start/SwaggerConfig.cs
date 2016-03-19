using System.Web.Http;
using Swashbuckle.Application;
using ValantTest.Presentation.Api;
using WebActivatorEx;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace ValantTest.Presentation.Api
{
    public class SwaggerConfig
    {
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration 
                .EnableSwagger(c => { c.SingleApiVersion("v1", "API"); })
                .EnableSwaggerUi(c => { });
        }
    }
}