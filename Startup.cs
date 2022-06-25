using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HelwanQuestionnaires.Startup))]
namespace HelwanQuestionnaires
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
