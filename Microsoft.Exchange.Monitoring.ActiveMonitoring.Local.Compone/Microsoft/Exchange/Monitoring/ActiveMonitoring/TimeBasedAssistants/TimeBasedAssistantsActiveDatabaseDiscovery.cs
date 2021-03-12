using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.TimeBasedAssistants
{
	// Token: 0x02000520 RID: 1312
	public static class TimeBasedAssistantsActiveDatabaseDiscovery
	{
		// Token: 0x0600204E RID: 8270 RVA: 0x000C5AB4 File Offset: 0x000C3CB4
		public static void ConfigureForTest()
		{
			TimeBasedAssistantsActiveDatabaseDiscovery.AssistantsActiveDatabaseProbeMaxAttempts = 1;
			TimeBasedAssistantsActiveDatabaseDiscovery.AssistantsActiveDatabaseMonitorThreshold = 2;
			TimeBasedAssistantsActiveDatabaseDiscovery.AssistantsActiveDatabaseProbeRecurrenceSeconds = (int)TimeSpan.FromSeconds(30.0).TotalSeconds;
			TimeBasedAssistantsActiveDatabaseDiscovery.AssistantsActiveDatabaseMonitoringIntervalSeconds = (int)TimeSpan.FromSeconds(60.0).TotalSeconds;
			TimeBasedAssistantsActiveDatabaseDiscovery.AssistantsActiveDatabaseEscalateResponderWaitIntervalSeconds = (int)TimeSpan.FromSeconds(10.0).TotalSeconds;
		}

		// Token: 0x0600204F RID: 8271 RVA: 0x000C5B21 File Offset: 0x000C3D21
		public static ProbeDefinition CreateActiveDatabaseProbeDefinition(TracingContext tracingContext)
		{
			ArgumentValidator.ThrowIfNull("tracingContext", tracingContext);
			return TimeBasedAssistantsDiscoveryHelpers.CreateProbe(ExchangeComponent.TimeBasedAssistants, "MSExchangeMailboxAssistants", LocalServer.GetServer().Fqdn, typeof(TimeBasedAssistantsActiveDatabaseProbe), TimeBasedAssistantsActiveDatabaseDiscovery.AssistantsActiveDatabaseProbeName, TimeBasedAssistantsActiveDatabaseDiscovery.AssistantsActiveDatabaseProbeRecurrenceSeconds, TimeBasedAssistantsActiveDatabaseDiscovery.AssistantsActiveDatabaseProbeMaxAttempts, tracingContext);
		}

		// Token: 0x06002050 RID: 8272 RVA: 0x000C5B64 File Offset: 0x000C3D64
		public static MonitorDefinition CreateActiveDatabaseMonitorDefinition(string mask, TracingContext tracingContext)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("mask", mask);
			ArgumentValidator.ThrowIfNull("tracingContext", tracingContext);
			MonitorDefinition monitorDefinition = OverallConsecutiveProbeFailuresMonitor.CreateDefinition(TimeBasedAssistantsActiveDatabaseDiscovery.AssistantsActiveDatabaseMonitorName, mask, ExchangeComponent.TimeBasedAssistants.Name, ExchangeComponent.TimeBasedAssistants, TimeBasedAssistantsActiveDatabaseDiscovery.AssistantsActiveDatabaseMonitorThreshold, true, TimeBasedAssistantsActiveDatabaseDiscovery.AssistantsActiveDatabaseMonitoringIntervalSeconds);
			monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Unhealthy, 0),
				new MonitorStateTransition(ServiceHealthStatus.Unrecoverable, monitorDefinition.RecurrenceIntervalSeconds / 2)
			};
			monitorDefinition.ServicePriority = 1;
			monitorDefinition.ScenarioDescription = "Validate Time Based Assistatns are running on active databases.";
			return monitorDefinition;
		}

		// Token: 0x06002051 RID: 8273 RVA: 0x000C5BEC File Offset: 0x000C3DEC
		public static ResponderDefinition CreateActiveDatabaseEscalateResponderDefinition(string monitorName, string alertMask)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("monitorName", monitorName);
			ArgumentValidator.ThrowIfNullOrWhiteSpace("alertMask", alertMask);
			ResponderDefinition responderDefinition = EscalateResponder.CreateDefinition(TimeBasedAssistantsActiveDatabaseDiscovery.AssistantsActiveDatabaseEscalateResponderName, ExchangeComponent.TimeBasedAssistants.Name, monitorName, alertMask, "MSExchangeMailboxAssistants", ServiceHealthStatus.Unrecoverable, ExchangeComponent.TimeBasedAssistants.EscalationTeam, Strings.AssistantsActiveDatabaseSubject, Strings.AssistantsActiveDatabaseMessage, true, NotificationServiceClass.UrgentInTraining, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
			responderDefinition.WaitIntervalSeconds = TimeBasedAssistantsActiveDatabaseDiscovery.AssistantsActiveDatabaseEscalateResponderWaitIntervalSeconds;
			return responderDefinition;
		}

		// Token: 0x040017BC RID: 6076
		private const string AssistantsActiveDatabaseWorkItemName = "TbaActiveDatabase";

		// Token: 0x040017BD RID: 6077
		private static readonly string AssistantsActiveDatabaseProbeName = TimeBasedAssistantsDiscoveryHelpers.GenerateProbeName("TbaActiveDatabase");

		// Token: 0x040017BE RID: 6078
		private static readonly string AssistantsActiveDatabaseMonitorName = TimeBasedAssistantsDiscoveryHelpers.GenerateMonitorName("TbaActiveDatabase");

		// Token: 0x040017BF RID: 6079
		private static readonly string AssistantsActiveDatabaseEscalateResponderName = TimeBasedAssistantsDiscoveryHelpers.GenerateResponderName("TbaActiveDatabase", "Escalate");

		// Token: 0x040017C0 RID: 6080
		private static int AssistantsActiveDatabaseProbeMaxAttempts = 3;

		// Token: 0x040017C1 RID: 6081
		private static int AssistantsActiveDatabaseMonitorThreshold = 4;

		// Token: 0x040017C2 RID: 6082
		private static int AssistantsActiveDatabaseProbeRecurrenceSeconds = (int)TimeSpan.FromMinutes(80.0).TotalSeconds;

		// Token: 0x040017C3 RID: 6083
		private static int AssistantsActiveDatabaseMonitoringIntervalSeconds = (int)TimeSpan.FromHours(8.0).TotalSeconds;

		// Token: 0x040017C4 RID: 6084
		private static int AssistantsActiveDatabaseEscalateResponderWaitIntervalSeconds = (int)TimeSpan.FromHours(8.0).TotalSeconds;
	}
}
