using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Autodiscover
{
	// Token: 0x0200000D RID: 13
	internal static class AutodiscoverPerformanceCounters
	{
		// Token: 0x06000054 RID: 84 RVA: 0x00003734 File Offset: 0x00001934
		public static void GetPerfCounterInfo(XElement element)
		{
			if (AutodiscoverPerformanceCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in AutodiscoverPerformanceCounters.AllCounters)
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

		// Token: 0x0400008A RID: 138
		public const string CategoryName = "MSExchangeAutodiscover";

		// Token: 0x0400008B RID: 139
		private static readonly ExPerformanceCounter TotalRequestsPerSecond = new ExPerformanceCounter("MSExchangeAutodiscover", "Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400008C RID: 140
		public static readonly ExPerformanceCounter TotalRequests = new ExPerformanceCounter("MSExchangeAutodiscover", "Total Requests", string.Empty, null, new ExPerformanceCounter[]
		{
			AutodiscoverPerformanceCounters.TotalRequestsPerSecond
		});

		// Token: 0x0400008D RID: 141
		private static readonly ExPerformanceCounter ErrorResponsesPerSecond = new ExPerformanceCounter("MSExchangeAutodiscover", "Error Responses/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400008E RID: 142
		public static readonly ExPerformanceCounter TotalErrorResponses = new ExPerformanceCounter("MSExchangeAutodiscover", "Error Responses", string.Empty, null, new ExPerformanceCounter[]
		{
			AutodiscoverPerformanceCounters.ErrorResponsesPerSecond
		});

		// Token: 0x0400008F RID: 143
		public static readonly ExPerformanceCounter PID = new ExPerformanceCounter("MSExchangeAutodiscover", "Process ID", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000090 RID: 144
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			AutodiscoverPerformanceCounters.TotalRequests,
			AutodiscoverPerformanceCounters.TotalErrorResponses,
			AutodiscoverPerformanceCounters.PID
		};
	}
}
