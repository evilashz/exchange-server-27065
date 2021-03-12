using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200060C RID: 1548
	[Cmdlet("Install", "NotificationsBrokerService")]
	[LocDescription(Strings.IDs.InstallNotificationsBrokerServiceTask)]
	public sealed class InstallNotificationsBrokerService : ManageNotificationsBrokerService
	{
		// Token: 0x060036DC RID: 14044 RVA: 0x000E3499 File Offset: 0x000E1699
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Install();
			TaskLogger.LogExit();
		}
	}
}
