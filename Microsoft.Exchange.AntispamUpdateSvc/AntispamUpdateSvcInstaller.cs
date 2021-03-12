using System;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace Microsoft.Exchange.AntispamUpdate
{
	// Token: 0x02000004 RID: 4
	[RunInstaller(true)]
	public class AntispamUpdateSvcInstaller : Installer
	{
		// Token: 0x06000011 RID: 17 RVA: 0x00002D0C File Offset: 0x00000F0C
		public AntispamUpdateSvcInstaller()
		{
			ServiceProcessInstaller serviceProcessInstaller = new ServiceProcessInstaller();
			ServiceInstaller serviceInstaller = new ServiceInstaller();
			serviceProcessInstaller.Account = ServiceAccount.LocalSystem;
			serviceInstaller.StartType = ServiceStartMode.Automatic;
			serviceInstaller.ServiceName = "MSExchangeAntispamUpdate";
			serviceInstaller.DisplayName = "Microsoft Exchange Anti-spam Update";
			serviceInstaller.Description = "The Microsoft Forefront Security for Exchange Server anti-spam update service";
			base.Installers.Add(serviceInstaller);
			base.Installers.Add(serviceProcessInstaller);
		}
	}
}
