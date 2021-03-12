using System;
using System.Configuration.Install;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000769 RID: 1897
	public abstract class ManageExchangeFileUploadService : ManageService
	{
		// Token: 0x0600434A RID: 17226 RVA: 0x0011438C File Offset: 0x0011258C
		protected ManageExchangeFileUploadService()
		{
			base.Account = ServiceAccount.LocalSystem;
			base.StartMode = ServiceStartMode.Automatic;
			base.DisplayName = Strings.FileUploadServiceDisplayName;
			base.Description = Strings.FileUploadServiceDescription;
			string path = Path.Combine(ConfigurationContext.Setup.InstallPath, this.RelativeInstallPath);
			InstallContext installContext = new InstallContext();
			installContext.Parameters["logtoconsole"] = "false";
			installContext.Parameters["assemblypath"] = Path.Combine(path, "Microsoft.Exchange.Management.DataMining.ExchangeFileUpload.exe");
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
			foreach (object obj in base.ServiceInstaller.Installers)
			{
				EventLogInstaller eventLogInstaller = obj as EventLogInstaller;
				if (eventLogInstaller != null)
				{
					eventLogInstaller.Source = "MSExchange DataMining";
					eventLogInstaller.Log = "Application";
				}
			}
		}

		// Token: 0x1700147A RID: 5242
		// (get) Token: 0x0600434B RID: 17227 RVA: 0x001144DC File Offset: 0x001126DC
		protected override string Name
		{
			get
			{
				return "MSExchangeFileUpload";
			}
		}

		// Token: 0x1700147B RID: 5243
		// (get) Token: 0x0600434C RID: 17228 RVA: 0x001144E3 File Offset: 0x001126E3
		protected string RelativeInstallPath
		{
			get
			{
				return "Datacenter\\DataMining\\Cosmos";
			}
		}

		// Token: 0x040029F3 RID: 10739
		protected const string ServiceShortName = "MSExchangeFileUpload";

		// Token: 0x040029F4 RID: 10740
		private const string ServiceBinaryName = "Microsoft.Exchange.Management.DataMining.ExchangeFileUpload.exe";

		// Token: 0x040029F5 RID: 10741
		private const string EventLogName = "Application";

		// Token: 0x040029F6 RID: 10742
		private const string EventLogSourceName = "MSExchange DataMining";
	}
}
