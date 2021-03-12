using System;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x0200005D RID: 93
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal static class MsExchangeTransportSyncManagerPerf
	{
		// Token: 0x06000449 RID: 1097 RVA: 0x0001AF50 File Offset: 0x00019150
		public static void GetPerfCounterInfo(XElement element)
		{
			if (MsExchangeTransportSyncManagerPerf.AllCounters == null)
			{
				return;
			}
			foreach (ExPerformanceCounter exPerformanceCounter in MsExchangeTransportSyncManagerPerf.AllCounters)
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

		// Token: 0x0400027F RID: 639
		public const string CategoryName = "MSExchange Transport Sync Manager";

		// Token: 0x04000280 RID: 640
		public static readonly ExPerformanceCounter SubscriptionQueueDispatchLag = new ExPerformanceCounter("MSExchange Transport Sync Manager", "Subscription queue dispatching lag (seconds)", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000281 RID: 641
		public static readonly ExPerformanceCounter TotalNumberOfMailboxesInSubscriptionCaches = new ExPerformanceCounter("MSExchange Transport Sync Manager", "Total number of user mailboxes in the Subscription Caches for all databases", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000282 RID: 642
		public static readonly ExPerformanceCounter TotalNumberOfMailboxesToBeRebuiltInSubscriptionCaches = new ExPerformanceCounter("MSExchange Transport Sync Manager", "Total number of mailboxes in the Subscription Caches to be rebuilt.", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000283 RID: 643
		public static readonly ExPerformanceCounter TotalNumberOfMailboxesRebuiltInSubscriptionCaches = new ExPerformanceCounter("MSExchange Transport Sync Manager", "Total number of mailboxes in the Subscription Caches rebuilt", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000284 RID: 644
		public static readonly ExPerformanceCounter TotalNumberOfMailboxesRepairRebuiltInSubscriptionCaches = new ExPerformanceCounter("MSExchange Transport Sync Manager", "Total number of mailboxes in the Subscription Caches that rebuild repaired", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000285 RID: 645
		public static readonly ExPerformanceCounter LastWaitToGetSubscriptionsCacheToken = new ExPerformanceCounter("MSExchange Transport Sync Manager", "Last wait (msec) to get a token", string.Empty, null, new ExPerformanceCounter[0]);

		// Token: 0x04000286 RID: 646
		public static readonly ExPerformanceCounter[] AllCounters = new ExPerformanceCounter[]
		{
			MsExchangeTransportSyncManagerPerf.SubscriptionQueueDispatchLag,
			MsExchangeTransportSyncManagerPerf.TotalNumberOfMailboxesInSubscriptionCaches,
			MsExchangeTransportSyncManagerPerf.TotalNumberOfMailboxesToBeRebuiltInSubscriptionCaches,
			MsExchangeTransportSyncManagerPerf.TotalNumberOfMailboxesRebuiltInSubscriptionCaches,
			MsExchangeTransportSyncManagerPerf.TotalNumberOfMailboxesRepairRebuiltInSubscriptionCaches,
			MsExchangeTransportSyncManagerPerf.LastWaitToGetSubscriptionsCacheToken
		};
	}
}
