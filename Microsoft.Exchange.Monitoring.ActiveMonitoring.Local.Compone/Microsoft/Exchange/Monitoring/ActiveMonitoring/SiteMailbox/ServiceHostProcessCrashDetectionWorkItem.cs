using System;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local.Components.Common.Probes;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.SiteMailbox
{
	// Token: 0x020004D4 RID: 1236
	internal class ServiceHostProcessCrashDetectionWorkItem
	{
		// Token: 0x06001EBC RID: 7868 RVA: 0x000B8C24 File Offset: 0x000B6E24
		public static ProbeDefinition CreateProbeDefinition(MaintenanceDefinition discoveryDefinition)
		{
			int recurrenceInterval = (int)TimeSpan.Parse(discoveryDefinition.Attributes["ServiceHostCrashDetectionProbeFrequency"]).TotalSeconds;
			bool enabled = bool.Parse(discoveryDefinition.Attributes["ServiceHostCrashDetectionProbeEnabled"]);
			ProbeDefinition probeDefinition = GenericProcessCrashDetectionProbe.CreateDefinition("ExchangeServiceHostProcessRepeatedlyCrashingProbe", "Microsoft.Exchange.ServiceHost", recurrenceInterval, null, false);
			probeDefinition.Enabled = enabled;
			probeDefinition.MaxRetryAttempts = 3;
			probeDefinition.ServiceName = ExchangeComponent.SiteMailbox.Name;
			return probeDefinition;
		}

		// Token: 0x06001EBD RID: 7869 RVA: 0x000B8C98 File Offset: 0x000B6E98
		public static MonitorDefinition CreateMonitorDefinition(MaintenanceDefinition discoveryDefinition)
		{
			int recurrenceInterval = (int)TimeSpan.Parse(discoveryDefinition.Attributes["ServiceHostCrashDetectionProbeFrequency"]).TotalSeconds;
			int numberOfFailures = int.Parse(discoveryDefinition.Attributes["ServiceHostCrashDetectionMonitorThreshold"]);
			int monitoringInterval = (int)TimeSpan.Parse(discoveryDefinition.Attributes["ServiceHostCrashDetectionMonitorInterval"]).TotalSeconds;
			TimeSpan zero = TimeSpan.Zero;
			TimeSpan timeSpan = TimeSpan.Parse(discoveryDefinition.Attributes["ServiceHostCrashTimeToEscalate"]);
			MonitorDefinition monitorDefinition = OverallXFailuresMonitor.CreateDefinition("ExchangeServiceHostProcessRepeatedlyCrashingMonitor", string.Format("{0}/{1}", "ExchangeServiceHostProcessRepeatedlyCrashingProbe", "Microsoft.Exchange.ServiceHost"), ExchangeComponent.SiteMailbox.Name, ExchangeComponent.SiteMailbox, monitoringInterval, recurrenceInterval, numberOfFailures, true);
			monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Degraded, (int)zero.TotalSeconds),
				new MonitorStateTransition(ServiceHealthStatus.Unhealthy, (int)timeSpan.TotalSeconds)
			};
			monitorDefinition.ServicePriority = 2;
			monitorDefinition.ScenarioDescription = "Validate SiteMailbox health is not impacted by service host process crashe issues";
			return monitorDefinition;
		}

		// Token: 0x06001EBE RID: 7870 RVA: 0x000B8D98 File Offset: 0x000B6F98
		public static ResponderDefinition CreateEscalateResponderDefinition(MaintenanceDefinition discoveryDefinition)
		{
			int minCount = int.Parse(discoveryDefinition.Attributes["ServiceHostCrashDetectionMonitorThreshold"]);
			int durationMinutes = (int)TimeSpan.Parse(discoveryDefinition.Attributes["ServiceHostCrashDetectionMonitorInterval"]).TotalMinutes;
			string escalationMessageUnhealthy = Strings.ProcessRepeatedlyCrashingEscalationMessage("Microsoft.Exchange.ServiceHost", minCount, durationMinutes);
			string escalationSubjectUnhealthy = Strings.ProcessRepeatedlyCrashingEscalationSubject("Microsoft.Exchange.ServiceHost");
			return EscalateResponder.CreateDefinition("ExchangeServiceHostProcessRepeatedlyCrashingEscalate", ExchangeComponent.SiteMailbox.Name, "ExchangeServiceHostProcessRepeatedlyCrashingMonitor", string.Format("{0}/{1}", "ExchangeServiceHostProcessRepeatedlyCrashingMonitor", ExchangeComponent.SiteMailbox.Name), "Microsoft.Exchange.ServiceHost", ServiceHealthStatus.Unhealthy, ExchangeComponent.SiteMailbox.EscalationTeam, escalationSubjectUnhealthy, escalationMessageUnhealthy, true, NotificationServiceClass.Urgent, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
		}

		// Token: 0x040015E6 RID: 5606
		private const string ServiceHostProcessName = "Microsoft.Exchange.ServiceHost";

		// Token: 0x040015E7 RID: 5607
		private const string ServiceHostCrashDetectionProbeEnabledName = "ServiceHostCrashDetectionProbeEnabled";

		// Token: 0x040015E8 RID: 5608
		private const string ServiceHostCrashDetectionProbeFrequencyName = "ServiceHostCrashDetectionProbeFrequency";

		// Token: 0x040015E9 RID: 5609
		private const string ServiceHostCrashDetectionMonitorThresholdName = "ServiceHostCrashDetectionMonitorThreshold";

		// Token: 0x040015EA RID: 5610
		private const string ServiceHostCrashDetectionMonitorIntervalName = "ServiceHostCrashDetectionMonitorInterval";

		// Token: 0x040015EB RID: 5611
		private const string EscalateTransitionSpanName = "ServiceHostCrashTimeToEscalate";

		// Token: 0x040015EC RID: 5612
		private const string ServiceHostProcessRepeatedlyCrashingProbeName = "ExchangeServiceHostProcessRepeatedlyCrashingProbe";

		// Token: 0x040015ED RID: 5613
		private const string ServiceHostProcessRepeatedlyCrashingMonitorName = "ExchangeServiceHostProcessRepeatedlyCrashingMonitor";

		// Token: 0x040015EE RID: 5614
		private const string ServiceHostProcessRepeatedlyCrashingEscalateResponderName = "ExchangeServiceHostProcessRepeatedlyCrashingEscalate";
	}
}
