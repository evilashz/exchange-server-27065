using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x020000BC RID: 188
	public class TypedMultiKeyCache<TCached, TPrimaryKey>
	{
		// Token: 0x060008A7 RID: 2215 RVA: 0x00019CA8 File Offset: 0x00017EA8
		protected void RegisterKeyDefinition(TypedMultiKeyCache<TCached, TPrimaryKey>.IKeyDefinition keyDefinition)
		{
			this.keyDefinitions.Add(keyDefinition);
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x00019CB6 File Offset: 0x00017EB6
		public TypedMultiKeyCache(EvictionPolicy<TPrimaryKey> evictionPolicy, ICachePerformanceCounters perfCounters)
		{
			this.primaryKeyToData = new Dictionary<TPrimaryKey, TCached>(evictionPolicy.Capacity);
			this.keyDefinitions = new List<TypedMultiKeyCache<TCached, TPrimaryKey>.IKeyDefinition>();
			this.evictionPolicy = evictionPolicy;
			this.performanceCounters = perfCounters;
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x00019CE8 File Offset: 0x00017EE8
		protected void Remove(TPrimaryKey primaryKey)
		{
			this.RemoveDataAndSecondaryKeys(primaryKey);
			this.evictionPolicy.Remove(primaryKey);
			if (this.performanceCounters != null)
			{
				this.performanceCounters.CacheRemoves.Increment();
				this.performanceCounters.CacheSize.RawValue = (long)this.primaryKeyToData.Count;
				this.performanceCounters.CacheExpirationQueueLength.RawValue = (long)this.evictionPolicy.CountOfKeysToCleanup;
			}
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x00019D5C File Offset: 0x00017F5C
		protected void RemoveDataAndSecondaryKeys(TPrimaryKey primaryKey)
		{
			this.primaryKeyToData.Remove(primaryKey);
			foreach (TypedMultiKeyCache<TCached, TPrimaryKey>.IKeyDefinition keyDefinition in this.keyDefinitions)
			{
				keyDefinition.Remove(primaryKey);
			}
		}

		// Token: 0x060008AB RID: 2219 RVA: 0x00019DBC File Offset: 0x00017FBC
		protected void Reset()
		{
			this.primaryKeyToData.Clear();
			foreach (TypedMultiKeyCache<TCached, TPrimaryKey>.IKeyDefinition keyDefinition in this.keyDefinitions)
			{
				keyDefinition.ResetCalledFromCache();
			}
			this.evictionPolicy.Reset();
			if (this.performanceCounters != null)
			{
				this.performanceCounters.CacheExpirationQueueLength.RawValue = 0L;
				this.performanceCounters.CacheSize.RawValue = 0L;
			}
		}

		// Token: 0x060008AC RID: 2220 RVA: 0x00019E50 File Offset: 0x00018050
		protected TypedMultiKeyCache<TCached, TPrimaryKey>._CriticalConsistencyBlock Critical()
		{
			return new TypedMultiKeyCache<TCached, TPrimaryKey>._CriticalConsistencyBlock
			{
				Cache = this
			};
		}

		// Token: 0x060008AD RID: 2221 RVA: 0x00019E70 File Offset: 0x00018070
		protected void EvictionCheckpoint()
		{
			this.evictionPolicy.EvictionCheckpoint();
			if (this.performanceCounters != null)
			{
				this.performanceCounters.CacheExpirationQueueLength.RawValue = (long)this.evictionPolicy.CountOfKeysToCleanup;
			}
			if (this.evictionPolicy.CountOfKeysToCleanup > 0)
			{
				foreach (TPrimaryKey primaryKey in this.evictionPolicy.GetKeysToCleanup(true))
				{
					this.RemoveDataAndSecondaryKeys(primaryKey);
					if (this.performanceCounters != null)
					{
						this.performanceCounters.CacheRemoves.Increment();
					}
				}
			}
			if (this.performanceCounters != null)
			{
				this.performanceCounters.CacheSize.RawValue = (long)this.primaryKeyToData.Count;
				this.performanceCounters.CacheExpirationQueueLength.RawValue = (long)this.evictionPolicy.CountOfKeysToCleanup;
			}
		}

		// Token: 0x04000733 RID: 1843
		private Dictionary<TPrimaryKey, TCached> primaryKeyToData;

		// Token: 0x04000734 RID: 1844
		private List<TypedMultiKeyCache<TCached, TPrimaryKey>.IKeyDefinition> keyDefinitions;

		// Token: 0x04000735 RID: 1845
		private ICachePerformanceCounters performanceCounters;

		// Token: 0x04000736 RID: 1846
		protected EvictionPolicy<TPrimaryKey> evictionPolicy;

		// Token: 0x020000BD RID: 189
		protected interface IKeyDefinition
		{
			// Token: 0x060008AE RID: 2222
			void Remove(TPrimaryKey primaryKey);

			// Token: 0x060008AF RID: 2223
			void Reset();

			// Token: 0x060008B0 RID: 2224
			void ResetCalledFromCache();
		}

		// Token: 0x020000BE RID: 190
		public class KeyDefinition<TSecondaryKey> : TypedMultiKeyCache<TCached, TPrimaryKey>.IKeyDefinition
		{
			// Token: 0x060008B1 RID: 2225 RVA: 0x00019F40 File Offset: 0x00018140
			void TypedMultiKeyCache<!0, !1>.IKeyDefinition.Remove(TPrimaryKey primaryKey)
			{
				List<TSecondaryKey> list;
				if (this.primaryKeyToSecondaryKeys.TryGetValue(primaryKey, out list))
				{
					foreach (TSecondaryKey key in list)
					{
						this.secondaryKeyToPrimaryKey.Remove(key);
					}
					this.primaryKeyToSecondaryKeys.Remove(primaryKey);
				}
			}

			// Token: 0x060008B2 RID: 2226 RVA: 0x00019FB4 File Offset: 0x000181B4
			void TypedMultiKeyCache<!0, !1>.IKeyDefinition.Reset()
			{
				this.cache.Reset();
			}

			// Token: 0x060008B3 RID: 2227 RVA: 0x00019FC1 File Offset: 0x000181C1
			void TypedMultiKeyCache<!0, !1>.IKeyDefinition.ResetCalledFromCache()
			{
				this.primaryKeyToSecondaryKeys.Clear();
				this.secondaryKeyToPrimaryKey.Clear();
			}

			// Token: 0x060008B4 RID: 2228 RVA: 0x00019FD9 File Offset: 0x000181D9
			public KeyDefinition(TypedMultiKeyCache<TCached, TPrimaryKey> cache, int capacity)
			{
				this.secondaryKeyToPrimaryKey = new Dictionary<TSecondaryKey, TPrimaryKey>(capacity);
				this.primaryKeyToSecondaryKeys = new Dictionary<TPrimaryKey, List<TSecondaryKey>>(capacity);
				this.cache = cache;
			}

			// Token: 0x060008B5 RID: 2229 RVA: 0x0001A000 File Offset: 0x00018200
			public IEnumerable<TSecondaryKey> GetKeys()
			{
				IList<TSecondaryKey> list;
				using (TypedMultiKeyCache<TCached, TPrimaryKey>._CriticalConsistencyBlock criticalConsistencyBlock = this.cache.Critical())
				{
					list = new List<TSecondaryKey>(this.primaryKeyToSecondaryKeys.Count);
					foreach (TPrimaryKey key in this.primaryKeyToSecondaryKeys.Keys)
					{
						bool flag = this.primaryKeyToSecondaryKeys[key].Count > 0;
						if (flag)
						{
							list.Add(this.primaryKeyToSecondaryKeys[key][0]);
						}
					}
					criticalConsistencyBlock.Success();
				}
				return list;
			}

			// Token: 0x060008B6 RID: 2230 RVA: 0x0001A0C4 File Offset: 0x000182C4
			public void Insert(TCached value, TPrimaryKey primaryKey, TSecondaryKey secondaryKey)
			{
				if (primaryKey == null)
				{
					throw new ArgumentNullException("primaryKey");
				}
				if (secondaryKey == null)
				{
					throw new ArgumentNullException("secondary key");
				}
				using (TypedMultiKeyCache<TCached, TPrimaryKey>._CriticalConsistencyBlock criticalConsistencyBlock = this.cache.Critical())
				{
					this.cache.EvictionCheckpoint();
					TPrimaryKey tprimaryKey;
					if (this.secondaryKeyToPrimaryKey.TryGetValue(secondaryKey, out tprimaryKey))
					{
						if (!primaryKey.Equals(tprimaryKey))
						{
							criticalConsistencyBlock.Success();
							return;
						}
						this.cache.primaryKeyToData[primaryKey] = value;
						this.cache.evictionPolicy.KeyAccess(primaryKey);
						if (this.cache.performanceCounters != null)
						{
							this.cache.performanceCounters.CacheExpirationQueueLength.RawValue = (long)this.cache.evictionPolicy.CountOfKeysToCleanup;
						}
					}
					else
					{
						this.secondaryKeyToPrimaryKey[secondaryKey] = primaryKey;
						if (!this.primaryKeyToSecondaryKeys.ContainsKey(primaryKey))
						{
							this.primaryKeyToSecondaryKeys[primaryKey] = new List<TSecondaryKey>(1);
						}
						this.primaryKeyToSecondaryKeys[primaryKey].Add(secondaryKey);
						bool flag;
						if (this.cache.primaryKeyToData.ContainsKey(primaryKey))
						{
							this.cache.evictionPolicy.KeyAccess(primaryKey);
							flag = false;
						}
						else
						{
							this.cache.evictionPolicy.Insert(primaryKey);
							flag = true;
						}
						this.cache.primaryKeyToData[primaryKey] = value;
						if (this.cache.performanceCounters != null && flag)
						{
							this.cache.performanceCounters.CacheExpirationQueueLength.RawValue = (long)this.cache.evictionPolicy.CountOfKeysToCleanup;
							this.cache.performanceCounters.CacheInserts.Increment();
							this.cache.performanceCounters.CacheSize.RawValue = (long)this.cache.primaryKeyToData.Count;
						}
					}
					criticalConsistencyBlock.Success();
				}
			}

			// Token: 0x060008B7 RID: 2231 RVA: 0x0001A2D0 File Offset: 0x000184D0
			public TCached Find(TSecondaryKey secondaryKey)
			{
				TCached result;
				using (TypedMultiKeyCache<TCached, TPrimaryKey>._CriticalConsistencyBlock criticalConsistencyBlock = this.cache.Critical())
				{
					this.cache.EvictionCheckpoint();
					if (this.cache.performanceCounters != null)
					{
						this.cache.performanceCounters.CacheLookups.Increment();
					}
					TCached tcached = default(TCached);
					TPrimaryKey key;
					if (this.secondaryKeyToPrimaryKey.TryGetValue(secondaryKey, out key))
					{
						this.cache.evictionPolicy.KeyAccess(key);
						if (this.cache.performanceCounters != null)
						{
							this.cache.performanceCounters.CacheExpirationQueueLength.RawValue = (long)this.cache.evictionPolicy.CountOfKeysToCleanup;
						}
						if (!this.cache.evictionPolicy.ContainsKeyToCleanup(key))
						{
							tcached = this.cache.primaryKeyToData[key];
							if (this.cache.performanceCounters != null)
							{
								this.cache.performanceCounters.CacheHits.Increment();
							}
						}
						else if (this.cache.performanceCounters != null)
						{
							this.cache.performanceCounters.CacheMisses.Increment();
						}
					}
					else if (this.cache.performanceCounters != null)
					{
						this.cache.performanceCounters.CacheMisses.Increment();
					}
					criticalConsistencyBlock.Success();
					result = tcached;
				}
				return result;
			}

			// Token: 0x060008B8 RID: 2232 RVA: 0x0001A440 File Offset: 0x00018640
			public void Remove(TSecondaryKey secondaryKey)
			{
				using (TypedMultiKeyCache<TCached, TPrimaryKey>._CriticalConsistencyBlock criticalConsistencyBlock = this.cache.Critical())
				{
					this.cache.EvictionCheckpoint();
					TPrimaryKey primaryKey;
					if (this.secondaryKeyToPrimaryKey.TryGetValue(secondaryKey, out primaryKey))
					{
						this.cache.Remove(primaryKey);
					}
					criticalConsistencyBlock.Success();
				}
			}

			// Token: 0x04000737 RID: 1847
			private Dictionary<TSecondaryKey, TPrimaryKey> secondaryKeyToPrimaryKey;

			// Token: 0x04000738 RID: 1848
			private Dictionary<TPrimaryKey, List<TSecondaryKey>> primaryKeyToSecondaryKeys;

			// Token: 0x04000739 RID: 1849
			private TypedMultiKeyCache<TCached, TPrimaryKey> cache;
		}

		// Token: 0x020000BF RID: 191
		protected struct _CriticalConsistencyBlock : IDisposable
		{
			// Token: 0x060008B9 RID: 2233 RVA: 0x0001A4A8 File Offset: 0x000186A8
			public void Dispose()
			{
				if (this.Cache != null)
				{
					this.Cache.Reset();
				}
			}

			// Token: 0x060008BA RID: 2234 RVA: 0x0001A4BD File Offset: 0x000186BD
			public void Success()
			{
				this.Cache = null;
			}

			// Token: 0x0400073A RID: 1850
			public TypedMultiKeyCache<TCached, TPrimaryKey> Cache;
		}
	}
}
