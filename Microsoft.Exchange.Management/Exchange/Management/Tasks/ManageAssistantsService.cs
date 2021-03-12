using System;
using System.Configuration.Install;
using System.IO;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Security.WindowsFirewall;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000415 RID: 1045
	public abstract class ManageAssistantsService : ManageService
	{
		// Token: 0x06002476 RID: 9334 RVA: 0x00091384 File Offset: 0x0008F584
		public ManageAssistantsService()
		{
			base.Account = ServiceAccount.LocalSystem;
			base.StartMode = ServiceStartMode.Automatic;
			base.DisplayName = Strings.AssistantsServiceDisplayName;
			base.Description = Strings.AssistantsServiceDescription;
			InstallContext installContext = new InstallContext();
			installContext.Parameters["logtoconsole"] = "false";
			installContext.Parameters["assemblypath"] = Path.Combine(ConfigurationContext.Setup.BinPath, "MSExchangeMailboxAssistants.exe");
			base.EventMessageFile = Path.Combine(ConfigurationContext.Setup.ResPath, "Microsoft.Exchange.InfoWorker.Eventlog.dll");
			base.CategoryCount = 19;
			base.FirstFailureActionType = ServiceActionType.Restart;
			base.FirstFailureActionDelay = 5000U;
			base.SecondFailureActionType = ServiceActionType.Restart;
			base.SecondFailureActionDelay = 5000U;
			base.AllOtherFailuresActionType = ServiceActionType.Restart;
			base.AllOtherFailuresActionDelay = 5000U;
			base.FailureResetPeriod = 0U;
			base.ServiceInstallContext = installContext;
			base.AddFirewallRule(new MSExchangeMailboxAssistantsRPCFirewallRule());
			base.AddFirewallRule(new MSExchangeMailboxAssistantsRPCEPMapFirewallRule());
		}

		// Token: 0x17000ABF RID: 2751
		// (get) Token: 0x06002477 RID: 9335 RVA: 0x00091476 File Offset: 0x0008F676
		protected override string Name
		{
			get
			{
				return "MSExchangeMailboxAssistants";
			}
		}

		// Token: 0x04001CEB RID: 7403
		private const string ServiceShortName = "MSExchangeMailboxAssistants";

		// Token: 0x04001CEC RID: 7404
		private const string ServiceBinaryName = "MSExchangeMailboxAssistants.exe";

		// Token: 0x04001CED RID: 7405
		private const string EventLogBinaryName = "Microsoft.Exchange.InfoWorker.Eventlog.dll";
	}
}
