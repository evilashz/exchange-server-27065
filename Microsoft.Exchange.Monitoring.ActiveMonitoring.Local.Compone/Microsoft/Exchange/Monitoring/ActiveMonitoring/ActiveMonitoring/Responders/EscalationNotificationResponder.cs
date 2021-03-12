using System;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders
{
	// Token: 0x020000F1 RID: 241
	public class EscalationNotificationResponder : ResponderWorkItem
	{
		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000780 RID: 1920 RVA: 0x0002CC8F File Offset: 0x0002AE8F
		// (set) Token: 0x06000781 RID: 1921 RVA: 0x0002CC97 File Offset: 0x0002AE97
		internal ProbeResult LastFailedProbeResult { get; private set; }

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000782 RID: 1922 RVA: 0x0002CCA0 File Offset: 0x0002AEA0
		// (set) Token: 0x06000783 RID: 1923 RVA: 0x0002CCA8 File Offset: 0x0002AEA8
		internal MonitorResult LastFailedMonitorResult { get; private set; }

		// Token: 0x06000784 RID: 1924 RVA: 0x0002CCB4 File Offset: 0x0002AEB4
		public static ResponderDefinition CreateDefinition(string name, string serviceName, string alertTypeId, string alertMask, string targetResource, ServiceHealthStatus targetHealthState, bool enabled = true, int recurrenceIntervalSeconds = 300)
		{
			return new ResponderDefinition
			{
				AssemblyPath = EscalationNotificationResponder.AssemblyPath,
				TypeName = EscalationNotificationResponder.TypeName,
				Name = name,
				ServiceName = serviceName,
				AlertTypeId = alertTypeId,
				AlertMask = alertMask,
				TargetResource = targetResource,
				TargetHealthState = targetHealthState,
				RecurrenceIntervalSeconds = recurrenceIntervalSeconds,
				TimeoutSeconds = Math.Min(recurrenceIntervalSeconds / 2, (int)EscalationNotificationResponder.DefaultTimeoutSeconds.TotalSeconds),
				MaxRetryAttempts = 3,
				Enabled = enabled
			};
		}

		// Token: 0x06000785 RID: 1925 RVA: 0x0002CD3C File Offset: 0x0002AF3C
		protected override void DoResponderWork(CancellationToken cancellationToken)
		{
			base.Result.RecoveryResult = ServiceRecoveryResult.Failed;
			this.LastFailedMonitorResult = WorkItemResultHelper.GetLastFailedMonitorResult(this, base.Broker, cancellationToken);
			if (this.LastFailedMonitorResult == null)
			{
				base.Result.StateAttribute1 = "No monitor result - skipping notification";
				return;
			}
			this.LastFailedProbeResult = WorkItemResultHelper.GetLastFailedProbeResult(this.LastFailedMonitorResult, base.Broker, cancellationToken);
			if (this.LastFailedProbeResult != null)
			{
				string message = this.LastFailedProbeResult.IsNotified ? this.LastFailedProbeResult.Error : this.LastFailedProbeResult.Exception;
				this.PublishNotification(message, base.Definition.ServiceName, base.Definition.Name, base.Definition.TargetPartition);
				return;
			}
			base.Result.StateAttribute1 = "No probe result - skipping notification";
		}

		// Token: 0x06000786 RID: 1926 RVA: 0x0002CE08 File Offset: 0x0002B008
		private void PublishNotification(string message, string serviceName, string notificationName, string notificationReason)
		{
			WTFDiagnostics.TraceInformation(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "Logging a notification for monitor watching for alert to turn into alert state", null, "PublishNotification", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Common\\Responders\\EscalationNotificationResponder.cs", 154);
			EventNotificationItem eventNotificationItem = new EventNotificationItem(serviceName, notificationName, notificationReason, message, ResultSeverityLevel.Error);
			eventNotificationItem.StateAttribute1 = base.Definition.TargetResource;
			this.PopulateCustomAttributes(eventNotificationItem);
			eventNotificationItem.Publish(false);
		}

		// Token: 0x06000787 RID: 1927 RVA: 0x0002CE68 File Offset: 0x0002B068
		public virtual void PopulateCustomAttributes(EventNotificationItem notificationItem)
		{
			if (this.LastFailedProbeResult != null)
			{
				notificationItem.StateAttribute2 = this.LastFailedProbeResult.WorkItemId.ToString();
				notificationItem.StateAttribute3 = this.LastFailedProbeResult.ResultId.ToString();
			}
		}

		// Token: 0x04000501 RID: 1281
		private static TimeSpan DefaultTimeoutSeconds = TimeSpan.FromSeconds(120.0);

		// Token: 0x04000502 RID: 1282
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x04000503 RID: 1283
		private static readonly string TypeName = typeof(EscalationNotificationResponder).FullName;
	}
}
