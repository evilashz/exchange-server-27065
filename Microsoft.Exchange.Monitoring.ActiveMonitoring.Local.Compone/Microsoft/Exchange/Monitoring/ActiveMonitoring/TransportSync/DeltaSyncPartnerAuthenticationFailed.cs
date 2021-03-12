using System;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.TransportSync
{
	// Token: 0x020004F8 RID: 1272
	internal class DeltaSyncPartnerAuthenticationFailed : IWorkItem
	{
		// Token: 0x06001F73 RID: 8051 RVA: 0x000BFF12 File Offset: 0x000BE112
		public void Initialize(MaintenanceDefinition discoveryDefinition, IMaintenanceWorkBroker broker, TracingContext traceContext)
		{
			this.InitializeMonitor(discoveryDefinition, broker, traceContext);
			this.InitializeResponder(discoveryDefinition, broker, traceContext);
		}

		// Token: 0x06001F74 RID: 8052 RVA: 0x000BFF28 File Offset: 0x000BE128
		private void InitializeMonitor(MaintenanceDefinition discoveryDefinition, IMaintenanceWorkBroker broker, TracingContext traceContext)
		{
			Configurations configurations = Configurations.CreateFromWorkDefinition(discoveryDefinition);
			MonitorDefinition monitorDefinition = OverallXFailuresMonitor.CreateDefinition("DeltaSync.PartnerAuthentication.Failed.Monitor", NotificationItem.GenerateResultName(ExchangeComponent.MailboxMigration.Name, TransportSyncNotificationEvent.DeltaSyncPartnerAuthenticationFailed.ToString(), null), ExchangeComponent.MailboxMigration.Name, ExchangeComponent.MailboxMigration, (int)DeltaSyncPartnerAuthenticationFailed.DeltaSyncPartnerAuthenticationFailedMonitoringInterval.TotalSeconds, (int)DeltaSyncPartnerAuthenticationFailed.DeltaSyncPartnerAuthenticationFailedRecurrenceInterval.TotalSeconds, 10, true);
			monitorDefinition.Enabled = configurations.DeltaSyncPartnerAuthenticationFailedMonitorAndResponderEnabled;
			monitorDefinition.ServicePriority = 2;
			monitorDefinition.ScenarioDescription = "Validate TransportSync is not impacted by DeltaSync partner authentication issues";
			broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, traceContext);
		}

		// Token: 0x06001F75 RID: 8053 RVA: 0x000BFFB8 File Offset: 0x000BE1B8
		private void InitializeResponder(MaintenanceDefinition discoveryDefinition, IMaintenanceWorkBroker broker, TracingContext traceContext)
		{
			Configurations configurations = Configurations.CreateFromWorkDefinition(discoveryDefinition);
			ResponderDefinition definition = EscalateResponder.CreateDefinition("DeltaSync.PartnerAuthentication.Failed.Escalate", ExchangeComponent.MailboxMigration.Name, "DeltaSync.PartnerAuthentication.Failed.Monitor", string.Format("{0}/{1}", "DeltaSync.PartnerAuthentication.Failed.Monitor", ExchangeComponent.MailboxMigration.Name), Environment.MachineName, ServiceHealthStatus.None, ExchangeComponent.MailboxMigration.EscalationTeam, Strings.EscalationSubjectUnhealthy, Strings.DeltaSyncPartnerAuthenticationFailedEscalationMessage, configurations.DeltaSyncPartnerAuthenticationFailedMonitorAndResponderEnabled, NotificationServiceClass.UrgentInTraining, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
			broker.AddWorkDefinition<ResponderDefinition>(definition, traceContext);
		}

		// Token: 0x04001714 RID: 5908
		private const int DeltaSyncPartnerAuthenticationFailedMonitorThreshold = 10;

		// Token: 0x04001715 RID: 5909
		private static readonly TimeSpan DeltaSyncPartnerAuthenticationFailedMonitoringInterval = TimeSpan.FromSeconds(3600.0);

		// Token: 0x04001716 RID: 5910
		private static readonly TimeSpan DeltaSyncPartnerAuthenticationFailedRecurrenceInterval = TimeSpan.FromSeconds(3600.0);
	}
}
