using System.Web.Http;
using WebActivatorEx;
using HIMS_Web;
using Swashbuckle.Application;
using System.IO;

[assembly: PreApplicationStartMethod(typeof(SwaggerConfig), "Register")]

namespace HIMS_Web
{
    public class SwaggerConfig
    {
        protected static string GetXmlCommentsPath()
        {
            return Path.Combine(System.Web.HttpRuntime.AppDomainAppPath, "bin", "HIMS_Web.xml");
        }
        public static void Register()
        {
            var thisAssembly = typeof(SwaggerConfig).Assembly;

            GlobalConfiguration.Configuration
                .EnableSwagger(c =>
                    {
                        c.SingleApiVersion("v1", "HIMS_Web");

                        c.IncludeXmlComments(GetXmlCommentsPath());

                        c.ApiKey("Token")
                            .Description("Filling bearer token here")
                            .Name("Authorization")
                            .In("header");
                    })
                .EnableSwaggerUi(c =>
                    {
                        c.EnableApiKeySupport("Authorization", "header");
                    });
        }
    }
}
