using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.Caching;
using Microsoft.Exchange.Common.Reputation;

namespace Microsoft.Exchange.Hygiene.Data.Optics
{
	// Token: 0x020001B6 RID: 438
	internal class ReputationQueryCache
	{
		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x0600124F RID: 4687 RVA: 0x00038033 File Offset: 0x00036233
		// (set) Token: 0x06001250 RID: 4688 RVA: 0x0003803B File Offset: 0x0003623B
		public bool Initialized { get; private set; }

		// Token: 0x06001251 RID: 4689 RVA: 0x00038044 File Offset: 0x00036244
		public void InitializeCache()
		{
			if (this.Initialized)
			{
				return;
			}
			lock (this.cacheInitializeLock)
			{
				if (!this.Initialized)
				{
					this.caches = new Dictionary<ReputationEntityType, MemoryCache>();
					for (ReputationEntityType reputationEntityType = ReputationEntityType.IP; reputationEntityType < ReputationEntityType.Max; reputationEntityType += 1)
					{
						this.caches.Add(reputationEntityType, new MemoryCache(string.Format("ReputationCache_{0}", reputationEntityType.ToString()), new NameValueCollection
						{
							{
								"physicalMemoryLimitPercentage",
								"1"
							},
							{
								"pollingInterval",
								"00:00:01"
							}
						}));
					}
					this.Initialized = true;
				}
			}
		}

		// Token: 0x06001252 RID: 4690 RVA: 0x00038100 File Offset: 0x00036300
		public bool TryGetValue(ReputationEntityType entityType, int dataPoint, string reputationEntityKey, out long value)
		{
			value = 0L;
			MemoryCache memoryCache = null;
			if (!this.caches.TryGetValue(entityType, out memoryCache))
			{
				return false;
			}
			object obj = memoryCache.Get(this.GetCacheIndex(reputationEntityKey, dataPoint), null);
			if (obj == null)
			{
				return false;
			}
			if (obj.GetType() != typeof(long))
			{
				return false;
			}
			value = (long)obj;
			return true;
		}

		// Token: 0x06001253 RID: 4691 RVA: 0x00038160 File Offset: 0x00036360
		public bool TryAddValue(ReputationEntityType entityType, int dataPoint, string reputationEntityKey, long value, int ttl)
		{
			MemoryCache memoryCache = null;
			return this.caches.TryGetValue(entityType, out memoryCache) && ttl >= 0 && memoryCache.Add(new CacheItem(this.GetCacheIndex(reputationEntityKey, dataPoint), value), new CacheItemPolicy
			{
				SlidingExpiration = TimeSpan.FromMilliseconds((double)ttl)
			});
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x000381B7 File Offset: 0x000363B7
		private string GetCacheIndex(string reputationEntityKey, int dataPoint)
		{
			return string.Format("{0}:{1}", reputationEntityKey, dataPoint);
		}

		// Token: 0x040008C4 RID: 2244
		private Dictionary<ReputationEntityType, MemoryCache> caches;

		// Token: 0x040008C5 RID: 2245
		private object cacheInitializeLock = new object();
	}
}
