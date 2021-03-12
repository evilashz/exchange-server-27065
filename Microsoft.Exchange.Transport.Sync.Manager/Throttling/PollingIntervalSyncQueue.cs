using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;

namespace Microsoft.Exchange.Transport.Sync.Manager.Throttling
{
	// Token: 0x02000050 RID: 80
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PollingIntervalSyncQueue<T> : SyncQueue<T>
	{
		// Token: 0x060003CD RID: 973 RVA: 0x00018078 File Offset: 0x00016278
		public PollingIntervalSyncQueue(int capacity, SyncLogSession syncLogSession)
		{
			SyncUtilities.ThrowIfArgumentLessThanZero("capacity", capacity);
			this.syncLogSession = syncLogSession;
			this.pollingSyncQueuesPriorityList = new SortedList<SyncQueueEntry<WorkType>, WorkType>(2, new PollingIntervalSyncQueue<T>.SyncQueueItemComparer());
			this.pollingSyncQueues = new Dictionary<WorkType, SortedQueue<SyncQueueEntry<T>>>(2);
			this.capacity = capacity;
			this.count = 0;
		}

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x060003CE RID: 974 RVA: 0x000180C8 File Offset: 0x000162C8
		public override int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x060003CF RID: 975 RVA: 0x000180D0 File Offset: 0x000162D0
		protected SyncLogSession SyncLogSession
		{
			get
			{
				return this.syncLogSession;
			}
		}

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x060003D0 RID: 976 RVA: 0x000180D8 File Offset: 0x000162D8
		protected SortedList<SyncQueueEntry<WorkType>, WorkType> PollingSyncQueuesPriorityList
		{
			get
			{
				return this.pollingSyncQueuesPriorityList;
			}
		}

		// Token: 0x060003D1 RID: 977 RVA: 0x000180E0 File Offset: 0x000162E0
		public override void Clear()
		{
			this.pollingSyncQueuesPriorityList.Clear();
			this.pollingSyncQueues.Clear();
			this.count = 0;
		}

		// Token: 0x060003D2 RID: 978 RVA: 0x00018100 File Offset: 0x00016300
		public override void EnqueueAtFront(T item, WorkType workType)
		{
			SortedQueue<SyncQueueEntry<T>> sortedQueueFromWorkType = this.GetSortedQueueFromWorkType(workType);
			ExDateTime exDateTime = ExDateTime.UtcNow;
			if (!sortedQueueFromWorkType.IsEmpty())
			{
				ExDateTime nextPollingTime = sortedQueueFromWorkType.Peek().NextPollingTime;
				if (nextPollingTime <= exDateTime)
				{
					exDateTime = nextPollingTime.AddMilliseconds(-1.0);
				}
			}
			this.Enqueue(item, workType, exDateTime);
		}

		// Token: 0x060003D3 RID: 979 RVA: 0x00018154 File Offset: 0x00016354
		public override void Enqueue(T item, WorkType workType, ExDateTime nextPollingTime)
		{
			SyncUtilities.ThrowIfArgumentNull("item", item);
			SortedQueue<SyncQueueEntry<T>> sortedQueueFromWorkType = this.GetSortedQueueFromWorkType(workType);
			SyncQueueEntry<T> item2 = new SyncQueueEntry<T>(item, nextPollingTime);
			if (sortedQueueFromWorkType.Count == 0)
			{
				sortedQueueFromWorkType.Enqueue(item2);
				this.pollingSyncQueuesPriorityList.Add(new SyncQueueEntry<WorkType>(workType, nextPollingTime), workType);
			}
			else
			{
				SyncQueueEntry<T> syncQueueEntry = sortedQueueFromWorkType.Peek();
				sortedQueueFromWorkType.Enqueue(item2);
				if (syncQueueEntry.CompareTo(sortedQueueFromWorkType.Peek()) != 0)
				{
					this.RebuildPriorityQueue();
				}
			}
			this.count++;
		}

		// Token: 0x060003D4 RID: 980 RVA: 0x000181D5 File Offset: 0x000163D5
		public override T Dequeue(WorkType workType)
		{
			base.ThrowIfQueueEmpty();
			return this.DequeueFromWorkQueue(workType);
		}

		// Token: 0x060003D5 RID: 981 RVA: 0x000181E4 File Offset: 0x000163E4
		public override T Peek(ExDateTime currentTime)
		{
			base.ThrowIfQueueEmpty();
			return this.InternalPeek(currentTime).Item;
		}

		// Token: 0x060003D6 RID: 982 RVA: 0x000181F8 File Offset: 0x000163F8
		public override bool IsEmpty()
		{
			return this.count == 0;
		}

		// Token: 0x060003D7 RID: 983 RVA: 0x00018203 File Offset: 0x00016403
		public override ExDateTime NextPollingTime(ExDateTime currentTime)
		{
			base.ThrowIfQueueEmpty();
			return this.InternalPeek(currentTime).NextPollingTime;
		}

		// Token: 0x060003D8 RID: 984 RVA: 0x00018218 File Offset: 0x00016418
		public IList<WorkType> GetDueWorkTypesByNextPollingTime(ExDateTime currentTime)
		{
			List<WorkType> list = new List<WorkType>(this.PollingSyncQueuesPriorityList.Count);
			for (int i = 0; i < this.pollingSyncQueuesPriorityList.Count; i++)
			{
				WorkType item = this.pollingSyncQueuesPriorityList.Keys[i].Item;
				if (this.IsWorkDue(currentTime, item))
				{
					list.Add(item);
				}
			}
			return list;
		}

		// Token: 0x060003D9 RID: 985 RVA: 0x00018278 File Offset: 0x00016478
		public bool IsWorkDue(ExDateTime currentTime, WorkType workType)
		{
			if (!this.pollingSyncQueues.ContainsKey(workType) || this.pollingSyncQueues[workType].IsEmpty())
			{
				return false;
			}
			ExDateTime nextPollingTime = this.pollingSyncQueues[workType].Peek().NextPollingTime;
			if (nextPollingTime <= currentTime)
			{
				return true;
			}
			this.syncLogSession.LogDebugging((TSLID)748UL, "NextPollingTime {0}, CurrentTime {1}.", new object[]
			{
				nextPollingTime,
				currentTime
			});
			return false;
		}

		// Token: 0x060003DA RID: 986 RVA: 0x00018300 File Offset: 0x00016500
		internal void AddDatabaseDiagnosticInfoTo(XElement parentElement, SyncDiagnosticMode mode)
		{
			XElement xelement = new XElement("DatabasePollingQueues");
			foreach (WorkType workType in this.pollingSyncQueues.Keys)
			{
				XElement xelement2 = new XElement("PollingQueue");
				SortedQueue<SyncQueueEntry<T>> sortedQueue = this.pollingSyncQueues[workType];
				ExDateTime? exDateTime = null;
				if (!sortedQueue.IsEmpty())
				{
					exDateTime = new ExDateTime?(sortedQueue.Peek().NextPollingTime);
				}
				xelement2.Add(new XElement("nextPollingTime", (exDateTime != null) ? exDateTime.Value.ToString("o") : string.Empty));
				WorkTypeDefinition workTypeDefinition = WorkTypeManager.Instance.GetWorkTypeDefinition(workType);
				workTypeDefinition.AddDiagnosticInfoTo(xelement2);
				xelement.Add(xelement2);
				if (mode == SyncDiagnosticMode.Info)
				{
					this.AddAdditionalWorkTypeDiagnosticInfoTo(xelement2, workType, workTypeDefinition);
				}
				if (mode == SyncDiagnosticMode.Verbose)
				{
					XElement xelement3 = new XElement("SubscriptionPriorityList");
					foreach (SyncQueueEntry<WorkType> syncQueueEntry in this.pollingSyncQueuesPriorityList.Keys)
					{
						xelement3.Add(new XElement("workType", syncQueueEntry.Item.ToString()));
					}
					xelement2.Add(xelement3);
				}
			}
			parentElement.Add(xelement);
		}

		// Token: 0x060003DB RID: 987 RVA: 0x000184B0 File Offset: 0x000166B0
		internal void AddSubscriptionDiagnosticInfoTo(XElement parentElement, SyncDiagnosticMode mode)
		{
			if (mode != SyncDiagnosticMode.Verbose)
			{
				return;
			}
			XElement xelement = new XElement("SubscriptionInstancesInQueue");
			foreach (WorkType workType in this.pollingSyncQueues.Keys)
			{
				SortedQueue<SyncQueueEntry<T>> sortedQueue = this.pollingSyncQueues[workType];
				foreach (SyncQueueEntry<T> syncQueueEntry in sortedQueue)
				{
					XElement xelement2 = new XElement("SubscriptionInstance");
					syncQueueEntry.AddDiagnosticInfoTo(xelement2, "subscriptionId");
					xelement2.Add(new XElement("workType", workType.ToString()));
					xelement.Add(xelement2);
				}
				parentElement.Add(xelement);
			}
		}

		// Token: 0x060003DC RID: 988 RVA: 0x000185B0 File Offset: 0x000167B0
		protected virtual void AddAdditionalWorkTypeDiagnosticInfoTo(XElement parentElement, WorkType workType, WorkTypeDefinition workTypeDefinition)
		{
			int num = 0;
			ExDateTime utcNow = ExDateTime.UtcNow;
			SortedQueue<SyncQueueEntry<T>> sortedQueue = this.pollingSyncQueues[workType];
			foreach (SyncQueueEntry<T> syncQueueEntry in sortedQueue)
			{
				ExDateTime nextPollingTime = syncQueueEntry.NextPollingTime;
				if (ExDateTime.Compare(utcNow, nextPollingTime, workTypeDefinition.TimeTillSyncDue) == 0)
				{
					break;
				}
				num++;
			}
			parentElement.Add(new XElement("itemsOutOfSla", num));
			parentElement.Add(new XElement("count", sortedQueue.Count));
			int num2 = 0;
			if (num != 0)
			{
				num2 = num / sortedQueue.Count * 100;
			}
			parentElement.Add(new XElement("itemsOutOfSlaPercent", num2));
		}

		// Token: 0x060003DD RID: 989 RVA: 0x00018694 File Offset: 0x00016894
		protected T DequeueFromWorkQueue(WorkType workType)
		{
			this.pollingSyncQueuesPriorityList.RemoveAt(this.pollingSyncQueuesPriorityList.IndexOfValue(workType));
			SortedQueue<SyncQueueEntry<T>> sortedQueue = this.pollingSyncQueues[workType];
			SyncQueueEntry<T> syncQueueEntry = sortedQueue.Dequeue();
			if (sortedQueue.Count > 0)
			{
				this.pollingSyncQueuesPriorityList.Add(new SyncQueueEntry<WorkType>(workType, sortedQueue.Peek().NextPollingTime), workType);
			}
			this.count--;
			return syncQueueEntry.Item;
		}

		// Token: 0x060003DE RID: 990 RVA: 0x00018708 File Offset: 0x00016908
		protected virtual SortedQueue<SyncQueueEntry<T>> GetSortedQueueFromWorkType(WorkType workType)
		{
			SortedQueue<SyncQueueEntry<T>> sortedQueue;
			if (!this.pollingSyncQueues.TryGetValue(workType, out sortedQueue))
			{
				sortedQueue = new SortedQueue<SyncQueueEntry<T>>(this.capacity);
				this.pollingSyncQueues.Add(workType, sortedQueue);
			}
			return sortedQueue;
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0001873F File Offset: 0x0001693F
		protected virtual SyncQueueEntry<T> InternalPeek(ExDateTime currentTime)
		{
			return this.pollingSyncQueues[this.pollingSyncQueuesPriorityList.Keys[0].Item].Peek();
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x00018768 File Offset: 0x00016968
		private void RebuildPriorityQueue()
		{
			this.pollingSyncQueuesPriorityList.Clear();
			foreach (WorkType workType in this.pollingSyncQueues.Keys)
			{
				SortedQueue<SyncQueueEntry<T>> sortedQueue = this.pollingSyncQueues[workType];
				if (sortedQueue.Count > 0)
				{
					this.pollingSyncQueuesPriorityList.Add(new SyncQueueEntry<WorkType>(workType, sortedQueue.Peek().NextPollingTime), workType);
				}
			}
		}

		// Token: 0x04000224 RID: 548
		private const int DefaultPollingIntervalCount = 2;

		// Token: 0x04000225 RID: 549
		private readonly int capacity;

		// Token: 0x04000226 RID: 550
		private readonly SortedList<SyncQueueEntry<WorkType>, WorkType> pollingSyncQueuesPriorityList;

		// Token: 0x04000227 RID: 551
		private readonly Dictionary<WorkType, SortedQueue<SyncQueueEntry<T>>> pollingSyncQueues;

		// Token: 0x04000228 RID: 552
		private int count;

		// Token: 0x04000229 RID: 553
		private SyncLogSession syncLogSession;

		// Token: 0x02000051 RID: 81
		private class SyncQueueItemComparer : IComparer<SyncQueueEntry<WorkType>>
		{
			// Token: 0x060003E1 RID: 993 RVA: 0x000187F8 File Offset: 0x000169F8
			public int Compare(SyncQueueEntry<WorkType> syncQueueEntryOne, SyncQueueEntry<WorkType> syncQueueEntryTwo)
			{
				SyncUtilities.ThrowIfArgumentNull("syncQueueEntryOne", syncQueueEntryOne);
				SyncUtilities.ThrowIfArgumentNull("syncQueueEntryTwo", syncQueueEntryTwo);
				if (syncQueueEntryOne.Item == syncQueueEntryTwo.Item)
				{
					return 0;
				}
				int num = syncQueueEntryOne.CompareTo(syncQueueEntryTwo);
				if (num == 0)
				{
					num = -1;
				}
				return num;
			}
		}
	}
}
