using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Clients.Security
{
	// Token: 0x02000002 RID: 2
	internal static class LiveIdAuthenticationCounters
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public static void GetPerfCounterInfo(XElement element)
		{
			if (LiveIdAuthenticationCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in LiveIdAuthenticationCounters.AllCounters)
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
		public const string CategoryName = "MSExchange LiveIdAuthentication";

		// Token: 0x04000002 RID: 2
		public static readonly ExPerformanceCounter TotalRetrievalsfromCache = new ExPerformanceCounter("MSExchange LiveIdAuthentication", "Total Retrievals from Cache", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000003 RID: 3
		public static readonly ExPerformanceCounter TotalFailedLookupsfromCache = new ExPerformanceCounter("MSExchange LiveIdAuthentication", "Total Failed Lookups from Cache", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000004 RID: 4
		public static readonly ExPerformanceCounter TotalSessionDataPreloadRequestsSent = new ExPerformanceCounter("MSExchange LiveIdAuthentication", "Total session data preload requests sent", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000005 RID: 5
		public static readonly ExPerformanceCounter TotalSessionDataPreloadRequestsFailed = new ExPerformanceCounter("MSExchange LiveIdAuthentication", "Total session data preload requests that failed", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000006 RID: 6
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			LiveIdAuthenticationCounters.TotalRetrievalsfromCache,
			LiveIdAuthenticationCounters.TotalFailedLookupsfromCache,
			LiveIdAuthenticationCounters.TotalSessionDataPreloadRequestsSent,
			LiveIdAuthenticationCounters.TotalSessionDataPreloadRequestsFailed
		};
	}
}
