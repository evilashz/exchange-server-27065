using System;
using System.Configuration;
using System.Reflection;
using System.ServiceProcess;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.FEP
{
	// Token: 0x0200017F RID: 383
	public sealed class FEPDiscovery : MaintenanceWorkItem
	{
		// Token: 0x06000B25 RID: 2853 RVA: 0x0004706C File Offset: 0x0004526C
		protected override void DoWork(CancellationToken cancellationToken)
		{
			if (!LocalEndpointManager.IsDataCenter)
			{
				return;
			}
			using (ServiceController serviceController = new ServiceController("MsMpSvc"))
			{
				if (serviceController.Status == ServiceControllerStatus.Running)
				{
					WTFDiagnostics.TraceFunction(ExTraceGlobals.FEPTracer, base.TraceContext, "FEPDiscovery.DoWork", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\FEP\\FEPDiscovery.cs", 85);
					this.CreateFEPServiceContext();
				}
			}
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x000470DC File Offset: 0x000452DC
		private void CreateFEPServiceContext()
		{
			bool enabled = bool.Parse(base.Definition.Attributes["FEPServiceRunningEnabled"]);
			int recurrenceIntervalSeconds = int.Parse(base.Definition.Attributes["FEPServiceRunningProbeRecurrenceIntervalSeconds"]);
			ProbeDefinition definition = this.CreateProbeDefinition("FEPServiceRunningProbe", typeof(GenericServiceProbe), "MsMpSvc", recurrenceIntervalSeconds, enabled);
			base.Broker.AddWorkDefinition<ProbeDefinition>(definition, base.TraceContext);
			int num = int.Parse(base.Definition.Attributes["FEPServiceRunningMonitorRecurrenceIntervalSeconds"]);
			int monitoringInterval = int.Parse(base.Definition.Attributes["FEPServiceRunningMonitorMonitoringIntervalSeconds"]);
			int numberOfFailures = int.Parse(base.Definition.Attributes["FEPServiceRunningMonitorMonitoringThreshold"]);
			int transitionTimeoutSeconds = int.Parse(base.Definition.Attributes["FEPServiceRunningMonitorUnhealthyStateTransitionTimeOut"]);
			MonitorDefinition monitorDefinition = OverallXFailuresMonitor.CreateDefinition("FEPServiceRunningMonitor", "FEPServiceRunningProbe/MsMpSvc", ExchangeComponent.FEP.Name, ExchangeComponent.FEP, monitoringInterval, num, numberOfFailures, enabled);
			WTFDiagnostics.TraceInformation<string, string>(ExTraceGlobals.FEPTracer, base.TraceContext, "FEPDiscovery.CreateMonitorDefinition: Created MonitorDefinition '{0}' for '{1}'", "FEPServiceRunningMonitor", ExchangeComponent.FEP.Name, null, "CreateFEPServiceContext", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\FEP\\FEPDiscovery.cs", 135);
			monitorDefinition.InsufficientSamplesIntervalSeconds = Math.Max(5 * monitorDefinition.RecurrenceIntervalSeconds, Convert.ToInt32(ConfigurationManager.AppSettings["InsufficientSamplesIntervalInSeconds"]));
			monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Degraded, 0),
				new MonitorStateTransition(ServiceHealthStatus.Unhealthy, transitionTimeoutSeconds)
			};
			monitorDefinition.ServicePriority = 2;
			monitorDefinition.ScenarioDescription = "Validate FEP health is not impacted any issues";
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
			int num2 = int.Parse(base.Definition.Attributes["FEPServiceRestartTimeoutSeconds"]);
			int waitIntervalSeconds = int.Parse(base.Definition.Attributes["FEPServiceRestartResponderThrottleSeconds"]);
			ResponderDefinition responderDefinition = this.CreateResponderDefinition("MsMpSvc", FEPDiscovery.RestartServiceResponderType, "FEPServiceRestartResponder", monitorDefinition.Name, monitorDefinition.Name, num, waitIntervalSeconds);
			responderDefinition.TargetHealthState = ServiceHealthStatus.Degraded;
			responderDefinition.Attributes["ServiceStartTimeout"] = TimeSpan.FromSeconds((double)num2).ToString();
			base.Broker.AddWorkDefinition<ResponderDefinition>(responderDefinition, base.TraceContext);
			ResponderDefinition responderDefinition2 = this.CreateEscalateResponderDefinition("FEPServiceRunningMonitor", ExchangeComponent.FEP.Name, "MsMpSvc", Strings.EscalationSubjectUnhealthy, Strings.EscalationMessageFailuresUnhealthy(Strings.FEPServiceNotRunningEscalationMessage), enabled);
			responderDefinition2.TargetHealthState = ServiceHealthStatus.Unhealthy;
			base.Broker.AddWorkDefinition<ResponderDefinition>(responderDefinition2, base.TraceContext);
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x0004738C File Offset: 0x0004558C
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
			probeDefinition.ServiceName = ExchangeComponent.FEP.Name;
			probeDefinition.Enabled = enabled;
			WTFDiagnostics.TraceInformation<string, string>(ExTraceGlobals.FEPTracer, base.TraceContext, "FEPDiscovery.CreateProbeDefinition: Created ProbeDefinition '{0}' for '{1}'.", probeName, targetResource, null, "CreateProbeDefinition", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\FEP\\FEPDiscovery.cs", 223);
			return probeDefinition;
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x00047424 File Offset: 0x00045624
		private ResponderDefinition CreateEscalateResponderDefinition(string monitorName, string serviceName, string targetResource, string escalationSubject, string escalationMessage, bool enabled)
		{
			string text = monitorName + "EscalateResponder";
			string alertMask = string.IsNullOrEmpty(targetResource) ? monitorName : (monitorName + "/" + targetResource);
			ResponderDefinition responderDefinition = EscalateResponder.CreateDefinition(text, serviceName, monitorName, alertMask, targetResource, ServiceHealthStatus.None, "Security", escalationSubject, escalationMessage, true, NotificationServiceClass.Urgent, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
			WTFDiagnostics.TraceInformation<string, string>(ExTraceGlobals.FEPTracer, base.TraceContext, "FEPDiscovery.CreateEscalateResponderDefinition: Created Escalate ResponderDefinition '{0}' for '{1}'", text, targetResource, null, "CreateEscalateResponderDefinition", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\FEP\\FEPDiscovery.cs", 263);
			responderDefinition.Enabled = enabled;
			return responderDefinition;
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x000474A8 File Offset: 0x000456A8
		private ResponderDefinition CreateResponderDefinition(string targetResource, Type responderType, string responderName, string alertMask, string alertTypeId, int recurrenceIntervalSeconds, int waitIntervalSeconds)
		{
			WTFDiagnostics.TraceDebug<string, string>(ExTraceGlobals.StoreTracer, base.TraceContext, "FEPDiscovery.DoWork: Creating {0} for {1}", responderName, targetResource, null, "CreateResponderDefinition", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\FEP\\FEPDiscovery.cs", 294);
			ResponderDefinition responderDefinition = new ResponderDefinition();
			responderDefinition.AssemblyPath = responderType.Assembly.Location;
			responderDefinition.TypeName = responderType.FullName;
			responderDefinition.Name = responderName;
			responderDefinition.RecurrenceIntervalSeconds = recurrenceIntervalSeconds;
			responderDefinition.TimeoutSeconds = recurrenceIntervalSeconds;
			responderDefinition.MaxRetryAttempts = 3;
			responderDefinition.Attributes["WindowsServiceName"] = targetResource;
			responderDefinition.AlertMask = alertMask;
			responderDefinition.AlertTypeId = alertTypeId;
			responderDefinition.ServiceName = ExchangeComponent.FEP.Name;
			responderDefinition.WaitIntervalSeconds = waitIntervalSeconds;
			WTFDiagnostics.TraceDebug<string, string>(ExTraceGlobals.StoreTracer, base.TraceContext, "StoreDiscovery.DoWork: Created {0} for {1}", responderName, targetResource, null, "CreateResponderDefinition", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\FEP\\FEPDiscovery.cs", 315);
			return responderDefinition;
		}

		// Token: 0x0400086B RID: 2155
		internal const string ServiceRunningProbeName = "FEPServiceRunningProbe";

		// Token: 0x0400086C RID: 2156
		private const int MaxRetryAttempt = 3;

		// Token: 0x0400086D RID: 2157
		private const string EscalationTeam = "Security";

		// Token: 0x0400086E RID: 2158
		private const string ServiceName = "MsMpSvc";

		// Token: 0x0400086F RID: 2159
		private const string ServiceRunningMonitorName = "FEPServiceRunningMonitor";

		// Token: 0x04000870 RID: 2160
		private const string ServiceRestartResponderName = "FEPServiceRestartResponder";

		// Token: 0x04000871 RID: 2161
		private static readonly string AssemblyPath = Assembly.GetExecutingAssembly().Location;

		// Token: 0x04000872 RID: 2162
		private static readonly Type RestartServiceResponderType = typeof(RestartServiceResponder);
	}
}
