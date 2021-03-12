using System;
using System.Collections.Generic;
using System.Configuration.Install;
using System.IO;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Management.Tasks;
using Microsoft.Exchange.Security.WindowsFirewall;

namespace Microsoft.Exchange.Management.SystemConfigurationTasks
{
	// Token: 0x020008CE RID: 2254
	public abstract class ManageDagMgmtService : ManageService
	{
		// Token: 0x06005013 RID: 20499 RVA: 0x0014F4E4 File Offset: 0x0014D6E4
		protected ManageDagMgmtService()
		{
			base.Account = ServiceAccount.LocalSystem;
			base.StartMode = ServiceStartMode.Automatic;
			base.DisplayName = Strings.DagMgmtServiceDisplayName;
			base.Description = Strings.DagMgmtServiceDescription;
			InstallContext installContext = new InstallContext();
			installContext.Parameters["logtoconsole"] = "false";
			installContext.Parameters["assemblypath"] = Path.Combine(ConfigurationContext.Setup.BinPath, "MSExchangeDagMgmt.exe");
			base.ServiceInstallContext = installContext;
			base.EventMessageFile = Path.Combine(ConfigurationContext.Setup.BinPath, "clusmsg.dll");
			base.CategoryCount = 6;
			base.FirstFailureActionType = ServiceActionType.Restart;
			base.FirstFailureActionDelay = 5000U;
			base.SecondFailureActionType = ServiceActionType.Restart;
			base.SecondFailureActionDelay = 5000U;
			base.AllOtherFailuresActionType = ServiceActionType.Restart;
			base.AllOtherFailuresActionDelay = 5000U;
			base.FailureResetPeriod = 0U;
			base.ServiceInstaller.AfterInstall += this.AfterInstallEventHandler;
			base.ServicesDependedOn = new List<string>(base.ServicesDependedOn)
			{
				"NetTcpPortSharing"
			}.ToArray();
			base.AddFirewallRule(new MSExchangeDagMgmtWcfServiceFirewallRule());
		}

		// Token: 0x170017EC RID: 6124
		// (get) Token: 0x06005014 RID: 20500 RVA: 0x0014F604 File Offset: 0x0014D804
		protected override string Name
		{
			get
			{
				return "MSExchangeDagMgmt";
			}
		}

		// Token: 0x06005015 RID: 20501 RVA: 0x0014F60B File Offset: 0x0014D80B
		private void AfterInstallEventHandler(object sender, InstallEventArgs e)
		{
			base.LockdownServiceAccess();
		}

		// Token: 0x04002F40 RID: 12096
		private const string ServiceShortName = "MSExchangeDagMgmt";

		// Token: 0x04002F41 RID: 12097
		private const string ServiceBinaryName = "MSExchangeDagMgmt.exe";

		// Token: 0x04002F42 RID: 12098
		private const string EventLogBinaryName = "clusmsg.dll";
	}
}
