using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x0200010B RID: 267
	internal static class PendingGetCounters
	{
		// Token: 0x060008AC RID: 2220 RVA: 0x0001A700 File Offset: 0x00018900
		public static void GetPerfCounterInfo(XElement element)
		{
			if (PendingGetCounters.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in PendingGetCounters.AllCounters)
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

		// Token: 0x040004DC RID: 1244
		public const string CategoryName = "MSExchange Push Notifications Pending Get";

		// Token: 0x040004DD RID: 1245
		public static readonly ExPerformanceCounter TryGetConnectionAverageTime = new ExPerformanceCounter("MSExchange Push Notifications Pending Get", "Try get Connection - Average Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040004DE RID: 1246
		public static readonly ExPerformanceCounter TryGetConnectionAverageTimeBase = new ExPerformanceCounter("MSExchange Push Notifications Pending Get", "Try get Connection - Average Time Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040004DF RID: 1247
		public static readonly ExPerformanceCounter ConnectionCachedAverageTime = new ExPerformanceCounter("MSExchange Push Notifications Pending Get", "Connection Cached - Average Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040004E0 RID: 1248
		public static readonly ExPerformanceCounter ConnectionCachedAverageTimeBase = new ExPerformanceCounter("MSExchange Push Notifications Pending Get", "Connection Cached - Average Time Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040004E1 RID: 1249
		public static readonly ExPerformanceCounter AddNewConnectionAverageTime = new ExPerformanceCounter("MSExchange Push Notifications Pending Get", "Add New Connection - Average Time", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040004E2 RID: 1250
		public static readonly ExPerformanceCounter AddNewConnectionAverageTimeBase = new ExPerformanceCounter("MSExchange Push Notifications Pending Get", "Add New Connection - Average Time Base", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040004E3 RID: 1251
		public static readonly ExPerformanceCounter PendingGetConnectionCacheCount = new ExPerformanceCounter("MSExchange Push Notifications Pending Get", "Pending Get Connection Cache - Count", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040004E4 RID: 1252
		public static readonly ExPerformanceCounter PendingGetConnectionCachePeak = new ExPerformanceCounter("MSExchange Push Notifications Pending Get", "Pending Get Connection Cache - Peak", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040004E5 RID: 1253
		public static readonly ExPerformanceCounter PendingGetConnectionCacheTotal = new ExPerformanceCounter("MSExchange Push Notifications Pending Get", "Pending Get Connection Cache - Total", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x040004E6 RID: 1254
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			PendingGetCounters.TryGetConnectionAverageTime,
			PendingGetCounters.TryGetConnectionAverageTimeBase,
			PendingGetCounters.ConnectionCachedAverageTime,
			PendingGetCounters.ConnectionCachedAverageTimeBase,
			PendingGetCounters.AddNewConnectionAverageTime,
			PendingGetCounters.AddNewConnectionAverageTimeBase,
			PendingGetCounters.PendingGetConnectionCacheCount,
			PendingGetCounters.PendingGetConnectionCachePeak,
			PendingGetCounters.PendingGetConnectionCacheTotal
		};
	}
}
