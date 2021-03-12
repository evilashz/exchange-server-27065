using System;
using System.Configuration.Install;
using System.IO;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000497 RID: 1175
	public abstract class ManageMessageTracingClientService : ManageService
	{
		// Token: 0x060029D6 RID: 10710 RVA: 0x000A63F8 File Offset: 0x000A45F8
		protected ManageMessageTracingClientService()
		{
			base.Account = ServiceAccount.NetworkService;
			base.StartMode = ServiceStartMode.Automatic;
			base.DisplayName = Strings.MessageTracingClientServiceDisplayName;
			base.Description = Strings.MessageTracingClientServiceDescription;
			InstallContext installContext = new InstallContext();
			installContext.Parameters["logtoconsole"] = "false";
			installContext.Parameters["assemblypath"] = Path.Combine(ConfigurationContext.Setup.BinPath, "MSMessageTracingClient.exe");
			base.FirstFailureActionType = ServiceActionType.Restart;
			base.FirstFailureActionDelay = 5000U;
			base.SecondFailureActionType = ServiceActionType.Restart;
			base.SecondFailureActionDelay = 5000U;
			base.AllOtherFailuresActionType = ServiceActionType.Restart;
			base.AllOtherFailuresActionDelay = 5000U;
			base.ServiceInstallContext = installContext;
			base.ServicesDependedOn = new string[]
			{
				ManagedServiceName.ActiveDirectoryTopologyService
			};
		}

		// Token: 0x17000C99 RID: 3225
		// (get) Token: 0x060029D7 RID: 10711 RVA: 0x000A64C6 File Offset: 0x000A46C6
		protected override string Name
		{
			get
			{
				return "MSMessageTracingClient";
			}
		}

		// Token: 0x04001E7B RID: 7803
		private const string ServiceShortName = "MSMessageTracingClient";

		// Token: 0x04001E7C RID: 7804
		private const string ServiceBinaryName = "MSMessageTracingClient.exe";
	}
}
