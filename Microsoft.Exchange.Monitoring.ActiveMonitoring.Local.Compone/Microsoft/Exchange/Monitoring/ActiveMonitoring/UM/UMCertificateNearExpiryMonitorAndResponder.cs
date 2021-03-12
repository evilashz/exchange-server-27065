using System;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.UM
{
	// Token: 0x020004A7 RID: 1191
	internal class UMCertificateNearExpiryMonitorAndResponder : IUMLocalMonitoringMonitorAndResponder
	{
		// Token: 0x06001DE5 RID: 7653 RVA: 0x000B4C54 File Offset: 0x000B2E54
		public void InitializeMonitorAndResponder(IMaintenanceWorkBroker broker, TracingContext traceContext)
		{
			UMNotificationEventUtils.InstantiateUMNotificationEventBasedUrgentAlerts(UMCertificateNearExpiryMonitorAndResponder.UMCertificateNearExpiryMonitorName, UMCertificateNearExpiryMonitorAndResponder.UMCertificateNearExpiryResponderName, ExchangeComponent.UMProtocol, 3600, 0, 1, Strings.UMCertificateNearExpiryEscalationMessage, 0, UMNotificationEvent.CertificateNearExpiry, broker, traceContext, NotificationServiceClass.Scheduled);
		}

		// Token: 0x040014E8 RID: 5352
		private const int UMCertificateNearExpiryMonitorRecurrenceIntervalInSecs = 0;

		// Token: 0x040014E9 RID: 5353
		private const int UMCertificateNearExpiryMonitorMonitoringIntervalInSecs = 3600;

		// Token: 0x040014EA RID: 5354
		private const int UMCertificateNearExpiryNumberOfFailures = 1;

		// Token: 0x040014EB RID: 5355
		private const int UMCertificateNearExpiryMonitorTransitionToUnhealthySecs = 0;

		// Token: 0x040014EC RID: 5356
		private static readonly string UMCertificateNearExpiryMonitorName = "UMCertificateNearExpiryMonitor";

		// Token: 0x040014ED RID: 5357
		private static readonly string UMCertificateNearExpiryResponderName = "UMCertificateNearExpiryEscalate";
	}
}
