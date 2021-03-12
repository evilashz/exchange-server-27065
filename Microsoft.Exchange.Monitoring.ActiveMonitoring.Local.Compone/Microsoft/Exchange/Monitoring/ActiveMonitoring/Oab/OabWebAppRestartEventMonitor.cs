using System;
using System.Diagnostics.Eventing.Reader;
using System.Reflection;
using System.Threading;
using Microsoft.Office.Datacenter.ActiveMonitoring;

namespace Microsoft.Exchange.Monitoring.ActiveMonitoring.Oab
{
	// Token: 0x02000243 RID: 579
	public class OabWebAppRestartEventMonitor : MonitorWorkItem
	{
		// Token: 0x06001021 RID: 4129 RVA: 0x0006C0EC File Offset: 0x0006A2EC
		public static MonitorDefinition CreateDefinition(string name, string sampleMask, Component component, int failureCount, int monitoringInterval, int recurrenceInterval)
		{
			return new MonitorDefinition
			{
				AssemblyPath = Assembly.GetExecutingAssembly().Location,
				TypeName = typeof(OabWebAppRestartEventMonitor).FullName,
				Name = name,
				SampleMask = sampleMask,
				Component = component,
				MonitoringThreshold = (double)failureCount,
				MonitoringIntervalSeconds = monitoringInterval,
				RecurrenceIntervalSeconds = recurrenceInterval,
				TimeoutSeconds = recurrenceInterval * 2,
				ServiceName = component.Name,
				MaxRetryAttempts = 3
			};
		}

		// Token: 0x06001022 RID: 4130 RVA: 0x0006C170 File Offset: 0x0006A370
		protected override void DoMonitorWork(CancellationToken cancellationToken)
		{
			string queryString = string.Format("<QueryList><Query Id=\"0\" Path=\"System\"><Select Path=\"System\">*[System[Provider[@Name='Microsoft-Windows-WAS'] and (EventID=5117) and (TimeCreated[@SystemTime&gt;='{0}' and @SystemTime&lt;='{1}'])] and (EventData/Data[@Name='AppPoolID']='MSExchangeOabAppPool')]</Select></Query></QueryList>", base.MonitoringWindowStartTime.ToString("o"), base.Result.ExecutionStartTime.ToString("o"));
			if (this.GetSystemEvent(queryString))
			{
				base.Result.IsAlert = true;
				NotificationItem notificationItem = new EventNotificationItem(ExchangeComponent.Oab.Name, ExchangeComponent.Oab.Name, null, "OABAppPoolTooManyRecycles", ResultSeverityLevel.Error);
				notificationItem.Publish(false);
			}
		}

		// Token: 0x06001023 RID: 4131 RVA: 0x0006C1F0 File Offset: 0x0006A3F0
		private bool GetSystemEvent(string queryString)
		{
			TimeSpan.FromSeconds(30.0);
			using (EventLogReader eventLogReader = new EventLogReader(new EventLogQuery("System", PathType.LogName, queryString)
			{
				ReverseDirection = true
			}))
			{
				base.Result.StateAttribute1 = eventLogReader.BatchSize.ToString();
				if (eventLogReader.BatchSize > OabWebAppRestartEventMonitor.noOfAppPoolRecyclesThreshold)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000C21 RID: 3105
		private static readonly int noOfAppPoolRecyclesThreshold = 3;
	}
}
