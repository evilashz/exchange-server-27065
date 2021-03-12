using System;
using Microsoft.Exchange.Data.Storage.LinkedFolder;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.SiteMailbox
{
	// Token: 0x020004D6 RID: 1238
	internal class SiteMailboxSyncSuccessWorkItem
	{
		// Token: 0x06001EC7 RID: 7879 RVA: 0x000B8FAC File Offset: 0x000B71AC
		public static MonitorDefinition CreateMonitorDefinition(MaintenanceDefinition discoveryDefinition)
		{
			bool enabled = bool.Parse(discoveryDefinition.Attributes["SiteMailboxSyncSuccessMonitorEnabled"]);
			double availabilityPercentage = double.Parse(discoveryDefinition.Attributes["SiteMailboxSyncSuccessMonitorThreshold"]);
			TimeSpan monitoringInterval = TimeSpan.Parse(discoveryDefinition.Attributes["SiteMailboxSyncSuccessMonitorInterval"]);
			int minimumErrorCount = int.Parse(discoveryDefinition.Attributes["SiteMailboxSyncSuccessMonitorMinErrorCount"]);
			TimeSpan zero = TimeSpan.Zero;
			TimeSpan timeSpan = TimeSpan.Parse(discoveryDefinition.Attributes["SiteMailboxSyncSuccessTimeToEscalate"]);
			string name = ExchangeComponent.SiteMailbox.Name;
			string name2 = typeof(SiteSynchronizer).Name;
			string sampleMask = NotificationItem.GenerateResultName(name, name2, null);
			MonitorDefinition monitorDefinition = OverallPercentSuccessMonitor.CreateDefinition("SiteMailboxDocumentSyncPercentSuccessMonitor", sampleMask, ExchangeComponent.SiteMailbox.Name, ExchangeComponent.SiteMailbox, availabilityPercentage, monitoringInterval, minimumErrorCount, true);
			monitorDefinition.Enabled = enabled;
			monitorDefinition.TargetResource = monitorDefinition.ServiceName;
			monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Degraded, (int)zero.TotalSeconds),
				new MonitorStateTransition(ServiceHealthStatus.Unrecoverable, (int)timeSpan.TotalSeconds)
			};
			monitorDefinition.ServicePriority = 2;
			monitorDefinition.ScenarioDescription = "Validate SiteMailbox health is not impacted by document sync issues";
			return monitorDefinition;
		}

		// Token: 0x06001EC8 RID: 7880 RVA: 0x000B90DC File Offset: 0x000B72DC
		public static ResponderDefinition CreateEscalateResponderDefinition(MaintenanceDefinition discoveryDefinition)
		{
			string name = typeof(DocumentLibSynchronizer).Name;
			double num = double.Parse(discoveryDefinition.Attributes["SiteMailboxSyncSuccessMonitorThreshold"]);
			string alertMask = string.Format("{0}/{1}", "SiteMailboxDocumentSyncPercentSuccessMonitor", ExchangeComponent.SiteMailbox.Name);
			string escalationMessageUnhealthy = Strings.SiteMailboxDocumentSyncEscalationMessage(Convert.ToInt32(100.0 - num));
			return EscalateResponder.CreateDefinition("SiteMailboxDocumentSyncEscalate", ExchangeComponent.SiteMailbox.Name, "SiteMailboxDocumentSyncPercentSuccessMonitor", alertMask, name, ServiceHealthStatus.Unrecoverable, ExchangeComponent.SiteMailbox.EscalationTeam, Strings.SiteMailboxDocumentSyncEscalationSubject, escalationMessageUnhealthy, true, NotificationServiceClass.Urgent, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
		}

		// Token: 0x040015EF RID: 5615
		private const string MonitorEnabledAttributeName = "SiteMailboxSyncSuccessMonitorEnabled";

		// Token: 0x040015F0 RID: 5616
		private const string MonitorThresholdAttributeName = "SiteMailboxSyncSuccessMonitorThreshold";

		// Token: 0x040015F1 RID: 5617
		private const string MonitorIntervalAttributeName = "SiteMailboxSyncSuccessMonitorInterval";

		// Token: 0x040015F2 RID: 5618
		private const string MonitorMinErrorCountAttributeName = "SiteMailboxSyncSuccessMonitorMinErrorCount";

		// Token: 0x040015F3 RID: 5619
		private const string EscalateTimeAttributeName = "SiteMailboxSyncSuccessTimeToEscalate";

		// Token: 0x040015F4 RID: 5620
		private const string SiteMailboxDocSyncPercentMonitorName = "SiteMailboxDocumentSyncPercentSuccessMonitor";

		// Token: 0x040015F5 RID: 5621
		private const string SiteMailboxDocSyncEscalateResponderName = "SiteMailboxDocumentSyncEscalate";
	}
}
