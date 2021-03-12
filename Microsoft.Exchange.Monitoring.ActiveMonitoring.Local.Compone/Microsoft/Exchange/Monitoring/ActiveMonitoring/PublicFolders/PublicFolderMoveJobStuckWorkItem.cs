using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.PublicFolders
{
	// Token: 0x02000410 RID: 1040
	internal class PublicFolderMoveJobStuckWorkItem
	{
		// Token: 0x06001A70 RID: 6768 RVA: 0x000910E0 File Offset: 0x0008F2E0
		public static IEnumerable<MonitorDefinition> GenerateMonitorDefinitions(MaintenanceDefinition discoveryDefinition)
		{
			yield return PublicFolderMoveJobStuckWorkItem.CreateMonitorDefinition(discoveryDefinition);
			yield break;
		}

		// Token: 0x06001A71 RID: 6769 RVA: 0x000911CC File Offset: 0x0008F3CC
		public static IEnumerable<ResponderDefinition> GenerateResponderDefinitions(MaintenanceDefinition discoveryDefinition)
		{
			yield return PublicFolderMoveJobStuckWorkItem.CreateEscalateResponderDefinition(discoveryDefinition);
			yield break;
		}

		// Token: 0x06001A72 RID: 6770 RVA: 0x000911EC File Offset: 0x0008F3EC
		public static MonitorDefinition CreateMonitorDefinition(MaintenanceDefinition discoveryDefinition)
		{
			bool enabled = bool.Parse(discoveryDefinition.Attributes["PublicFolderMoveJobStuckMonitorEnabled"]);
			int monitoringInterval = (int)TimeSpan.Parse(discoveryDefinition.Attributes["PublicFolderMoveJobStuckMonitorInterval"]).TotalSeconds;
			int recurrenceInterval = (int)TimeSpan.Parse(discoveryDefinition.Attributes["PublicFolderMoveJobStuckRecurrenceInterval"]).TotalSeconds;
			int numberOfFailures = int.Parse(discoveryDefinition.Attributes["PublicFolderMoveJobStuckMonitorMinErrorCount"]);
			TimeSpan timeSpan = TimeSpan.Parse(discoveryDefinition.Attributes["PublicFolderMoveJobStuckTimeToEscalate"]);
			string name = ExchangeComponent.PublicFolders.Name;
			string component = "PFMoveJobStuck";
			string sampleMask = NotificationItem.GenerateResultName(name, component, null);
			if (ExEnvironment.IsTest)
			{
				recurrenceInterval = (int)TimeSpan.FromSeconds(10.0).TotalSeconds;
				timeSpan = TimeSpan.FromSeconds(5.0);
				monitoringInterval = (int)TimeSpan.FromSeconds(60.0).TotalSeconds;
			}
			MonitorDefinition monitorDefinition = OverallXFailuresMonitor.CreateDefinition("PublicFolderMoveJobStuckMonitor", sampleMask, ExchangeComponent.PublicFolders.Name, ExchangeComponent.PublicFolders, monitoringInterval, recurrenceInterval, numberOfFailures, true);
			monitorDefinition.Enabled = enabled;
			monitorDefinition.TargetResource = monitorDefinition.ServiceName;
			monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Unrecoverable, (int)timeSpan.TotalSeconds)
			};
			monitorDefinition.ServicePriority = 2;
			monitorDefinition.ScenarioDescription = "Validate PublicFolder health is not impacted by move job issues";
			return monitorDefinition;
		}

		// Token: 0x06001A73 RID: 6771 RVA: 0x00091358 File Offset: 0x0008F558
		public static ResponderDefinition CreateEscalateResponderDefinition(MaintenanceDefinition discoveryDefinition)
		{
			string targetResource = "PFMoveJobStuck";
			double totalMinutes = TimeSpan.Parse(discoveryDefinition.Attributes["PublicFolderMoveJobStuckMonitorInterval"]).TotalMinutes;
			int.Parse(discoveryDefinition.Attributes["PublicFolderMoveJobStuckMonitorMinErrorCount"]);
			string alertMask = string.Format("{0}/{1}", "PublicFolderMoveJobStuckMonitor", ExchangeComponent.PublicFolders.Name);
			ResponderDefinition responderDefinition = EscalateResponder.CreateDefinition("PublicFolderMoveJobStuckEscalate", ExchangeComponent.PublicFolders.Name, "PublicFolderMoveJobStuckMonitor", alertMask, targetResource, ServiceHealthStatus.Unrecoverable, ExchangeComponent.PublicFolders.EscalationTeam, Strings.PublicFolderMoveJobStuckEscalationSubject, Strings.PublicFolderMoveJobStuckEscalationMessage, true, NotificationServiceClass.Scheduled, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
			if (ExEnvironment.IsTest)
			{
				responderDefinition.TargetHealthState = ServiceHealthStatus.None;
				responderDefinition.RecurrenceIntervalSeconds = (int)TimeSpan.FromSeconds(20.0).TotalSeconds;
			}
			return responderDefinition;
		}

		// Token: 0x040011F5 RID: 4597
		private const string MonitorEnabledAttributeName = "PublicFolderMoveJobStuckMonitorEnabled";

		// Token: 0x040011F6 RID: 4598
		private const string MonitorIntervalAttributeName = "PublicFolderMoveJobStuckMonitorInterval";

		// Token: 0x040011F7 RID: 4599
		private const string RecurrenceIntervalAttributeName = "PublicFolderMoveJobStuckRecurrenceInterval";

		// Token: 0x040011F8 RID: 4600
		private const string MonitorMinErrorCountAttributeName = "PublicFolderMoveJobStuckMonitorMinErrorCount";

		// Token: 0x040011F9 RID: 4601
		private const string EscalateTimeAttributeName = "PublicFolderMoveJobStuckTimeToEscalate";

		// Token: 0x040011FA RID: 4602
		private const string MonitorName = "PublicFolderMoveJobStuckMonitor";

		// Token: 0x040011FB RID: 4603
		private const string EscalateResponderName = "PublicFolderMoveJobStuckEscalate";
	}
}
