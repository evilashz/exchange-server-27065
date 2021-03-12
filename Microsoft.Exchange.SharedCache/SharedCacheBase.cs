using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.SharedCache;
using Microsoft.Exchange.Rpc.SharedCache;
using Microsoft.Exchange.SharedCache.Exceptions;

namespace Microsoft.Exchange.SharedCache
{
	// Token: 0x02000008 RID: 8
	internal abstract class SharedCacheBase : ISharedCache, IDisposable
	{
		// Token: 0x06000016 RID: 22 RVA: 0x000026A0 File Offset: 0x000008A0
		public SharedCacheBase(CacheSettings cacheSettings)
		{
			ArgumentValidator.ThrowIfNull("cacheSettings", cacheSettings);
			if (cacheSettings.Size < 1)
			{
				throw new ArgumentException("settings.Size cannot be less than 1.");
			}
			this.perfCounters = new PerfCounters(cacheSettings);
			this.settings = cacheSettings;
			ExTraceGlobals.CacheTracer.TraceDebug((long)this.GetHashCode(), "New SharedTimeoutCache created: " + this.Name);
			ExTraceGlobals.CacheTracer.TraceDebug((long)this.GetHashCode(), "Settings: " + cacheSettings.ToString());
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002727 File Offset: 0x00000927
		public string Name
		{
			get
			{
				return this.settings.Name;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000018 RID: 24
		public abstract long Count { get; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002734 File Offset: 0x00000934
		public PerfCounters PerformanceCounters
		{
			get
			{
				return this.perfCounters;
			}
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600001A RID: 26 RVA: 0x0000273C File Offset: 0x0000093C
		public CacheSettings Settings
		{
			get
			{
				return this.settings;
			}
		}

		// Token: 0x0600001B RID: 27 RVA: 0x00002744 File Offset: 0x00000944
		public CacheResponse Get(string key)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("key", key);
			this.BeforeProcessRequest();
			CacheEntry cacheEntry = null;
			try
			{
				cacheEntry = this.InternalGet(key);
			}
			catch (CorruptCacheEntryException ex)
			{
				this.PerformanceCounters.IncrementCorruptEntries();
				ex.Key = key;
				this.InternalDelete(key);
				throw;
			}
			CacheResponse cacheResponse;
			if (cacheEntry != null)
			{
				cacheResponse = CacheResponse.Create(ResponseCode.OK, cacheEntry.Value);
			}
			else
			{
				cacheResponse = CacheResponse.Create(ResponseCode.KeyNotFound);
			}
			this.perfCounters.UpdateCacheHitRate(cacheResponse.ResponseCode == ResponseCode.OK);
			return cacheResponse;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000027CC File Offset: 0x000009CC
		public CacheResponse Insert(string key, byte[] value, DateTime valueValidAsOf)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("key", key);
			ArgumentValidator.ThrowIfNull("value", value);
			ArgumentValidator.ThrowIfNull("valueValidAsOf", valueValidAsOf);
			this.BeforeProcessRequest();
			bool flag = true;
			CacheEntry cacheEntry = null;
			DateTime dateTime = DateTime.UtcNow + TimeSpan.FromMinutes(2.0);
			if (valueValidAsOf > dateTime)
			{
				throw new ValueTimestampTooFarIntoFutureException(key, valueValidAsOf, dateTime);
			}
			try
			{
				cacheEntry = this.InternalGet(key);
			}
			catch (CorruptCacheEntryException)
			{
				this.PerformanceCounters.IncrementCorruptEntries();
				this.InternalDelete(key);
			}
			if (cacheEntry != null)
			{
				string text = string.Empty;
				DateTime updatedAt = cacheEntry.UpdatedAt;
				if (cacheEntry.UpdatedAt < valueValidAsOf)
				{
					text = "Found existing value and incoming value is newer.";
					cacheEntry.UpdatedAt = valueValidAsOf;
					cacheEntry.Value = value;
				}
				else
				{
					text = "Found existing value but incoming value is older. No-Op.";
					flag = false;
				}
				ExTraceGlobals.CacheTracer.TraceDebug((long)this.GetHashCode(), "[{0}::Insert({1})] {2} ExistingEntryTime={3}, NewValueTime={4}", new object[]
				{
					this.Name,
					key,
					text,
					updatedAt.ToString("s", CultureInfo.InvariantCulture),
					valueValidAsOf.ToString("s", CultureInfo.InvariantCulture)
				});
			}
			else
			{
				ExTraceGlobals.CacheTracer.TraceDebug<string, string, string>((long)this.GetHashCode(), "[{0}::Insert({1})] Creating new cache entry. EntryTime={2}", this.Name, key, valueValidAsOf.ToString("s", CultureInfo.InvariantCulture));
				cacheEntry = new CacheEntry
				{
					Value = value,
					UpdatedAt = valueValidAsOf,
					CreatedAt = valueValidAsOf
				};
			}
			if (flag)
			{
				this.InternalInsert(key, cacheEntry);
				this.perfCounters.UpdateCacheSize(this.Count);
				return CacheResponse.Create(ResponseCode.OK);
			}
			return CacheResponse.Create(ResponseCode.NoOp);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002980 File Offset: 0x00000B80
		public CacheResponse Delete(string key)
		{
			ArgumentValidator.ThrowIfNullOrEmpty("key", key);
			this.BeforeProcessRequest();
			CacheEntry cacheEntry = this.InternalDelete(key);
			CacheResponse result;
			if (cacheEntry != null)
			{
				result = CacheResponse.Create(ResponseCode.OK, cacheEntry.Value);
			}
			else
			{
				result = CacheResponse.Create(ResponseCode.KeyNotFound);
			}
			this.perfCounters.UpdateCacheSize(this.Count);
			return result;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x000029D1 File Offset: 0x00000BD1
		public virtual void Dispose()
		{
			ExTraceGlobals.CacheTracer.TraceDebug((long)this.GetHashCode(), this.Name + " base class is disposing.");
			if (this.perfCounters != null)
			{
				this.perfCounters.Dispose();
				this.perfCounters = null;
			}
		}

		// Token: 0x0600001F RID: 31
		internal abstract KeyValuePair<string, CacheEntry>[] InternalSearch(string keyWildcard);

		// Token: 0x06000020 RID: 32
		internal abstract CacheEntry InternalGet(string key);

		// Token: 0x06000021 RID: 33
		internal abstract void InternalInsert(string key, CacheEntry cacheEntryToInsert);

		// Token: 0x06000022 RID: 34
		internal abstract CacheEntry InternalDelete(string key);

		// Token: 0x06000023 RID: 35 RVA: 0x00002A0E File Offset: 0x00000C0E
		protected virtual void BeforeProcessRequest()
		{
			this.perfCounters.IncrementTotalRequests();
		}

		// Token: 0x04000019 RID: 25
		private CacheSettings settings;

		// Token: 0x0400001A RID: 26
		private PerfCounters perfCounters;
	}
}
