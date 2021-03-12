using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x0200006A RID: 106
	public class MultiKeyCache<TCached, TKey>
	{
		// Token: 0x060005DE RID: 1502 RVA: 0x00010254 File Offset: 0x0000E454
		public MultiKeyCache(EvictionPolicy<TKey> evictionPolicy, ICachePerformanceCounters perfCounters)
		{
			this.data = new Dictionary<TKey, MultiKeyCache<TCached, TKey>._CacheEntry<TCached, TKey>>(evictionPolicy.Capacity);
			this.secondaryKeys = new Dictionary<TKey, TKey>(evictionPolicy.Capacity);
			this.evictionPolicy = evictionPolicy;
			this.performanceCounters = perfCounters;
		}

		// Token: 0x060005DF RID: 1503 RVA: 0x0001028C File Offset: 0x0000E48C
		public void Insert(TCached value, TKey primaryKey, TKey lookupKey)
		{
			using (MultiKeyCache<TCached, TKey>._CriticalConsistencyBlock criticalConsistencyBlock = this.Critical())
			{
				this.EvictionCheckpoint();
				if (this.secondaryKeys.ContainsKey(lookupKey))
				{
					TKey tkey = this.secondaryKeys[lookupKey];
					if (!primaryKey.Equals(tkey))
					{
						criticalConsistencyBlock.Success();
					}
					else
					{
						this.data[primaryKey].Value = value;
						this.evictionPolicy.KeyAccess(primaryKey);
						if (this.performanceCounters != null)
						{
							this.performanceCounters.CacheExpirationQueueLength.RawValue = (long)this.evictionPolicy.CountOfKeysToCleanup;
						}
						criticalConsistencyBlock.Success();
					}
				}
				else if (this.data.ContainsKey(primaryKey))
				{
					this.data[primaryKey].Value = value;
					this.data[primaryKey].Keys.Add(lookupKey);
					this.secondaryKeys[lookupKey] = primaryKey;
					this.evictionPolicy.KeyAccess(primaryKey);
					if (this.performanceCounters != null)
					{
						this.performanceCounters.CacheExpirationQueueLength.RawValue = (long)this.evictionPolicy.CountOfKeysToCleanup;
					}
					criticalConsistencyBlock.Success();
				}
				else
				{
					this.data[primaryKey] = new MultiKeyCache<TCached, TKey>._CacheEntry<TCached, TKey>();
					this.data[primaryKey].Value = value;
					this.data[primaryKey].Keys.Add(lookupKey);
					this.secondaryKeys[lookupKey] = primaryKey;
					this.evictionPolicy.Insert(primaryKey);
					if (this.performanceCounters != null)
					{
						this.performanceCounters.CacheExpirationQueueLength.RawValue = (long)this.evictionPolicy.CountOfKeysToCleanup;
						this.performanceCounters.CacheInserts.Increment();
						this.performanceCounters.CacheSize.RawValue = (long)this.data.Count;
					}
					criticalConsistencyBlock.Success();
				}
			}
		}

		// Token: 0x060005E0 RID: 1504 RVA: 0x00010488 File Offset: 0x0000E688
		public void Remove(TKey key)
		{
			using (MultiKeyCache<TCached, TKey>._CriticalConsistencyBlock criticalConsistencyBlock = this.Critical())
			{
				this.EvictionCheckpoint();
				TKey key2 = default(TKey);
				if (this.secondaryKeys.ContainsKey(key))
				{
					key2 = this.secondaryKeys[key];
				}
				else
				{
					key2 = key;
				}
				if (this.data.ContainsKey(key2))
				{
					foreach (TKey key3 in this.data[key2].Keys)
					{
						this.secondaryKeys.Remove(key3);
					}
					this.data.Remove(key2);
					this.evictionPolicy.Remove(key2);
					if (this.performanceCounters != null)
					{
						this.performanceCounters.CacheExpirationQueueLength.RawValue = (long)this.evictionPolicy.CountOfKeysToCleanup;
						this.performanceCounters.CacheRemoves.Increment();
						this.performanceCounters.CacheSize.RawValue = (long)this.data.Count;
					}
				}
				criticalConsistencyBlock.Success();
			}
		}

		// Token: 0x060005E1 RID: 1505 RVA: 0x000105C0 File Offset: 0x0000E7C0
		public TCached Find(TKey key)
		{
			TCached result = default(TCached);
			using (MultiKeyCache<TCached, TKey>._CriticalConsistencyBlock criticalConsistencyBlock = this.Critical())
			{
				this.EvictionCheckpoint();
				if (this.performanceCounters != null)
				{
					this.performanceCounters.CacheLookups.Increment();
				}
				TKey key2 = default(TKey);
				if (this.secondaryKeys.ContainsKey(key))
				{
					key2 = this.secondaryKeys[key];
				}
				else
				{
					key2 = key;
				}
				if (this.data.ContainsKey(key2))
				{
					this.evictionPolicy.KeyAccess(key2);
					if (this.performanceCounters != null)
					{
						this.performanceCounters.CacheExpirationQueueLength.RawValue = (long)this.evictionPolicy.CountOfKeysToCleanup;
					}
					if (!this.evictionPolicy.ContainsKeyToCleanup(key2))
					{
						if (this.performanceCounters != null)
						{
							this.performanceCounters.CacheHits.Increment();
						}
						result = this.data[key2].Value;
					}
					else if (this.performanceCounters != null)
					{
						this.performanceCounters.CacheMisses.Increment();
					}
				}
				else if (this.performanceCounters != null)
				{
					this.performanceCounters.CacheMisses.Increment();
				}
				criticalConsistencyBlock.Success();
			}
			return result;
		}

		// Token: 0x060005E2 RID: 1506 RVA: 0x000106FC File Offset: 0x0000E8FC
		public void ResetCache()
		{
			this.data.Clear();
			this.secondaryKeys.Clear();
			this.evictionPolicy.Reset();
			if (this.performanceCounters != null)
			{
				this.performanceCounters.CacheExpirationQueueLength.RawValue = 0L;
				this.performanceCounters.CacheSize.RawValue = 0L;
			}
		}

		// Token: 0x060005E3 RID: 1507 RVA: 0x00010758 File Offset: 0x0000E958
		private MultiKeyCache<TCached, TKey>._CriticalConsistencyBlock Critical()
		{
			return new MultiKeyCache<TCached, TKey>._CriticalConsistencyBlock
			{
				Cache = this
			};
		}

		// Token: 0x060005E4 RID: 1508 RVA: 0x00010778 File Offset: 0x0000E978
		protected void EvictionCheckpoint()
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
					foreach (TKey key2 in this.data[key].Keys)
					{
						this.secondaryKeys.Remove(key2);
					}
					this.data.Remove(key);
					if (this.performanceCounters != null)
					{
						this.performanceCounters.CacheRemoves.Increment();
					}
				}
				if (this.performanceCounters != null)
				{
					this.performanceCounters.CacheSize.RawValue = (long)this.data.Count;
					this.performanceCounters.CacheExpirationQueueLength.RawValue = (long)this.evictionPolicy.CountOfKeysToCleanup;
				}
			}
		}

		// Token: 0x040005FA RID: 1530
		private Dictionary<TKey, MultiKeyCache<TCached, TKey>._CacheEntry<TCached, TKey>> data;

		// Token: 0x040005FB RID: 1531
		private Dictionary<TKey, TKey> secondaryKeys;

		// Token: 0x040005FC RID: 1532
		private EvictionPolicy<TKey> evictionPolicy;

		// Token: 0x040005FD RID: 1533
		private ICachePerformanceCounters performanceCounters;

		// Token: 0x0200006B RID: 107
		private class _CacheEntry<_TCached, _TKey>
		{
			// Token: 0x060005E5 RID: 1509 RVA: 0x000108A8 File Offset: 0x0000EAA8
			public _CacheEntry()
			{
				this.value = default(_TCached);
				this.keys = new List<_TKey>();
			}

			// Token: 0x1700015D RID: 349
			// (get) Token: 0x060005E6 RID: 1510 RVA: 0x000108C7 File Offset: 0x0000EAC7
			// (set) Token: 0x060005E7 RID: 1511 RVA: 0x000108CF File Offset: 0x0000EACF
			public _TCached Value
			{
				get
				{
					return this.value;
				}
				set
				{
					this.value = value;
				}
			}

			// Token: 0x1700015E RID: 350
			// (get) Token: 0x060005E8 RID: 1512 RVA: 0x000108D8 File Offset: 0x0000EAD8
			public List<_TKey> Keys
			{
				get
				{
					return this.keys;
				}
			}

			// Token: 0x040005FE RID: 1534
			private _TCached value;

			// Token: 0x040005FF RID: 1535
			private List<_TKey> keys;
		}

		// Token: 0x0200006C RID: 108
		private struct _CriticalConsistencyBlock : IDisposable
		{
			// Token: 0x060005E9 RID: 1513 RVA: 0x000108E0 File Offset: 0x0000EAE0
			public void Dispose()
			{
				if (this.Cache != null)
				{
					this.Cache.ResetCache();
				}
			}

			// Token: 0x060005EA RID: 1514 RVA: 0x000108F5 File Offset: 0x0000EAF5
			public void Success()
			{
				this.Cache = null;
			}

			// Token: 0x04000600 RID: 1536
			public MultiKeyCache<TCached, TKey> Cache;
		}
	}
}
