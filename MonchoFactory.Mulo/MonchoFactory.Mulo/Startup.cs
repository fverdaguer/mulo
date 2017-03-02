using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(MonchoFactory.Mulo.WebApi.Startup))]

namespace MonchoFactory.Mulo.WebApi
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureOAuth(app);
        }
    }
}