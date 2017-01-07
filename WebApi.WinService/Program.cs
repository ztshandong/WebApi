using System.ServiceProcess;

namespace WebApi.WinService
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceBase[] ServicesToRun2;
            ServicesToRun2 = new ServiceBase[]
            {
                new WebApiService()
            };
            ServiceBase.Run(ServicesToRun2);
        }
    }
}
