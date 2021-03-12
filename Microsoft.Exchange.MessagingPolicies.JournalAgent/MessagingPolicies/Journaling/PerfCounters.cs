using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MessagingPolicies.Journaling
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
		public const string CategoryName = "MSExchange Journaling Agent";

		// Token: 0x04000002 RID: 2
		private static readonly ExPerformanceCounter UsersJournaledRate = new ExPerformanceCounter("MSExchange Journaling Agent", "Users Journaled/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000003 RID: 3
		public static readonly ExPerformanceCounter UsersJournaled = new ExPerformanceCounter("MSExchange Journaling Agent", "Users Journaled", string.Empty, null, new ExPerformanceCounter[]
		{
			PerfCounters.UsersJournaledRate
		});

		// Token: 0x04000004 RID: 4
		public static readonly ExPerformanceCounter UsersJournaledPerHour = new ExPerformanceCounter("MSExchange Journaling Agent", "Users Journaled/hour", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000005 RID: 5
		public static readonly ExPerformanceCounter ReportsGeneratedWithRMSProtectedMessage = new ExPerformanceCounter("MSExchange Journaling Agent", "Journal Reports with RMS protected messages created", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000006 RID: 6
		private static readonly ExPerformanceCounter ReportsGeneratedRate = new ExPerformanceCounter("MSExchange Journaling Agent", "Journal Reports Created/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000007 RID: 7
		public static readonly ExPerformanceCounter ReportsGenerated = new ExPerformanceCounter("MSExchange Journaling Agent", "Journal Reports created Total", string.Empty, null, new ExPerformanceCounter[]
		{
			PerfCounters.ReportsGeneratedRate
		});

		// Token: 0x04000008 RID: 8
		public static readonly ExPerformanceCounter ReportsGeneratedPerHour = new ExPerformanceCounter("MSExchange Journaling Agent", "Journal Reports Created/hour", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000009 RID: 9
		private static readonly ExPerformanceCounter AverageProcessingTime = new ExPerformanceCounter("MSExchange Journaling Agent", "Journaling Processing Time per Message", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400000A RID: 10
		public static readonly ExPerformanceCounter ProcessingTime = new ExPerformanceCounter("MSExchange Journaling Agent", "Journaling Processing Time", string.Empty, null, new ExPerformanceCounter[]
		{
			PerfCounters.AverageProcessingTime
		});

		// Token: 0x0400000B RID: 11
		private static readonly ExPerformanceCounter AverageProcessingTimeBase = new ExPerformanceCounter("MSExchange Journaling Agent", "Average journaling processing time/message base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400000C RID: 12
		public static readonly ExPerformanceCounter MessagesProcessed = new ExPerformanceCounter("MSExchange Journaling Agent", "Messages Processed by Journaling", string.Empty, null, new ExPerformanceCounter[]
		{
			PerfCounters.AverageProcessingTimeBase
		});

		// Token: 0x0400000D RID: 13
		private static readonly ExPerformanceCounter MessagesDeferredWithinJournalAgentRates = new ExPerformanceCounter("MSExchange Journaling Agent", "Messages Deferred Within Journal Agent/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400000E RID: 14
		public static readonly ExPerformanceCounter MessagesDeferredWithinJournalAgent = new ExPerformanceCounter("MSExchange Journaling Agent", "Messages Deferred Within Journal Agent", string.Empty, null, new ExPerformanceCounter[]
		{
			PerfCounters.MessagesDeferredWithinJournalAgentRates
		});

		// Token: 0x0400000F RID: 15
		public static readonly ExPerformanceCounter MessagesDeferredWithinJournalAgentPerHour = new ExPerformanceCounter("MSExchange Journaling Agent", "Messages Deferred Within Journal Agent/hour", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000010 RID: 16
		private static readonly ExPerformanceCounter JournalReportsThatCouldNotBeCreatedRates = new ExPerformanceCounter("MSExchange Journaling Agent", "Journal Reports That Could Not Be Created/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000011 RID: 17
		public static readonly ExPerformanceCounter JournalReportsThatCouldNotBeCreated = new ExPerformanceCounter("MSExchange Journaling Agent", "Journal Reports That Could Not Be Created", string.Empty, null, new ExPerformanceCounter[]
		{
			PerfCounters.JournalReportsThatCouldNotBeCreatedRates
		});

		// Token: 0x04000012 RID: 18
		public static readonly ExPerformanceCounter JournalReportsThatCouldNotBeCreatedPerHour = new ExPerformanceCounter("MSExchange Journaling Agent", "Journal Reports That Could Not Be Created/hour", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000013 RID: 19
		private static readonly ExPerformanceCounter MessagesDeferredWithinJournalAgentLawfulInterceptRates = new ExPerformanceCounter("MSExchange Journaling Agent", "Messages Deferred Within Journal Agent (Lawful Intercept)/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000014 RID: 20
		public static readonly ExPerformanceCounter MessagesDeferredWithinJournalAgentLawfulIntercept = new ExPerformanceCounter("MSExchange Journaling Agent", "Messages Deferred Within Journal Agent (Lawful Intercept)", string.Empty, null, new ExPerformanceCounter[]
		{
			PerfCounters.MessagesDeferredWithinJournalAgentLawfulInterceptRates
		});

		// Token: 0x04000015 RID: 21
		public static readonly ExPerformanceCounter MessagesDeferredWithinJournalAgentLawfulInterceptPerHour = new ExPerformanceCounter("MSExchange Journaling Agent", "Messages Deferred Within Journal Agent (Lawful Intercept)/hour", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000016 RID: 22
		private static readonly ExPerformanceCounter IncomingJournalReportsDroppedRates = new ExPerformanceCounter("MSExchange Journaling Agent", "Incoming Journal Reports (Dropped)/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000017 RID: 23
		public static readonly ExPerformanceCounter IncomingJournalReportsDropped = new ExPerformanceCounter("MSExchange Journaling Agent", "Incoming Journal Reports (Dropped)", string.Empty, null, new ExPerformanceCounter[]
		{
			PerfCounters.IncomingJournalReportsDroppedRates
		});

		// Token: 0x04000018 RID: 24
		public static readonly ExPerformanceCounter IncomingJournalReportsDroppedPerHour = new ExPerformanceCounter("MSExchange Journaling Agent", "Incoming Journal Reports (Dropped)/hour", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000019 RID: 25
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			PerfCounters.UsersJournaled,
			PerfCounters.UsersJournaledPerHour,
			PerfCounters.ReportsGenerated,
			PerfCounters.ReportsGeneratedWithRMSProtectedMessage,
			PerfCounters.ReportsGeneratedPerHour,
			PerfCounters.MessagesProcessed,
			PerfCounters.ProcessingTime,
			PerfCounters.MessagesDeferredWithinJournalAgent,
			PerfCounters.JournalReportsThatCouldNotBeCreated,
			PerfCounters.MessagesDeferredWithinJournalAgentLawfulIntercept,
			PerfCounters.IncomingJournalReportsDropped,
			PerfCounters.MessagesDeferredWithinJournalAgentPerHour,
			PerfCounters.JournalReportsThatCouldNotBeCreatedPerHour,
			PerfCounters.MessagesDeferredWithinJournalAgentLawfulInterceptPerHour,
			PerfCounters.IncomingJournalReportsDroppedPerHour
		};
	}
}
