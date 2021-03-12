using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Auditing
{
	// Token: 0x0200000E RID: 14
	public sealed class AuditingPassiveDiscovery : DiscoveryWorkItem
	{
		// Token: 0x06000044 RID: 68 RVA: 0x00004DF8 File Offset: 0x00002FF8
		public static string MonitorName(string eventName)
		{
			return eventName + "Monitor";
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00004E05 File Offset: 0x00003005
		public static string ResponderName(string eventName)
		{
			return eventName + "EscalateResponder";
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00004E12 File Offset: 0x00003012
		public static string FailureTriggerErrorMask(string failureTriggerError)
		{
			return NotificationItem.GenerateResultName(ExchangeComponent.Eds.Name, failureTriggerError, null);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00004E28 File Offset: 0x00003028
		protected override void CreateWorkTasks(CancellationToken cancellationToken)
		{
			this.breadcrumbs = new Breadcrumbs(1024, this.traceContext);
			try
			{
				if (!LocalEndpointManager.IsDataCenter)
				{
					this.breadcrumbs.Drop("CreateWorkTasks: Datacenter only.");
				}
				else if (!LocalEndpointManager.Instance.ExchangeServerRoleEndpoint.IsMailboxRoleInstalled)
				{
					this.breadcrumbs.Drop("CreateWorkTasks: Mailbox role is not present on this server.");
				}
				else
				{
					this.Configure();
					if (this.monitorActivationStatus[PassiveMonitorType.MailboxAuditingAvailability])
					{
						this.breadcrumbs.Drop("Creating Mailbox Auditing Availability Failure work definitions");
						this.CreateFailureMonitorAndResponder(AuditingPassiveDiscovery.MailboxAuditingAvailabilityFailureEvent, AuditingPassiveDiscovery.FailureTriggerErrorMask(AuditingPassiveDiscovery.MailboxAuditingAvailabilityFailureError), ExchangeComponent.Compliance, 1800, 1800, 2, Strings.MailboxAuditingAvailabilityFailureEscalationSubject, Strings.MailboxAuditingAvailabilityFailureEscalationBody, NotificationServiceClass.UrgentInTraining);
					}
					if (this.monitorActivationStatus[PassiveMonitorType.AdminAuditingAvailability])
					{
						this.breadcrumbs.Drop("Creating Admin Auditing Availability Failure work definitions");
						this.CreateFailureMonitorAndResponder(AuditingPassiveDiscovery.AdminAuditingAvailabilityFailureEvent, AuditingPassiveDiscovery.FailureTriggerErrorMask(AuditingPassiveDiscovery.AdminAuditingAvailabilityFailureError), ExchangeComponent.Compliance, 1800, 1800, 2, Strings.AdminAuditingAvailabilityFailureEscalationSubject, Strings.AdminAuditingAvailabilityFailureEscalationBody, NotificationServiceClass.UrgentInTraining);
					}
					if (this.monitorActivationStatus[PassiveMonitorType.SynchronousAuditSearchAvailability])
					{
						this.breadcrumbs.Drop("Creating Synchronous Audit Search Availability Failure work definitions");
						this.CreateFailureMonitorAndResponder(AuditingPassiveDiscovery.SynchronousAuditSearchAvailabilityFailureEvent, AuditingPassiveDiscovery.FailureTriggerErrorMask(AuditingPassiveDiscovery.SynchronousAuditSearchAvailabilityFailureError), ExchangeComponent.Compliance, 1800, 1800, 2, Strings.SynchronousAuditSearchAvailabilityFailureEscalationSubject, Strings.SynchronousAuditSearchAvailabilityFailureEscalationBody, NotificationServiceClass.Urgent);
					}
					if (this.monitorActivationStatus[PassiveMonitorType.AsynchronousAuditSearchAvailability])
					{
						this.breadcrumbs.Drop("Creating Asynchronous Audit Search Availability Failure work definitions");
						this.CreateFailureMonitorAndResponder(AuditingPassiveDiscovery.AsynchronousAuditSearchAvailabilityFailureEvent, AuditingPassiveDiscovery.FailureTriggerErrorMask(AuditingPassiveDiscovery.AsynchronousAuditSearchAvailabilityFailureError), ExchangeComponent.Compliance, 1800, 1800, 2, Strings.AsynchronousAuditSearchAvailabilityFailureEscalationSubject, Strings.AsynchronousAuditSearchAvailabilityFailureEscalationBody, NotificationServiceClass.Urgent);
					}
				}
			}
			finally
			{
				this.ReportResult();
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000048 RID: 72 RVA: 0x00005018 File Offset: 0x00003218
		protected override Trace Trace
		{
			get
			{
				return ExTraceGlobals.AuditTracer;
			}
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00005020 File Offset: 0x00003220
		private void CreateFailureMonitorAndResponder(string eventName, string sampleMask, Component exchangeComponent, int monitoringIntervalSeconds, int recurrenceIntervalSeconds, int failureThreshold, string escalationSubject, string escalationBody, NotificationServiceClass alertClass)
		{
			string text = AuditingPassiveDiscovery.MonitorName(eventName);
			MonitorDefinition monitorDefinition = OverallXFailuresMonitor.CreateDefinition(text, sampleMask, exchangeComponent.Name, exchangeComponent, monitoringIntervalSeconds, recurrenceIntervalSeconds, failureThreshold, true);
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, this.traceContext);
			ResponderDefinition definition = EscalateResponder.CreateDefinition(AuditingPassiveDiscovery.ResponderName(eventName), exchangeComponent.Name, text, monitorDefinition.ConstructWorkItemResultName(), exchangeComponent.Name, ServiceHealthStatus.Unhealthy, ExchangeComponent.Auditing.EscalationTeam, escalationSubject, escalationBody, true, alertClass, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
			base.Broker.AddWorkDefinition<ResponderDefinition>(definition, this.traceContext);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000050AC File Offset: 0x000032AC
		private void ReportResult()
		{
			string text = this.breadcrumbs.ToString();
			base.Result.StateAttribute5 = text;
			WTFDiagnostics.TraceInformation(ExTraceGlobals.AuditTracer, this.traceContext, text, null, "ReportResult", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Auditing\\AuditingPassiveDiscovery.cs", 262);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x000050F4 File Offset: 0x000032F4
		private void Configure()
		{
			foreach (object obj in Enum.GetValues(typeof(PassiveMonitorType)))
			{
				PassiveMonitorType passiveMonitorType = (PassiveMonitorType)obj;
				this.monitorActivationStatus[passiveMonitorType] = this.ReadAttribute(passiveMonitorType + "MonitorEnabled", false);
			}
		}

		// Token: 0x04000038 RID: 56
		internal const string MonitorNameSuffix = "Monitor";

		// Token: 0x04000039 RID: 57
		private const string ResponderNameSuffix = "EscalateResponder";

		// Token: 0x0400003A RID: 58
		private const int DefaultFailureThreshold = 2;

		// Token: 0x0400003B RID: 59
		private const int DefaultIntervalSeconds = 1800;

		// Token: 0x0400003C RID: 60
		public static readonly string AdminAuditingAvailabilityFailureEvent = "ComplianceAdminAuditingAvailabilityFailure";

		// Token: 0x0400003D RID: 61
		public static readonly string AdminAuditingAvailabilityFailureError = "AdminAuditingFailureAboveThreshold";

		// Token: 0x0400003E RID: 62
		public static readonly string MailboxAuditingAvailabilityFailureEvent = "ComplianceMbxAuditingAvailabilityFailure";

		// Token: 0x0400003F RID: 63
		public static readonly string MailboxAuditingAvailabilityFailureError = "MailboxAuditingFailureAboveThreshold";

		// Token: 0x04000040 RID: 64
		public static readonly string SynchronousAuditSearchAvailabilityFailureEvent = "ComplianceSyncAuditSearchAvailabilityFailure";

		// Token: 0x04000041 RID: 65
		public static readonly string SynchronousAuditSearchAvailabilityFailureError = "SynchronousAuditSearchFailureAboveThreshold";

		// Token: 0x04000042 RID: 66
		public static readonly string AsynchronousAuditSearchAvailabilityFailureEvent = "ComplianceAsyncAuditSearchAvailabilityFailure";

		// Token: 0x04000043 RID: 67
		public static readonly string AsynchronousAuditSearchAvailabilityFailureError = "AsynchronousAuditSearchFailureAboveThreshold";

		// Token: 0x04000044 RID: 68
		private Breadcrumbs breadcrumbs;

		// Token: 0x04000045 RID: 69
		private Dictionary<PassiveMonitorType, bool> monitorActivationStatus = new Dictionary<PassiveMonitorType, bool>();

		// Token: 0x04000046 RID: 70
		private TracingContext traceContext = TracingContext.Default;
	}
}
