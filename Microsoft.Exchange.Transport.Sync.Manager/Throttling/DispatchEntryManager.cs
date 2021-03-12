using System;
using System.Collections.Generic;
using System.Threading;
using System.Xml.Linq;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Threading;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.SyncHealthLog;

namespace Microsoft.Exchange.Transport.Sync.Manager.Throttling
{
	// Token: 0x02000048 RID: 72
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class DispatchEntryManager : DisposeTrackableBase, IDispatchEntryManager
	{
		// Token: 0x06000392 RID: 914 RVA: 0x000172E8 File Offset: 0x000154E8
		public DispatchEntryManager(SyncLogSession syncLogSession, ISyncHealthLog syncHealthLog, ISyncManagerConfiguration configuration)
		{
			this.syncLogSession = syncLogSession;
			this.expirationCheckFrequency = configuration.DispatchEntryExpirationCheckFrequency;
			this.timeToEntryExpiration = configuration.DispatchEntryExpirationTime;
			this.attemptingDispatch = new Dictionary<Guid, DispatchEntry>();
			this.dispatched = new Dictionary<Guid, DispatchEntry>();
			this.entriesPerDatabase = new Dictionary<Guid, int>();
			this.workTypeBudgetManager = new WorkTypeBudgetManager(this.syncLogSession, syncHealthLog, configuration);
			this.expirationTimer = new GuardedTimer(new TimerCallback(this.CleanExpiredItemsCallback), null, (int)this.expirationCheckFrequency.TotalMilliseconds, -1);
		}

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x06000393 RID: 915 RVA: 0x0001737E File Offset: 0x0001557E
		// (remove) Token: 0x06000394 RID: 916 RVA: 0x00017387 File Offset: 0x00015587
		public event EventHandler<DispatchEntry> EntryExpiredEvent
		{
			add
			{
				this.InternalEntryExpiredEvent += value;
			}
			remove
			{
				this.InternalEntryExpiredEvent -= value;
			}
		}

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06000395 RID: 917 RVA: 0x00017390 File Offset: 0x00015590
		// (remove) Token: 0x06000396 RID: 918 RVA: 0x000173C8 File Offset: 0x000155C8
		private event EventHandler<DispatchEntry> InternalEntryExpiredEvent;

		// Token: 0x06000397 RID: 919 RVA: 0x00017400 File Offset: 0x00015600
		public bool ContainsSubscription(Guid subscriptionGuid)
		{
			this.collectionLock.EnterReadLock();
			bool result;
			try
			{
				result = (this.attemptingDispatch.ContainsKey(subscriptionGuid) || this.dispatched.ContainsKey(subscriptionGuid));
			}
			finally
			{
				this.collectionLock.ExitReadLock();
			}
			return result;
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00017458 File Offset: 0x00015658
		public int GetNumberOfEntriesForDatabase(Guid databaseGuid)
		{
			this.collectionLock.EnterReadLock();
			int result;
			try
			{
				if (!this.entriesPerDatabase.ContainsKey(databaseGuid))
				{
					result = 0;
				}
				else
				{
					result = this.entriesPerDatabase[databaseGuid];
				}
			}
			finally
			{
				this.collectionLock.ExitReadLock();
			}
			return result;
		}

		// Token: 0x06000399 RID: 921 RVA: 0x000174B0 File Offset: 0x000156B0
		public bool HasBudget(WorkType workType)
		{
			return this.workTypeBudgetManager.HasBudget(workType);
		}

		// Token: 0x0600039A RID: 922 RVA: 0x000174C0 File Offset: 0x000156C0
		public void Add(DispatchEntry dispatchEntry)
		{
			this.collectionLock.EnterWriteLock();
			try
			{
				if (!this.entriesPerDatabase.ContainsKey(dispatchEntry.MiniSubscriptionInformation.DatabaseGuid))
				{
					this.entriesPerDatabase.Add(dispatchEntry.MiniSubscriptionInformation.DatabaseGuid, 1);
				}
				else
				{
					Dictionary<Guid, int> dictionary;
					Guid databaseGuid;
					(dictionary = this.entriesPerDatabase)[databaseGuid = dispatchEntry.MiniSubscriptionInformation.DatabaseGuid] = dictionary[databaseGuid] + 1;
				}
				this.attemptingDispatch.Add(dispatchEntry.MiniSubscriptionInformation.SubscriptionGuid, dispatchEntry);
			}
			finally
			{
				this.collectionLock.ExitWriteLock();
			}
		}

		// Token: 0x0600039B RID: 923 RVA: 0x00017564 File Offset: 0x00015764
		public DispatchEntry RemoveDispatchAttempt(Guid databaseGuid, Guid subscriptionGuid)
		{
			SyncUtilities.ThrowIfArgumentNull("databaseGuid", databaseGuid);
			SyncUtilities.ThrowIfArgumentNull("subscriptionGuid", subscriptionGuid);
			this.collectionLock.EnterWriteLock();
			DispatchEntry result;
			try
			{
				if (!this.attemptingDispatch.ContainsKey(subscriptionGuid))
				{
					throw new InvalidOperationException();
				}
				Dictionary<Guid, int> dictionary;
				(dictionary = this.entriesPerDatabase)[databaseGuid] = dictionary[databaseGuid] - 1;
				DispatchEntry dispatchEntry = this.attemptingDispatch[subscriptionGuid];
				this.attemptingDispatch.Remove(subscriptionGuid);
				result = dispatchEntry;
			}
			finally
			{
				this.collectionLock.ExitWriteLock();
			}
			return result;
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00017604 File Offset: 0x00015804
		public void MarkDispatchSuccess(Guid subscriptionGuid)
		{
			SyncUtilities.ThrowIfArgumentNull("subscriptionGuid", subscriptionGuid);
			this.collectionLock.EnterWriteLock();
			try
			{
				DispatchEntry dispatchEntry = this.attemptingDispatch[subscriptionGuid];
				this.attemptingDispatch.Remove(subscriptionGuid);
				this.dispatched.Add(subscriptionGuid, dispatchEntry);
				this.workTypeBudgetManager.Increment(dispatchEntry.WorkType);
			}
			finally
			{
				this.collectionLock.ExitWriteLock();
			}
		}

		// Token: 0x0600039D RID: 925 RVA: 0x00017684 File Offset: 0x00015884
		public bool TryRemoveDispatchedEntry(Guid subscriptionGuid, out DispatchEntry dispatchEntry)
		{
			SyncUtilities.ThrowIfArgumentNull("subscriptionGuid", subscriptionGuid);
			dispatchEntry = null;
			this.collectionLock.EnterWriteLock();
			bool result;
			try
			{
				if (this.dispatched.ContainsKey(subscriptionGuid))
				{
					dispatchEntry = this.RemoveDispatchedEntry(subscriptionGuid);
					result = true;
				}
				else
				{
					result = false;
				}
			}
			finally
			{
				this.collectionLock.ExitWriteLock();
			}
			return result;
		}

		// Token: 0x0600039E RID: 926 RVA: 0x000176EC File Offset: 0x000158EC
		public XElement GetDiagnosticInfo(SyncDiagnosticMode mode)
		{
			XElement xelement = new XElement("DispatchEntryManager");
			this.collectionLock.EnterReadLock();
			try
			{
				xelement.Add(new XElement("attemptingDispatchCount", this.attemptingDispatch.Count));
				xelement.Add(new XElement("dispatchedCount", this.dispatched.Count));
				XElement xelement2 = new XElement("EntriesPerDatabase");
				foreach (Guid guid in this.entriesPerDatabase.Keys)
				{
					XElement xelement3 = new XElement("Database");
					xelement3.Add(new XElement("databaseId", guid));
					xelement3.Add(new XElement("countOfEntries", this.entriesPerDatabase[guid]));
					xelement2.Add(xelement3);
				}
				xelement.Add(xelement2);
				xelement.Add(this.workTypeBudgetManager.GetDiagnosticInfo());
				if (mode == SyncDiagnosticMode.Verbose)
				{
					XElement xelement4 = new XElement("DispatchAttemptEntries");
					foreach (DispatchEntry dispatchEntry in this.attemptingDispatch.Values)
					{
						xelement4.Add(dispatchEntry.GetDiagnosticInfo());
					}
					xelement.Add(xelement4);
					XElement xelement5 = new XElement("DispatchedEntries");
					foreach (DispatchEntry dispatchEntry2 in this.dispatched.Values)
					{
						xelement5.Add(dispatchEntry2.GetDiagnosticInfo());
					}
					xelement.Add(xelement5);
				}
			}
			finally
			{
				this.collectionLock.ExitReadLock();
			}
			return xelement;
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00017948 File Offset: 0x00015B48
		public void DisabledExpiration()
		{
			this.expirationTimer.Change(-1, -1);
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00017958 File Offset: 0x00015B58
		protected override void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				if (this.expirationTimer != null)
				{
					this.expirationTimer.Dispose(true);
					this.expirationTimer = null;
				}
				if (this.workTypeBudgetManager != null)
				{
					this.workTypeBudgetManager.Dispose();
					this.workTypeBudgetManager = null;
				}
			}
		}

		// Token: 0x060003A1 RID: 929 RVA: 0x00017992 File Offset: 0x00015B92
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<DispatchEntryManager>(this);
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x0001799C File Offset: 0x00015B9C
		private DispatchEntry RemoveDispatchedEntry(Guid subscriptionGuid)
		{
			SyncUtilities.ThrowIfArgumentNull("subscriptionGuid", subscriptionGuid);
			DispatchEntry dispatchEntry = this.dispatched[subscriptionGuid];
			this.dispatched.Remove(subscriptionGuid);
			if (dispatchEntry != null)
			{
				this.workTypeBudgetManager.Decrement(dispatchEntry.WorkType);
				Dictionary<Guid, int> dictionary;
				Guid databaseGuid;
				(dictionary = this.entriesPerDatabase)[databaseGuid = dispatchEntry.MiniSubscriptionInformation.DatabaseGuid] = dictionary[databaseGuid] - 1;
			}
			return dispatchEntry;
		}

		// Token: 0x060003A3 RID: 931 RVA: 0x00017A0C File Offset: 0x00015C0C
		private void CleanExpiredItemsCallback(object state)
		{
			this.syncLogSession.LogDebugging((TSLID)1294UL, "CleanExpiredItemsCallback", new object[0]);
			ExDateTime utcNow = ExDateTime.UtcNow;
			List<DispatchEntry> list = new List<DispatchEntry>();
			this.collectionLock.EnterWriteLock();
			try
			{
				foreach (DispatchEntry dispatchEntry in this.dispatched.Values)
				{
					TimeSpan t = utcNow - dispatchEntry.DispatchAttemptTime;
					if (t > this.timeToEntryExpiration)
					{
						list.Add(dispatchEntry);
					}
				}
				foreach (DispatchEntry dispatchEntry2 in list)
				{
					this.RemoveDispatchedEntry(dispatchEntry2.MiniSubscriptionInformation.SubscriptionGuid);
					if (this.InternalEntryExpiredEvent != null)
					{
						this.InternalEntryExpiredEvent(this, dispatchEntry2);
					}
				}
			}
			finally
			{
				this.expirationTimer.Change((int)this.expirationCheckFrequency.TotalMilliseconds, -1);
				this.collectionLock.ExitWriteLock();
			}
		}

		// Token: 0x040001EC RID: 492
		private readonly Dictionary<Guid, DispatchEntry> attemptingDispatch;

		// Token: 0x040001ED RID: 493
		private readonly Dictionary<Guid, DispatchEntry> dispatched;

		// Token: 0x040001EE RID: 494
		private readonly Dictionary<Guid, int> entriesPerDatabase;

		// Token: 0x040001EF RID: 495
		private WorkTypeBudgetManager workTypeBudgetManager;

		// Token: 0x040001F0 RID: 496
		private ReaderWriterLockSlim collectionLock = new ReaderWriterLockSlim();

		// Token: 0x040001F1 RID: 497
		private GuardedTimer expirationTimer;

		// Token: 0x040001F2 RID: 498
		private TimeSpan expirationCheckFrequency;

		// Token: 0x040001F3 RID: 499
		private TimeSpan timeToEntryExpiration;

		// Token: 0x040001F4 RID: 500
		private SyncLogSession syncLogSession;
	}
}
