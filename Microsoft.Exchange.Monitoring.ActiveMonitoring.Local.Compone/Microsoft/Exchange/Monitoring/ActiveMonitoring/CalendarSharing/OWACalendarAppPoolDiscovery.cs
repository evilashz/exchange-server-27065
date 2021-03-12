using System;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.CalendarSharing.Probes;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Responders;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.CalendarSharing
{
	// Token: 0x02000058 RID: 88
	public sealed class OWACalendarAppPoolDiscovery : MaintenanceWorkItem
	{
		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x00012A64 File Offset: 0x00010C64
		// (set) Token: 0x060002B9 RID: 697 RVA: 0x00012A6C File Offset: 0x00010C6C
		public string OWACalendarAppPoolUrl { get; private set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060002BA RID: 698 RVA: 0x00012A75 File Offset: 0x00010C75
		// (set) Token: 0x060002BB RID: 699 RVA: 0x00012A7D File Offset: 0x00010C7D
		public bool IsOnPremisesEnabled { get; private set; }

		// Token: 0x17000098 RID: 152
		// (get) Token: 0x060002BC RID: 700 RVA: 0x00012A86 File Offset: 0x00010C86
		// (set) Token: 0x060002BD RID: 701 RVA: 0x00012A8E File Offset: 0x00010C8E
		public int UnrecoverableTransitionSpan { get; private set; }

		// Token: 0x17000099 RID: 153
		// (get) Token: 0x060002BE RID: 702 RVA: 0x00012A97 File Offset: 0x00010C97
		// (set) Token: 0x060002BF RID: 703 RVA: 0x00012A9F File Offset: 0x00010C9F
		public int ProbeRecurrenceInterval { get; private set; }

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x060002C0 RID: 704 RVA: 0x00012AA8 File Offset: 0x00010CA8
		// (set) Token: 0x060002C1 RID: 705 RVA: 0x00012AB0 File Offset: 0x00010CB0
		public int MonitorInterval { get; private set; }

		// Token: 0x1700009B RID: 155
		// (get) Token: 0x060002C2 RID: 706 RVA: 0x00012AB9 File Offset: 0x00010CB9
		// (set) Token: 0x060002C3 RID: 707 RVA: 0x00012AC1 File Offset: 0x00010CC1
		public int MonitorRecurrenceInterval { get; private set; }

		// Token: 0x1700009C RID: 156
		// (get) Token: 0x060002C4 RID: 708 RVA: 0x00012ACA File Offset: 0x00010CCA
		// (set) Token: 0x060002C5 RID: 709 RVA: 0x00012AD2 File Offset: 0x00010CD2
		public int ResponderRecurrenceInterval { get; private set; }

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x060002C6 RID: 710 RVA: 0x00012ADB File Offset: 0x00010CDB
		// (set) Token: 0x060002C7 RID: 711 RVA: 0x00012AE3 File Offset: 0x00010CE3
		public int ProbeTimeout { get; private set; }

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x060002C8 RID: 712 RVA: 0x00012AEC File Offset: 0x00010CEC
		// (set) Token: 0x060002C9 RID: 713 RVA: 0x00012AF4 File Offset: 0x00010CF4
		public int AlertResponderWaitInterval { get; private set; }

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x060002CA RID: 714 RVA: 0x00012AFD File Offset: 0x00010CFD
		// (set) Token: 0x060002CB RID: 715 RVA: 0x00012B05 File Offset: 0x00010D05
		public int ResetAppPoolResponderWaitInterval { get; private set; }

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x060002CC RID: 716 RVA: 0x00012B0E File Offset: 0x00010D0E
		// (set) Token: 0x060002CD RID: 717 RVA: 0x00012B16 File Offset: 0x00010D16
		public int FailureCount { get; private set; }

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002CE RID: 718 RVA: 0x00012B1F File Offset: 0x00010D1F
		// (set) Token: 0x060002CF RID: 719 RVA: 0x00012B27 File Offset: 0x00010D27
		public bool IsAlertResponderEnabled { get; private set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x060002D0 RID: 720 RVA: 0x00012B30 File Offset: 0x00010D30
		// (set) Token: 0x060002D1 RID: 721 RVA: 0x00012B38 File Offset: 0x00010D38
		public bool IsRestartAppPoolResponderEnabled { get; private set; }

		// Token: 0x060002D2 RID: 722 RVA: 0x00012B44 File Offset: 0x00010D44
		protected override void DoWork(CancellationToken cancellationToken)
		{
			try
			{
				LocalEndpointManager instance = LocalEndpointManager.Instance;
				this.OWACalendarAppPoolUrl = this.ReadAttribute("OWACalendarAppPoolUrl", OWACalendarAppPoolDiscovery.DefaultOWACalendarAppPoolUrl);
				this.IsOnPremisesEnabled = this.ReadAttribute("EnableOnPrem", false);
				this.ProbeRecurrenceInterval = (int)this.ReadAttribute("ProbeRecurrenceInterval", OWACalendarAppPoolDiscovery.DefaultProbeRecurrenceInterval).TotalSeconds;
				this.MonitorRecurrenceInterval = (int)this.ReadAttribute("MonitorRecurrenceInterval", OWACalendarAppPoolDiscovery.DefaultMonitorRecurrenceInterval).TotalSeconds;
				this.MonitorInterval = (int)this.ReadAttribute("MonitorInterval", OWACalendarAppPoolDiscovery.DefaultMonitorInterval).TotalSeconds;
				this.ResponderRecurrenceInterval = (int)this.ReadAttribute("ResponderRecurrenceInterval", OWACalendarAppPoolDiscovery.DefaultResponderRecurrenceInterval).TotalSeconds;
				this.ProbeTimeout = (int)this.ReadAttribute("ProbeTimeout", OWACalendarAppPoolDiscovery.DefaultProbeTimeout).TotalSeconds;
				this.AlertResponderWaitInterval = (int)this.ReadAttribute("AlertResponderWaitInterval", OWACalendarAppPoolDiscovery.DefaultAlertResponderWaitInterval).TotalSeconds;
				this.ResetAppPoolResponderWaitInterval = (int)this.ReadAttribute("ResetAppPoolResponderWaitInterval", OWACalendarAppPoolDiscovery.DefaultResetAppPoolResponderWaitInterval).TotalSeconds;
				this.FailureCount = this.ReadAttribute("FailureCount", OWACalendarAppPoolDiscovery.DefaultFailureCount);
				this.UnrecoverableTransitionSpan = (int)this.ReadAttribute("UnrecoverableTransitionSpan", OWACalendarAppPoolDiscovery.DefaultUnrecoverableTransitionSpan).TotalSeconds;
				this.IsAlertResponderEnabled = this.ReadAttribute("AlertResponderEnabled", true);
				this.IsRestartAppPoolResponderEnabled = this.ReadAttribute("RestartAppPoolResponderEnabled", true);
				this.breadcrumbs = new Breadcrumbs(1024, base.TraceContext);
				if (!LocalEndpointManager.IsDataCenter && !this.IsOnPremisesEnabled)
				{
					this.breadcrumbs.Drop("OWACalendarAppPoolDiscovery.DoWork: Skip creating the probe on On-Prem servers with IsOnPremEnabled flag set to false");
				}
				else if (!instance.ExchangeServerRoleEndpoint.IsMailboxRoleInstalled || instance.ExchangeServerRoleEndpoint.IsCafeRoleInstalled)
				{
					this.breadcrumbs.Drop("OWACalendarAppPoolDiscovery.DoWork: Skip creating the probe for non-MBX server or MBX server with Cafe installed");
				}
				else
				{
					this.SetupOWACalendarAppPoolMonitoring(base.TraceContext, instance);
					WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CalendarSharingTracer, base.TraceContext, "OWACalendarAppPoolDiscovery.DoWork: Created OWACalendarAppPool probe, monitor and responder for server {0}", Environment.MachineName, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\CalendarSharing\\OWACalendarAppPoolDiscovery.cs", 168);
				}
			}
			finally
			{
				this.ReportResult();
			}
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00012D78 File Offset: 0x00010F78
		private void ReportResult()
		{
			string text = this.breadcrumbs.ToString();
			base.Result.StateAttribute5 = text;
			WTFDiagnostics.TraceInformation(ExTraceGlobals.CalendarSharingTracer, base.TraceContext, text, null, "ReportResult", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\CalendarSharing\\OWACalendarAppPoolDiscovery.cs", 189);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00012DC0 File Offset: 0x00010FC0
		private void SetupOWACalendarAppPoolMonitoring(TracingContext traceContext, LocalEndpointManager endpointManager)
		{
			Strings.OWACalendarAppPoolEscalationSubject(Environment.MachineName);
			Strings.OWACalendarAppPoolEscalationBody;
			WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CalendarSharingTracer, base.TraceContext, "OWACalendarAppPoolDiscovery.SetupOWACalendarAppPoolMonitoring: Creating {0} for this server", "OWACalendarSelfTestProbe", null, "SetupOWACalendarAppPoolMonitoring", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\CalendarSharing\\OWACalendarAppPoolDiscovery.cs", 206);
			ProbeDefinition probeDefinition = new ProbeDefinition();
			probeDefinition.AssemblyPath = OWACalendarAppPoolDiscovery.AssemblyPath;
			probeDefinition.TypeName = OWACalendarAppPoolDiscovery.OWACalendarAppPoolProbeTypeName;
			probeDefinition.Name = "OWACalendarSelfTestProbe";
			probeDefinition.TargetResource = Environment.MachineName;
			probeDefinition.RecurrenceIntervalSeconds = this.ProbeRecurrenceInterval;
			probeDefinition.TimeoutSeconds = this.ProbeTimeout;
			probeDefinition.MaxRetryAttempts = 0;
			probeDefinition.Endpoint = this.OWACalendarAppPoolUrl;
			probeDefinition.ServiceName = ExchangeComponent.Calendaring.Name;
			base.Broker.AddWorkDefinition<ProbeDefinition>(probeDefinition, base.TraceContext);
			WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CalendarSharingTracer, base.TraceContext, "OWACalendarAppPoolDiscovery.SetupOWACalendarAppPoolMonitoring: Creating {0} for this server", "OWACalendarSelfTestMonitor", null, "SetupOWACalendarAppPoolMonitoring", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\CalendarSharing\\OWACalendarAppPoolDiscovery.cs", 226);
			MonitorDefinition monitorDefinition = OverallConsecutiveProbeFailuresMonitor.CreateDefinition("OWACalendarSelfTestMonitor", probeDefinition.ConstructWorkItemResultName(), ExchangeComponent.Calendaring.Name, ExchangeComponent.Calendaring, this.FailureCount, true, this.MonitorInterval);
			monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Degraded, 0),
				new MonitorStateTransition(ServiceHealthStatus.Unrecoverable, this.UnrecoverableTransitionSpan)
			};
			monitorDefinition.RecurrenceIntervalSeconds = this.MonitorRecurrenceInterval;
			monitorDefinition.ServicePriority = 2;
			monitorDefinition.ScenarioDescription = "Validate OWA calendar health is not impacetd by apppool issues";
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
			string text = monitorDefinition.ConstructWorkItemResultName();
			if (this.IsRestartAppPoolResponderEnabled)
			{
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CalendarSharingTracer, base.TraceContext, "OWACalendarAppPoolDiscovery.SetupOWACalendarAppPoolMonitoring: Creating {0} for this server", "OWACalendarSelfTestRecycleAppPool", null, "SetupOWACalendarAppPoolMonitoring", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\CalendarSharing\\OWACalendarAppPoolDiscovery.cs", 264);
				string responderName = "OWACalendarSelfTestRecycleAppPool";
				string monitorName = text;
				string appPoolName = "OWACalendarAppPool";
				ServiceHealthStatus responderTargetState = ServiceHealthStatus.Degraded;
				bool enabled = true;
				ResponderDefinition responderDefinition = ResetIISAppPoolResponder.CreateDefinition(responderName, monitorName, appPoolName, responderTargetState, DumpMode.None, null, 15.0, 0, ExchangeComponent.Calendaring.Name, enabled, "Dag");
				responderDefinition.MinimumSecondsBetweenEscalates = this.ResetAppPoolResponderWaitInterval;
				responderDefinition.WaitIntervalSeconds = this.ResetAppPoolResponderWaitInterval;
				responderDefinition.RecurrenceIntervalSeconds = this.ResponderRecurrenceInterval;
				base.Broker.AddWorkDefinition<ResponderDefinition>(responderDefinition, base.TraceContext);
			}
			if (this.IsAlertResponderEnabled)
			{
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CalendarSharingTracer, base.TraceContext, "OWACalendarAppPoolDiscovery.SetupOWACalendarAppPoolMonitoring: Creating {0} for this server", "OWACalendarSelfTestEscalate", null, "SetupOWACalendarAppPoolMonitoring", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\CalendarSharing\\OWACalendarAppPoolDiscovery.cs", 288);
				ResponderDefinition responderDefinition2 = EscalateResponder.CreateDefinition("OWACalendarSelfTestEscalate", ExchangeComponent.Calendaring.Name, "OWACalendarSelfTestMonitor", text, Environment.MachineName, ServiceHealthStatus.Unrecoverable, ExchangeComponent.Calendaring.EscalationTeam, Strings.OWACalendarAppPoolEscalationSubject(Environment.MachineName), Strings.OWACalendarAppPoolEscalationBody, true, NotificationServiceClass.Urgent, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
				responderDefinition2.WaitIntervalSeconds = this.AlertResponderWaitInterval;
				responderDefinition2.MinimumSecondsBetweenEscalates = this.AlertResponderWaitInterval;
				responderDefinition2.RecurrenceIntervalSeconds = this.ResponderRecurrenceInterval;
				responderDefinition2.NotificationServiceClass = NotificationServiceClass.Scheduled;
				base.Broker.AddWorkDefinition<ResponderDefinition>(responderDefinition2, base.TraceContext);
			}
		}

		// Token: 0x04000204 RID: 516
		private const string ProbeName = "OWACalendarSelfTestProbe";

		// Token: 0x04000205 RID: 517
		private const string MonitorName = "OWACalendarSelfTestMonitor";

		// Token: 0x04000206 RID: 518
		private const string EscalateResponderName = "OWACalendarSelfTestEscalate";

		// Token: 0x04000207 RID: 519
		private const string RecycleAppPoolResponderName = "OWACalendarSelfTestRecycleAppPool";

		// Token: 0x04000208 RID: 520
		private const string OWACalendarAppPoolName = "OWACalendarAppPool";

		// Token: 0x04000209 RID: 521
		public static readonly string DefaultOWACalendarAppPoolUrl = "http://localhost:81/owa/calendar/ping.owa";

		// Token: 0x0400020A RID: 522
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x0400020B RID: 523
		private static readonly TimeSpan DefaultProbeRecurrenceInterval = TimeSpan.FromMinutes(3.0);

		// Token: 0x0400020C RID: 524
		private static readonly TimeSpan DefaultMonitorRecurrenceInterval = TimeSpan.FromMinutes(0.0);

		// Token: 0x0400020D RID: 525
		private static readonly TimeSpan DefaultMonitorInterval = TimeSpan.FromMinutes(12.0);

		// Token: 0x0400020E RID: 526
		private static readonly TimeSpan DefaultResponderRecurrenceInterval = TimeSpan.FromMinutes(3.0);

		// Token: 0x0400020F RID: 527
		private static readonly TimeSpan DefaultAlertResponderWaitInterval = TimeSpan.FromHours(2.0);

		// Token: 0x04000210 RID: 528
		private static readonly TimeSpan DefaultResetAppPoolResponderWaitInterval = TimeSpan.FromHours(2.0);

		// Token: 0x04000211 RID: 529
		private static readonly TimeSpan DefaultProbeTimeout = TimeSpan.FromMinutes(2.0);

		// Token: 0x04000212 RID: 530
		private static readonly TimeSpan DefaultUnrecoverableTransitionSpan = TimeSpan.FromMinutes(15.0);

		// Token: 0x04000213 RID: 531
		private static readonly int DefaultFailureCount = 4;

		// Token: 0x04000214 RID: 532
		private Breadcrumbs breadcrumbs;

		// Token: 0x04000215 RID: 533
		private static readonly string OWACalendarAppPoolProbeTypeName = typeof(OWACalendarAppPoolProbe).FullName;
	}
}
