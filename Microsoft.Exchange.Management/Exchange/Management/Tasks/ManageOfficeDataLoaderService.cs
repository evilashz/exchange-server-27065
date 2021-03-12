using System;
using System.Configuration.Install;
using System.Diagnostics;
using System.IO;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200076C RID: 1900
	public abstract class ManageOfficeDataLoaderService : ManageService
	{
		// Token: 0x06004351 RID: 17233 RVA: 0x00114520 File Offset: 0x00112720
		protected ManageOfficeDataLoaderService()
		{
			base.Account = ServiceAccount.LocalSystem;
			base.StartMode = ServiceStartMode.Automatic;
			base.DisplayName = Strings.OfficeDataLoaderServiceDisplayName;
			base.Description = Strings.OfficeDataLoaderServiceDescription;
			string path = Path.Combine(ConfigurationContext.Setup.InstallPath, this.RelativeInstallPath);
			InstallContext installContext = new InstallContext();
			installContext.Parameters["logtoconsole"] = "false";
			installContext.Parameters["assemblypath"] = Path.Combine(path, "Microsoft.Office.BigData.DataLoader.exe");
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

		// Token: 0x1700147C RID: 5244
		// (get) Token: 0x06004352 RID: 17234 RVA: 0x00114670 File Offset: 0x00112870
		protected override string Name
		{
			get
			{
				return "MSOfficeDataLoader";
			}
		}

		// Token: 0x1700147D RID: 5245
		// (get) Token: 0x06004353 RID: 17235 RVA: 0x00114677 File Offset: 0x00112877
		protected string RelativeInstallPath
		{
			get
			{
				return "Datacenter\\DataMining\\OfficeDataLoader";
			}
		}

		// Token: 0x040029F7 RID: 10743
		protected const string ServiceShortName = "MSOfficeDataLoader";

		// Token: 0x040029F8 RID: 10744
		private const string ServiceBinaryName = "Microsoft.Office.BigData.DataLoader.exe";

		// Token: 0x040029F9 RID: 10745
		private const string EventLogName = "Application";

		// Token: 0x040029FA RID: 10746
		private const string EventLogSourceName = "MSExchange DataMining";
	}
}
