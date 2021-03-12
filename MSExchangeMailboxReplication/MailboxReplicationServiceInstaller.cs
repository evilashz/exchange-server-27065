using System;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000002 RID: 2
	[RunInstaller(true)]
	internal class MailboxReplicationServiceInstaller : Installer
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public MailboxReplicationServiceInstaller()
		{
			ServiceProcessInstaller serviceProcessInstaller = new ServiceProcessInstaller();
			ServiceInstaller serviceInstaller = new ServiceInstaller();
			serviceProcessInstaller.Account = ServiceAccount.LocalSystem;
			serviceInstaller.StartType = ServiceStartMode.Automatic;
			serviceInstaller.ServiceName = "MSExchangeMailboxReplication";
			serviceInstaller.DisplayName = "Microsoft Exchange Mailbox Replication Service";
			serviceInstaller.Description = "Microsoft Exchange Mailbox Replication Service";
			base.Installers.Add(serviceInstaller);
			base.Installers.Add(serviceProcessInstaller);
		}
	}
}
