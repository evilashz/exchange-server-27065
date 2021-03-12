using System;
using System.Configuration.Install;
using System.IO;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Security.WindowsFirewall;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000CFC RID: 3324
	public abstract class ManageThrottlingService : ManageService
	{
		// Token: 0x06007FD8 RID: 32728 RVA: 0x0020AEC0 File Offset: 0x002090C0
		public ManageThrottlingService()
		{
			base.Account = ServiceAccount.NetworkService;
			base.StartMode = ServiceStartMode.Automatic;
			base.DisplayName = Strings.ThrottlingServiceDisplayName;
			base.Description = Strings.ThrottlingServiceDescription;
			InstallContext installContext = new InstallContext();
			installContext.Parameters["logtoconsole"] = "false";
			installContext.Parameters["assemblypath"] = Path.Combine(ConfigurationContext.Setup.BinPath, "MSExchangeThrottling.exe");
			base.EventMessageFile = Path.Combine(ConfigurationContext.Setup.ResPath, "Microsoft.Exchange.Data.ThrottlingService.EventLog.dll");
			base.CategoryCount = 1;
			base.FirstFailureActionType = ServiceActionType.Restart;
			base.FirstFailureActionDelay = 5000U;
			base.SecondFailureActionType = ServiceActionType.Restart;
			base.SecondFailureActionDelay = 5000U;
			base.AllOtherFailuresActionType = ServiceActionType.Restart;
			base.AllOtherFailuresActionDelay = 5000U;
			base.FailureResetPeriod = 0U;
			base.ServiceInstallContext = installContext;
			base.AddFirewallRule(new MSExchangeThrottlingRPCEPMapFirewallRule());
			base.AddFirewallRule(new MSExchangeThrottlingRPCFirewallRule());
		}

		// Token: 0x170027AA RID: 10154
		// (get) Token: 0x06007FD9 RID: 32729 RVA: 0x0020AFB1 File Offset: 0x002091B1
		protected override string Name
		{
			get
			{
				return "MSExchangeThrottling";
			}
		}

		// Token: 0x04003EB6 RID: 16054
		private const string ServiceShortName = "MSExchangeThrottling";

		// Token: 0x04003EB7 RID: 16055
		private const string ServiceBinaryName = "MSExchangeThrottling.exe";

		// Token: 0x04003EB8 RID: 16056
		private const string EventLogBinaryName = "Microsoft.Exchange.Data.ThrottlingService.EventLog.dll";
	}
}
