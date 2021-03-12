using System;
using System.Collections.Generic;
using System.Configuration.Install;
using System.IO;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Security.WindowsFirewall;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020002E1 RID: 737
	public abstract class ManageTopologyService : ManageService
	{
		// Token: 0x0600199F RID: 6559 RVA: 0x00072090 File Offset: 0x00070290
		protected ManageTopologyService()
		{
			base.Account = ServiceAccount.LocalSystem;
			base.StartMode = ServiceStartMode.Automatic;
			base.DisplayName = Strings.TopologyServiceDisplayName;
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

		// Token: 0x17000788 RID: 1928
		// (get) Token: 0x060019A0 RID: 6560 RVA: 0x00072179 File Offset: 0x00070379
		protected override string Name
		{
			get
			{
				return "MSExchangeTopologyService";
			}
		}

		// Token: 0x04000B18 RID: 2840
		protected const string ServiceShortName = "MSExchangeTopologyService";

		// Token: 0x04000B19 RID: 2841
		private const string ServiceBinaryName = "Microsoft.Exchange.Directory.TopologyService.exe";

		// Token: 0x04000B1A RID: 2842
		private const string EventLogBinaryName = "";
	}
}
