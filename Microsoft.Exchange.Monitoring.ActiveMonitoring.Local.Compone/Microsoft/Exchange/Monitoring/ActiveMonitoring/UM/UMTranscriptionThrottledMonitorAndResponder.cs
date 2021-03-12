using System;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.UM
{
	// Token: 0x020004AC RID: 1196
	internal class UMTranscriptionThrottledMonitorAndResponder : IUMLocalMonitoringMonitorAndResponder
	{
		// Token: 0x06001DF4 RID: 7668 RVA: 0x000B5064 File Offset: 0x000B3264
		public void InitializeMonitorAndResponder(IMaintenanceWorkBroker broker, TracingContext traceContext)
		{
			UMNotificationEventUtils.InitializeMonitorAndResponderBasedOnOverallPercentSuccessMonitor(UMTranscriptionThrottledMonitorAndResponder.UMTranscriptionThrottledMonitorName, UMTranscriptionThrottledMonitorAndResponder.UMTranscriptionThrottledResponderName, ExchangeComponent.UMProtocol, 86400, 0, 50, Strings.UMTranscriptionThrottledEscalationMessage(50), 86400, UMMonitoringConstants.UMProtocolHealthSet, UMNotificationEvent.UMTranscriptionThrottling, broker, traceContext, NotificationServiceClass.Scheduled);
		}

		// Token: 0x04001500 RID: 5376
		private const int UMTranscriptionThrottledMonitorRecurrenceIntervalInSecs = 0;

		// Token: 0x04001501 RID: 5377
		private const int UMTranscriptionThrottledMonitorMonitoringIntervalInSecs = 86400;

		// Token: 0x04001502 RID: 5378
		private const int UMTranscriptionThrottledMonitorMonitoringThreshold = 50;

		// Token: 0x04001503 RID: 5379
		private const int UMTranscriptionThrottledMonitorTransitionToUnhealthySecs = 86400;

		// Token: 0x04001504 RID: 5380
		private static readonly string UMTranscriptionThrottledMonitorName = "UMTranscriptionThrottledMonitor";

		// Token: 0x04001505 RID: 5381
		private static readonly string UMTranscriptionThrottledResponderName = "UMTranscriptionThrottledEscalate";
	}
}
