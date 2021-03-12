using System;
using System.Configuration.Install;
using System.IO;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Edge.SetupTasks
{
	// Token: 0x02000303 RID: 771
	public abstract class ManageEdgeTransportService : ManageService
	{
		// Token: 0x06001A44 RID: 6724 RVA: 0x00074A7C File Offset: 0x00072C7C
		protected ManageEdgeTransportService()
		{
			base.Account = ServiceAccount.NetworkService;
			base.StartMode = ServiceStartMode.Automatic;
			base.DisplayName = Strings.EdgeTransportServiceDisplayName;
			base.Description = Strings.EdgeTransportServiceDescription;
			InstallContext installContext = new InstallContext();
			installContext.Parameters["logtoconsole"] = "false";
			installContext.Parameters["assemblypath"] = Path.Combine(ConfigurationContext.Setup.BinPath, "MSExchangeTransport.exe");
			base.FirstFailureActionType = ServiceActionType.Restart;
			base.FirstFailureActionDelay = 5000U;
			base.SecondFailureActionType = ServiceActionType.Restart;
			base.SecondFailureActionDelay = 5000U;
			base.AllOtherFailuresActionType = ServiceActionType.Restart;
			base.AllOtherFailuresActionDelay = 5000U;
			base.ServiceInstallContext = installContext;
			base.ServicesDependedOn = null;
		}

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x06001A45 RID: 6725 RVA: 0x00074B3B File Offset: 0x00072D3B
		protected override string Name
		{
			get
			{
				return "MSExchangeTransport";
			}
		}

		// Token: 0x04000B71 RID: 2929
		private const string ServiceShortName = "MSExchangeTransport";

		// Token: 0x04000B72 RID: 2930
		private const string ServiceBinaryName = "MSExchangeTransport.exe";

		// Token: 0x04000B73 RID: 2931
		private const string EventLogBinaryName = "Microsoft.Exchange.Common.ProcessManagerMsg.dll";
	}
}
