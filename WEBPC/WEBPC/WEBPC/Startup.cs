using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WEBPC.Startup))]
namespace WEBPC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
