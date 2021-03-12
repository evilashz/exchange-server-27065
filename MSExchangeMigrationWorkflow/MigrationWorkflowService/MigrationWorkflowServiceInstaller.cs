using System;
using System.ComponentModel;
using System.Configuration.Install;
using System.ServiceProcess;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MigrationWorkflowService
{
	// Token: 0x02000004 RID: 4
	[ClassAccessLevel(AccessLevel.Implementation)]
	[RunInstaller(true)]
	internal class MigrationWorkflowServiceInstaller : Installer
	{
		// Token: 0x06000012 RID: 18 RVA: 0x000026E4 File Offset: 0x000008E4
		public MigrationWorkflowServiceInstaller()
		{
			ServiceProcessInstaller serviceProcessInstaller = new ServiceProcessInstaller();
			ServiceInstaller serviceInstaller = new ServiceInstaller();
			serviceProcessInstaller.Account = ServiceAccount.LocalSystem;
			serviceInstaller.StartType = ServiceStartMode.Automatic;
			serviceInstaller.ServiceName = "MSExchangeMigrationWorkflow";
			serviceInstaller.DisplayName = "Microsoft Exchange Migration Workflow Service";
			serviceInstaller.Description = "Microsoft Exchange Migration Workflow Service";
			base.Installers.Add(serviceInstaller);
			base.Installers.Add(serviceProcessInstaller);
		}
	}
}
