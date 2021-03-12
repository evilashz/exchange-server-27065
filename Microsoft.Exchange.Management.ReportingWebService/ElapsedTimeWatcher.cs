using System;
using System.Web;

namespace Microsoft.Exchange.Management.ReportingWebService
{
	// Token: 0x02000002 RID: 2
	internal sealed class ElapsedTimeWatcher
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public static void Watch(RequestStatistics.RequestStatItem name, Action action)
		{
			DateTime utcNow = DateTime.UtcNow;
			action();
			if (HttpContext.Current != null && HttpContext.Current.Items != null)
			{
				RequestStatistics requestStatistics = HttpContext.Current.Items[RequestStatistics.RequestStatsKey] as RequestStatistics;
				if (requestStatistics != null)
				{
					requestStatistics.AddStatisticsDataPoint(name, utcNow, DateTime.UtcNow);
				}
			}
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002128 File Offset: 0x00000328
		public static void WatchStartTime(string eventName)
		{
			DateTime utcNow = DateTime.UtcNow;
			if (HttpContext.Current != null && HttpContext.Current.Items != null)
			{
				RequestStatistics requestStatistics = HttpContext.Current.Items[RequestStatistics.RequestStatsKey] as RequestStatistics;
				if (requestStatistics != null)
				{
					requestStatistics.AddExtendedStatisticsDataPoint(eventName, utcNow.ToString("hh:mm:ss.ffftt"));
				}
			}
		}

		// Token: 0x06000003 RID: 3 RVA: 0x00002180 File Offset: 0x00000380
		public static void WatchMessage(string messageName, string messageData)
		{
			if (HttpContext.Current != null && HttpContext.Current.Items != null)
			{
				RequestStatistics requestStatistics = HttpContext.Current.Items[RequestStatistics.RequestStatsKey] as RequestStatistics;
				if (requestStatistics != null)
				{
					requestStatistics.AddExtendedStatisticsDataPoint(messageName, messageData);
				}
			}
		}
	}
}
