using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.TimeBasedAssistants
{
	// Token: 0x02000536 RID: 1334
	public static class TimeBasedAssistantsOutOfSlaMonitor
	{
		// Token: 0x060020C1 RID: 8385 RVA: 0x000C7BF8 File Offset: 0x000C5DF8
		public static void ConfigureForTest()
		{
			TimeBasedAssistantsOutOfSlaMonitor.AssistantsOutOfSlaProbeMaxAttempts = 1;
			TimeBasedAssistantsOutOfSlaMonitor.AssistantsOutOfSlaMonitorThreshold = 2;
			TimeBasedAssistantsOutOfSlaMonitor.AssistantsOutOfSlaProbeRecurrence = (int)TimeSpan.FromSeconds(30.0).TotalSeconds;
			TimeBasedAssistantsOutOfSlaMonitor.AssistantsOutOfSlaMonitoringInterval = (int)TimeSpan.FromSeconds(60.0).TotalSeconds;
			TimeBasedAssistantsOutOfSlaMonitor.AssistantsOutOfSlaEscalateResponderWaitInterval = (int)TimeSpan.FromSeconds(10.0).TotalSeconds;
		}

		// Token: 0x060020C2 RID: 8386 RVA: 0x000C7C68 File Offset: 0x000C5E68
		public static ProbeDefinition CreateOutOfSlaProbeDefinition(TracingContext tracingContext, TbaOutOfSlaAlertType alertType)
		{
			ArgumentValidator.ThrowIfNull("tracingContext", tracingContext);
			Type typeFromHandle;
			string probeName;
			if (alertType == TbaOutOfSlaAlertType.Urgent)
			{
				typeFromHandle = typeof(TimeBasedAssistantsOutOfSlaProbeUrgent);
				probeName = TimeBasedAssistantsOutOfSlaMonitor.AssistantsOutOfSlaUrgentProbeName;
			}
			else
			{
				typeFromHandle = typeof(TimeBasedAssistantsOutOfSlaProbeScheduled);
				probeName = TimeBasedAssistantsOutOfSlaMonitor.AssistantsOutOfSlaScheduledProbeName;
			}
			return TimeBasedAssistantsDiscoveryHelpers.CreateProbe(ExchangeComponent.TimeBasedAssistants, "MSExchangeMailboxAssistants", LocalServer.GetServer().Fqdn, typeFromHandle, probeName, TimeBasedAssistantsOutOfSlaMonitor.AssistantsOutOfSlaProbeRecurrence, TimeBasedAssistantsOutOfSlaMonitor.AssistantsOutOfSlaProbeMaxAttempts, tracingContext);
		}

		// Token: 0x060020C3 RID: 8387 RVA: 0x000C7CD0 File Offset: 0x000C5ED0
		public static MonitorDefinition CreateOutOfSlaMonitorDefinition(string mask, TracingContext tracingContext, TbaOutOfSlaAlertType alertType)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("mask", mask);
			ArgumentValidator.ThrowIfNull("tracingContext", tracingContext);
			string name;
			if (alertType == TbaOutOfSlaAlertType.Urgent)
			{
				name = TimeBasedAssistantsOutOfSlaMonitor.AssistantsOutOfSlaUrgentMonitorName;
			}
			else
			{
				name = TimeBasedAssistantsOutOfSlaMonitor.AssistantsOutOfSlaScheduledMonitorName;
			}
			MonitorDefinition monitorDefinition = OverallConsecutiveProbeFailuresMonitor.CreateDefinition(name, mask, ExchangeComponent.TimeBasedAssistants.Name, ExchangeComponent.TimeBasedAssistants, TimeBasedAssistantsOutOfSlaMonitor.AssistantsOutOfSlaMonitorThreshold, true, TimeBasedAssistantsOutOfSlaMonitor.AssistantsOutOfSlaMonitoringInterval);
			monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Unhealthy, 0),
				new MonitorStateTransition(ServiceHealthStatus.Unrecoverable, monitorDefinition.RecurrenceIntervalSeconds / 2)
			};
			monitorDefinition.ServicePriority = 1;
			monitorDefinition.ScenarioDescription = "Validate Time Based Assistatns are meeting their goal.";
			return monitorDefinition;
		}

		// Token: 0x060020C4 RID: 8388 RVA: 0x000C7D68 File Offset: 0x000C5F68
		public static ResponderDefinition CreateOutOfSlaEscalateResponderDefinition(string monitorName, string alertMask, TbaOutOfSlaAlertType alertType)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("monitorName", monitorName);
			ArgumentValidator.ThrowIfNullOrWhiteSpace("alertMask", alertMask);
			string name;
			if (alertType == TbaOutOfSlaAlertType.Urgent)
			{
				name = TimeBasedAssistantsOutOfSlaMonitor.AssistantsOutOfSlaUrgentEscalateResponderName;
			}
			else
			{
				name = TimeBasedAssistantsOutOfSlaMonitor.AssistantsOutOfSlaScheduledEscalateResponderName;
			}
			ResponderDefinition responderDefinition = EscalateResponder.CreateDefinition(name, ExchangeComponent.TimeBasedAssistants.Name, monitorName, alertMask, "MSExchangeMailboxAssistants", ServiceHealthStatus.Unrecoverable, ExchangeComponent.TimeBasedAssistants.EscalationTeam, Strings.AssistantsOutOfSlaSubject, Strings.AssistantsOutOfSlaMessage, true, NotificationServiceClass.UrgentInTraining, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
			responderDefinition.WaitIntervalSeconds = TimeBasedAssistantsOutOfSlaMonitor.AssistantsOutOfSlaEscalateResponderWaitInterval;
			return responderDefinition;
		}

		// Token: 0x0400180C RID: 6156
		private const string AssistantsOutOfSlaUrgentWorkItemName = "TbaOutOfSlaUrgent";

		// Token: 0x0400180D RID: 6157
		private const string AssistantsOutOfSlaScheduledWorkItemName = "TbaOutOfSlaScheduled";

		// Token: 0x0400180E RID: 6158
		private static readonly string AssistantsOutOfSlaUrgentProbeName = TimeBasedAssistantsDiscoveryHelpers.GenerateProbeName("TbaOutOfSlaUrgent");

		// Token: 0x0400180F RID: 6159
		private static readonly string AssistantsOutOfSlaUrgentMonitorName = TimeBasedAssistantsDiscoveryHelpers.GenerateMonitorName("TbaOutOfSlaUrgent");

		// Token: 0x04001810 RID: 6160
		private static readonly string AssistantsOutOfSlaUrgentEscalateResponderName = TimeBasedAssistantsDiscoveryHelpers.GenerateResponderName("TbaOutOfSlaUrgent", "Escalate");

		// Token: 0x04001811 RID: 6161
		private static readonly string AssistantsOutOfSlaScheduledProbeName = TimeBasedAssistantsDiscoveryHelpers.GenerateProbeName("TbaOutOfSlaScheduled");

		// Token: 0x04001812 RID: 6162
		private static readonly string AssistantsOutOfSlaScheduledMonitorName = TimeBasedAssistantsDiscoveryHelpers.GenerateMonitorName("TbaOutOfSlaScheduled");

		// Token: 0x04001813 RID: 6163
		private static readonly string AssistantsOutOfSlaScheduledEscalateResponderName = TimeBasedAssistantsDiscoveryHelpers.GenerateResponderName("TbaOutOfSlaScheduled", "Escalate");

		// Token: 0x04001814 RID: 6164
		private static int AssistantsOutOfSlaProbeMaxAttempts = 3;

		// Token: 0x04001815 RID: 6165
		private static int AssistantsOutOfSlaMonitorThreshold = 4;

		// Token: 0x04001816 RID: 6166
		private static int AssistantsOutOfSlaProbeRecurrence = (int)TimeSpan.FromMinutes(20.0).TotalSeconds;

		// Token: 0x04001817 RID: 6167
		private static int AssistantsOutOfSlaMonitoringInterval = (int)TimeSpan.FromHours(2.0).TotalSeconds;

		// Token: 0x04001818 RID: 6168
		private static int AssistantsOutOfSlaEscalateResponderWaitInterval = (int)TimeSpan.FromHours(8.0).TotalSeconds;
	}
}
