using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RecipesWebApp.Startup))]
namespace RecipesWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
