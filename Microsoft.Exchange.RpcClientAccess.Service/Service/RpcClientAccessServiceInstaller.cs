using System;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace Microsoft.Exchange.RpcClientAccess.Service
{
	// Token: 0x02000002 RID: 2
	[RunInstaller(true)]
	public sealed class RpcClientAccessServiceInstaller : Installer
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public RpcClientAccessServiceInstaller()
		{
			ServiceProcessInstaller serviceProcessInstaller = new ServiceProcessInstaller();
			ServiceInstaller serviceInstaller = new ServiceInstaller();
			serviceProcessInstaller.Account = ServiceAccount.NetworkService;
			serviceInstaller.StartType = ServiceStartMode.Automatic;
			serviceInstaller.ServiceName = RpcServiceManager.RpcServiceManagerServiceName;
			serviceInstaller.DisplayName = "Microsoft Exchange RPC Client Access Service";
			serviceInstaller.Description = "Microsoft Exchange RPC Client Access Service";
			base.Installers.Add(serviceInstaller);
			base.Installers.Add(serviceProcessInstaller);
		}
	}
}
