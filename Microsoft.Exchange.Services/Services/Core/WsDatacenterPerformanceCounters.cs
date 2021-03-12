using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Services.Core
{
	// Token: 0x02000F7C RID: 3964
	internal static class WsDatacenterPerformanceCounters
	{
		// Token: 0x06006772 RID: 26482 RVA: 0x0014D16C File Offset: 0x0014B36C
		public static void GetPerfCounterInfo(XElement element)
		{
			if (WsDatacenterPerformanceCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in WsDatacenterPerformanceCounters.AllCounters)
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

		// Token: 0x04003BFE RID: 15358
		public const string CategoryName = "MSExchangeWS:Datacenter";

		// Token: 0x04003BFF RID: 15359
		private static readonly ExPerformanceCounter RequestsReceivedWithPartnerTokenPerSecond = new ExPerformanceCounter("MSExchangeWS:Datacenter", "Requests Received with Partner Token/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003C00 RID: 15360
		public static readonly ExPerformanceCounter TotalRequestsReceivedWithPartnerToken = new ExPerformanceCounter("MSExchangeWS:Datacenter", "Total Requests Received with Partner Token", string.Empty, null, new ExPerformanceCounter[]
		{
			WsDatacenterPerformanceCounters.RequestsReceivedWithPartnerTokenPerSecond
		});

		// Token: 0x04003C01 RID: 15361
		private static readonly ExPerformanceCounter UnauthorizedRequestsReceivedWithPartnerTokenPerSecond = new ExPerformanceCounter("MSExchangeWS:Datacenter", "Unauthorized Requests Received with Partner Token/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003C02 RID: 15362
		public static readonly ExPerformanceCounter TotalUnauthorizedRequestsReceivedWithPartnerToken = new ExPerformanceCounter("MSExchangeWS:Datacenter", "Total Unauthorized Requests Received with Partner Token", string.Empty, null, new ExPerformanceCounter[]
		{
			WsDatacenterPerformanceCounters.UnauthorizedRequestsReceivedWithPartnerTokenPerSecond
		});

		// Token: 0x04003C03 RID: 15363
		public static readonly ExPerformanceCounter PartnerTokenCacheEntries = new ExPerformanceCounter("MSExchangeWS:Datacenter", "Partner Token Cache Entries", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04003C04 RID: 15364
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			WsDatacenterPerformanceCounters.TotalRequestsReceivedWithPartnerToken,
			WsDatacenterPerformanceCounters.TotalUnauthorizedRequestsReceivedWithPartnerToken,
			WsDatacenterPerformanceCounters.PartnerTokenCacheEntries
		};
	}
}
