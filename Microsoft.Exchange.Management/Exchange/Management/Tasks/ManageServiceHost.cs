using System;
using System.Configuration.Install;
using System.IO;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Security.WindowsFirewall;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000763 RID: 1891
	public abstract class ManageServiceHost : ManageService
	{
		// Token: 0x06004334 RID: 17204 RVA: 0x00114044 File Offset: 0x00112244
		protected ManageServiceHost()
		{
			base.Account = ServiceAccount.LocalSystem;
			base.StartMode = ServiceStartMode.Automatic;
			base.DisplayName = Strings.ServiceHostDisplayName;
			base.Description = Strings.ServiceHostDescription;
			string path = Path.Combine(ConfigurationContext.Setup.InstallPath, this.RelativeInstallPath);
			InstallContext installContext = new InstallContext();
			installContext.Parameters["logtoconsole"] = "false";
			installContext.Parameters["assemblypath"] = Path.Combine(path, "Microsoft.Exchange.ServiceHost.exe");
			base.FirstFailureActionType = ServiceActionType.Restart;
			base.FirstFailureActionDelay = 5000U;
			base.SecondFailureActionType = ServiceActionType.Restart;
			base.SecondFailureActionDelay = 5000U;
			base.AllOtherFailuresActionType = ServiceActionType.Restart;
			base.AllOtherFailuresActionDelay = 5000U;
			base.FailureResetPeriod = 0U;
			base.ServiceInstallContext = installContext;
			base.CategoryCount = 1;
			base.EventMessageFile = Path.Combine(ConfigurationContext.Setup.ResPath, "Microsoft.Exchange.ServiceHost.EventLog.dll");
			base.ServiceInstaller.AfterInstall += this.AfterInstallEventHandler;
			base.AddFirewallRule(new MSExchangeServiceHostRPCFirewallRule());
			base.AddFirewallRule(new MSExchangeServiceHostRPCEPMapFirewallRule());
		}

		// Token: 0x06004335 RID: 17205 RVA: 0x00114159 File Offset: 0x00112359
		private void AfterInstallEventHandler(object sender, InstallEventArgs e)
		{
			base.LockdownServiceAccess();
		}

		// Token: 0x17001474 RID: 5236
		// (get) Token: 0x06004336 RID: 17206 RVA: 0x00114161 File Offset: 0x00112361
		protected override string Name
		{
			get
			{
				return "MSExchangeServiceHost";
			}
		}

		// Token: 0x17001475 RID: 5237
		// (get) Token: 0x06004337 RID: 17207 RVA: 0x00114168 File Offset: 0x00112368
		protected string RelativeInstallPath
		{
			get
			{
				return "bin";
			}
		}

		// Token: 0x040029EF RID: 10735
		public bool ForceFailure;
	}
}
