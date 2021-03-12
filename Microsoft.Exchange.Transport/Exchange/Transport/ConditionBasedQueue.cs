using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000328 RID: 808
	internal class ConditionBasedQueue
	{
		// Token: 0x060022FC RID: 8956 RVA: 0x00084978 File Offset: 0x00082B78
		public ConditionBasedQueue()
		{
			this.lockedList = new Dictionary<WaitCondition, ConditionBasedQueue.LinkedListPair>();
			this.activeList = new ConditionBasedQueue.LinkedListPair(null, null);
			this.activeLock = new object();
			this.lockedLock = new object();
		}

		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x060022FD RID: 8957 RVA: 0x000849AE File Offset: 0x00082BAE
		public int ActiveCount
		{
			get
			{
				return this.activeCount;
			}
		}

		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x060022FE RID: 8958 RVA: 0x000849B6 File Offset: 0x00082BB6
		public int TotalCount
		{
			get
			{
				return this.lockedCount + this.activeCount;
			}
		}

		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x060022FF RID: 8959 RVA: 0x000849C8 File Offset: 0x00082BC8
		public long OldestItem
		{
			get
			{
				if (!this.activeList.IsEmpty)
				{
					return this.activeList.Head.CreatedAt;
				}
				return DateTime.UtcNow.Ticks;
			}
		}

		// Token: 0x06002300 RID: 8960 RVA: 0x00084A00 File Offset: 0x00082C00
		public void Lock(IQueueItem item, WaitCondition condition, int dehydrateThreshold, Action<IQueueItem> dehydrateItem)
		{
			QueueNode node = new QueueNode(item);
			lock (this.lockedLock)
			{
				ConditionBasedQueue.Add(this.lockedList, condition, node, dehydrateThreshold, dehydrateItem);
				this.lockedCount++;
			}
		}

		// Token: 0x06002301 RID: 8961 RVA: 0x00084A60 File Offset: 0x00082C60
		public void RelockAll(IList<IQueueItem> items, string lockReason, ItemRelocked itemRelocked)
		{
			lock (this.activeLock)
			{
				lock (this.lockedLock)
				{
					this.lockedCount += this.activeCount;
					this.activeCount = 0;
					Dictionary<WaitCondition, ConditionBasedQueue.LinkedListPair> dictionary = new Dictionary<WaitCondition, ConditionBasedQueue.LinkedListPair>();
					if (items != null)
					{
						foreach (IQueueItem item in items)
						{
							WaitCondition condition;
							itemRelocked(item, lockReason, out condition);
							QueueNode node = new QueueNode(item);
							ConditionBasedQueue.Add(dictionary, condition, node, 0, null);
							this.lockedCount++;
						}
					}
					for (QueueNode queueNode = this.activeList.RemoveFirst(); queueNode != null; queueNode = this.activeList.RemoveFirst())
					{
						WaitCondition condition;
						itemRelocked(queueNode.Value, lockReason, out condition);
						ConditionBasedQueue.Add(dictionary, condition, queueNode, 0, null);
					}
					foreach (KeyValuePair<WaitCondition, ConditionBasedQueue.LinkedListPair> keyValuePair in dictionary)
					{
						ConditionBasedQueue.LinkedListPair list;
						if (!this.lockedList.TryGetValue(keyValuePair.Key, out list))
						{
							this.lockedList.Add(keyValuePair.Key, keyValuePair.Value);
						}
						else
						{
							keyValuePair.Value.Append(list);
							this.lockedList[keyValuePair.Key] = keyValuePair.Value;
						}
					}
				}
			}
		}

		// Token: 0x06002302 RID: 8962 RVA: 0x00084C48 File Offset: 0x00082E48
		public QueueItemList DequeueAll(Predicate<IQueueItem> match, bool lockedOnly)
		{
			QueueItemList queueItemList = new QueueItemList();
			if (!lockedOnly)
			{
				lock (this.activeLock)
				{
					queueItemList = ConditionBasedQueue.DequeueAll(match, this.activeList);
					this.activeCount -= queueItemList.Count;
				}
			}
			lock (this.lockedList)
			{
				List<WaitCondition> list = new List<WaitCondition>();
				foreach (KeyValuePair<WaitCondition, ConditionBasedQueue.LinkedListPair> keyValuePair in this.lockedList)
				{
					ConditionBasedQueue.LinkedListPair value = keyValuePair.Value;
					QueueItemList queueItemList2 = ConditionBasedQueue.DequeueAll(match, value);
					if (value.IsEmpty)
					{
						list.Add(keyValuePair.Key);
					}
					this.lockedCount -= queueItemList2.Count;
					queueItemList.Concat(queueItemList2);
				}
				if (list.Count > 0)
				{
					foreach (WaitCondition key in list)
					{
						this.lockedList.Remove(key);
					}
				}
			}
			return queueItemList;
		}

		// Token: 0x06002303 RID: 8963 RVA: 0x00084DB4 File Offset: 0x00082FB4
		public IQueueItem Dequeue()
		{
			IQueueItem result;
			lock (this.activeLock)
			{
				QueueNode queueNode = this.activeList.RemoveFirst();
				if (queueNode == null)
				{
					if (this.activeCount != 0)
					{
						throw new InvalidOperationException("We have active items that we have lost");
					}
					result = null;
				}
				else
				{
					this.activeCount--;
					result = queueNode.Value;
				}
			}
			return result;
		}

		// Token: 0x06002304 RID: 8964 RVA: 0x00084E2C File Offset: 0x0008302C
		public IQueueItem DequeueItem(DequeueMatch match, bool iterateLocked, out bool matchFound)
		{
			matchFound = false;
			lock (this.activeLock)
			{
				QueueNode queueNode;
				if (ConditionBasedQueue.DequeueItem(match, this.activeList, out queueNode))
				{
					matchFound = true;
					if (queueNode != null)
					{
						this.activeCount--;
						return queueNode.Value;
					}
					return null;
				}
			}
			if (iterateLocked)
			{
				lock (this.lockedLock)
				{
					IQueueItem result = null;
					List<WaitCondition> list = new List<WaitCondition>();
					foreach (KeyValuePair<WaitCondition, ConditionBasedQueue.LinkedListPair> keyValuePair in this.lockedList)
					{
						ConditionBasedQueue.LinkedListPair value = keyValuePair.Value;
						QueueNode queueNode;
						matchFound = ConditionBasedQueue.DequeueItem(match, value, out queueNode);
						if (matchFound)
						{
							if (value.IsEmpty)
							{
								list.Add(keyValuePair.Key);
							}
							if (queueNode != null)
							{
								this.lockedCount--;
								result = queueNode.Value;
								break;
							}
							break;
						}
					}
					if (list.Count > 0)
					{
						foreach (WaitCondition key in list)
						{
							this.lockedList.Remove(key);
						}
					}
					return result;
				}
			}
			return null;
		}

		// Token: 0x06002305 RID: 8965 RVA: 0x00084FD4 File Offset: 0x000831D4
		public void ForEach(Action<IQueueItem> action, bool iterateLocked)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			bool flag;
			this.DequeueItem(delegate(IQueueItem item)
			{
				MessageQueue.RunUnderPoisonContext(item, action);
				return DequeueMatchResult.Continue;
			}, iterateLocked, out flag);
		}

		// Token: 0x06002306 RID: 8966 RVA: 0x00085034 File Offset: 0x00083234
		public void ForEach<T>(Action<IQueueItem, T> action, T state, bool iterateLocked)
		{
			if (action == null)
			{
				throw new ArgumentNullException("action");
			}
			bool flag;
			this.DequeueItem(delegate(IQueueItem item)
			{
				MessageQueue.RunUnderPoisonContext<T>(item, state, action);
				return DequeueMatchResult.Continue;
			}, iterateLocked, out flag);
		}

		// Token: 0x06002307 RID: 8967 RVA: 0x00085080 File Offset: 0x00083280
		public bool ActivateOne(WaitCondition condition, AccessToken token, ItemUnlocked itemUnlocked)
		{
			lock (this.activeLock)
			{
				QueueNode queueNode;
				lock (this.lockedLock)
				{
					ConditionBasedQueue.LinkedListPair linkedListPair;
					if (!this.lockedList.TryGetValue(condition, out linkedListPair))
					{
						return false;
					}
					for (;;)
					{
						queueNode = linkedListPair.RemoveFirst();
						if (queueNode == null)
						{
							break;
						}
						this.lockedCount--;
						if (itemUnlocked(queueNode.Value, token))
						{
							goto Block_7;
						}
					}
					this.lockedList.Remove(condition);
					return false;
					Block_7:;
				}
				this.activeList.Append(queueNode);
				this.activeCount++;
			}
			return true;
		}

		// Token: 0x06002308 RID: 8968 RVA: 0x00085154 File Offset: 0x00083354
		internal XElement GetDiagnosticInfo()
		{
			XElement xelement = new XElement("conditionQueue");
			xelement.Add(new XElement("UnlockedCount", this.ActiveCount));
			xelement.Add(new XElement("Locked", this.lockedCount));
			lock (this.lockedLock)
			{
				XElement xelement2 = new XElement("LockedList");
				foreach (KeyValuePair<WaitCondition, ConditionBasedQueue.LinkedListPair> keyValuePair in this.lockedList)
				{
					XElement xelement3 = new XElement("condition");
					xelement3.Add(new XElement("condition", keyValuePair.Key.ToString()));
					xelement3.Add(new XElement("lockedCount", keyValuePair.Value.Count));
					xelement3.Add(new XElement("dehydrated", keyValuePair.Value.Dehydrated));
					xelement3.Add(new XElement("lastProcessedTime", keyValuePair.Value.LastDequeueTime));
					xelement2.Add(xelement3);
				}
				xelement.Add(xelement2);
			}
			return xelement;
		}

		// Token: 0x06002309 RID: 8969 RVA: 0x00085318 File Offset: 0x00083518
		private static void Add(Dictionary<WaitCondition, ConditionBasedQueue.LinkedListPair> lockedList, WaitCondition condition, QueueNode node, int dehydrateThreshold, Action<IQueueItem> dehydrateDelegate)
		{
			ConditionBasedQueue.LinkedListPair linkedListPair;
			if (!lockedList.TryGetValue(condition, out linkedListPair))
			{
				linkedListPair = new ConditionBasedQueue.LinkedListPair(node, null);
				lockedList[condition] = linkedListPair;
			}
			else
			{
				linkedListPair.Append(node);
			}
			if (dehydrateThreshold <= 0 || dehydrateDelegate == null || linkedListPair.Count <= dehydrateThreshold)
			{
				linkedListPair.Dehydrated = false;
				return;
			}
			if (!linkedListPair.Dehydrated)
			{
				QueueNode queueNode;
				ConditionBasedQueue.DequeueItem(delegate(IQueueItem item)
				{
					dehydrateDelegate(item);
					return DequeueMatchResult.Continue;
				}, linkedListPair, out queueNode);
				linkedListPair.Dehydrated = true;
				return;
			}
			dehydrateDelegate(node.Value);
		}

		// Token: 0x0600230A RID: 8970 RVA: 0x000853B4 File Offset: 0x000835B4
		private static bool DequeueItem(DequeueMatch match, ConditionBasedQueue.LinkedListPair list, out QueueNode matchedItem)
		{
			QueueNode queueNode = list.Head;
			QueueNode previous = null;
			bool flag = false;
			matchedItem = null;
			while (queueNode != null && !flag)
			{
				switch (match(queueNode.Value))
				{
				case DequeueMatchResult.Break:
					flag = true;
					break;
				case DequeueMatchResult.DequeueAndBreak:
					list.RemoveCurrent(previous, queueNode);
					matchedItem = queueNode;
					flag = true;
					break;
				case DequeueMatchResult.Continue:
					previous = queueNode;
					queueNode = (QueueNode)queueNode.Next;
					break;
				default:
					throw new InvalidOperationException("Invalid return value from match()");
				}
			}
			return flag;
		}

		// Token: 0x0600230B RID: 8971 RVA: 0x00085428 File Offset: 0x00083628
		private static QueueItemList DequeueAll(Predicate<IQueueItem> match, ConditionBasedQueue.LinkedListPair list)
		{
			QueueItemList queueItemList = new QueueItemList();
			QueueNode queueNode = list.Head;
			QueueNode previous = null;
			while (queueNode != null)
			{
				if (match(queueNode.Value))
				{
					list.RemoveCurrent(previous, queueNode);
					queueItemList.Add(queueNode.Value);
				}
				else
				{
					previous = queueNode;
				}
				queueNode = (QueueNode)queueNode.Next;
			}
			return queueItemList;
		}

		// Token: 0x04001237 RID: 4663
		private ConditionBasedQueue.LinkedListPair activeList;

		// Token: 0x04001238 RID: 4664
		private Dictionary<WaitCondition, ConditionBasedQueue.LinkedListPair> lockedList;

		// Token: 0x04001239 RID: 4665
		private int lockedCount;

		// Token: 0x0400123A RID: 4666
		private int activeCount;

		// Token: 0x0400123B RID: 4667
		private object activeLock;

		// Token: 0x0400123C RID: 4668
		private object lockedLock;

		// Token: 0x02000329 RID: 809
		private class LinkedListPair
		{
			// Token: 0x0600230C RID: 8972 RVA: 0x0008547C File Offset: 0x0008367C
			public LinkedListPair(QueueNode head, QueueNode tail)
			{
				this.Head = head;
				this.Tail = tail;
				this.count = ((this.Head != null) ? 1 : 0) + ((this.Tail != null) ? 1 : 0);
			}

			// Token: 0x17000B09 RID: 2825
			// (get) Token: 0x0600230D RID: 8973 RVA: 0x000854BC File Offset: 0x000836BC
			public bool IsEmpty
			{
				get
				{
					return this.Head == null;
				}
			}

			// Token: 0x17000B0A RID: 2826
			// (get) Token: 0x0600230E RID: 8974 RVA: 0x000854C7 File Offset: 0x000836C7
			public int Count
			{
				get
				{
					return this.count;
				}
			}

			// Token: 0x17000B0B RID: 2827
			// (get) Token: 0x0600230F RID: 8975 RVA: 0x000854CF File Offset: 0x000836CF
			// (set) Token: 0x06002310 RID: 8976 RVA: 0x000854D7 File Offset: 0x000836D7
			public bool Dehydrated
			{
				get
				{
					return this.dehydrated;
				}
				set
				{
					this.dehydrated = value;
				}
			}

			// Token: 0x17000B0C RID: 2828
			// (get) Token: 0x06002311 RID: 8977 RVA: 0x000854E0 File Offset: 0x000836E0
			public DateTime LastDequeueTime
			{
				get
				{
					return this.lastDequeueTime;
				}
			}

			// Token: 0x17000B0D RID: 2829
			// (get) Token: 0x06002312 RID: 8978 RVA: 0x000854E8 File Offset: 0x000836E8
			// (set) Token: 0x06002313 RID: 8979 RVA: 0x000854F0 File Offset: 0x000836F0
			public QueueNode Head { get; set; }

			// Token: 0x17000B0E RID: 2830
			// (get) Token: 0x06002314 RID: 8980 RVA: 0x000854F9 File Offset: 0x000836F9
			// (set) Token: 0x06002315 RID: 8981 RVA: 0x00085501 File Offset: 0x00083701
			public QueueNode Tail { get; set; }

			// Token: 0x06002316 RID: 8982 RVA: 0x0008550C File Offset: 0x0008370C
			public void Append(QueueNode end)
			{
				if (end == null)
				{
					throw new ArgumentNullException("end");
				}
				this.count++;
				if (this.Head == null)
				{
					this.Head = end;
					return;
				}
				if (this.Tail == null)
				{
					this.Head.Next = end;
					this.Tail = end;
					return;
				}
				this.Tail.Next = end;
				this.Tail = end;
			}

			// Token: 0x06002317 RID: 8983 RVA: 0x00085574 File Offset: 0x00083774
			public void Append(ConditionBasedQueue.LinkedListPair list2)
			{
				if (list2 == null)
				{
					throw new ArgumentNullException("list2");
				}
				if (list2.IsEmpty)
				{
					return;
				}
				if (this.IsEmpty)
				{
					this.Head = list2.Head;
					this.Tail = list2.Tail;
					this.count = list2.Count;
					return;
				}
				this.count += list2.Count;
				QueueNode tail = list2.Tail;
				if (list2.Tail == null)
				{
					tail = list2.Head;
				}
				if (this.Tail != null)
				{
					this.Tail.Next = list2.Head;
				}
				else
				{
					this.Head.Next = list2.Head;
				}
				this.Tail = tail;
			}

			// Token: 0x06002318 RID: 8984 RVA: 0x00085624 File Offset: 0x00083824
			public void Prepend(QueueNode start)
			{
				if (start == null)
				{
					throw new ArgumentNullException("start");
				}
				this.count++;
				if (this.Tail == null)
				{
					this.Tail = this.Head;
				}
				start.Next = this.Head;
				this.Head = start;
			}

			// Token: 0x06002319 RID: 8985 RVA: 0x00085674 File Offset: 0x00083874
			public QueueNode RemoveFirst()
			{
				if (this.IsEmpty)
				{
					return null;
				}
				this.count--;
				QueueNode head = this.Head;
				if (this.Tail == null)
				{
					this.Head = null;
				}
				else
				{
					this.Head = (QueueNode)this.Head.Next;
					if (this.Head == this.Tail)
					{
						this.Tail = null;
					}
				}
				this.lastDequeueTime = DateTime.UtcNow;
				head.Next = null;
				return head;
			}

			// Token: 0x0600231A RID: 8986 RVA: 0x000856F0 File Offset: 0x000838F0
			public void RemoveCurrent(QueueNode previous, QueueNode current)
			{
				this.count--;
				if (previous == null)
				{
					this.Head = (QueueNode)current.Next;
					if (!this.IsEmpty && this.Head.Next == null)
					{
						this.Tail = null;
						return;
					}
				}
				else if (current == this.Tail)
				{
					previous.Next = null;
					if (previous == this.Head)
					{
						this.Tail = null;
						return;
					}
					this.Tail = previous;
					return;
				}
				else
				{
					previous.Next = current.Next;
				}
			}

			// Token: 0x0400123D RID: 4669
			private int count;

			// Token: 0x0400123E RID: 4670
			private bool dehydrated;

			// Token: 0x0400123F RID: 4671
			private DateTime lastDequeueTime = DateTime.UtcNow;
		}
	}
}
