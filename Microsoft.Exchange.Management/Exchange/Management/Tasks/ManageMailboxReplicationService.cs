using System;
using System.Collections.Generic;
using System.Configuration.Install;
using System.IO;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Security.WindowsFirewall;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000C70 RID: 3184
	public abstract class ManageMailboxReplicationService : ManageService
	{
		// Token: 0x0600798F RID: 31119 RVA: 0x001EF744 File Offset: 0x001ED944
		public ManageMailboxReplicationService()
		{
			base.Account = ServiceAccount.LocalSystem;
			base.StartMode = ServiceStartMode.Automatic;
			base.DisplayName = Strings.MailboxReplicationServiceDisplayName;
			base.Description = Strings.MailboxReplicationServiceDescription;
			InstallContext installContext = new InstallContext();
			installContext.Parameters["logtoconsole"] = "false";
			installContext.Parameters["assemblypath"] = Path.Combine(ConfigurationContext.Setup.BinPath, "MSExchangeMailboxReplication.exe");
			base.EventMessageFile = Path.Combine(ConfigurationContext.Setup.ResPath, "Microsoft.Exchange.MailboxReplicationService.EventLog.dll");
			base.CategoryCount = 2;
			base.FirstFailureActionType = ServiceActionType.Restart;
			base.FirstFailureActionDelay = 5000U;
			base.SecondFailureActionType = ServiceActionType.Restart;
			base.SecondFailureActionDelay = 5000U;
			base.AllOtherFailuresActionType = ServiceActionType.Restart;
			base.AllOtherFailuresActionDelay = 5000U;
			base.FailureResetPeriod = 0U;
			base.ServiceInstallContext = installContext;
			base.ServicesDependedOn = new List<string>(base.ServicesDependedOn)
			{
				"NetTcpPortSharing"
			}.ToArray();
			base.AddFirewallRule(new MSExchangeMailboxReplicationFirewallRule());
		}

		// Token: 0x1700259F RID: 9631
		// (get) Token: 0x06007990 RID: 31120 RVA: 0x001EF84D File Offset: 0x001EDA4D
		protected override string Name
		{
			get
			{
				return "MSExchangeMailboxReplication";
			}
		}

		// Token: 0x04003C60 RID: 15456
		private const string ServiceShortName = "MSExchangeMailboxReplication";

		// Token: 0x04003C61 RID: 15457
		private const string ServiceBinaryName = "MSExchangeMailboxReplication.exe";

		// Token: 0x04003C62 RID: 15458
		private const string EventLogBinaryName = "Microsoft.Exchange.MailboxReplicationService.EventLog.dll";
	}
}
