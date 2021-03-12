using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Exchange.SharedCache.Client;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.SharedCache
{
	// Token: 0x02000491 RID: 1169
	public sealed class SharedCacheDiscovery : DiscoveryWorkItem
	{
		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x06001D86 RID: 7558 RVA: 0x000B17EC File Offset: 0x000AF9EC
		protected override Trace Trace
		{
			get
			{
				return ExTraceGlobals.CafeTracer;
			}
		}

		// Token: 0x06001D87 RID: 7559 RVA: 0x000B17F4 File Offset: 0x000AF9F4
		protected override void CreateWorkTasks(CancellationToken cancellationToken)
		{
			this.breadcrumbs = new Breadcrumbs(1024, this.traceContext);
			try
			{
				if (!LocalEndpointManager.IsDataCenter)
				{
					this.breadcrumbs.Drop("SharedCacheDiscovery.CreateWorkTasks: Only applicable in datacenter.");
				}
				else if (!LocalEndpointManager.Instance.ExchangeServerRoleEndpoint.IsCafeRoleInstalled)
				{
					this.breadcrumbs.Drop("SharedCacheDiscovery.CreateWorkTasks: Cafe role is not present on this server.");
				}
				else
				{
					this.Configure();
					this.CreateAllWorkItems();
				}
			}
			finally
			{
				this.ReportResult();
			}
		}

		// Token: 0x06001D88 RID: 7560 RVA: 0x000B1878 File Offset: 0x000AFA78
		private void CreateAllWorkItems()
		{
			this.CreateWorkItemsInternal("MailboxServerLocator", WellKnownSharedCache.MailboxServerLocator);
			this.CreateWorkItemsInternal("AnchorMailbox", WellKnownSharedCache.AnchorMailboxCache);
		}

		// Token: 0x06001D89 RID: 7561 RVA: 0x000B189C File Offset: 0x000AFA9C
		private void CreateWorkItemsInternal(string cacheName, Guid cacheGuid)
		{
			string targetResource = cacheGuid.ToString();
			this.breadcrumbs.Drop("Creating probe identity");
			ProbeIdentity probeIdentity = ProbeIdentity.Create(ExchangeComponent.SharedCache, ProbeType.Service, null, targetResource);
			this.breadcrumbs.Drop("Creating probe definition");
			ProbeDefinition probeDefinition = SharedCacheProbe.CreateDefinition(probeIdentity, this.ProbeRpcTimeoutMilliseconds);
			probeDefinition.RecurrenceIntervalSeconds = this.ProbeRecurrenceIntervalSeconds;
			probeDefinition.TimeoutSeconds = this.ProbeTimeoutSeconds;
			base.Broker.AddWorkDefinition<ProbeDefinition>(probeDefinition, this.traceContext);
			this.breadcrumbs.Drop("Creating monitor definition");
			MonitorIdentity monitorIdentity = probeIdentity.CreateMonitorIdentity();
			this.breadcrumbs.Drop("Creating monitor definition");
			MonitorDefinition monitorDefinition = OverallConsecutiveProbeFailuresMonitor.CreateDefinition(monitorIdentity.Name, probeIdentity.GetAlertMask(), monitorIdentity.Component.Name, monitorIdentity.Component, this.ProbeFailureCount, true, 300);
			monitorDefinition.TargetResource = targetResource;
			monitorDefinition.RecurrenceIntervalSeconds = 0;
			monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Degraded, this.DegradedTransitionSeconds),
				new MonitorStateTransition(ServiceHealthStatus.Unrecoverable, this.UnrecoverableTransitionSeconds)
			};
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, this.traceContext);
			if (this.RestartServiceResponderEnabled)
			{
				ResponderIdentity responderIdentity = monitorIdentity.CreateResponderIdentity(string.Format("{0}Restart", cacheName), null);
				this.breadcrumbs.Drop("Responder: {0}", new object[]
				{
					responderIdentity.Name
				});
				ResponderDefinition responderDefinition = RestartServiceResponder.CreateDefinition(responderIdentity.Name, monitorIdentity.Name, SharedCacheDiscovery.SharedCacheServiceName, ServiceHealthStatus.Degraded, 15, 120, 0, false, DumpMode.FullDump, null, 15.0, 0, "Exchange", null, true, true, null, false);
				responderDefinition.RecurrenceIntervalSeconds = 0;
				base.Broker.AddWorkDefinition<ResponderDefinition>(responderDefinition, this.traceContext);
			}
			if (this.EscalateResponderEnabled)
			{
				ResponderIdentity responderIdentity = monitorIdentity.CreateResponderIdentity(string.Format("{0}Escalate", cacheName), null);
				this.breadcrumbs.Drop("Responder: {0}", new object[]
				{
					responderIdentity.Name
				});
				ResponderDefinition responderDefinition2 = EscalateResponder.CreateDefinition(responderIdentity.Name, responderIdentity.Component.Name, monitorIdentity.Name, monitorIdentity.GetAlertMask(), responderIdentity.TargetResource, ServiceHealthStatus.Unrecoverable, monitorIdentity.Component.EscalationTeam, Strings.SharedCacheEscalationSubject, Strings.SharedCacheEscalationMessage, true, this.PagingAlertsEnabled ? NotificationServiceClass.Scheduled : NotificationServiceClass.UrgentInTraining, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
				responderDefinition2.RecurrenceIntervalSeconds = 0;
				base.Broker.AddWorkDefinition<ResponderDefinition>(responderDefinition2, this.traceContext);
			}
		}

		// Token: 0x06001D8A RID: 7562 RVA: 0x000B1B28 File Offset: 0x000AFD28
		private void ReportResult()
		{
			string text = this.breadcrumbs.ToString();
			base.Result.StateAttribute5 = text;
			WTFDiagnostics.TraceInformation(this.Trace, this.traceContext, text, null, "ReportResult", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\SharedCache\\SharedCacheDiscovery.cs", 194);
		}

		// Token: 0x06001D8B RID: 7563 RVA: 0x000B1B70 File Offset: 0x000AFD70
		private void Configure()
		{
			this.ProbeFailureCount = this.ReadAttribute("ProbeFailureCount", 3);
			this.ProbeRecurrenceIntervalSeconds = (int)this.ReadAttribute("ProbeRecurrenceInterval", SharedCacheDiscovery.DefaultValues.ProbeRecurrenceInterval).TotalSeconds;
			this.ProbeTimeoutSeconds = (int)this.ReadAttribute("ProbeTimeoutInterval", SharedCacheDiscovery.DefaultValues.ProbeTimeoutInterval).TotalSeconds;
			this.ProbeRpcTimeoutMilliseconds = this.ReadAttribute("ProbeRpcTimeoutMilliseconds", SharedCacheDiscovery.DefaultValues.ProbeRpcTimeoutMilliseconds);
			this.DegradedTransitionSeconds = (int)this.ReadAttribute("DegradedTransitionSpan", SharedCacheDiscovery.DefaultValues.DegradedTransitionSpan).TotalSeconds;
			this.UnrecoverableTransitionSeconds = (int)this.ReadAttribute("UnrecoverableTransitionSpan", SharedCacheDiscovery.DefaultValues.UnrecoverableTransitionSpan).TotalSeconds;
			this.RestartServiceResponderEnabled = this.ReadAttribute("RestartServiceResponderEnabled", false);
			this.EscalateResponderEnabled = this.ReadAttribute("EscalateResponderEnabled", false);
			this.PagingAlertsEnabled = this.ReadAttribute("PagingAlertsEnabled", false);
		}

		// Token: 0x0400147F RID: 5247
		private static readonly string SharedCacheServiceName = "MSExchangeSharedCache";

		// Token: 0x04001480 RID: 5248
		private Breadcrumbs breadcrumbs;

		// Token: 0x04001481 RID: 5249
		private TracingContext traceContext = TracingContext.Default;

		// Token: 0x04001482 RID: 5250
		private int ProbeFailureCount;

		// Token: 0x04001483 RID: 5251
		private int ProbeRecurrenceIntervalSeconds;

		// Token: 0x04001484 RID: 5252
		private int ProbeTimeoutSeconds;

		// Token: 0x04001485 RID: 5253
		private int ProbeRpcTimeoutMilliseconds;

		// Token: 0x04001486 RID: 5254
		private int DegradedTransitionSeconds;

		// Token: 0x04001487 RID: 5255
		private int UnrecoverableTransitionSeconds;

		// Token: 0x04001488 RID: 5256
		private bool RestartServiceResponderEnabled;

		// Token: 0x04001489 RID: 5257
		private bool EscalateResponderEnabled;

		// Token: 0x0400148A RID: 5258
		private bool PagingAlertsEnabled;

		// Token: 0x02000492 RID: 1170
		private class DefaultValues
		{
			// Token: 0x0400148B RID: 5259
			public const int ProbeFailureCount = 3;

			// Token: 0x0400148C RID: 5260
			public static readonly int ProbeRpcTimeoutMilliseconds = 100;

			// Token: 0x0400148D RID: 5261
			public static readonly TimeSpan ProbeRecurrenceInterval = TimeSpan.FromSeconds(60.0);

			// Token: 0x0400148E RID: 5262
			public static readonly TimeSpan ProbeTimeoutInterval = TimeSpan.FromSeconds(5.0);

			// Token: 0x0400148F RID: 5263
			public static readonly TimeSpan DegradedTransitionSpan = TimeSpan.FromSeconds(0.0);

			// Token: 0x04001490 RID: 5264
			public static readonly TimeSpan UnrecoverableTransitionSpan = TimeSpan.FromMinutes(15.0);
		}
	}
}
