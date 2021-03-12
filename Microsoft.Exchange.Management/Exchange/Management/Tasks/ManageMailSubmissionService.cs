using System;
using System.Configuration.Install;
using System.IO;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200045E RID: 1118
	public abstract class ManageMailSubmissionService : ManageService
	{
		// Token: 0x06002788 RID: 10120 RVA: 0x0009C47C File Offset: 0x0009A67C
		public ManageMailSubmissionService()
		{
			base.Account = ServiceAccount.LocalSystem;
			base.StartMode = ServiceStartMode.Disabled;
			base.DisplayName = Strings.MailSubmissionServiceDisplayName;
			base.Description = Strings.MailSubmissionServiceDescription;
			InstallContext installContext = new InstallContext();
			installContext.Parameters["logtoconsole"] = "false";
			installContext.Parameters["assemblypath"] = Path.Combine(ConfigurationContext.Setup.BinPath, "MSExchangeMailSubmission.exe");
			base.EventMessageFile = Path.Combine(ConfigurationContext.Setup.ResPath, "Microsoft.Exchange.MailSubmission.EventLog.dll");
			base.CategoryCount = 1;
			base.FirstFailureActionType = ServiceActionType.Restart;
			base.FirstFailureActionDelay = 5000U;
			base.SecondFailureActionType = ServiceActionType.Restart;
			base.SecondFailureActionDelay = 5000U;
			base.AllOtherFailuresActionType = ServiceActionType.Restart;
			base.AllOtherFailuresActionDelay = 5000U;
			base.FailureResetPeriod = 0U;
			base.ServiceInstallContext = installContext;
		}

		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x06002789 RID: 10121 RVA: 0x0009C557 File Offset: 0x0009A757
		protected override string Name
		{
			get
			{
				return "MSExchangeMailSubmission";
			}
		}

		// Token: 0x04001DA1 RID: 7585
		private const string ServiceShortName = "MSExchangeMailSubmission";

		// Token: 0x04001DA2 RID: 7586
		private const string ServiceBinaryName = "MSExchangeMailSubmission.exe";

		// Token: 0x04001DA3 RID: 7587
		private const string EventLogBinaryName = "Microsoft.Exchange.MailSubmission.EventLog.dll";
	}
}
