using System;
using System.Configuration.Install;
using System.IO;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Security.WindowsFirewall;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020005FE RID: 1534
	public abstract class ManageMonitoringService : ManageService
	{
		// Token: 0x060036BB RID: 14011 RVA: 0x000E2C34 File Offset: 0x000E0E34
		public ManageMonitoringService()
		{
			base.Account = ServiceAccount.LocalSystem;
			base.StartMode = ServiceStartMode.Manual;
			base.DisplayName = Strings.MonitoringServiceDisplayName;
			base.Description = Strings.MonitoringServiceDescription;
			InstallContext installContext = new InstallContext();
			installContext.Parameters["logtoconsole"] = "false";
			installContext.Parameters["assemblypath"] = Path.Combine(ConfigurationContext.Setup.BinPath, "Microsoft.Exchange.Monitoring.exe");
			base.FirstFailureActionType = ServiceActionType.Restart;
			base.FirstFailureActionDelay = 5000U;
			base.SecondFailureActionType = ServiceActionType.Restart;
			base.SecondFailureActionDelay = 5000U;
			base.AllOtherFailuresActionType = ServiceActionType.Restart;
			base.AllOtherFailuresActionDelay = 5000U;
			base.FailureResetPeriod = 0U;
			base.ServiceInstallContext = installContext;
			base.ServicesDependedOn = null;
			base.AddFirewallRule(new MSExchangeMonitoringFirewallRule());
		}

		// Token: 0x17001044 RID: 4164
		// (get) Token: 0x060036BC RID: 14012 RVA: 0x000E2D05 File Offset: 0x000E0F05
		protected override string Name
		{
			get
			{
				return "MSExchangeMonitoring";
			}
		}

		// Token: 0x0400255F RID: 9567
		private const string ServiceShortName = "MSExchangeMonitoring";

		// Token: 0x04002560 RID: 9568
		private const string ServiceBinaryName = "Microsoft.Exchange.Monitoring.exe";
	}
}
