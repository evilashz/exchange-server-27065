using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.OfficeGraph.Probes;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Search;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Search.Responders;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.OfficeGraph
{
	// Token: 0x0200024F RID: 591
	public sealed class OfficeGraphDiscovery : MaintenanceWorkItem
	{
		// Token: 0x06001099 RID: 4249 RVA: 0x0006E1C0 File Offset: 0x0006C3C0
		protected override void DoWork(CancellationToken cancellationToken)
		{
			try
			{
				if (!LocalEndpointManager.IsDataCenter && !LocalEndpointManager.IsDataCenterDedicated)
				{
					WTFDiagnostics.TraceInformation(ExTraceGlobals.OfficeGraphTracer, base.TraceContext, "OfficeGraphDiscovery.DoWork: Not in datacenter, thus no need to create OfficeGraph related work items", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OfficeGraph\\OfficeGraphDiscovery.cs", 72);
				}
				else
				{
					this.attributeHelper = new AttributeHelper(base.Definition);
					LocalEndpointManager instance = LocalEndpointManager.Instance;
					if (instance.ExchangeServerRoleEndpoint == null)
					{
						WTFDiagnostics.TraceInformation(ExTraceGlobals.OfficeGraphTracer, base.TraceContext, "OfficeGraphDiscovery.DoWork: Could not find ExchangeServerRoleEndpoint, thus no need to create OfficeGraph related work items", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OfficeGraph\\OfficeGraphDiscovery.cs", 83);
					}
					else if (this.IsInMaintenance())
					{
						WTFDiagnostics.TraceInformation(ExTraceGlobals.OfficeGraphTracer, base.TraceContext, "OfficeGraphDiscovery.DoWork: Server is in maintenance mode, thus skip monitoring", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OfficeGraph\\OfficeGraphDiscovery.cs", 90);
					}
					else if (instance.ExchangeServerRoleEndpoint.IsMailboxRoleInstalled)
					{
						WTFDiagnostics.TraceInformation(ExTraceGlobals.OfficeGraphTracer, base.TraceContext, "OfficeGraphDiscovery.DoWork: Creating monitoring contexts for mailbox", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OfficeGraph\\OfficeGraphDiscovery.cs", 97);
						this.CreateMonitoringContextsForMailbox();
						WTFDiagnostics.TraceInformation(ExTraceGlobals.OfficeGraphTracer, base.TraceContext, "OfficeGraphDiscovery.DoWork: Created monitoring contexts for mailbox", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OfficeGraph\\OfficeGraphDiscovery.cs", 99);
					}
					else if (instance.ExchangeServerRoleEndpoint.IsFrontendTransportRoleInstalled || instance.ExchangeServerRoleEndpoint.IsBridgeheadRoleInstalled)
					{
						WTFDiagnostics.TraceInformation(ExTraceGlobals.OfficeGraphTracer, base.TraceContext, "OfficeGraphDiscovery.DoWork: Creating monitoring contexts for transport", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OfficeGraph\\OfficeGraphDiscovery.cs", 106);
						this.CreateMonitoringContextsForTransport();
						WTFDiagnostics.TraceInformation(ExTraceGlobals.OfficeGraphTracer, base.TraceContext, "OfficeGraphDiscovery.DoWork: Created monitoring contexts for transport", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OfficeGraph\\OfficeGraphDiscovery.cs", 108);
					}
				}
			}
			catch (EndpointManagerEndpointUninitializedException)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.OfficeGraphTracer, base.TraceContext, "OfficeGraphDiscovery.DoWork: EndpointManagerEndpointUninitializedException is caught. Endpoint is not available to do monitoring", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OfficeGraph\\OfficeGraphDiscovery.cs", 114);
			}
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x0006E37C File Offset: 0x0006C57C
		private bool IsInMaintenance()
		{
			return DirectoryAccessor.Instance.IsRecoveryActionsEnabledOffline(LocalServer.GetServer().Name);
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x0006E392 File Offset: 0x0006C592
		private void CreateMonitoringContextsForMailbox()
		{
			this.CreateTransportDeliveryAgentContext(false);
			this.CreateMessageTracingPluginProcessingTimeContext(false);
			this.CreateMessageTracingPluginLogDirectorySizeContext(false);
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x0006E3A9 File Offset: 0x0006C5A9
		private void CreateMonitoringContextsForTransport()
		{
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x0006E3AC File Offset: 0x0006C5AC
		private void CreateTransportDeliveryAgentContext(bool isEscalateModeUrgent)
		{
			bool @bool = this.attributeHelper.GetBool("OfficeGraphTransportDeliveryAgentEnabled", true, true);
			int @int = this.attributeHelper.GetInt("OfficeGraphTransportDeliveryAgentProbeRecurrenceIntervalSeconds", true, 0, null, null);
			int int2 = this.attributeHelper.GetInt("OfficeGraphTransportDeliveryAgentMonitorMonitoringThreshold", true, 0, null, null);
			int int3 = this.attributeHelper.GetInt("OfficeGraphTransportDeliveryAgentMonitorMonitoringIntervalSeconds", true, 0, null, null);
			int int4 = this.attributeHelper.GetInt("OfficeGraphTransportDeliveryAgentMonitorUnhealthyStateSeconds", true, 0, null, null);
			ProbeDefinition probeDefinition = this.CreateProbeDefinition("OfficeGraphTransportDeliveryAgentProcessingTimeProbe", typeof(OfficeGraphTransportDeliveryAgentProcessingTimeProbe), string.Empty, @int, @bool);
			base.Broker.AddWorkDefinition<ProbeDefinition>(probeDefinition, base.TraceContext);
			MonitorDefinition monitorDefinition = this.CreateMonitorDefinition("OfficeGraphTransportDeliveryAgentProcessingTimeMonitor", typeof(OverallXFailuresMonitor), probeDefinition.ConstructWorkItemResultName(), string.Empty, @int, int3, int2, @bool);
			monitorDefinition.ServicePriority = 1;
			monitorDefinition.ScenarioDescription = "Monitor Office Graph transport delivery agent's processing time";
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
			List<MonitorStateTransition> list = new List<MonitorStateTransition>();
			list.Add(new MonitorStateTransition(ServiceHealthStatus.Unhealthy, int4));
			ResponderDefinition definition = SearchMonitoringHelper.CreateEscalateResponderDefinition(monitorDefinition, "OfficeGraphTransportDeliveryAgentProcessingTimeProbe escalation", @bool, ServiceHealthStatus.Unhealthy, SearchEscalateResponder.EscalateModes.Scheduled, true);
			base.Broker.AddWorkDefinition<ResponderDefinition>(definition, base.TraceContext);
			monitorDefinition.MonitorStateTransitions = list.ToArray();
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x0006E534 File Offset: 0x0006C734
		private void CreateMessageTracingPluginProcessingTimeContext(bool isEscalateModeUrgent)
		{
			bool @bool = this.attributeHelper.GetBool("OfficeGraphMessageTracingPluginEnabled", true, true);
			int @int = this.attributeHelper.GetInt("OfficeGraphMessageTracingPluginProbeRecurrenceIntervalSeconds", true, 0, null, null);
			int int2 = this.attributeHelper.GetInt("OfficeGraphMessageTracingPluginMonitorMonitoringThreshold", true, 0, null, null);
			int int3 = this.attributeHelper.GetInt("OfficeGraphMessageTracingPluginMonitorMonitoringIntervalSeconds", true, 0, null, null);
			int int4 = this.attributeHelper.GetInt("OfficeGraphMessageTracingPluginMonitorUnhealthyStateSeconds", true, 0, null, null);
			ProbeDefinition probeDefinition = this.CreateProbeDefinition("OfficeGraphMessageTracingPluginProcessingTimeProbe", typeof(OfficeGraphMessageTracingPluginProcessingTimeProbe), string.Empty, @int, @bool);
			base.Broker.AddWorkDefinition<ProbeDefinition>(probeDefinition, base.TraceContext);
			MonitorDefinition monitorDefinition = this.CreateMonitorDefinition("OfficeGraphMessageTracingPluginProcessingTimeMonitor", typeof(OverallXFailuresMonitor), probeDefinition.ConstructWorkItemResultName(), string.Empty, @int, int3, int2, @bool);
			monitorDefinition.ServicePriority = 1;
			monitorDefinition.ScenarioDescription = "Monitor Office Graph message tracing plugin's processing time";
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
			List<MonitorStateTransition> list = new List<MonitorStateTransition>();
			list.Add(new MonitorStateTransition(ServiceHealthStatus.Unhealthy, int4));
			ResponderDefinition definition = SearchMonitoringHelper.CreateEscalateResponderDefinition(monitorDefinition, "OfficeGraphMessageTracingPluginProcessingTimeProbe escalation", @bool, ServiceHealthStatus.Unhealthy, SearchEscalateResponder.EscalateModes.Scheduled, true);
			base.Broker.AddWorkDefinition<ResponderDefinition>(definition, base.TraceContext);
			monitorDefinition.MonitorStateTransitions = list.ToArray();
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x0006E6BC File Offset: 0x0006C8BC
		private void CreateMessageTracingPluginLogDirectorySizeContext(bool isEscalateModeUrgent)
		{
			bool @bool = this.attributeHelper.GetBool("OfficeGraphMessageTracingPluginEnabled", true, true);
			int @int = this.attributeHelper.GetInt("OfficeGraphMessageTracingPluginProbeRecurrenceIntervalSeconds", true, 0, null, null);
			int int2 = this.attributeHelper.GetInt("OfficeGraphMessageTracingPluginMonitorMonitoringThreshold", true, 0, null, null);
			int int3 = this.attributeHelper.GetInt("OfficeGraphMessageTracingPluginMonitorMonitoringIntervalSeconds", true, 0, null, null);
			int int4 = this.attributeHelper.GetInt("OfficeGraphMessageTracingPluginMonitorUnhealthyStateSeconds", true, 0, null, null);
			ProbeDefinition probeDefinition = this.CreateProbeDefinition("OfficeGraphMessageTracingPluginLogSizeProbe", typeof(OfficeGraphMessageTracingPluginLogSizeProbe), string.Empty, @int, @bool);
			base.Broker.AddWorkDefinition<ProbeDefinition>(probeDefinition, base.TraceContext);
			MonitorDefinition monitorDefinition = this.CreateMonitorDefinition("OfficeGraphMessageTracingPluginLogSizeMonitor", typeof(OverallXFailuresMonitor), probeDefinition.ConstructWorkItemResultName(), string.Empty, @int, int3, int2, @bool);
			monitorDefinition.ServicePriority = 1;
			monitorDefinition.ScenarioDescription = "Monitor Office Graph message tracing plugin's log directory size";
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
			List<MonitorStateTransition> list = new List<MonitorStateTransition>();
			list.Add(new MonitorStateTransition(ServiceHealthStatus.Unhealthy, int4));
			ResponderDefinition definition = SearchMonitoringHelper.CreateEscalateResponderDefinition(monitorDefinition, "OfficeGraphMessageTracingPluginLogSizeProbe escalation", @bool, ServiceHealthStatus.Unhealthy, SearchEscalateResponder.EscalateModes.Scheduled, true);
			base.Broker.AddWorkDefinition<ResponderDefinition>(definition, base.TraceContext);
			monitorDefinition.MonitorStateTransitions = list.ToArray();
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x0006E844 File Offset: 0x0006CA44
		private ProbeDefinition CreateProbeDefinition(string probeName, Type probeType, string targetResource, int recurrenceIntervalSeconds, bool enabled)
		{
			ProbeDefinition probeDefinition = new ProbeDefinition();
			probeDefinition.AssemblyPath = probeType.Assembly.Location;
			probeDefinition.TypeName = probeType.FullName;
			probeDefinition.Name = probeName;
			probeDefinition.ServiceName = ExchangeComponent.OfficeGraph.Name;
			probeDefinition.TargetResource = targetResource;
			probeDefinition.RecurrenceIntervalSeconds = recurrenceIntervalSeconds;
			probeDefinition.TimeoutSeconds = 2 * recurrenceIntervalSeconds;
			probeDefinition.MaxRetryAttempts = 0;
			probeDefinition.Enabled = enabled;
			WTFDiagnostics.TraceInformation<string, string>(ExTraceGlobals.OfficeGraphTracer, base.TraceContext, "OfficeGraphDiscovery.CreateProbeDefinition: Created ProbeDefinition '{0}' for '{1}'.", probeName, targetResource, null, "CreateProbeDefinition", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OfficeGraph\\OfficeGraphDiscovery.cs", 321);
			return probeDefinition;
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x0006E8DC File Offset: 0x0006CADC
		private MonitorDefinition CreateMonitorDefinition(string monitorName, Type monitorType, string sampleMask, string targetResource, int recurrenceIntervalSeconds, int monitoringIntervalSeconds, int monitoringThreshold, bool enabled)
		{
			MonitorDefinition monitorDefinition = new MonitorDefinition();
			monitorDefinition.AssemblyPath = monitorType.Assembly.Location;
			monitorDefinition.TypeName = monitorType.FullName;
			monitorDefinition.Name = monitorName;
			monitorDefinition.ServiceName = ExchangeComponent.OfficeGraph.Name;
			monitorDefinition.SampleMask = sampleMask;
			monitorDefinition.TargetResource = targetResource;
			monitorDefinition.Component = ExchangeComponent.OfficeGraph;
			monitorDefinition.RecurrenceIntervalSeconds = recurrenceIntervalSeconds;
			monitorDefinition.InsufficientSamplesIntervalSeconds = Math.Max(5 * monitorDefinition.RecurrenceIntervalSeconds, Convert.ToInt32(ConfigurationManager.AppSettings["InsufficientSamplesIntervalInSeconds"]));
			monitorDefinition.TimeoutSeconds = 2 * recurrenceIntervalSeconds;
			monitorDefinition.MonitoringIntervalSeconds = monitoringIntervalSeconds;
			monitorDefinition.MonitoringThreshold = (double)monitoringThreshold;
			monitorDefinition.Enabled = enabled;
			WTFDiagnostics.TraceInformation<string, string>(ExTraceGlobals.OfficeGraphTracer, base.TraceContext, "OfficeGraphDiscovery.CreateMonitorDefinition: Created MonitorDefinition '{0}' for '{1}'.", monitorName, targetResource, null, "CreateMonitorDefinition", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\OfficeGraph\\OfficeGraphDiscovery.cs", 367);
			return monitorDefinition;
		}

		// Token: 0x04000C6C RID: 3180
		internal const int MaxRetryAttempt = 0;

		// Token: 0x04000C6D RID: 3181
		private static readonly Type OverallXFailuresMonitorType = typeof(OverallXFailuresMonitor);

		// Token: 0x04000C6E RID: 3182
		private static readonly Type OverallConsecutiveProbeFailuresMonitorType = typeof(OverallConsecutiveProbeFailuresMonitor);

		// Token: 0x04000C6F RID: 3183
		private static readonly Type RestartServiceResponderType = typeof(RestartServiceResponder);

		// Token: 0x04000C70 RID: 3184
		private AttributeHelper attributeHelper;
	}
}
