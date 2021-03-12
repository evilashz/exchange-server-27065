using System;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Exchange.UM.UMCommon;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.UM
{
	// Token: 0x020004AA RID: 1194
	internal class UMPipelineFullMonitorAndResponder : IUMLocalMonitoringMonitorAndResponder
	{
		// Token: 0x06001DEE RID: 7662 RVA: 0x000B4E90 File Offset: 0x000B3090
		public void InitializeMonitorAndResponder(IMaintenanceWorkBroker broker, TracingContext traceContext)
		{
			MonitorDefinition monitorDefinition = OverallXFailuresMonitor.CreateDefinition(UMPipelineFullMonitorAndResponder.UMPipelineFullMonitorName, NotificationItem.GenerateResultName(ExchangeComponent.UMProtocol.Name, UMNotificationEvent.UMPipelineFull.ToString(), null), UMMonitoringConstants.UMProtocolHealthSet, ExchangeComponent.UMProtocol, 60, 60, 1, true);
			monitorDefinition.TargetResource = string.Empty;
			monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Unhealthy, 3600)
			};
			broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, traceContext);
			ResponderDefinition definition = EscalateResponder.CreateDefinition("UMPipelineFullEscalate", UMMonitoringConstants.UMProtocolHealthSet, UMPipelineFullMonitorAndResponder.UMPipelineFullMonitorName, UMPipelineFullMonitorAndResponder.UMPipelineFullMonitorName, string.Empty, ServiceHealthStatus.Unhealthy, UMMonitoringConstants.UmEscalationTeam, Strings.EscalationSubjectUnhealthy, Strings.UMPipelineFullEscalationMessageString, true, NotificationServiceClass.Urgent, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
			broker.AddWorkDefinition<ResponderDefinition>(definition, traceContext);
		}

		// Token: 0x040014F4 RID: 5364
		private const int UMPipelineFullMonitorMonitoringIntervalSecs = 60;

		// Token: 0x040014F5 RID: 5365
		private const int UMPipelineFullMonitorRecurrenceIntervalSecs = 60;

		// Token: 0x040014F6 RID: 5366
		private const int UMPipelineFullMonitorNumberOfFailures = 1;

		// Token: 0x040014F7 RID: 5367
		private const int UMPipelineFullMonitorTransitionToDegradedSecs = 0;

		// Token: 0x040014F8 RID: 5368
		private const int UMPipelineFullMonitorTransitionToUnhealthySecs = 3600;

		// Token: 0x040014F9 RID: 5369
		private static readonly string UMPipelineFullMonitorName = "UMPipelineFullMonitor";
	}
}
