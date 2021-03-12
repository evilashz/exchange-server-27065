using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Logging.Search
{
	// Token: 0x02000062 RID: 98
	internal static class PerfCounters
	{
		// Token: 0x060001E7 RID: 487 RVA: 0x0000CE04 File Offset: 0x0000B004
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

		// Token: 0x0400017D RID: 381
		public const string CategoryName = "MSExchange Log Search Service";

		// Token: 0x0400017E RID: 382
		private static readonly ExPerformanceCounter SearchesProcessedRate = new ExPerformanceCounter("MSExchange Log Search Service", "Search requests processed/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400017F RID: 383
		public static readonly ExPerformanceCounter SearchesProcessed = new ExPerformanceCounter("MSExchange Log Search Service", "Search requests processed", string.Empty, null, new ExPerformanceCounter[]
		{
			PerfCounters.SearchesProcessedRate
		});

		// Token: 0x04000180 RID: 384
		public static readonly ExPerformanceCounter AverageProcessingTime = new ExPerformanceCounter("MSExchange Log Search Service", "Average search processing time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000181 RID: 385
		public static readonly ExPerformanceCounter AverageProcessingTimeBase = new ExPerformanceCounter("MSExchange Log Search Service", "Base to compute average processing time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000182 RID: 386
		public static readonly ExPerformanceCounter SearchesRejected = new ExPerformanceCounter("MSExchange Log Search Service", "Search requests rejected", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000183 RID: 387
		public static readonly ExPerformanceCounter SearchesTimedOut = new ExPerformanceCounter("MSExchange Log Search Service", "Search requests timed out", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000184 RID: 388
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			PerfCounters.SearchesProcessed,
			PerfCounters.AverageProcessingTime,
			PerfCounters.AverageProcessingTimeBase,
			PerfCounters.SearchesRejected,
			PerfCounters.SearchesTimedOut
		};
	}
}
