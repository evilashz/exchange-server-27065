using System;
using System.Configuration.Install;
using System.IO;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Tasks;
using Microsoft.Exchange.Security.WindowsFirewall;

namespace Microsoft.Exchange.Management.Edge.SetupTasks
{
	// Token: 0x02000309 RID: 777
	public abstract class ManageTransportLogSearchService : ManageService
	{
		// Token: 0x06001A5B RID: 6747 RVA: 0x00074ECC File Offset: 0x000730CC
		protected ManageTransportLogSearchService()
		{
			base.Account = ServiceAccount.LocalSystem;
			base.StartMode = ServiceStartMode.Automatic;
			base.DisplayName = Strings.TransportLogSearchServiceDisplayName;
			base.Description = Strings.TransportLogSearchServiceDescription;
			InstallContext installContext = new InstallContext();
			installContext.Parameters["logtoconsole"] = "false";
			installContext.Parameters["assemblypath"] = Path.Combine(ConfigurationContext.Setup.BinPath, "MSExchangeTransportLogSearch.exe");
			base.FirstFailureActionType = ServiceActionType.Restart;
			base.FirstFailureActionDelay = 5000U;
			base.SecondFailureActionType = ServiceActionType.Restart;
			base.SecondFailureActionDelay = 5000U;
			base.AllOtherFailuresActionType = ServiceActionType.Restart;
			base.AllOtherFailuresActionDelay = 5000U;
			base.ServiceInstallContext = installContext;
			base.ServicesDependedOn = null;
			base.AddFirewallRule(new MSExchangeTransportLogSearchFirewallRule());
			base.AddFirewallRule(new MSExchangeTransportLogSearchRPCEPMapperFirewallRule());
		}

		// Token: 0x170007A8 RID: 1960
		// (get) Token: 0x06001A5C RID: 6748 RVA: 0x00074FA1 File Offset: 0x000731A1
		protected override string Name
		{
			get
			{
				return "MSExchangeTransportLogSearch";
			}
		}

		// Token: 0x04000B7C RID: 2940
		private const string ServiceShortName = "MSExchangeTransportLogSearch";

		// Token: 0x04000B7D RID: 2941
		private const string ServiceBinaryName = "MSExchangeTransportLogSearch.exe";
	}
}
