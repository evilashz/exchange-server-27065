using System;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.UM
{
	// Token: 0x020004AF RID: 1199
	internal class PerfCounterUMPipelineSLAMonitorAndResponder : IUMLocalMonitoringMonitorAndResponder
	{
		// Token: 0x06001DFD RID: 7677 RVA: 0x000B5190 File Offset: 0x000B3390
		public void InitializeMonitorAndResponder(IMaintenanceWorkBroker broker, TracingContext traceContext)
		{
			MonitorDefinition monitorDefinition = OverallConsecutiveSampleValueBelowThresholdMonitor.CreateDefinition(PerfCounterUMPipelineSLAMonitorAndResponder.UMPipelineSLAMonitorName, PerformanceCounterNotificationItem.GenerateResultName("MSExchangeUMAvailability\\% of Messages Successfully Processed Over the Last Hour"), UMMonitoringConstants.UMProtocolHealthSet, ExchangeComponent.UMProtocol, 50.0, 2, true);
			monitorDefinition.RecurrenceIntervalSeconds = 0;
			monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Unhealthy, 0)
			};
			monitorDefinition.ServicePriority = 1;
			monitorDefinition.ScenarioDescription = "Validate UM health is not impacted by SLA impacting issues";
			broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, traceContext);
			ResponderDefinition definition = EscalateResponder.CreateDefinition(PerfCounterUMPipelineSLAMonitorAndResponder.UMPipelineSLAResponderName, UMMonitoringConstants.UMProtocolHealthSet, PerfCounterUMPipelineSLAMonitorAndResponder.UMPipelineSLAMonitorName, PerfCounterUMPipelineSLAMonitorAndResponder.UMPipelineSLAMonitorName, string.Empty, ServiceHealthStatus.None, UMMonitoringConstants.UmEscalationTeam, Strings.EscalationSubjectUnhealthy, PerfCounterUMPipelineSLAMonitorAndResponder.UMPipelineSLAEscalationMessageString, true, NotificationServiceClass.Scheduled, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
			broker.AddWorkDefinition<ResponderDefinition>(definition, traceContext);
		}

		// Token: 0x04001514 RID: 5396
		private const int UMPipelineSLAPercentThreshold = 50;

		// Token: 0x04001515 RID: 5397
		private const int UMPipelineSLANumberOfSamples = 2;

		// Token: 0x04001516 RID: 5398
		private const string UMPipelineSLAPerfCounterName = "MSExchangeUMAvailability\\% of Messages Successfully Processed Over the Last Hour";

		// Token: 0x04001517 RID: 5399
		private const int UMPipelineSLATransitionToUnhealthySecs = 0;

		// Token: 0x04001518 RID: 5400
		private static readonly string UMPipelineSLAMonitorName = "UMPipelineSLAMonitor";

		// Token: 0x04001519 RID: 5401
		private static readonly string UMPipelineSLAResponderName = "UMPipelineSLAEscalate";

		// Token: 0x0400151A RID: 5402
		private static readonly string UMPipelineSLAEscalationMessageString = Strings.UMPipelineSLAEscalationMessageString(50);
	}
}
