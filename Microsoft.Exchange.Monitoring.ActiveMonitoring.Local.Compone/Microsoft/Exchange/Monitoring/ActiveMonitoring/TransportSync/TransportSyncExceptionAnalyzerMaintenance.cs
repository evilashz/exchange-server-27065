using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.TransportSync
{
	// Token: 0x020004FC RID: 1276
	public sealed class TransportSyncExceptionAnalyzerMaintenance : MaintenanceWorkItem
	{
		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06001F92 RID: 8082 RVA: 0x000C03B2 File Offset: 0x000BE5B2
		// (set) Token: 0x06001F93 RID: 8083 RVA: 0x000C03BA File Offset: 0x000BE5BA
		public TimeSpan ExceptionRequestsMonitorRecurrenceInterval { get; private set; }

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06001F94 RID: 8084 RVA: 0x000C03C3 File Offset: 0x000BE5C3
		// (set) Token: 0x06001F95 RID: 8085 RVA: 0x000C03CB File Offset: 0x000BE5CB
		public TimeSpan ExceptionRequestsMonitoringInterval { get; private set; }

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06001F96 RID: 8086 RVA: 0x000C03D4 File Offset: 0x000BE5D4
		// (set) Token: 0x06001F97 RID: 8087 RVA: 0x000C03DC File Offset: 0x000BE5DC
		public TimeSpan ResponderRecurrenceInterval { get; private set; }

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06001F98 RID: 8088 RVA: 0x000C03E5 File Offset: 0x000BE5E5
		// (set) Token: 0x06001F99 RID: 8089 RVA: 0x000C03ED File Offset: 0x000BE5ED
		public bool AlertResponderEnabled { get; private set; }

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06001F9A RID: 8090 RVA: 0x000C03F6 File Offset: 0x000BE5F6
		// (set) Token: 0x06001F9B RID: 8091 RVA: 0x000C03FE File Offset: 0x000BE5FE
		public bool ExceptionRequestsMonitorEnabled { get; private set; }

		// Token: 0x06001F9C RID: 8092 RVA: 0x000C0408 File Offset: 0x000BE608
		protected override void DoWork(CancellationToken cancellationToken)
		{
			LocalEndpointManager instance = LocalEndpointManager.Instance;
			string message;
			if (!instance.ExchangeServerRoleEndpoint.IsMailboxRoleInstalled)
			{
				message = "TransportSyncExceptionAnalyzerMaintenance.DoWork: Mailbox role is not present on this server.";
				WTFDiagnostics.TraceInformation(ExTraceGlobals.RBATracer, base.TraceContext, message, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\TransportSync\\Discovery\\TransportSyncExceptionAnalyzerMaintenance.cs", 97);
				return;
			}
			message = "TransportSyncExceptionAnalyzerMaintenance.DoWork: Mailbox role is present on this server, so creating monitor and responder definitions";
			WTFDiagnostics.TraceInformation(ExTraceGlobals.RBATracer, base.TraceContext, message, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\TransportSync\\Discovery\\TransportSyncExceptionAnalyzerMaintenance.cs", 102);
			this.Configure(base.TraceContext);
			foreach (TransportSyncExceptionAnalyzerAlertDefinition transportSyncExceptionAnalyzerAlertDefinition in new List<TransportSyncExceptionAnalyzerAlertDefinition>
			{
				this.CreateAlertDefinition(AggregationSubscriptionType.Pop, ExchangeComponent.MailboxMigration),
				this.CreateAlertDefinition(AggregationSubscriptionType.IMAP, ExchangeComponent.MailboxMigration),
				this.CreateAlertDefinition(AggregationSubscriptionType.DeltaSyncMail, ExchangeComponent.MailboxMigration),
				this.CreateAlertDefinition(AggregationSubscriptionType.Facebook, ExchangeComponent.PeopleConnect),
				this.CreateAlertDefinition(AggregationSubscriptionType.LinkedIn, ExchangeComponent.PeopleConnect)
			})
			{
				if (transportSyncExceptionAnalyzerAlertDefinition.IsEnabled)
				{
					MonitorDefinition monitorDefinition = OverallConsecutiveProbeFailuresMonitor.CreateDefinition(transportSyncExceptionAnalyzerAlertDefinition.MonitorName, string.Format("{0}/{1}", transportSyncExceptionAnalyzerAlertDefinition.Component.Name, transportSyncExceptionAnalyzerAlertDefinition.MonitorName), transportSyncExceptionAnalyzerAlertDefinition.Component.Name, transportSyncExceptionAnalyzerAlertDefinition.Component, 1, true, 300);
					monitorDefinition.MonitoringIntervalSeconds = (int)transportSyncExceptionAnalyzerAlertDefinition.MonitoringInterval.TotalSeconds;
					monitorDefinition.RecurrenceIntervalSeconds = (int)transportSyncExceptionAnalyzerAlertDefinition.RecurrenceInterval.TotalSeconds;
					monitorDefinition.ServicePriority = 2;
					monitorDefinition.ScenarioDescription = "Validate TransportSync is not impacted by any issues";
					base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
					if (this.AlertResponderEnabled)
					{
						ResponderDefinition responderDefinition = EscalateResponder.CreateDefinition(transportSyncExceptionAnalyzerAlertDefinition.MonitorName, transportSyncExceptionAnalyzerAlertDefinition.Component.Name, transportSyncExceptionAnalyzerAlertDefinition.MonitorName, transportSyncExceptionAnalyzerAlertDefinition.MonitorName, Environment.MachineName, ServiceHealthStatus.None, transportSyncExceptionAnalyzerAlertDefinition.Component.EscalationTeam, transportSyncExceptionAnalyzerAlertDefinition.MessageSubject, transportSyncExceptionAnalyzerAlertDefinition.MessageBody, true, NotificationServiceClass.Urgent, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
						responderDefinition.RecurrenceIntervalSeconds = (int)this.ResponderRecurrenceInterval.TotalSeconds;
						base.Broker.AddWorkDefinition<ResponderDefinition>(responderDefinition, base.TraceContext);
					}
				}
			}
		}

		// Token: 0x06001F9D RID: 8093 RVA: 0x000C0650 File Offset: 0x000BE850
		private TransportSyncExceptionAnalyzerAlertDefinition CreateAlertDefinition(AggregationSubscriptionType subscriptionType, Component component)
		{
			return new TransportSyncExceptionAnalyzerAlertDefinition
			{
				RedEvent = string.Format("TxSync{0}RequestsWithExceptionsReachedThreshold", subscriptionType),
				MessageSubject = string.Format("The number of {0} subscription sync requests with exceptions has exceeded threshold.", subscriptionType),
				MessageBody = "{Probe.ExtensionXml}",
				RecurrenceInterval = this.ExceptionRequestsMonitorRecurrenceInterval,
				MonitoringInterval = this.ExceptionRequestsMonitoringInterval,
				IsEnabled = this.ExceptionRequestsMonitorEnabled,
				Component = component
			};
		}

		// Token: 0x06001F9E RID: 8094 RVA: 0x000C06C8 File Offset: 0x000BE8C8
		private void Configure(TracingContext context)
		{
			this.ExceptionRequestsMonitorRecurrenceInterval = this.ReadAttribute("ExceptionRequestsMonitorRecurrenceInterval", TransportSyncExceptionAnalyzerMaintenance.defaultMonitorRecurrenceInteral);
			this.ExceptionRequestsMonitoringInterval = this.ReadAttribute("ExceptionRequestsMonitoringInterval", TransportSyncExceptionAnalyzerMaintenance.defaultMonitoringInterval);
			this.ResponderRecurrenceInterval = this.ReadAttribute("ResponderRecurrenceInterval", TransportSyncExceptionAnalyzerMaintenance.defaultResponderRecurrenceInterval);
			this.AlertResponderEnabled = this.ReadAttribute("AlertResponderEnabled", TransportSyncExceptionAnalyzerMaintenance.defaultAlertResponderEnabled);
			this.ExceptionRequestsMonitorEnabled = this.ReadAttribute("ExceptionRequestsMonitorEnabled", TransportSyncExceptionAnalyzerMaintenance.defaultExceptionRequestsMonitorEnabled);
			WTFDiagnostics.TraceInformation(ExTraceGlobals.TransportSyncTracer, context, "Configuration value are initialized successfully", null, "Configure", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\TransportSync\\Discovery\\TransportSyncExceptionAnalyzerMaintenance.cs", 191);
		}

		// Token: 0x04001724 RID: 5924
		private static TimeSpan defaultMonitorRecurrenceInteral = TimeSpan.FromMinutes(5.0);

		// Token: 0x04001725 RID: 5925
		private static TimeSpan defaultResponderRecurrenceInterval = TimeSpan.FromMinutes(5.0);

		// Token: 0x04001726 RID: 5926
		private static TimeSpan defaultMonitoringInterval = TimeSpan.FromMinutes(15.0);

		// Token: 0x04001727 RID: 5927
		private static bool defaultAlertResponderEnabled = true;

		// Token: 0x04001728 RID: 5928
		private static bool defaultExceptionRequestsMonitorEnabled = true;
	}
}
