using System;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.HttpProxy.Common;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.SharedCache.Client;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x0200002D RID: 45
	internal class AnchorMailboxCache
	{
		// Token: 0x06000146 RID: 326 RVA: 0x000076D8 File Offset: 0x000058D8
		private AnchorMailboxCache()
		{
			this.innerCache = new ExactTimeoutCache<string, AnchorMailboxCacheEntry>(delegate(string k, AnchorMailboxCacheEntry v, RemoveReason r)
			{
				this.UpdateCacheSizeCounter();
			}, null, null, AnchorMailboxCache.AnchorMailboxCacheSize.Value, false);
			if (HttpProxySettings.AnchorMailboxSharedCacheEnabled.Value)
			{
				this.sharedCacheClient = new SharedCacheClient(WellKnownSharedCache.AnchorMailboxCache, "AnchorMailboxCache_" + HttpProxyGlobals.ProtocolType, HttpProxySettings.GlobalSharedCacheRpcTimeout.Value, ConcurrencyGuards.SharedCache);
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000147 RID: 327 RVA: 0x00007758 File Offset: 0x00005958
		public static AnchorMailboxCache Instance
		{
			get
			{
				if (AnchorMailboxCache.instance == null)
				{
					lock (AnchorMailboxCache.staticLock)
					{
						if (AnchorMailboxCache.instance == null)
						{
							AnchorMailboxCache.instance = new AnchorMailboxCache();
						}
					}
				}
				return AnchorMailboxCache.instance;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000148 RID: 328 RVA: 0x000077B0 File Offset: 0x000059B0
		private static TimeSpan AnchorMailboxCacheAbsoluteTimeout
		{
			get
			{
				if (HttpProxySettings.AnchorMailboxSharedCacheEnabled.Value)
				{
					return AnchorMailboxCache.CacheAbsoluteTimeoutWithSharedCache.Value;
				}
				return AnchorMailboxCache.CacheAbsoluteTimeoutInMemoryCache.Value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000149 RID: 329 RVA: 0x000077D3 File Offset: 0x000059D3
		private static TimeSpan AnchorMailboxCacheSlidingTimeout
		{
			get
			{
				if (HttpProxySettings.AnchorMailboxSharedCacheEnabled.Value)
				{
					return AnchorMailboxCache.CacheSlidingTimeoutWithSharedCache.Value;
				}
				return AnchorMailboxCache.CacheSlidingTimeoutInMemoryCache.Value;
			}
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00007844 File Offset: 0x00005A44
		public bool TryGet(string key, IRequestContext requestContext, out AnchorMailboxCacheEntry entry)
		{
			PerfCounters.HttpProxyCacheCountersInstance.AnchorMailboxLocalCacheHitsRateBase.Increment();
			PerfCounters.HttpProxyCacheCountersInstance.AnchorMailboxOverallCacheHitsRateBase.Increment();
			entry = null;
			if (this.innerCache.TryGetValue(key, out entry))
			{
				PerfCounters.HttpProxyCacheCountersInstance.AnchorMailboxLocalCacheHitsRate.Increment();
			}
			else if (HttpProxySettings.AnchorMailboxSharedCacheEnabled.Value)
			{
				long latency = 0L;
				string diagInfo = null;
				AnchorMailboxCacheEntry sharedCacheEntry = null;
				bool latency2 = LatencyTracker.GetLatency<bool>(() => this.sharedCacheClient.TryGet<AnchorMailboxCacheEntry>(key, requestContext.ActivityId, out sharedCacheEntry, out diagInfo), out latency);
				requestContext.LogSharedCacheCall(latency, diagInfo);
				if (latency2)
				{
					this.Add(key, sharedCacheEntry, requestContext, false);
					entry = sharedCacheEntry;
				}
			}
			if (entry != null)
			{
				PerfCounters.HttpProxyCacheCountersInstance.AnchorMailboxOverallCacheHitsRate.Increment();
				return true;
			}
			return false;
		}

		// Token: 0x0600014B RID: 331 RVA: 0x00007940 File Offset: 0x00005B40
		public void Add(string key, AnchorMailboxCacheEntry entry, IRequestContext requestContext)
		{
			this.Add(key, entry, requestContext, true);
		}

		// Token: 0x0600014C RID: 332 RVA: 0x0000794C File Offset: 0x00005B4C
		public void Add(string key, AnchorMailboxCacheEntry entry, IRequestContext requestContext, bool addToSharedCache)
		{
			this.Add(key, entry, DateTime.UtcNow, requestContext, addToSharedCache);
		}

		// Token: 0x0600014D RID: 333 RVA: 0x000079CC File Offset: 0x00005BCC
		public void Add(string key, AnchorMailboxCacheEntry entry, DateTime valueTimestamp, IRequestContext requestContext, bool addToSharedCache)
		{
			this.innerCache.TryInsertLimitedSliding(key, entry, AnchorMailboxCache.AnchorMailboxCacheAbsoluteTimeout, AnchorMailboxCache.AnchorMailboxCacheSlidingTimeout);
			if (HttpProxySettings.AnchorMailboxSharedCacheEnabled.Value && addToSharedCache)
			{
				long latency = 0L;
				string diagInfo = null;
				LatencyTracker.GetLatency<bool>(() => this.sharedCacheClient.TryInsert(key, entry, valueTimestamp, requestContext.ActivityId, out diagInfo), out latency);
				requestContext.LogSharedCacheCall(latency, diagInfo);
			}
			this.UpdateCacheSizeCounter();
		}

		// Token: 0x0600014E RID: 334 RVA: 0x00007AC0 File Offset: 0x00005CC0
		public void Remove(string key, IRequestContext requestContext)
		{
			this.innerCache.Remove(key);
			if (HttpProxySettings.AnchorMailboxSharedCacheEnabled.Value)
			{
				long latency = 0L;
				string diagInfo = null;
				LatencyTracker.GetLatency<bool>(() => this.sharedCacheClient.TryRemove(key, requestContext.ActivityId, out diagInfo), out latency);
				requestContext.LogSharedCacheCall(latency, diagInfo);
			}
		}

		// Token: 0x0600014F RID: 335 RVA: 0x00007B43 File Offset: 0x00005D43
		private void UpdateCacheSizeCounter()
		{
			if (AnchorMailboxCache.AnchorMailboxCacheSizeCounterUpdateEnabled.Value)
			{
				PerfCounters.HttpProxyCacheCountersInstance.AnchorMailboxCacheSize.RawValue = (long)this.innerCache.Count;
			}
		}

		// Token: 0x04000071 RID: 113
		private static readonly TimeSpanAppSettingsEntry CacheAbsoluteTimeoutInMemoryCache = new TimeSpanAppSettingsEntry(HttpProxySettings.Prefix("AnchorMailboxCache.InMemoryOnly.AbsoluteTimeout"), TimeSpanUnit.Minutes, TimeSpan.FromMinutes(60.0), ExTraceGlobals.VerboseTracer);

		// Token: 0x04000072 RID: 114
		private static readonly TimeSpanAppSettingsEntry CacheAbsoluteTimeoutWithSharedCache = new TimeSpanAppSettingsEntry(HttpProxySettings.Prefix("AnchorMailboxCache.WithSharedCache.AbsoluteTimeout"), TimeSpanUnit.Minutes, TimeSpan.FromMinutes(6.0), ExTraceGlobals.VerboseTracer);

		// Token: 0x04000073 RID: 115
		private static readonly TimeSpanAppSettingsEntry CacheSlidingTimeoutInMemoryCache = new TimeSpanAppSettingsEntry(HttpProxySettings.Prefix("AnchorMailboxCache.InMemoryOnly.SlidingTimeout"), TimeSpanUnit.Minutes, TimeSpan.FromMinutes(30.0), ExTraceGlobals.VerboseTracer);

		// Token: 0x04000074 RID: 116
		private static readonly TimeSpanAppSettingsEntry CacheSlidingTimeoutWithSharedCache = new TimeSpanAppSettingsEntry(HttpProxySettings.Prefix("AnchorMailboxCache.WithSharedCache.SlidingTimeout"), TimeSpanUnit.Minutes, TimeSpan.FromMinutes(3.0), ExTraceGlobals.VerboseTracer);

		// Token: 0x04000075 RID: 117
		private static readonly IntAppSettingsEntry AnchorMailboxCacheSize = new IntAppSettingsEntry(HttpProxySettings.Prefix("AnchorMailboxCache.InMemoryMaxSize"), 100000, ExTraceGlobals.VerboseTracer);

		// Token: 0x04000076 RID: 118
		private static readonly BoolAppSettingsEntry AnchorMailboxCacheSizeCounterUpdateEnabled = new BoolAppSettingsEntry(HttpProxySettings.Prefix("AnchorMailboxCacheSizeCounterUpdateEnabled"), true, ExTraceGlobals.VerboseTracer);

		// Token: 0x04000077 RID: 119
		private static AnchorMailboxCache instance;

		// Token: 0x04000078 RID: 120
		private static object staticLock = new object();

		// Token: 0x04000079 RID: 121
		private readonly ExactTimeoutCache<string, AnchorMailboxCacheEntry> innerCache;

		// Token: 0x0400007A RID: 122
		private SharedCacheClient sharedCacheClient;
	}
}
