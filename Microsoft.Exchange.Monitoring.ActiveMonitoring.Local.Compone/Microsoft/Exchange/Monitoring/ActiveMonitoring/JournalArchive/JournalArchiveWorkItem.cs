using System;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.JournalArchive
{
	// Token: 0x020001DE RID: 478
	internal class JournalArchiveWorkItem
	{
		// Token: 0x06000D50 RID: 3408 RVA: 0x0005AEA0 File Offset: 0x000590A0
		public static MonitorDefinition CreateMonitorDefinition(MaintenanceDefinition discoveryDefinition)
		{
			bool enabled = bool.Parse(discoveryDefinition.Attributes["JournalArchiveMonitorEnabled"]);
			int monitoringInterval = (int)TimeSpan.Parse(discoveryDefinition.Attributes["JournalArchiveMonitorInterval"]).TotalSeconds;
			int recurrenceInterval = (int)TimeSpan.Parse(discoveryDefinition.Attributes["JournalArchiveMonitorRecurrenceInterval"]).TotalSeconds;
			int numberOfFailures = int.Parse(discoveryDefinition.Attributes["JournalArchiveMonitorMinErrorCount"]);
			TimeSpan timeSpan = TimeSpan.Parse(discoveryDefinition.Attributes["JournalingTimeToEscalate"]);
			string name = ExchangeComponent.JournalArchive.Name;
			string sampleMask = NotificationItem.GenerateResultName(name, "JournalArchiveComponent", null);
			MonitorDefinition monitorDefinition = OverallXFailuresMonitor.CreateDefinition("JournalArchiveMonitor", sampleMask, ExchangeComponent.JournalArchive.Name, ExchangeComponent.JournalArchive, monitoringInterval, recurrenceInterval, numberOfFailures, true);
			monitorDefinition.Enabled = enabled;
			monitorDefinition.TargetResource = monitorDefinition.ServiceName;
			monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Unrecoverable, (int)timeSpan.TotalSeconds)
			};
			return monitorDefinition;
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x0005AFAC File Offset: 0x000591AC
		public static ResponderDefinition CreateEscalateResponderDefinition(MaintenanceDefinition discoveryDefinition)
		{
			string alertMask = string.Format("{0}/{1}", "JournalArchiveMonitor", ExchangeComponent.JournalArchive.Name);
			return JournalArchiveResponder.CreateDefinition("JournalArchiveResponder", ExchangeComponent.JournalArchive.Name, "JournalArchiveMonitor", alertMask, "JournalArchive", ServiceHealthStatus.Unrecoverable, ExchangeComponent.JournalArchive.EscalationTeam, Strings.JournalArchiveEscalationSubject, Strings.JournalArchiveEscalationMessage, NotificationServiceClass.UrgentInTraining, true);
		}

		// Token: 0x040009ED RID: 2541
		private const string MonitorEnabledAttributeName = "JournalArchiveMonitorEnabled";

		// Token: 0x040009EE RID: 2542
		private const string MonitorIntervalAttributeName = "JournalArchiveMonitorInterval";

		// Token: 0x040009EF RID: 2543
		private const string MonitorRecurranceIntervalAttributeName = "JournalArchiveMonitorRecurrenceInterval";

		// Token: 0x040009F0 RID: 2544
		private const string MonitorMinErrorCountAttributeName = "JournalArchiveMonitorMinErrorCount";

		// Token: 0x040009F1 RID: 2545
		private const string EscalateTimeAttributeName = "JournalingTimeToEscalate";

		// Token: 0x040009F2 RID: 2546
		private const string MonitorName = "JournalArchiveMonitor";

		// Token: 0x040009F3 RID: 2547
		private const string ResponderName = "JournalArchiveResponder";
	}
}
