using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Healty.Startup))]
namespace Healty
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
