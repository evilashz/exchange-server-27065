using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Journaling
{
	// Token: 0x020001E6 RID: 486
	internal class JournalFilterAgentWorkItem
	{
		// Token: 0x06000D80 RID: 3456 RVA: 0x0005BAA4 File Offset: 0x00059CA4
		public static MonitorDefinition CreateMonitorDefinition(MaintenanceDefinition discoveryDefinition)
		{
			bool enabled = bool.Parse(discoveryDefinition.Attributes["JournalFilterAgentMonitorEnabled"]);
			int monitoringInterval = (int)TimeSpan.Parse(discoveryDefinition.Attributes["JournalFilterAgentMonitorInterval"]).TotalSeconds;
			int recurrenceInterval = (int)TimeSpan.Parse(discoveryDefinition.Attributes["JournalFilterAgentMonitorRecurranceInterval"]).TotalSeconds;
			if (ExEnvironment.IsTest)
			{
				recurrenceInterval = (int)TimeSpan.FromSeconds(60.0).TotalSeconds;
			}
			int numberOfFailures = int.Parse(discoveryDefinition.Attributes["JournalFilterAgentMonitorMinErrorCount"]);
			TimeSpan timeSpan = TimeSpan.Parse(discoveryDefinition.Attributes["JournalFilterAgentTimeToEscalate"]);
			string name = ExchangeComponent.Compliance.Name;
			string sampleMask = NotificationItem.GenerateResultName(name, "JournalFilterAgentComponent", null);
			MonitorDefinition monitorDefinition = OverallXFailuresMonitor.CreateDefinition("JournalFilterAgentMonitor", sampleMask, ExchangeComponent.Compliance.Name, ExchangeComponent.Compliance, monitoringInterval, recurrenceInterval, numberOfFailures, enabled);
			monitorDefinition.TargetResource = monitorDefinition.ServiceName;
			monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Unrecoverable, (int)timeSpan.TotalSeconds)
			};
			return monitorDefinition;
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x0005BBC8 File Offset: 0x00059DC8
		public static ResponderDefinition CreateEscalateResponderDefinition(MaintenanceDefinition discoveryDefinition)
		{
			string alertMask = string.Format("{0}/{1}", "JournalFilterAgentMonitor", ExchangeComponent.Compliance.Name);
			return EscalateResponder.CreateDefinition("JournalFilterAgentResponder", ExchangeComponent.Compliance.Name, "JournalFilterAgentMonitor", alertMask, "Compliance", ServiceHealthStatus.Unrecoverable, ExchangeComponent.Compliance.EscalationTeam, Strings.JournalFilterAgentEscalationSubject, Strings.JournalFilterAgentEscalationMessage, true, NotificationServiceClass.UrgentInTraining, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
		}

		// Token: 0x04000A14 RID: 2580
		private const string MonitorEnabledAttributeName = "JournalFilterAgentMonitorEnabled";

		// Token: 0x04000A15 RID: 2581
		private const string MonitorIntervalAttributeName = "JournalFilterAgentMonitorInterval";

		// Token: 0x04000A16 RID: 2582
		private const string MonitorRecurranceIntervalAttributeName = "JournalFilterAgentMonitorRecurranceInterval";

		// Token: 0x04000A17 RID: 2583
		private const string MonitorMinErrorCountAttributeName = "JournalFilterAgentMonitorMinErrorCount";

		// Token: 0x04000A18 RID: 2584
		private const string EscalateTimeAttributeName = "JournalFilterAgentTimeToEscalate";

		// Token: 0x04000A19 RID: 2585
		private const string MonitorName = "JournalFilterAgentMonitor";

		// Token: 0x04000A1A RID: 2586
		private const string ResponderName = "JournalFilterAgentResponder";
	}
}
