using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LR_5.Startup))]
namespace LR_5
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
