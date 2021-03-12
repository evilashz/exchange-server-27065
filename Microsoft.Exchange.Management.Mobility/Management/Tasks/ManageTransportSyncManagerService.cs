using System;
using System.Configuration.Install;
using System.IO;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000042 RID: 66
	public abstract class ManageTransportSyncManagerService : ManageService
	{
		// Token: 0x0600028B RID: 651 RVA: 0x0000BE08 File Offset: 0x0000A008
		protected ManageTransportSyncManagerService()
		{
			base.Account = ServiceAccount.LocalSystem;
			base.StartMode = ServiceStartMode.Automatic;
			base.DisplayName = Strings.TransportSyncManagerServiceDisplayName;
			base.Description = Strings.TransportSyncManagerServiceDescription;
			InstallContext installContext = new InstallContext();
			installContext.Parameters["logtoconsole"] = "false";
			installContext.Parameters["assemblypath"] = Path.Combine(ConfigurationContext.Setup.BinPath, "Microsoft.Exchange.TransportSyncManagerSvc.exe");
			base.FirstFailureActionType = ServiceActionType.Restart;
			base.FirstFailureActionDelay = 5000U;
			base.SecondFailureActionType = ServiceActionType.Restart;
			base.SecondFailureActionDelay = 5000U;
			base.AllOtherFailuresActionType = ServiceActionType.Restart;
			base.AllOtherFailuresActionDelay = 5000U;
			base.FailureResetPeriod = 0U;
			base.ServiceInstallContext = installContext;
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x0600028C RID: 652 RVA: 0x0000BEC7 File Offset: 0x0000A0C7
		protected override string Name
		{
			get
			{
				return "MSExchangeTransportSyncManagerSvc";
			}
		}

		// Token: 0x040000B8 RID: 184
		private const string ServiceShortName = "MSExchangeTransportSyncManagerSvc";

		// Token: 0x040000B9 RID: 185
		private const string ServiceBinaryName = "Microsoft.Exchange.TransportSyncManagerSvc.exe";
	}
}
