using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MapiHttp.PerformanceCounters
{
	// Token: 0x02000003 RID: 3
	internal static class NspiPerformanceCounters
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002570 File Offset: 0x00000770
		public static void GetPerfCounterInfo(XElement element)
		{
			if (NspiPerformanceCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in NspiPerformanceCounters.AllCounters)
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

		// Token: 0x0400001C RID: 28
		public const string CategoryName = "MSExchange MapiHttp Nspi";

		// Token: 0x0400001D RID: 29
		public static readonly ExPerformanceCounter PID = new ExPerformanceCounter("MSExchange MapiHttp Nspi", "PID", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400001E RID: 30
		public static readonly ExPerformanceCounter NspiConnectionsCurrent = new ExPerformanceCounter("MSExchange MapiHttp Nspi", "NSPI Connections Current", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400001F RID: 31
		public static readonly ExPerformanceCounter NspiConnectionsTotal = new ExPerformanceCounter("MSExchange MapiHttp Nspi", "NSPI Connections Total", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000020 RID: 32
		public static readonly ExPerformanceCounter NspiConnectionsRate = new ExPerformanceCounter("MSExchange MapiHttp Nspi", "NSPI Connections/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000021 RID: 33
		public static readonly ExPerformanceCounter NspiRequests = new ExPerformanceCounter("MSExchange MapiHttp Nspi", "NSPI Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000022 RID: 34
		public static readonly ExPerformanceCounter NspiRequestsTotal = new ExPerformanceCounter("MSExchange MapiHttp Nspi", "NSPI Requests Total", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000023 RID: 35
		public static readonly ExPerformanceCounter NspiRequestsAverageLatency = new ExPerformanceCounter("MSExchange MapiHttp Nspi", "NSPI Requests Average Latency", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000024 RID: 36
		public static readonly ExPerformanceCounter NspiRequestsRate = new ExPerformanceCounter("MSExchange MapiHttp Nspi", "NSPI Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000025 RID: 37
		public static readonly ExPerformanceCounter NspiBrowseRequests = new ExPerformanceCounter("MSExchange MapiHttp Nspi", "NSPI Browse Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000026 RID: 38
		public static readonly ExPerformanceCounter NspiBrowseRequestsTotal = new ExPerformanceCounter("MSExchange MapiHttp Nspi", "NSPI Browse Requests Total", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000027 RID: 39
		public static readonly ExPerformanceCounter NspiBrowseRequestsAverageLatency = new ExPerformanceCounter("MSExchange MapiHttp Nspi", "NSPI Browse Requests Average Latency", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000028 RID: 40
		public static readonly ExPerformanceCounter NspiBrowseRequestsRate = new ExPerformanceCounter("MSExchange MapiHttp Nspi", "NSPI Browse Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000029 RID: 41
		public static readonly ExPerformanceCounter RfrRequests = new ExPerformanceCounter("MSExchange MapiHttp Nspi", "Referral Requests", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400002A RID: 42
		public static readonly ExPerformanceCounter RfrRequestsTotal = new ExPerformanceCounter("MSExchange MapiHttp Nspi", "Referral Requests Total", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400002B RID: 43
		public static readonly ExPerformanceCounter RfrRequestsRate = new ExPerformanceCounter("MSExchange MapiHttp Nspi", "Referral Requests/sec", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400002C RID: 44
		public static readonly ExPerformanceCounter RfrRequestsAverageLatency = new ExPerformanceCounter("MSExchange MapiHttp Nspi", "Referral Requests Average Latency", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400002D RID: 45
		public static readonly ExPerformanceCounter ThumbnailPhotoAverageTime = new ExPerformanceCounter("MSExchange MapiHttp Nspi", "ThumbnailPhoto Average Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400002E RID: 46
		public static readonly ExPerformanceCounter ThumbnailPhotoAverageTimeBase = new ExPerformanceCounter("MSExchange MapiHttp Nspi", "ThumbnailPhoto Average Time Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x0400002F RID: 47
		public static readonly ExPerformanceCounter ThumbnailPhotoFromMailboxCount = new ExPerformanceCounter("MSExchange MapiHttp Nspi", "ThumbnailPhoto From Mailbox Count", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000030 RID: 48
		public static readonly ExPerformanceCounter ThumbnailPhotoFromDirectoryCount = new ExPerformanceCounter("MSExchange MapiHttp Nspi", "ThumbnailPhoto From Directory Count", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000031 RID: 49
		public static readonly ExPerformanceCounter ThumbnailPhotoNotPresentCount = new ExPerformanceCounter("MSExchange MapiHttp Nspi", "ThumbnailPhoto Not Present Count", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000032 RID: 50
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			NspiPerformanceCounters.PID,
			NspiPerformanceCounters.NspiConnectionsCurrent,
			NspiPerformanceCounters.NspiConnectionsTotal,
			NspiPerformanceCounters.NspiConnectionsRate,
			NspiPerformanceCounters.NspiRequests,
			NspiPerformanceCounters.NspiRequestsTotal,
			NspiPerformanceCounters.NspiRequestsAverageLatency,
			NspiPerformanceCounters.NspiRequestsRate,
			NspiPerformanceCounters.NspiBrowseRequests,
			NspiPerformanceCounters.NspiBrowseRequestsTotal,
			NspiPerformanceCounters.NspiBrowseRequestsAverageLatency,
			NspiPerformanceCounters.NspiBrowseRequestsRate,
			NspiPerformanceCounters.RfrRequests,
			NspiPerformanceCounters.RfrRequestsTotal,
			NspiPerformanceCounters.RfrRequestsRate,
			NspiPerformanceCounters.RfrRequestsAverageLatency,
			NspiPerformanceCounters.ThumbnailPhotoAverageTime,
			NspiPerformanceCounters.ThumbnailPhotoAverageTimeBase,
			NspiPerformanceCounters.ThumbnailPhotoFromMailboxCount,
			NspiPerformanceCounters.ThumbnailPhotoFromDirectoryCount,
			NspiPerformanceCounters.ThumbnailPhotoNotPresentCount
		};
	}
}
