using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Directory.GlobalLocatorService
{
	// Token: 0x02000A42 RID: 2626
	internal static class GlsPerformanceCounters
	{
		// Token: 0x06007848 RID: 30792 RVA: 0x0018D670 File Offset: 0x0018B870
		public static void GetPerfCounterInfo(XElement element)
		{
			if (GlsPerformanceCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in GlsPerformanceCounters.AllCounters)
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

		// Token: 0x04004F0C RID: 20236
		public const string CategoryName = "MSExchange Global Locator";

		// Token: 0x04004F0D RID: 20237
		public static readonly ExPerformanceCounter AverageOverallLatency = new ExPerformanceCounter("MSExchange Global Locator", "Average Overall Latency", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004F0E RID: 20238
		public static readonly ExPerformanceCounter AverageOverallLatencyBase = new ExPerformanceCounter("MSExchange Global Locator", "Base for Average Overall Latency", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004F0F RID: 20239
		public static readonly ExPerformanceCounter AverageReadLatency = new ExPerformanceCounter("MSExchange Global Locator", "Average Read Latency", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004F10 RID: 20240
		public static readonly ExPerformanceCounter AverageReadLatencyBase = new ExPerformanceCounter("MSExchange Global Locator", "Base for Average Read Latency", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004F11 RID: 20241
		public static readonly ExPerformanceCounter AverageWriteLatency = new ExPerformanceCounter("MSExchange Global Locator", "Average Write Latency", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004F12 RID: 20242
		public static readonly ExPerformanceCounter AverageWriteLatencyBase = new ExPerformanceCounter("MSExchange Global Locator", "Base for Average Write Latency", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04004F13 RID: 20243
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			GlsPerformanceCounters.AverageOverallLatency,
			GlsPerformanceCounters.AverageOverallLatencyBase,
			GlsPerformanceCounters.AverageReadLatency,
			GlsPerformanceCounters.AverageReadLatencyBase,
			GlsPerformanceCounters.AverageWriteLatency,
			GlsPerformanceCounters.AverageWriteLatencyBase
		};
	}
}
