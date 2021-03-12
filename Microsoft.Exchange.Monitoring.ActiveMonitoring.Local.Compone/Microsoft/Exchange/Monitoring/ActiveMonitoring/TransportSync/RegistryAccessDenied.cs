using System;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.TransportSync
{
	// Token: 0x020004FA RID: 1274
	internal class RegistryAccessDenied : IWorkItem
	{
		// Token: 0x06001F7D RID: 8061 RVA: 0x000C01CA File Offset: 0x000BE3CA
		public void Initialize(MaintenanceDefinition discoveryDefinition, IMaintenanceWorkBroker broker, TracingContext traceContext)
		{
			this.InitializeMonitor(discoveryDefinition, broker, traceContext);
			this.InitializeResponder(discoveryDefinition, broker, traceContext);
		}

		// Token: 0x06001F7E RID: 8062 RVA: 0x000C01E0 File Offset: 0x000BE3E0
		private void InitializeMonitor(MaintenanceDefinition discoveryDefinition, IMaintenanceWorkBroker broker, TracingContext traceContext)
		{
			Configurations configurations = Configurations.CreateFromWorkDefinition(discoveryDefinition);
			MonitorDefinition monitorDefinition = OverallXFailuresMonitor.CreateDefinition("Registry.AccessDenied.Monitor", NotificationItem.GenerateResultName(ExchangeComponent.MailboxMigration.Name, TransportSyncNotificationEvent.RegistryAccessDenied.ToString(), null), ExchangeComponent.MailboxMigration.Name, ExchangeComponent.MailboxMigration, (int)RegistryAccessDenied.RegistryAccessDeniedMonitoringInterval.TotalSeconds, (int)RegistryAccessDenied.RegistryAccessDeniedRecurrenceInterval.TotalSeconds, 10, true);
			monitorDefinition.Enabled = configurations.RegistryAccessDeniedMonitorAndResponderEnabled;
			monitorDefinition.ServicePriority = 2;
			monitorDefinition.ScenarioDescription = "Validate TransportSync health is not impacted by Registry access issues";
			broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, traceContext);
		}

		// Token: 0x06001F7F RID: 8063 RVA: 0x000C0270 File Offset: 0x000BE470
		private void InitializeResponder(MaintenanceDefinition discoveryDefinition, IMaintenanceWorkBroker broker, TracingContext traceContext)
		{
			Configurations configurations = Configurations.CreateFromWorkDefinition(discoveryDefinition);
			ResponderDefinition definition = EscalateResponder.CreateDefinition("Registry.AccessDenied.Escalate", ExchangeComponent.MailboxMigration.Name, "Registry.AccessDenied.Monitor", string.Format("{0}/{1}", "Registry.AccessDenied.Monitor", ExchangeComponent.MailboxMigration.Name), Environment.MachineName, ServiceHealthStatus.None, ExchangeComponent.MailboxMigration.EscalationTeam, Strings.EscalationSubjectUnhealthy, Strings.RegistryAccessDeniedEscalationMessage, configurations.RegistryAccessDeniedMonitorAndResponderEnabled, NotificationServiceClass.UrgentInTraining, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
			broker.AddWorkDefinition<ResponderDefinition>(definition, traceContext);
		}

		// Token: 0x0400171A RID: 5914
		private const int RegistryAccessDeniedMonitorThreshold = 10;

		// Token: 0x0400171B RID: 5915
		private static readonly TimeSpan RegistryAccessDeniedMonitoringInterval = TimeSpan.FromSeconds(3600.0);

		// Token: 0x0400171C RID: 5916
		private static readonly TimeSpan RegistryAccessDeniedRecurrenceInterval = TimeSpan.FromSeconds(3600.0);
	}
}
