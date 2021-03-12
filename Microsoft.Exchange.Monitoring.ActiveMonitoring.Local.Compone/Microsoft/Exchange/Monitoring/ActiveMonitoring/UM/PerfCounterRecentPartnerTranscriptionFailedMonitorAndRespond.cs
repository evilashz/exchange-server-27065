using System;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.UM
{
	// Token: 0x020004AE RID: 1198
	internal class PerfCounterRecentPartnerTranscriptionFailedMonitorAndResponder : IUMLocalMonitoringMonitorAndResponder
	{
		// Token: 0x06001DFA RID: 7674 RVA: 0x000B512C File Offset: 0x000B332C
		public void InitializeMonitorAndResponder(IMaintenanceWorkBroker broker, TracingContext traceContext)
		{
			UMPerfCounterUtils.InstantiatePerfCountersBasedUrgentAlerts(50, 2, "MSExchangeUMAvailability\\% of Partner Voice Message Transcription Failures Over the Last Hour", PerfCounterRecentPartnerTranscriptionFailedMonitorAndResponder.RecentPartnerTranscriptionFailedMonitorName, PerfCounterRecentPartnerTranscriptionFailedMonitorAndResponder.RecentPartnerTranscriptionFailedResponderName, ExchangeComponent.UMProtocol, PerfCounterRecentPartnerTranscriptionFailedMonitorAndResponder.RecentPartnerTranscriptionFailedEscalationMessageString, 0, broker, traceContext, NotificationServiceClass.Scheduled);
		}

		// Token: 0x0400150D RID: 5389
		private const int RecentPartnerTranscriptionFailedPercentThreshold = 50;

		// Token: 0x0400150E RID: 5390
		private const int RecentPartnerTranscriptionFailedNumberOfSamples = 2;

		// Token: 0x0400150F RID: 5391
		private const string RecentPartnerTranscriptionFailedPerfCounterName = "MSExchangeUMAvailability\\% of Partner Voice Message Transcription Failures Over the Last Hour";

		// Token: 0x04001510 RID: 5392
		private const int RecentPartnerTranscriptionFailedTransitionToUnhealthySecs = 0;

		// Token: 0x04001511 RID: 5393
		private static readonly string RecentPartnerTranscriptionFailedMonitorName = "UMServiceRecentPartnerTranscriptionFailedMonitor";

		// Token: 0x04001512 RID: 5394
		private static readonly string RecentPartnerTranscriptionFailedResponderName = "UMServiceRecentPartnerTranscriptionFailedEscalate";

		// Token: 0x04001513 RID: 5395
		private static readonly string RecentPartnerTranscriptionFailedEscalationMessageString = Strings.UMRecentPartnerTranscriptionFailedEscalationMessageString(50);
	}
}
