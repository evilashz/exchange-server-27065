using System;
using System.Collections.Generic;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.PublicFolders
{
	// Token: 0x02000411 RID: 1041
	internal class PublicFolderSyncSuccessWorkItem
	{
		// Token: 0x06001A75 RID: 6773 RVA: 0x00091504 File Offset: 0x0008F704
		public static IEnumerable<MonitorDefinition> GenerateMonitorDefinitions(MaintenanceDefinition discoveryDefinition)
		{
			yield return PublicFolderSyncSuccessWorkItem.CreateMonitorDefinition(discoveryDefinition);
			yield break;
		}

		// Token: 0x06001A76 RID: 6774 RVA: 0x000915F0 File Offset: 0x0008F7F0
		public static IEnumerable<ResponderDefinition> GenerateResponderDefinitions(MaintenanceDefinition discoveryDefinition)
		{
			yield return PublicFolderSyncSuccessWorkItem.CreateEscalateResponderDefinition(discoveryDefinition);
			yield break;
		}

		// Token: 0x06001A77 RID: 6775 RVA: 0x00091610 File Offset: 0x0008F810
		public static MonitorDefinition CreateMonitorDefinition(MaintenanceDefinition discoveryDefinition)
		{
			bool enabled = bool.Parse(discoveryDefinition.Attributes["PublicFolderSyncSuccessMonitorEnabled"]);
			int monitoringInterval = (int)TimeSpan.Parse(discoveryDefinition.Attributes["PublicFolderSyncSuccessMonitorInterval"]).TotalSeconds;
			int recurrenceInterval = (int)TimeSpan.Parse(discoveryDefinition.Attributes["PublicFolderSyncSuccessRecurrenceInterval"]).TotalSeconds;
			int numberOfFailures = int.Parse(discoveryDefinition.Attributes["PublicFolderSyncSuccessMonitorMinErrorCount"]);
			TimeSpan timeSpan = TimeSpan.Parse(discoveryDefinition.Attributes["PublicFolderSyncSuccessTimeToEscalate"]);
			string name = ExchangeComponent.PublicFolders.Name;
			string component = "PublicFolderMailboxSync";
			string sampleMask = NotificationItem.GenerateResultName(name, component, null);
			MonitorDefinition monitorDefinition = OverallXFailuresMonitor.CreateDefinition("PublicFolderSyncFailureMonitor", sampleMask, ExchangeComponent.PublicFolders.Name, ExchangeComponent.PublicFolders, monitoringInterval, recurrenceInterval, numberOfFailures, true);
			monitorDefinition.Enabled = enabled;
			monitorDefinition.TargetResource = monitorDefinition.ServiceName;
			monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Unrecoverable, (int)timeSpan.TotalSeconds)
			};
			monitorDefinition.ServicePriority = 2;
			monitorDefinition.ScenarioDescription = "Validate PublicFolder health is not impacted by sync issues";
			return monitorDefinition;
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x00091734 File Offset: 0x0008F934
		public static ResponderDefinition CreateEscalateResponderDefinition(MaintenanceDefinition discoveryDefinition)
		{
			string targetResource = "PublicFolderMailboxSync";
			int durationMinutes = (int)TimeSpan.Parse(discoveryDefinition.Attributes["PublicFolderSyncSuccessMonitorInterval"]).TotalMinutes;
			int minCount = int.Parse(discoveryDefinition.Attributes["PublicFolderSyncSuccessMonitorMinErrorCount"]);
			string alertMask = string.Format("{0}/{1}", "PublicFolderSyncFailureMonitor", ExchangeComponent.PublicFolders.Name);
			string escalationMessageUnhealthy = Strings.PublicFolderSyncEscalationMessage(minCount, durationMinutes);
			return EscalateResponder.CreateDefinition("PublicFolderSyncFailureEscalate", ExchangeComponent.PublicFolders.Name, "PublicFolderSyncFailureMonitor", alertMask, targetResource, ServiceHealthStatus.Unrecoverable, ExchangeComponent.PublicFolders.EscalationTeam, Strings.PublicFolderSyncEscalationSubject, escalationMessageUnhealthy, true, NotificationServiceClass.Scheduled, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
		}

		// Token: 0x040011FC RID: 4604
		private const string MonitorEnabledAttributeName = "PublicFolderSyncSuccessMonitorEnabled";

		// Token: 0x040011FD RID: 4605
		private const string MonitorIntervalAttributeName = "PublicFolderSyncSuccessMonitorInterval";

		// Token: 0x040011FE RID: 4606
		private const string RecurrenceIntervalAttributeName = "PublicFolderSyncSuccessRecurrenceInterval";

		// Token: 0x040011FF RID: 4607
		private const string MonitorMinErrorCountAttributeName = "PublicFolderSyncSuccessMonitorMinErrorCount";

		// Token: 0x04001200 RID: 4608
		private const string EscalateTimeAttributeName = "PublicFolderSyncSuccessTimeToEscalate";

		// Token: 0x04001201 RID: 4609
		private const string MonitorName = "PublicFolderSyncFailureMonitor";

		// Token: 0x04001202 RID: 4610
		private const string EscalateResponderName = "PublicFolderSyncFailureEscalate";
	}
}
