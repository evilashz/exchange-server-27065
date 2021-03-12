using System;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.TransportSync
{
	// Token: 0x020004F7 RID: 1271
	internal class DeltaSyncEndpointUnreachable : IWorkItem
	{
		// Token: 0x06001F6E RID: 8046 RVA: 0x000BFDB6 File Offset: 0x000BDFB6
		public void Initialize(MaintenanceDefinition discoveryDefinition, IMaintenanceWorkBroker broker, TracingContext traceContext)
		{
			this.InitializeMonitor(discoveryDefinition, broker, traceContext);
			this.InitializeResponder(discoveryDefinition, broker, traceContext);
		}

		// Token: 0x06001F6F RID: 8047 RVA: 0x000BFDCC File Offset: 0x000BDFCC
		private void InitializeMonitor(MaintenanceDefinition discoveryDefinition, IMaintenanceWorkBroker broker, TracingContext traceContext)
		{
			Configurations configurations = Configurations.CreateFromWorkDefinition(discoveryDefinition);
			MonitorDefinition monitorDefinition = OverallXFailuresMonitor.CreateDefinition("DeltaSync.EndpointUnreachable.Monitor", NotificationItem.GenerateResultName(ExchangeComponent.MailboxMigration.Name, TransportSyncNotificationEvent.DeltaSyncEndpointUnreachable.ToString(), null), ExchangeComponent.MailboxMigration.Name, ExchangeComponent.MailboxMigration, (int)DeltaSyncEndpointUnreachable.DeltaSyncEndpointUnreachableMonitoringInterval.TotalSeconds, (int)DeltaSyncEndpointUnreachable.DeltaSyncEndpointUnreachableRecurrenceInterval.TotalSeconds, 10, true);
			monitorDefinition.Enabled = configurations.DeltaSyncEndpointUnreachableMonitorAndResponderEnabled;
			monitorDefinition.ServicePriority = 2;
			monitorDefinition.ScenarioDescription = "Validate TransportSync health is not impacted by DeltaSync endpoint issues";
			broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, traceContext);
		}

		// Token: 0x06001F70 RID: 8048 RVA: 0x000BFE5C File Offset: 0x000BE05C
		private void InitializeResponder(MaintenanceDefinition discoveryDefinition, IMaintenanceWorkBroker broker, TracingContext traceContext)
		{
			Configurations configurations = Configurations.CreateFromWorkDefinition(discoveryDefinition);
			ResponderDefinition definition = EscalateResponder.CreateDefinition("DeltaSync.EndpointUnreachable.Escalate", ExchangeComponent.MailboxMigration.Name, "DeltaSync.EndpointUnreachable.Monitor", string.Format("{0}/{1}", "DeltaSync.EndpointUnreachable.Monitor", ExchangeComponent.MailboxMigration.Name), Environment.MachineName, ServiceHealthStatus.None, ExchangeComponent.MailboxMigration.EscalationTeam, Strings.EscalationSubjectUnhealthy, Strings.DeltaSyncEndpointUnreachableEscalationMessage, configurations.DeltaSyncEndpointUnreachableMonitorAndResponderEnabled, NotificationServiceClass.UrgentInTraining, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
			broker.AddWorkDefinition<ResponderDefinition>(definition, traceContext);
		}

		// Token: 0x04001711 RID: 5905
		private const int DeltaSyncEndpointUnreachableMonitorThreshold = 10;

		// Token: 0x04001712 RID: 5906
		private static readonly TimeSpan DeltaSyncEndpointUnreachableMonitoringInterval = TimeSpan.FromSeconds(3600.0);

		// Token: 0x04001713 RID: 5907
		private static readonly TimeSpan DeltaSyncEndpointUnreachableRecurrenceInterval = TimeSpan.FromSeconds(3600.0);
	}
}
