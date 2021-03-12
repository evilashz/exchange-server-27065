using System;
using System.Collections.Generic;
using System.Configuration.Install;
using System.IO;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Security.WindowsFirewall;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020002CC RID: 716
	public abstract class ManageADTopologyService : ManageService
	{
		// Token: 0x06001934 RID: 6452 RVA: 0x00070DD0 File Offset: 0x0006EFD0
		protected ManageADTopologyService()
		{
			base.Account = ServiceAccount.LocalSystem;
			base.StartMode = ServiceStartMode.Automatic;
			base.DisplayName = Strings.ADTopologyServiceDisplayName;
			base.Description = Strings.ADTopologyServiceDescription;
			InstallContext installContext = new InstallContext();
			installContext.Parameters["logtoconsole"] = "false";
			installContext.Parameters["assemblypath"] = Path.Combine(ConfigurationContext.Setup.BinPath, "Microsoft.Exchange.Directory.TopologyService.exe");
			base.FirstFailureActionType = ServiceActionType.Restart;
			base.FirstFailureActionDelay = 5000U;
			base.SecondFailureActionType = ServiceActionType.Restart;
			base.SecondFailureActionDelay = 5000U;
			base.AllOtherFailuresActionType = ServiceActionType.Restart;
			base.AllOtherFailuresActionDelay = 5000U;
			base.FailureResetPeriod = 0U;
			base.ServiceInstallContext = installContext;
			List<string> list = new List<string>
			{
				"NetTcpPortSharing"
			};
			base.ServicesDependedOn = list.ToArray();
			base.AddFirewallRule(new MSExchangeADTopologyWCFFirewallRule());
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x06001935 RID: 6453 RVA: 0x00070EB9 File Offset: 0x0006F0B9
		protected override string Name
		{
			get
			{
				return "MSExchangeADTopology";
			}
		}

		// Token: 0x04000B00 RID: 2816
		protected const string ServiceShortName = "MSExchangeADTopology";

		// Token: 0x04000B01 RID: 2817
		private const string ServiceBinaryName = "Microsoft.Exchange.Directory.TopologyService.exe";

		// Token: 0x04000B02 RID: 2818
		private const string EventLogBinaryName = "";
	}
}
