using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BasicApp.Startup))]
namespace BasicApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
