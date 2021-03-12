using System;
using System.Configuration.Install;
using System.IO;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x020007D1 RID: 2001
	public abstract class ManageExchangeStreamingOptics : ManageService
	{
		// Token: 0x0600461C RID: 17948 RVA: 0x0012004C File Offset: 0x0011E24C
		protected ManageExchangeStreamingOptics()
		{
			base.Account = ServiceAccount.NetworkService;
			base.StartMode = ServiceStartMode.Automatic;
			base.DisplayName = Strings.ExchangeStreamingOpticsDisplayName;
			base.Description = Strings.ExchangeStreamingOpticsDescription;
			InstallContext installContext = new InstallContext();
			installContext.Parameters["logtoconsole"] = "false";
			installContext.Parameters["assemblypath"] = Path.Combine(ConfigurationContext.Setup.BinPath, "MSExchangeStreamingOptics.exe");
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

		// Token: 0x1700152E RID: 5422
		// (get) Token: 0x0600461D RID: 17949 RVA: 0x0012011A File Offset: 0x0011E31A
		protected override string Name
		{
			get
			{
				return "MSExchangeStreamingOptics";
			}
		}

		// Token: 0x04002AFC RID: 11004
		private const string ServiceShortName = "MSExchangeStreamingOptics";

		// Token: 0x04002AFD RID: 11005
		private const string ServiceBinaryName = "MSExchangeStreamingOptics.exe";
	}
}
