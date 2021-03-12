using System;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.TransportSync
{
	// Token: 0x020004F9 RID: 1273
	internal class DeltaSyncServiceEndpointsLoadFailed : IWorkItem
	{
		// Token: 0x06001F78 RID: 8056 RVA: 0x000C006E File Offset: 0x000BE26E
		public void Initialize(MaintenanceDefinition discoveryDefinition, IMaintenanceWorkBroker broker, TracingContext traceContext)
		{
			this.InitializeMonitor(discoveryDefinition, broker, traceContext);
			this.InitializeResponder(discoveryDefinition, broker, traceContext);
		}

		// Token: 0x06001F79 RID: 8057 RVA: 0x000C0084 File Offset: 0x000BE284
		private void InitializeMonitor(MaintenanceDefinition discoveryDefinition, IMaintenanceWorkBroker broker, TracingContext traceContext)
		{
			Configurations configurations = Configurations.CreateFromWorkDefinition(discoveryDefinition);
			MonitorDefinition monitorDefinition = OverallXFailuresMonitor.CreateDefinition("DeltaSync.ServiceEndpointsLoad.Failed.Monitor", NotificationItem.GenerateResultName(ExchangeComponent.MailboxMigration.Name, TransportSyncNotificationEvent.DeltaSyncServiceEndpointsLoadFailed.ToString(), null), ExchangeComponent.MailboxMigration.Name, ExchangeComponent.MailboxMigration, (int)DeltaSyncServiceEndpointsLoadFailed.DeltaSyncServiceEndpointsLoadFailedMonitoringInterval.TotalSeconds, (int)DeltaSyncServiceEndpointsLoadFailed.DeltaSyncServiceEndpointsLoadFailedRecurrenceInterval.TotalSeconds, 10, true);
			monitorDefinition.Enabled = configurations.DeltaSyncServiceEndpointsLoadFailedMonitorAndResponderEnabled;
			monitorDefinition.ServicePriority = 2;
			monitorDefinition.ScenarioDescription = "Valdiate TransportSync is not impacted by DeltaSync service endpoint failure issues";
			broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, traceContext);
		}

		// Token: 0x06001F7A RID: 8058 RVA: 0x000C0114 File Offset: 0x000BE314
		private void InitializeResponder(MaintenanceDefinition discoveryDefinition, IMaintenanceWorkBroker broker, TracingContext traceContext)
		{
			Configurations configurations = Configurations.CreateFromWorkDefinition(discoveryDefinition);
			ResponderDefinition definition = EscalateResponder.CreateDefinition("DeltaSync.ServiceEndpointsLoad.Failed.Escalate", ExchangeComponent.MailboxMigration.Name, "DeltaSync.ServiceEndpointsLoad.Failed.Monitor", string.Format("{0}/{1}", "DeltaSync.ServiceEndpointsLoad.Failed.Monitor", ExchangeComponent.MailboxMigration.Name), Environment.MachineName, ServiceHealthStatus.None, ExchangeComponent.MailboxMigration.EscalationTeam, Strings.EscalationSubjectUnhealthy, Strings.DeltaSyncServiceEndpointsLoadFailedEscalationMessage, configurations.DeltaSyncServiceEndpointsLoadFailedMonitorAndResponderEnabled, NotificationServiceClass.UrgentInTraining, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
			broker.AddWorkDefinition<ResponderDefinition>(definition, traceContext);
		}

		// Token: 0x04001717 RID: 5911
		private const int DeltaSyncServiceEndpointsLoadFailedMonitorThreshold = 10;

		// Token: 0x04001718 RID: 5912
		private static readonly TimeSpan DeltaSyncServiceEndpointsLoadFailedMonitoringInterval = TimeSpan.FromSeconds(3600.0);

		// Token: 0x04001719 RID: 5913
		private static readonly TimeSpan DeltaSyncServiceEndpointsLoadFailedRecurrenceInterval = TimeSpan.FromSeconds(3600.0);
	}
}
