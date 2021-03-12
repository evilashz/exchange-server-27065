using System;
using System.Threading;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Store.Responders
{
	// Token: 0x020004E8 RID: 1256
	public class DatabaseSchemaVersionCheckEscalationNotificationResponder : EscalationNotificationResponder
	{
		// Token: 0x06001F25 RID: 7973 RVA: 0x000BEAC9 File Offset: 0x000BCCC9
		protected override void DoResponderWork(CancellationToken cancellationToken)
		{
			base.DoResponderWork(cancellationToken);
		}

		// Token: 0x06001F26 RID: 7974 RVA: 0x000BEAD4 File Offset: 0x000BCCD4
		public override void PopulateCustomAttributes(EventNotificationItem notificationItem)
		{
			if (base.LastFailedMonitorResult != null)
			{
				notificationItem.StateAttribute2 = base.LastFailedMonitorResult.StateAttribute6.ToString();
				notificationItem.StateAttribute3 = base.LastFailedMonitorResult.StateAttribute7.ToString();
				notificationItem.StateAttribute4 = base.LastFailedMonitorResult.StateAttribute5;
			}
		}
	}
}
