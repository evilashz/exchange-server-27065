using System;
using System.Collections.Generic;

namespace Microsoft.Exchange.Server.Storage.Common
{
	// Token: 0x0200000B RID: 11
	internal struct KeyQueue<TKey>
	{
		// Token: 0x060001DA RID: 474 RVA: 0x00004446 File Offset: 0x00002646
		public KeyQueue(int capacity)
		{
			this.capacity = capacity;
			this.hashData = new Dictionary<TKey, LinkedListNode<TKey>>(capacity);
			this.listData = new LinkedList<TKey>();
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00004466 File Offset: 0x00002666
		public int Capacity
		{
			get
			{
				return this.capacity;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x060001DC RID: 476 RVA: 0x0000446E File Offset: 0x0000266E
		public int Count
		{
			get
			{
				return this.hashData.Count;
			}
		}

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x060001DD RID: 477 RVA: 0x0000447B File Offset: 0x0000267B
		public TKey TailKey
		{
			get
			{
				return this.listData.Last.Value;
			}
		}

		// Token: 0x060001DE RID: 478 RVA: 0x0000448D File Offset: 0x0000268D
		public void AddTail(TKey key)
		{
			this.listData.AddLast(key);
			this.hashData[key] = this.listData.Last;
		}

		// Token: 0x060001DF RID: 479 RVA: 0x000044B3 File Offset: 0x000026B3
		public void AddHead(TKey key)
		{
			this.listData.AddFirst(key);
			this.hashData[key] = this.listData.First;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x000044D9 File Offset: 0x000026D9
		public bool Contains(TKey key)
		{
			return this.hashData.ContainsKey(key);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x000044E8 File Offset: 0x000026E8
		public void Remove(TKey key)
		{
			LinkedListNode<TKey> node;
			if (this.hashData.TryGetValue(key, out node))
			{
				this.listData.Remove(node);
				this.hashData.Remove(key);
			}
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x0000451E File Offset: 0x0000271E
		public void RemoveTail()
		{
			this.hashData.Remove(this.listData.Last.Value);
			this.listData.RemoveLast();
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00004548 File Offset: 0x00002748
		public void MoveToHead(TKey key)
		{
			LinkedListNode<TKey> linkedListNode;
			if (this.hashData.TryGetValue(key, out linkedListNode) && linkedListNode != this.listData.First)
			{
				this.listData.Remove(linkedListNode);
				this.listData.AddFirst(linkedListNode);
			}
		}

		// Token: 0x060001E4 RID: 484 RVA: 0x0000458B File Offset: 0x0000278B
		public void Reset()
		{
			this.hashData.Clear();
			this.listData.Clear();
		}

		// Token: 0x040002C9 RID: 713
		private readonly int capacity;

		// Token: 0x040002CA RID: 714
		private Dictionary<TKey, LinkedListNode<TKey>> hashData;

		// Token: 0x040002CB RID: 715
		private LinkedList<TKey> listData;
	}
}
