using System;
using Microsoft.Exchange.Data.Directory.SystemConfiguration;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.WorkerTaskFramework;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.TimeBasedAssistants
{
	// Token: 0x02000524 RID: 1316
	public static class InfrastructureValidationDiscovery
	{
		// Token: 0x0600205C RID: 8284 RVA: 0x000C60E4 File Offset: 0x000C42E4
		public static void ConfigureForTest()
		{
			InfrastructureValidationDiscovery.InfrastructureValidationProbeMaxAttempts = 1;
			InfrastructureValidationDiscovery.InfrastructureValidationMonitorThreshold = 1;
			InfrastructureValidationDiscovery.InfrastructureValidationProbeRecurrence = (int)TimeSpan.FromSeconds(30.0).TotalSeconds;
			InfrastructureValidationDiscovery.InfrastructureValidationMonitoringInterval = (int)TimeSpan.FromSeconds(30.0).TotalSeconds;
			InfrastructureValidationDiscovery.InfrastructureValidationEscalateResponderWaitInterval = (int)TimeSpan.FromSeconds(10.0).TotalSeconds;
		}

		// Token: 0x0600205D RID: 8285 RVA: 0x000C6151 File Offset: 0x000C4351
		public static ProbeDefinition CreateInfrastructureValidationProbeDefinition(TracingContext tracingContext)
		{
			ArgumentValidator.ThrowIfNull("tracingContext", tracingContext);
			return TimeBasedAssistantsDiscoveryHelpers.CreateProbe(ExchangeComponent.TimeBasedAssistants, "MSExchangeMailboxAssistants", LocalServer.GetServer().Fqdn, typeof(InfrastructureValidationProbe), InfrastructureValidationDiscovery.InfrastructureValidationFailureProbeName, InfrastructureValidationDiscovery.InfrastructureValidationProbeRecurrence, InfrastructureValidationDiscovery.InfrastructureValidationProbeMaxAttempts, tracingContext);
		}

		// Token: 0x0600205E RID: 8286 RVA: 0x000C6194 File Offset: 0x000C4394
		public static MonitorDefinition CreateInfrastructureValidationMonitorDefinition(string mask, TracingContext tracingContext)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("mask", mask);
			ArgumentValidator.ThrowIfNull("tracingContext", tracingContext);
			MonitorDefinition monitorDefinition = OverallConsecutiveProbeFailuresMonitor.CreateDefinition(InfrastructureValidationDiscovery.InfrastructureValidationMonitorName, mask, ExchangeComponent.TimeBasedAssistants.Name, ExchangeComponent.TimeBasedAssistants, InfrastructureValidationDiscovery.InfrastructureValidationMonitorThreshold, true, InfrastructureValidationDiscovery.InfrastructureValidationMonitoringInterval);
			monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Unhealthy, 0),
				new MonitorStateTransition(ServiceHealthStatus.Unrecoverable, monitorDefinition.RecurrenceIntervalSeconds / 2)
			};
			monitorDefinition.ServicePriority = 1;
			monitorDefinition.ScenarioDescription = "Find Time Based Assistants Infrastructure is not functional.";
			return monitorDefinition;
		}

		// Token: 0x0600205F RID: 8287 RVA: 0x000C621C File Offset: 0x000C441C
		public static ResponderDefinition CreateInfrastructureValidationEscalateResponderDefinition(string monitorName, string alertMask)
		{
			ArgumentValidator.ThrowIfNullOrWhiteSpace("monitorName", monitorName);
			ArgumentValidator.ThrowIfNullOrWhiteSpace("alertMask", alertMask);
			ResponderDefinition responderDefinition = EscalateResponder.CreateDefinition(InfrastructureValidationDiscovery.InfrastructureValidationEscalateResponderName, ExchangeComponent.TimeBasedAssistants.Name, monitorName, alertMask, "MSExchangeMailboxAssistants", ServiceHealthStatus.Unrecoverable, ExchangeComponent.TimeBasedAssistants.EscalationTeam, Strings.InfrastructureValidationSubject, Strings.InfrastructureValidationMessage, true, NotificationServiceClass.UrgentInTraining, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
			responderDefinition.WaitIntervalSeconds = InfrastructureValidationDiscovery.InfrastructureValidationEscalateResponderWaitInterval;
			return responderDefinition;
		}

		// Token: 0x040017C8 RID: 6088
		private const string InfrastructureValidationWorkItemName = "InfrastructureValidation";

		// Token: 0x040017C9 RID: 6089
		private static int InfrastructureValidationProbeMaxAttempts = 3;

		// Token: 0x040017CA RID: 6090
		private static int InfrastructureValidationMonitorThreshold = 10;

		// Token: 0x040017CB RID: 6091
		private static readonly string InfrastructureValidationFailureProbeName = TimeBasedAssistantsDiscoveryHelpers.GenerateProbeName("InfrastructureValidation");

		// Token: 0x040017CC RID: 6092
		private static readonly string InfrastructureValidationMonitorName = TimeBasedAssistantsDiscoveryHelpers.GenerateMonitorName("InfrastructureValidation");

		// Token: 0x040017CD RID: 6093
		private static readonly string InfrastructureValidationEscalateResponderName = TimeBasedAssistantsDiscoveryHelpers.GenerateResponderName("InfrastructureValidation", "Escalate");

		// Token: 0x040017CE RID: 6094
		private static int InfrastructureValidationProbeRecurrence = (int)TimeSpan.FromMinutes(30.0).TotalSeconds;

		// Token: 0x040017CF RID: 6095
		private static int InfrastructureValidationMonitoringInterval = (int)TimeSpan.FromHours(6.0).TotalSeconds;

		// Token: 0x040017D0 RID: 6096
		private static int InfrastructureValidationEscalateResponderWaitInterval = (int)TimeSpan.FromHours(12.0).TotalSeconds;
	}
}
