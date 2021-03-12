using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x0200000C RID: 12
	internal struct KeyTimeQueue<TKey>
	{
		// Token: 0x060001E5 RID: 485 RVA: 0x000045A3 File Offset: 0x000027A3
		public KeyTimeQueue(int initialCapacity)
		{
			this.keyList = new LinkedList<KeyValuePair<TKey, DateTime>>();
			this.keyDictionary = new Dictionary<TKey, LinkedListNode<KeyValuePair<TKey, DateTime>>>(initialCapacity);
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x060001E6 RID: 486 RVA: 0x000045BC File Offset: 0x000027BC
		public int Count
		{
			get
			{
				return this.keyDictionary.Count;
			}
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060001E7 RID: 487 RVA: 0x000045CC File Offset: 0x000027CC
		public TKey TailKey
		{
			get
			{
				return this.keyList.Last.Value.Key;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060001E8 RID: 488 RVA: 0x000045F4 File Offset: 0x000027F4
		public DateTime TailKeyTime
		{
			get
			{
				return this.keyList.Last.Value.Value;
			}
		}

		// Token: 0x060001E9 RID: 489 RVA: 0x00004619 File Offset: 0x00002819
		public bool Contains(TKey key)
		{
			return this.keyDictionary.ContainsKey(key);
		}

		// Token: 0x060001EA RID: 490 RVA: 0x00004627 File Offset: 0x00002827
		public void AddHead(TKey key)
		{
			this.AddHeadWithDateTime(key, DateTime.UtcNow);
		}

		// Token: 0x060001EB RID: 491 RVA: 0x00004638 File Offset: 0x00002838
		public void Remove(TKey key)
		{
			LinkedListNode<KeyValuePair<TKey, DateTime>> node;
			if (this.keyDictionary.TryGetValue(key, out node))
			{
				this.keyList.Remove(node);
				this.keyDictionary.Remove(key);
			}
		}

		// Token: 0x060001EC RID: 492 RVA: 0x00004670 File Offset: 0x00002870
		public void RemoveTail()
		{
			TKey tailKey = this.TailKey;
			this.keyDictionary.Remove(tailKey);
			this.keyList.RemoveLast();
		}

		// Token: 0x060001ED RID: 493 RVA: 0x0000469C File Offset: 0x0000289C
		public void BumpToHead(TKey key)
		{
			this.BumpToHeadWithDateTime(key, DateTime.UtcNow);
		}

		// Token: 0x060001EE RID: 494 RVA: 0x000046AA File Offset: 0x000028AA
		public void Reset()
		{
			this.keyList.Clear();
			this.keyDictionary.Clear();
		}

		// Token: 0x060001EF RID: 495 RVA: 0x000046C4 File Offset: 0x000028C4
		internal void BumpToHeadWithDateTime(TKey key, DateTime newDateTime)
		{
			if (this.keyList.First.Value.Value > newDateTime)
			{
				newDateTime = this.keyList.First.Value.Value;
			}
			LinkedListNode<KeyValuePair<TKey, DateTime>> linkedListNode;
			if (this.keyDictionary.TryGetValue(key, out linkedListNode))
			{
				linkedListNode.Value = new KeyValuePair<TKey, DateTime>(key, newDateTime);
				if (linkedListNode != this.keyList.First)
				{
					this.keyList.Remove(linkedListNode);
					this.keyList.AddFirst(linkedListNode);
				}
			}
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x00004750 File Offset: 0x00002950
		internal void AddHeadWithDateTime(TKey key, DateTime newDateTime)
		{
			if (this.Count != 0 && this.keyList.First.Value.Value > newDateTime)
			{
				newDateTime = this.keyList.First.Value.Value;
			}
			this.keyList.AddFirst(new KeyValuePair<TKey, DateTime>(key, newDateTime));
			this.keyDictionary[key] = this.keyList.First;
		}

		// Token: 0x040002CC RID: 716
		private readonly LinkedList<KeyValuePair<TKey, DateTime>> keyList;

		// Token: 0x040002CD RID: 717
		private Dictionary<TKey, LinkedListNode<KeyValuePair<TKey, DateTime>>> keyDictionary;
	}
}
