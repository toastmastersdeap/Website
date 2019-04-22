using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Toast.Startup))]
namespace Toast
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
