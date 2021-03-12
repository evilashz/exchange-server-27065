using System;
using System.Configuration.Install;
using System.IO;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200076F RID: 1903
	public abstract class ManageFileDistributionService : ManageService
	{
		// Token: 0x0600435B RID: 17243 RVA: 0x00114708 File Offset: 0x00112908
		protected ManageFileDistributionService()
		{
			base.Account = ServiceAccount.LocalSystem;
			base.StartMode = ServiceStartMode.Automatic;
			base.DisplayName = Strings.FDServiceDisplayName;
			base.Description = Strings.FDServiceDescription;
			InstallContext installContext = new InstallContext();
			installContext.Parameters["logtoconsole"] = "false";
			installContext.Parameters["assemblypath"] = Path.Combine(ConfigurationContext.Setup.BinPath, "MSExchangeFDS.exe");
			base.EventMessageFile = Path.Combine(ConfigurationContext.Setup.ResPath, "Microsoft.Exchange.Data.FileDistributionService.EventLog.dll");
			base.CategoryCount = 2;
			base.FirstFailureActionDelay = 5000U;
			base.FirstFailureActionType = ServiceActionType.Restart;
			base.SecondFailureActionType = ServiceActionType.Restart;
			base.SecondFailureActionDelay = 5000U;
			base.AllOtherFailuresActionType = ServiceActionType.Restart;
			base.AllOtherFailuresActionDelay = 5000U;
			base.FailureActionsFlag = true;
			base.FailureResetPeriod = 0U;
			base.ServiceInstallContext = installContext;
			base.ServicesDependedOn = new string[]
			{
				ManageFileDistributionService.ActiveDirectoryTopologyService,
				"lanmanworkstation"
			};
			base.ServiceInstallContext = installContext;
			base.ServiceInstaller.AfterInstall += this.AfterInstallEventHandler;
		}

		// Token: 0x1700147E RID: 5246
		// (get) Token: 0x0600435C RID: 17244 RVA: 0x00114826 File Offset: 0x00112A26
		protected override string Name
		{
			get
			{
				return "MSExchangeFDS";
			}
		}

		// Token: 0x0600435D RID: 17245 RVA: 0x0011482D File Offset: 0x00112A2D
		private void AfterInstallEventHandler(object sender, InstallEventArgs e)
		{
			base.LockdownServiceAccess();
		}

		// Token: 0x040029FC RID: 10748
		private const string ServiceShortName = "MSExchangeFDS";

		// Token: 0x040029FD RID: 10749
		private const string ServiceBinaryName = "MSExchangeFDS.exe";

		// Token: 0x040029FE RID: 10750
		private const string EventLogBinaryName = "Microsoft.Exchange.Data.FileDistributionService.EventLog.dll";

		// Token: 0x040029FF RID: 10751
		public const string WorkstationService = "lanmanworkstation";

		// Token: 0x04002A00 RID: 10752
		public static readonly string ActiveDirectoryTopologyService = "MSExchangeADTopology";
	}
}
