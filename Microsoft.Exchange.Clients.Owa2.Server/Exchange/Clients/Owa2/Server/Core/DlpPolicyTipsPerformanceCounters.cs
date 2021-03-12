using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Owa2.Server.Core
{
	// Token: 0x020004A4 RID: 1188
	internal static class DlpPolicyTipsPerformanceCounters
	{
		// Token: 0x0600288B RID: 10379 RVA: 0x000960A8 File Offset: 0x000942A8
		public static void GetPerfCounterInfo(XElement element)
		{
			if (DlpPolicyTipsPerformanceCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in DlpPolicyTipsPerformanceCounters.AllCounters)
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

		// Token: 0x0400176E RID: 5998
		public const string CategoryName = "MSExchange DlpPolicyTips";

		// Token: 0x0400176F RID: 5999
		public static readonly ExPerformanceCounter DlpPolicyTipsTotalRequests = new ExPerformanceCounter("MSExchange DlpPolicyTips", "Total DlpPolicyTips requests processed", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04001770 RID: 6000
		public static readonly ExPerformanceCounter DlpPolicyTipsPendingRequests = new ExPerformanceCounter("MSExchange DlpPolicyTips", "Current Pending DlpPolicyTips requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04001771 RID: 6001
		public static readonly ExPerformanceCounter DlpPolicyTipsSuccessfulRequests = new ExPerformanceCounter("MSExchange DlpPolicyTips", "Successful DlpPolicyTips requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04001772 RID: 6002
		public static readonly ExPerformanceCounter DlpPolicyTipsAverageRequestLatency = new ExPerformanceCounter("MSExchange DlpPolicyTips", "Average latency of the DlpPolicyTips requests within the last 5 minutes", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04001773 RID: 6003
		public static readonly ExPerformanceCounter DlpPolicyTipsHighLatencyRequests = new ExPerformanceCounter("MSExchange DlpPolicyTips", "Number of DlpPolicyTips requests with latency more than 1 minute", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04001774 RID: 6004
		public static readonly ExPerformanceCounter DlpPolicyTipsPercentHighLatency = new ExPerformanceCounter("MSExchange DlpPolicyTips", "Percent of requests with Latency more than 1 minute within the last 5 minutes", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04001775 RID: 6005
		public static readonly ExPerformanceCounter DlpPolicyTipsSkippedRequestsInputError = new ExPerformanceCounter("MSExchange DlpPolicyTips", "Skipped DlpPolicyTips requests InputError", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04001776 RID: 6006
		public static readonly ExPerformanceCounter DlpPolicyTipsAllServerFailedRequests = new ExPerformanceCounter("MSExchange DlpPolicyTips", "Total server Failed DlpPolicyTips requests received", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04001777 RID: 6007
		public static readonly ExPerformanceCounter DlpPolicyTipsPercentServerFailures = new ExPerformanceCounter("MSExchange DlpPolicyTips", "Percent of all server Failed requests within the last 5 minutes", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04001778 RID: 6008
		public static readonly ExPerformanceCounter DlpPolicyTipsFailedRequestsUnknownError = new ExPerformanceCounter("MSExchange DlpPolicyTips", "Failed DlpPolicyTips requests UnknownError", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04001779 RID: 6009
		public static readonly ExPerformanceCounter DlpPolicyTipsFailedRequestsFips = new ExPerformanceCounter("MSExchange DlpPolicyTips", "Failed DlpPolicyTips requests Fips exceptions", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400177A RID: 6010
		public static readonly ExPerformanceCounter DlpPolicyTipsFailedRequestsFipsTimeOut = new ExPerformanceCounter("MSExchange DlpPolicyTips", "Failed DlpPolicyTips requests Fips TimeOut exceptions", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400177B RID: 6011
		public static readonly ExPerformanceCounter DlpPolicyTipsFailedRequestsEtr = new ExPerformanceCounter("MSExchange DlpPolicyTips", "Failed DlpPolicyTips requests Etr exceptions", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400177C RID: 6012
		public static readonly ExPerformanceCounter DlpPolicyTipsFailedRequestsAd = new ExPerformanceCounter("MSExchange DlpPolicyTips", "Failed DlpPolicyTips requests Ad exceptions", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400177D RID: 6013
		public static readonly ExPerformanceCounter DlpPolicyTipsFailedRequestsXso = new ExPerformanceCounter("MSExchange DlpPolicyTips", "Failed DlpPolicyTips requests Xso exceptions", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400177E RID: 6014
		public static readonly ExPerformanceCounter DlpPolicyTipsFailedRequestsOws = new ExPerformanceCounter("MSExchange DlpPolicyTips", "Failed DlpPolicyTips requests Ows exceptions", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400177F RID: 6015
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			DlpPolicyTipsPerformanceCounters.DlpPolicyTipsTotalRequests,
			DlpPolicyTipsPerformanceCounters.DlpPolicyTipsPendingRequests,
			DlpPolicyTipsPerformanceCounters.DlpPolicyTipsSuccessfulRequests,
			DlpPolicyTipsPerformanceCounters.DlpPolicyTipsAverageRequestLatency,
			DlpPolicyTipsPerformanceCounters.DlpPolicyTipsHighLatencyRequests,
			DlpPolicyTipsPerformanceCounters.DlpPolicyTipsPercentHighLatency,
			DlpPolicyTipsPerformanceCounters.DlpPolicyTipsSkippedRequestsInputError,
			DlpPolicyTipsPerformanceCounters.DlpPolicyTipsAllServerFailedRequests,
			DlpPolicyTipsPerformanceCounters.DlpPolicyTipsPercentServerFailures,
			DlpPolicyTipsPerformanceCounters.DlpPolicyTipsFailedRequestsUnknownError,
			DlpPolicyTipsPerformanceCounters.DlpPolicyTipsFailedRequestsFips,
			DlpPolicyTipsPerformanceCounters.DlpPolicyTipsFailedRequestsFipsTimeOut,
			DlpPolicyTipsPerformanceCounters.DlpPolicyTipsFailedRequestsEtr,
			DlpPolicyTipsPerformanceCounters.DlpPolicyTipsFailedRequestsAd,
			DlpPolicyTipsPerformanceCounters.DlpPolicyTipsFailedRequestsXso,
			DlpPolicyTipsPerformanceCounters.DlpPolicyTipsFailedRequestsOws
		};
	}
}
