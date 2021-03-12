using System;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x02000003 RID: 3
	[RunInstaller(true)]
	public class TransportSyncManagerSvcInstaller : Installer
	{
		// Token: 0x0600000A RID: 10 RVA: 0x00002378 File Offset: 0x00000578
		public TransportSyncManagerSvcInstaller()
		{
			ServiceProcessInstaller serviceProcessInstaller = new ServiceProcessInstaller();
			ServiceInstaller serviceInstaller = new ServiceInstaller();
			serviceProcessInstaller.Account = ServiceAccount.LocalSystem;
			serviceInstaller.StartType = ServiceStartMode.Automatic;
			serviceInstaller.ServiceName = TransportSyncManagerSvc.TransportSyncManagerSvcName;
			serviceInstaller.DisplayName = "Microsoft Exchange Transport Sync Manager Service";
			serviceInstaller.Description = "Microsoft Exchange Transport Sync Manager Service";
			base.Installers.Add(serviceInstaller);
			base.Installers.Add(serviceProcessInstaller);
		}
	}
}
