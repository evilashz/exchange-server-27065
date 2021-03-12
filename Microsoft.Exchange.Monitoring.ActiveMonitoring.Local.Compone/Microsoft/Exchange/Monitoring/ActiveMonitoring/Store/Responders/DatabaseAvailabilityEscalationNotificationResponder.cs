using System;
using System.Threading;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Store.Responders
{
	// Token: 0x020004DE RID: 1246
	public class DatabaseAvailabilityEscalationNotificationResponder : EscalationNotificationResponder
	{
		// Token: 0x06001EE2 RID: 7906 RVA: 0x000BA397 File Offset: 0x000B8597
		protected override void DoResponderWork(CancellationToken cancellationToken)
		{
			base.DoResponderWork(cancellationToken);
		}

		// Token: 0x06001EE3 RID: 7907 RVA: 0x000BA3A0 File Offset: 0x000B85A0
		public override void PopulateCustomAttributes(EventNotificationItem notificationItem)
		{
			if (base.LastFailedProbeResult != null)
			{
				notificationItem.StateAttribute4 = base.LastFailedProbeResult.StateAttribute12;
			}
			base.PopulateCustomAttributes(notificationItem);
		}
	}
}
