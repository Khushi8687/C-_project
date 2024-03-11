using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(C__project.Startup))]
namespace C__project
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
          
        }
    }
    
}
