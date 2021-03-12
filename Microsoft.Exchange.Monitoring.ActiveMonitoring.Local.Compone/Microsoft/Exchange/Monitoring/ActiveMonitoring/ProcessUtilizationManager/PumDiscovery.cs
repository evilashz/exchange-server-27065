using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.ProcessUtilizationManager
{
	// Token: 0x02000296 RID: 662
	public sealed class PumDiscovery : MaintenanceWorkItem
	{
		// Token: 0x060012D2 RID: 4818 RVA: 0x00082F5C File Offset: 0x0008115C
		public PumDiscovery()
		{
			this.traceContext = new TracingContext();
			this.jobObjectNames = new List<string>
			{
				"DefaultAppPool"
			};
			if (ExEnvironment.IsTest)
			{
				this.jobObjectNames.Add("TestingThresholdBreachEventJobObject");
			}
		}

		// Token: 0x1700038B RID: 907
		// (get) Token: 0x060012D3 RID: 4819 RVA: 0x00082FAC File Offset: 0x000811AC
		private static bool IsWindows2012OrHigher
		{
			get
			{
				Version version = Environment.OSVersion.Version;
				return version.Major == 6 && version.Minor >= 2;
			}
		}

		// Token: 0x060012D4 RID: 4820 RVA: 0x00082FDC File Offset: 0x000811DC
		protected override void DoWork(CancellationToken cancellationToken)
		{
			if (PumDiscovery.IsWindows2012OrHigher && LocalEndpointManager.Instance.ExchangeServerRoleEndpoint.IsMailboxRoleInstalled)
			{
				WTFDiagnostics.TraceFunction(ExTraceGlobals.PUMTracer, this.traceContext, "PumDiscovery.DoWork", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\ProcessUtilizationManager\\PumDiscovery.cs", 120);
				this.CreatePumServiceContext();
				this.CreateJobObjectMonitorsAndResponders();
			}
		}

		// Token: 0x060012D5 RID: 4821 RVA: 0x00083030 File Offset: 0x00081230
		private void CreatePumServiceContext()
		{
			bool enabled = bool.Parse(base.Definition.Attributes["PumServiceRunningEnabled"]);
			int recurrenceIntervalSeconds = int.Parse(base.Definition.Attributes["PumServiceRunningProbeRecurrenceIntervalSeconds"]);
			ProbeDefinition definition = this.CreateProbeDefinition("PumServiceRunningProbe", typeof(GenericServiceProbe), "MSExchangeProcessUtilizationManager", recurrenceIntervalSeconds, enabled);
			base.Broker.AddWorkDefinition<ProbeDefinition>(definition, this.traceContext);
			int num = int.Parse(base.Definition.Attributes["PumServiceRunningMonitorRecurrenceIntervalSeconds"]);
			int monitoringInterval = int.Parse(base.Definition.Attributes["PumServiceRunningMonitorMonitoringIntervalSeconds"]);
			int numberOfFailures = int.Parse(base.Definition.Attributes["PumServiceRunningMonitorMonitoringThreshold"]);
			int transitionTimeoutSeconds = int.Parse(base.Definition.Attributes["PumServiceRunningMonitorUnhealthyStateTransitionTimeOut"]);
			MonitorDefinition monitorDefinition = OverallXFailuresMonitor.CreateDefinition("PumServiceRunningMonitor", "PumServiceRunningProbe/MSExchangeProcessUtilizationManager", ExchangeComponent.Pum.Name, ExchangeComponent.Pum, monitoringInterval, num, numberOfFailures, enabled);
			monitorDefinition.TargetResource = "MSExchangeProcessUtilizationManager";
			monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Degraded, 0),
				new MonitorStateTransition(ServiceHealthStatus.Unhealthy, transitionTimeoutSeconds)
			};
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
			int num2 = int.Parse(base.Definition.Attributes["PumServiceRestartTimeoutSeconds"]);
			int waitIntervalSeconds = int.Parse(base.Definition.Attributes["PumServiceRestartResponderThrottleSeconds"]);
			ResponderDefinition responderDefinition = RestartServiceResponder.CreateDefinition("PumServiceRestartResponder", monitorDefinition.Name, "MSExchangeProcessUtilizationManager", ServiceHealthStatus.Degraded, 15, 120, 0, false, DumpMode.None, null, 15.0, 0, "Exchange", null, true, true, null, false);
			WTFDiagnostics.TraceDebug<string, string>(ExTraceGlobals.PUMTracer, this.traceContext, "PumDiscovery.DoWork: Created {0} for {1}", "PumServiceRestartResponder", "MSExchangeProcessUtilizationManager", null, "CreatePumServiceContext", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\ProcessUtilizationManager\\PumDiscovery.cs", 197);
			responderDefinition.RecurrenceIntervalSeconds = num;
			responderDefinition.ServiceName = ExchangeComponent.Pum.Name;
			responderDefinition.WaitIntervalSeconds = waitIntervalSeconds;
			responderDefinition.Attributes["ServiceStartTimeout"] = TimeSpan.FromSeconds((double)num2).ToString();
			base.Broker.AddWorkDefinition<ResponderDefinition>(responderDefinition, this.traceContext);
			ResponderDefinition responderDefinition2 = this.CreateEscalateResponderDefinition("PumServiceRunningMonitor", ExchangeComponent.Pum, "MSExchangeProcessUtilizationManager", Strings.EscalationSubjectUnhealthy, Strings.EscalationMessageFailuresUnhealthy(Strings.PumServiceNotRunningEscalationMessage), enabled, NotificationServiceClass.Urgent);
			responderDefinition2.TargetHealthState = ServiceHealthStatus.Unhealthy;
			responderDefinition2.WaitIntervalSeconds = int.Parse(base.Definition.Attributes["PumServiceEscalateResponderThrottleSeconds"]);
			base.Broker.AddWorkDefinition<ResponderDefinition>(responderDefinition2, this.traceContext);
		}

		// Token: 0x060012D6 RID: 4822 RVA: 0x000832F0 File Offset: 0x000814F0
		private void CreateJobObjectMonitorsAndResponders()
		{
			Array values = Enum.GetValues(typeof(PumDiscovery.ResourceTypes));
			foreach (string text in this.jobObjectNames)
			{
				foreach (object obj in values)
				{
					PumDiscovery.ResourceTypes resourceTypes = (PumDiscovery.ResourceTypes)obj;
					Component pum = ExchangeComponent.Pum;
					int recurrenceInterval = int.Parse(base.Definition.Attributes["JobObjectMonitorRecurrenceIntervalSeconds"]);
					int monitoringInterval = int.Parse(base.Definition.Attributes["JobObjectMonitorMonitoringIntervalSeconds"]);
					int numberOfFailures = int.Parse(base.Definition.Attributes["JobObjectMonitorMonitoringThreshold"]);
					MonitorDefinition monitorDefinition = OverallXFailuresMonitor.CreateDefinition(string.Join("_", new string[]
					{
						resourceTypes.ToString(),
						text
					}), NotificationItem.GenerateResultName(pum.Name, resourceTypes.ToString(), text), pum.Name, pum, monitoringInterval, recurrenceInterval, numberOfFailures, true);
					monitorDefinition.TargetResource = text;
					monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
					{
						new MonitorStateTransition(ServiceHealthStatus.Unhealthy, 0)
					};
					base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, this.traceContext);
					string escalationSubject = string.Empty;
					string escalationMessage = string.Empty;
					NotificationServiceClass notificationType = NotificationServiceClass.UrgentInTraining;
					PumDiscovery.ResourceTypes resourceTypes2 = resourceTypes;
					if (resourceTypes2 == PumDiscovery.ResourceTypes.Cpu)
					{
						escalationSubject = Strings.JobobjectCpuExceededThresholdSubject(text);
						escalationMessage = Strings.JobobjectCpuExceededThresholdMessage(text);
					}
					ResponderDefinition responderDefinition = this.CreateEscalateResponderDefinition(monitorDefinition.Name, pum, monitorDefinition.TargetResource, escalationSubject, escalationMessage, true, notificationType);
					responderDefinition.TargetHealthState = ServiceHealthStatus.Unhealthy;
					responderDefinition.WaitIntervalSeconds = int.Parse(base.Definition.Attributes["JobObjectEscalateResponderThrottleSeconds"]);
					base.Broker.AddWorkDefinition<ResponderDefinition>(responderDefinition, this.traceContext);
				}
			}
		}

		// Token: 0x060012D7 RID: 4823 RVA: 0x00083528 File Offset: 0x00081728
		private ProbeDefinition CreateProbeDefinition(string probeName, Type probeType, string targetResource, int recurrenceIntervalSeconds, bool enabled)
		{
			ProbeDefinition probeDefinition = new ProbeDefinition();
			probeDefinition.AssemblyPath = probeType.Assembly.Location;
			probeDefinition.TypeName = probeType.FullName;
			probeDefinition.Name = probeName;
			probeDefinition.RecurrenceIntervalSeconds = recurrenceIntervalSeconds;
			probeDefinition.TimeoutSeconds = recurrenceIntervalSeconds;
			probeDefinition.MaxRetryAttempts = 3;
			probeDefinition.TargetResource = targetResource;
			probeDefinition.ServiceName = ExchangeComponent.Pum.Name;
			probeDefinition.Enabled = enabled;
			WTFDiagnostics.TraceInformation<string, string>(ExTraceGlobals.PUMTracer, this.traceContext, "PumDiscovery.CreateProbeDefinition: Created ProbeDefinition '{0}' for '{1}'.", probeName, targetResource, null, "CreateProbeDefinition", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\ProcessUtilizationManager\\PumDiscovery.cs", 328);
			return probeDefinition;
		}

		// Token: 0x060012D8 RID: 4824 RVA: 0x000835C0 File Offset: 0x000817C0
		private ResponderDefinition CreateEscalateResponderDefinition(string monitorName, Component component, string targetResource, string escalationSubject, string escalationMessage, bool enabled, NotificationServiceClass notificationType)
		{
			string text = monitorName + "EscalateResponder";
			string alertMask = string.IsNullOrEmpty(targetResource) ? monitorName : (monitorName + "/" + targetResource);
			ResponderDefinition result = EscalateResponder.CreateDefinition(text, component.Name, monitorName, alertMask, targetResource, ServiceHealthStatus.None, component.Service, component.EscalationTeam, escalationSubject, escalationMessage, enabled, notificationType, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
			WTFDiagnostics.TraceInformation<string, string>(ExTraceGlobals.PUMTracer, this.traceContext, "PumDiscovery.CreateEscalateResponderDefinition: Created Escalate ResponderDefinition '{0}' for '{1}'", text, targetResource, null, "CreateEscalateResponderDefinition", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\ProcessUtilizationManager\\PumDiscovery.cs", 373);
			return result;
		}

		// Token: 0x04000E24 RID: 3620
		internal const string ServiceRunningProbeName = "PumServiceRunningProbe";

		// Token: 0x04000E25 RID: 3621
		private const int MaxRetryAttempt = 3;

		// Token: 0x04000E26 RID: 3622
		private const string ServiceName = "MSExchangeProcessUtilizationManager";

		// Token: 0x04000E27 RID: 3623
		private const string ServiceRunningMonitorName = "PumServiceRunningMonitor";

		// Token: 0x04000E28 RID: 3624
		private const string ServiceRestartResponderName = "PumServiceRestartResponder";

		// Token: 0x04000E29 RID: 3625
		private static readonly Type restartServiceResponderType = typeof(RestartServiceResponder);

		// Token: 0x04000E2A RID: 3626
		private static readonly Type OverallXFailuresMonitorType = typeof(OverallXFailuresMonitor);

		// Token: 0x04000E2B RID: 3627
		private readonly TracingContext traceContext;

		// Token: 0x04000E2C RID: 3628
		private readonly List<string> jobObjectNames;

		// Token: 0x02000297 RID: 663
		public enum ResourceTypes
		{
			// Token: 0x04000E2E RID: 3630
			Cpu
		}
	}
}
