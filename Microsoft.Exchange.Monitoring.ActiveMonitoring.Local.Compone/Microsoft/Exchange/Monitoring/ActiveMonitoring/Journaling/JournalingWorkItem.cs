using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Journaling
{
	// Token: 0x020001E7 RID: 487
	internal class JournalingWorkItem
	{
		// Token: 0x06000D83 RID: 3459 RVA: 0x0005BC44 File Offset: 0x00059E44
		public static MonitorDefinition CreateMonitorDefinition(MaintenanceDefinition discoveryDefinition)
		{
			bool enabled = bool.Parse(discoveryDefinition.Attributes["JournalingMonitorEnabled"]);
			int monitoringInterval = (int)TimeSpan.Parse(discoveryDefinition.Attributes["JournalingMonitorInterval"]).TotalSeconds;
			int recurrenceInterval = (int)TimeSpan.Parse(discoveryDefinition.Attributes["JournalingMonitorRecurranceInterval"]).TotalSeconds;
			if (ExEnvironment.IsTest)
			{
				recurrenceInterval = (int)TimeSpan.FromSeconds(60.0).TotalSeconds;
			}
			int numberOfFailures = int.Parse(discoveryDefinition.Attributes["JournalingMonitorMinErrorCount"]);
			TimeSpan zero = TimeSpan.Zero;
			TimeSpan timeSpan = TimeSpan.Parse(discoveryDefinition.Attributes["JournalingTimeToEscalate"]);
			string name = ExchangeComponent.Compliance.Name;
			string sampleMask = NotificationItem.GenerateResultName(name, "JournalAgent", null);
			MonitorDefinition monitorDefinition = OverallXFailuresMonitor.CreateDefinition("JournanlingMonitor", sampleMask, ExchangeComponent.Compliance.Name, ExchangeComponent.Compliance, monitoringInterval, recurrenceInterval, numberOfFailures, enabled);
			monitorDefinition.TargetResource = monitorDefinition.ServiceName;
			monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Unrecoverable, (int)timeSpan.TotalSeconds)
			};
			return monitorDefinition;
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x0005BD6C File Offset: 0x00059F6C
		public static ResponderDefinition CreateEscalateResponderDefinition(MaintenanceDefinition discoveryDefinition)
		{
			string alertMask = string.Format("{0}/{1}", "JournanlingMonitor", ExchangeComponent.Compliance.Name);
			ResponderDefinition responderDefinition = EscalateResponder.CreateDefinition("JournalingEscalateResponder", ExchangeComponent.Compliance.Name, "JournanlingMonitor", alertMask, "Compliance", ServiceHealthStatus.Unrecoverable, ExchangeComponent.Compliance.EscalationTeam, Strings.JournalingEscalationSubject, Strings.JournalingEscalationMessage, true, NotificationServiceClass.Urgent, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
			responderDefinition.Enabled = true;
			responderDefinition.NotificationServiceClass = NotificationServiceClass.UrgentInTraining;
			return responderDefinition;
		}

		// Token: 0x04000A1B RID: 2587
		private const string MonitorEnabledAttributeName = "JournalingMonitorEnabled";

		// Token: 0x04000A1C RID: 2588
		private const string MonitorIntervalAttributeName = "JournalingMonitorInterval";

		// Token: 0x04000A1D RID: 2589
		private const string MonitorRecurranceIntervalAttributeName = "JournalingMonitorRecurranceInterval";

		// Token: 0x04000A1E RID: 2590
		private const string MonitorMinErrorCountAttributeName = "JournalingMonitorMinErrorCount";

		// Token: 0x04000A1F RID: 2591
		private const string EscalateTimeAttributeName = "JournalingTimeToEscalate";

		// Token: 0x04000A20 RID: 2592
		private const string MonitorName = "JournanlingMonitor";

		// Token: 0x04000A21 RID: 2593
		private const string ResponderName = "JournalingEscalateResponder";
	}
}
