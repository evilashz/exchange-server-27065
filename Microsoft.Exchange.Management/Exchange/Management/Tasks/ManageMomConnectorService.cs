using System;
using System.Configuration.Install;
using System.IO;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020000D7 RID: 215
	public abstract class ManageMomConnectorService : ManageService
	{
		// Token: 0x0600067A RID: 1658 RVA: 0x0001B93C File Offset: 0x00019B3C
		protected ManageMomConnectorService()
		{
			base.Account = ServiceAccount.LocalSystem;
			base.StartMode = ServiceStartMode.Automatic;
			base.DisplayName = Strings.MomConnectorServiceDisplayName;
			base.Description = Strings.MomConnectorServiceDescription;
			InstallContext installContext = new InstallContext();
			installContext.Parameters["logtoconsole"] = "false";
			installContext.Parameters["assemblypath"] = Path.Combine(ConfigurationContext.Setup.BinPath, "Microsoft.Exchange.Management.CentralAdmin.MomConnector.exe");
			base.FirstFailureActionType = ServiceActionType.Restart;
			base.FirstFailureActionDelay = 5000U;
			base.SecondFailureActionType = ServiceActionType.Restart;
			base.SecondFailureActionDelay = 5000U;
			base.AllOtherFailuresActionType = ServiceActionType.Restart;
			base.AllOtherFailuresActionDelay = 5000U;
			base.ServiceInstallContext = installContext;
			base.CategoryCount = 1;
			base.EventMessageFile = Path.Combine(ConfigurationContext.Setup.BinPath, "Microsoft.Exchange.Management.CentralAdmin.MomConnectorMsg.dll");
			base.ServicesDependedOn = null;
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x0600067B RID: 1659 RVA: 0x0001BA17 File Offset: 0x00019C17
		protected override string Name
		{
			get
			{
				return "MSExchangeCAMOMConnector";
			}
		}

		// Token: 0x0400030B RID: 779
		protected const string ServiceShortName = "MSExchangeCAMOMConnector";

		// Token: 0x0400030C RID: 780
		private const string ServiceBinaryName = "Microsoft.Exchange.Management.CentralAdmin.MomConnector.exe";

		// Token: 0x0400030D RID: 781
		private const string EventLogBinaryName = "Microsoft.Exchange.Management.CentralAdmin.MomConnectorMsg.dll";
	}
}
