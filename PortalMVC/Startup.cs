using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PortalMVC.Startup))]
namespace PortalMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
