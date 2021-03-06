using System;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.TransportSync
{
	// Token: 0x020004F5 RID: 1269
	internal class ServiceAvailability : IWorkItem
	{
		// Token: 0x06001F62 RID: 8034 RVA: 0x000BFA35 File Offset: 0x000BDC35
		public void Initialize(MaintenanceDefinition discoveryDefinition, IMaintenanceWorkBroker broker, TracingContext traceContext)
		{
			this.InitializeProbe(discoveryDefinition, broker, traceContext);
			this.InitializeMonitor(discoveryDefinition, broker, traceContext);
			this.InitializeRecoveryResponder(discoveryDefinition, broker, traceContext);
			this.InitializeEscalateResponder(discoveryDefinition, broker, traceContext);
		}

		// Token: 0x06001F63 RID: 8035 RVA: 0x000BFA5C File Offset: 0x000BDC5C
		private void InitializeProbe(MaintenanceDefinition discoveryDefinition, IMaintenanceWorkBroker broker, TracingContext traceContext)
		{
			Configurations configurations = Configurations.CreateFromWorkDefinition(discoveryDefinition);
			ProbeDefinition definition = WorkDefinitionHelper.CreateProbeDefinition("TransportSyncManager.Started.Probe", typeof(GenericServiceProbe), Configurations.TransportSyncManagerServiceName, ExchangeComponent.MailboxMigration.Name, configurations.ServiceAvailabilityRecurrenceInterval, configurations.ServiceAvailabilityEnabled);
			broker.AddWorkDefinition<ProbeDefinition>(definition, traceContext);
		}

		// Token: 0x06001F64 RID: 8036 RVA: 0x000BFAAC File Offset: 0x000BDCAC
		private void InitializeMonitor(MaintenanceDefinition discoveryDefinition, IMaintenanceWorkBroker broker, TracingContext traceContext)
		{
			Configurations configurations = Configurations.CreateFromWorkDefinition(discoveryDefinition);
			MonitorDefinition monitorDefinition = OverallXFailuresMonitor.CreateDefinition("TransportSyncManager.Started.Monitor", "TransportSyncManager.Started.Probe", ExchangeComponent.MailboxMigration.Name, ExchangeComponent.MailboxMigration, (int)ServiceAvailability.ServiceAvailabilityMonitoringInterval.TotalSeconds, (int)ServiceAvailability.ServiceAvailabilityMonitoringRecurrenceInterval.TotalSeconds, 10, true);
			monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Degraded, (int)ServiceAvailability.DegradedTransitionSpan.TotalSeconds),
				new MonitorStateTransition(ServiceHealthStatus.Unrecoverable, (int)ServiceAvailability.UnrecoverableTransitionSpan.TotalSeconds)
			};
			monitorDefinition.Enabled = configurations.ServiceAvailabilityEnabled;
			monitorDefinition.ServicePriority = 2;
			monitorDefinition.ScenarioDescription = "Validate TransportSync is not impacted by service availability issues";
			broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, traceContext);
		}

		// Token: 0x06001F65 RID: 8037 RVA: 0x000BFB68 File Offset: 0x000BDD68
		private void InitializeRecoveryResponder(MaintenanceDefinition discoveryDefinition, IMaintenanceWorkBroker broker, TracingContext traceContext)
		{
			Configurations configurations = Configurations.CreateFromWorkDefinition(discoveryDefinition);
			ResponderDefinition definition = WorkDefinitionHelper.CreateRestartResponderDefinition("TransportSyncManager.Service.Restart", typeof(RestartServiceResponder), Configurations.TransportSyncManagerServiceName, ExchangeComponent.MailboxMigration.Name, "TransportSyncManager.Started.Monitor", "TransportSyncManager.Started.Monitor", ServiceHealthStatus.Degraded, configurations.ServiceAvailabilityEnabled);
			broker.AddWorkDefinition<ResponderDefinition>(definition, traceContext);
		}

		// Token: 0x06001F66 RID: 8038 RVA: 0x000BFBBC File Offset: 0x000BDDBC
		private void InitializeEscalateResponder(MaintenanceDefinition discoveryDefinition, IMaintenanceWorkBroker broker, TracingContext traceContext)
		{
			Configurations configurations = Configurations.CreateFromWorkDefinition(discoveryDefinition);
			ResponderDefinition definition = EscalateResponder.CreateDefinition("TransportSyncManager.Service.Escalate", ExchangeComponent.MailboxMigration.Name, string.Format("{0}/{1}", "TransportSyncManager.Started.Monitor", ExchangeComponent.MailboxMigration.Name), "TransportSyncManager.Started.Monitor", Configurations.TransportSyncManagerServiceName, ServiceHealthStatus.Unrecoverable, ExchangeComponent.MailboxMigration.EscalationTeam, Strings.EscalationSubjectUnhealthy, Strings.TransportSyncManagerServiceNotRunningEscalationMessage(Configurations.TransportSyncManagerServiceName), configurations.ServiceAvailabilityEnabled, NotificationServiceClass.UrgentInTraining, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
			broker.AddWorkDefinition<ResponderDefinition>(definition, traceContext);
		}

		// Token: 0x0400170B RID: 5899
		private const int ServiceAvailabilityMonitorThreshold = 10;

		// Token: 0x0400170C RID: 5900
		private static readonly TimeSpan ServiceAvailabilityMonitoringInterval = TimeSpan.FromSeconds(3600.0);

		// Token: 0x0400170D RID: 5901
		private static readonly TimeSpan ServiceAvailabilityMonitoringRecurrenceInterval = TimeSpan.FromSeconds(3600.0);

		// Token: 0x0400170E RID: 5902
		private static readonly TimeSpan DegradedTransitionSpan = TimeSpan.FromMinutes(0.0);

		// Token: 0x0400170F RID: 5903
		private static readonly TimeSpan UnrecoverableTransitionSpan = TimeSpan.FromSeconds(3600.0);
	}
}
