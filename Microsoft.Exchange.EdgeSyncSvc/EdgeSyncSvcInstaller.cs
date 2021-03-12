using System;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace Microsoft.Exchange.EdgeSync
{
	// Token: 0x02000015 RID: 21
	[RunInstaller(true)]
	public class EdgeSyncSvcInstaller : Installer
	{
		// Token: 0x060000C3 RID: 195 RVA: 0x00008CFC File Offset: 0x00006EFC
		public EdgeSyncSvcInstaller()
		{
			ServiceProcessInstaller serviceProcessInstaller = new ServiceProcessInstaller();
			ServiceInstaller serviceInstaller = new ServiceInstaller();
			serviceProcessInstaller.Account = ServiceAccount.LocalSystem;
			serviceInstaller.StartType = ServiceStartMode.Automatic;
			serviceInstaller.ServiceName = EdgeSyncSvc.EdgeSyncSvcName;
			serviceInstaller.DisplayName = "Microsoft Exchange Edge Sync Service";
			serviceInstaller.Description = "Microsoft Exchange Edge Sync Service";
			base.Installers.Add(serviceInstaller);
			base.Installers.Add(serviceProcessInstaller);
		}
	}
}
