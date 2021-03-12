using System;
using System.Configuration.Install;
using System.IO;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Management.Tasks;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020008D1 RID: 2257
	public abstract class ManageWatchDogService : ManageService
	{
		// Token: 0x0600501A RID: 20506 RVA: 0x0014F648 File Offset: 0x0014D848
		protected ManageWatchDogService()
		{
			base.Account = ServiceAccount.LocalSystem;
			base.StartMode = ServiceStartMode.Automatic;
			base.DisplayName = Strings.WatchDogServiceDisplayName;
			base.Description = Strings.WatchDogServiceDescription;
			InstallContext installContext = new InstallContext();
			installContext.Parameters["logtoconsole"] = "false";
			installContext.Parameters["assemblypath"] = Path.Combine(ConfigurationContext.Setup.BinPath, "MSExchangeWatchDog.exe");
			base.ServiceInstallContext = installContext;
			base.EventMessageFile = Path.Combine(ConfigurationContext.Setup.BinPath, "clusmsg.dll");
			base.CategoryCount = 6;
			base.FirstFailureActionType = ServiceActionType.Restart;
			base.FirstFailureActionDelay = 5000U;
			base.SecondFailureActionType = ServiceActionType.Restart;
			base.SecondFailureActionDelay = 5000U;
			base.AllOtherFailuresActionType = ServiceActionType.Restart;
			base.AllOtherFailuresActionDelay = 5000U;
			base.FailureResetPeriod = 0U;
			base.ServiceInstaller.AfterInstall += this.AfterInstallEventHandler;
		}

		// Token: 0x170017ED RID: 6125
		// (get) Token: 0x0600501B RID: 20507 RVA: 0x0014F73A File Offset: 0x0014D93A
		protected override string Name
		{
			get
			{
				return "MSExchangeWatchDog";
			}
		}

		// Token: 0x0600501C RID: 20508 RVA: 0x0014F741 File Offset: 0x0014D941
		private void AfterInstallEventHandler(object sender, InstallEventArgs e)
		{
			base.LockdownServiceAccess();
		}

		// Token: 0x04002F43 RID: 12099
		private const string ServiceShortName = "MSExchangeWatchDog";

		// Token: 0x04002F44 RID: 12100
		private const string ServiceBinaryName = "MSExchangeWatchDog.exe";

		// Token: 0x04002F45 RID: 12101
		private const string EventLogBinaryName = "clusmsg.dll";
	}
}
