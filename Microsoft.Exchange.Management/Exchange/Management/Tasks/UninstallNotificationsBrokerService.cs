using System;
using System.Management.Automation;
using Microsoft.Exchange.Configuration.Tasks;

namespace Microsoft.Exchange.Management.Tasks
{
	// Token: 0x0200060D RID: 1549
	[Cmdlet("Uninstall", "NotificationsBrokerService")]
	[LocDescription(Strings.IDs.UninstallNotificationsBrokerServiceTask)]
	public sealed class UninstallNotificationsBrokerService : ManageNotificationsBrokerService
	{
		// Token: 0x060036DE RID: 14046 RVA: 0x000E34B3 File Offset: 0x000E16B3
		protected override void InternalProcessRecord()
		{
			TaskLogger.LogEnter();
			base.Uninstall();
			TaskLogger.LogExit();
		}
	}
}
