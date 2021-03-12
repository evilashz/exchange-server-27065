using System;
using System.Configuration.Install;
using System.IO;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Edge.SetupTasks
{
	// Token: 0x02000307 RID: 775
	public abstract class ManageAntispamUpdateService : ManageService
	{
		// Token: 0x06001A52 RID: 6738 RVA: 0x00074D38 File Offset: 0x00072F38
		protected ManageAntispamUpdateService()
		{
			base.Account = ServiceAccount.LocalSystem;
			base.StartMode = ServiceStartMode.Automatic;
			base.DisplayName = Strings.AntispamUpdateServiceDisplayName;
			base.Description = Strings.AntispamUpdateServiceDescription;
			InstallContext installContext = new InstallContext();
			installContext.Parameters["logtoconsole"] = "false";
			installContext.Parameters["assemblypath"] = Path.Combine(ConfigurationContext.Setup.BinPath, "Microsoft.Exchange.AntispamUpdateSvc.exe");
			base.FirstFailureActionType = ServiceActionType.Restart;
			base.FirstFailureActionDelay = 5000U;
			base.SecondFailureActionType = ServiceActionType.Restart;
			base.SecondFailureActionDelay = 5000U;
			base.AllOtherFailuresActionType = ServiceActionType.Restart;
			base.AllOtherFailuresActionDelay = 5000U;
			base.FailureResetPeriod = 0U;
			base.ServiceInstallContext = installContext;
		}

		// Token: 0x170007A5 RID: 1957
		// (get) Token: 0x06001A53 RID: 6739 RVA: 0x00074DF7 File Offset: 0x00072FF7
		protected override string Name
		{
			get
			{
				return "MSExchangeAntispamUpdate";
			}
		}

		// Token: 0x04000B79 RID: 2937
		private const string ServiceShortName = "MSExchangeAntispamUpdate";

		// Token: 0x04000B7A RID: 2938
		private const string ServiceBinaryName = "Microsoft.Exchange.AntispamUpdateSvc.exe";
	}
}
