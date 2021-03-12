using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.PublicFolders
{
	// Token: 0x0200040A RID: 1034
	internal class PFMailboxQuotaWorkItem
	{
		// Token: 0x06001A32 RID: 6706 RVA: 0x0008E6E0 File Offset: 0x0008C8E0
		public static IEnumerable<MonitorDefinition> GenerateMonitorDefinitions(MaintenanceDefinition discoveryDefinition)
		{
			yield return PFMailboxQuotaWorkItem.CreateMonitorDefinition(discoveryDefinition);
			yield break;
		}

		// Token: 0x06001A33 RID: 6707 RVA: 0x0008E7BC File Offset: 0x0008C9BC
		public static IEnumerable<ResponderDefinition> GenerateResponderDefinitions(MaintenanceDefinition discoveryDefinition)
		{
			yield return PFMailboxQuotaWorkItem.CreatePFMailboxQuotaResponderDefinition();
			yield break;
		}

		// Token: 0x06001A34 RID: 6708 RVA: 0x0008E7D4 File Offset: 0x0008C9D4
		public static MonitorDefinition CreateMonitorDefinition(MaintenanceDefinition discoveryDefinition)
		{
			bool enabled = bool.Parse(discoveryDefinition.Attributes["PFMailboxQuotaWarningMonitorEnabled"]);
			int monitoringInterval = (int)TimeSpan.Parse(discoveryDefinition.Attributes["PFMailboxQuotaWarningMonitorInterval"]).TotalSeconds;
			int recurrenceInterval = (int)TimeSpan.Parse(discoveryDefinition.Attributes["PFMailboxQuotaWarningMonitorRecurranceInterval"]).TotalSeconds;
			int numberOfFailures = int.Parse(discoveryDefinition.Attributes["PFMailboxQuotaWarningMonitorMinErrorCount"]);
			TimeSpan timeSpan = TimeSpan.Parse(discoveryDefinition.Attributes["PFMailboxQuotaWarningTimeToEscalate"]);
			if (ExEnvironment.IsTest)
			{
				recurrenceInterval = (int)TimeSpan.FromSeconds(10.0).TotalSeconds;
				timeSpan = TimeSpan.FromSeconds(5.0);
				monitoringInterval = (int)TimeSpan.FromSeconds(120.0).TotalSeconds;
			}
			string name = ExchangeComponent.PublicFolders.Name;
			string sampleMask = NotificationItem.GenerateResultName(name, "PublicFolderMailboxQuota", null);
			MonitorDefinition monitorDefinition = OverallXFailuresMonitor.CreateDefinition("PublicFolderMailboxQuotaMonitor", sampleMask, ExchangeComponent.PublicFolders.Name, ExchangeComponent.PublicFolders, monitoringInterval, recurrenceInterval, numberOfFailures, true);
			monitorDefinition.Enabled = enabled;
			monitorDefinition.TargetResource = monitorDefinition.ServiceName;
			monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Unrecoverable, (int)timeSpan.TotalSeconds)
			};
			monitorDefinition.ServicePriority = 2;
			monitorDefinition.ScenarioDescription = "Validate PublicFolders health is not impacted by mailbox quota issues";
			return monitorDefinition;
		}

		// Token: 0x06001A35 RID: 6709 RVA: 0x0008E93C File Offset: 0x0008CB3C
		public static ResponderDefinition CreatePFMailboxQuotaResponderDefinition()
		{
			ResponderDefinition mailboxQuotaResponderWorkItemDefiniteion = PFMailboxQuotaWorkItem.GetMailboxQuotaResponderWorkItemDefiniteion();
			if (ExEnvironment.IsTest)
			{
				mailboxQuotaResponderWorkItemDefiniteion.TargetHealthState = ServiceHealthStatus.None;
				mailboxQuotaResponderWorkItemDefiniteion.RecurrenceIntervalSeconds = (int)TimeSpan.FromSeconds(200.0).TotalSeconds;
				mailboxQuotaResponderWorkItemDefiniteion.MinimumSecondsBetweenEscalates = (int)TimeSpan.FromSeconds(5.0).TotalSeconds;
				mailboxQuotaResponderWorkItemDefiniteion.WaitIntervalSeconds = (int)TimeSpan.FromSeconds(5.0).TotalSeconds;
			}
			else
			{
				mailboxQuotaResponderWorkItemDefiniteion.RecurrenceIntervalSeconds = (int)TimeSpan.FromMinutes(30.0).TotalSeconds;
			}
			return mailboxQuotaResponderWorkItemDefiniteion;
		}

		// Token: 0x06001A36 RID: 6710 RVA: 0x0008E9D8 File Offset: 0x0008CBD8
		public static ResponderDefinition GetMailboxQuotaResponderWorkItemDefiniteion()
		{
			ResponderDefinition result;
			if (LocalEndpointManager.IsDataCenter)
			{
				result = PublicFolderMailboxResponder.CreateDefinition("PublicFolderMailboxQuotaResponder", ExchangeComponent.PublicFolders.Name, "PublicFolderMailboxQuotaMonitor", string.Format("{0}/{1}", "PublicFolderMailboxQuotaMonitor", ExchangeComponent.PublicFolders.Name), "PublicFolderMailbox.Quota", ServiceHealthStatus.Unrecoverable, true, NotificationServiceClass.Scheduled);
			}
			else
			{
				result = EscalateResponder.CreateDefinition("PublicFolderMailboxQuotaResponder", ExchangeComponent.PublicFolders.Name, "PublicFolderMailboxQuotaMonitor", string.Format("{0}/{1}", "PublicFolderMailboxQuotaMonitor", ExchangeComponent.PublicFolders.Name), "PublicFolderMailbox.Quota", ServiceHealthStatus.Unrecoverable, ExchangeComponent.PublicFolders.EscalationTeam, Strings.PublicFolderMailboxQuotaEscalationSubject, Strings.PublicFolderMailboxQuotaEscalationMessage, true, NotificationServiceClass.Scheduled, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
			}
			return result;
		}

		// Token: 0x040011D7 RID: 4567
		private const string MonitorEnabledAttributeName = "PFMailboxQuotaWarningMonitorEnabled";

		// Token: 0x040011D8 RID: 4568
		private const string MonitorIntervalAttributeName = "PFMailboxQuotaWarningMonitorInterval";

		// Token: 0x040011D9 RID: 4569
		private const string MonitorRecurranceIntervalAttributeName = "PFMailboxQuotaWarningMonitorRecurranceInterval";

		// Token: 0x040011DA RID: 4570
		private const string MonitorMinErrorCountAttributeName = "PFMailboxQuotaWarningMonitorMinErrorCount";

		// Token: 0x040011DB RID: 4571
		private const string EscalateTimeAttributeName = "PFMailboxQuotaWarningTimeToEscalate";

		// Token: 0x040011DC RID: 4572
		private const string MonitorName = "PublicFolderMailboxQuotaMonitor";

		// Token: 0x040011DD RID: 4573
		private const string PFMailboxQuotaResponderName = "PublicFolderMailboxQuotaResponder";

		// Token: 0x040011DE RID: 4574
		private const string PublicFolderMailboxQuotaNameMask = "PublicFolderMailboxQuota";
	}
}
