using System;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.UM
{
	// Token: 0x020004A6 RID: 1190
	internal class UMCallRouterCertificateNearExpiryMonitorAndResponder : IUMLocalMonitoringMonitorAndResponder
	{
		// Token: 0x06001DE2 RID: 7650 RVA: 0x000B4BFC File Offset: 0x000B2DFC
		public void InitializeMonitorAndResponder(IMaintenanceWorkBroker broker, TracingContext traceContext)
		{
			UMNotificationEventUtils.InstantiateUMNotificationEventBasedUrgentAlerts(UMCallRouterCertificateNearExpiryMonitorAndResponder.UMCallRouterCertificateNearExpiryMonitorName, UMCallRouterCertificateNearExpiryMonitorAndResponder.UMCallRouterCertificateNearExpiryResponderName, ExchangeComponent.UMCallRouter, 3600, 0, 1, Strings.UMCallRouterCertificateNearExpiryEscalationMessage, 0, UMNotificationEvent.CallRouterCertificateNearExpiry, broker, traceContext, NotificationServiceClass.Scheduled);
		}

		// Token: 0x040014E2 RID: 5346
		private const int UMCallRouterCertificateNearExpiryMonitorRecurrenceIntervalInSecs = 0;

		// Token: 0x040014E3 RID: 5347
		private const int UMCallRouterCertificateNearExpiryMonitorMonitoringIntervalInSecs = 3600;

		// Token: 0x040014E4 RID: 5348
		private const int UMCallRouterCertificateNearExpiryNumberOfFailures = 1;

		// Token: 0x040014E5 RID: 5349
		private const int UMCallRouterCertificateNearExpiryMonitorTransitionToUnhealthySecs = 0;

		// Token: 0x040014E6 RID: 5350
		private static readonly string UMCallRouterCertificateNearExpiryMonitorName = "UMCallRouterCertificateNearExpiryMonitor";

		// Token: 0x040014E7 RID: 5351
		private static readonly string UMCallRouterCertificateNearExpiryResponderName = "UMCallRouterCertificateNearExpiryEscalate";
	}
}
