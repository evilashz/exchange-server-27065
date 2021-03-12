using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MessagingPolicies.UnJournalAgent
{
	// Token: 0x02000002 RID: 2
	internal static class PerfCounters
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public static void GetPerfCounterInfo(XElement element)
		{
			if (PerfCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in PerfCounters.AllCounters)
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

		// Token: 0x04000001 RID: 1
		public const string CategoryName = "MSExchange UnJournaling Agent";

		// Token: 0x04000002 RID: 2
		public static readonly ExPerformanceCounter MessagesProcessedPerHour = new ExPerformanceCounter("MSExchange UnJournaling Agent", "Messages Processed by Unjournaling per hour", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000003 RID: 3
		public static readonly ExPerformanceCounter MessagesUnjournaled = new ExPerformanceCounter("MSExchange UnJournaling Agent", "Messages Unjournaled successfully by Unjournaling", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000004 RID: 4
		public static readonly ExPerformanceCounter MessagesUnjournaledPerHour = new ExPerformanceCounter("MSExchange UnJournaling Agent", "Messages Unjournaled successfully by Unjournaling per hour", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000005 RID: 5
		public static readonly ExPerformanceCounter UsersUnjournaled = new ExPerformanceCounter("MSExchange UnJournaling Agent", "Unjournaled messages recipients by Unjournaling", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000006 RID: 6
		public static readonly ExPerformanceCounter UsersUnjournaledPerHour = new ExPerformanceCounter("MSExchange UnJournaling Agent", "Unjournaled messages recipients by Unjournaling per hour", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000007 RID: 7
		public static readonly ExPerformanceCounter DefectiveJournals = new ExPerformanceCounter("MSExchange UnJournaling Agent", "Messages marked as defective journals by Unjournaling", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000008 RID: 8
		public static readonly ExPerformanceCounter DefectiveJournalsPerHour = new ExPerformanceCounter("MSExchange UnJournaling Agent", "Messages marked as defective journals by Unjournaling per hour", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000009 RID: 9
		private static readonly ExPerformanceCounter AverageProcessingTime = new ExPerformanceCounter("MSExchange UnJournaling Agent", "UnJournaling Processing Time per Message", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400000A RID: 10
		public static readonly ExPerformanceCounter ProcessingTime = new ExPerformanceCounter("MSExchange UnJournaling Agent", "UnJournaling Processing Time", string.Empty, null, new ExPerformanceCounter[]
		{
			PerfCounters.AverageProcessingTime
		});

		// Token: 0x0400000B RID: 11
		private static readonly ExPerformanceCounter AverageProcessingTimeBase = new ExPerformanceCounter("MSExchange UnJournaling Agent", "Average unjournaling processing time/message base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400000C RID: 12
		public static readonly ExPerformanceCounter MessagesProcessed = new ExPerformanceCounter("MSExchange UnJournaling Agent", "Messages Processed by Unjournaling", string.Empty, null, new ExPerformanceCounter[]
		{
			PerfCounters.AverageProcessingTimeBase
		});

		// Token: 0x0400000D RID: 13
		private static readonly ExPerformanceCounter MessagesUnjournaledSizePerSecond = new ExPerformanceCounter("MSExchange UnJournaling Agent", "Messages Unjournaled size per second", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400000E RID: 14
		public static readonly ExPerformanceCounter TotalMessagesUnjournaledSize = new ExPerformanceCounter("MSExchange UnJournaling Agent", "Total Messages Unjournaled Size in bytes.", string.Empty, null, new ExPerformanceCounter[]
		{
			PerfCounters.MessagesUnjournaledSizePerSecond
		});

		// Token: 0x0400000F RID: 15
		public static readonly ExPerformanceCounter PermanentError = new ExPerformanceCounter("MSExchange UnJournaling Agent", "Permanent error by Unjournaling", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000010 RID: 16
		public static readonly ExPerformanceCounter PermanentErrorPerHour = new ExPerformanceCounter("MSExchange UnJournaling Agent", "Permanent error by Unjournaling per hour", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000011 RID: 17
		public static readonly ExPerformanceCounter TransientError = new ExPerformanceCounter("MSExchange UnJournaling Agent", "Transient error by Unjournaling", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000012 RID: 18
		public static readonly ExPerformanceCounter TransientErrorPerHour = new ExPerformanceCounter("MSExchange UnJournaling Agent", "Transient error by Unjournaling per hour", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000013 RID: 19
		public static readonly ExPerformanceCounter NdrProcessSuccess = new ExPerformanceCounter("MSExchange UnJournaling Agent", "Ndr successfully processed by Unjournaling", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000014 RID: 20
		public static readonly ExPerformanceCounter NdrProcessSuccessPerHour = new ExPerformanceCounter("MSExchange UnJournaling Agent", "Ndr successfully processed by Unjournaling per hour", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000015 RID: 21
		public static readonly ExPerformanceCounter LegacyArchiveJournallingDisabled = new ExPerformanceCounter("MSExchange UnJournaling Agent", "Legacy archive journalling disabled detected by Unjournaling", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000016 RID: 22
		public static readonly ExPerformanceCounter LegacyArchiveJournallingDisabledPerHour = new ExPerformanceCounter("MSExchange UnJournaling Agent", "Legacy archive journalling disabled detected by Unjournaling per hour", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000017 RID: 23
		public static readonly ExPerformanceCounter NonJournalMsgFromLegacyArchiveCustomer = new ExPerformanceCounter("MSExchange UnJournaling Agent", "Teed message detected by Unjournaling", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000018 RID: 24
		public static readonly ExPerformanceCounter NonJournalMsgFromLegacyArchiveCustomerPerHour = new ExPerformanceCounter("MSExchange UnJournaling Agent", "Teed message detected by Unjournaling per hour", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000019 RID: 25
		public static readonly ExPerformanceCounter AlreadyProcessed = new ExPerformanceCounter("MSExchange UnJournaling Agent", "Messages already processed detected by Unjournaling", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400001A RID: 26
		public static readonly ExPerformanceCounter AlreadyProcessedPerHour = new ExPerformanceCounter("MSExchange UnJournaling Agent", "Messages already processed detected by Unjournaling per hour", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400001B RID: 27
		public static readonly ExPerformanceCounter DropJournalReportWithoutNdr = new ExPerformanceCounter("MSExchange UnJournaling Agent", "Messages dropped silently by Unjournaling", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400001C RID: 28
		public static readonly ExPerformanceCounter DropJournalReportWithoutNdrPerHour = new ExPerformanceCounter("MSExchange UnJournaling Agent", "Messages dropped silently by Unjournaling per hour", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400001D RID: 29
		public static readonly ExPerformanceCounter NoUsersResolved = new ExPerformanceCounter("MSExchange UnJournaling Agent", "Messages with user unable to resolved by Unjournaling", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400001E RID: 30
		public static readonly ExPerformanceCounter NoUsersResolvedPerHour = new ExPerformanceCounter("MSExchange UnJournaling Agent", "Messages with user unable to resolved by Unjournaling per hour", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400001F RID: 31
		public static readonly ExPerformanceCounter NdrJournalReport = new ExPerformanceCounter("MSExchange UnJournaling Agent", "Messages ndr back to onpremise address by Unjournaling", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000020 RID: 32
		public static readonly ExPerformanceCounter NdrJournalReportPerHour = new ExPerformanceCounter("MSExchange UnJournaling Agent", "Messages ndr back to onpremise address by Unjournaling per hour", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000021 RID: 33
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			PerfCounters.MessagesProcessed,
			PerfCounters.MessagesProcessedPerHour,
			PerfCounters.MessagesUnjournaled,
			PerfCounters.MessagesUnjournaledPerHour,
			PerfCounters.UsersUnjournaled,
			PerfCounters.UsersUnjournaledPerHour,
			PerfCounters.TotalMessagesUnjournaledSize,
			PerfCounters.DefectiveJournals,
			PerfCounters.DefectiveJournalsPerHour,
			PerfCounters.ProcessingTime,
			PerfCounters.PermanentError,
			PerfCounters.PermanentErrorPerHour,
			PerfCounters.TransientError,
			PerfCounters.TransientErrorPerHour,
			PerfCounters.NdrProcessSuccess,
			PerfCounters.NdrProcessSuccessPerHour,
			PerfCounters.LegacyArchiveJournallingDisabled,
			PerfCounters.LegacyArchiveJournallingDisabledPerHour,
			PerfCounters.NonJournalMsgFromLegacyArchiveCustomer,
			PerfCounters.NonJournalMsgFromLegacyArchiveCustomerPerHour,
			PerfCounters.AlreadyProcessed,
			PerfCounters.AlreadyProcessedPerHour,
			PerfCounters.DropJournalReportWithoutNdr,
			PerfCounters.DropJournalReportWithoutNdrPerHour,
			PerfCounters.NoUsersResolved,
			PerfCounters.NoUsersResolvedPerHour,
			PerfCounters.NdrJournalReport,
			PerfCounters.NdrJournalReportPerHour
		};
	}
}
