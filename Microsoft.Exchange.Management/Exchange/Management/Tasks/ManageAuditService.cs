using System;
using System.Configuration.Install;
using System.IO;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200009B RID: 155
	public abstract class ManageAuditService : ManageService
	{
		// Token: 0x06000519 RID: 1305 RVA: 0x00015798 File Offset: 0x00013998
		protected ManageAuditService()
		{
			base.Account = ServiceAccount.LocalSystem;
			base.StartMode = ServiceStartMode.Automatic;
			base.DisplayName = "Microsoft Exchange Audit Service";
			base.Description = "Exchange security audits.";
			string binPath = ConfigurationContext.Setup.BinPath;
			InstallContext installContext = new InstallContext();
			installContext.Parameters["logtoconsole"] = "false";
			installContext.Parameters["assemblypath"] = Path.Combine(binPath, "Microsoft.Exchange.Audit.Service.exe");
			base.FirstFailureActionType = ServiceActionType.Restart;
			base.FirstFailureActionDelay = 5000U;
			base.SecondFailureActionType = ServiceActionType.Restart;
			base.SecondFailureActionDelay = 5000U;
			base.AllOtherFailuresActionType = ServiceActionType.Restart;
			base.AllOtherFailuresActionDelay = 5000U;
			base.FailureResetPeriod = 0U;
			base.FailureActionsFlag = true;
			base.ServiceInstallContext = installContext;
			base.CategoryCount = 2;
			base.ServicesDependedOn = null;
		}

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x0600051A RID: 1306 RVA: 0x00015864 File Offset: 0x00013A64
		protected override string Name
		{
			get
			{
				return "MSExchangeAS";
			}
		}

		// Token: 0x0400027B RID: 635
		protected const string ServiceShortName = "MSExchangeAS";

		// Token: 0x0400027C RID: 636
		private const string ServiceBinaryName = "Microsoft.Exchange.Audit.Service.exe";
	}
}
