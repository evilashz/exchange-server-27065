using System;
using System.Runtime.Caching;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Protocol;

namespace Microsoft.Exchange.Compliance.TaskDistributionFabric.Routing
{
	// Token: 0x0200001C RID: 28
	internal abstract class Entry
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000073 RID: 115 RVA: 0x00004534 File Offset: 0x00002734
		// (set) Token: 0x06000074 RID: 116 RVA: 0x0000453C File Offset: 0x0000273C
		public int RetryCount { get; set; }

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00004545 File Offset: 0x00002745
		// (set) Token: 0x06000076 RID: 118 RVA: 0x0000454D File Offset: 0x0000274D
		public Guid CorrelationId { get; set; }

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000077 RID: 119 RVA: 0x00004556 File Offset: 0x00002756
		// (set) Token: 0x06000078 RID: 120 RVA: 0x0000455E File Offset: 0x0000275E
		public string MessageId { get; set; }

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000079 RID: 121 RVA: 0x00004567 File Offset: 0x00002767
		// (set) Token: 0x0600007A RID: 122 RVA: 0x0000456F File Offset: 0x0000276F
		public ComplianceMessage Message { get; set; }

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x0600007B RID: 123 RVA: 0x00004578 File Offset: 0x00002778
		// (set) Token: 0x0600007C RID: 124 RVA: 0x00004580 File Offset: 0x00002780
		protected TimeSpan ExpiryTime
		{
			get
			{
				return this.expiryTime;
			}
			set
			{
				this.expiryTime = value;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600007D RID: 125 RVA: 0x00004589 File Offset: 0x00002789
		// (set) Token: 0x0600007E RID: 126 RVA: 0x00004591 File Offset: 0x00002791
		protected bool KeepAlive
		{
			get
			{
				return this.keepAlive;
			}
			set
			{
				this.keepAlive = value;
			}
		}

		// Token: 0x0600007F RID: 127
		public abstract string GetKey();

		// Token: 0x06000080 RID: 128 RVA: 0x0000459A File Offset: 0x0000279A
		public virtual CacheItem GetCacheItem()
		{
			if (this.cacheItem == null)
			{
				this.cacheItem = new CacheItem(this.GetKey(), this);
			}
			return this.cacheItem;
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000045BC File Offset: 0x000027BC
		public virtual CacheItemPolicy GetCachePolicy()
		{
			if (this.cachePolicy == null)
			{
				this.cachePolicy = new CacheItemPolicy
				{
					SlidingExpiration = this.ExpiryTime,
					RemovedCallback = new CacheEntryRemovedCallback(this.CacheExpiry)
				};
			}
			return this.cachePolicy;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00004604 File Offset: 0x00002804
		public Entry UpdateCache(ObjectCache cache)
		{
			CacheItem value = this.GetCacheItem();
			CacheItem cacheItem = cache.AddOrGetExisting(value, this.GetCachePolicy());
			if (cacheItem.Value != null)
			{
				Entry entry = cacheItem.Value as Entry;
				this.UpdateExistingEntry(entry);
				return entry;
			}
			return this;
		}

		// Token: 0x06000083 RID: 131
		public abstract void EvaluateState(bool expired);

		// Token: 0x06000084 RID: 132
		protected abstract void UpdateExistingEntry(Entry existing);

		// Token: 0x06000085 RID: 133 RVA: 0x00004644 File Offset: 0x00002844
		protected virtual void CacheExpiry(CacheEntryRemovedArguments e)
		{
			if (e.RemovedReason == CacheEntryRemovedReason.Expired)
			{
				this.RetryCount++;
				this.EvaluateState(true);
				if (!this.KeepAlive)
				{
					return;
				}
			}
			else if (e.RemovedReason == CacheEntryRemovedReason.Removed)
			{
				return;
			}
			this.UpdateCache(e.Source);
		}

		// Token: 0x04000029 RID: 41
		private TimeSpan expiryTime = new TimeSpan(0, 5, 0);

		// Token: 0x0400002A RID: 42
		private bool keepAlive = true;

		// Token: 0x0400002B RID: 43
		private CacheItem cacheItem;

		// Token: 0x0400002C RID: 44
		private CacheItemPolicy cachePolicy;
	}
}
