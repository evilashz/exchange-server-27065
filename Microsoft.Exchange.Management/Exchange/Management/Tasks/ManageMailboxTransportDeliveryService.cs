using System;
using System.Configuration.Install;
using System.IO;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x02000475 RID: 1141
	public abstract class ManageMailboxTransportDeliveryService : ManageService
	{
		// Token: 0x0600283D RID: 10301 RVA: 0x0009E8F4 File Offset: 0x0009CAF4
		public ManageMailboxTransportDeliveryService()
		{
			base.Account = ServiceAccount.NetworkService;
			base.StartMode = ServiceStartMode.Automatic;
			base.DisplayName = Strings.MailboxTransportDeliveryServiceDisplayName;
			base.Description = Strings.MailboxTransportDeliveryServiceDescription;
			InstallContext installContext = new InstallContext();
			installContext.Parameters["logtoconsole"] = "false";
			installContext.Parameters["assemblypath"] = Path.Combine(ConfigurationContext.Setup.BinPath, "MSExchangeDelivery.exe");
			base.FirstFailureActionType = ServiceActionType.Restart;
			base.FirstFailureActionDelay = 5000U;
			base.SecondFailureActionType = ServiceActionType.Restart;
			base.SecondFailureActionDelay = 5000U;
			base.AllOtherFailuresActionType = ServiceActionType.Restart;
			base.AllOtherFailuresActionDelay = 5000U;
			base.FailureResetPeriod = 0U;
			base.ServiceInstallContext = installContext;
		}

		// Token: 0x17000C05 RID: 3077
		// (get) Token: 0x0600283E RID: 10302 RVA: 0x0009E9B3 File Offset: 0x0009CBB3
		protected override string Name
		{
			get
			{
				return "MSExchangeDelivery";
			}
		}

		// Token: 0x04001DE7 RID: 7655
		private const string ServiceShortName = "MSExchangeDelivery";

		// Token: 0x04001DE8 RID: 7656
		private const string ServiceBinaryName = "MSExchangeDelivery.exe";

		// Token: 0x04001DE9 RID: 7657
		private const int DeliveryServiceFirstFailureActionDelay = 5000;

		// Token: 0x04001DEA RID: 7658
		private const int DeliveryServiceSecondFailureActionDelay = 5000;

		// Token: 0x04001DEB RID: 7659
		private const int DeliveryServiceAllOtherFailuresActionDelay = 5000;

		// Token: 0x04001DEC RID: 7660
		private const int DeliveryServiceFailureResetPeriod = 0;
	}
}
