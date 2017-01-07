using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Owin;
using WebApi.OWIN;

namespace WebApi.WinService
{
    //必须是pulic不可用internal，否则服务会正常启动，但无法Configuration
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            IniControllerType.Ini(appBuilder);
        }
    }
}
