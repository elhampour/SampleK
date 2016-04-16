using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SampleKendo.Startup))]
namespace SampleKendo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
