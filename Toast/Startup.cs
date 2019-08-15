using Toast.App_Start;
//using Toast.Hubs;
using Microsoft.AspNet.SignalR;
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

            // Any connection or hub wire up and configuration should go here
            //GlobalHost.HubPipeline.AddModule(new ErrorHandlingPipelineModule());
            //app.MapSignalR();
            //MappingConfig.RegisterMaps();
        }
    }
}
