using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.PublicFolders
{
	// Token: 0x02000409 RID: 1033
	internal class PFMailboxConnectionWorkItem
	{
		// Token: 0x06001A2C RID: 6700 RVA: 0x0008E2B0 File Offset: 0x0008C4B0
		public static IEnumerable<MonitorDefinition> GenerateMonitorDefinitions(MaintenanceDefinition discoveryDefinition)
		{
			yield return PFMailboxConnectionWorkItem.CreateMonitorDefinition(discoveryDefinition);
			yield break;
		}

		// Token: 0x06001A2D RID: 6701 RVA: 0x0008E38C File Offset: 0x0008C58C
		public static IEnumerable<ResponderDefinition> GenerateResponderDefinitions(MaintenanceDefinition discoveryDefinition)
		{
			yield return PFMailboxConnectionWorkItem.CreatePFConnectionResponderDefinition();
			yield break;
		}

		// Token: 0x06001A2E RID: 6702 RVA: 0x0008E3A4 File Offset: 0x0008C5A4
		public static MonitorDefinition CreateMonitorDefinition(MaintenanceDefinition discoveryDefinition)
		{
			bool enabled = bool.Parse(discoveryDefinition.Attributes["PFMailboxConnectionCountMonitorEnabled"]);
			int monitoringInterval = (int)TimeSpan.Parse(discoveryDefinition.Attributes["PFMailboxConnectionCountMonitorInterval"]).TotalSeconds;
			int recurrenceInterval = (int)TimeSpan.Parse(discoveryDefinition.Attributes["PFMailboxConnectionCountMonitorRecurranceInterval"]).TotalSeconds;
			int numberOfFailures = int.Parse(discoveryDefinition.Attributes["PFMailboxConnectionCountMonitorMinErrorCount"]);
			TimeSpan timeSpan = TimeSpan.Parse(discoveryDefinition.Attributes["PFMailboxConnectionCountTimeToEscalate"]);
			if (ExEnvironment.IsTest)
			{
				recurrenceInterval = (int)TimeSpan.FromSeconds(10.0).TotalSeconds;
				timeSpan = TimeSpan.FromSeconds(5.0);
				monitoringInterval = (int)TimeSpan.FromSeconds(60.0).TotalSeconds;
			}
			string name = ExchangeComponent.PublicFolders.Name;
			string sampleMask = NotificationItem.GenerateResultName(name, "PublicFolderMailboxConnectionCount", null);
			MonitorDefinition monitorDefinition = OverallXFailuresMonitor.CreateDefinition("PFMailboxConnectionCountMonitor", sampleMask, ExchangeComponent.PublicFolders.Name, ExchangeComponent.PublicFolders, monitoringInterval, recurrenceInterval, numberOfFailures, true);
			monitorDefinition.Enabled = enabled;
			monitorDefinition.TargetResource = monitorDefinition.ServiceName;
			monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Unrecoverable, (int)timeSpan.TotalSeconds)
			};
			monitorDefinition.ServicePriority = 2;
			monitorDefinition.ScenarioDescription = "Validate PublicFolders health is not impacted by mailbox connectivity issues";
			return monitorDefinition;
		}

		// Token: 0x06001A2F RID: 6703 RVA: 0x0008E50C File Offset: 0x0008C70C
		public static ResponderDefinition CreatePFConnectionResponderDefinition()
		{
			ResponderDefinition connectionResponderWorkItemDefinition = PFMailboxConnectionWorkItem.GetConnectionResponderWorkItemDefinition();
			if (ExEnvironment.IsTest)
			{
				connectionResponderWorkItemDefinition.TargetHealthState = ServiceHealthStatus.None;
				connectionResponderWorkItemDefinition.RecurrenceIntervalSeconds = (int)TimeSpan.FromSeconds(20.0).TotalSeconds;
			}
			return connectionResponderWorkItemDefinition;
		}

		// Token: 0x06001A30 RID: 6704 RVA: 0x0008E54C File Offset: 0x0008C74C
		public static ResponderDefinition GetConnectionResponderWorkItemDefinition()
		{
			ResponderDefinition result;
			if (LocalEndpointManager.IsDataCenter)
			{
				result = PublicFolderConnectionResponder.CreateDefinition("PFMailboxConnectionCountResponder", ExchangeComponent.PublicFolders.Name, "PFMailboxConnectionCountMonitor", string.Format("{0}/{1}", "PFMailboxConnectionCountMonitor", ExchangeComponent.PublicFolders.Name), "PublicFolderMailbox.ConnectionCount", ServiceHealthStatus.Unrecoverable, true);
			}
			else
			{
				result = EscalateResponder.CreateDefinition("PFMailboxConnectionCountResponder", ExchangeComponent.PublicFolders.Name, "PFMailboxConnectionCountMonitor", string.Format("{0}/{1}", "PFMailboxConnectionCountMonitor", ExchangeComponent.PublicFolders.Name), "PublicFolderMailbox.ConnectionCount", ServiceHealthStatus.Unrecoverable, ExchangeComponent.PublicFolders.EscalationTeam, Strings.PublicFolderConnectionCountEscalationSubject, Strings.EscalationMessageFailuresUnhealthy(Strings.PublicFolderConnectionCountEscalationMessage), true, NotificationServiceClass.Scheduled, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
			}
			return result;
		}

		// Token: 0x040011CF RID: 4559
		private const string MonitorEnabledAttributeName = "PFMailboxConnectionCountMonitorEnabled";

		// Token: 0x040011D0 RID: 4560
		private const string MonitorIntervalAttributeName = "PFMailboxConnectionCountMonitorInterval";

		// Token: 0x040011D1 RID: 4561
		private const string MonitorRecurranceIntervalAttributeName = "PFMailboxConnectionCountMonitorRecurranceInterval";

		// Token: 0x040011D2 RID: 4562
		private const string MonitorMinErrorCountAttributeName = "PFMailboxConnectionCountMonitorMinErrorCount";

		// Token: 0x040011D3 RID: 4563
		private const string EscalateTimeAttributeName = "PFMailboxConnectionCountTimeToEscalate";

		// Token: 0x040011D4 RID: 4564
		private const string ConnectionCountMonitorName = "PFMailboxConnectionCountMonitor";

		// Token: 0x040011D5 RID: 4565
		private const string ConnectionCountResponderName = "PFMailboxConnectionCountResponder";

		// Token: 0x040011D6 RID: 4566
		private const string ConnectionCountNameMask = "PublicFolderMailboxConnectionCount";
	}
}
