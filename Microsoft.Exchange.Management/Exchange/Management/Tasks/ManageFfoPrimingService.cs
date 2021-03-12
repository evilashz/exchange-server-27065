using System;
using System.Configuration.Install;
using System.IO;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000384 RID: 900
	public abstract class ManageFfoPrimingService : ManageService
	{
		// Token: 0x06001F5D RID: 8029 RVA: 0x00087948 File Offset: 0x00085B48
		protected ManageFfoPrimingService()
		{
			base.Account = ServiceAccount.NetworkService;
			base.StartMode = ServiceStartMode.Automatic;
			base.DisplayName = Strings.FfoPrimingServiceDisplayName;
			base.Description = Strings.FfoPrimingServiceDescription;
			InstallContext installContext = new InstallContext();
			installContext.Parameters["logtoconsole"] = "false";
			installContext.Parameters["assemblypath"] = Path.Combine(ConfigurationContext.Setup.BinPath, "Microsoft.Exchange.Hygiene.Cache.PrimingService.exe");
			base.ServiceInstallContext = installContext;
			base.ServicesDependedOn = null;
			base.FirstFailureActionType = ServiceActionType.Restart;
			base.FirstFailureActionDelay = 5000U;
			base.SecondFailureActionType = ServiceActionType.Restart;
			base.SecondFailureActionDelay = 5000U;
			base.AllOtherFailuresActionType = ServiceActionType.Restart;
			base.AllOtherFailuresActionDelay = 5000U;
		}

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x06001F5E RID: 8030 RVA: 0x00087A07 File Offset: 0x00085C07
		protected override string Name
		{
			get
			{
				return "FfoPrimingService";
			}
		}

		// Token: 0x0400196E RID: 6510
		private const string ServiceShortName = "FfoPrimingService";

		// Token: 0x0400196F RID: 6511
		private const string ServiceBinaryName = "Microsoft.Exchange.Hygiene.Cache.PrimingService.exe";
	}
}
