using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCWEB.Startup))]
namespace MVCWEB
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
