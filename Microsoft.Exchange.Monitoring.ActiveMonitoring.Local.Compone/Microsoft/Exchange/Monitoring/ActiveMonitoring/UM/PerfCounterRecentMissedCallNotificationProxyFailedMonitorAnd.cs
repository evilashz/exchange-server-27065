using System;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.UM
{
	// Token: 0x020004AD RID: 1197
	internal class PerfCounterRecentMissedCallNotificationProxyFailedMonitorAndResponder : IUMLocalMonitoringMonitorAndResponder
	{
		// Token: 0x06001DF7 RID: 7671 RVA: 0x000B50C8 File Offset: 0x000B32C8
		public void InitializeMonitorAndResponder(IMaintenanceWorkBroker broker, TracingContext traceContext)
		{
			UMPerfCounterUtils.InstantiatePerfCountersBasedUrgentAlerts(50, 2, "MSExchangeUMCallRouterAvailability\\% of Missed Call Notification Proxy Failed at UM Call Router Over the Last Hour", PerfCounterRecentMissedCallNotificationProxyFailedMonitorAndResponder.RecentUMCallRouterMissedCallNotificationProxyFailedMonitorName, PerfCounterRecentMissedCallNotificationProxyFailedMonitorAndResponder.RecentUMCallRouterMissedCallNotificationProxyFailedResponderName, ExchangeComponent.UMCallRouter, PerfCounterRecentMissedCallNotificationProxyFailedMonitorAndResponder.RecentUMCallRouterMissedCallNotificationProxyFailedEscalationMessageString, 0, broker, traceContext, NotificationServiceClass.Scheduled);
		}

		// Token: 0x04001506 RID: 5382
		private const int RecentUMCallRouterMissedCallNotificationProxyFailedPercentThreshold = 50;

		// Token: 0x04001507 RID: 5383
		private const int RecentUMCallRouterMissedCallNotificationProxyFailedNumberOfSamples = 2;

		// Token: 0x04001508 RID: 5384
		private const string RecentUMCallRouterMissedCallNotificationProxyFailedPerfCounterName = "MSExchangeUMCallRouterAvailability\\% of Missed Call Notification Proxy Failed at UM Call Router Over the Last Hour";

		// Token: 0x04001509 RID: 5385
		private const int RecentUMCallRouterMissedCallNotificationProxyFailedTransitionToUnhealthySecs = 0;

		// Token: 0x0400150A RID: 5386
		private static readonly string RecentUMCallRouterMissedCallNotificationProxyFailedMonitorName = "UMCallRouterRecentMissedCallNotificationProxyFailedMonitor";

		// Token: 0x0400150B RID: 5387
		private static readonly string RecentUMCallRouterMissedCallNotificationProxyFailedResponderName = "UMCallRouterRecentMissedCallNotificationProxyFailedEscalate";

		// Token: 0x0400150C RID: 5388
		private static readonly string RecentUMCallRouterMissedCallNotificationProxyFailedEscalationMessageString = Strings.UMCallRouterRecentMissedCallNotificationProxyFailedEscalationMessageString(50);
	}
}
