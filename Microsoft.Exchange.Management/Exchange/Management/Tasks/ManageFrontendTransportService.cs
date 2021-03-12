using System;
using System.Configuration.Install;
using System.IO;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000CFF RID: 3327
	public abstract class ManageFrontendTransportService : ManageService
	{
		// Token: 0x06007FDE RID: 32734 RVA: 0x0020AFEC File Offset: 0x002091EC
		public ManageFrontendTransportService()
		{
			base.Account = ServiceAccount.LocalSystem;
			base.StartMode = ServiceStartMode.Automatic;
			base.DisplayName = Strings.FrontEndTransportServiceDisplayName;
			base.Description = Strings.FrontEndTransportServiceDescription;
			InstallContext installContext = new InstallContext();
			installContext.Parameters["logtoconsole"] = "false";
			installContext.Parameters["assemblypath"] = Path.Combine(ConfigurationContext.Setup.BinPath, "MSExchangeFrontendTransport.exe");
			base.FirstFailureActionType = ServiceActionType.Restart;
			base.FirstFailureActionDelay = 5000U;
			base.SecondFailureActionType = ServiceActionType.Restart;
			base.SecondFailureActionDelay = 5000U;
			base.AllOtherFailuresActionType = ServiceActionType.Restart;
			base.AllOtherFailuresActionDelay = 5000U;
			base.FailureResetPeriod = 0U;
			base.ServiceInstallContext = installContext;
		}

		// Token: 0x170027AB RID: 10155
		// (get) Token: 0x06007FDF RID: 32735 RVA: 0x0020B0AB File Offset: 0x002092AB
		protected override string Name
		{
			get
			{
				return "MSExchangeFrontendTransport";
			}
		}

		// Token: 0x04003EB9 RID: 16057
		private const string ServiceShortName = "MSExchangeFrontendTransport";

		// Token: 0x04003EBA RID: 16058
		private const string ServiceBinaryName = "MSExchangeFrontendTransport.exe";
	}
}
