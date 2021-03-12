using System;
using System.Configuration.Install;
using System.IO;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200074E RID: 1870
	public abstract class ManageSearchService : ManageService
	{
		// Token: 0x06004259 RID: 16985 RVA: 0x0010FAA0 File Offset: 0x0010DCA0
		protected ManageSearchService()
		{
			base.Account = ServiceAccount.LocalSystem;
			base.StartMode = ServiceStartMode.Automatic;
			base.DisplayName = Strings.SearchServiceDisplayName;
			base.Description = Strings.SearchServiceDescription;
			string path = Path.Combine(ConfigurationContext.Setup.InstallPath, this.RelativeInstallPath);
			InstallContext installContext = new InstallContext();
			installContext.Parameters["logtoconsole"] = "false";
			installContext.Parameters["assemblypath"] = Path.Combine(path, "Microsoft.Exchange.Search.Service.exe");
			base.FirstFailureActionType = ServiceActionType.Restart;
			base.FirstFailureActionDelay = 5000U;
			base.SecondFailureActionType = ServiceActionType.Restart;
			base.SecondFailureActionDelay = 5000U;
			base.AllOtherFailuresActionType = ServiceActionType.Restart;
			base.AllOtherFailuresActionDelay = 5000U;
			base.FailureResetPeriod = 0U;
			base.FailureActionsFlag = true;
			base.ServiceInstallContext = installContext;
			base.CategoryCount = 1;
			base.EventMessageFile = Path.Combine(ConfigurationContext.Setup.ResPath, "Microsoft.Exchange.Search.Service.EventLog.dll");
			base.ServiceInstaller.AfterInstall += this.AfterInstallEventHandler;
		}

		// Token: 0x0600425A RID: 16986 RVA: 0x0010FBA6 File Offset: 0x0010DDA6
		private void AfterInstallEventHandler(object sender, InstallEventArgs e)
		{
			base.LockdownServiceAccess();
		}

		// Token: 0x17001424 RID: 5156
		// (get) Token: 0x0600425B RID: 16987 RVA: 0x0010FBAE File Offset: 0x0010DDAE
		protected override string Name
		{
			get
			{
				return "MSExchangeFastSearch";
			}
		}

		// Token: 0x17001425 RID: 5157
		// (get) Token: 0x0600425C RID: 16988 RVA: 0x0010FBB5 File Offset: 0x0010DDB5
		protected string RelativeInstallPath
		{
			get
			{
				return "bin";
			}
		}

		// Token: 0x040029B6 RID: 10678
		public bool ForceFailure;
	}
}
