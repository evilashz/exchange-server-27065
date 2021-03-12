using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000310 RID: 784
	internal class FifoQueue
	{
		// Token: 0x0600220F RID: 8719 RVA: 0x00080ACA File Offset: 0x0007ECCA
		public FifoQueue()
		{
			this.conditionQueue = new ConditionBasedQueue();
		}

		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x06002210 RID: 8720 RVA: 0x00080ADD File Offset: 0x0007ECDD
		public int ActiveCount
		{
			get
			{
				return this.count + this.conditionQueue.ActiveCount;
			}
		}

		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x06002211 RID: 8721 RVA: 0x00080AF1 File Offset: 0x0007ECF1
		public int LockedCount
		{
			get
			{
				return this.conditionQueue.TotalCount - this.conditionQueue.ActiveCount;
			}
		}

		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x06002212 RID: 8722 RVA: 0x00080B0A File Offset: 0x0007ED0A
		public int TotalCount
		{
			get
			{
				return this.count + this.conditionQueue.TotalCount;
			}
		}

		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x06002213 RID: 8723 RVA: 0x00080B20 File Offset: 0x0007ED20
		public long OldestItem
		{
			get
			{
				long num = (this.top != null) ? this.top.CreatedAt : DateTime.UtcNow.Ticks;
				if (this.conditionQueue.OldestItem < num)
				{
					num = this.conditionQueue.OldestItem;
				}
				return num;
			}
		}

		// Token: 0x06002214 RID: 8724 RVA: 0x00080B6C File Offset: 0x0007ED6C
		public void Enqueue(IQueueItem item)
		{
			if (item == null)
			{
				return;
			}
			QueueNode next = new QueueNode(item);
			if (this.tail == null)
			{
				this.top = next;
			}
			else
			{
				this.tail.Next = next;
			}
			this.tail = next;
			this.count++;
		}

		// Token: 0x06002215 RID: 8725 RVA: 0x00080BB8 File Offset: 0x0007EDB8
		public IQueueItem Dequeue()
		{
			IQueueItem queueItem = this.conditionQueue.Dequeue();
			if (queueItem != null)
			{
				return queueItem;
			}
			QueueNode queueNode = this.top;
			if (queueNode == null)
			{
				return null;
			}
			this.top = (QueueNode)this.top.Next;
			if (this.top == null)
			{
				this.tail = null;
			}
			this.count--;
			queueNode.Next = null;
			return queueNode.Value;
		}

		// Token: 0x06002216 RID: 8726 RVA: 0x00080C24 File Offset: 0x0007EE24
		public QueueItemList DequeueAll(Predicate<IQueueItem> match)
		{
			QueueNode queueNode = this.top;
			QueueNode queueNode2 = null;
			QueueItemList queueItemList = new QueueItemList();
			queueItemList.Concat(this.conditionQueue.DequeueAll(match, false));
			while (queueNode != null)
			{
				if (match(queueNode.Value))
				{
					QueueNode node = queueNode;
					queueNode = (QueueNode)queueNode.Next;
					if (queueNode2 == null)
					{
						this.top = queueNode;
					}
					else
					{
						queueNode2.Next = queueNode;
					}
					if (queueNode == null)
					{
						this.tail = queueNode2;
					}
					queueItemList.Add(node);
					this.count--;
				}
				else
				{
					queueNode2 = queueNode;
					queueNode = (QueueNode)queueNode.Next;
				}
			}
			return queueItemList;
		}

		// Token: 0x06002217 RID: 8727 RVA: 0x00080CB7 File Offset: 0x0007EEB7
		public QueueItemList DequeueAllLocked(Predicate<IQueueItem> match)
		{
			return this.conditionQueue.DequeueAll(match, true);
		}

		// Token: 0x06002218 RID: 8728 RVA: 0x00080CC8 File Offset: 0x0007EEC8
		public IQueueItem DequeueItem(DequeueMatch match, out bool matchFound)
		{
			matchFound = false;
			IQueueItem result = this.conditionQueue.DequeueItem(match, true, out matchFound);
			if (matchFound)
			{
				return result;
			}
			QueueNode queueNode = this.top;
			QueueNode queueNode2 = null;
			while (queueNode != null && !matchFound)
			{
				switch (match(queueNode.Value))
				{
				case DequeueMatchResult.Break:
					matchFound = true;
					break;
				case DequeueMatchResult.DequeueAndBreak:
					result = queueNode.Value;
					queueNode = (QueueNode)queueNode.Next;
					if (queueNode2 == null)
					{
						this.top = queueNode;
					}
					else
					{
						queueNode2.Next = queueNode;
					}
					if (queueNode == null)
					{
						this.tail = queueNode2;
					}
					this.count--;
					matchFound = true;
					break;
				case DequeueMatchResult.Continue:
					queueNode2 = queueNode;
					queueNode = (QueueNode)queueNode.Next;
					break;
				default:
					throw new InvalidOperationException("Invalid return value from match()");
				}
			}
			return result;
		}

		// Token: 0x06002219 RID: 8729 RVA: 0x00080D8C File Offset: 0x0007EF8C
		public void ForEach(Action<IQueueItem> action)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			for (QueueNode queueNode = this.top; queueNode != null; queueNode = (QueueNode)queueNode.Next)
			{
				MessageQueue.RunUnderPoisonContext(queueNode.Value, action);
			}
			this.conditionQueue.ForEach(action, true);
		}

		// Token: 0x0600221A RID: 8730 RVA: 0x00080DD8 File Offset: 0x0007EFD8
		public void ForEach<T>(Action<IQueueItem, T> action, T state)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			for (QueueNode queueNode = this.top; queueNode != null; queueNode = (QueueNode)queueNode.Next)
			{
				MessageQueue.RunUnderPoisonContext<T>(queueNode.Value, state, action);
			}
			this.conditionQueue.ForEach<T>(action, state, true);
		}

		// Token: 0x0600221B RID: 8731 RVA: 0x00080E26 File Offset: 0x0007F026
		public void Lock(IQueueItem item, WaitCondition condition, int dehydrateThreshold, Action<IQueueItem> dehydrateItem)
		{
			this.conditionQueue.Lock(item, condition, dehydrateThreshold, dehydrateItem);
		}

		// Token: 0x0600221C RID: 8732 RVA: 0x00080E38 File Offset: 0x0007F038
		public void RelockAll(IList<IQueueItem> items, string lockReason, ItemRelocked relockedItem)
		{
			this.conditionQueue.RelockAll(items, lockReason, relockedItem);
		}

		// Token: 0x0600221D RID: 8733 RVA: 0x00080E48 File Offset: 0x0007F048
		public bool ActivateOne(WaitCondition condition, AccessToken token, ItemUnlocked itemUnlocked)
		{
			return this.conditionQueue.ActivateOne(condition, token, itemUnlocked);
		}

		// Token: 0x0600221E RID: 8734 RVA: 0x00080E58 File Offset: 0x0007F058
		internal XElement GetDiagnosticInfo(XElement queue, bool conditionalQueuing)
		{
			queue.Add(new XElement("TotalCount", this.TotalCount));
			queue.Add(new XElement("LockedCount", this.LockedCount));
			queue.Add(new XElement("OldestItem", new DateTime(this.OldestItem).ToString()));
			if (conditionalQueuing)
			{
				queue.Add(this.conditionQueue.GetDiagnosticInfo());
			}
			return queue;
		}

		// Token: 0x040011D1 RID: 4561
		private ConditionBasedQueue conditionQueue;

		// Token: 0x040011D2 RID: 4562
		private QueueNode top;

		// Token: 0x040011D3 RID: 4563
		private QueueNode tail;

		// Token: 0x040011D4 RID: 4564
		private int count;
	}
}
