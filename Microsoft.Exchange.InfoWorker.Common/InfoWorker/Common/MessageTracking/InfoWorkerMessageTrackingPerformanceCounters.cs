using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.MessageTracking
{
	// Token: 0x02000319 RID: 793
	internal static class InfoWorkerMessageTrackingPerformanceCounters
	{
		// Token: 0x06001764 RID: 5988 RVA: 0x0006CE64 File Offset: 0x0006B064
		public static void GetPerfCounterInfo(XElement element)
		{
			if (InfoWorkerMessageTrackingPerformanceCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in InfoWorkerMessageTrackingPerformanceCounters.AllCounters)
			{
				try
				{
					element.Add(new XElement(ExPerformanceCounter.GetEncodedName(exPerformanceCounter.CounterName), exPerformanceCounter.NextValue()));
				}
				catch (XmlException ex)
				{
					XElement content = new XElement("Error", ex.Message);
					element.Add(content);
				}
			}
		}

		// Token: 0x04000F05 RID: 3845
		public const string CategoryName = "MSExchange Message Tracking";

		// Token: 0x04000F06 RID: 3846
		private static readonly ExPerformanceCounter SearchMessageTrackingReportExecutedRate = new ExPerformanceCounter("MSExchange Message Tracking", "Search-MessageTrackingReport task executed/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F07 RID: 3847
		private static readonly ExPerformanceCounter SearchMessageTrackingReportAverageProcessingTime = new ExPerformanceCounter("MSExchange Message Tracking", "Average Search-MessageTrackingReport Processing Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F08 RID: 3848
		public static readonly ExPerformanceCounter SearchMessageTrackingReportProcessingTime = new ExPerformanceCounter("MSExchange Message Tracking", "Search-MessageTrackingReport Processing Time", string.Empty, null, new ExPerformanceCounter[]
		{
			InfoWorkerMessageTrackingPerformanceCounters.SearchMessageTrackingReportAverageProcessingTime
		});

		// Token: 0x04000F09 RID: 3849
		private static readonly ExPerformanceCounter SearchMessageTrackingReportAverageProcessingTimeBase = new ExPerformanceCounter("MSExchange Message Tracking", "Average Search-MessageTrackingReport Processing Time base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F0A RID: 3850
		private static readonly ExPerformanceCounter SearchMessageTrackingReportAverageQueries = new ExPerformanceCounter("MSExchange Message Tracking", "Average Queries by Search-MessageTrackingReport", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F0B RID: 3851
		public static readonly ExPerformanceCounter SearchMessageTrackingReportQueries = new ExPerformanceCounter("MSExchange Message Tracking", "Total Queries by Search-MessageTrackingReport", string.Empty, null, new ExPerformanceCounter[]
		{
			InfoWorkerMessageTrackingPerformanceCounters.SearchMessageTrackingReportAverageQueries
		});

		// Token: 0x04000F0C RID: 3852
		private static readonly ExPerformanceCounter SearchMessageTrackingReportAverageQueriesBase = new ExPerformanceCounter("MSExchange Message Tracking", "Average Queries by Search-MessageTrackingReport base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F0D RID: 3853
		public static readonly ExPerformanceCounter SearchMessageTrackingReportExecuted = new ExPerformanceCounter("MSExchange Message Tracking", "Search-MessageTrackingReport task executed", string.Empty, null, new ExPerformanceCounter[]
		{
			InfoWorkerMessageTrackingPerformanceCounters.SearchMessageTrackingReportExecutedRate,
			InfoWorkerMessageTrackingPerformanceCounters.SearchMessageTrackingReportAverageProcessingTimeBase,
			InfoWorkerMessageTrackingPerformanceCounters.SearchMessageTrackingReportAverageQueriesBase
		});

		// Token: 0x04000F0E RID: 3854
		private static readonly ExPerformanceCounter GetMessageTrackingReportExecutedRate = new ExPerformanceCounter("MSExchange Message Tracking", "Get-MessageTrackingReport Task Executed/Sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F0F RID: 3855
		private static readonly ExPerformanceCounter GetMessageTrackingReportAverageProcessingTime = new ExPerformanceCounter("MSExchange Message Tracking", "Average Get-MessageTrackingReport Processing Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F10 RID: 3856
		public static readonly ExPerformanceCounter GetMessageTrackingReportProcessingTime = new ExPerformanceCounter("MSExchange Message Tracking", "Get-MessageTrackingReport Processing Time", string.Empty, null, new ExPerformanceCounter[]
		{
			InfoWorkerMessageTrackingPerformanceCounters.GetMessageTrackingReportAverageProcessingTime
		});

		// Token: 0x04000F11 RID: 3857
		private static readonly ExPerformanceCounter GetMessageTrackingReportAverageProcessingTimeBase = new ExPerformanceCounter("MSExchange Message Tracking", "Average Get-MessageTrackingReport Processing Time Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F12 RID: 3858
		private static readonly ExPerformanceCounter GetMessageTrackingReportAverageQueries = new ExPerformanceCounter("MSExchange Message Tracking", "Average Queries by Get-MessageTrackingReport", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F13 RID: 3859
		public static readonly ExPerformanceCounter GetMessageTrackingReportQueries = new ExPerformanceCounter("MSExchange Message Tracking", "Total Queries by Get-MessageTrackingReport", string.Empty, null, new ExPerformanceCounter[]
		{
			InfoWorkerMessageTrackingPerformanceCounters.GetMessageTrackingReportAverageQueries
		});

		// Token: 0x04000F14 RID: 3860
		private static readonly ExPerformanceCounter GetMessageTrackingReportAverageQueriesBase = new ExPerformanceCounter("MSExchange Message Tracking", "Average Queries by Get-MessageTrackingReport base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F15 RID: 3861
		public static readonly ExPerformanceCounter GetMessageTrackingReportExecuted = new ExPerformanceCounter("MSExchange Message Tracking", "Get-MessageTrackingReport Task Executed", string.Empty, null, new ExPerformanceCounter[]
		{
			InfoWorkerMessageTrackingPerformanceCounters.GetMessageTrackingReportExecutedRate,
			InfoWorkerMessageTrackingPerformanceCounters.GetMessageTrackingReportAverageProcessingTimeBase,
			InfoWorkerMessageTrackingPerformanceCounters.GetMessageTrackingReportAverageQueriesBase
		});

		// Token: 0x04000F16 RID: 3862
		public static readonly ExPerformanceCounter CurrentRequestDispatcherRequests = new ExPerformanceCounter("MSExchange Message Tracking", "MessageTracking current request dispatcher requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F17 RID: 3863
		public static readonly ExPerformanceCounter MessageTrackingFailureRateTask = new ExPerformanceCounter("MSExchange Message Tracking", "Percentage of MessageTrackingReport operations (via task) completed with errors", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F18 RID: 3864
		public static readonly ExPerformanceCounter MessageTrackingFailureRateWebService = new ExPerformanceCounter("MSExchange Message Tracking", "Percentage of MessageTrackingReport operations (via EWS) completed with errors", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F19 RID: 3865
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			InfoWorkerMessageTrackingPerformanceCounters.SearchMessageTrackingReportExecuted,
			InfoWorkerMessageTrackingPerformanceCounters.SearchMessageTrackingReportProcessingTime,
			InfoWorkerMessageTrackingPerformanceCounters.SearchMessageTrackingReportQueries,
			InfoWorkerMessageTrackingPerformanceCounters.GetMessageTrackingReportExecuted,
			InfoWorkerMessageTrackingPerformanceCounters.GetMessageTrackingReportProcessingTime,
			InfoWorkerMessageTrackingPerformanceCounters.GetMessageTrackingReportQueries,
			InfoWorkerMessageTrackingPerformanceCounters.CurrentRequestDispatcherRequests,
			InfoWorkerMessageTrackingPerformanceCounters.MessageTrackingFailureRateTask,
			InfoWorkerMessageTrackingPerformanceCounters.MessageTrackingFailureRateWebService
		};
	}
}
