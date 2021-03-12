using System;
using System.Configuration.Install;
using System.IO;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200063F RID: 1599
	public abstract class ManageProcessUtilizationManagerService : ManageService
	{
		// Token: 0x06003819 RID: 14361 RVA: 0x000E830C File Offset: 0x000E650C
		protected ManageProcessUtilizationManagerService()
		{
			base.Account = ServiceAccount.LocalSystem;
			base.StartMode = ServiceStartMode.Automatic;
			base.DisplayName = Strings.ProcessUtilizationManagerServiceDisplayName;
			base.Description = Strings.ProcessUtilizationManagerServiceDescription;
			string binPath = ConfigurationContext.Setup.BinPath;
			InstallContext installContext = new InstallContext();
			installContext.Parameters["logtoconsole"] = "false";
			installContext.Parameters["assemblypath"] = Path.Combine(binPath, "Microsoft.Exchange.ProcessUtilizationManager.exe");
			base.FirstFailureActionType = ServiceActionType.Restart;
			base.FirstFailureActionDelay = 5000U;
			base.SecondFailureActionType = ServiceActionType.Restart;
			base.SecondFailureActionDelay = 5000U;
			base.AllOtherFailuresActionType = ServiceActionType.Restart;
			base.AllOtherFailuresActionDelay = 60000U;
			base.FailureResetPeriod = 3600U;
			base.FailureActionsFlag = true;
			base.ServiceInstallContext = installContext;
			base.CategoryCount = 2;
			base.ServicesDependedOn = null;
			base.CategoryCount = 2;
			base.EventMessageFile = Path.Combine(ConfigurationContext.Setup.BinPath, "Microsoft.Exchange.ProcessUtilizationManager.EventLog.dll");
		}

		// Token: 0x170010C2 RID: 4290
		// (get) Token: 0x0600381A RID: 14362 RVA: 0x000E8402 File Offset: 0x000E6602
		protected override string Name
		{
			get
			{
				return "MSExchangeProcessUtilizationManager";
			}
		}

		// Token: 0x040025BD RID: 9661
		protected const string ServiceShortName = "MSExchangeProcessUtilizationManager";

		// Token: 0x040025BE RID: 9662
		private const string ServiceBinaryName = "Microsoft.Exchange.ProcessUtilizationManager.exe";

		// Token: 0x040025BF RID: 9663
		private const string EventLogBinaryName = "Microsoft.Exchange.ProcessUtilizationManager.EventLog.dll";
	}
}
