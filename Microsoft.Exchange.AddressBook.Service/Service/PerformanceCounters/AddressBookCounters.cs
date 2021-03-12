using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.AddressBook.Service.PerformanceCounters
{
	// Token: 0x0200004E RID: 78
	internal static class AddressBookCounters
	{
		// Token: 0x06000328 RID: 808 RVA: 0x00014124 File Offset: 0x00012324
		public static void GetPerfCounterInfo(XElement element)
		{
			if (AddressBookCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in AddressBookCounters.AllCounters)
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

		// Token: 0x040001EC RID: 492
		public const string CategoryName = "MSExchangeAB";

		// Token: 0x040001ED RID: 493
		public static readonly ExPerformanceCounter PID = new ExPerformanceCounter("MSExchangeAB", "PID", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040001EE RID: 494
		public static readonly ExPerformanceCounter NspiConnectionsCurrent = new ExPerformanceCounter("MSExchangeAB", "NSPI Connections Current", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040001EF RID: 495
		public static readonly ExPerformanceCounter NspiConnectionsTotal = new ExPerformanceCounter("MSExchangeAB", "NSPI Connections Total", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040001F0 RID: 496
		public static readonly ExPerformanceCounter NspiConnectionsRate = new ExPerformanceCounter("MSExchangeAB", "NSPI Connections/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040001F1 RID: 497
		public static readonly ExPerformanceCounter NspiRpcRequests = new ExPerformanceCounter("MSExchangeAB", "NSPI RPC Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040001F2 RID: 498
		public static readonly ExPerformanceCounter NspiRpcRequestsTotal = new ExPerformanceCounter("MSExchangeAB", "NSPI RPC Requests Total", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040001F3 RID: 499
		public static readonly ExPerformanceCounter NspiRpcRequestsAverageLatency = new ExPerformanceCounter("MSExchangeAB", "NSPI RPC Requests Average Latency", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040001F4 RID: 500
		public static readonly ExPerformanceCounter NspiRpcRequestsRate = new ExPerformanceCounter("MSExchangeAB", "NSPI RPC Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040001F5 RID: 501
		public static readonly ExPerformanceCounter NspiRpcBrowseRequests = new ExPerformanceCounter("MSExchangeAB", "NSPI RPC Browse Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040001F6 RID: 502
		public static readonly ExPerformanceCounter NspiRpcBrowseRequestsTotal = new ExPerformanceCounter("MSExchangeAB", "NSPI RPC Browse Requests Total", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040001F7 RID: 503
		public static readonly ExPerformanceCounter NspiRpcBrowseRequestsAverageLatency = new ExPerformanceCounter("MSExchangeAB", "NSPI RPC Browse Requests Average Latency", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040001F8 RID: 504
		public static readonly ExPerformanceCounter NspiRpcBrowseRequestsRate = new ExPerformanceCounter("MSExchangeAB", "NSPI RPC Browse Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040001F9 RID: 505
		public static readonly ExPerformanceCounter RfrRpcRequests = new ExPerformanceCounter("MSExchangeAB", "Referral RPC Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040001FA RID: 506
		public static readonly ExPerformanceCounter RfrRpcRequestsTotal = new ExPerformanceCounter("MSExchangeAB", "Referral RPC Requests Total", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040001FB RID: 507
		public static readonly ExPerformanceCounter RfrRpcRequestsRate = new ExPerformanceCounter("MSExchangeAB", "Referral RPC Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040001FC RID: 508
		public static readonly ExPerformanceCounter RfrRpcRequestsAverageLatency = new ExPerformanceCounter("MSExchangeAB", "Referral RPC Requests Average Latency", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040001FD RID: 509
		public static readonly ExPerformanceCounter ThumbnailPhotoAverageTime = new ExPerformanceCounter("MSExchangeAB", "ThumbnailPhoto Average Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040001FE RID: 510
		public static readonly ExPerformanceCounter ThumbnailPhotoAverageTimeBase = new ExPerformanceCounter("MSExchangeAB", "ThumbnailPhoto Average Time Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040001FF RID: 511
		public static readonly ExPerformanceCounter ThumbnailPhotoFromMailboxCount = new ExPerformanceCounter("MSExchangeAB", "ThumbnailPhoto From Mailbox Count", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000200 RID: 512
		public static readonly ExPerformanceCounter ThumbnailPhotoFromDirectoryCount = new ExPerformanceCounter("MSExchangeAB", "ThumbnailPhoto From Directory Count", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000201 RID: 513
		public static readonly ExPerformanceCounter ThumbnailPhotoNotPresentCount = new ExPerformanceCounter("MSExchangeAB", "ThumbnailPhoto Not Present Count", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000202 RID: 514
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			AddressBookCounters.PID,
			AddressBookCounters.NspiConnectionsCurrent,
			AddressBookCounters.NspiConnectionsTotal,
			AddressBookCounters.NspiConnectionsRate,
			AddressBookCounters.NspiRpcRequests,
			AddressBookCounters.NspiRpcRequestsTotal,
			AddressBookCounters.NspiRpcRequestsAverageLatency,
			AddressBookCounters.NspiRpcRequestsRate,
			AddressBookCounters.NspiRpcBrowseRequests,
			AddressBookCounters.NspiRpcBrowseRequestsTotal,
			AddressBookCounters.NspiRpcBrowseRequestsAverageLatency,
			AddressBookCounters.NspiRpcBrowseRequestsRate,
			AddressBookCounters.RfrRpcRequests,
			AddressBookCounters.RfrRpcRequestsTotal,
			AddressBookCounters.RfrRpcRequestsRate,
			AddressBookCounters.RfrRpcRequestsAverageLatency,
			AddressBookCounters.ThumbnailPhotoAverageTime,
			AddressBookCounters.ThumbnailPhotoAverageTimeBase,
			AddressBookCounters.ThumbnailPhotoFromMailboxCount,
			AddressBookCounters.ThumbnailPhotoFromDirectoryCount,
			AddressBookCounters.ThumbnailPhotoNotPresentCount
		};
	}
}
