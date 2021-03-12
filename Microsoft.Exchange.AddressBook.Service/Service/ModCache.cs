using System;
using System.Collections.Generic;
using Microsoft.Exchange.AddressBook.Nspi;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.AddressBook.Service;
using Microsoft.Mapi;

namespace Microsoft.Exchange.AddressBook.Service
{
	// Token: 0x0200002B RID: 43
	internal class ModCache
	{
		// Token: 0x06000165 RID: 357 RVA: 0x00006E00 File Offset: 0x00005000
		internal ModCache(NspiContext nspiContext, int expiryTimeInSeconds)
		{
			this.nspiContext = nspiContext;
			this.logExpiry = TimeSpan.FromSeconds((double)expiryTimeInSeconds);
			this.cache = new Dictionary<ModCache.LogKey, ModCache.Log>();
		}

		// Token: 0x06000166 RID: 358 RVA: 0x00006E28 File Offset: 0x00005028
		internal void RecordMods(int mid, PropTag propTag, IList<int> mids, bool isDeletion)
		{
			ModCache.ModCacheTracer.TraceDebug((long)this.nspiContext.ContextHandle, "ModCache.Log: mid='{0}', propTag={1}, mids.Count={2}, isDeletion={3}", new object[]
			{
				mid,
				propTag,
				mids.Count,
				isDeletion
			});
			ModCache.LogKey key = new ModCache.LogKey(mid, propTag);
			ModCache.Log log;
			if (!this.cache.TryGetValue(key, out log))
			{
				log = new ModCache.Log(mids.Count);
				this.cache.Add(key, log);
			}
			foreach (int key2 in mids)
			{
				log.Mods[key2] = isDeletion;
			}
		}

		// Token: 0x06000167 RID: 359 RVA: 0x00006EFC File Offset: 0x000050FC
		internal bool HasMods(int mid, PropTag propTag)
		{
			return this.cache.ContainsKey(new ModCache.LogKey(mid, propTag));
		}

		// Token: 0x06000168 RID: 360 RVA: 0x00006F10 File Offset: 0x00005110
		internal void PurgeMods(int mid, PropTag propTag)
		{
			this.cache.Remove(new ModCache.LogKey(mid, propTag));
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00006F28 File Offset: 0x00005128
		internal QueryFilter ApplyMods(QueryFilter filter, int mid, PropTag propTag)
		{
			ModCache.ModCacheTracer.TraceDebug<int, PropTag>((long)this.nspiContext.ContextHandle, "ModCache.ApplyMods: mid='{0}', propTag={1}", mid, propTag);
			QueryFilter queryFilter = filter;
			ModCache.Log log;
			if (!this.cache.TryGetValue(new ModCache.LogKey(mid, propTag), out log))
			{
				ModCache.ModCacheTracer.TraceDebug((long)this.nspiContext.ContextHandle, "Mod log not available");
				return queryFilter;
			}
			if (log.Mods.Count == 0)
			{
				ModCache.ModCacheTracer.TraceDebug((long)this.nspiContext.ContextHandle, "Mod log not available");
				return queryFilter;
			}
			List<QueryFilter> list = null;
			List<QueryFilter> list2 = null;
			foreach (KeyValuePair<int, bool> keyValuePair in log.Mods)
			{
				Guid guid;
				EphemeralIdTable.NamingContext namingContext;
				if (!this.nspiContext.EphemeralIdTable.GetGuid(keyValuePair.Key, out guid, out namingContext))
				{
					throw new InvalidOperationException("Mid in modcache without a guid");
				}
				if (!keyValuePair.Value)
				{
					if (list == null)
					{
						list = new List<QueryFilter>(1);
					}
					list.Add(new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Guid, guid));
				}
				else
				{
					if (list2 == null)
					{
						list2 = new List<QueryFilter>(1);
					}
					list2.Add(new ComparisonFilter(ComparisonOperator.Equal, ADObjectSchema.Guid, guid));
				}
			}
			if (list != null)
			{
				ModCache.ModCacheTracer.TraceDebug<int>((long)this.nspiContext.ContextHandle, "Applying {0} cached SET mod(s)", list.Count);
				list.Add(queryFilter);
				queryFilter = new OrFilter(list.ToArray());
			}
			if (list2 != null)
			{
				ModCache.ModCacheTracer.TraceDebug<int>((long)this.nspiContext.ContextHandle, "Applying {0} cached DELETE mod(s)", list2.Count);
				queryFilter = new AndFilter(new QueryFilter[]
				{
					queryFilter,
					new NotFilter(new OrFilter(list2.ToArray()))
				});
			}
			return queryFilter;
		}

		// Token: 0x0600016A RID: 362 RVA: 0x000070F8 File Offset: 0x000052F8
		internal bool PurgeExpiredLogs()
		{
			List<ModCache.LogKey> list = null;
			int num = 0;
			int num2 = 0;
			foreach (KeyValuePair<ModCache.LogKey, ModCache.Log> keyValuePair in this.cache)
			{
				ModCache.LogKey key = keyValuePair.Key;
				ModCache.Log value = keyValuePair.Value;
				num += value.Mods.Count;
				if (value.LastUpdate + this.logExpiry < DateTime.UtcNow)
				{
					if (list == null)
					{
						list = new List<ModCache.LogKey>(1);
					}
					list.Add(key);
					num2 += value.Mods.Count;
				}
			}
			if (list != null)
			{
				foreach (ModCache.LogKey key2 in list)
				{
					this.cache.Remove(key2);
				}
				ModCache.ModCacheTracer.TraceDebug<int, int, int>((long)this.nspiContext.ContextHandle, "ModCache.PurgeExpiredLogs: Purged {0} expired logs, total: {1}, removed: {2}", list.Count, num, num2);
			}
			return num == num2;
		}

		// Token: 0x040000F0 RID: 240
		private static readonly Trace ModCacheTracer = ExTraceGlobals.ModCacheTracer;

		// Token: 0x040000F1 RID: 241
		private readonly TimeSpan logExpiry;

		// Token: 0x040000F2 RID: 242
		private Dictionary<ModCache.LogKey, ModCache.Log> cache;

		// Token: 0x040000F3 RID: 243
		private NspiContext nspiContext;

		// Token: 0x0200002C RID: 44
		private class LogKey
		{
			// Token: 0x0600016C RID: 364 RVA: 0x00007228 File Offset: 0x00005428
			internal LogKey(int mid, PropTag propTag)
			{
				this.mid = mid;
				this.propTag = propTag;
			}

			// Token: 0x0600016D RID: 365 RVA: 0x0000723E File Offset: 0x0000543E
			public override int GetHashCode()
			{
				return this.mid ^ (int)this.propTag;
			}

			// Token: 0x0600016E RID: 366 RVA: 0x00007250 File Offset: 0x00005450
			public override bool Equals(object other)
			{
				ModCache.LogKey logKey = other as ModCache.LogKey;
				return logKey != null && logKey.propTag == this.propTag && logKey.mid == this.mid;
			}

			// Token: 0x040000F4 RID: 244
			private int mid;

			// Token: 0x040000F5 RID: 245
			private PropTag propTag;
		}

		// Token: 0x0200002D RID: 45
		private class Log
		{
			// Token: 0x0600016F RID: 367 RVA: 0x00007287 File Offset: 0x00005487
			internal Log(int capacity)
			{
				this.mods = new Dictionary<int, bool>(capacity);
				this.lastUpdate = DateTime.UtcNow;
			}

			// Token: 0x17000052 RID: 82
			// (get) Token: 0x06000170 RID: 368 RVA: 0x000072A6 File Offset: 0x000054A6
			internal Dictionary<int, bool> Mods
			{
				get
				{
					return this.mods;
				}
			}

			// Token: 0x17000053 RID: 83
			// (get) Token: 0x06000171 RID: 369 RVA: 0x000072AE File Offset: 0x000054AE
			internal DateTime LastUpdate
			{
				get
				{
					return this.lastUpdate;
				}
			}

			// Token: 0x040000F6 RID: 246
			private Dictionary<int, bool> mods;

			// Token: 0x040000F7 RID: 247
			private DateTime lastUpdate;
		}
	}
}
