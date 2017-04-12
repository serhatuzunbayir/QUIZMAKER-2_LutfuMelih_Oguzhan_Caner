using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(QuizMakerApp.Startup))]
namespace QuizMakerApp
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
