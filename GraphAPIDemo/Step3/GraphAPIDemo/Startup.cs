using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(GraphAPIDemo.Startup))]

namespace GraphAPIDemo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}