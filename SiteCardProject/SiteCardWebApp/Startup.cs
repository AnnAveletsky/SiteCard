using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SiteCardWebApp.Startup))]
namespace SiteCardWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
