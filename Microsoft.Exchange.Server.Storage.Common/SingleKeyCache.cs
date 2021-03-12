using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x02000081 RID: 129
	public class SingleKeyCache<TKey, TCached>
	{
		// Token: 0x0600071C RID: 1820 RVA: 0x00013DE0 File Offset: 0x00011FE0
		public SingleKeyCache(EvictionPolicy<TKey> evictionPolicy, ICachePerformanceCounters perfCounters)
		{
			this.keyToData = new Dictionary<TKey, TCached>(evictionPolicy.Capacity);
			this.evictionPolicy = evictionPolicy;
			this.performanceCounters = perfCounters;
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x00013E07 File Offset: 0x00012007
		public void Insert(TKey key, TCached value)
		{
			this.Insert(key, value, true);
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x00013E14 File Offset: 0x00012014
		public void Insert(TKey key, TCached value, bool shouldEvict)
		{
			if (key == null)
			{
				throw new ArgumentNullException("key");
			}
			using (SingleKeyCache<TKey, TCached>._CriticalConsistencyBlock criticalConsistencyBlock = this.Critical())
			{
				if (shouldEvict)
				{
					this.EvictionCheckpoint();
				}
				bool flag = this.keyToData.ContainsKey(key);
				this.keyToData[key] = value;
				if (flag)
				{
					this.evictionPolicy.KeyAccess(key);
				}
				else
				{
					if (this.performanceCounters != null)
					{
						this.performanceCounters.CacheInserts.Increment();
					}
					this.evictionPolicy.Insert(key);
				}
				if (this.performanceCounters != null)
				{
					this.performanceCounters.CacheExpirationQueueLength.RawValue = (long)this.evictionPolicy.CountOfKeysToCleanup;
					this.performanceCounters.CacheSize.RawValue = (long)this.keyToData.Count;
				}
				criticalConsistencyBlock.Success();
			}
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x00013EFC File Offset: 0x000120FC
		public TCached Find(TKey key)
		{
			return this.Find(key, true);
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x00013F08 File Offset: 0x00012108
		public TCached Find(TKey key, bool shouldEvict)
		{
			TCached result;
			using (SingleKeyCache<TKey, TCached>._CriticalConsistencyBlock criticalConsistencyBlock = this.Critical())
			{
				if (shouldEvict)
				{
					this.EvictionCheckpoint();
				}
				if (this.performanceCounters != null)
				{
					this.performanceCounters.CacheLookups.Increment();
				}
				TCached tcached = default(TCached);
				if (this.keyToData.TryGetValue(key, out tcached))
				{
					this.evictionPolicy.KeyAccess(key);
					if (this.performanceCounters != null)
					{
						this.performanceCounters.CacheExpirationQueueLength.RawValue = (long)this.evictionPolicy.CountOfKeysToCleanup;
					}
					if (!this.evictionPolicy.ContainsKeyToCleanup(key))
					{
						if (this.performanceCounters != null)
						{
							this.performanceCounters.CacheHits.Increment();
						}
					}
					else
					{
						if (this.performanceCounters != null)
						{
							this.performanceCounters.CacheMisses.Increment();
						}
						tcached = default(TCached);
					}
				}
				else if (this.performanceCounters != null)
				{
					this.performanceCounters.CacheMisses.Increment();
				}
				criticalConsistencyBlock.Success();
				result = tcached;
			}
			return result;
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x00014018 File Offset: 0x00012218
		public void Remove(TKey key)
		{
			this.Remove(key, true);
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x00014024 File Offset: 0x00012224
		public void Remove(TKey key, bool shouldEvict)
		{
			using (SingleKeyCache<TKey, TCached>._CriticalConsistencyBlock criticalConsistencyBlock = this.Critical())
			{
				if (shouldEvict)
				{
					this.EvictionCheckpoint();
				}
				if (this.keyToData.Remove(key))
				{
					this.evictionPolicy.Remove(key);
					if (this.performanceCounters != null)
					{
						this.performanceCounters.CacheRemoves.Increment();
						this.performanceCounters.CacheSize.RawValue = (long)this.keyToData.Count;
						this.performanceCounters.CacheExpirationQueueLength.RawValue = (long)this.evictionPolicy.CountOfKeysToCleanup;
					}
				}
				criticalConsistencyBlock.Success();
			}
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x000140D4 File Offset: 0x000122D4
		public virtual void Reset()
		{
			this.keyToData.Clear();
			this.evictionPolicy.Reset();
			if (this.performanceCounters != null)
			{
				this.performanceCounters.CacheExpirationQueueLength.RawValue = 0L;
				this.performanceCounters.CacheSize.RawValue = 0L;
			}
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x00014124 File Offset: 0x00012324
		public virtual void EvictionCheckpoint()
		{
			this.evictionPolicy.EvictionCheckpoint();
			if (this.performanceCounters != null)
			{
				this.performanceCounters.CacheExpirationQueueLength.RawValue = (long)this.evictionPolicy.CountOfKeysToCleanup;
			}
			if (this.evictionPolicy.CountOfKeysToCleanup > 0)
			{
				foreach (TKey key in this.evictionPolicy.GetKeysToCleanup(true))
				{
					this.keyToData.Remove(key);
					if (this.performanceCounters != null)
					{
						this.performanceCounters.CacheRemoves.Increment();
					}
				}
			}
			if (this.performanceCounters != null)
			{
				this.performanceCounters.CacheSize.RawValue = (long)this.keyToData.Count;
				this.performanceCounters.CacheExpirationQueueLength.RawValue = (long)this.evictionPolicy.CountOfKeysToCleanup;
			}
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x000141F8 File Offset: 0x000123F8
		private SingleKeyCache<TKey, TCached>._CriticalConsistencyBlock Critical()
		{
			return new SingleKeyCache<TKey, TCached>._CriticalConsistencyBlock
			{
				Cache = this
			};
		}

		// Token: 0x0400066D RID: 1645
		protected Dictionary<TKey, TCached> keyToData;

		// Token: 0x0400066E RID: 1646
		protected ICachePerformanceCounters performanceCounters;

		// Token: 0x0400066F RID: 1647
		protected EvictionPolicy<TKey> evictionPolicy;

		// Token: 0x02000082 RID: 130
		private struct _CriticalConsistencyBlock : IDisposable
		{
			// Token: 0x06000726 RID: 1830 RVA: 0x00014216 File Offset: 0x00012416
			public void Dispose()
			{
				if (this.Cache != null)
				{
					this.Cache.Reset();
				}
			}

			// Token: 0x06000727 RID: 1831 RVA: 0x0001422B File Offset: 0x0001242B
			public void Success()
			{
				this.Cache = null;
			}

			// Token: 0x04000670 RID: 1648
			public SingleKeyCache<TKey, TCached> Cache;
		}
	}
}
