using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QuizWebApp.Startup))]
namespace QuizWebApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
