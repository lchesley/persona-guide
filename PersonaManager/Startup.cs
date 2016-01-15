using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PersonaManager.Startup))]
namespace PersonaManager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
