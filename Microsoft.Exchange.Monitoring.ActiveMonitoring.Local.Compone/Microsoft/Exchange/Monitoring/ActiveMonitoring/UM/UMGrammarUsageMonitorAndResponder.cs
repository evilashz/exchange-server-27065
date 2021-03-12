using System;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.UM
{
	// Token: 0x020004A8 RID: 1192
	internal class UMGrammarUsageMonitorAndResponder : IUMLocalMonitoringMonitorAndResponder
	{
		// Token: 0x06001DE8 RID: 7656 RVA: 0x000B4CAC File Offset: 0x000B2EAC
		public void InitializeMonitorAndResponder(IMaintenanceWorkBroker broker, TracingContext traceContext)
		{
			UMNotificationEventUtils.InitializeMonitorAndResponderBasedOnOverallPercentSuccessMonitor(UMGrammarUsageMonitorAndResponder.UMGrammarUsageMonitorName, UMGrammarUsageMonitorAndResponder.UMGrammarUsageResponderName, ExchangeComponent.UMProtocol, 86400, 86400, 50, Strings.UMGrammarUsageEscalationMessage(50), 86400, UMMonitoringConstants.UMProtocolHealthSet, UMNotificationEvent.UMGrammarUsage, broker, traceContext, NotificationServiceClass.Scheduled);
		}

		// Token: 0x040014EE RID: 5358
		private const int UMGrammarUsageMonitorRecurrenceIntervalInSecs = 0;

		// Token: 0x040014EF RID: 5359
		private const int UMGrammarUsageMonitorMonitoringIntervalInSecs = 86400;

		// Token: 0x040014F0 RID: 5360
		private const int UMGrammarUsageMonitorMonitoringThreshold = 50;

		// Token: 0x040014F1 RID: 5361
		private const int UMGrammarUsageMonitorTransitionToUnhealthySecs = 86400;

		// Token: 0x040014F2 RID: 5362
		private static readonly string UMGrammarUsageMonitorName = "UMGrammarUsageMonitor";

		// Token: 0x040014F3 RID: 5363
		private static readonly string UMGrammarUsageResponderName = "UMGrammarUsageEscalate";
	}
}
