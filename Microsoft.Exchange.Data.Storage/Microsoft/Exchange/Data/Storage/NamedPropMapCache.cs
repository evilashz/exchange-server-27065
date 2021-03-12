using System;
using System.Threading;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000C8C RID: 3212
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class NamedPropMapCache
	{
		// Token: 0x06007064 RID: 28772 RVA: 0x001F1ECC File Offset: 0x001F00CC
		internal NamedPropMapCache()
		{
			this.perfCounters.NamedPropertyCacheEntries.RawValue = 0L;
			this.currentMappingSize = 0;
			Action<int> mappingSizeChanged = delegate(int sizeDelta)
			{
				Interlocked.Add(ref this.currentMappingSize, sizeDelta);
			};
			double minSizeRatio = 0.9;
			this.lru = new LRUCache<string, NamedPropMap>(this.namedPropertyCacheNumberOfUsers, (string key) => new NamedPropMap(mappingSizeChanged), new double?(minSizeRatio), delegate()
			{
				this.perfCounters.NamedPropertyCacheMisses_Base.Increment();
			}, delegate()
			{
				this.perfCounters.NamedPropertyCacheMisses.Increment();
			}, () => this.currentMappingSize >= this.namedPropertyCachePropertiesPerUser * this.namedPropertyCacheNumberOfUsers, () => (double)this.currentMappingSize >= minSizeRatio * (double)this.namedPropertyCachePropertiesPerUser * (double)this.namedPropertyCacheNumberOfUsers, new Action<NamedPropMap>(this.ElementEvictCallback));
		}

		// Token: 0x06007065 RID: 28773 RVA: 0x001F1FC8 File Offset: 0x001F01C8
		internal NamedPropMap GetMapping(string signature)
		{
			if (string.IsNullOrEmpty(signature))
			{
				return null;
			}
			bool flag;
			NamedPropMap result = this.lru.Get(signature, out flag);
			if (flag)
			{
				NamedPropertyDefinition.NamedPropertyKey.ClearUnreferenced();
			}
			return result;
		}

		// Token: 0x06007066 RID: 28774 RVA: 0x001F1FF8 File Offset: 0x001F01F8
		private void ElementEvictCallback(NamedPropMap namedPropMapEvicted)
		{
			int num = namedPropMapEvicted.UnregisterSizeChangedDelegate();
			Interlocked.Add(ref this.currentMappingSize, -num);
			this.perfCounters.NamedPropertyCacheEntries.IncrementBy((long)(-(long)num));
		}

		// Token: 0x17001E33 RID: 7731
		// (get) Token: 0x06007067 RID: 28775 RVA: 0x001F202E File Offset: 0x001F022E
		public static NamedPropMapCache Default
		{
			get
			{
				return NamedPropMapCache.defaultInstance;
			}
		}

		// Token: 0x06007068 RID: 28776 RVA: 0x001F2035 File Offset: 0x001F0235
		internal NamedPropMap GetMapping(StoreSession storeSession)
		{
			if (storeSession != null)
			{
				return storeSession.NamedPropertyResolutionCache;
			}
			return null;
		}

		// Token: 0x06007069 RID: 28777 RVA: 0x001F20A0 File Offset: 0x001F02A0
		internal void UpdateCacheLimits(int namedPropertyCacheNumberOfUsers, int namedPropertyCachePropertiesPerUser, out int oldNamedPropertyCacheNumberOfUsers, out int oldNamedPropertyCachePropertiesPerUser)
		{
			int localNamedPropertyCacheNumberOfUsers = 0;
			int localNamedPropertyCachePropertiesPerUser = 0;
			this.lru.UpdateCapacity(this.namedPropertyCacheNumberOfUsers, delegate
			{
				localNamedPropertyCacheNumberOfUsers = this.namedPropertyCacheNumberOfUsers;
				localNamedPropertyCachePropertiesPerUser = this.namedPropertyCachePropertiesPerUser;
				this.namedPropertyCacheNumberOfUsers = namedPropertyCacheNumberOfUsers;
				this.namedPropertyCachePropertiesPerUser = namedPropertyCachePropertiesPerUser;
			});
			oldNamedPropertyCacheNumberOfUsers = localNamedPropertyCacheNumberOfUsers;
			oldNamedPropertyCachePropertiesPerUser = localNamedPropertyCachePropertiesPerUser;
		}

		// Token: 0x0600706A RID: 28778 RVA: 0x001F2104 File Offset: 0x001F0304
		internal void Reset()
		{
			this.lru.Reset();
			NamedPropertyDefinition.NamedPropertyKey.ClearUnreferenced();
		}

		// Token: 0x04004D8D RID: 19853
		private const int NamedPropertyCachePropertiesPerUserDefault = 100;

		// Token: 0x04004D8E RID: 19854
		private const int NamedPropertyCacheNumberOfUsersDefault = 2500;

		// Token: 0x04004D8F RID: 19855
		private readonly MiddleTierStoragePerformanceCountersInstance perfCounters = NamedPropMap.GetPerfCounters();

		// Token: 0x04004D90 RID: 19856
		private static readonly NamedPropMapCache defaultInstance = new NamedPropMapCache();

		// Token: 0x04004D91 RID: 19857
		private int namedPropertyCachePropertiesPerUser = 100;

		// Token: 0x04004D92 RID: 19858
		private int namedPropertyCacheNumberOfUsers = 100;

		// Token: 0x04004D93 RID: 19859
		private LRUCache<string, NamedPropMap> lru;

		// Token: 0x04004D94 RID: 19860
		private int currentMappingSize;
	}
}
