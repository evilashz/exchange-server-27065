using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.TimeBasedAssistants
{
	// Token: 0x02000530 RID: 1328
	public static class TimeBasedAssistantsNotRunningToCompletionDiscovery
	{
		// Token: 0x060020B3 RID: 8371 RVA: 0x000C7628 File Offset: 0x000C5828
		public static void ConfigureForTest()
		{
			TimeBasedAssistantsNotRunningToCompletionDiscovery.AssistantsNotRunningToCompletionProbeMaxAttempts = 1;
			TimeBasedAssistantsNotRunningToCompletionDiscovery.AssistantsNotRunningToCompletionMonitorThreshold = 2;
			TimeBasedAssistantsNotRunningToCompletionDiscovery.AssistantsNotRunningToCompletionProbeRecurrence = (int)TimeSpan.FromSeconds(30.0).TotalSeconds;
			TimeBasedAssistantsNotRunningToCompletionDiscovery.AssistantsNotRunningToCompletionMonitoringInterval = (int)TimeSpan.FromSeconds(60.0).TotalSeconds;
			TimeBasedAssistantsNotRunningToCompletionDiscovery.AssistantsNotRunningToCompletionEscalateResponderWaitInterval = (int)TimeSpan.FromSeconds(10.0).TotalSeconds;
		}

		// Token: 0x060020B4 RID: 8372 RVA: 0x000C7695 File Offset: 0x000C5895
		public static ProbeDefinition CreateNotRunningToCompletionProbeDefinition(TracingContext tracingContext)
		{
			ArgumentValidator.ThrowIfNull("tracingContext", tracingContext);
			return TimeBasedAssistantsDiscoveryHelpers.CreateProbe(ExchangeComponent.TimeBasedAssistants, "MSExchangeMailboxAssistants", LocalServer.GetServer().Fqdn, typeof(TimeBasedAssistantsNotRunningToCompletionProbe), TimeBasedAssistantsNotRunningToCompletionDiscovery.AssistantsNotRunningToCompletionProbeName, TimeBasedAssistantsNotRunningToCompletionDiscovery.AssistantsNotRunningToCompletionProbeRecurrence, TimeBasedAssistantsNotRunningToCompletionDiscovery.AssistantsNotRunningToCompletionProbeMaxAttempts, tracingContext);
		}

		// Token: 0x060020B5 RID: 8373 RVA: 0x000C76D8 File Offset: 0x000C58D8
		public static MonitorDefinition CreateNotRunningToCompletionMonitorDefinition(string mask, TracingContext tracingContext)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("mask", mask);
			ArgumentValidator.ThrowIfNull("tracingContext", tracingContext);
			MonitorDefinition monitorDefinition = OverallConsecutiveProbeFailuresMonitor.CreateDefinition(TimeBasedAssistantsNotRunningToCompletionDiscovery.AssistantsNotRunningToCompletionMonitorName, mask, ExchangeComponent.TimeBasedAssistants.Name, ExchangeComponent.TimeBasedAssistants, TimeBasedAssistantsNotRunningToCompletionDiscovery.AssistantsNotRunningToCompletionMonitorThreshold, true, TimeBasedAssistantsNotRunningToCompletionDiscovery.AssistantsNotRunningToCompletionMonitoringInterval);
			monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Unhealthy, 0),
				new MonitorStateTransition(ServiceHealthStatus.Unrecoverable, monitorDefinition.RecurrenceIntervalSeconds / 2)
			};
			monitorDefinition.ServicePriority = 1;
			monitorDefinition.ScenarioDescription = "Validate Time Based Assistatns are running to completion.";
			return monitorDefinition;
		}

		// Token: 0x060020B6 RID: 8374 RVA: 0x000C7760 File Offset: 0x000C5960
		public static ResponderDefinition CreateNotRunningToCompletionEscalateResponderDefinition(string monitorName, string alertMask)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("monitorName", monitorName);
			ArgumentValidator.ThrowIfNullOrWhiteSpace("alertMask", alertMask);
			ResponderDefinition responderDefinition = EscalateResponder.CreateDefinition(TimeBasedAssistantsNotRunningToCompletionDiscovery.AssistantsNotRunningToCompletionEscalateResponderName, ExchangeComponent.TimeBasedAssistants.Name, monitorName, alertMask, "MSExchangeMailboxAssistants", ServiceHealthStatus.Unrecoverable, ExchangeComponent.TimeBasedAssistants.EscalationTeam, Strings.AssistantsNotRunningToCompletionSubject, Strings.AssistantsNotRunningToCompletionMessage, true, NotificationServiceClass.UrgentInTraining, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
			responderDefinition.WaitIntervalSeconds = TimeBasedAssistantsNotRunningToCompletionDiscovery.AssistantsNotRunningToCompletionEscalateResponderWaitInterval;
			return responderDefinition;
		}

		// Token: 0x040017F8 RID: 6136
		private const string AssistantsNotRunningToCompletionWorkItemName = "TbaNotRunningToCompletion";

		// Token: 0x040017F9 RID: 6137
		private static readonly string AssistantsNotRunningToCompletionProbeName = TimeBasedAssistantsDiscoveryHelpers.GenerateProbeName("TbaNotRunningToCompletion");

		// Token: 0x040017FA RID: 6138
		private static readonly string AssistantsNotRunningToCompletionMonitorName = TimeBasedAssistantsDiscoveryHelpers.GenerateMonitorName("TbaNotRunningToCompletion");

		// Token: 0x040017FB RID: 6139
		private static readonly string AssistantsNotRunningToCompletionEscalateResponderName = TimeBasedAssistantsDiscoveryHelpers.GenerateResponderName("TbaNotRunningToCompletion", "Escalate");

		// Token: 0x040017FC RID: 6140
		private static int AssistantsNotRunningToCompletionProbeMaxAttempts = 3;

		// Token: 0x040017FD RID: 6141
		private static int AssistantsNotRunningToCompletionMonitorThreshold = 4;

		// Token: 0x040017FE RID: 6142
		private static int AssistantsNotRunningToCompletionProbeRecurrence = (int)TimeSpan.FromMinutes(20.0).TotalSeconds;

		// Token: 0x040017FF RID: 6143
		private static int AssistantsNotRunningToCompletionMonitoringInterval = (int)TimeSpan.FromHours(2.0).TotalSeconds;

		// Token: 0x04001800 RID: 6144
		private static int AssistantsNotRunningToCompletionEscalateResponderWaitInterval = (int)TimeSpan.FromHours(8.0).TotalSeconds;
	}
}
