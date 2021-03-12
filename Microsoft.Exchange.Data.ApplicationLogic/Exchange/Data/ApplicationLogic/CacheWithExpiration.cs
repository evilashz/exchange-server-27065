using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.ProvisioningAgent;

namespace Microsoft.Exchange.Data.ApplicationLogic
{
	// Token: 0x0200009B RID: 155
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class CacheWithExpiration<TKey, TValue> where TValue : ILifetimeTrackable
	{
		// Token: 0x060006AF RID: 1711 RVA: 0x00018292 File Offset: 0x00016492
		public CacheWithExpiration(int maximumCacheSize, TimeSpan entryLifeTime, Action<TValue> cleanupDelegate)
		{
			this.cacheSize = maximumCacheSize;
			this.entryLifeTime = entryLifeTime;
			this.cleanupDelegate = cleanupDelegate;
			this.cache = new Dictionary<TKey, TValue>();
			this.cacheMutex = new object();
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x000182C8 File Offset: 0x000164C8
		public bool TryGetValue(TKey key, DateTime currentTime, out TValue cachedValue)
		{
			bool result;
			lock (this.cacheMutex)
			{
				result = this.TryGetValueUnsafe(key, currentTime, out cachedValue);
			}
			return result;
		}

		// Token: 0x060006B1 RID: 1713 RVA: 0x00018310 File Offset: 0x00016510
		public void Add(TKey key, DateTime currentTime, TValue newValue)
		{
			lock (this.cacheMutex)
			{
				this.AddUnsafe(key, currentTime, newValue);
			}
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x00018354 File Offset: 0x00016554
		public bool TryAdd(TKey key, DateTime currentTime, TValue newValue)
		{
			bool result;
			lock (this.cacheMutex)
			{
				TValue tvalue;
				bool flag2 = this.TryGetValueUnsafe(key, currentTime, out tvalue);
				if (flag2)
				{
					result = false;
				}
				else
				{
					this.AddUnsafe(key, currentTime, newValue);
					result = true;
				}
			}
			return result;
		}

		// Token: 0x060006B3 RID: 1715 RVA: 0x000183B0 File Offset: 0x000165B0
		public bool Set(TKey key, DateTime currentTime, TValue newValue)
		{
			bool result;
			lock (this.cacheMutex)
			{
				bool flag2 = this.cache.Remove(key);
				this.AddUnsafe(key, currentTime, newValue);
				result = flag2;
			}
			return result;
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x00018404 File Offset: 0x00016604
		public bool Remove(TKey key)
		{
			bool result;
			lock (this.cacheMutex)
			{
				bool flag2 = this.cache.Remove(key);
				result = flag2;
			}
			return result;
		}

		// Token: 0x060006B5 RID: 1717 RVA: 0x00018450 File Offset: 0x00016650
		private bool TryGetValueUnsafe(TKey key, DateTime currentTime, out TValue cachedValue)
		{
			bool result = false;
			cachedValue = default(TValue);
			TValue tvalue;
			if (this.cache.TryGetValue(key, out tvalue))
			{
				if (tvalue.CreateTime > currentTime || currentTime.Subtract(tvalue.CreateTime) > this.entryLifeTime)
				{
					this.cache.Remove(key);
					if (this.cleanupDelegate != null)
					{
						this.cleanupDelegate(tvalue);
					}
					if (ExTraceGlobals.AdminAuditLogTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.AdminAuditLogTracer.TraceDebug<TKey, DateTime, DateTime>((long)this.GetHashCode(), "Entry for key '{0}' was found in the cache, but was evicted by time. Create time=[{1}], check time=[{2}]", key, tvalue.CreateTime, currentTime);
					}
				}
				else
				{
					tvalue.LastAccessTime = currentTime;
					cachedValue = tvalue;
					result = true;
					if (ExTraceGlobals.AdminAuditLogTracer.IsTraceEnabled(TraceType.DebugTrace))
					{
						ExTraceGlobals.AdminAuditLogTracer.TraceDebug<TKey>((long)this.GetHashCode(), "Entry for key '{0}' was found in the cache.", key);
					}
				}
			}
			else if (ExTraceGlobals.AdminAuditLogTracer.IsTraceEnabled(TraceType.DebugTrace))
			{
				ExTraceGlobals.AdminAuditLogTracer.TraceDebug<TKey>((long)this.GetHashCode(), "Entry for key '{0}' was not found in the cache", key);
			}
			return result;
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x00018590 File Offset: 0x00016790
		private void AddUnsafe(TKey key, DateTime currentTime, TValue newValue)
		{
			if (this.cache.Count == this.cacheSize)
			{
				TKey key2 = this.cache.OrderBy(delegate(KeyValuePair<TKey, TValue> pair)
				{
					TValue value = pair.Value;
					return value.LastAccessTime;
				}).First<KeyValuePair<TKey, TValue>>().Key;
				TValue obj = this.cache[key2];
				this.cache.Remove(key2);
				if (this.cleanupDelegate != null)
				{
					this.cleanupDelegate(obj);
				}
				if (ExTraceGlobals.AdminAuditLogTracer.IsTraceEnabled(TraceType.DebugTrace))
				{
					ExTraceGlobals.AdminAuditLogTracer.TraceDebug<TKey>((long)this.GetHashCode(), "Entry for key '{0}' was evicted from the cache by size.", key);
				}
			}
			this.cache.Add(key, newValue);
			newValue.LastAccessTime = currentTime;
		}

		// Token: 0x040002F5 RID: 757
		private readonly int cacheSize;

		// Token: 0x040002F6 RID: 758
		private readonly TimeSpan entryLifeTime;

		// Token: 0x040002F7 RID: 759
		private readonly Action<TValue> cleanupDelegate;

		// Token: 0x040002F8 RID: 760
		private readonly Dictionary<TKey, TValue> cache;

		// Token: 0x040002F9 RID: 761
		private readonly object cacheMutex;
	}
}
