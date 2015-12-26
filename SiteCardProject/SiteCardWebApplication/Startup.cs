using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SiteCardWebApplication.Startup))]
namespace SiteCardWebApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
