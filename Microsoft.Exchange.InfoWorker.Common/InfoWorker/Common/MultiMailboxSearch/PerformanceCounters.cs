using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.InfoWorker.Common.MultiMailboxSearch
{
	// Token: 0x0200031C RID: 796
	internal static class PerformanceCounters
	{
		// Token: 0x0600176A RID: 5994 RVA: 0x0006DC08 File Offset: 0x0006BE08
		public static void GetPerfCounterInfo(XElement element)
		{
			if (PerformanceCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in PerformanceCounters.AllCounters)
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

		// Token: 0x04000F54 RID: 3924
		public const string CategoryName = "MSExchange MultiMailboxSearch";

		// Token: 0x04000F55 RID: 3925
		public static readonly ExPerformanceCounter AveragePreviewRequestProcessingTime = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "Average preview request processing time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F56 RID: 3926
		public static readonly ExPerformanceCounter AveragePreviewRequestProcessingTimeBase = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "Base for Average preview request processing time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F57 RID: 3927
		public static readonly ExPerformanceCounter AverageStatisticsRequestProcessingTime = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "Average statistics request processing time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F58 RID: 3928
		public static readonly ExPerformanceCounter AverageStatisticsRequestProcessingTimeBase = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "Base for Average statistics request processing time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F59 RID: 3929
		public static readonly ExPerformanceCounter AveragePreviewAndStatisticsRequestProcessingTime = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "Availability Requests (sec)", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F5A RID: 3930
		public static readonly ExPerformanceCounter AveragePreviewAndStatisticsRequestProcessingTimeBase = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "Base for preview and statistics request processing time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F5B RID: 3931
		public static readonly ExPerformanceCounter TotalRequestsPerSecond = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "Total search Requests (sec)", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F5C RID: 3932
		public static readonly ExPerformanceCounter PreviewRequestsPerSecond = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "Preview search Requests (sec)", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F5D RID: 3933
		public static readonly ExPerformanceCounter StatisticsRequestsPerSecond = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "Statistics Requests (sec)", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F5E RID: 3934
		public static readonly ExPerformanceCounter PreviewAndStatisticsRequestsPerSecond = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "Preview and Statistics Requests (sec)", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F5F RID: 3935
		public static readonly ExPerformanceCounter AverageKeywordsCountPerQuery = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "Average Number of Keywords in a query", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F60 RID: 3936
		public static readonly ExPerformanceCounter AverageKeywordsCountPerQueryBase = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "Base for Average Number of Keywords in a query", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F61 RID: 3937
		public static readonly ExPerformanceCounter AverageMailboxCountPerQuery = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "Average Number of Mailboxes Processed per query", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F62 RID: 3938
		public static readonly ExPerformanceCounter AverageMailboxCountPerQueryBase = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "Base for Average Number of Mailboxes Processed per query", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F63 RID: 3939
		public static readonly ExPerformanceCounter AverageFailedMailboxesInPreview = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "Average failed mailboxes in preview", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F64 RID: 3940
		public static readonly ExPerformanceCounter AverageFailedMailboxesInPreviewBase = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "Base for Average failed mailboxes in preview", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F65 RID: 3941
		public static readonly ExPerformanceCounter AverageFailedMailboxesInStatistics = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "Average failed mailbox statistics search", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F66 RID: 3942
		public static readonly ExPerformanceCounter AverageFailedMailboxesInStatisticsBase = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "Base for Average failed mailbox statistics search", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F67 RID: 3943
		public static readonly ExPerformanceCounter AveragePreviewSearchPerMailboxProcessingTime = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "Average preview search time per mailbox", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F68 RID: 3944
		public static readonly ExPerformanceCounter AveragePreviewSearchPerMailboxProcessingTimeBase = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "Base for Average preview search time per mailbox", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F69 RID: 3945
		public static readonly ExPerformanceCounter AverageStatisticsSearchPerMailboxProcessingTime = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "Average statistics search time per mailbox", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F6A RID: 3946
		public static readonly ExPerformanceCounter AverageStatisticsSearchPerMailboxProcessingTimeBase = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "Base for Average statistics search time per mailbox", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F6B RID: 3947
		public static readonly ExPerformanceCounter AverageMailboxSearchedPerDatabase = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "Average Mailbox per database", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F6C RID: 3948
		public static readonly ExPerformanceCounter AverageMailboxSearchedPerDatabaseBase = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "Base for Average mailbox per database", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F6D RID: 3949
		public static readonly ExPerformanceCounter AverageDatabaseSearchedPerServer = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "Average database per server", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F6E RID: 3950
		public static readonly ExPerformanceCounter AverageDatabaseSearchedPerServerBase = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "Base for Average database per server", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F6F RID: 3951
		public static readonly ExPerformanceCounter AverageSearchGroupCreated = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "Average number of search group created", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F70 RID: 3952
		public static readonly ExPerformanceCounter AverageSearchGroupCreatedBase = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "Base for average number of search group created", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F71 RID: 3953
		public static readonly ExPerformanceCounter TotalSearches = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "Total searches", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F72 RID: 3954
		public static readonly ExPerformanceCounter TotalSearchesInProgress = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "Total searches in progress", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F73 RID: 3955
		public static readonly ExPerformanceCounter AverageLocalSearchGroupCreated = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "Average number of local search group created", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F74 RID: 3956
		public static readonly ExPerformanceCounter AverageLocalSearchGroupCreatedBase = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "Base for average number of local search group created", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F75 RID: 3957
		public static readonly ExPerformanceCounter AverageFanOutSearchGroupCreated = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "Average number of remote search group created", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F76 RID: 3958
		public static readonly ExPerformanceCounter AverageFanOutSearchGroupCreatedBase = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "Base for average number of remote search group created", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F77 RID: 3959
		public static readonly ExPerformanceCounter TotalLocalSearches = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "Total local searches", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F78 RID: 3960
		public static readonly ExPerformanceCounter TotalLocalSearchesInProgress = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "Total local searches in progress", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F79 RID: 3961
		public static readonly ExPerformanceCounter TotalFanOutSearches = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "Total fan out searches", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F7A RID: 3962
		public static readonly ExPerformanceCounter TotalFanOutSearchesInProgress = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "Total fan out searches in progress", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F7B RID: 3963
		public static readonly ExPerformanceCounter TotalSearchesBelow5Mailboxes = new ExPerformanceCounter("MSExchange MultiMailboxSearch", " Searches with less than 5 mailboxes", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F7C RID: 3964
		public static readonly ExPerformanceCounter TotalSearchesBetween5To10Mailboxes = new ExPerformanceCounter("MSExchange MultiMailboxSearch", " Searches with 5 - 10 mailboxes", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F7D RID: 3965
		public static readonly ExPerformanceCounter TotalSearchesBetween10To50Mailboxes = new ExPerformanceCounter("MSExchange MultiMailboxSearch", " Searches with 10 - 50 mailboxes", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F7E RID: 3966
		public static readonly ExPerformanceCounter TotalSearchesBetween50To100Mailboxes = new ExPerformanceCounter("MSExchange MultiMailboxSearch", " Searches with 50 - 100 mailboxes", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F7F RID: 3967
		public static readonly ExPerformanceCounter TotalSearchesBetween100To400Mailboxes = new ExPerformanceCounter("MSExchange MultiMailboxSearch", " Searches with 100 - 400 mailboxes", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F80 RID: 3968
		public static readonly ExPerformanceCounter TotalSearchesBetween400To700Mailboxes = new ExPerformanceCounter("MSExchange MultiMailboxSearch", " Searches with 400 - 700 mailboxes", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F81 RID: 3969
		public static readonly ExPerformanceCounter TotalSearchesGreaterThan700Mailboxes = new ExPerformanceCounter("MSExchange MultiMailboxSearch", " Searches with greater than 700 mailboxes", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F82 RID: 3970
		public static readonly ExPerformanceCounter TotalSearchesWithNoKeywords = new ExPerformanceCounter("MSExchange MultiMailboxSearch", " Searches with no keywords", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F83 RID: 3971
		public static readonly ExPerformanceCounter TotalSearchesBetween1To10Keywords = new ExPerformanceCounter("MSExchange MultiMailboxSearch", " Searches with 1 - 10 keywords", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F84 RID: 3972
		public static readonly ExPerformanceCounter TotalSearchesBetween10To20Keywords = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "Preview Searches with 10 - 20 keywords", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F85 RID: 3973
		public static readonly ExPerformanceCounter TotalSearchesBetween20To50Keywords = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "Preview Searches with 20 - 50 keywords", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F86 RID: 3974
		public static readonly ExPerformanceCounter TotalSearchesBetween50To100Keywords = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "Preview Searches with 50 - 100 keywords", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F87 RID: 3975
		public static readonly ExPerformanceCounter TotalSearchesBetween100To300Keywords = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "Preview Searches with 100 - 300 keywords", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F88 RID: 3976
		public static readonly ExPerformanceCounter TotalSearchesGreaterThan300Keywords = new ExPerformanceCounter("MSExchange MultiMailboxSearch", " Searches with greater than 300 keywords", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F89 RID: 3977
		public static readonly ExPerformanceCounter TotalPreviewSearchesBelow500msec = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "Preview Searches completed in less than 500 ms", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F8A RID: 3978
		public static readonly ExPerformanceCounter TotalPreviewSearchesBetween500msecTo2sec = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "Preview Searches completed in 0.5-2 sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F8B RID: 3979
		public static readonly ExPerformanceCounter TotalPreviewSearchesBetween2To10sec = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "Preview Searches completed in 2-10 sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F8C RID: 3980
		public static readonly ExPerformanceCounter TotalPreviewSearchesBetween10SecTo60Sec = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "Preview Searches completed in 10-60 sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F8D RID: 3981
		public static readonly ExPerformanceCounter TotalPreviewSearchesBetween60SecTo120Sec = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "Preview Searches completed in 60-120 sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F8E RID: 3982
		public static readonly ExPerformanceCounter TotalPreviewSearchesGreaterThan120Seconds = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "Preview Searches completed in greater than 120 sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F8F RID: 3983
		public static readonly ExPerformanceCounter TotalPreviewSearchesWithRpcLatencyBelow500msec = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "RPC Latency of preview searches completed in less than 500 ms", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F90 RID: 3984
		public static readonly ExPerformanceCounter TotalPreviewSearchesWithRpcLatencyBetween500msecTo2sec = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "RPC Latency of preview searches completed in 0.5-2 sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F91 RID: 3985
		public static readonly ExPerformanceCounter TotalPreviewSearchesWithRpcLatencyBetween2To10sec = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "RPC Latency of preview searches completed in 2-10 sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F92 RID: 3986
		public static readonly ExPerformanceCounter TotalPreviewSearchesWithRpcLatencyBetween10SecTo60Sec = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "RPC Latency of preview searches completed in 10-60 sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F93 RID: 3987
		public static readonly ExPerformanceCounter TotalPreviewSearchesWithRpcLatencyBetween60SecTo120Sec = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "RPC Latency of preview searches completed in 60-120 sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F94 RID: 3988
		public static readonly ExPerformanceCounter TotalPreviewSearchesWithRpcLatencyGreaterThan120Seconds = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "RPC Latency of preview searches completed in greater than 120 sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F95 RID: 3989
		public static readonly ExPerformanceCounter TotalStatsSearchesBelow500msec = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "Stats Searches completed in less than 500 ms", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F96 RID: 3990
		public static readonly ExPerformanceCounter TotalStatsSearchesBetween500msecTo2sec = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "Stats Searches completed in 0.5-2 sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F97 RID: 3991
		public static readonly ExPerformanceCounter TotalStatsSearchesBetween2To10sec = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "Stats Searches completed in 2-10 sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F98 RID: 3992
		public static readonly ExPerformanceCounter TotalStatsSearchesBetween10SecTo60Sec = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "Stats Searches completed in 10-60 sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F99 RID: 3993
		public static readonly ExPerformanceCounter TotalStatsSearchesBetween60SecTo120Sec = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "Stats Searches completed in 60-120 sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F9A RID: 3994
		public static readonly ExPerformanceCounter TotalStatsSearchesGreaterThan120Seconds = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "Stats Searches completed in greater than 120 sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F9B RID: 3995
		public static readonly ExPerformanceCounter TotalStatsSearchesWithRpcLatencyBelow500msec = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "RPC Latency of stats searches completed in less than 500 ms", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F9C RID: 3996
		public static readonly ExPerformanceCounter TotalStatsSearchesWithRpcLatencyBetween500msecTo2sec = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "RPC Latency of stats searches completed in 0.5-2 sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F9D RID: 3997
		public static readonly ExPerformanceCounter TotalStatsSearchesWithRpcLatencyBetween2To10sec = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "RPC Latency of stats searches completed in 2-10 sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F9E RID: 3998
		public static readonly ExPerformanceCounter TotalStatsSearchesWithRpcLatencyBetween10SecTo60Sec = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "RPC Latency of stats searches completed in 10-60 sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000F9F RID: 3999
		public static readonly ExPerformanceCounter TotalStatsSearchesWithRpcLatencyBetween60SecTo120Sec = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "RPC Latency of stats searches completed in 60-120 sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000FA0 RID: 4000
		public static readonly ExPerformanceCounter TotalStatsSearchesWithRpcLatencyGreaterThan120Seconds = new ExPerformanceCounter("MSExchange MultiMailboxSearch", "RPC Latency of stats searches completed in greater than 120 sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000FA1 RID: 4001
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			PerformanceCounters.AveragePreviewRequestProcessingTime,
			PerformanceCounters.AveragePreviewRequestProcessingTimeBase,
			PerformanceCounters.AverageStatisticsRequestProcessingTime,
			PerformanceCounters.AverageStatisticsRequestProcessingTimeBase,
			PerformanceCounters.AveragePreviewAndStatisticsRequestProcessingTime,
			PerformanceCounters.AveragePreviewAndStatisticsRequestProcessingTimeBase,
			PerformanceCounters.TotalRequestsPerSecond,
			PerformanceCounters.PreviewRequestsPerSecond,
			PerformanceCounters.StatisticsRequestsPerSecond,
			PerformanceCounters.PreviewAndStatisticsRequestsPerSecond,
			PerformanceCounters.AverageKeywordsCountPerQuery,
			PerformanceCounters.AverageKeywordsCountPerQueryBase,
			PerformanceCounters.AverageMailboxCountPerQuery,
			PerformanceCounters.AverageMailboxCountPerQueryBase,
			PerformanceCounters.AverageFailedMailboxesInPreview,
			PerformanceCounters.AverageFailedMailboxesInPreviewBase,
			PerformanceCounters.AverageFailedMailboxesInStatistics,
			PerformanceCounters.AverageFailedMailboxesInStatisticsBase,
			PerformanceCounters.AveragePreviewSearchPerMailboxProcessingTime,
			PerformanceCounters.AveragePreviewSearchPerMailboxProcessingTimeBase,
			PerformanceCounters.AverageStatisticsSearchPerMailboxProcessingTime,
			PerformanceCounters.AverageStatisticsSearchPerMailboxProcessingTimeBase,
			PerformanceCounters.AverageMailboxSearchedPerDatabase,
			PerformanceCounters.AverageMailboxSearchedPerDatabaseBase,
			PerformanceCounters.AverageDatabaseSearchedPerServer,
			PerformanceCounters.AverageDatabaseSearchedPerServerBase,
			PerformanceCounters.AverageSearchGroupCreated,
			PerformanceCounters.AverageSearchGroupCreatedBase,
			PerformanceCounters.TotalSearches,
			PerformanceCounters.TotalSearchesInProgress,
			PerformanceCounters.AverageLocalSearchGroupCreated,
			PerformanceCounters.AverageLocalSearchGroupCreatedBase,
			PerformanceCounters.AverageFanOutSearchGroupCreated,
			PerformanceCounters.AverageFanOutSearchGroupCreatedBase,
			PerformanceCounters.TotalLocalSearches,
			PerformanceCounters.TotalLocalSearchesInProgress,
			PerformanceCounters.TotalFanOutSearches,
			PerformanceCounters.TotalFanOutSearchesInProgress,
			PerformanceCounters.TotalSearchesBelow5Mailboxes,
			PerformanceCounters.TotalSearchesBetween5To10Mailboxes,
			PerformanceCounters.TotalSearchesBetween10To50Mailboxes,
			PerformanceCounters.TotalSearchesBetween50To100Mailboxes,
			PerformanceCounters.TotalSearchesBetween100To400Mailboxes,
			PerformanceCounters.TotalSearchesBetween400To700Mailboxes,
			PerformanceCounters.TotalSearchesGreaterThan700Mailboxes,
			PerformanceCounters.TotalSearchesWithNoKeywords,
			PerformanceCounters.TotalSearchesBetween1To10Keywords,
			PerformanceCounters.TotalSearchesBetween10To20Keywords,
			PerformanceCounters.TotalSearchesBetween20To50Keywords,
			PerformanceCounters.TotalSearchesBetween50To100Keywords,
			PerformanceCounters.TotalSearchesBetween100To300Keywords,
			PerformanceCounters.TotalSearchesGreaterThan300Keywords,
			PerformanceCounters.TotalPreviewSearchesBelow500msec,
			PerformanceCounters.TotalPreviewSearchesBetween500msecTo2sec,
			PerformanceCounters.TotalPreviewSearchesBetween2To10sec,
			PerformanceCounters.TotalPreviewSearchesBetween10SecTo60Sec,
			PerformanceCounters.TotalPreviewSearchesBetween60SecTo120Sec,
			PerformanceCounters.TotalPreviewSearchesGreaterThan120Seconds,
			PerformanceCounters.TotalPreviewSearchesWithRpcLatencyBelow500msec,
			PerformanceCounters.TotalPreviewSearchesWithRpcLatencyBetween500msecTo2sec,
			PerformanceCounters.TotalPreviewSearchesWithRpcLatencyBetween2To10sec,
			PerformanceCounters.TotalPreviewSearchesWithRpcLatencyBetween10SecTo60Sec,
			PerformanceCounters.TotalPreviewSearchesWithRpcLatencyBetween60SecTo120Sec,
			PerformanceCounters.TotalPreviewSearchesWithRpcLatencyGreaterThan120Seconds,
			PerformanceCounters.TotalStatsSearchesBelow500msec,
			PerformanceCounters.TotalStatsSearchesBetween500msecTo2sec,
			PerformanceCounters.TotalStatsSearchesBetween2To10sec,
			PerformanceCounters.TotalStatsSearchesBetween10SecTo60Sec,
			PerformanceCounters.TotalStatsSearchesBetween60SecTo120Sec,
			PerformanceCounters.TotalStatsSearchesGreaterThan120Seconds,
			PerformanceCounters.TotalStatsSearchesWithRpcLatencyBelow500msec,
			PerformanceCounters.TotalStatsSearchesWithRpcLatencyBetween500msecTo2sec,
			PerformanceCounters.TotalStatsSearchesWithRpcLatencyBetween2To10sec,
			PerformanceCounters.TotalStatsSearchesWithRpcLatencyBetween10SecTo60Sec,
			PerformanceCounters.TotalStatsSearchesWithRpcLatencyBetween60SecTo120Sec,
			PerformanceCounters.TotalStatsSearchesWithRpcLatencyGreaterThan120Seconds
		};
	}
}
