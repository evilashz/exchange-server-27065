using System;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.TransportSync
{
	// Token: 0x020004F6 RID: 1270
	internal class SubscriptionSlaMissed : IWorkItem
	{
		// Token: 0x06001F69 RID: 8041 RVA: 0x000BFCA9 File Offset: 0x000BDEA9
		public void Initialize(MaintenanceDefinition discoveryDefinition, IMaintenanceWorkBroker broker, TracingContext traceContext)
		{
			this.InitializeMonitor(discoveryDefinition, broker, traceContext);
			this.InitializeResponder(discoveryDefinition, broker, traceContext);
		}

		// Token: 0x06001F6A RID: 8042 RVA: 0x000BFCC0 File Offset: 0x000BDEC0
		private void InitializeMonitor(MaintenanceDefinition discoveryDefinition, IMaintenanceWorkBroker broker, TracingContext traceContext)
		{
			Configurations configurations = Configurations.CreateFromWorkDefinition(discoveryDefinition);
			MonitorDefinition monitorDefinition = OverallConsecutiveSampleValueAboveThresholdMonitor.CreateDefinition("TransportSync.NotDispatchingWithin1HourSla.Monitor", PerformanceCounterNotificationItem.GenerateResultName(SubscriptionSlaMissed.SubscriptionSlaMissedPerfCounterName), ExchangeComponent.MailboxMigration.Name, ExchangeComponent.MailboxMigration, (double)configurations.SubscriptionSlaMissedPerfCounterThreshold, configurations.SubscriptionSlaMissedMonitorThreshold, true);
			monitorDefinition.Enabled = configurations.SubscriptionSlaMissedMonitorAndResponderEnabled;
			monitorDefinition.ServicePriority = 2;
			monitorDefinition.ScenarioDescription = "Validate TransportSync is not impacted by SLA missed issues";
			broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, traceContext);
		}

		// Token: 0x06001F6B RID: 8043 RVA: 0x000BFD30 File Offset: 0x000BDF30
		private void InitializeResponder(MaintenanceDefinition discoveryDefinition, IMaintenanceWorkBroker broker, TracingContext traceContext)
		{
			Configurations configurations = Configurations.CreateFromWorkDefinition(discoveryDefinition);
			ResponderDefinition definition = EscalateResponder.CreateDefinition("TransportSync.NotDispatchingWithin1HourSla.Escalate", ExchangeComponent.MailboxMigration.Name, "TransportSync.NotDispatchingWithin1HourSla.Monitor", "TransportSync.NotDispatchingWithin1HourSla.Monitor", string.Empty, ServiceHealthStatus.None, ExchangeComponent.MailboxMigration.EscalationTeam, Strings.EscalationSubjectUnhealthy, Strings.SubscriptionSlaMissedEscalationMessage, configurations.SubscriptionSlaMissedMonitorAndResponderEnabled, NotificationServiceClass.Scheduled, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
			broker.AddWorkDefinition<ResponderDefinition>(definition, traceContext);
		}

		// Token: 0x04001710 RID: 5904
		private static readonly string SubscriptionSlaMissedPerfCounterName = "MSExchange Transport Sync Manager By SLA\\95 percentile subscription polling frequency (seconds)";
	}
}
