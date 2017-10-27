using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WP_search.Startup))]
namespace WP_search
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
