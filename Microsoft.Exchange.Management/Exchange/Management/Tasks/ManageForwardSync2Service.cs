using System;
using System.Configuration.Install;
using System.IO;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000381 RID: 897
	public abstract class ManageForwardSync2Service : ManageService
	{
		// Token: 0x06001F57 RID: 8023 RVA: 0x0008783C File Offset: 0x00085A3C
		protected ManageForwardSync2Service()
		{
			base.Account = ServiceAccount.LocalSystem;
			base.StartMode = ServiceStartMode.Automatic;
			base.DisplayName = Strings.ForwardSyncService2DisplayName;
			base.Description = Strings.ForwardSyncServiceDescription;
			InstallContext installContext = new InstallContext();
			installContext.Parameters["logtoconsole"] = "false";
			installContext.Parameters["assemblypath"] = Path.Combine(ConfigurationContext.Setup.BinPath, "Microsoft.Exchange.Management.ForwardSync2.exe");
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
		}

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x06001F58 RID: 8024 RVA: 0x0008790A File Offset: 0x00085B0A
		protected override string Name
		{
			get
			{
				return "MSExchangeForwardSync2";
			}
		}

		// Token: 0x0400196C RID: 6508
		private const string ServiceShortName = "MSExchangeForwardSync2";

		// Token: 0x0400196D RID: 6509
		private const string ServiceBinaryName = "Microsoft.Exchange.Management.ForwardSync2.exe";
	}
}
