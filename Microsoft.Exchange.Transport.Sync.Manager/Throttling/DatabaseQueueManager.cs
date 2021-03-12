using System;
using System.Collections.Generic;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;

namespace Microsoft.Exchange.Transport.Sync.Manager.Throttling
{
	// Token: 0x02000044 RID: 68
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DatabaseQueueManager
	{
		// Token: 0x0600035E RID: 862 RVA: 0x000166A4 File Offset: 0x000148A4
		public DatabaseQueueManager(Guid databaseGuid, SyncLogSession syncLogSession)
		{
			SyncUtilities.ThrowIfGuidEmpty("databaseGuid", databaseGuid);
			SyncUtilities.ThrowIfArgumentNull("syncLogSession", syncLogSession);
			this.syncLogSession = syncLogSession;
			this.databaseGuid = databaseGuid;
			this.pollingIntervalSyncQueue = new PollingIntervalSyncQueue<Guid>(1000, this.syncLogSession);
			this.subscriptionList = new Dictionary<Guid, DatabaseQueueManager.SubscriptionQueueEntry>(15000);
			this.count = 0;
		}

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x0600035F RID: 863 RVA: 0x00016718 File Offset: 0x00014918
		// (remove) Token: 0x06000360 RID: 864 RVA: 0x0001675C File Offset: 0x0001495C
		public event EventHandler<SyncQueueEventArgs> SubscriptionAddedOrRemovedEvent
		{
			add
			{
				lock (this.syncRoot)
				{
					this.InternalSubscriptionAddedOrRemovedEvent += value;
				}
			}
			remove
			{
				lock (this.syncRoot)
				{
					this.InternalSubscriptionAddedOrRemovedEvent -= value;
				}
			}
		}

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x06000361 RID: 865 RVA: 0x000167A0 File Offset: 0x000149A0
		// (remove) Token: 0x06000362 RID: 866 RVA: 0x000167E4 File Offset: 0x000149E4
		public event EventHandler<SyncQueueEventArgs> SubscriptionEnqueuedOrDequeuedEvent
		{
			add
			{
				lock (this.syncRoot)
				{
					this.InternalSubscriptionEnqueuedOrDequeuedEvent += value;
				}
			}
			remove
			{
				lock (this.syncRoot)
				{
					this.InternalSubscriptionEnqueuedOrDequeuedEvent -= value;
				}
			}
		}

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x06000363 RID: 867 RVA: 0x00016828 File Offset: 0x00014A28
		// (remove) Token: 0x06000364 RID: 868 RVA: 0x00016860 File Offset: 0x00014A60
		private event EventHandler<SyncQueueEventArgs> InternalSubscriptionAddedOrRemovedEvent;

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x06000365 RID: 869 RVA: 0x00016898 File Offset: 0x00014A98
		// (remove) Token: 0x06000366 RID: 870 RVA: 0x000168D0 File Offset: 0x00014AD0
		private event EventHandler<SyncQueueEventArgs> InternalSubscriptionEnqueuedOrDequeuedEvent;

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000367 RID: 871 RVA: 0x00016905 File Offset: 0x00014B05
		public Guid DatabaseGuid
		{
			get
			{
				return this.databaseGuid;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000368 RID: 872 RVA: 0x0001690D File Offset: 0x00014B0D
		public int Count
		{
			get
			{
				return this.count;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000369 RID: 873 RVA: 0x00016917 File Offset: 0x00014B17
		public int SubscriptionCount
		{
			get
			{
				return this.subscriptionList.Count;
			}
		}

		// Token: 0x0600036A RID: 874 RVA: 0x00016924 File Offset: 0x00014B24
		public bool IsEmpty()
		{
			return this.pollingIntervalSyncQueue.IsEmpty();
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00016934 File Offset: 0x00014B34
		public void Clear()
		{
			lock (this.syncRoot)
			{
				this.pollingIntervalSyncQueue.Clear();
				this.subscriptionList.Clear();
				this.count = 0;
			}
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00016990 File Offset: 0x00014B90
		public bool Add(ISubscriptionInformation subscriptionInformation)
		{
			SyncUtilities.ThrowIfArgumentNull("subscriptionInformation", subscriptionInformation);
			SyncLogSession syncLogSession = this.syncLogSession.OpenWithContext(subscriptionInformation.MailboxGuid, subscriptionInformation.SubscriptionGuid);
			bool result;
			lock (this.syncRoot)
			{
				DatabaseQueueManager.SubscriptionQueueEntry value = new DatabaseQueueManager.SubscriptionQueueEntry(new MiniSubscriptionInformation(subscriptionInformation.ExternalDirectoryOrgId, subscriptionInformation.DatabaseGuid, subscriptionInformation.MailboxGuid, subscriptionInformation.SubscriptionGuid, subscriptionInformation.SubscriptionType, ExDateTime.UtcNow));
				DatabaseQueueManager.SubscriptionQueueEntry subscriptionQueueEntry;
				if (!this.subscriptionList.TryGetValue(subscriptionInformation.SubscriptionGuid, out subscriptionQueueEntry))
				{
					this.subscriptionList.Add(subscriptionInformation.SubscriptionGuid, value);
					this.RaiseSubscriptionAddedEvent();
					syncLogSession.LogDebugging((TSLID)461UL, "DQM.Add added subscription", new object[0]);
					result = true;
				}
				else
				{
					syncLogSession.LogDebugging((TSLID)463UL, "DQM.Add subscription already added, not adding now", new object[0]);
					result = false;
				}
			}
			return result;
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00016A8C File Offset: 0x00014C8C
		public void Remove(Guid subscriptionGuid)
		{
			SyncUtilities.ThrowIfGuidEmpty("subscriptionGuid", subscriptionGuid);
			lock (this.syncRoot)
			{
				DatabaseQueueManager.SubscriptionQueueEntry subscriptionQueueEntry = null;
				if (this.subscriptionList.TryGetValue(subscriptionGuid, out subscriptionQueueEntry))
				{
					this.subscriptionList.Remove(subscriptionGuid);
					this.RaiseSubscriptionRemovedEvent();
					SyncLogSession syncLogSession = this.syncLogSession.OpenWithContext(subscriptionGuid);
					syncLogSession.LogDebugging((TSLID)484UL, "DQM.Remove removed subscription", new object[0]);
				}
			}
		}

		// Token: 0x0600036E RID: 878 RVA: 0x00016B24 File Offset: 0x00014D24
		public bool TryFindSubscription(Guid subscriptionGuid, out HashSet<WorkType> queuedWorkTypes)
		{
			SyncUtilities.ThrowIfGuidEmpty("subscriptionGuid", subscriptionGuid);
			queuedWorkTypes = null;
			lock (this.syncRoot)
			{
				DatabaseQueueManager.SubscriptionQueueEntry subscriptionQueueEntry = null;
				if (this.subscriptionList.TryGetValue(subscriptionGuid, out subscriptionQueueEntry))
				{
					queuedWorkTypes = subscriptionQueueEntry.GetCopyOfQueuedWorkTypes();
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00016B90 File Offset: 0x00014D90
		public bool EnqueueAtFront(WorkType workType, Guid subscriptionGuid)
		{
			SyncUtilities.ThrowIfGuidEmpty("subscriptionGuid", subscriptionGuid);
			SyncLogSession syncLogSession = this.syncLogSession.OpenWithContext(subscriptionGuid);
			syncLogSession.LogDebugging((TSLID)493UL, "DQM.EnqueueAtFront", new object[0]);
			return this.InternalEnqueue(workType, subscriptionGuid, null, true);
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00016BE4 File Offset: 0x00014DE4
		public bool Enqueue(WorkType workType, Guid subscriptionGuid, ExDateTime nextDispatchTime)
		{
			SyncUtilities.ThrowIfGuidEmpty("subscriptionGuid", subscriptionGuid);
			SyncLogSession syncLogSession = this.syncLogSession.OpenWithContext(subscriptionGuid);
			syncLogSession.LogDebugging((TSLID)494UL, "DQM.Enqueue in DB {0} WorkType {1} with nextDispatchTime {2}", new object[]
			{
				this.databaseGuid,
				workType,
				nextDispatchTime
			});
			return this.InternalEnqueue(workType, subscriptionGuid, new ExDateTime?(nextDispatchTime), false);
		}

		// Token: 0x06000371 RID: 881 RVA: 0x00016C58 File Offset: 0x00014E58
		public ExDateTime? NextPollingTime(ExDateTime currentTime)
		{
			ExDateTime? result;
			lock (this.syncRoot)
			{
				if (!this.pollingIntervalSyncQueue.IsEmpty())
				{
					result = new ExDateTime?(this.pollingIntervalSyncQueue.NextPollingTime(currentTime));
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x06000372 RID: 882 RVA: 0x00016CC0 File Offset: 0x00014EC0
		public IList<WorkType> GetDueWorkTypesByNextPollingTime(ExDateTime currentTime)
		{
			IList<WorkType> result;
			lock (this.syncRoot)
			{
				if (!this.pollingIntervalSyncQueue.IsEmpty())
				{
					result = this.pollingIntervalSyncQueue.GetDueWorkTypesByNextPollingTime(currentTime);
				}
				else
				{
					result = null;
				}
			}
			return result;
		}

		// Token: 0x06000373 RID: 883 RVA: 0x00016D1C File Offset: 0x00014F1C
		public bool TryDequeue(ExDateTime currentTime, WorkType workType, out MiniSubscriptionInformation miniSubscriptionInformation, out ExDateTime dueTimeOfNextSubscription)
		{
			miniSubscriptionInformation = null;
			dueTimeOfNextSubscription = ExDateTime.UtcNow;
			bool result;
			lock (this.syncRoot)
			{
				if (this.pollingIntervalSyncQueue.IsWorkDue(currentTime, workType))
				{
					this.syncLogSession.LogDebugging((TSLID)495UL, "DQM.TryDequeue Dequeuing from polling interval queue", new object[0]);
					dueTimeOfNextSubscription = this.pollingIntervalSyncQueue.NextPollingTime(currentTime);
					Guid guid = this.pollingIntervalSyncQueue.Dequeue(workType);
					TimeSpan timeSpan = currentTime - dueTimeOfNextSubscription;
					SyncLogSession syncLogSession = this.syncLogSession.OpenWithContext(guid);
					syncLogSession.LogDebugging((TSLID)1285UL, "DQM.TryDequeue Time Since Due:{0}", new object[]
					{
						timeSpan
					});
					this.count--;
					WorkTypeDefinition workTypeDefinition = WorkTypeManager.Instance.GetWorkTypeDefinition(workType);
					this.RaiseSubscriptionDequeuedEvent(workTypeDefinition.TimeTillSyncDue);
					DatabaseQueueManager.SubscriptionQueueEntry subscriptionQueueEntry = null;
					if (this.subscriptionList.TryGetValue(guid, out subscriptionQueueEntry))
					{
						subscriptionQueueEntry.UnMarkWorkTypeQueued(workType);
						miniSubscriptionInformation = subscriptionQueueEntry.MiniSubscriptionInformation;
						result = true;
					}
					else
					{
						syncLogSession.LogDebugging((TSLID)1286UL, "DQM.TryDequeue Subscription no longer in DQM, failing dequeue", new object[0]);
						result = false;
					}
				}
				else
				{
					result = false;
				}
			}
			return result;
		}

		// Token: 0x06000374 RID: 884 RVA: 0x00016E88 File Offset: 0x00015088
		internal XElement GetDiagnosticInfo(SyncDiagnosticMode mode)
		{
			XElement xelement = new XElement("DatabaseQueueManager");
			lock (this.syncRoot)
			{
				xelement.Add(new XElement("databaseId", this.databaseGuid));
				xelement.Add(new XElement("nextPollingTime", (!this.pollingIntervalSyncQueue.IsEmpty()) ? this.pollingIntervalSyncQueue.NextPollingTime(ExDateTime.UtcNow).ToString("o") : string.Empty));
				XElement xelement2 = new XElement("Counts");
				xelement2.Add(new XElement("subscriptionList", this.subscriptionList.Count));
				xelement2.Add(new XElement("subscriptionInstancesInQueues", this.Count));
				xelement.Add(xelement2);
				this.pollingIntervalSyncQueue.AddDatabaseDiagnosticInfoTo(xelement, mode);
				this.pollingIntervalSyncQueue.AddSubscriptionDiagnosticInfoTo(xelement, mode);
			}
			return xelement;
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00016FB0 File Offset: 0x000151B0
		private bool InternalEnqueue(WorkType workType, Guid subscriptionGuid, ExDateTime? nextDispatchTime, bool enqueueAtFront)
		{
			if ((!enqueueAtFront && nextDispatchTime == null) || (enqueueAtFront && nextDispatchTime != null))
			{
				throw new ArgumentException("If we aren't enqueueing at the front, we must have a next dispatch time");
			}
			SyncLogSession syncLogSession = this.syncLogSession.OpenWithContext(subscriptionGuid);
			lock (this.syncRoot)
			{
				DatabaseQueueManager.SubscriptionQueueEntry subscriptionQueueEntry;
				if (!this.subscriptionList.TryGetValue(subscriptionGuid, out subscriptionQueueEntry))
				{
					syncLogSession.LogDebugging((TSLID)430UL, "DQM.Enqueue: Subscription is not managed by DQM, will not be enqueued.", new object[0]);
					return false;
				}
				WorkTypeDefinition workTypeDefinition = WorkTypeManager.Instance.GetWorkTypeDefinition(workType);
				if (!subscriptionQueueEntry.MarkWorkTypeQueued(workType))
				{
					syncLogSession.LogDebugging((TSLID)1292UL, "DQM.InternalEnqueue: {0} already enqueued.", new object[]
					{
						workType
					});
					return false;
				}
				if (enqueueAtFront)
				{
					this.pollingIntervalSyncQueue.EnqueueAtFront(subscriptionGuid, workType);
				}
				else
				{
					this.pollingIntervalSyncQueue.Enqueue(subscriptionGuid, workType, nextDispatchTime.Value);
				}
				this.count++;
				this.RaiseSubscriptionEnqueuedEvent(workTypeDefinition.TimeTillSyncDue);
			}
			return true;
		}

		// Token: 0x06000376 RID: 886 RVA: 0x000170E0 File Offset: 0x000152E0
		private void ThrowIfQueueEmpty()
		{
			if (this.IsEmpty())
			{
				throw new InvalidOperationException("Database Queue Manager is empty.");
			}
		}

		// Token: 0x06000377 RID: 887 RVA: 0x000170F5 File Offset: 0x000152F5
		private void RaiseSubscriptionAddedEvent()
		{
			if (this.InternalSubscriptionAddedOrRemovedEvent != null)
			{
				this.InternalSubscriptionAddedOrRemovedEvent(this, SyncQueueEventArgs.CreateSubscriptionAddedEventArgs(this.databaseGuid));
			}
		}

		// Token: 0x06000378 RID: 888 RVA: 0x00017116 File Offset: 0x00015316
		private void RaiseSubscriptionRemovedEvent()
		{
			if (this.InternalSubscriptionAddedOrRemovedEvent != null)
			{
				this.InternalSubscriptionAddedOrRemovedEvent(this, SyncQueueEventArgs.CreateSubscriptionRemovedEventArgs(this.databaseGuid));
			}
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00017137 File Offset: 0x00015337
		private void RaiseSubscriptionEnqueuedEvent(TimeSpan syncInterval)
		{
			if (this.InternalSubscriptionEnqueuedOrDequeuedEvent != null)
			{
				this.InternalSubscriptionEnqueuedOrDequeuedEvent(this, SyncQueueEventArgs.CreateSubscriptionEnqueuedEventArgs(this.databaseGuid, syncInterval));
			}
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00017159 File Offset: 0x00015359
		private void RaiseSubscriptionDequeuedEvent(TimeSpan syncInterval)
		{
			if (this.InternalSubscriptionEnqueuedOrDequeuedEvent != null)
			{
				this.InternalSubscriptionEnqueuedOrDequeuedEvent(this, SyncQueueEventArgs.CreateSubscriptionDequeuedEventArgs(this.databaseGuid, syncInterval));
			}
		}

		// Token: 0x040001DC RID: 476
		private const int DefaultNumberOfSubscriptionsPerMailboxServer = 15000;

		// Token: 0x040001DD RID: 477
		private const int DefaultNumberOfSyncsScheduledPerDatabase = 1000;

		// Token: 0x040001DE RID: 478
		private readonly object syncRoot = new object();

		// Token: 0x040001DF RID: 479
		private readonly Guid databaseGuid;

		// Token: 0x040001E0 RID: 480
		private readonly PollingIntervalSyncQueue<Guid> pollingIntervalSyncQueue;

		// Token: 0x040001E1 RID: 481
		private readonly Dictionary<Guid, DatabaseQueueManager.SubscriptionQueueEntry> subscriptionList;

		// Token: 0x040001E2 RID: 482
		private volatile int count;

		// Token: 0x040001E3 RID: 483
		private SyncLogSession syncLogSession;

		// Token: 0x02000045 RID: 69
		private sealed class SubscriptionQueueEntry
		{
			// Token: 0x0600037B RID: 891 RVA: 0x0001717B File Offset: 0x0001537B
			internal SubscriptionQueueEntry(MiniSubscriptionInformation miniSubscriptionInformation)
			{
				SyncUtilities.ThrowIfArgumentNull("miniSubscriptionInformation", miniSubscriptionInformation);
				this.miniSubscriptionInformation = miniSubscriptionInformation;
				this.queuedWorkTypes = new HashSet<WorkType>();
			}

			// Token: 0x17000100 RID: 256
			// (get) Token: 0x0600037C RID: 892 RVA: 0x000171A0 File Offset: 0x000153A0
			internal MiniSubscriptionInformation MiniSubscriptionInformation
			{
				get
				{
					return this.miniSubscriptionInformation;
				}
			}

			// Token: 0x0600037D RID: 893 RVA: 0x000171A8 File Offset: 0x000153A8
			internal HashSet<WorkType> GetCopyOfQueuedWorkTypes()
			{
				WorkType[] array = new WorkType[this.queuedWorkTypes.Count];
				this.queuedWorkTypes.CopyTo(array);
				return new HashSet<WorkType>(array);
			}

			// Token: 0x0600037E RID: 894 RVA: 0x000171D8 File Offset: 0x000153D8
			internal bool MarkWorkTypeQueued(WorkType workType)
			{
				if (this.queuedWorkTypes.Contains(workType))
				{
					return false;
				}
				this.queuedWorkTypes.Add(workType);
				return true;
			}

			// Token: 0x0600037F RID: 895 RVA: 0x000171F8 File Offset: 0x000153F8
			internal void UnMarkWorkTypeQueued(WorkType workType)
			{
				this.queuedWorkTypes.Remove(workType);
			}

			// Token: 0x040001E6 RID: 486
			private readonly MiniSubscriptionInformation miniSubscriptionInformation;

			// Token: 0x040001E7 RID: 487
			private HashSet<WorkType> queuedWorkTypes;
		}
	}
}
