using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(HotelPractice.Startup))]
namespace HotelPractice
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
