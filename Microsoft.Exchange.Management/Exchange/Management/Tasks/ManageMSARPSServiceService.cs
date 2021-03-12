using System;
using System.Configuration.Install;
using System.IO;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000606 RID: 1542
	public abstract class ManageMSARPSServiceService : ManageService
	{
		// Token: 0x060036CF RID: 14031 RVA: 0x000E31DC File Offset: 0x000E13DC
		protected ManageMSARPSServiceService()
		{
			base.Account = ServiceAccount.LocalSystem;
			base.StartMode = ServiceStartMode.Automatic;
			base.DisplayName = Strings.MSARPSServiceDisplayName;
			base.Description = Strings.MSARPSServiceDescription;
			string path = Path.Combine(ConfigurationContext.Setup.InstallPath, this.RelativeInstallPath);
			InstallContext installContext = new InstallContext();
			installContext.Parameters["logtoconsole"] = "false";
			installContext.Parameters["assemblypath"] = Path.Combine(path, "Microsoft.Exchange.Security.MSARPSService.exe");
			base.FirstFailureActionType = ServiceActionType.Restart;
			base.FirstFailureActionDelay = 5000U;
			base.SecondFailureActionType = ServiceActionType.Restart;
			base.SecondFailureActionDelay = 5000U;
			base.AllOtherFailuresActionType = ServiceActionType.Restart;
			base.AllOtherFailuresActionDelay = 5000U;
			base.FailureResetPeriod = 0U;
			base.ServiceInstallContext = installContext;
			base.CategoryCount = 1;
		}

		// Token: 0x17001047 RID: 4167
		// (get) Token: 0x060036D0 RID: 14032 RVA: 0x000E32AF File Offset: 0x000E14AF
		protected override string Name
		{
			get
			{
				return "MSExchangeMSARPS";
			}
		}

		// Token: 0x17001048 RID: 4168
		// (get) Token: 0x060036D1 RID: 14033 RVA: 0x000E32B6 File Offset: 0x000E14B6
		protected string RelativeInstallPath
		{
			get
			{
				return "bin";
			}
		}

		// Token: 0x0400256A RID: 9578
		public bool ForceFailure;
	}
}
