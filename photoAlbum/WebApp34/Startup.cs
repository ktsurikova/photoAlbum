using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebApp34.Startup))]
namespace WebApp34
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
