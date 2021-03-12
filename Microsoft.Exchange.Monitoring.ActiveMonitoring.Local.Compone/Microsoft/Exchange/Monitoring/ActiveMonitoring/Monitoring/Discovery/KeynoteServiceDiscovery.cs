using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Monitoring.Discovery
{
	// Token: 0x0200021C RID: 540
	public class KeynoteServiceDiscovery : CentralMaintenanceWorkItem
	{
		// Token: 0x06000F32 RID: 3890 RVA: 0x000652F4 File Offset: 0x000634F4
		public override Task GenerateWorkItems(CancellationToken cancellationToken)
		{
			return Task.Factory.StartNew(delegate()
			{
				ProbeDefinition probeDefinition = KeynoteServiceProbe.CreateDefinition();
				base.Broker.AddWorkDefinition<ProbeDefinition>(probeDefinition, base.TraceContext);
				MonitorDefinition monitorDefinition = KeynoteServiceDiscovery.CreateMonitorDefinition(probeDefinition.ConstructWorkItemResultName());
				base.Broker.AddWorkDefinition<MonitorDefinition>(monitorDefinition, base.TraceContext);
				ResponderDefinition[] array = KeynoteServiceDiscovery.CreateRespondersDefinition(monitorDefinition.ConstructWorkItemResultName(), Environment.MachineName);
				foreach (ResponderDefinition definition in array)
				{
					base.Broker.AddWorkDefinition<ResponderDefinition>(definition, base.TraceContext);
				}
			}, cancellationToken, TaskCreationOptions.AttachedToParent, TaskScheduler.Default);
		}

		// Token: 0x06000F33 RID: 3891 RVA: 0x00065320 File Offset: 0x00063520
		private static MonitorDefinition CreateMonitorDefinition(string sampleMask)
		{
			MonitorDefinition monitorDefinition = OverallConsecutiveProbeFailuresMonitor.CreateDefinition("KeynoteServiceAvailabilityMonitor", sampleMask, ExchangeComponent.Monitoring.Name, ExchangeComponent.Monitoring, 1, true, 300);
			monitorDefinition.RecurrenceIntervalSeconds = 1800;
			monitorDefinition.TimeoutSeconds = 60;
			monitorDefinition.MonitoringIntervalSeconds = 1800;
			monitorDefinition.MaxRetryAttempts = 0;
			monitorDefinition.WorkItemVersion = KeynoteServiceDiscovery.AssemblyVersion;
			monitorDefinition.ExecutionLocation = "EXO";
			monitorDefinition.TargetResource = "Microsoft.Exchange.Monitoring.KeynoteDataReader";
			monitorDefinition.InsufficientSamplesIntervalSeconds = 0;
			monitorDefinition.MinimumErrorCount = 3;
			monitorDefinition.MonitoringThreshold = 1.0;
			monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Degraded, 1200),
				new MonitorStateTransition(ServiceHealthStatus.Unhealthy, 3600)
			};
			return monitorDefinition;
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x000653E0 File Offset: 0x000635E0
		private static ResponderDefinition[] CreateRespondersDefinition(string alertMask, string target)
		{
			ResponderDefinition responderDefinition = RestartServiceResponder.CreateDefinition("KeynoteServiceRestartResponder", "KeynoteServiceAvailabilityMonitor", "Microsoft.Exchange.Monitoring.KeynoteDataReader", ServiceHealthStatus.Degraded, 15, 120, 0, false, DumpMode.None, null, 15.0, 0, "Exchange", null, true, true, null, false);
			responderDefinition.WorkItemVersion = KeynoteServiceDiscovery.AssemblyVersion;
			responderDefinition.RecurrenceIntervalSeconds = 600;
			responderDefinition.TimeoutSeconds = 60;
			responderDefinition.MaxRetryAttempts = 3;
			responderDefinition.TargetResource = target;
			responderDefinition.ExecutionLocation = "EXO";
			responderDefinition.AccountPassword = string.Empty;
			ResponderDefinition responderDefinition2 = EscalateResponder.CreateDefinition(KeynoteServiceDiscovery.KeynoteServiceDownResponderName, "Exchange", "Monitoring/CentralActiveMonitoring/KeynoteServiceDown", alertMask, target, ServiceHealthStatus.Unhealthy, ExchangeComponent.Monitoring.Name, string.Format("Keynote service is not running on server '{0}'", target), string.Format("Keynote service monitor failed for server {0}. Please start the service.", target), false, NotificationServiceClass.UrgentInTraining, 10800, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
			responderDefinition2.WorkItemVersion = KeynoteServiceDiscovery.AssemblyVersion;
			responderDefinition2.RecurrenceIntervalSeconds = 600;
			responderDefinition2.TimeoutSeconds = 30;
			responderDefinition2.MaxRetryAttempts = 3;
			responderDefinition2.TargetResource = target;
			responderDefinition2.ExecutionLocation = "EXO";
			responderDefinition2.AccountPassword = string.Empty;
			return new ResponderDefinition[]
			{
				responderDefinition,
				responderDefinition2
			};
		}

		// Token: 0x04000B61 RID: 2913
		public const string ServiceToMonitor = "Microsoft.Exchange.Monitoring.KeynoteDataReader";

		// Token: 0x04000B62 RID: 2914
		private const string MonitorName = "KeynoteServiceAvailabilityMonitor";

		// Token: 0x04000B63 RID: 2915
		private const int MonitorRecurrenceInSeconds = 1800;

		// Token: 0x04000B64 RID: 2916
		private const int ResponderRecurrenceInSeconds = 600;

		// Token: 0x04000B65 RID: 2917
		private const int MinimumTimeBetweenEscalations = 10800;

		// Token: 0x04000B66 RID: 2918
		private const string KeynoteServiceRestartResponderName = "KeynoteServiceRestartResponder";

		// Token: 0x04000B67 RID: 2919
		private const string KeynoteServiceAlertTypeId = "Monitoring/CentralActiveMonitoring/KeynoteServiceDown";

		// Token: 0x04000B68 RID: 2920
		public static string AssemblyVersion = Assembly.GetExecutingAssembly().GetName().Version.ToString();

		// Token: 0x04000B69 RID: 2921
		private static string KeynoteServiceDownResponderName = "KeynoteServiceDownResponder";
	}
}
