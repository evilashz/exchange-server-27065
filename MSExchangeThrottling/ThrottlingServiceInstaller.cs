using System;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace Microsoft.Exchange.Data.ThrottlingService
{
	// Token: 0x02000008 RID: 8
	[RunInstaller(true)]
	internal class ThrottlingServiceInstaller : Installer
	{
		// Token: 0x06000037 RID: 55 RVA: 0x000030F8 File Offset: 0x000012F8
		public ThrottlingServiceInstaller()
		{
			ServiceProcessInstaller serviceProcessInstaller = new ServiceProcessInstaller();
			ServiceInstaller serviceInstaller = new ServiceInstaller();
			serviceProcessInstaller.Account = ServiceAccount.NetworkService;
			serviceInstaller.StartType = ServiceStartMode.Automatic;
			serviceInstaller.ServiceName = "MSExchangeThrottling";
			serviceInstaller.DisplayName = "Microsoft Exchange Throttling Service";
			serviceInstaller.Description = "Microsoft Exchange Throttling Service";
			base.Installers.Add(serviceInstaller);
			base.Installers.Add(serviceProcessInstaller);
		}
	}
}
