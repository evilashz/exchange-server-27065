using System;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x02000010 RID: 16
	public class LRU2WithTimeToLiveExpirationPolicy<TKey> : LRU2ExpirationPolicy<TKey>
	{
		// Token: 0x06000217 RID: 535 RVA: 0x00004C52 File Offset: 0x00002E52
		public LRU2WithTimeToLiveExpirationPolicy(int capacity, TimeSpan timeToLive, bool ageEviction) : base(capacity)
		{
			this.timeToLive = timeToLive;
			this.ageEviction = ageEviction;
			this.keyTimeQueue = new KeyTimeQueue<TKey>(capacity);
		}

		// Token: 0x06000218 RID: 536 RVA: 0x00004C78 File Offset: 0x00002E78
		public override void EvictionCheckpoint()
		{
			base.EvictionCheckpoint();
			DateTime t = DateTime.UtcNow - this.timeToLive;
			while (this.keyTimeQueue.Count != 0 && this.keyTimeQueue.TailKeyTime < t)
			{
				base.ForceEvictKey(this.keyTimeQueue.TailKey);
			}
		}

		// Token: 0x06000219 RID: 537 RVA: 0x00004CCF File Offset: 0x00002ECF
		public override void Insert(TKey key)
		{
			base.Insert(key);
			this.keyTimeQueue.AddHead(key);
		}

		// Token: 0x0600021A RID: 538 RVA: 0x00004CE4 File Offset: 0x00002EE4
		public override void Remove(TKey key)
		{
			if (this.keyTimeQueue.Contains(key))
			{
				this.keyTimeQueue.Remove(key);
			}
			base.Remove(key);
		}

		// Token: 0x0600021B RID: 539 RVA: 0x00004D07 File Offset: 0x00002F07
		public override void KeyAccess(TKey key)
		{
			if (this.ageEviction)
			{
				if (!base.ContainsKeyToCleanup(key))
				{
					base.KeyAccess(key);
					return;
				}
			}
			else
			{
				base.KeyAccess(key);
				this.keyTimeQueue.BumpToHead(key);
			}
		}

		// Token: 0x0600021C RID: 540 RVA: 0x00004D35 File Offset: 0x00002F35
		public override void Reset()
		{
			this.keyTimeQueue.Reset();
			base.Reset();
		}

		// Token: 0x0600021D RID: 541 RVA: 0x00004D48 File Offset: 0x00002F48
		protected override void OnEnqueuedKeyForEviction(TKey key)
		{
			this.keyTimeQueue.Remove(key);
			base.OnEnqueuedKeyForEviction(key);
		}

		// Token: 0x040002D6 RID: 726
		private readonly TimeSpan timeToLive;

		// Token: 0x040002D7 RID: 727
		private readonly bool ageEviction;

		// Token: 0x040002D8 RID: 728
		private KeyTimeQueue<TKey> keyTimeQueue;
	}
}
