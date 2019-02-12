using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Cotacao.Startup))]
namespace Cotacao
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
