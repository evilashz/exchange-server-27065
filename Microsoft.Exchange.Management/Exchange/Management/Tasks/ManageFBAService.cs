using System;
using System.Configuration.Install;
using System.IO;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020000CC RID: 204
	public abstract class ManageFBAService : ManageService
	{
		// Token: 0x06000636 RID: 1590 RVA: 0x0001AB28 File Offset: 0x00018D28
		protected ManageFBAService()
		{
			base.Account = ServiceAccount.LocalSystem;
			base.StartMode = ServiceStartMode.Automatic;
			base.DisplayName = Strings.FBAServiceDisplayName;
			base.Description = Strings.FBAServiceDescription;
			InstallContext installContext = new InstallContext();
			installContext.Parameters["logtoconsole"] = "false";
			installContext.Parameters["assemblypath"] = Path.Combine(ConfigurationContext.Setup.BinPath, "ExFBA.exe");
			base.FirstFailureActionType = ServiceActionType.Restart;
			base.FirstFailureActionDelay = 5000U;
			base.SecondFailureActionType = ServiceActionType.Restart;
			base.SecondFailureActionDelay = 5000U;
			base.AllOtherFailuresActionType = ServiceActionType.Restart;
			base.AllOtherFailuresActionDelay = 5000U;
			base.ServiceInstallContext = installContext;
			base.ServicesDependedOn = null;
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000637 RID: 1591 RVA: 0x0001ABE7 File Offset: 0x00018DE7
		protected override string Name
		{
			get
			{
				return "MSExchangeFBA";
			}
		}

		// Token: 0x04000303 RID: 771
		protected const string ServiceShortName = "MSExchangeFBA";

		// Token: 0x04000304 RID: 772
		private const string ServiceBinaryName = "ExFBA.exe";

		// Token: 0x04000305 RID: 773
		private const string EventLogBinaryName = "";
	}
}
