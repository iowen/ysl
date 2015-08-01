using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ysl_template.YslStartup))]
namespace ysl_template
{
    public partial class YslStartup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
