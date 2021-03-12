using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Probes;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring
{
	// Token: 0x020001D1 RID: 465
	public sealed class ObserverMaintenance : MaintenanceWorkItem
	{
		// Token: 0x06000D21 RID: 3361 RVA: 0x00055E8C File Offset: 0x0005408C
		protected override void DoWork(CancellationToken cancellationToken)
		{
			WTFDiagnostics.TraceFunction(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "Start discovery for Observer Maintenance.", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\HM\\ObserverMaintenance.cs", 137);
			if (FfoLocalEndpointManager.IsForefrontForOfficeDatacenter && !FfoLocalEndpointManager.IsHubTransportRoleInstalled && !FfoLocalEndpointManager.IsFrontendTransportRoleInstalled)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "LAM: HealthManagerObserverMaintenance for non Exchange boxes must be disabled until PS 3063229 is fixed", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\HM\\ObserverMaintenance.cs", 145);
				return;
			}
			AttributeHelper attributeHelper = new AttributeHelper(base.Definition);
			TimeSpan timeSpan = attributeHelper.GetTimeSpan("ProbeRecurrenceInterval", false, ObserverMaintenance.DefaultHeartbeatProbeRecurrenceInterval, null, null);
			TimeSpan timeSpan2 = attributeHelper.GetTimeSpan("MissingResponderResultLimit", false, ObserverMaintenance.DefaultMissingResponderResultLimit, null, null);
			int @int = attributeHelper.GetInt("ConsecutiveProbeFailureCount", false, ObserverMaintenance.DefaultConsecutiveProbeFailureCount, null, null);
			bool @bool = attributeHelper.GetBool("EscalateResponderEnabled", false, ObserverMaintenance.DefaultIsEscalateResponderEnabled);
			bool bool2 = attributeHelper.GetBool("RestartResponderEnabled", false, ObserverMaintenance.DefaultIsRestartResponderEnabled);
			WTFDiagnostics.TraceInformation<TimeSpan, TimeSpan, int, bool, bool>(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "Probe Recurrence Interval = '{0}', Missing Responder Result Limit = '{1}', Consecutive Probe Failure Count = '{2}', Is Escalate Responder Enabled = '{3}', Is Restart Responder Enabled = '{4}'.", timeSpan, timeSpan2, @int, @bool, bool2, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\HM\\ObserverMaintenance.cs", 161);
			Component escalationComponent = ExchangeComponent.Monitoring;
			if (DatacenterRegistry.IsForefrontForOffice())
			{
				escalationComponent = ExchangeComponent.FfoMonitoring;
			}
			List<string> list = new List<string>(MonitoringServerManager.GetAllSubjects());
			List<string> allHistoricalSubjects = LocalEndpointManager.Instance.SubjectListEndpoint.AllHistoricalSubjects;
			foreach (string text in list)
			{
				if (!allHistoricalSubjects.Contains(text))
				{
					allHistoricalSubjects.Add(text);
					this.CreateWorkItemsForSubject(text, timeSpan, timeSpan2, @int, @bool, bool2, escalationComponent);
				}
			}
			if (allHistoricalSubjects.Count - list.Count > ObserverMaintenance.MaxAllowedZombieSubjects)
			{
				WTFDiagnostics.TraceInformation<int>(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "We have dropped {0} subjects since the last time the Health Manager started. Requesting a restart to clear obsolete probe definitions.", allHistoricalSubjects.Count - list.Count, null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\HM\\ObserverMaintenance.cs", 206);
				LocalEndpointManager.Instance.SubjectListEndpoint.RestartOnChange = true;
			}
		}

		// Token: 0x06000D22 RID: 3362 RVA: 0x000560B8 File Offset: 0x000542B8
		private void CreateWorkItemsForSubject(string subject, TimeSpan probeRecurrenceInterval, TimeSpan missingResponderResultLimit, int consecutiveProbeFailureCount, bool isEscalateResponderEnabled, bool isRestartResponderEnabled, Component escalationComponent)
		{
			WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "Adding observer heartbeat probe for subject {0}.", subject, null, "CreateWorkItemsForSubject", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\HM\\ObserverMaintenance.cs", 224);
			base.Broker.AddWorkDefinition<ProbeDefinition>(ObserverHeartbeatProbe.CreateDefinition("HealthManagerObserverProbe", subject, probeRecurrenceInterval, missingResponderResultLimit, base.TraceContext), base.TraceContext);
			WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "Adding observer heartbeat monitor for subject {0}.", subject, null, "CreateWorkItemsForSubject", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\HM\\ObserverMaintenance.cs", 235);
			MonitorDefinition monitorDefinition = OverallConsecutiveProbeFailuresMonitor.CreateDefinition("HealthManagerObserverMonitor", string.Format("{0}/{1}", "HealthManagerObserverProbe", subject), base.Definition.ServiceName, ExchangeComponent.RemoteMonitoring, consecutiveProbeFailureCount, true, 300);
			monitorDefinition.TargetResource = subject;
			monitorDefinition.MonitoringIntervalSeconds = (int)(probeRecurrenceInterval.TotalSeconds * (double)(consecutiveProbeFailureCount + 1));
			monitorDefinition.RecurrenceIntervalSeconds = 0;
			monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Degraded, 0),
				new MonitorStateTransition(ServiceHealthStatus.Unhealthy, (int)TimeSpan.FromMinutes(15.0).TotalSeconds),
				new MonitorStateTransition(ServiceHealthStatus.Unrecoverable, (int)TimeSpan.FromMinutes(30.0).TotalSeconds)
			};
			if (LocalEndpointManager.IsDataCenter && !string.IsNullOrWhiteSpace(subject))
			{
				string text = subject.Substring(subject.IndexOf('.') + 1);
				if (text.Length >= 2)
				{
					monitorDefinition.TargetScopes = text.Substring(0, 2);
				}
			}
			monitorDefinition.ServicePriority = 0;
			monitorDefinition.ScenarioDescription = "Validate Monitoring health is not impacted by any issues";
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
			if (isRestartResponderEnabled)
			{
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "Adding observer server service start responder for subject {0}.", subject, null, "CreateWorkItemsForSubject", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\HM\\ObserverMaintenance.cs", 281);
				ResponderDefinition definition = RemoteRestartServiceResponder.CreateDefinition("ServerServiceObserverStart", "HealthManagerObserverMonitor", subject, "LANMANServer", ServiceHealthStatus.Degraded, ExchangeComponent.RemoteMonitoring, (int)probeRecurrenceInterval.TotalSeconds, true);
				base.Broker.AddWorkDefinition<ResponderDefinition>(definition, base.TraceContext);
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "Adding observer heartbeat restart responder for subject {0}.", subject, null, "CreateWorkItemsForSubject", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\HM\\ObserverMaintenance.cs", 295);
				ResponderDefinition definition2 = RemoteRestartServiceResponder.CreateDefinition("HealthManagerObserverRestart", "HealthManagerObserverMonitor", subject, "MSExchangeHM", ServiceHealthStatus.Degraded, ExchangeComponent.RemoteMonitoring, (int)probeRecurrenceInterval.TotalSeconds * consecutiveProbeFailureCount, false);
				base.Broker.AddWorkDefinition<ResponderDefinition>(definition2, base.TraceContext);
			}
			else
			{
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "Skipping adding observer heartbeat restart responder for subject {0}.", subject, null, "CreateWorkItemsForSubject", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\HM\\ObserverMaintenance.cs", 310);
			}
			if (isEscalateResponderEnabled)
			{
				WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "Adding observer heartbeat escalate responder for subject {0}.", subject, null, "CreateWorkItemsForSubject", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\HM\\ObserverMaintenance.cs", 315);
				string localComputerFqdn = NativeHelpers.GetLocalComputerFqdn(true);
				ResponderDefinition responderDefinition = EscalateResponder.CreateDefinition("HealthManagerObserverEscalate", base.Definition.ServiceName, "HealthManagerObserverMonitor", string.Format("{0}/{1}", "HealthManagerObserverMonitor", subject), "MSExchangeHM", ServiceHealthStatus.Unrecoverable, escalationComponent.EscalationTeam, Strings.ObserverHeartbeatEscalateResponderSubject(subject), Strings.ObserverHeartbeatEscalateResponderMessage(subject, localComputerFqdn), false, NotificationServiceClass.Urgent, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
				responderDefinition.RecurrenceIntervalSeconds = 0;
				responderDefinition.TargetGroup = RecoveryActionHelper.GetShortServerName(subject);
				base.Broker.AddWorkDefinition<ResponderDefinition>(responderDefinition, base.TraceContext);
				return;
			}
			WTFDiagnostics.TraceInformation<string>(ExTraceGlobals.CommonComponentsTracer, base.TraceContext, "Skipping adding observer heartbeat escalate responder for subject {0}.", subject, null, "CreateWorkItemsForSubject", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\HM\\ObserverMaintenance.cs", 343);
		}

		// Token: 0x040009AA RID: 2474
		internal const string MissingResponderResultLimitName = "MissingResponderResultLimit";

		// Token: 0x040009AB RID: 2475
		internal const string ObserverHeartbeatProbeName = "HealthManagerObserverProbe";

		// Token: 0x040009AC RID: 2476
		private const string ObserverHeartbeatMonitorName = "HealthManagerObserverMonitor";

		// Token: 0x040009AD RID: 2477
		private const string ObserverHeartbeatRestartResponderName = "HealthManagerObserverRestart";

		// Token: 0x040009AE RID: 2478
		private const string ObserverServerStartResponderName = "ServerServiceObserverStart";

		// Token: 0x040009AF RID: 2479
		private const string ObserverHeartbeatRebootResponderName = "HealthManagerObserverReboot";

		// Token: 0x040009B0 RID: 2480
		private const string ObserverHeartbeatEscalateResponderName = "HealthManagerObserverEscalate";

		// Token: 0x040009B1 RID: 2481
		private const string ProbeRecurrenceIntervalName = "ProbeRecurrenceInterval";

		// Token: 0x040009B2 RID: 2482
		private const string ConsecutiveProbeFailureCountName = "ConsecutiveProbeFailureCount";

		// Token: 0x040009B3 RID: 2483
		private const string RestartResponderEnabledName = "RestartResponderEnabled";

		// Token: 0x040009B4 RID: 2484
		private const string EscalateResponderEnabledName = "EscalateResponderEnabled";

		// Token: 0x040009B5 RID: 2485
		private const string ServerServiceName = "LANMANServer";

		// Token: 0x040009B6 RID: 2486
		private const string HealthManagerServiceName = "MSExchangeHM";

		// Token: 0x040009B7 RID: 2487
		private static readonly int MaxAllowedZombieSubjects = Settings.MaxZombieSubjects;

		// Token: 0x040009B8 RID: 2488
		private static readonly TimeSpan DefaultHeartbeatProbeRecurrenceInterval = TimeSpan.FromMinutes(5.0);

		// Token: 0x040009B9 RID: 2489
		private static readonly TimeSpan DefaultMissingResponderResultLimit = TimeSpan.FromMinutes(15.0);

		// Token: 0x040009BA RID: 2490
		private static readonly int DefaultConsecutiveProbeFailureCount = 3;

		// Token: 0x040009BB RID: 2491
		private static readonly bool DefaultIsEscalateResponderEnabled = true;

		// Token: 0x040009BC RID: 2492
		private static readonly bool DefaultIsRestartResponderEnabled = true;
	}
}
