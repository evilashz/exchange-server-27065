using System;
using System.Configuration.Install;
using System.IO;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020000D5 RID: 213
	public abstract class ManageCentralAdminService : ManageService
	{
		// Token: 0x0600066F RID: 1647 RVA: 0x0001B750 File Offset: 0x00019950
		protected ManageCentralAdminService()
		{
			base.Account = ServiceAccount.LocalSystem;
			base.StartMode = ServiceStartMode.Automatic;
			base.DisplayName = Strings.CentralAdminServiceDisplayName;
			base.Description = Strings.CentralAdminServiceDescription;
			InstallContext installContext = new InstallContext();
			installContext.Parameters["logtoconsole"] = "false";
			installContext.Parameters["assemblypath"] = Path.Combine(ConfigurationContext.Setup.BinPath, "Microsoft.Exchange.Management.CentralAdmin.CentralAdminService.exe");
			base.FirstFailureActionType = ServiceActionType.Restart;
			base.FirstFailureActionDelay = 5000U;
			base.SecondFailureActionType = ServiceActionType.Restart;
			base.SecondFailureActionDelay = 5000U;
			base.AllOtherFailuresActionType = ServiceActionType.Restart;
			base.AllOtherFailuresActionDelay = 5000U;
			base.ServiceInstallContext = installContext;
			base.CategoryCount = 1;
			base.EventMessageFile = Path.Combine(ConfigurationContext.Setup.BinPath, "Microsoft.Exchange.Management.CentralAdmin.CentralAdminServiceMsg.dll");
			base.ServicesDependedOn = null;
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x06000670 RID: 1648 RVA: 0x0001B82B File Offset: 0x00019A2B
		protected override string Name
		{
			get
			{
				return "MSExchangeCentralAdmin";
			}
		}

		// Token: 0x04000308 RID: 776
		protected const string ServiceShortName = "MSExchangeCentralAdmin";

		// Token: 0x04000309 RID: 777
		private const string ServiceBinaryName = "Microsoft.Exchange.Management.CentralAdmin.CentralAdminService.exe";

		// Token: 0x0400030A RID: 778
		private const string EventLogBinaryName = "Microsoft.Exchange.Management.CentralAdmin.CentralAdminServiceMsg.dll";
	}
}
