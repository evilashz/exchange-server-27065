using System;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.ActiveMonitoring.Responders;
using Microsoft.Exchange.Monitoring.ActiveMonitoring.Local;
using Microsoft.Office.Datacenter.ActiveMonitoring;
using Microsoft.Office.Datacenter.Monitoring.ActiveMonitoring.Recovery;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.SiteMailbox
{
	// Token: 0x020004D3 RID: 1235
	internal class ServiceHostAvailabilityWorkItem
	{
		// Token: 0x06001EB6 RID: 7862 RVA: 0x000B88DC File Offset: 0x000B6ADC
		public static ProbeDefinition CreateProbeDefinition(MaintenanceDefinition discoveryDefinition)
		{
			TimeSpan timeSpan = TimeSpan.Parse(discoveryDefinition.Attributes["ServiceHostServiceProbeFrequency"]);
			bool enabled = bool.Parse(discoveryDefinition.Attributes["ServiceHostServiceProbeEnabled"]);
			return new ProbeDefinition
			{
				TypeName = typeof(GenericServiceProbe).FullName,
				AssemblyPath = typeof(GenericServiceProbe).Assembly.Location,
				Enabled = enabled,
				MaxRetryAttempts = 3,
				Name = "ExchangeServiceHostServiceRunningProbe",
				RecurrenceIntervalSeconds = (int)timeSpan.TotalSeconds,
				ServiceName = ExchangeComponent.SiteMailbox.Name,
				TargetResource = "MSExchangeServiceHost",
				TimeoutSeconds = (int)timeSpan.TotalSeconds
			};
		}

		// Token: 0x06001EB7 RID: 7863 RVA: 0x000B899C File Offset: 0x000B6B9C
		public static MonitorDefinition CreateMonitorDefinition(MaintenanceDefinition discoveryDefinition)
		{
			int num = (int)TimeSpan.Parse(discoveryDefinition.Attributes["ServiceHostServiceProbeFrequency"]).TotalSeconds;
			int num2 = int.Parse(discoveryDefinition.Attributes["ServiceHostServiceMonitorThreshold"]);
			int monitoringInterval = num * num2;
			TimeSpan t = TimeSpan.Parse(discoveryDefinition.Attributes["ServiceHostServiceTimeToRestart"]);
			TimeSpan timeSpan = t + TimeSpan.FromMinutes(30.0);
			TimeSpan timeSpan2 = TimeSpan.Parse(discoveryDefinition.Attributes["ServiceHostServiceTimeToEscalate"]);
			MonitorDefinition monitorDefinition = OverallXFailuresMonitor.CreateDefinition("ExchangeServiceHostServiceRunningMonitor", "ExchangeServiceHostServiceRunningProbe", ExchangeComponent.SiteMailbox.Name, ExchangeComponent.SiteMailbox, monitoringInterval, num, num2, true);
			monitorDefinition.MonitorStateTransitions = new MonitorStateTransition[]
			{
				new MonitorStateTransition(ServiceHealthStatus.Degraded, (int)t.TotalSeconds),
				new MonitorStateTransition(ServiceHealthStatus.Unhealthy, (int)timeSpan.TotalSeconds),
				new MonitorStateTransition(ServiceHealthStatus.Unrecoverable, (int)timeSpan2.TotalSeconds)
			};
			monitorDefinition.ServicePriority = 2;
			monitorDefinition.ScenarioDescription = "Validate SiteMailbox health is not impacted by service host availability issues";
			return monitorDefinition;
		}

		// Token: 0x06001EB8 RID: 7864 RVA: 0x000B8AA8 File Offset: 0x000B6CA8
		public static ResponderDefinition CreateRecoveryResponderDefinition()
		{
			string monitorName = string.Format("{0}/{1}", "ExchangeServiceHostServiceRunningMonitor", ExchangeComponent.SiteMailbox.Name);
			ResponderDefinition responderDefinition = RestartServiceResponder.CreateDefinition("ExchangeServiceHostServiceRestart", monitorName, "MSExchangeServiceHost", ServiceHealthStatus.Degraded, 15, 120, 0, false, DumpMode.None, null, 15.0, 0, "Exchange", null, true, true, "Dag", false);
			responderDefinition.ServiceName = ExchangeComponent.SiteMailbox.Name;
			return responderDefinition;
		}

		// Token: 0x06001EB9 RID: 7865 RVA: 0x000B8B14 File Offset: 0x000B6D14
		public static ResponderDefinition CreateRecovery2ResponderDefinition()
		{
			string monitorName = string.Format("{0}/{1}", "ExchangeServiceHostServiceRunningMonitor", ExchangeComponent.SiteMailbox.Name);
			ResponderDefinition responderDefinition = RestartServiceResponder.CreateDefinition("ExchangeServiceHostServiceRestart2", monitorName, "MSExchangeServiceHost", ServiceHealthStatus.Unhealthy, 15, 120, 0, false, DumpMode.None, null, 15.0, 0, "Exchange", null, true, true, "Dag", false);
			responderDefinition.ServiceName = ExchangeComponent.SiteMailbox.Name;
			return responderDefinition;
		}

		// Token: 0x06001EBA RID: 7866 RVA: 0x000B8B80 File Offset: 0x000B6D80
		public static ResponderDefinition CreateEscalateResponderDefinition()
		{
			string alertMask = string.Format("{0}/{1}", "ExchangeServiceHostServiceRunningMonitor", ExchangeComponent.SiteMailbox.Name);
			string escalationSubjectUnhealthy = Strings.ServiceNotRunningEscalationSubject("MSExchangeServiceHost");
			string escalationMessageUnhealthy;
			if (Datacenter.IsMicrosoftHostedOnly(true))
			{
				escalationMessageUnhealthy = Strings.ServiceNotRunningEscalationMessageDc("MSExchangeServiceHost");
			}
			else
			{
				escalationMessageUnhealthy = Strings.ServiceNotRunningEscalationMessageEnt("MSExchangeServiceHost");
			}
			return EscalateResponder.CreateDefinition("ExchangeServiceHostRunningEscalate", ExchangeComponent.SiteMailbox.Name, "ExchangeServiceHostServiceRunningMonitor", alertMask, "MSExchangeServiceHost", ServiceHealthStatus.Unrecoverable, ExchangeComponent.SiteMailbox.EscalationTeam, escalationSubjectUnhealthy, escalationMessageUnhealthy, true, NotificationServiceClass.Urgent, 14400, "Pacific Standard Time/Monday,Tuesday,Wednesday,Thursday,Friday,Saturday,Sunday/00:00/23:59", false);
		}

		// Token: 0x040015DC RID: 5596
		private const string ServiceHostServiceName = "MSExchangeServiceHost";

		// Token: 0x040015DD RID: 5597
		private const string ServiceHostServiceProbeEnabledName = "ServiceHostServiceProbeEnabled";

		// Token: 0x040015DE RID: 5598
		private const string ServiceHostServiceProbeFrequencyName = "ServiceHostServiceProbeFrequency";

		// Token: 0x040015DF RID: 5599
		private const string ServiceHostServiceMonitorThresholdName = "ServiceHostServiceMonitorThreshold";

		// Token: 0x040015E0 RID: 5600
		private const string RestartTransitionSpanName = "ServiceHostServiceTimeToRestart";

		// Token: 0x040015E1 RID: 5601
		private const string EscalateTransitionSpanName = "ServiceHostServiceTimeToEscalate";

		// Token: 0x040015E2 RID: 5602
		private const string ServiceHostServiceProbeName = "ExchangeServiceHostServiceRunningProbe";

		// Token: 0x040015E3 RID: 5603
		private const string ServiceHostServiceMonitorName = "ExchangeServiceHostServiceRunningMonitor";

		// Token: 0x040015E4 RID: 5604
		private const string ServiceHostServiceRestartResponderName = "ExchangeServiceHostServiceRestart";

		// Token: 0x040015E5 RID: 5605
		private const string ServiceHostEscalateResponderName = "ExchangeServiceHostRunningEscalate";
	}
}
