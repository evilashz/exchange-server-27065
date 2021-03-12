using System;
using System.Configuration.Install;
using System.IO;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000766 RID: 1894
	public abstract class ManageProtectedServiceHost : ManageService
	{
		// Token: 0x0600433F RID: 17215 RVA: 0x001141F4 File Offset: 0x001123F4
		protected ManageProtectedServiceHost()
		{
			base.Account = ServiceAccount.LocalSystem;
			base.StartMode = ServiceStartMode.Automatic;
			base.DisplayName = Strings.ProtectedServiceHostDisplayName;
			base.Description = Strings.ProtectedServiceHostDescription;
			string path = Path.Combine(ConfigurationContext.Setup.InstallPath, this.RelativeInstallPath);
			InstallContext installContext = new InstallContext();
			installContext.Parameters["logtoconsole"] = "false";
			installContext.Parameters["assemblypath"] = Path.Combine(path, "Microsoft.Exchange.ProtectedServiceHost.exe");
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
		}

		// Token: 0x06004340 RID: 17216 RVA: 0x001142F3 File Offset: 0x001124F3
		private void AfterInstallEventHandler(object sender, InstallEventArgs e)
		{
			base.LockdownServiceAccess();
		}

		// Token: 0x17001477 RID: 5239
		// (get) Token: 0x06004341 RID: 17217 RVA: 0x001142FB File Offset: 0x001124FB
		protected override string Name
		{
			get
			{
				return "MSExchangeProtectedServiceHost";
			}
		}

		// Token: 0x17001478 RID: 5240
		// (get) Token: 0x06004342 RID: 17218 RVA: 0x00114302 File Offset: 0x00112502
		protected string RelativeInstallPath
		{
			get
			{
				return "bin";
			}
		}

		// Token: 0x040029F1 RID: 10737
		public bool ForceFailure;
	}
}
