using System;
using Microsoft.Exchange.Collections.TimeoutCache;
using Microsoft.Exchange.Diagnostics.Components.HttpProxy;
using Microsoft.Exchange.HttpProxy.Common;
using Microsoft.Exchange.Net;

namespace Microsoft.Exchange.HttpProxy
{
	// Token: 0x02000032 RID: 50
	internal class NegativeAnchorMailboxCache
	{
		// Token: 0x06000188 RID: 392 RVA: 0x000090F8 File Offset: 0x000072F8
		internal NegativeAnchorMailboxCache(TimeSpan cacheAbsoluteTimeout, TimeSpan gen1Timeout, TimeSpan gen2Timeout)
		{
			this.cacheAbsoluteTimeout = cacheAbsoluteTimeout;
			this.gen1Timeout = gen1Timeout;
			this.gen2Timeout = gen2Timeout;
			this.innerCache = new ExactTimeoutCache<string, NegativeAnchorMailboxCacheEntry>(delegate(string k, NegativeAnchorMailboxCacheEntry v, RemoveReason r)
			{
				this.UpdateCacheSizeCounter();
			}, null, null, NegativeAnchorMailboxCache.NegativeAnchorMailboxCacheSize.Value, false);
		}

		// Token: 0x06000189 RID: 393 RVA: 0x0000914B File Offset: 0x0000734B
		private NegativeAnchorMailboxCache() : this(NegativeAnchorMailboxCache.CacheAbsoluteTimeout.Value, NegativeAnchorMailboxCache.Gen1Timeout.Value, NegativeAnchorMailboxCache.Gen2Timeout.Value)
		{
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x0600018A RID: 394 RVA: 0x00009171 File Offset: 0x00007371
		public static NegativeAnchorMailboxCache Instance
		{
			get
			{
				return NegativeAnchorMailboxCache.StaticInstance;
			}
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00009178 File Offset: 0x00007378
		public void Add(string key, NegativeAnchorMailboxCacheEntry entry)
		{
			TimeSpan timeSpan = this.cacheAbsoluteTimeout;
			NegativeAnchorMailboxCacheEntry negativeAnchorMailboxCacheEntry;
			if (!this.TryGet(key, false, out negativeAnchorMailboxCacheEntry))
			{
				entry.StartTime = DateTime.UtcNow;
				entry.Generation = NegativeAnchorMailboxCacheEntry.CacheGeneration.One;
				this.Add(key, entry, timeSpan, true);
				return;
			}
			double num;
			NegativeAnchorMailboxCacheEntry.CacheGeneration generation;
			if (!this.IsDueForRefresh(negativeAnchorMailboxCacheEntry, out num, out generation))
			{
				return;
			}
			if (timeSpan.TotalSeconds > num)
			{
				negativeAnchorMailboxCacheEntry.Generation = generation;
				this.Add(key, negativeAnchorMailboxCacheEntry, timeSpan - TimeSpan.FromSeconds(num), false);
			}
		}

		// Token: 0x0600018C RID: 396 RVA: 0x000091EC File Offset: 0x000073EC
		public bool TryGet(string key, out NegativeAnchorMailboxCacheEntry entry)
		{
			if (!this.TryGet(key, true, out entry))
			{
				return false;
			}
			double num;
			NegativeAnchorMailboxCacheEntry.CacheGeneration cacheGeneration;
			if (this.IsDueForRefresh(entry, out num, out cacheGeneration))
			{
				return false;
			}
			PerfCounters.HttpProxyCacheCountersInstance.NegativeAnchorMailboxLocalCacheHitsRate.Increment();
			return true;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00009227 File Offset: 0x00007427
		public void Remove(string key)
		{
			this.innerCache.Remove(key);
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00009238 File Offset: 0x00007438
		private bool IsDueForRefresh(NegativeAnchorMailboxCacheEntry entry, out double timeElapsedInSeconds, out NegativeAnchorMailboxCacheEntry.CacheGeneration nextGeneration)
		{
			timeElapsedInSeconds = 0.0;
			nextGeneration = NegativeAnchorMailboxCacheEntry.CacheGeneration.Max;
			if (entry.Generation == NegativeAnchorMailboxCacheEntry.CacheGeneration.Max)
			{
				return false;
			}
			timeElapsedInSeconds = (DateTime.UtcNow - entry.StartTime).TotalSeconds;
			if (timeElapsedInSeconds > this.gen2Timeout.TotalSeconds)
			{
				nextGeneration = NegativeAnchorMailboxCacheEntry.CacheGeneration.Max;
				return true;
			}
			if (timeElapsedInSeconds > this.gen1Timeout.TotalSeconds)
			{
				nextGeneration = NegativeAnchorMailboxCacheEntry.CacheGeneration.Two;
				return entry.Generation == NegativeAnchorMailboxCacheEntry.CacheGeneration.One;
			}
			return false;
		}

		// Token: 0x0600018F RID: 399 RVA: 0x000092BB File Offset: 0x000074BB
		private bool TryGet(string key, bool updatePerfCounter, out NegativeAnchorMailboxCacheEntry entry)
		{
			if (updatePerfCounter)
			{
				PerfCounters.HttpProxyCacheCountersInstance.NegativeAnchorMailboxLocalCacheHitsRateBase.Increment();
			}
			entry = null;
			return this.innerCache.TryGetValue(key, out entry);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x000092E5 File Offset: 0x000074E5
		private void Add(string key, NegativeAnchorMailboxCacheEntry entry, TimeSpan timeout, bool updatePerfCounter)
		{
			this.innerCache.TryInsertAbsolute(key, entry, timeout);
			if (updatePerfCounter)
			{
				this.UpdateCacheSizeCounter();
			}
		}

		// Token: 0x06000191 RID: 401 RVA: 0x00009300 File Offset: 0x00007500
		private void UpdateCacheSizeCounter()
		{
			PerfCounters.HttpProxyCacheCountersInstance.NegativeAnchorMailboxCacheSize.RawValue = (long)this.innerCache.Count;
		}

		// Token: 0x0400009B RID: 155
		private static readonly TimeSpanAppSettingsEntry CacheAbsoluteTimeout = new TimeSpanAppSettingsEntry(HttpProxySettings.Prefix("NegativeAnchorMailboxCacheAbsoluteTimeout"), TimeSpanUnit.Seconds, TimeSpan.FromSeconds(86400.0), ExTraceGlobals.VerboseTracer);

		// Token: 0x0400009C RID: 156
		private static readonly TimeSpanAppSettingsEntry Gen1Timeout = new TimeSpanAppSettingsEntry(HttpProxySettings.Prefix("NegativeAnchorMailboxCacheG1Timeout"), TimeSpanUnit.Seconds, TimeSpan.FromSeconds(300.0), ExTraceGlobals.VerboseTracer);

		// Token: 0x0400009D RID: 157
		private static readonly TimeSpanAppSettingsEntry Gen2Timeout = new TimeSpanAppSettingsEntry(HttpProxySettings.Prefix("NegativeAnchorMailboxCacheG2Timeout"), TimeSpanUnit.Seconds, TimeSpan.FromSeconds(2100.0), ExTraceGlobals.VerboseTracer);

		// Token: 0x0400009E RID: 158
		private static readonly IntAppSettingsEntry NegativeAnchorMailboxCacheSize = new IntAppSettingsEntry(HttpProxySettings.Prefix("NegativeAnchorMailboxCacheSize"), 4000, ExTraceGlobals.VerboseTracer);

		// Token: 0x0400009F RID: 159
		private static readonly NegativeAnchorMailboxCache StaticInstance = new NegativeAnchorMailboxCache();

		// Token: 0x040000A0 RID: 160
		private readonly ExactTimeoutCache<string, NegativeAnchorMailboxCacheEntry> innerCache;

		// Token: 0x040000A1 RID: 161
		private readonly TimeSpan cacheAbsoluteTimeout;

		// Token: 0x040000A2 RID: 162
		private readonly TimeSpan gen1Timeout;

		// Token: 0x040000A3 RID: 163
		private readonly TimeSpan gen2Timeout;
	}
}
