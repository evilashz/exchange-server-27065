using System;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.UM
{
	// Token: 0x020004A3 RID: 1187
	internal class MediaEdgeAuthenticationServiceCredentialsAcquisitionFailedMonitorAndResponder : IUMLocalMonitoringMonitorAndResponder
	{
		// Token: 0x06001DD9 RID: 7641 RVA: 0x000B4AF4 File Offset: 0x000B2CF4
		public void InitializeMonitorAndResponder(IMaintenanceWorkBroker broker, TracingContext traceContext)
		{
			UMNotificationEventUtils.InstantiateUMNotificationEventBasedUrgentAlerts(MediaEdgeAuthenticationServiceCredentialsAcquisitionFailedMonitorAndResponder.MediaEdgeAuthenticationServiceCredentialsAcquisitionFailedMonitorName, MediaEdgeAuthenticationServiceCredentialsAcquisitionFailedMonitorAndResponder.MediaEdgeAuthenticationServiceCredentialsAcquisitionFailedResponderName, ExchangeComponent.UMProtocol, 3600, 0, 3, Strings.MediaEdgeAuthenticationServiceCredentialsAcquisitionFailedEscalationMessage, 0, UMNotificationEvent.MediaEdgeAuthenticationServiceCredentialsAcquisition, broker, traceContext, NotificationServiceClass.Scheduled);
		}

		// Token: 0x040014D0 RID: 5328
		private const int MediaEdgeAuthenticationServiceCredentialsAcquisitionFailedMonitorRecurrenceIntervalInSecs = 0;

		// Token: 0x040014D1 RID: 5329
		private const int MediaEdgeAuthenticationServiceCredentialsAcquisitionFailedMonitorMonitoringIntervalInSecs = 3600;

		// Token: 0x040014D2 RID: 5330
		private const int MediaEdgeAuthenticationServiceCredentialsAcquisitionFailedMonitorNumberOfFailures = 3;

		// Token: 0x040014D3 RID: 5331
		private const int MediaEdgeAuthenticationServiceCredentialsAcquisitionFailedMonitorTransitionToUnhealthySecs = 0;

		// Token: 0x040014D4 RID: 5332
		private static readonly string MediaEdgeAuthenticationServiceCredentialsAcquisitionFailedMonitorName = "MediaEdgeAuthenticationServiceCredentialsAcquisitionFailedMonitor";

		// Token: 0x040014D5 RID: 5333
		private static readonly string MediaEdgeAuthenticationServiceCredentialsAcquisitionFailedResponderName = "MediaEdgeAuthenticationServiceCredentialsAcquisitionFailedEscalate";
	}
}
