using System;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x0200000F RID: 15
	public class LRU2ExpirationPolicy<TKey> : EvictionPolicy<TKey>
	{
		// Token: 0x06000209 RID: 521 RVA: 0x000049D2 File Offset: 0x00002BD2
		public LRU2ExpirationPolicy(int capacity) : base(capacity)
		{
			this.longTermQueue = new KeyQueue<TKey>(capacity * 3 / 4);
			this.shortTermQueue = new KeyQueue<TKey>(capacity / 4);
			this.memoryQueue = new KeyQueue<TKey>(capacity / 2);
		}

		// Token: 0x17000023 RID: 35
		// (get) Token: 0x0600020A RID: 522 RVA: 0x00004A07 File Offset: 0x00002C07
		public override int Count
		{
			get
			{
				return this.longTermQueue.Count + this.shortTermQueue.Count + base.Count;
			}
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x0600020B RID: 523 RVA: 0x00004A27 File Offset: 0x00002C27
		internal KeyQueue<TKey> LongTermQueueForTest
		{
			get
			{
				return this.longTermQueue;
			}
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x0600020C RID: 524 RVA: 0x00004A2F File Offset: 0x00002C2F
		internal KeyQueue<TKey> ShortTermQueueForTest
		{
			get
			{
				return this.shortTermQueue;
			}
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x0600020D RID: 525 RVA: 0x00004A37 File Offset: 0x00002C37
		internal KeyQueue<TKey> MemoryQueueForTest
		{
			get
			{
				return this.memoryQueue;
			}
		}

		// Token: 0x0600020E RID: 526 RVA: 0x00004A3F File Offset: 0x00002C3F
		public override void EvictionCheckpoint()
		{
		}

		// Token: 0x0600020F RID: 527 RVA: 0x00004A44 File Offset: 0x00002C44
		public override void Insert(TKey key)
		{
			if (this.Contains(key))
			{
				return;
			}
			if (this.memoryQueue.Contains(key))
			{
				if (this.longTermQueue.Count == this.longTermQueue.Capacity)
				{
					TKey tailKey = this.longTermQueue.TailKey;
					this.longTermQueue.RemoveTail();
					base.AddKeyToCleanup(tailKey);
					this.OnEnqueuedKeyForEviction(tailKey);
				}
				this.longTermQueue.AddHead(key);
				return;
			}
			if (this.shortTermQueue.Count == this.shortTermQueue.Capacity)
			{
				TKey tailKey2 = this.shortTermQueue.TailKey;
				this.shortTermQueue.RemoveTail();
				this.InsertToMemoryQueue(tailKey2);
				base.AddKeyToCleanup(tailKey2);
				this.OnEnqueuedKeyForEviction(tailKey2);
			}
			this.shortTermQueue.AddHead(key);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x00004B03 File Offset: 0x00002D03
		public override void Remove(TKey key)
		{
			if (this.longTermQueue.Contains(key))
			{
				this.longTermQueue.Remove(key);
				return;
			}
			if (this.shortTermQueue.Contains(key))
			{
				this.shortTermQueue.Remove(key);
				return;
			}
			base.RemoveKeyToCleanup(key);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00004B42 File Offset: 0x00002D42
		public override void KeyAccess(TKey key)
		{
			if (this.longTermQueue.Contains(key))
			{
				this.longTermQueue.MoveToHead(key);
				return;
			}
			if (this.shortTermQueue.Contains(key))
			{
				return;
			}
			base.RemoveKeyToCleanup(key);
			this.Insert(key);
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00004B7C File Offset: 0x00002D7C
		public override bool Contains(TKey key)
		{
			return this.longTermQueue.Contains(key) || this.shortTermQueue.Contains(key) || base.Contains(key);
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00004BA3 File Offset: 0x00002DA3
		public override void Reset()
		{
			this.longTermQueue.Reset();
			this.shortTermQueue.Reset();
			this.memoryQueue.Reset();
			base.Reset();
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00004BCC File Offset: 0x00002DCC
		protected virtual void OnEnqueuedKeyForEviction(TKey key)
		{
		}

		// Token: 0x06000215 RID: 533 RVA: 0x00004BD0 File Offset: 0x00002DD0
		protected void ForceEvictKey(TKey key)
		{
			if (this.shortTermQueue.Contains(key))
			{
				this.shortTermQueue.Remove(key);
			}
			else if (this.longTermQueue.Contains(key))
			{
				this.longTermQueue.Remove(key);
			}
			base.AddKeyToCleanup(key);
			this.OnEnqueuedKeyForEviction(key);
		}

		// Token: 0x06000216 RID: 534 RVA: 0x00004C21 File Offset: 0x00002E21
		private void InsertToMemoryQueue(TKey lastKey)
		{
			if (this.memoryQueue.Count >= this.memoryQueue.Capacity)
			{
				this.memoryQueue.RemoveTail();
			}
			this.memoryQueue.AddHead(lastKey);
		}

		// Token: 0x040002D3 RID: 723
		private KeyQueue<TKey> longTermQueue;

		// Token: 0x040002D4 RID: 724
		private KeyQueue<TKey> shortTermQueue;

		// Token: 0x040002D5 RID: 725
		private KeyQueue<TKey> memoryQueue;
	}
}
