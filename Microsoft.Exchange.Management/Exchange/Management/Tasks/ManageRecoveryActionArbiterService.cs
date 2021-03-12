using System;
using System.Collections.Generic;
using System.Configuration.Install;
using System.IO;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020000D9 RID: 217
	public abstract class ManageRecoveryActionArbiterService : ManageService
	{
		// Token: 0x06000684 RID: 1668 RVA: 0x0001BB28 File Offset: 0x00019D28
		protected ManageRecoveryActionArbiterService()
		{
			base.Account = ServiceAccount.LocalSystem;
			base.StartMode = ServiceStartMode.Automatic;
			base.DisplayName = Strings.RecoveryActionArbiterServiceDisplayName;
			base.Description = Strings.RecoveryActionArbiterServiceDescription;
			InstallContext installContext = new InstallContext();
			installContext.Parameters["logtoconsole"] = "false";
			installContext.Parameters["assemblypath"] = Path.Combine(ConfigurationContext.Setup.BinPath, "Microsoft.Forefront.RecoveryActionArbiter.RaaService.exe");
			base.FirstFailureActionType = ServiceActionType.Restart;
			base.FirstFailureActionDelay = 5000U;
			base.SecondFailureActionType = ServiceActionType.Restart;
			base.SecondFailureActionDelay = 5000U;
			base.AllOtherFailuresActionType = ServiceActionType.Restart;
			base.AllOtherFailuresActionDelay = 5000U;
			base.ServiceInstallContext = installContext;
			base.CategoryCount = 1;
			base.EventMessageFile = Path.Combine(ConfigurationContext.Setup.BinPath, "Microsoft.Forefront.RecoveryActionArbiter.RaaServiceMsg.dll");
			List<string> list = new List<string>
			{
				"NetTcpPortSharing"
			};
			base.ServicesDependedOn = list.ToArray();
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000685 RID: 1669 RVA: 0x0001BC1B File Offset: 0x00019E1B
		protected override string Name
		{
			get
			{
				return "FfoRecoveryActionArbiter";
			}
		}

		// Token: 0x0400030E RID: 782
		protected const string ServiceShortName = "FfoRecoveryActionArbiter";

		// Token: 0x0400030F RID: 783
		private const string ServiceBinaryName = "Microsoft.Forefront.RecoveryActionArbiter.RaaService.exe";

		// Token: 0x04000310 RID: 784
		private const string EventLogBinaryName = "Microsoft.Forefront.RecoveryActionArbiter.RaaServiceMsg.dll";
	}
}
