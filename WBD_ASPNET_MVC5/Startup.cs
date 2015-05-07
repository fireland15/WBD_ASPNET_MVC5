using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WBD_ASPNET_MVC5.Startup))]
namespace WBD_ASPNET_MVC5
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
