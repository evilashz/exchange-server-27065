using System;
using System.Configuration.Install;
using System.IO;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200013D RID: 317
	public abstract class ManageComplianceAuditService : ManageService
	{
		// Token: 0x06000B6A RID: 2922 RVA: 0x0003563C File Offset: 0x0003383C
		protected ManageComplianceAuditService()
		{
			base.Account = ServiceAccount.LocalSystem;
			base.StartMode = ServiceStartMode.Automatic;
			base.DisplayName = Strings.ComplianceAuditServiceDisplayName;
			base.Description = Strings.ComplianceAuditServiceDescription;
			InstallContext installContext = new InstallContext();
			installContext.Parameters["logtoconsole"] = "false";
			installContext.Parameters["assemblypath"] = Path.Combine(ConfigurationContext.Setup.BinPath, "ComplianceAuditService.exe");
			base.FirstFailureActionType = ServiceActionType.Restart;
			base.FirstFailureActionDelay = 5000U;
			base.SecondFailureActionType = ServiceActionType.Restart;
			base.SecondFailureActionDelay = 5000U;
			base.AllOtherFailuresActionType = ServiceActionType.Restart;
			base.AllOtherFailuresActionDelay = 5000U;
			base.FailureResetPeriod = 0U;
			base.ServiceInstallContext = installContext;
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000B6B RID: 2923 RVA: 0x000356FB File Offset: 0x000338FB
		protected override string Name
		{
			get
			{
				return "MSComplianceAudit";
			}
		}

		// Token: 0x0400059A RID: 1434
		private const string ServiceShortName = "MSComplianceAudit";

		// Token: 0x0400059B RID: 1435
		private const string ServiceBinaryName = "ComplianceAuditService.exe";
	}
}
