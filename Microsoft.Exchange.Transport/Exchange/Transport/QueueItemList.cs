using System;
using System.Collections;
using System.Collections.Generic;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200031E RID: 798
	internal class QueueItemList : ICollection<IQueueItem>, IEnumerable<IQueueItem>, IEnumerable
	{
		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x0600228B RID: 8843 RVA: 0x00082B95 File Offset: 0x00080D95
		public bool IsEmpty
		{
			get
			{
				return this.head == null;
			}
		}

		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x0600228C RID: 8844 RVA: 0x00082BA0 File Offset: 0x00080DA0
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x0600228D RID: 8845 RVA: 0x00082BA8 File Offset: 0x00080DA8
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600228E RID: 8846 RVA: 0x00082BAB File Offset: 0x00080DAB
		public void Add(IQueueItem item)
		{
			this.Add(new Node<IQueueItem>(item));
		}

		// Token: 0x0600228F RID: 8847 RVA: 0x00082BBC File Offset: 0x00080DBC
		public void Add(Node<IQueueItem> node)
		{
			if (node == null)
			{
				throw new ArgumentNullException("node");
			}
			if (this.IsEmpty)
			{
				this.head = node;
				this.tail = node;
			}
			else
			{
				this.tail.Next = node;
				this.tail = node;
			}
			this.count++;
			node.Next = null;
		}

		// Token: 0x06002290 RID: 8848 RVA: 0x00082C17 File Offset: 0x00080E17
		public void Clear()
		{
			this.head = null;
			this.tail = null;
			this.count = 0;
		}

		// Token: 0x06002291 RID: 8849 RVA: 0x00082C2E File Offset: 0x00080E2E
		public bool Contains(IQueueItem item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002292 RID: 8850 RVA: 0x00082C35 File Offset: 0x00080E35
		public void CopyTo(IQueueItem[] array, int arrayIndex)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002293 RID: 8851 RVA: 0x00082C3C File Offset: 0x00080E3C
		public bool Remove(IQueueItem item)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06002294 RID: 8852 RVA: 0x00082C44 File Offset: 0x00080E44
		public void Concat(QueueItemList list)
		{
			if (list.IsEmpty)
			{
				return;
			}
			if (this.IsEmpty)
			{
				this.head = list.head;
				this.tail = list.tail;
			}
			else
			{
				this.tail.Next = list.head;
				this.tail = list.tail;
			}
			this.count += list.count;
		}

		// Token: 0x06002295 RID: 8853 RVA: 0x00082CAC File Offset: 0x00080EAC
		public void ForEach(Action<IQueueItem> action)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			for (Node<IQueueItem> next = this.head; next != null; next = next.Next)
			{
				MessageQueue.RunUnderPoisonContext(next.Value, action);
			}
		}

		// Token: 0x06002296 RID: 8854 RVA: 0x00082D8C File Offset: 0x00080F8C
		public IEnumerator<IQueueItem> GetEnumerator()
		{
			for (Node<IQueueItem> crt = this.head; crt != null; crt = crt.Next)
			{
				yield return crt.Value;
			}
			yield break;
		}

		// Token: 0x06002297 RID: 8855 RVA: 0x00082DA8 File Offset: 0x00080FA8
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this.GetEnumerator();
		}

		// Token: 0x04001205 RID: 4613
		private int count;

		// Token: 0x04001206 RID: 4614
		private Node<IQueueItem> head;

		// Token: 0x04001207 RID: 4615
		private Node<IQueueItem> tail;
	}
}
