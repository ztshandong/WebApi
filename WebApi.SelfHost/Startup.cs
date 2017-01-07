using Owin;
using WebApi.OWIN;

namespace WebApi.SelfHost
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            IniControllerType.Ini(appBuilder);
        }
    }
}
