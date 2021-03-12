using System;
using System.Configuration.Install;
using System.IO;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000472 RID: 1138
	public abstract class ManageMailboxTransportSubmissionService : ManageService
	{
		// Token: 0x06002837 RID: 10295 RVA: 0x0009E7F8 File Offset: 0x0009C9F8
		public ManageMailboxTransportSubmissionService()
		{
			base.Account = ServiceAccount.LocalSystem;
			base.StartMode = ServiceStartMode.Automatic;
			base.DisplayName = Strings.MailboxTransportSubmissionServiceDisplayName;
			base.Description = Strings.MailboxTransportSubmissionServiceDescription;
			InstallContext installContext = new InstallContext();
			installContext.Parameters["logtoconsole"] = "false";
			installContext.Parameters["assemblypath"] = Path.Combine(ConfigurationContext.Setup.BinPath, "MSExchangeSubmission.exe");
			base.FirstFailureActionType = ServiceActionType.Restart;
			base.FirstFailureActionDelay = 5000U;
			base.SecondFailureActionType = ServiceActionType.Restart;
			base.SecondFailureActionDelay = 5000U;
			base.AllOtherFailuresActionType = ServiceActionType.Restart;
			base.AllOtherFailuresActionDelay = 5000U;
			base.FailureResetPeriod = 0U;
			base.ServiceInstallContext = installContext;
		}

		// Token: 0x17000C04 RID: 3076
		// (get) Token: 0x06002838 RID: 10296 RVA: 0x0009E8B7 File Offset: 0x0009CAB7
		protected override string Name
		{
			get
			{
				return "MSExchangeSubmission";
			}
		}

		// Token: 0x04001DE1 RID: 7649
		private const string ServiceShortName = "MSExchangeSubmission";

		// Token: 0x04001DE2 RID: 7650
		private const string ServiceBinaryName = "MSExchangeSubmission.exe";

		// Token: 0x04001DE3 RID: 7651
		private const int SubmissionServiceFirstFailureActionDelay = 5000;

		// Token: 0x04001DE4 RID: 7652
		private const int SubmissionServiceSecondFailureActionDelay = 5000;

		// Token: 0x04001DE5 RID: 7653
		private const int SubmissionServiceAllOtherFailuresActionDelay = 5000;

		// Token: 0x04001DE6 RID: 7654
		private const int SubmissionServiceFailureResetPeriod = 0;
	}
}
