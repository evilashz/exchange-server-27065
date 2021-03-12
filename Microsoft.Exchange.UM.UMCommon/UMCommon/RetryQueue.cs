using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;

namespace Microsoft.Exchange.UM.UMCommon
{
	// Token: 0x02000151 RID: 337
	internal class RetryQueue<T> where T : class
	{
		// Token: 0x06000ADF RID: 2783 RVA: 0x00028E90 File Offset: 0x00027090
		public RetryQueue(Trace tracer, TimeSpan retryInterval)
		{
			this.retryInterval = retryInterval;
			this.tracer = tracer;
			this.itemList = new List<RetryQueue<T>.QueueItem<T>>();
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06000AE0 RID: 2784 RVA: 0x00028EB1 File Offset: 0x000270B1
		// (set) Token: 0x06000AE1 RID: 2785 RVA: 0x00028EB9 File Offset: 0x000270B9
		public TimeSpan RetryInterval
		{
			get
			{
				return this.retryInterval;
			}
			set
			{
				this.retryInterval = value;
			}
		}

		// Token: 0x1700029E RID: 670
		// (get) Token: 0x06000AE2 RID: 2786 RVA: 0x00028EC2 File Offset: 0x000270C2
		public int Count
		{
			get
			{
				return this.itemList.Count;
			}
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x00028ED0 File Offset: 0x000270D0
		public void Enqueue(T item)
		{
			ExDateTime exDateTime = ExDateTime.UtcNow.Add(this.retryInterval);
			CallIdTracer.TraceDebug(this.tracer, this.GetHashCode(), "RetryQueue: Enqueue {0} for retry at {1}.", new object[]
			{
				item.GetHashCode(),
				exDateTime
			});
			this.itemList.Add(new RetryQueue<T>.QueueItem<T>(item, exDateTime));
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x00028F44 File Offset: 0x00027144
		public T Dequeue()
		{
			return this.Dequeue(false);
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x00028F50 File Offset: 0x00027150
		public T Dequeue(bool forceDequeue)
		{
			if (this.itemList.Count == 0 || (!forceDequeue && !this.itemList[0].Expired))
			{
				return default(T);
			}
			RetryQueue<T>.QueueItem<T> queueItem = this.itemList[0];
			this.itemList.RemoveAt(0);
			CallIdTracer.TraceDebug(this.tracer, this.GetHashCode(), "Dequeue {0} at {1}.", new object[]
			{
				queueItem.GetHashCode(),
				queueItem.Expiry
			});
			return queueItem.Item;
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x00028FE8 File Offset: 0x000271E8
		public void Clear()
		{
			this.itemList.Clear();
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x00028FF8 File Offset: 0x000271F8
		public void CopyTo(List<T> serverList)
		{
			for (int i = 0; i < this.itemList.Count; i++)
			{
				serverList.Add(this.itemList[i].Item);
			}
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x00029034 File Offset: 0x00027234
		public void DeleteInvalid(IList<T> validList)
		{
			Dictionary<T, bool> dictionary = new Dictionary<T, bool>();
			foreach (T key in validList)
			{
				dictionary.Add(key, true);
			}
			int i = 0;
			while (i < this.itemList.Count)
			{
				RetryQueue<T>.QueueItem<T> queueItem = this.itemList[i];
				if (!dictionary.ContainsKey(queueItem.Item))
				{
					this.itemList.RemoveAt(i);
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x000290C8 File Offset: 0x000272C8
		public bool Contains(T item)
		{
			for (int i = 0; i < this.itemList.Count; i++)
			{
				T item2 = this.itemList[i].Item;
				if (item.Equals(item2))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x00029118 File Offset: 0x00027318
		public bool Remove(T item)
		{
			int num = -1;
			for (int i = 0; i < this.itemList.Count; i++)
			{
				T item2 = this.itemList[i].Item;
				if (item2.Equals(item))
				{
					num = i;
					break;
				}
			}
			if (num != -1)
			{
				this.itemList.RemoveAt(num);
				return true;
			}
			return false;
		}

		// Token: 0x040005D6 RID: 1494
		private TimeSpan retryInterval;

		// Token: 0x040005D7 RID: 1495
		private Trace tracer;

		// Token: 0x040005D8 RID: 1496
		private List<RetryQueue<T>.QueueItem<T>> itemList;

		// Token: 0x02000152 RID: 338
		private class QueueItem<Q> : IEquatable<Q> where Q : class
		{
			// Token: 0x06000AEB RID: 2795 RVA: 0x0002917B File Offset: 0x0002737B
			public QueueItem(Q item, ExDateTime expiry)
			{
				this.item = item;
				this.expiry = expiry;
			}

			// Token: 0x1700029F RID: 671
			// (get) Token: 0x06000AEC RID: 2796 RVA: 0x00029191 File Offset: 0x00027391
			public ExDateTime Expiry
			{
				get
				{
					return this.expiry;
				}
			}

			// Token: 0x170002A0 RID: 672
			// (get) Token: 0x06000AED RID: 2797 RVA: 0x00029199 File Offset: 0x00027399
			public bool Expired
			{
				get
				{
					return ExDateTime.Compare(ExDateTime.UtcNow, this.expiry) > 0;
				}
			}

			// Token: 0x170002A1 RID: 673
			// (get) Token: 0x06000AEE RID: 2798 RVA: 0x000291AE File Offset: 0x000273AE
			public Q Item
			{
				get
				{
					return this.item;
				}
			}

			// Token: 0x06000AEF RID: 2799 RVA: 0x000291B8 File Offset: 0x000273B8
			public override bool Equals(object obj)
			{
				if (obj == null)
				{
					return false;
				}
				Q q = obj as Q;
				if (q != null)
				{
					return this.item.Equals(q);
				}
				return base.Equals(obj);
			}

			// Token: 0x06000AF0 RID: 2800 RVA: 0x000291FD File Offset: 0x000273FD
			public override int GetHashCode()
			{
				return this.item.GetHashCode();
			}

			// Token: 0x06000AF1 RID: 2801 RVA: 0x00029210 File Offset: 0x00027410
			public bool Equals(Q other)
			{
				Q q = this.Item;
				return q.Equals(other);
			}

			// Token: 0x040005D9 RID: 1497
			private Q item;

			// Token: 0x040005DA RID: 1498
			private ExDateTime expiry;
		}
	}
}
