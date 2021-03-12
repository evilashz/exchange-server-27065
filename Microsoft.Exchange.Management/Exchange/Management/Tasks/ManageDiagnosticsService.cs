using System;
using System.Configuration.Install;
using System.IO;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020002B4 RID: 692
	public abstract class ManageDiagnosticsService : ManageService
	{
		// Token: 0x0600185C RID: 6236 RVA: 0x00067320 File Offset: 0x00065520
		protected ManageDiagnosticsService()
		{
			base.Account = ServiceAccount.LocalSystem;
			base.StartMode = ServiceStartMode.Automatic;
			base.DisplayName = Strings.DiagnosticsServiceDisplayName;
			base.Description = Strings.DiagnosticsServiceDescription;
			string binPath = ConfigurationContext.Setup.BinPath;
			InstallContext installContext = new InstallContext();
			installContext.Parameters["logtoconsole"] = "false";
			installContext.Parameters["assemblypath"] = Path.Combine(binPath, "Microsoft.Exchange.Diagnostics.Service.exe");
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
			base.EventMessageFile = Path.Combine(ConfigurationContext.Setup.BinPath, "Microsoft.Exchange.Diagnostics.Service.EventLog.dll");
		}

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x0600185D RID: 6237 RVA: 0x00067416 File Offset: 0x00065616
		protected override string Name
		{
			get
			{
				return "MSExchangeDiagnostics";
			}
		}

		// Token: 0x04000A9F RID: 2719
		protected const string ServiceShortName = "MSExchangeDiagnostics";

		// Token: 0x04000AA0 RID: 2720
		private const string ServiceBinaryName = "Microsoft.Exchange.Diagnostics.Service.exe";

		// Token: 0x04000AA1 RID: 2721
		private const string EventLogBinaryName = "Microsoft.Exchange.Diagnostics.Service.EventLog.dll";
	}
}
