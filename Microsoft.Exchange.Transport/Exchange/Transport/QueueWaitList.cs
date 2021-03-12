using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000339 RID: 825
	internal class QueueWaitList
	{
		// Token: 0x06002393 RID: 9107 RVA: 0x00087424 File Offset: 0x00085624
		public QueueWaitList(NextHopSolutionKey queue, Trace tracer) : this(tracer)
		{
			QueueWaitList.QueueWeight value = new QueueWaitList.QueueWeight(queue);
			this.queues[queue] = value;
			this.messageCount++;
		}

		// Token: 0x06002394 RID: 9108 RVA: 0x0008745A File Offset: 0x0008565A
		public QueueWaitList(Trace tracer)
		{
			this.tracer = tracer;
			this.queues = new Dictionary<NextHopSolutionKey, QueueWaitList.QueueWeight>();
		}

		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x06002395 RID: 9109 RVA: 0x0008747F File Offset: 0x0008567F
		// (set) Token: 0x06002396 RID: 9110 RVA: 0x00087486 File Offset: 0x00085686
		public static TimeSpan MaxConditionSkewInterval { get; set; }

		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x06002397 RID: 9111 RVA: 0x0008748E File Offset: 0x0008568E
		public int QueueCount
		{
			get
			{
				return this.queues.Count;
			}
		}

		// Token: 0x17000B2B RID: 2859
		// (get) Token: 0x06002398 RID: 9112 RVA: 0x0008749B File Offset: 0x0008569B
		public int MessageCount
		{
			get
			{
				return this.messageCount;
			}
		}

		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x06002399 RID: 9113 RVA: 0x000874A3 File Offset: 0x000856A3
		public int PendingMessageCount
		{
			get
			{
				return this.pendingMessageCount;
			}
		}

		// Token: 0x0600239A RID: 9114 RVA: 0x000874AC File Offset: 0x000856AC
		public bool HasOlderMessages(NextHopSolutionKey queue)
		{
			lock (this.syncRoot)
			{
				QueueWaitList.QueueWeight queueWeight;
				if (this.queues.TryGetValue(queue, out queueWeight))
				{
					return queueWeight.HasOlderMessages;
				}
			}
			return false;
		}

		// Token: 0x0600239B RID: 9115 RVA: 0x00087504 File Offset: 0x00085704
		public int GetMessageCount(NextHopSolutionKey queue)
		{
			lock (this.syncRoot)
			{
				QueueWaitList.QueueWeight queueWeight;
				if (this.queues.TryGetValue(queue, out queueWeight))
				{
					return queueWeight.MessageCount;
				}
			}
			return 0;
		}

		// Token: 0x0600239C RID: 9116 RVA: 0x0008755C File Offset: 0x0008575C
		public int GetPendingMessageCount(NextHopSolutionKey queue)
		{
			lock (this.syncRoot)
			{
				QueueWaitList.QueueWeight queueWeight;
				if (this.queues.TryGetValue(queue, out queueWeight))
				{
					return queueWeight.PendingMessageCount;
				}
			}
			return 0;
		}

		// Token: 0x0600239D RID: 9117 RVA: 0x000875B4 File Offset: 0x000857B4
		public bool HasDisabledMessages(NextHopSolutionKey queue)
		{
			lock (this.syncRoot)
			{
				QueueWaitList.QueueWeight queueWeight;
				if (this.queues.TryGetValue(queue, out queueWeight))
				{
					return queueWeight.HasDisabledMessages;
				}
			}
			return false;
		}

		// Token: 0x0600239E RID: 9118 RVA: 0x0008760C File Offset: 0x0008580C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			lock (this.syncRoot)
			{
				stringBuilder.AppendFormat("Found {0} queues {", this.queues.Count);
				foreach (KeyValuePair<NextHopSolutionKey, QueueWaitList.QueueWeight> keyValuePair in this.queues)
				{
					stringBuilder.AppendFormat("Queue {0}: {1}\n", keyValuePair.Key.ToShortString(), keyValuePair.Value.ToString());
				}
			}
			stringBuilder.AppendLine("}");
			return stringBuilder.ToString();
		}

		// Token: 0x0600239F RID: 9119 RVA: 0x000876E0 File Offset: 0x000858E0
		internal bool Add(NextHopSolutionKey queue)
		{
			bool result;
			lock (this.syncRoot)
			{
				if (this.state == WaitListState.Deleted)
				{
					result = false;
				}
				else
				{
					QueueWaitList.QueueWeight queueWeight;
					if (this.queues.TryGetValue(queue, out queueWeight))
					{
						queueWeight.Add();
					}
					else
					{
						queueWeight = new QueueWaitList.QueueWeight(queue);
						this.queues[queue] = queueWeight;
					}
					this.messageCount++;
					result = true;
				}
			}
			return result;
		}

		// Token: 0x060023A0 RID: 9120 RVA: 0x00087764 File Offset: 0x00085964
		internal bool GetNextItem(NextHopSolutionKey queue)
		{
			lock (this.syncRoot)
			{
				QueueWaitList.QueueWeight queueWeight;
				if (this.queues.TryGetValue(queue, out queueWeight) && queueWeight.ProvisionallyRemove())
				{
					this.messageCount--;
					this.pendingMessageCount++;
					return true;
				}
			}
			return false;
		}

		// Token: 0x060023A1 RID: 9121 RVA: 0x000877DC File Offset: 0x000859DC
		internal bool ConfirmRemove(NextHopSolutionKey queue)
		{
			lock (this.syncRoot)
			{
				QueueWaitList.QueueWeight queueWeight;
				if (!this.queues.TryGetValue(queue, out queueWeight))
				{
					throw new KeyNotFoundException("The specified queue name was not found");
				}
				if (queueWeight.ConfirmRemove())
				{
					return this.RemoveAndCleanupIfLast(queue);
				}
			}
			return false;
		}

		// Token: 0x060023A2 RID: 9122 RVA: 0x00087850 File Offset: 0x00085A50
		internal bool RemoveWaitingAndOneOutstanding(NextHopSolutionKey queue)
		{
			lock (this.syncRoot)
			{
				QueueWaitList.QueueWeight queueWeight;
				if (this.queues.TryGetValue(queue, out queueWeight))
				{
					this.tracer.TraceWarning(0L, "Removing items may make the map out of sync with the queues");
					bool flag2 = queueWeight.RemoveWaitingAndOneOutstanding();
					this.messageCount = this.queues.Values.Sum((QueueWaitList.QueueWeight q) => q.MessageCount);
					this.pendingMessageCount--;
					if (flag2)
					{
						return this.RemoveAndCleanupIfLast(queue);
					}
				}
			}
			return false;
		}

		// Token: 0x060023A3 RID: 9123 RVA: 0x00087910 File Offset: 0x00085B10
		internal bool MoveToDisabled(NextHopSolutionKey queue)
		{
			bool result;
			lock (this.syncRoot)
			{
				if (this.state == WaitListState.Deleted)
				{
					result = false;
				}
				else
				{
					QueueWaitList.QueueWeight queueWeight;
					if (!this.queues.TryGetValue(queue, out queueWeight))
					{
						queueWeight = new QueueWaitList.QueueWeight(queue);
						this.queues[queue] = queueWeight;
					}
					queueWeight.MoveToDisabled();
					this.messageCount = this.queues.Values.Sum((QueueWaitList.QueueWeight q) => q.MessageCount);
					result = true;
				}
			}
			return result;
		}

		// Token: 0x060023A4 RID: 9124 RVA: 0x000879B8 File Offset: 0x00085BB8
		internal bool DisabledMessagesCleared(NextHopSolutionKey queue)
		{
			lock (this.syncRoot)
			{
				QueueWaitList.QueueWeight queueWeight;
				if (this.queues.TryGetValue(queue, out queueWeight))
				{
					this.tracer.TraceWarning(0L, "Removing items may make the map out of sync with the queues");
					if (queueWeight.DisabledMessagesCleared())
					{
						return this.RemoveAndCleanupIfLast(queue);
					}
				}
			}
			return false;
		}

		// Token: 0x060023A5 RID: 9125 RVA: 0x00087A2C File Offset: 0x00085C2C
		internal bool CompleteItem(NextHopSolutionKey queue)
		{
			lock (this.syncRoot)
			{
				QueueWaitList.QueueWeight queueWeight;
				if (!this.queues.TryGetValue(queue, out queueWeight))
				{
					throw new KeyNotFoundException("The specified queue name was not found");
				}
				bool flag2 = queueWeight.CompleteItem();
				this.pendingMessageCount--;
				if (flag2)
				{
					return this.RemoveAndCleanupIfLast(queue);
				}
			}
			return false;
		}

		// Token: 0x060023A6 RID: 9126 RVA: 0x00087AB8 File Offset: 0x00085CB8
		internal bool Cleanup(NextHopSolutionKey queue)
		{
			lock (this.syncRoot)
			{
				if (this.queues.ContainsKey(queue))
				{
					this.tracer.TraceWarning(0L, "Removing a queue that could have pending tokens");
					bool result = this.RemoveAndCleanupIfLast(queue);
					this.messageCount = this.queues.Values.Sum((QueueWaitList.QueueWeight q) => q.MessageCount);
					this.pendingMessageCount = this.queues.Values.Sum((QueueWaitList.QueueWeight q) => q.PendingMessageCount);
					return result;
				}
			}
			return this.queues.Count == 0;
		}

		// Token: 0x060023A7 RID: 9127 RVA: 0x00087BA0 File Offset: 0x00085DA0
		internal bool CleanupItem(NextHopSolutionKey queue)
		{
			bool result;
			lock (this.syncRoot)
			{
				QueueWaitList.QueueWeight queueWeight;
				if (this.queues.TryGetValue(queue, out queueWeight))
				{
					bool flag2 = queueWeight.CleanupItem();
					this.messageCount = this.queues.Values.Sum((QueueWaitList.QueueWeight q) => q.MessageCount);
					if (flag2)
					{
						return this.RemoveAndCleanupIfLast(queue);
					}
				}
				result = (this.queues.Count == 0);
			}
			return result;
		}

		// Token: 0x060023A8 RID: 9128 RVA: 0x00087C44 File Offset: 0x00085E44
		internal NextHopSolutionKey[] Clear()
		{
			if (this.queues.Count == 0)
			{
				return null;
			}
			NextHopSolutionKey[] result;
			lock (this.syncRoot)
			{
				NextHopSolutionKey[] array = new NextHopSolutionKey[this.queues.Count];
				this.queues.Keys.CopyTo(array, 0);
				this.queues.Clear();
				this.state = WaitListState.Deleted;
				this.messageCount = 0;
				this.pendingMessageCount = 0;
				result = array;
			}
			return result;
		}

		// Token: 0x060023A9 RID: 9129 RVA: 0x00087CD4 File Offset: 0x00085ED4
		internal void Reset()
		{
			lock (this.syncRoot)
			{
				this.state = WaitListState.Live;
				this.Clear();
			}
		}

		// Token: 0x060023AA RID: 9130 RVA: 0x00087D1C File Offset: 0x00085F1C
		private bool RemoveAndCleanupIfLast(NextHopSolutionKey queue)
		{
			this.queues.Remove(queue);
			if (this.queues.Count == 0)
			{
				this.state = WaitListState.Deleted;
				return true;
			}
			return false;
		}

		// Token: 0x04001276 RID: 4726
		private Dictionary<NextHopSolutionKey, QueueWaitList.QueueWeight> queues;

		// Token: 0x04001277 RID: 4727
		private Trace tracer;

		// Token: 0x04001278 RID: 4728
		private object syncRoot = new object();

		// Token: 0x04001279 RID: 4729
		private WaitListState state;

		// Token: 0x0400127A RID: 4730
		private int messageCount;

		// Token: 0x0400127B RID: 4731
		private int pendingMessageCount;

		// Token: 0x0200033A RID: 826
		private class QueueWeight
		{
			// Token: 0x060023B0 RID: 9136 RVA: 0x00087D42 File Offset: 0x00085F42
			public QueueWeight(NextHopSolutionKey queue)
			{
				this.queue = queue;
				this.messageCount = 1;
			}

			// Token: 0x17000B2D RID: 2861
			// (get) Token: 0x060023B1 RID: 9137 RVA: 0x00087D58 File Offset: 0x00085F58
			public NextHopSolutionKey Name
			{
				get
				{
					return this.queue;
				}
			}

			// Token: 0x17000B2E RID: 2862
			// (get) Token: 0x060023B2 RID: 9138 RVA: 0x00087D60 File Offset: 0x00085F60
			public int MessageCount
			{
				get
				{
					return this.messageCount;
				}
			}

			// Token: 0x17000B2F RID: 2863
			// (get) Token: 0x060023B3 RID: 9139 RVA: 0x00087D68 File Offset: 0x00085F68
			public int PendingMessageCount
			{
				get
				{
					return this.outstandingItems;
				}
			}

			// Token: 0x17000B30 RID: 2864
			// (get) Token: 0x060023B4 RID: 9140 RVA: 0x00087D70 File Offset: 0x00085F70
			public bool HasOlderMessages
			{
				get
				{
					return this.messageCount > 0 || this.outstandingItems > 0;
				}
			}

			// Token: 0x17000B31 RID: 2865
			// (get) Token: 0x060023B5 RID: 9141 RVA: 0x00087D86 File Offset: 0x00085F86
			public bool HasDisabledMessages
			{
				get
				{
					return this.hasDisabledMessages;
				}
			}

			// Token: 0x060023B6 RID: 9142 RVA: 0x00087D90 File Offset: 0x00085F90
			public override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.AppendFormat("Queue {0}: messageCount={1}, tokens={2}", this.Name, this.messageCount, this.outstandingItems);
				return stringBuilder.ToString();
			}

			// Token: 0x060023B7 RID: 9143 RVA: 0x00087DD6 File Offset: 0x00085FD6
			internal void Add()
			{
				this.messageCount++;
			}

			// Token: 0x060023B8 RID: 9144 RVA: 0x00087DE6 File Offset: 0x00085FE6
			internal bool ProvisionallyRemove()
			{
				if (this.messageCount == 0)
				{
					return false;
				}
				this.outstandingItems++;
				this.provisionallyRemovedItems++;
				this.messageCount--;
				return true;
			}

			// Token: 0x060023B9 RID: 9145 RVA: 0x00087E1D File Offset: 0x0008601D
			internal bool ConfirmRemove()
			{
				if (this.provisionallyRemovedItems <= 0)
				{
					throw new InvalidOperationException("Cannot update an item without first provisionally removing it");
				}
				this.provisionallyRemovedItems--;
				return this.IsEmpty();
			}

			// Token: 0x060023BA RID: 9146 RVA: 0x00087E47 File Offset: 0x00086047
			internal bool RemoveWaitingAndOneOutstanding()
			{
				this.messageCount = 0;
				this.provisionallyRemovedItems--;
				this.outstandingItems--;
				return this.IsEmpty();
			}

			// Token: 0x060023BB RID: 9147 RVA: 0x00087E72 File Offset: 0x00086072
			internal bool CompleteItem()
			{
				if (this.outstandingItems <= 0)
				{
					throw new InvalidOperationException("There is no pending item to activate. UpdateItemActivated should be called after GetNextQueue");
				}
				this.outstandingItems--;
				return this.IsEmpty();
			}

			// Token: 0x060023BC RID: 9148 RVA: 0x00087E9C File Offset: 0x0008609C
			internal bool CleanupItem()
			{
				if (this.messageCount > 0)
				{
					this.messageCount--;
				}
				return this.IsEmpty();
			}

			// Token: 0x060023BD RID: 9149 RVA: 0x00087EBB File Offset: 0x000860BB
			internal void MoveToDisabled()
			{
				if (this.messageCount > 0)
				{
					this.messageCount--;
				}
				this.hasDisabledMessages = true;
			}

			// Token: 0x060023BE RID: 9150 RVA: 0x00087EDB File Offset: 0x000860DB
			internal bool DisabledMessagesCleared()
			{
				this.hasDisabledMessages = false;
				return this.IsEmpty();
			}

			// Token: 0x060023BF RID: 9151 RVA: 0x00087EEA File Offset: 0x000860EA
			private bool IsEmpty()
			{
				return this.outstandingItems == 0 && this.messageCount == 0 && this.provisionallyRemovedItems == 0 && !this.hasDisabledMessages;
			}

			// Token: 0x04001282 RID: 4738
			private NextHopSolutionKey queue;

			// Token: 0x04001283 RID: 4739
			private int messageCount;

			// Token: 0x04001284 RID: 4740
			private int outstandingItems;

			// Token: 0x04001285 RID: 4741
			private int provisionallyRemovedItems;

			// Token: 0x04001286 RID: 4742
			private bool hasDisabledMessages;
		}
	}
}
