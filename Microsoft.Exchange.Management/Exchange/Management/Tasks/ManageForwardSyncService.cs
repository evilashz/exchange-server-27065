using System;
using System.Configuration.Install;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200037E RID: 894
	public abstract class ManageForwardSyncService : ManageService
	{
		// Token: 0x06001F51 RID: 8017 RVA: 0x000876C4 File Offset: 0x000858C4
		protected ManageForwardSyncService()
		{
			base.Account = ServiceAccount.LocalSystem;
			base.StartMode = ServiceStartMode.Automatic;
			base.DisplayName = Strings.ForwardSyncServiceDisplayName;
			base.Description = Strings.ForwardSyncServiceDescription;
			InstallContext installContext = new InstallContext();
			installContext.Parameters["logtoconsole"] = "false";
			installContext.Parameters["assemblypath"] = Path.Combine(ConfigurationContext.Setup.BinPath, "Microsoft.Exchange.Management.ForwardSync.exe");
			base.FirstFailureActionType = ServiceActionType.Restart;
			base.FirstFailureActionDelay = 5000U;
			base.SecondFailureActionType = ServiceActionType.Restart;
			base.SecondFailureActionDelay = 5000U;
			base.AllOtherFailuresActionType = ServiceActionType.Restart;
			base.AllOtherFailuresActionDelay = 5000U;
			base.ServiceInstallContext = installContext;
			base.ServicesDependedOn = new string[]
			{
				ManagedServiceName.ActiveDirectoryTopologyService
			};
			foreach (object obj in base.ServiceInstaller.Installers)
			{
				EventLogInstaller eventLogInstaller = obj as EventLogInstaller;
				if (eventLogInstaller != null)
				{
					eventLogInstaller.Source = "MSExchangeForwardSync";
					eventLogInstaller.Log = "ForwardSync";
				}
			}
		}

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x06001F52 RID: 8018 RVA: 0x00087800 File Offset: 0x00085A00
		protected override string Name
		{
			get
			{
				return "MSExchangeForwardSync";
			}
		}

		// Token: 0x04001968 RID: 6504
		protected const string ServiceShortName = "MSExchangeForwardSync";

		// Token: 0x04001969 RID: 6505
		private const string ServiceBinaryName = "Microsoft.Exchange.Management.ForwardSync.exe";

		// Token: 0x0400196A RID: 6506
		private const string EventLogName = "ForwardSync";

		// Token: 0x0400196B RID: 6507
		private const string EventLogSourceName = "MSExchangeForwardSync";
	}
}
