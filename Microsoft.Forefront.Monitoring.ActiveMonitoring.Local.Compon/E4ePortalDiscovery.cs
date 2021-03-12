using System;
using System.Reflection;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Forefront.Monitoring.ActiveMonitoring
{
	// Token: 0x0200008B RID: 139
	internal sealed class E4ePortalDiscovery : MaintenanceWorkItem
	{
		// Token: 0x060003C6 RID: 966 RVA: 0x000165C0 File Offset: 0x000147C0
		protected override void DoWork(CancellationToken cancellationToken)
		{
			try
			{
				this.probeRecurrenceInterval = this.ReadAttribute("ProbeRecurrenceInterval", TimeSpan.Zero);
				this.probeTimeout = this.ReadAttribute("ProbeTimeout", TimeSpan.Zero);
				this.monitoringFailureCount = this.ReadAttribute("MonitoringFailureCount", 0);
				this.monitoringInterval = this.ReadAttribute("MonitoringInterval", TimeSpan.Zero);
				this.monitorRecurrenceInterval = this.ReadAttribute("MonitorRecurrenceInterval", TimeSpan.Zero);
				this.responderRecurrenceInterval = this.ReadAttribute("ResponderRecurrenceInterval", TimeSpan.Zero);
				LocalEndpointManager instance = LocalEndpointManager.Instance;
				this.breadcrumbs = new Breadcrumbs(1024, base.TraceContext);
				if (!LocalEndpointManager.IsDataCenter)
				{
					this.breadcrumbs.Drop("E4ePortalDiscovery.DoWork: Skip creating the probe outside datacenter");
				}
				else if (!instance.ExchangeServerRoleEndpoint.IsMailboxRoleInstalled)
				{
					this.breadcrumbs.Drop("E4EAppPoolDiscovery.DoWork: Skip creating the probe for non-MBX server");
				}
				else if (!VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).E4E.E4E.Enabled)
				{
					this.breadcrumbs.Drop("E4ePortalDiscovery.DoWork: Skip creating the probe if the feature is disabled in flighting");
				}
				else
				{
					WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.E4ETracer, base.TraceContext, "E4ePortalDiscovery.SetupE4ePortalProbe: Creating E4ePortalProbe for server {0}", Environment.MachineName, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Encryption\\E4ePortalDiscovery.cs", 150);
					this.SetupE4ePortalProbe(base.TraceContext);
					WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.E4ETracer, base.TraceContext, "E4ePortalDiscovery.SetupE4ePortalProbe: Created E4ePortalProbe for server {0}", Environment.MachineName, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Encryption\\E4ePortalDiscovery.cs", 158);
				}
			}
			finally
			{
				this.ReportResult();
			}
		}

		// Token: 0x060003C7 RID: 967 RVA: 0x00016760 File Offset: 0x00014960
		private void ReportResult()
		{
			string text = this.breadcrumbs.ToString();
			base.Result.StateAttribute5 = text;
			WTFDiagnostics.TraceInformation(ExTraceGlobals.E4ETracer, base.TraceContext, text, null, "ReportResult", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Encryption\\E4ePortalDiscovery.cs", 179);
		}

		// Token: 0x060003C8 RID: 968 RVA: 0x000167A8 File Offset: 0x000149A8
		private void SetupE4ePortalProbe(TracingContext traceContext)
		{
			ProbeDefinition probeDefinition = new ProbeDefinition();
			probeDefinition.AssemblyPath = E4ePortalDiscovery.AssemblyPath;
			probeDefinition.TypeName = E4ePortalDiscovery.ProbeTypeName;
			probeDefinition.Name = "E4ePortalProbe";
			probeDefinition.TargetResource = Environment.MachineName;
			probeDefinition.RecurrenceIntervalSeconds = (int)this.probeRecurrenceInterval.TotalSeconds;
			probeDefinition.TimeoutSeconds = (int)this.probeTimeout.TotalSeconds;
			probeDefinition.MaxRetryAttempts = 0;
			probeDefinition.ServiceName = ExchangeComponent.E4E.Name;
			if (ExEnvironment.IsSdfDomain)
			{
				probeDefinition.Endpoint = "sdfpilot.outlook.com";
			}
			else
			{
				probeDefinition.Endpoint = "outlook.office365.com";
			}
			base.Broker.AddWorkDefinition<ProbeDefinition>(probeDefinition, base.TraceContext).Wait();
			WTFDiagnostics.TraceInformation(ExTraceGlobals.E4ETracer, base.TraceContext, "E4ePortalDiscovery.SetupE4ePortalProbe: E4ePortalProbe created.", null, "SetupE4ePortalProbe", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Encryption\\E4ePortalDiscovery.cs", 215);
			MonitorDefinition monitorDefinition = OverallConsecutiveProbeFailuresMonitor.CreateDefinition("E4ePortalMonitor", probeDefinition.ConstructWorkItemResultName(), ExchangeComponent.E4E.Name, ExchangeComponent.E4E, this.monitoringFailureCount, true, 300);
			monitorDefinition.MonitoringIntervalSeconds = (int)this.monitoringInterval.TotalSeconds;
			monitorDefinition.RecurrenceIntervalSeconds = (int)this.monitorRecurrenceInterval.TotalSeconds;
			monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Unhealthy, TimeSpan.Zero)
			};
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext).Wait();
			WTFDiagnostics.TraceInformation(ExTraceGlobals.E4ETracer, base.TraceContext, "E4ePortalDiscovery.SetupE4ePortalProbe: OverallConsecutiveProbeFailuresMonitor created.", null, "SetupE4ePortalProbe", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Encryption\\E4ePortalDiscovery.cs", 239);
			ResponderDefinition responderDefinition = EscalateResponder.CreateDefinition("E4ePortalResponder", ExchangeComponent.E4E.Name, "E4ePortalMonitor", monitorDefinition.ConstructWorkItemResultName(), Environment.MachineName, ServiceHealthStatus.Unhealthy, ExchangeComponent.E4E.EscalationTeam, E4ePortalDiscovery.EscalationSubject, E4ePortalDiscovery.EscalationBody, true, NotificationServiceClass.UrgentInTraining, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
			responderDefinition.RecurrenceIntervalSeconds = (int)this.responderRecurrenceInterval.TotalSeconds;
			base.Broker.AddWorkDefinition<ResponderDefinition>(responderDefinition, base.TraceContext).Wait();
			WTFDiagnostics.TraceInformation(ExTraceGlobals.E4ETracer, base.TraceContext, "E4ePortalDiscovery.SetupE4ePortalProbe: EscalateResponder created.", null, "SetupE4ePortalProbe", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ForefrontActiveMonitoring\\Components\\Encryption\\E4ePortalDiscovery.cs", 262);
		}

		// Token: 0x04000234 RID: 564
		private const string ProbeName = "E4ePortalProbe";

		// Token: 0x04000235 RID: 565
		private const string MonitorName = "E4ePortalMonitor";

		// Token: 0x04000236 RID: 566
		private const string ResponderName = "E4ePortalResponder";

		// Token: 0x04000237 RID: 567
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x04000238 RID: 568
		private static readonly string ProbeTypeName = typeof(E4ePortalProbe).FullName;

		// Token: 0x04000239 RID: 569
		private static readonly string EscalationSubject = string.Format("E4ePortalProbe failed on: {0}", Environment.MachineName);

		// Token: 0x0400023A RID: 570
		private static readonly string EscalationBody = string.Format("The E4ePortalProbe failed with the following error. <br> Start time: {{Monitor.FirstAlertObservedTime}}<br>Failure Count: {{Monitor.MonitoringThreshold}}<br>Exception: <pre>{{Probe.Exception}}</pre><br>Error Type: {{Probe.Error}}<br>Trace Info: {{Probe.ExecutionContext}}<br>Additional Trace Info: {{Probe.StateAttribute1}}<br>Failed Verify String (or Metadata): {{Probe.StateAttribute2}}<br>Failed Verify Substring: {{Probe.StateAttribute3}}<br>Monitoring MDB Count: {{Probe.StateAttribute16}}<br>Monitoring Tenant Id: {{Probe.StateAttribute21}}<br>Message From/To: {{Probe.StateAttribute4}}<br>Message Attachments: {{Probe.StateAttribute15}}<br>Message Attachment Count: {{Probe.StateAttribute6}}<br>Message Size (Plaintext): {{Probe.StateAttribute8}}<br>Message Size (Encrypted): {{Probe.StateAttribute9}}<br>Message.html RPMSG Length: {{Probe.StateAttribute7}}<br>Portal Response StatusCode: {{Probe.StateAttribute10}}<br>Portal X-CalculatedBETarget: {{Probe.StateAttribute11}}<br>Portal X-FEServer: {{Probe.StateAttribute12}}<br>Portal X-BEServer: {{Probe.StateAttribute13}}<br>Portal X-DiagInfo: {{Probe.StateAttribute14}}<br>Portal ItemId::ItemId::OTPMessageId: {{Probe.StateAttribute5}}<br>Portal Request Cookie: {{Probe.StateAttribute23}}<br>Portal Response Body: {{Probe.StateAttribute24}}<br>Portal Redirect Location: {{Probe.StateAttribute25}}<br>OTP Portal Response Body: {{Probe.StateAttribute22}}<br>", new object[0]);

		// Token: 0x0400023B RID: 571
		private static readonly Type E4ePortalProbeType = typeof(E4ePortalProbe);

		// Token: 0x0400023C RID: 572
		private static readonly Type E4ePortalMonitorType = typeof(OverallConsecutiveProbeFailuresMonitor);

		// Token: 0x0400023D RID: 573
		private static readonly Type E4ePortalResponderType = typeof(EscalateResponder);

		// Token: 0x0400023E RID: 574
		private TimeSpan probeRecurrenceInterval;

		// Token: 0x0400023F RID: 575
		private TimeSpan probeTimeout;

		// Token: 0x04000240 RID: 576
		private int monitoringFailureCount;

		// Token: 0x04000241 RID: 577
		private TimeSpan monitoringInterval;

		// Token: 0x04000242 RID: 578
		private TimeSpan monitorRecurrenceInterval;

		// Token: 0x04000243 RID: 579
		private TimeSpan responderRecurrenceInterval;

		// Token: 0x04000244 RID: 580
		private Breadcrumbs breadcrumbs;
	}
}
