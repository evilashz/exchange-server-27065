using System;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x0200000E RID: 14
	public class SimpleTimeToLiveExpirationPolicy<TKey> : EvictionPolicy<TKey>
	{
		// Token: 0x060001FF RID: 511 RVA: 0x00004884 File Offset: 0x00002A84
		public SimpleTimeToLiveExpirationPolicy(int initialCapacity, TimeSpan timeToLive, bool ageEviction) : base(initialCapacity)
		{
			this.timeToLive = timeToLive;
			this.ageEviction = ageEviction;
			this.keyTimeQueue = new KeyTimeQueue<TKey>(initialCapacity);
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000200 RID: 512 RVA: 0x000048A7 File Offset: 0x00002AA7
		public override int Count
		{
			get
			{
				return this.keyTimeQueue.Count + base.Count;
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x000048BB File Offset: 0x00002ABB
		public override void EvictionCheckpoint()
		{
			this.EvictionCheckpointAtDateTime(DateTime.UtcNow);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x000048C8 File Offset: 0x00002AC8
		public override void Insert(TKey key)
		{
			this.InsertAtDateTime(key, DateTime.UtcNow);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x000048D6 File Offset: 0x00002AD6
		public override void Remove(TKey key)
		{
			if (this.keyTimeQueue.Contains(key))
			{
				this.keyTimeQueue.Remove(key);
				return;
			}
			base.RemoveKeyToCleanup(key);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x000048FA File Offset: 0x00002AFA
		public override void KeyAccess(TKey key)
		{
			if (this.ageEviction)
			{
				return;
			}
			if (base.ContainsKeyToCleanup(key))
			{
				base.RemoveKeyToCleanup(key);
				this.keyTimeQueue.AddHead(key);
				return;
			}
			this.keyTimeQueue.BumpToHead(key);
		}

		// Token: 0x06000205 RID: 517 RVA: 0x0000492E File Offset: 0x00002B2E
		public override bool Contains(TKey key)
		{
			return this.keyTimeQueue.Contains(key) || base.Contains(key);
		}

		// Token: 0x06000206 RID: 518 RVA: 0x00004947 File Offset: 0x00002B47
		public override void Reset()
		{
			this.keyTimeQueue.Reset();
			base.Reset();
		}

		// Token: 0x06000207 RID: 519 RVA: 0x0000495A File Offset: 0x00002B5A
		internal void InsertAtDateTime(TKey key, DateTime datetimeInsertion)
		{
			if (!this.keyTimeQueue.Contains(key))
			{
				this.keyTimeQueue.AddHeadWithDateTime(key, datetimeInsertion);
			}
		}

		// Token: 0x06000208 RID: 520 RVA: 0x00004978 File Offset: 0x00002B78
		internal void EvictionCheckpointAtDateTime(DateTime datetimeEviction)
		{
			DateTime t = datetimeEviction - this.timeToLive;
			while (this.keyTimeQueue.Count != 0 && this.keyTimeQueue.TailKeyTime < t)
			{
				TKey tailKey = this.keyTimeQueue.TailKey;
				this.keyTimeQueue.RemoveTail();
				base.AddKeyToCleanup(tailKey);
			}
		}

		// Token: 0x040002D0 RID: 720
		private readonly TimeSpan timeToLive;

		// Token: 0x040002D1 RID: 721
		private readonly bool ageEviction;

		// Token: 0x040002D2 RID: 722
		private KeyTimeQueue<TKey> keyTimeQueue;
	}
}
