using System;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.UM
{
	// Token: 0x020004A4 RID: 1188
	internal class MediaEdgeResourceAllocationFailedMonitorAndResponder : IUMLocalMonitoringMonitorAndResponder
	{
		// Token: 0x06001DDC RID: 7644 RVA: 0x000B4B4C File Offset: 0x000B2D4C
		public void InitializeMonitorAndResponder(IMaintenanceWorkBroker broker, TracingContext traceContext)
		{
			UMNotificationEventUtils.InstantiateUMNotificationEventBasedUrgentAlerts(MediaEdgeResourceAllocationFailedMonitorAndResponder.MediaEdgeResourceAllocationFailedMonitorName, MediaEdgeResourceAllocationFailedMonitorAndResponder.MediaEdgeResourceAllocationFailedResponderName, ExchangeComponent.UMProtocol, 3600, 0, 3, Strings.MediaEdgeResourceAllocationFailedEscalationMessage, 0, UMNotificationEvent.MediaEdgeResourceAllocation, broker, traceContext, NotificationServiceClass.Scheduled);
		}

		// Token: 0x040014D6 RID: 5334
		private const int MediaEdgeResourceAllocationFailedMonitorRecurrenceIntervalInSecs = 0;

		// Token: 0x040014D7 RID: 5335
		private const int MediaEdgeResourceAllocationFailedMonitorMonitoringIntervalInSecs = 3600;

		// Token: 0x040014D8 RID: 5336
		private const int MediaEdgeResourceAllocationFailedMonitorNumberOfFailures = 3;

		// Token: 0x040014D9 RID: 5337
		private const int MediaEdgeResourceAllocationFailedMonitorTransitionToUnhealthySecs = 0;

		// Token: 0x040014DA RID: 5338
		private static readonly string MediaEdgeResourceAllocationFailedMonitorName = "MediaEdgeResourceAllocationFailedMonitor";

		// Token: 0x040014DB RID: 5339
		private static readonly string MediaEdgeResourceAllocationFailedResponderName = "MediaEdgeResourceAllocationFailedEscalate";
	}
}
