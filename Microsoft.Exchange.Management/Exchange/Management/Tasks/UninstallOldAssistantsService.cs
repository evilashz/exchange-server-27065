using System;
using System.Configuration.Install;
using System.IO;
using System.Management.Automation;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000418 RID: 1048
	[LocDescription(Strings.IDs.UninstallOldAssistantsServiceTask)]
	[Cmdlet("Uninstall", "OldAssistantsService")]
	public class UninstallOldAssistantsService : ManageService
	{
		// Token: 0x0600247C RID: 9340 RVA: 0x000914B1 File Offset: 0x0008F6B1
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Uninstall();
			TaskLogger.LogExit();
		}

		// Token: 0x0600247D RID: 9341 RVA: 0x000914C4 File Offset: 0x0008F6C4
		public UninstallOldAssistantsService()
		{
			base.Account = ServiceAccount.LocalSystem;
			base.StartMode = ServiceStartMode.Automatic;
			base.DisplayName = "Microsoft Exchange Mailbox Assistants";
			base.Description = Strings.AssistantsServiceDescription;
			InstallContext installContext = new InstallContext();
			installContext.Parameters["logtoconsole"] = "false";
			installContext.Parameters["assemblypath"] = Path.Combine(ConfigurationContext.Setup.BinPath, "Microsoft.Exchange.InfoWorker.Assistants.exe");
			base.EventMessageFile = Path.Combine(ConfigurationContext.Setup.ResPath, "Microsoft.Exchange.InfoWorker.Eventlog.dll");
			base.CategoryCount = 10;
			base.ServiceInstallContext = installContext;
		}

		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x0600247E RID: 9342 RVA: 0x0009155E File Offset: 0x0008F75E
		protected override string Name
		{
			get
			{
				return "MSExchangeMA";
			}
		}

		// Token: 0x04001CEE RID: 7406
		private const string ServiceShortName = "MSExchangeMA";

		// Token: 0x04001CEF RID: 7407
		private const string ServiceDisplayName = "Microsoft Exchange Mailbox Assistants";

		// Token: 0x04001CF0 RID: 7408
		private const string ServiceBinaryName = "Microsoft.Exchange.InfoWorker.Assistants.exe";

		// Token: 0x04001CF1 RID: 7409
		private const string EventLogBinaryName = "Microsoft.Exchange.InfoWorker.Eventlog.dll";
	}
}
