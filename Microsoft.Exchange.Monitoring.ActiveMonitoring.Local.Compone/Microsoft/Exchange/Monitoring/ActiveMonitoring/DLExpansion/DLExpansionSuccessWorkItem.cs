using System;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.DLExpansion
{
	// Token: 0x02000160 RID: 352
	internal class DLExpansionSuccessWorkItem
	{
		// Token: 0x06000A03 RID: 2563 RVA: 0x0003F1C0 File Offset: 0x0003D3C0
		public static MonitorDefinition CreateMonitorDefinition(MaintenanceDefinition discoveryDefinition)
		{
			bool enabled = bool.Parse(discoveryDefinition.Attributes["DLExpansionMonitorEnabled"]);
			double availabilityPercentage = double.Parse(discoveryDefinition.Attributes["DLExpansionMonitorThreshold"]);
			TimeSpan monitoringInterval = TimeSpan.Parse(discoveryDefinition.Attributes["DLExpansionMonitorInterval"]);
			int minimumErrorCount = int.Parse(discoveryDefinition.Attributes["DLExpansionMonitorMinErrorCount"]);
			TimeSpan zero = TimeSpan.Zero;
			TimeSpan timeSpan = TimeSpan.Parse(discoveryDefinition.Attributes["DLExpansionTimeToEscalate"]);
			string name = ExchangeComponent.DLExpansion.Name;
			string sampleMask = NotificationItem.GenerateResultName(name, "DLExpansionComponent", null);
			MonitorDefinition monitorDefinition = OverallPercentSuccessMonitor.CreateDefinition("DLExpansionPercentMonitor", sampleMask, ExchangeComponent.DLExpansion.Name, ExchangeComponent.DLExpansion, availabilityPercentage, monitoringInterval, minimumErrorCount, true);
			monitorDefinition.Enabled = enabled;
			monitorDefinition.TargetResource = monitorDefinition.ServiceName;
			monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Degraded, (int)zero.TotalSeconds),
				new MonitorStateTransition(ServiceHealthStatus.Unrecoverable, (int)timeSpan.TotalSeconds)
			};
			return monitorDefinition;
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x0003F2CC File Offset: 0x0003D4CC
		public static ResponderDefinition CreateEscalateResponderDefinition(MaintenanceDefinition discoveryDefinition)
		{
			string alertMask = string.Format("{0}/{1}", "DLExpansionPercentMonitor", ExchangeComponent.DLExpansion.Name);
			return EscalateResponder.CreateDefinition("DLExpansionEscalateResponder", ExchangeComponent.DLExpansion.Name, "DLExpansionPercentMonitor", alertMask, "DLExpansion", ServiceHealthStatus.Unrecoverable, ExchangeComponent.DLExpansion.EscalationTeam, Strings.DLExpansionEscalationSubject, Strings.DLExpansionEscalationMessage, true, NotificationServiceClass.Scheduled, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
		}

		// Token: 0x04000724 RID: 1828
		private const string MonitorEnabledAttributeName = "DLExpansionMonitorEnabled";

		// Token: 0x04000725 RID: 1829
		private const string MonitorThresholdAttributeName = "DLExpansionMonitorThreshold";

		// Token: 0x04000726 RID: 1830
		private const string MonitorIntervalAttributeName = "DLExpansionMonitorInterval";

		// Token: 0x04000727 RID: 1831
		private const string MonitorMinErrorCountAttributeName = "DLExpansionMonitorMinErrorCount";

		// Token: 0x04000728 RID: 1832
		private const string EscalateTimeAttributeName = "DLExpansionTimeToEscalate";

		// Token: 0x04000729 RID: 1833
		private const string MonitorName = "DLExpansionPercentMonitor";

		// Token: 0x0400072A RID: 1834
		private const string ResponderName = "DLExpansionEscalateResponder";
	}
}
