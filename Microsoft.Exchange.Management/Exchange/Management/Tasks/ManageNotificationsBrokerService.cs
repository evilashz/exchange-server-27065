using System;
using System.Collections.Generic;
using System.Configuration.Install;
using System.IO;
using System.ServiceProcess;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200060B RID: 1547
	public abstract class ManageNotificationsBrokerService : ManageService
	{
		// Token: 0x060036DA RID: 14042 RVA: 0x000E33B0 File Offset: 0x000E15B0
		public ManageNotificationsBrokerService()
		{
			base.Account = ServiceAccount.LocalSystem;
			base.StartMode = ServiceStartMode.Automatic;
			base.DisplayName = Strings.NotificationsBrokerServiceDisplayName;
			base.Description = Strings.NotificationsBrokerServiceDescription;
			InstallContext installContext = new InstallContext();
			installContext.Parameters["logtoconsole"] = "false";
			installContext.Parameters["assemblypath"] = Path.Combine(ConfigurationContext.Setup.BinPath, "Microsoft.Exchange.Notifications.Broker.exe");
			base.FirstFailureActionType = ServiceActionType.Restart;
			base.FirstFailureActionDelay = 5000U;
			base.SecondFailureActionType = ServiceActionType.Restart;
			base.SecondFailureActionDelay = 5000U;
			base.AllOtherFailuresActionType = ServiceActionType.Restart;
			base.AllOtherFailuresActionDelay = 5000U;
			base.FailureResetPeriod = 0U;
			base.ServiceInstallContext = installContext;
			base.ServicesDependedOn = new List<string>(base.ServicesDependedOn)
			{
				"NetTcpPortSharing"
			}.ToArray();
		}

		// Token: 0x1700104A RID: 4170
		// (get) Token: 0x060036DB RID: 14043 RVA: 0x000E3492 File Offset: 0x000E1692
		protected override string Name
		{
			get
			{
				return "MSExchangeNotificationsBroker";
			}
		}

		// Token: 0x0400256C RID: 9580
		private const string ServiceShortName = "MSExchangeNotificationsBroker";

		// Token: 0x0400256D RID: 9581
		private const string ServiceBinaryName = "Microsoft.Exchange.Notifications.Broker.exe";

		// Token: 0x0400256E RID: 9582
		private const string EventLogBinaryName = "Microsoft.Exchange.Notifications.Broker.EventLog.dll";
	}
}
