using System;
using System.Configuration.Install;
using System.IO;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000147 RID: 327
	public abstract class ManageComplianceService : ManageService
	{
		// Token: 0x06000BCB RID: 3019 RVA: 0x00036EC0 File Offset: 0x000350C0
		public ManageComplianceService()
		{
			base.Account = ServiceAccount.NetworkService;
			base.StartMode = ServiceStartMode.Automatic;
			base.DisplayName = Strings.ComplianceServiceDisplayName;
			base.Description = Strings.ComplianceServiceDescription;
			InstallContext installContext = new InstallContext();
			installContext.Parameters["logtoconsole"] = "false";
			installContext.Parameters["assemblypath"] = Path.Combine(ConfigurationContext.Setup.BinPath, "MSExchangeCompliance.exe");
			base.FirstFailureActionType = ServiceActionType.Restart;
			base.FirstFailureActionDelay = 5000U;
			base.SecondFailureActionType = ServiceActionType.Restart;
			base.SecondFailureActionDelay = 5000U;
			base.AllOtherFailuresActionType = ServiceActionType.Restart;
			base.AllOtherFailuresActionDelay = 5000U;
			base.FailureResetPeriod = 0U;
			base.ServiceInstallContext = installContext;
		}

		// Token: 0x1700033E RID: 830
		// (get) Token: 0x06000BCC RID: 3020 RVA: 0x00036F7F File Offset: 0x0003517F
		protected override string Name
		{
			get
			{
				return "MSExchangeCompliance";
			}
		}

		// Token: 0x040005B0 RID: 1456
		private const string ServiceShortName = "MSExchangeCompliance";

		// Token: 0x040005B1 RID: 1457
		private const string ServiceBinaryName = "MSExchangeCompliance.exe";
	}
}
