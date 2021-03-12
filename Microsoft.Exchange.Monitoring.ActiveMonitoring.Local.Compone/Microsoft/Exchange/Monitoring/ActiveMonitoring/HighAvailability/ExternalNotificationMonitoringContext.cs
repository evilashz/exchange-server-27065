using System;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.HighAvailability
{
	// Token: 0x020001A2 RID: 418
	internal class ExternalNotificationMonitoringContext : MonitoringContextBase
	{
		// Token: 0x06000C06 RID: 3078 RVA: 0x0004DBC8 File Offset: 0x0004BDC8
		public ExternalNotificationMonitoringContext(IMaintenanceWorkBroker broker, LocalEndpointManager endpointManager, TracingContext traceContext) : base(broker, endpointManager, traceContext)
		{
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x0004DBE3 File Offset: 0x0004BDE3
		public override void CreateContext()
		{
			base.InvokeCatchAndLog(delegate
			{
				this.CreateSiteSwitchoverNotificationContext();
			});
			base.InvokeCatchAndLog(delegate
			{
				this.CreateStoreNotificationContext();
			});
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x0004DC0C File Offset: 0x0004BE0C
		private void CreateSiteSwitchoverNotificationContext()
		{
			string name = "ServerSiteFailureMonitor";
			string name2 = "ServerSiteFailureEscalate";
			MonitorDefinition monitorDefinition = OverallConsecutiveProbeFailuresMonitor.CreateDefinition(name, NotificationItem.GenerateResultName("Network", "SiteFailure", null), HighAvailabilityConstants.ServiceName, ExchangeComponent.DataProtection, 1, true, 300);
			monitorDefinition.TargetResource = Environment.MachineName;
			monitorDefinition.RecurrenceIntervalSeconds = 0;
			monitorDefinition.ServicePriority = 0;
			monitorDefinition.ScenarioDescription = "Validate HA health is not impacted by site failure issues";
			base.AddChainedResponders(ref monitorDefinition, new MonitorStateResponderTuple[]
			{
				new MonitorStateResponderTuple
				{
					MonitorState = new MonitorStateTransition(ServiceHealthStatus.Unhealthy, 0),
					Responder = EscalateResponder.CreateDefinition(name2, HighAvailabilityConstants.ServiceName, monitorDefinition.Name, monitorDefinition.ConstructWorkItemResultName(), monitorDefinition.TargetResource, ServiceHealthStatus.Unhealthy, "High Availability", Strings.SiteFailureEscalationSubject(HighAvailabilityConstants.ServiceName, Environment.MachineName), Strings.SiteFailureEscalationMessage, true, NotificationServiceClass.Urgent, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false)
				}
			});
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x0004DCFC File Offset: 0x0004BEFC
		private void CreateStoreNotificationContext()
		{
			foreach (MailboxDatabaseInfo mailboxDatabaseInfo in base.EndpointManager.MailboxDatabaseEndpoint.MailboxDatabaseInfoCollectionForBackend)
			{
				string name = "DatabaseHealthStoreNotificationMonitor";
				string name2 = "DatabaseHealthStoreNotificationEscalate";
				MonitorDefinition monitorDefinition = OverallConsecutiveProbeFailuresMonitor.CreateDefinition(name, NotificationItem.GenerateResultName("MSExchangeIS", "DatabaseNotAvailable", mailboxDatabaseInfo.MailboxDatabaseName), HighAvailabilityConstants.ServiceName, ExchangeComponent.DataProtection, 1, true, 300);
				monitorDefinition.TargetResource = mailboxDatabaseInfo.MailboxDatabaseName;
				monitorDefinition.RecurrenceIntervalSeconds = 0;
				monitorDefinition.ServicePriority = 0;
				monitorDefinition.ScenarioDescription = "Validate HA health is not impacted by database availability issues";
				base.AddChainedResponders(ref monitorDefinition, new MonitorStateResponderTuple[]
				{
					new MonitorStateResponderTuple
					{
						MonitorState = new MonitorStateTransition(ServiceHealthStatus.Unhealthy, 0),
						Responder = EscalateResponder.CreateDefinition(name2, HighAvailabilityConstants.ServiceName, monitorDefinition.Name, monitorDefinition.ConstructWorkItemResultName(), monitorDefinition.TargetResource, ServiceHealthStatus.Unhealthy, "High Availability", Strings.StoreNotificationEscalationSubject(HighAvailabilityConstants.ServiceName, Environment.MachineName, mailboxDatabaseInfo.MailboxDatabaseName), Strings.StoreNotificationEscalationMessage(mailboxDatabaseInfo.MailboxDatabaseName), true, NotificationServiceClass.Urgent, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false)
					}
				});
			}
		}
	}
}
