using System;
using System.Configuration.Install;
using System.IO;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000C73 RID: 3187
	public abstract class ManageMigrationWorkflowService : ManageService
	{
		// Token: 0x06007995 RID: 31125 RVA: 0x001EF888 File Offset: 0x001EDA88
		protected ManageMigrationWorkflowService()
		{
			base.Account = ServiceAccount.LocalSystem;
			base.StartMode = ServiceStartMode.Automatic;
			base.DisplayName = Strings.MigrationWorkflowServiceDisplayName;
			base.Description = Strings.MigrationWorkflowServiceDescription;
			InstallContext installContext = new InstallContext();
			installContext.Parameters["logtoconsole"] = "false";
			installContext.Parameters["assemblypath"] = Path.Combine(ConfigurationContext.Setup.BinPath, "MSExchangeMigrationWorkflow.exe");
			base.EventMessageFile = Path.Combine(ConfigurationContext.Setup.ResPath, "Microsoft.Exchange.MigrationWorkflowService.EventLog.dll");
			base.CategoryCount = 2;
			base.FirstFailureActionType = ServiceActionType.Restart;
			base.FirstFailureActionDelay = 5000U;
			base.SecondFailureActionType = ServiceActionType.Restart;
			base.SecondFailureActionDelay = 5000U;
			base.AllOtherFailuresActionType = ServiceActionType.Restart;
			base.AllOtherFailuresActionDelay = 5000U;
			base.FailureResetPeriod = 0U;
			base.ServiceInstallContext = installContext;
			base.ServicesDependedOn = ManageMigrationWorkflowService.ServicesDependencies;
		}

		// Token: 0x170025A0 RID: 9632
		// (get) Token: 0x06007996 RID: 31126 RVA: 0x001EF96E File Offset: 0x001EDB6E
		protected override string Name
		{
			get
			{
				return "MSExchangeMigrationWorkflow";
			}
		}

		// Token: 0x04003C63 RID: 15459
		private const string ServiceShortName = "MSExchangeMigrationWorkflow";

		// Token: 0x04003C64 RID: 15460
		private const string ServiceBinaryName = "MSExchangeMigrationWorkflow.exe";

		// Token: 0x04003C65 RID: 15461
		private const string EventLogBinaryName = "Microsoft.Exchange.MigrationWorkflowService.EventLog.dll";

		// Token: 0x04003C66 RID: 15462
		private static readonly string[] ServicesDependencies = new string[]
		{
			"NetTcpPortSharing"
		};
	}
}
