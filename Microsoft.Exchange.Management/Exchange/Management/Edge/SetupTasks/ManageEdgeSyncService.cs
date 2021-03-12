using System;
using System.Configuration.Install;
using System.IO;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Security.WindowsFirewall;

namespace Microsoft.Exchange.Management.Edge.SetupTasks
{
	// Token: 0x02000301 RID: 769
	public abstract class ManageEdgeSyncService : ManageService
	{
		// Token: 0x06001A40 RID: 6720 RVA: 0x00074984 File Offset: 0x00072B84
		protected ManageEdgeSyncService()
		{
			base.Account = ServiceAccount.LocalSystem;
			base.StartMode = ServiceStartMode.Automatic;
			base.DisplayName = Strings.EdgeSyncServiceDisplayName;
			base.Description = Strings.EdgeSyncServiceDescription;
			InstallContext installContext = new InstallContext();
			installContext.Parameters["logtoconsole"] = "false";
			installContext.Parameters["assemblypath"] = Path.Combine(ConfigurationContext.Setup.BinPath, "Microsoft.Exchange.EdgeSyncSvc.exe");
			base.FirstFailureActionType = ServiceActionType.Restart;
			base.FirstFailureActionDelay = 5000U;
			base.SecondFailureActionType = ServiceActionType.Restart;
			base.SecondFailureActionDelay = 5000U;
			base.AllOtherFailuresActionType = ServiceActionType.Restart;
			base.AllOtherFailuresActionDelay = 5000U;
			base.FailureResetPeriod = 0U;
			base.ServiceInstallContext = installContext;
			base.AddFirewallRule(new MSExchangeEdgesyncRPCRule());
			base.AddFirewallRule(new MSExchangeEdgesyncRPCEPMapRule());
		}

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x06001A41 RID: 6721 RVA: 0x00074A59 File Offset: 0x00072C59
		protected override string Name
		{
			get
			{
				return "MSExchangeEdgeSync";
			}
		}

		// Token: 0x04000B6F RID: 2927
		private const string ServiceShortName = "MSExchangeEdgeSync";

		// Token: 0x04000B70 RID: 2928
		private const string ServiceBinaryName = "Microsoft.Exchange.EdgeSyncSvc.exe";
	}
}
