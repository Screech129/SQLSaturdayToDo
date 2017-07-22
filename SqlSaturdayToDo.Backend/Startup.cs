using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(SqlSaturdayToDo.Backend.Startup))]

namespace SqlSaturdayToDo.Backend
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureMobileApp(app);
        }
    }
}