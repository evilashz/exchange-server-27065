using System;
using System.Threading;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.MailboxSpace.Responders
{
	// Token: 0x020001EF RID: 495
	public class DatabaseSizeEscalationNotificationResponder : EscalationNotificationResponder
	{
		// Token: 0x06000DA7 RID: 3495 RVA: 0x0005D8FD File Offset: 0x0005BAFD
		protected override void DoResponderWork(CancellationToken cancellationToken)
		{
			base.DoResponderWork(cancellationToken);
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x0005D908 File Offset: 0x0005BB08
		public override void PopulateCustomAttributes(EventNotificationItem notificationItem)
		{
			if (base.LastFailedMonitorResult != null)
			{
				notificationItem.StateAttribute2 = base.LastFailedMonitorResult.StateAttribute4;
				notificationItem.StateAttribute3 = base.LastFailedMonitorResult.StateAttribute7.ToString();
				notificationItem.StateAttribute4 = base.LastFailedMonitorResult.StateAttribute6.ToString();
				notificationItem.StateAttribute5 = base.LastFailedMonitorResult.StateAttribute8.ToString();
			}
		}
	}
}
