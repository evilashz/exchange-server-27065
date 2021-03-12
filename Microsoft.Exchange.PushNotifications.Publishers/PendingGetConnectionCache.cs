using System;
using System.Web;
using System.Web.Caching;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.PushNotifications;

namespace Microsoft.Exchange.PushNotifications.Publishers
{
	// Token: 0x020000B1 RID: 177
	internal sealed class PendingGetConnectionCache : IPendingGetConnectionCache
	{
		// Token: 0x060005F7 RID: 1527 RVA: 0x00013854 File Offset: 0x00011A54
		static PendingGetConnectionCache()
		{
			foreach (ExPerformanceCounter exPerformanceCounter in PendingGetCounters.AllCounters)
			{
				exPerformanceCounter.Reset();
			}
			PendingGetConnectionCache.Instance = new PendingGetConnectionCache();
			PendingGetConnectionCache.ConnectionCacheCounter = new ItemCounter(PendingGetCounters.PendingGetConnectionCacheCount, PendingGetCounters.PendingGetConnectionCachePeak, PendingGetCounters.PendingGetConnectionCacheTotal);
		}

		// Token: 0x17000190 RID: 400
		// (get) Token: 0x060005F8 RID: 1528 RVA: 0x000138A2 File Offset: 0x00011AA2
		// (set) Token: 0x060005F9 RID: 1529 RVA: 0x000138A9 File Offset: 0x00011AA9
		private static ItemCounter ConnectionCacheCounter { get; set; }

		// Token: 0x060005FA RID: 1530 RVA: 0x000138B4 File Offset: 0x00011AB4
		public IPendingGetConnection AddOrGetConnection(string connectionId)
		{
			IPendingGetConnection result;
			if (!this.TryGetConnection(connectionId, out result))
			{
				result = PendingGetConnectionCache.AddNewConnection(connectionId);
			}
			return result;
		}

		// Token: 0x060005FB RID: 1531 RVA: 0x000138D4 File Offset: 0x00011AD4
		public bool TryGetConnection(string connectionId, out IPendingGetConnection connection)
		{
			AverageTimeCounterBase averageTimeCounterBase = new AverageTimeCounterBase(PendingGetCounters.TryGetConnectionAverageTime, PendingGetCounters.TryGetConnectionAverageTimeBase, true);
			connection = (HttpRuntime.Cache.Get(PendingGetConnectionCache.GetCacheKey(connectionId)) as IPendingGetConnection);
			averageTimeCounterBase.Stop();
			return connection != null;
		}

		// Token: 0x060005FC RID: 1532 RVA: 0x00013938 File Offset: 0x00011B38
		private static IPendingGetConnection AddNewConnection(string connectionId)
		{
			AverageTimeCounterBase averageTimeCounterBase = new AverageTimeCounterBase(PendingGetCounters.AddNewConnectionAverageTime, PendingGetCounters.AddNewConnectionAverageTimeBase, true);
			PendingGetConnection pendingGetConnection = new PendingGetConnection(connectionId);
			ExTraceGlobals.PendingGetPublisherTracer.TraceDebug<string>((long)pendingGetConnection.GetHashCode(), "[AddNewConnection] Create new PendingGet session for PendingGet channel - {0}", connectionId);
			AverageTimeCounterBase connectionCachedTime = new AverageTimeCounterBase(PendingGetCounters.ConnectionCachedAverageTime, PendingGetCounters.ConnectionCachedAverageTimeBase, true);
			HttpRuntime.Cache.Insert(PendingGetConnectionCache.GetCacheKey(connectionId), pendingGetConnection, null, DateTime.MaxValue, TimeSpan.FromMinutes(120.0), CacheItemPriority.Normal, delegate(string key, object value, CacheItemRemovedReason reason)
			{
				PendingGetConnectionCache.ConnectionCacheCounter.Decrement();
				connectionCachedTime.Stop();
			});
			averageTimeCounterBase.Stop();
			PendingGetConnectionCache.ConnectionCacheCounter.Increment();
			return pendingGetConnection;
		}

		// Token: 0x060005FD RID: 1533 RVA: 0x000139D4 File Offset: 0x00011BD4
		private static string GetCacheKey(string connectionId)
		{
			return "pnm__" + connectionId;
		}

		// Token: 0x040002FD RID: 765
		private const long DefaultPendingGetSessionTimeoutInMinutes = 120L;

		// Token: 0x040002FE RID: 766
		private const string PendingGetSessionKeyPrefix = "pnm__";

		// Token: 0x040002FF RID: 767
		public static readonly PendingGetConnectionCache Instance;
	}
}
