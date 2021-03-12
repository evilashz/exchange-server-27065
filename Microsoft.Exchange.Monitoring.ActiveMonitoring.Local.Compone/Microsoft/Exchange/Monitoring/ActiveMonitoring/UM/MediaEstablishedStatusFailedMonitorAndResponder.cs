using System;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.UM
{
	// Token: 0x020004A5 RID: 1189
	internal class MediaEstablishedStatusFailedMonitorAndResponder : IUMLocalMonitoringMonitorAndResponder
	{
		// Token: 0x06001DDF RID: 7647 RVA: 0x000B4BA4 File Offset: 0x000B2DA4
		public void InitializeMonitorAndResponder(IMaintenanceWorkBroker broker, TracingContext traceContext)
		{
			UMNotificationEventUtils.InstantiateUMNotificationEventBasedUrgentAlerts(MediaEstablishedStatusFailedMonitorAndResponder.MediaEstablishedStatusFailedMonitorName, MediaEstablishedStatusFailedMonitorAndResponder.MediaEstablishedStatusFailedResponderName, ExchangeComponent.UMProtocol, 3600, 0, 3, Strings.MediaEstablishedFailedEscalationMessage, 0, UMNotificationEvent.MediaEstablishedStatus, broker, traceContext, NotificationServiceClass.Scheduled);
		}

		// Token: 0x040014DC RID: 5340
		private const int MediaEstablishedStatusFailedMonitorRecurrenceIntervalInSecs = 0;

		// Token: 0x040014DD RID: 5341
		private const int MediaEstablishedStatusFailedMonitorMonitoringIntervalInSecs = 3600;

		// Token: 0x040014DE RID: 5342
		private const int MediaEstablishedStatusFailedMonitorNumberOfFailures = 3;

		// Token: 0x040014DF RID: 5343
		private const int MediaEstablishedStatusFailedMonitorTransitionToUnhealthySecs = 0;

		// Token: 0x040014E0 RID: 5344
		private static readonly string MediaEstablishedStatusFailedMonitorName = "MediaEstablishedFailedMonitor";

		// Token: 0x040014E1 RID: 5345
		private static readonly string MediaEstablishedStatusFailedResponderName = "MediaEstablishedFailedEscalate";
	}
}
