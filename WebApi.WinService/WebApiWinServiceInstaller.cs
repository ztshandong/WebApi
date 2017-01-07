using System.ComponentModel;
using System.Configuration.Install;

namespace WebApi.WinService
{
    [RunInstaller(true)]
    public partial class WebApiWinServiceInstaller : System.Configuration.Install.Installer
    {
        public WebApiWinServiceInstaller()
        {
            InitializeComponent();
            this.AfterInstall += new InstallEventHandler(ProjectInstaller_AfterInstall);
        }
        void ProjectInstaller_AfterInstall(object sender, InstallEventArgs e)
        {
            System.ServiceProcess.ServiceController sc = new System.ServiceProcess.ServiceController("WebApiServer");
            if (sc != null)
                sc.Start();
        }
    }
}
