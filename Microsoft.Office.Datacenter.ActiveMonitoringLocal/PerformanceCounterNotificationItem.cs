using System;

namespace Microsoft.Office.Datacenter.ActiveMonitoring
{
	// Token: 0x0200008F RID: 143
	internal class PerformanceCounterNotificationItem : NotificationItem
	{
		// Token: 0x0600073F RID: 1855 RVA: 0x0001DA40 File Offset: 0x0001BC40
		internal PerformanceCounterNotificationItem(string counterName, double counterValue, DateTime timeStamp) : base(PerformanceCounterNotificationItem.edsNotificationServiceName, PerformanceCounterNotificationItem.PerformanceCounterComponentName, counterName, counterName, ResultSeverityLevel.Informational)
		{
			base.SampleValue = counterValue;
			base.CustomProperties.Add("CollectionTime", timeStamp.ToString());
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000740 RID: 1856 RVA: 0x0001DA79 File Offset: 0x0001BC79
		internal static string PerformanceCounterComponentName
		{
			get
			{
				return "Performance Counter";
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000741 RID: 1857 RVA: 0x0001DA80 File Offset: 0x0001BC80
		internal static string PerformanceCounterAnalyzerName
		{
			get
			{
				return "PerfLogActiveMonitoringAnalyzer";
			}
		}

		// Token: 0x06000742 RID: 1858 RVA: 0x0001DA87 File Offset: 0x0001BC87
		public static string GenerateResultName(string perfCounterName)
		{
			return NotificationItem.GenerateResultName(PerformanceCounterNotificationItem.edsNotificationServiceName, PerformanceCounterNotificationItem.PerformanceCounterComponentName, perfCounterName);
		}

		// Token: 0x0400048B RID: 1163
		private static readonly string edsNotificationServiceName = ExchangeComponent.Eds.Name;
	}
}
