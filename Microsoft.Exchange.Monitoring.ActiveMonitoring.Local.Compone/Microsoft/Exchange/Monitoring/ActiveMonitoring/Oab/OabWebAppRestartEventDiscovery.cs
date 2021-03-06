using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Oab
{
	// Token: 0x02000242 RID: 578
	public sealed class OabWebAppRestartEventDiscovery : MaintenanceWorkItem
	{
		// Token: 0x0600101E RID: 4126 RVA: 0x0006BF4C File Offset: 0x0006A14C
		protected override void DoWork(CancellationToken cancellationToken)
		{
			LocalEndpointManager instance = LocalEndpointManager.Instance;
			try
			{
				if (instance.ExchangeServerRoleEndpoint == null)
				{
					WTFDiagnostics.TraceInformation(ExTraceGlobals.OABTracer, base.TraceContext, "OabWebAppRestartEventDiscovery:: DoWork(): Could not find ExchangeServerRoleEndpoint", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Oab\\OabWebAppRestartEventDiscovery.cs", 52);
					return;
				}
			}
			catch (EndpointManagerEndpointUninitializedException ex)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.OABTracer, base.TraceContext, string.Format("OabWebAppRestartEventDiscovery:: DoWork(): ExchangeServerRoleEndpoint object threw exception.  Exception:{0}", ex.ToString()), null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Oab\\OabWebAppRestartEventDiscovery.cs", 58);
				return;
			}
			if (instance.ExchangeServerRoleEndpoint.IsMailboxRoleInstalled && instance.ExchangeServerRoleEndpoint.IsCafeRoleInstalled)
			{
				MonitorDefinition monitorDefinition = OabWebAppRestartEventMonitor.CreateDefinition("OabWebAppRestartEventMonitor", NotificationItem.GenerateResultName(ExchangeComponent.Oab.Name, "OABAppPoolTooManyRecycles", null), ExchangeComponent.Oab, 1, OabWebAppRestartEventDiscovery.MonitorIntervalSeconds, OabWebAppRestartEventDiscovery.MonitorIntervalSeconds);
				MonitorStateTransition[] monitorStateTransitions = new MonitorStateTransition[]
				{
					new MonitorStateTransition(ServiceHealthStatus.Unrecoverable, 0)
				};
				monitorDefinition.MonitorStateTransitions = monitorStateTransitions;
				monitorDefinition.ServicePriority = 0;
				monitorDefinition.ScenarioDescription = "Validate Oab health is not impacted by apppool issues";
				base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
				ResponderDefinition definition = EscalateResponder.CreateDefinition("OabWebAppRestartEventResponder", ExchangeComponent.Oab.Name, "OabWebAppRestartEventMonitor", string.Format("{0}/{1}", "OabWebAppRestartEventMonitor", ExchangeComponent.Oab.Name), Environment.MachineName, ServiceHealthStatus.Unrecoverable, ExchangeComponent.Oab.EscalationTeam, Strings.OabTooManyWebAppStartsSubject, Strings.OabTooManyWebAppStartsBody(Environment.MachineName), true, NotificationServiceClass.UrgentInTraining, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
				base.Broker.AddWorkDefinition<ResponderDefinition>(definition, base.TraceContext);
				return;
			}
		}

		// Token: 0x04000C1E RID: 3102
		private const string OabTooManyWebAppStartsMonitorName = "OabWebAppRestartEventMonitor";

		// Token: 0x04000C1F RID: 3103
		private const string OabTooManyWebAppStartsResponderName = "OabWebAppRestartEventResponder";

		// Token: 0x04000C20 RID: 3104
		private static readonly int MonitorIntervalSeconds = 3600;
	}
}
