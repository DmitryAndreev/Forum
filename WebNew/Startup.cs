using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebNew.Startup))]
namespace WebNew
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
