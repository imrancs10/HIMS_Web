using Microsoft.Owin;
using Microsoft.Owin.Builder;
using Owin;

[assembly: OwinStartupAttribute(typeof(HIMS_Web.Startup))]
namespace HIMS_Web
{
    public partial class Startup
    {
        public void Configuration(AppBuilder app)
        {
        }
    }
}
