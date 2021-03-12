using System;
using Microsoft.Exchange.Data;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x0200030F RID: 783
	internal class DeferredQueue
	{
		// Token: 0x06002201 RID: 8705 RVA: 0x00080710 File Offset: 0x0007E910
		public DeferredQueue()
		{
			this.deferredCount = new int[DeferredQueue.deferReasonsCount];
		}

		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x06002202 RID: 8706 RVA: 0x00080750 File Offset: 0x0007E950
		public int TotalExcludedCount
		{
			get
			{
				return this.totalExcludedCount;
			}
		}

		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x06002203 RID: 8707 RVA: 0x00080758 File Offset: 0x0007E958
		public int TotalCount
		{
			get
			{
				return this.count + this.TotalExcludedCount;
			}
		}

		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x06002204 RID: 8708 RVA: 0x00080767 File Offset: 0x0007E967
		public long NextActivationTime
		{
			get
			{
				return this.nextActivation;
			}
		}

		// Token: 0x06002205 RID: 8709 RVA: 0x0008076F File Offset: 0x0007E96F
		public void UpdateNextActivationTime(long ticks)
		{
			if (ticks < this.nextActivation)
			{
				this.nextActivation = ticks;
			}
		}

		// Token: 0x06002206 RID: 8710 RVA: 0x00080784 File Offset: 0x0007E984
		public void Enqueue(IQueueItem item)
		{
			if (item == null)
			{
				return;
			}
			this.head = new Node<IQueueItem>(item)
			{
				Next = this.head
			};
			this.IncrementCount(item);
			DateTime dateTime;
			if (item.DeferUntil == DateTime.MaxValue)
			{
				dateTime = item.Expiry;
			}
			else
			{
				dateTime = item.DeferUntil;
			}
			this.UpdateNextActivationTime(dateTime.Ticks);
		}

		// Token: 0x06002207 RID: 8711 RVA: 0x000807E8 File Offset: 0x0007E9E8
		public QueueItemList DequeueAll(Predicate<IQueueItem> match)
		{
			long val = DateTime.UtcNow.Ticks + 36000000000L;
			QueueItemList queueItemList = new QueueItemList();
			Node<IQueueItem> next = this.head;
			Node<IQueueItem> node = null;
			while (next != null)
			{
				long val2 = Math.Min(next.Value.DeferUntil.Ticks, next.Value.Expiry.Ticks);
				if (match(next.Value))
				{
					this.DecrementCount(next.Value);
					if (node == null)
					{
						this.head = next.Next;
					}
					else
					{
						node.Next = next.Next;
					}
					Node<IQueueItem> node2 = next;
					next = next.Next;
					queueItemList.Add(node2);
				}
				else
				{
					val = Math.Min(val, val2);
					node = next;
					next = next.Next;
				}
			}
			this.nextActivation = val;
			return queueItemList;
		}

		// Token: 0x06002208 RID: 8712 RVA: 0x000808BC File Offset: 0x0007EABC
		public IQueueItem DequeueItem(DequeueMatch match, out bool matchFound)
		{
			matchFound = false;
			IQueueItem result = null;
			Node<IQueueItem> next = this.head;
			Node<IQueueItem> node = null;
			while (next != null && !matchFound)
			{
				switch (match(next.Value))
				{
				case DequeueMatchResult.Break:
					matchFound = true;
					break;
				case DequeueMatchResult.DequeueAndBreak:
					this.DecrementCount(next.Value);
					if (node == null)
					{
						this.head = next.Next;
					}
					else
					{
						node.Next = next.Next;
					}
					result = next.Value;
					matchFound = true;
					break;
				case DequeueMatchResult.Continue:
					node = next;
					next = next.Next;
					break;
				default:
					throw new InvalidOperationException("Invalid return value from match()");
				}
			}
			return result;
		}

		// Token: 0x06002209 RID: 8713 RVA: 0x00080954 File Offset: 0x0007EB54
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

		// Token: 0x0600220A RID: 8714 RVA: 0x00080990 File Offset: 0x0007EB90
		public void ForEach<T>(Action<IQueueItem, T> action, T state)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			for (Node<IQueueItem> next = this.head; next != null; next = next.Next)
			{
				MessageQueue.RunUnderPoisonContext<T>(next.Value, state, action);
			}
		}

		// Token: 0x0600220B RID: 8715 RVA: 0x000809CC File Offset: 0x0007EBCC
		public int GetDeferredCount(DeferReason reason)
		{
			if (reason < DeferReason.None || reason >= (DeferReason)DeferredQueue.deferReasonsCount)
			{
				throw new ArgumentOutOfRangeException("reason");
			}
			return this.deferredCount[(int)reason];
		}

		// Token: 0x0600220C RID: 8716 RVA: 0x000809FC File Offset: 0x0007EBFC
		private void IncrementCount(IQueueItem item)
		{
			RoutedMailItem routedMailItem = item as RoutedMailItem;
			if (routedMailItem != null && routedMailItem.DeferReason != DeferReason.None)
			{
				this.deferredCount[(int)routedMailItem.DeferReason]++;
				this.totalExcludedCount++;
				return;
			}
			this.count++;
		}

		// Token: 0x0600220D RID: 8717 RVA: 0x00080A58 File Offset: 0x0007EC58
		private void DecrementCount(IQueueItem item)
		{
			RoutedMailItem routedMailItem = item as RoutedMailItem;
			if (routedMailItem != null && routedMailItem.DeferReason != DeferReason.None)
			{
				this.deferredCount[(int)routedMailItem.DeferReason]--;
				this.totalExcludedCount--;
				return;
			}
			this.count--;
		}

		// Token: 0x040011CB RID: 4555
		private static readonly int deferReasonsCount = Enum.GetNames(typeof(DeferReason)).Length;

		// Token: 0x040011CC RID: 4556
		private Node<IQueueItem> head;

		// Token: 0x040011CD RID: 4557
		private long nextActivation = DateTime.UtcNow.Ticks + 36000000000L;

		// Token: 0x040011CE RID: 4558
		private int count;

		// Token: 0x040011CF RID: 4559
		private int[] deferredCount;

		// Token: 0x040011D0 RID: 4560
		private int totalExcludedCount;
	}
}
