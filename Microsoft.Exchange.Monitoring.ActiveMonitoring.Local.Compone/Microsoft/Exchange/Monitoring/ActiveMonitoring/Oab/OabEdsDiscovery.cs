using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.ActiveMonitoring;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Responders;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Oab
{
	// Token: 0x02000241 RID: 577
	public sealed class OabEdsDiscovery : MaintenanceWorkItem
	{
		// Token: 0x0600101B RID: 4123 RVA: 0x0006BC30 File Offset: 0x00069E30
		protected override void DoWork(CancellationToken cancellationToken)
		{
			LocalEndpointManager instance = LocalEndpointManager.Instance;
			try
			{
				if (instance.ExchangeServerRoleEndpoint == null)
				{
					WTFDiagnostics.TraceInformation(ExTraceGlobals.OABTracer, OabEdsDiscovery.traceContext, "OabEdsDiscovery:: DoWork(): Could not find ExchangeServerRoleEndpoint", null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Oab\\Eds\\OabEdsDiscovery.cs", 61);
					return;
				}
			}
			catch (EndpointManagerEndpointUninitializedException ex)
			{
				WTFDiagnostics.TraceInformation(ExTraceGlobals.OABTracer, OabEdsDiscovery.traceContext, string.Format("OabEdsDiscovery:: DoWork(): ExchangeServerRoleEndpoint object threw exception.  Exception:{0}", ex.ToString()), null, "DoWork", "f:\\15.00.1497\\sources\\dev\\monitoring\\src\\ActiveMonitoring\\Components\\Oab\\Eds\\OabEdsDiscovery.cs", 67);
				return;
			}
			if (instance.ExchangeServerRoleEndpoint.IsMailboxRoleInstalled)
			{
				OabEdsAlertDefinitions item = new OabEdsAlertDefinitions("OabTooManyHttpErrorResponsesEncountered", Strings.OabTooManyHttpErrorResponsesEncounteredSubject, Strings.OabTooManyHttpErrorResponsesEncounteredBody, NotificationServiceClass.Scheduled, true);
				OabEdsAlertDefinitions item2 = new OabEdsAlertDefinitions("OabFileLoadException", Strings.OabFileLoadExceptionEncounteredSubject, Strings.OabFileLoadExceptionEncounteredBody, NotificationServiceClass.Scheduled, true);
				OabEdsAlertDefinitions item3 = new OabEdsAlertDefinitions("OABGenTenantOutOfSLA", Strings.OABGenTenantOutOfSLASubject, Strings.OABGenTenantOutOfSLABody, NotificationServiceClass.Scheduled, false);
				foreach (OabEdsAlertDefinitions oabEdsAlertDefinitions in new List<OabEdsAlertDefinitions>
				{
					item,
					item3,
					item2
				})
				{
					MonitorDefinition monitorDefinition = OverallConsecutiveProbeFailuresMonitor.CreateDefinition(oabEdsAlertDefinitions.MonitorName, string.Format("{0}/{1}", ExchangeComponent.Eds.Name, oabEdsAlertDefinitions.RedEvent.ToString()), ExchangeComponent.Oab.Name, ExchangeComponent.Oab, 1, true, 300);
					monitorDefinition.RecurrenceIntervalSeconds = 0;
					if (oabEdsAlertDefinitions.RecycleAppPool)
					{
						monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
						{
							new MonitorStateTransition(ServiceHealthStatus.Unhealthy, 0),
							new MonitorStateTransition(ServiceHealthStatus.Unrecoverable, TimeSpan.FromMinutes(15.0))
						};
					}
					else
					{
						monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
						{
							new MonitorStateTransition(ServiceHealthStatus.Unrecoverable, 0)
						};
					}
					monitorDefinition.ServicePriority = 0;
					monitorDefinition.ScenarioDescription = "Validate OAB health is not impacted by EDS monitoring reported isssues";
					base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, OabEdsDiscovery.traceContext);
					if (oabEdsAlertDefinitions.RecycleAppPool)
					{
						ResponderDefinition responderDefinition = ResetIISAppPoolResponder.CreateDefinition(oabEdsAlertDefinitions.RecycleResponderName, oabEdsAlertDefinitions.MonitorName, "MSExchangeOABAppPool", ServiceHealthStatus.Unhealthy, DumpMode.FullDump, null, 25.0, 90, "Exchange", true, "Dag");
						responderDefinition.ServiceName = ExchangeComponent.Oab.Name;
						responderDefinition.RecurrenceIntervalSeconds = 0;
						base.Broker.AddWorkDefinition<ResponderDefinition>(responderDefinition, OabEdsDiscovery.traceContext);
					}
					ResponderDefinition definition = EscalateResponder.CreateDefinition(oabEdsAlertDefinitions.EscalateResponderName, ExchangeComponent.Oab.Name, oabEdsAlertDefinitions.MonitorName, oabEdsAlertDefinitions.MonitorName, Environment.MachineName, ServiceHealthStatus.Unrecoverable, ExchangeComponent.Oab.EscalationTeam, oabEdsAlertDefinitions.MessageSubject, oabEdsAlertDefinitions.MessageBody, true, oabEdsAlertDefinitions.NotificationClass, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
					base.Broker.AddWorkDefinition<ResponderDefinition>(definition, OabEdsDiscovery.traceContext);
				}
				return;
			}
		}

		// Token: 0x04000C1A RID: 3098
		private const string OabTooManyHttpErrorResponsesEncounteredString = "OabTooManyHttpErrorResponsesEncountered";

		// Token: 0x04000C1B RID: 3099
		private const string OabFileLoadException = "OabFileLoadException";

		// Token: 0x04000C1C RID: 3100
		private const string OABGenTenantOutOfSLAString = "OABGenTenantOutOfSLA";

		// Token: 0x04000C1D RID: 3101
		private static TracingContext traceContext = new TracingContext();
	}
}
