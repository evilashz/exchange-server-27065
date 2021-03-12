using System;
using System.Threading;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Exchange.VariantConfiguration;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Places
{
	// Token: 0x0200027B RID: 635
	public sealed class FindPlacesEdsDiscovery : MaintenanceWorkItem
	{
		// Token: 0x060011E7 RID: 4583 RVA: 0x000788B0 File Offset: 0x00076AB0
		public static string MonitorName(string eventName)
		{
			return eventName + "Monitor";
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x000788BD File Offset: 0x00076ABD
		public static string ResponderName(string eventName)
		{
			return eventName + "Escalate";
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x000788CC File Offset: 0x00076ACC
		protected override void DoWork(CancellationToken cancellationToken)
		{
			if (!VariantConfiguration.GetSnapshot(MachineSettingsContext.Local, null, null).OwaDeployment.Places.Enabled || !LocalEndpointManager.Instance.ExchangeServerRoleEndpoint.IsMailboxRoleInstalled)
			{
				return;
			}
			this.CreateFailureMonitorAndResponder("FindPlacesFailureAboveThreshold", ExchangeComponent.Places, 1, Strings.FindPlacesRequestsError(Environment.MachineName));
			this.CreateFailureMonitorAndResponder("BingServicesLatencyAboveThreshold", ExchangeComponent.Places, 1, Strings.FindPlacesRequestsError(Environment.MachineName));
		}

		// Token: 0x060011EA RID: 4586 RVA: 0x0007894C File Offset: 0x00076B4C
		private void CreateFailureMonitorAndResponder(string eventName, Component exchangeComponent, int consecutiveFailures, string escalationMessage)
		{
			string text = FindPlacesEdsDiscovery.MonitorName(eventName);
			MonitorDefinition monitorDefinition = OverallConsecutiveProbeFailuresMonitor.CreateDefinition(text, NotificationItem.GenerateResultName(exchangeComponent.Name, eventName, null), exchangeComponent.Name, exchangeComponent, consecutiveFailures, true, 300);
			monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Unhealthy, 0)
			};
			monitorDefinition.ServicePriority = 2;
			monitorDefinition.ScenarioDescription = "Validate Places health is not impacted by any issues";
			base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
			ResponderDefinition definition = EscalateResponder.CreateDefinition(FindPlacesEdsDiscovery.ResponderName(eventName), exchangeComponent.Name, text, text, string.Empty, ServiceHealthStatus.Unhealthy, exchangeComponent.EscalationTeam, Strings.EscalationSubjectUnhealthy, escalationMessage, true, NotificationServiceClass.Scheduled, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
			base.Broker.AddWorkDefinition<ResponderDefinition>(definition, base.TraceContext);
		}

		// Token: 0x04000D76 RID: 3446
		internal const int DefaultMonitorThreshold = 1;

		// Token: 0x04000D77 RID: 3447
		internal const string MonitorNameSuffix = "Monitor";

		// Token: 0x04000D78 RID: 3448
		internal const string ResponderNameSuffix = "Escalate";

		// Token: 0x04000D79 RID: 3449
		internal const string PlacesFailuresEventName = "FindPlacesFailureAboveThreshold";

		// Token: 0x04000D7A RID: 3450
		internal const string BingHighLatencyEventName = "BingServicesLatencyAboveThreshold";
	}
}
