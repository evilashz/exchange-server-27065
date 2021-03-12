using System;
using System.Configuration.Install;
using System.IO;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000603 RID: 1539
	public abstract class ManageMonitoringCorrelationService : ManageService
	{
		// Token: 0x060036C9 RID: 14025 RVA: 0x000E30D8 File Offset: 0x000E12D8
		public ManageMonitoringCorrelationService()
		{
			base.Account = ServiceAccount.LocalSystem;
			base.StartMode = ServiceStartMode.Automatic;
			base.DisplayName = Strings.MonitoringCorrelationServiceDisplayName;
			base.Description = Strings.MonitoringCorrelationServiceDescription;
			InstallContext installContext = new InstallContext();
			installContext.Parameters["logtoconsole"] = "false";
			installContext.Parameters["assemblypath"] = Path.Combine(ConfigurationContext.Setup.BinPath, "Microsoft.Exchange.Monitoring.CorrelationEngine.exe");
			base.FirstFailureActionType = ServiceActionType.Restart;
			base.FirstFailureActionDelay = 5000U;
			base.SecondFailureActionType = ServiceActionType.Restart;
			base.SecondFailureActionDelay = 5000U;
			base.AllOtherFailuresActionType = ServiceActionType.Restart;
			base.AllOtherFailuresActionDelay = 5000U;
			base.FailureResetPeriod = 0U;
			base.ServiceInstallContext = installContext;
			base.ServicesDependedOn = null;
		}

		// Token: 0x17001046 RID: 4166
		// (get) Token: 0x060036CA RID: 14026 RVA: 0x000E319E File Offset: 0x000E139E
		protected override string Name
		{
			get
			{
				return "MSExchangeMonitoringCorrelation";
			}
		}

		// Token: 0x04002568 RID: 9576
		private const string ServiceShortName = "MSExchangeMonitoringCorrelation";

		// Token: 0x04002569 RID: 9577
		private const string ServiceBinaryName = "Microsoft.Exchange.Monitoring.CorrelationEngine.exe";
	}
}
