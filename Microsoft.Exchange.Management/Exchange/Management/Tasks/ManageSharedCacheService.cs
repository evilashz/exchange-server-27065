using System;
using System.Configuration.Install;
using System.IO;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000771 RID: 1905
	public abstract class ManageSharedCacheService : ManageService
	{
		// Token: 0x06004361 RID: 17249 RVA: 0x0011485C File Offset: 0x00112A5C
		protected ManageSharedCacheService()
		{
			base.Account = ServiceAccount.LocalSystem;
			base.StartMode = ServiceStartMode.Automatic;
			base.DisplayName = Strings.SharedCacheServiceDisplayName;
			base.Description = Strings.SharedCacheServiceDescription;
			string path = Path.Combine(ConfigurationContext.Setup.InstallPath, this.RelativeInstallPath);
			InstallContext installContext = new InstallContext();
			installContext.Parameters["logtoconsole"] = "false";
			installContext.Parameters["assemblypath"] = Path.Combine(path, "Microsoft.Exchange.SharedCache.exe");
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

		// Token: 0x1700147F RID: 5247
		// (get) Token: 0x06004362 RID: 17250 RVA: 0x0011492F File Offset: 0x00112B2F
		protected override string Name
		{
			get
			{
				return "MSExchangeSharedCache";
			}
		}

		// Token: 0x17001480 RID: 5248
		// (get) Token: 0x06004363 RID: 17251 RVA: 0x00114936 File Offset: 0x00112B36
		protected string RelativeInstallPath
		{
			get
			{
				return "bin";
			}
		}

		// Token: 0x04002A01 RID: 10753
		public bool ForceFailure;
	}
}
