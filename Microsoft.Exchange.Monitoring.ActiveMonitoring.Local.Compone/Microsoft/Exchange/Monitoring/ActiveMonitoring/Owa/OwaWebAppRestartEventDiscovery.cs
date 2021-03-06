using System;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Owa
{
	// Token: 0x02000269 RID: 617
	public sealed class OwaWebAppRestartEventDiscovery : MaintenanceWorkItem
	{
		// Token: 0x06001182 RID: 4482 RVA: 0x000757AC File Offset: 0x000739AC
		protected override void DoWork(CancellationToken cancellationToken)
		{
			LocalEndpointManager instance = LocalEndpointManager.Instance;
			try
			{
				if (instance.ExchangeServerRoleEndpoint == null)
				{
					WTFDiagnostics.TraceInformation(ExTraceGlobals.OWATracer, base.TraceContext, "OwaWebAppRestartEventDiscovery:: DoWork(): Could not find ExchangeServerRoleEndpoint", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Owa\\OwaWebAppRestartEventDiscovery.cs", 57);
					return;
				}
			}
			catch (EndpointManagerEndpointUninitializedException ex)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.OWATracer, base.TraceContext, string.Format("OwaWebAppRestartEventDiscovery:: DoWork(): ExchangeServerRoleEndpoint object threw exception.  Exception:{0}", ex.ToString()), null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Owa\\OwaWebAppRestartEventDiscovery.cs", 63);
				return;
			}
			if (instance.ExchangeServerRoleEndpoint.IsMailboxRoleInstalled)
			{
				MonitorDefinition monitorDefinition = OwaWebAppRestartEventMonitor.CreateDefinition("OwaTooManyWebAppStartsMonitor", NotificationItem.GenerateResultName(ExchangeComponent.Owa.Name, "OwaWebAppStarted", null), ExchangeComponent.Owa.Name, ExchangeComponent.Owa, OwaWebAppRestartEventDiscovery.MonitorIntervalSeconds, OwaWebAppRestartEventDiscovery.MonitorIntervalSeconds, OwaWebAppRestartEventDiscovery.EventCount, true);
				MonitorStateTransition[] monitorStateTransitions = new MonitorStateTransition[]
				{
					new MonitorStateTransition(ServiceHealthStatus.Unrecoverable, 0)
				};
				monitorDefinition.MonitorStateTransitions = monitorStateTransitions;
				monitorDefinition.ServicePriority = 0;
				monitorDefinition.ScenarioDescription = "Validate OWA health is not impacted by apppool issues";
				base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
				ResponderDefinition definition = EscalateResponder.CreateDefinition("OwaTooManyWebAppStartsResponder", ExchangeComponent.Owa.Name, "OwaTooManyWebAppStartsMonitor", string.Format("{0}/{1}", "OwaTooManyWebAppStartsMonitor", ExchangeComponent.Owa.Name), Environment.MachineName, ServiceHealthStatus.Unrecoverable, ExchangeComponent.Owa.EscalationTeam, Strings.OwaTooManyWebAppStartsSubject, Strings.OwaTooManyWebAppStartsBody(Environment.MachineName), true, NotificationServiceClass.Scheduled, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
				base.Broker.AddWorkDefinition<ResponderDefinition>(definition, base.TraceContext);
				return;
			}
		}

		// Token: 0x04000D32 RID: 3378
		private const string OwaTooManyWebAppStartsMonitorName = "OwaTooManyWebAppStartsMonitor";

		// Token: 0x04000D33 RID: 3379
		private const string OwaTooManyWebAppStartsResponderName = "OwaTooManyWebAppStartsResponder";

		// Token: 0x04000D34 RID: 3380
		private static readonly int MonitorIntervalSeconds = 3600;

		// Token: 0x04000D35 RID: 3381
		private static readonly int EventCount = 5;
	}
}
