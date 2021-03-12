using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Assistants.EventLog;
using Microsoft.Exchange.Data;
using Microsoft.Exchange.Data.Directory;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics.Components.Assistants;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x0200006A RID: 106
	internal sealed class MailboxDispatcher : Base, IDisposable
	{
		// Token: 0x060002FD RID: 765 RVA: 0x0000F1C5 File Offset: 0x0000D3C5
		internal static void SetTestHookForBeginningOfSetAsDead(Action testhook)
		{
			MailboxDispatcher.syncWithTestCodeBeginningOfSetAsDead = testhook;
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000F1CD File Offset: 0x0000D3CD
		internal static void SetTestHookForEndOfSetAsDead(Action testhook)
		{
			MailboxDispatcher.syncWithTestCodeEndOfSetAsDead = testhook;
		}

		// Token: 0x060002FF RID: 767 RVA: 0x0000F1D5 File Offset: 0x0000D3D5
		private MailboxDispatcher(Guid mailboxGuid, EventControllerPrivate controller, int numberOfAssistants)
		{
			this.MailboxGuid = mailboxGuid;
			this.controller = controller;
			this.assistantDispatchers = new Dictionary<Guid, EventDispatcherPrivate>(numberOfAssistants);
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000300 RID: 768 RVA: 0x0000F20D File Offset: 0x0000D40D
		// (set) Token: 0x06000301 RID: 769 RVA: 0x0000F215 File Offset: 0x0000D415
		public Guid MailboxGuid { get; private set; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000302 RID: 770 RVA: 0x0000F21E File Offset: 0x0000D41E
		public DatabaseInfo DatabaseInfo
		{
			get
			{
				return this.controller.DatabaseInfo;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000303 RID: 771 RVA: 0x0000F22B File Offset: 0x0000D42B
		public bool IsMailboxDead
		{
			get
			{
				return this.decayedEventCounter > 0L;
			}
		}

		// Token: 0x170000D0 RID: 208
		// (get) Token: 0x06000304 RID: 772 RVA: 0x0000F237 File Offset: 0x0000D437
		public long DecayedEventCounter
		{
			get
			{
				return this.decayedEventCounter;
			}
		}

		// Token: 0x170000D1 RID: 209
		// (get) Token: 0x06000305 RID: 773 RVA: 0x0000F240 File Offset: 0x0000D440
		public string MailboxDisplayName
		{
			get
			{
				ExchangePrincipal exchangePrincipal = this.mailboxOwner;
				if (exchangePrincipal == null)
				{
					return this.MailboxGuid.ToString();
				}
				return exchangePrincipal.MailboxInfo.DisplayName + " (" + this.MailboxGuid.ToString() + ")";
			}
		}

		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x06000306 RID: 774 RVA: 0x0000F29A File Offset: 0x0000D49A
		// (set) Token: 0x06000307 RID: 775 RVA: 0x0000F2A2 File Offset: 0x0000D4A2
		public bool Shutdown { get; private set; }

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x06000308 RID: 776 RVA: 0x0000F2AC File Offset: 0x0000D4AC
		public bool IsIdle
		{
			get
			{
				foreach (EventDispatcherPrivate eventDispatcherPrivate in this.assistantDispatchers.Values)
				{
					if (!eventDispatcherPrivate.IsIdle)
					{
						return false;
					}
				}
				return true;
			}
		}

		// Token: 0x06000309 RID: 777 RVA: 0x0000F30C File Offset: 0x0000D50C
		public static MailboxDispatcher CreateFromBookmark(EventControllerPrivate controller, EventAccess eventAccess, MapiEvent[] eventTable, Bookmark mailboxBookmark, Bookmark databaseBookmark)
		{
			MailboxDispatcher mailboxDispatcher = new MailboxDispatcher(mailboxBookmark.Identity, controller, controller.Assistants.Count);
			foreach (AssistantCollectionEntry assistantCollectionEntry in controller.Assistants)
			{
				EventDispatcherPrivate value = new EventDispatcherPrivate(mailboxDispatcher, assistantCollectionEntry, controller, mailboxBookmark[assistantCollectionEntry.Identity]);
				mailboxDispatcher.assistantDispatchers.Add(assistantCollectionEntry.Identity, value);
				assistantCollectionEntry.PerformanceCounters.EventDispatchers.Increment();
			}
			foreach (EventDispatcherPrivate eventDispatcherPrivate in mailboxDispatcher.assistantDispatchers.Values)
			{
				eventDispatcherPrivate.Initialize(eventAccess, eventTable, databaseBookmark[eventDispatcherPrivate.AssistantIdentity]);
			}
			return mailboxDispatcher;
		}

		// Token: 0x0600030A RID: 778 RVA: 0x0000F400 File Offset: 0x0000D600
		public static MailboxDispatcher CreateWithoutBookmark(EventControllerPrivate controller, EventAccess eventAccess, Guid mailboxGuid, Bookmark databaseBookmark, bool dispatcherIsUpToDate)
		{
			Bookmark mailboxBookmark = eventAccess.GetMailboxBookmark(mailboxGuid, databaseBookmark, dispatcherIsUpToDate);
			return MailboxDispatcher.CreateFromBookmark(controller, eventAccess, null, mailboxBookmark, databaseBookmark);
		}

		// Token: 0x0600030B RID: 779 RVA: 0x0000F422 File Offset: 0x0000D622
		public override string ToString()
		{
			return "MailboxDispatcher for " + this.MailboxDisplayName;
		}

		// Token: 0x0600030C RID: 780 RVA: 0x0000F434 File Offset: 0x0000D634
		public void Dispose()
		{
			foreach (EventDispatcherPrivate eventDispatcherPrivate in this.assistantDispatchers.Values)
			{
				eventDispatcherPrivate.Dispose();
			}
			this.DisposeMailboxSession();
		}

		// Token: 0x0600030D RID: 781 RVA: 0x0000F494 File Offset: 0x0000D694
		public EventDispatcherPrivate GetAssistantDispatcher(Guid assistantId)
		{
			return this.assistantDispatchers[assistantId];
		}

		// Token: 0x0600030E RID: 782 RVA: 0x0000F4A4 File Offset: 0x0000D6A4
		public void DispatchEvent(InterestingEvent interestingEvent, MailboxDispatcher.MailboxFilterDelegate mailboxFilterMethod, MailboxDispatcher.DispatchDelegate eventHandlerMethod, string nonLocalizedAssistantName)
		{
			lock (this.mailboxSessionLocker)
			{
				this.LoadMailboxOwner(interestingEvent.MapiEvent.TenantHint, nonLocalizedAssistantName);
				if (!mailboxFilterMethod(this.mailboxOwner))
				{
					ExTraceGlobals.EventDispatcherTracer.TraceDebug<MailboxDispatcher>((long)this.GetHashCode(), "{0}: Discarding event since the assistant is no longer interested in it after inspecting the mailbox owner.", this);
				}
				else
				{
					this.ConnectMailboxSession(nonLocalizedAssistantName);
					eventHandlerMethod(this.mailboxSession);
				}
			}
		}

		// Token: 0x0600030F RID: 783 RVA: 0x0000F530 File Offset: 0x0000D730
		public void UpdateWatermark(Guid assistantId, long eventCounter)
		{
			this.assistantDispatchers[assistantId].CommittedWatermark = eventCounter;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x0000F544 File Offset: 0x0000D744
		public void RequestShutdown()
		{
			this.Shutdown = true;
		}

		// Token: 0x06000311 RID: 785 RVA: 0x0000F550 File Offset: 0x0000D750
		public void WaitForShutdown()
		{
			foreach (EventDispatcherPrivate eventDispatcherPrivate in this.assistantDispatchers.Values)
			{
				eventDispatcherPrivate.WaitForShutdown();
			}
		}

		// Token: 0x06000312 RID: 786 RVA: 0x0000F5A8 File Offset: 0x0000D7A8
		public void OnWorkersStarted()
		{
			Interlocked.Increment(ref this.numberOfActiveDispatchers);
		}

		// Token: 0x06000313 RID: 787 RVA: 0x0000F5B8 File Offset: 0x0000D7B8
		public void OnWorkersClear()
		{
			if (Interlocked.Decrement(ref this.numberOfActiveDispatchers) == 0)
			{
				this.DisposeMailboxSession();
			}
		}

		// Token: 0x06000314 RID: 788 RVA: 0x0000F5DC File Offset: 0x0000D7DC
		public void ProcessPolledEvent(MapiEvent mapiEvent)
		{
			ExTraceGlobals.EventDispatcherTracer.TraceDebug<MailboxDispatcher, long>((long)this.GetHashCode(), "{0}: ProcessPolledEvent {1}", this, mapiEvent.EventCounter);
			if (this.IsMailboxDead)
			{
				ExTraceGlobals.EventDispatcherTracer.TraceDebug<MailboxDispatcher, long>((long)this.GetHashCode(), "{0}: Dead mailbox; discarding event {1}", this, mapiEvent.EventCounter);
				return;
			}
			if ((mapiEvent.EventMask & MapiEventTypeFlags.MailboxDeleted) != (MapiEventTypeFlags)0 || ((mapiEvent.EventMask & MapiEventTypeFlags.MailboxMoveSucceeded) != (MapiEventTypeFlags)0 && (mapiEvent.EventFlags & MapiEventFlags.Source) != MapiEventFlags.None))
			{
				ExTraceGlobals.EventDispatcherTracer.TraceDebug<MailboxDispatcher>((long)this.GetHashCode(), "{0}: Mailbox may have been deleted or moved away", this);
				if (this.DoesMailboxExist())
				{
					ExTraceGlobals.EventDispatcherTracer.TraceDebug<MailboxDispatcher, MapiEvent>((long)this.GetHashCode(), "{0}: Mailbox still exists while processing mapiEvent {1}", this, mapiEvent);
				}
				else
				{
					ExTraceGlobals.EventDispatcherTracer.TraceDebug<MailboxDispatcher>((long)this.GetHashCode(), "{0}: Mailbox no longer exists on this database", this);
					this.SetAsDeadMailbox(mapiEvent.EventCounter, mapiEvent.EventCounter);
				}
			}
			EmergencyKit emergencyKit = new EmergencyKit(mapiEvent);
			int num = 0;
			foreach (EventDispatcherPrivate eventDispatcherPrivate in this.assistantDispatchers.Values)
			{
				bool flag = eventDispatcherPrivate.IsAssistantInterestedInMailbox(this.mailboxOwner);
				if (flag)
				{
					bool flag2 = eventDispatcherPrivate.ProcessPolledEvent(emergencyKit);
					if (flag2)
					{
						num++;
					}
				}
			}
			if (num > 0)
			{
				this.controller.DatabaseCounters.InterestingEvents.Increment();
				if (num > 1)
				{
					this.controller.DatabaseCounters.EventsInterestingToMultipleAsssitants.Increment();
				}
			}
			this.controller.DatabaseCounters.InterestingEventsBase.Increment();
		}

		// Token: 0x06000315 RID: 789 RVA: 0x0000F774 File Offset: 0x0000D974
		public void SetAsDeadMailbox(long problemEventCounter, long decayedEventCounter)
		{
			if (MailboxDispatcher.syncWithTestCodeBeginningOfSetAsDead != null)
			{
				MailboxDispatcher.syncWithTestCodeBeginningOfSetAsDead();
			}
			lock (this.deadMailboxLocker)
			{
				ExTraceGlobals.EventDispatcherTracer.TraceDebug<MailboxDispatcher, long, long>((long)this.GetHashCode(), "{0}: Setting mailbox dead events [{1}-{2}]", this, problemEventCounter, decayedEventCounter);
				if (this.IsMailboxDead)
				{
					this.decayedEventCounter = Math.Min(this.decayedEventCounter, decayedEventCounter);
					ExTraceGlobals.EventDispatcherTracer.TraceDebug<MailboxDispatcher, long>((long)this.GetHashCode(), "{0}: this.decayedEventCounter: {1}", this, this.decayedEventCounter);
				}
				else
				{
					this.decayedEventCounter = decayedEventCounter;
					base.LogEvent(AssistantsEventLogConstants.Tuple_DeadMailbox, null, new object[]
					{
						problemEventCounter,
						decayedEventCounter,
						this.MailboxDisplayName,
						(this.mailboxOwner == null) ? null : this.mailboxOwner.MailboxInfo.OrganizationId.GetTenantGuid().ToString(),
						(this.mailboxOwner == null) ? null : this.mailboxOwner.MailboxInfo.Location.ServerFqdn,
						this.DatabaseInfo.DisplayName
					});
					foreach (EventDispatcherPrivate eventDispatcherPrivate in this.assistantDispatchers.Values)
					{
						ExTraceGlobals.EventDispatcherTracer.TraceDebug<MailboxDispatcher>((long)this.GetHashCode(), "{0}: Clearing event queue", this);
						eventDispatcherPrivate.ClearPendingQueue();
					}
					this.controller.DeadDispatcher(this);
				}
			}
			if (MailboxDispatcher.syncWithTestCodeEndOfSetAsDead != null)
			{
				MailboxDispatcher.syncWithTestCodeEndOfSetAsDead();
			}
		}

		// Token: 0x06000316 RID: 790 RVA: 0x0000F940 File Offset: 0x0000DB40
		public IList<EventDispatcherPrivate> GetEventDispatcher(Guid? assistantGuid)
		{
			List<EventDispatcherPrivate> list = new List<EventDispatcherPrivate>();
			lock (this.assistantDispatchers)
			{
				foreach (KeyValuePair<Guid, EventDispatcherPrivate> keyValuePair in this.assistantDispatchers)
				{
					if (assistantGuid == null || keyValuePair.Key == assistantGuid)
					{
						list.Add(keyValuePair.Value);
					}
				}
			}
			return list;
		}

		// Token: 0x06000317 RID: 791 RVA: 0x0000F9FC File Offset: 0x0000DBFC
		public override void ExportToQueryableObject(QueryableObject queryableObject)
		{
			base.ExportToQueryableObject(queryableObject);
			QueryableMailboxDispatcher queryableMailboxDispatcher = queryableObject as QueryableMailboxDispatcher;
			if (queryableMailboxDispatcher != null)
			{
				queryableMailboxDispatcher.MailboxGuid = this.MailboxGuid;
				queryableMailboxDispatcher.DecayedEventCounter = this.decayedEventCounter;
				queryableMailboxDispatcher.NumberOfActiveDispatchers = this.numberOfActiveDispatchers;
				queryableMailboxDispatcher.IsMailboxDead = this.IsMailboxDead;
				queryableMailboxDispatcher.IsIdle = this.IsIdle;
			}
		}

		// Token: 0x06000318 RID: 792 RVA: 0x0000FA56 File Offset: 0x0000DC56
		private void ThrowAppropriateSessionException(Exception e)
		{
			if (this.DoesMailboxExist())
			{
				throw new DisconnectedMailboxException(e);
			}
			throw new DeadMailboxException(e);
		}

		// Token: 0x06000319 RID: 793 RVA: 0x0000FA83 File Offset: 0x0000DC83
		private void ConnectMailboxSession(string nonLocalizedAssistantName)
		{
			this.InvokeAndMapException(delegate
			{
				if (this.mailboxSession == null)
				{
					this.mailboxSession = this.CreateMailboxSession();
				}
			}, nonLocalizedAssistantName);
		}

		// Token: 0x0600031A RID: 794 RVA: 0x0000FB28 File Offset: 0x0000DD28
		private void LoadMailboxOwner(byte[] tenantPartitionHintBlob, string nonLocalizedAssistantName)
		{
			if (this.mailboxOwner == null)
			{
				this.InvokeAndMapException(delegate
				{
					ExTraceGlobals.EventDispatcherTracer.TraceDebug<MailboxDispatcher>((long)this.GetHashCode(), "{0}: Looking up ExchangePrincipal...", this);
					ADSessionSettings adSettings;
					if (tenantPartitionHintBlob != null && tenantPartitionHintBlob.Length != 0)
					{
						adSettings = ADSessionSettings.FromTenantPartitionHint(TenantPartitionHint.FromPersistablePartitionHint(tenantPartitionHintBlob));
					}
					else
					{
						adSettings = ADSessionSettings.FromRootOrgScopeSet();
					}
					this.mailboxOwner = ExchangePrincipal.FromLocalServerMailboxGuid(adSettings, this.DatabaseInfo.Guid, this.MailboxGuid);
				}, nonLocalizedAssistantName);
			}
		}

		// Token: 0x0600031B RID: 795 RVA: 0x0000FC48 File Offset: 0x0000DE48
		private void InvokeAndMapException(MailboxDispatcher.OpenMailboxDelegate method, string nonLocalizedAssistantName)
		{
			ExTraceGlobals.EventDispatcherTracer.TraceDebug<MailboxDispatcher>((long)this.GetHashCode(), "{0}: InvokeAndMapException", this);
			try
			{
				base.CatchMeIfYouCan(delegate
				{
					try
					{
						method();
					}
					catch (WrongServerException ex2)
					{
						ExTraceGlobals.EventDispatcherTracer.TraceError<MailboxDispatcher, WrongServerException>((long)this.GetHashCode(), "{0}: Unable to open session: {1}", this, ex2);
						this.ThrowAppropriateSessionException(ex2);
					}
					catch (ObjectNotFoundException ex3)
					{
						ExTraceGlobals.EventDispatcherTracer.TraceError<MailboxDispatcher, ObjectNotFoundException>((long)this.GetHashCode(), "{0}: User account/mailbox not found: {1}", this, ex3);
						this.ThrowAppropriateSessionException(ex3);
					}
					catch (DataValidationException ex4)
					{
						ExTraceGlobals.EventDispatcherTracer.TraceError<MailboxDispatcher, DataValidationException>((long)this.GetHashCode(), "{0}: User object is not valid: {1}", this, ex4);
						throw new MailboxIneptException(ex4);
					}
				}, nonLocalizedAssistantName);
			}
			catch (AIException ex)
			{
				ExTraceGlobals.EventDispatcherTracer.TraceError<MailboxDispatcher, AIException>((long)this.GetHashCode(), "{0}: Could not open mailbox: {1}", this, ex);
				base.LogEvent(AssistantsEventLogConstants.Tuple_MailboxSessionException, null, new object[]
				{
					this.MailboxDisplayName,
					(this.mailboxOwner == null) ? null : this.mailboxOwner.MailboxInfo.OrganizationId.GetTenantGuid().ToString(),
					(this.mailboxOwner == null) ? null : this.mailboxOwner.MailboxInfo.Location.ServerFqdn,
					this.DatabaseInfo.DisplayName,
					ex
				});
				throw;
			}
		}

		// Token: 0x0600031C RID: 796 RVA: 0x0000FD50 File Offset: 0x0000DF50
		private void DisposeMailboxSession()
		{
			lock (this.mailboxSessionLocker)
			{
				if (this.mailboxSession != null)
				{
					ExTraceGlobals.EventDispatcherTracer.TraceDebug<MailboxDispatcher>((long)this.GetHashCode(), "{0}: Disposing of the mailbox session", this);
					this.mailboxSession.Dispose();
					this.mailboxSession = null;
					this.mailboxOwner = null;
					this.controller.DatabaseCounters.MailboxSessionsInUseByDispatchers.Decrement();
				}
			}
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0000FDD8 File Offset: 0x0000DFD8
		private bool DoesMailboxExist()
		{
			ExTraceGlobals.EventDispatcherTracer.TraceDebug<MailboxDispatcher>((long)this.GetHashCode(), "{0}: Searching for mailbox", this);
			if (this.MailboxGuid == Guid.Empty)
			{
				return false;
			}
			bool result;
			using (ExRpcAdmin exRpcAdmin = ExRpcAdmin.Create("Client=EBA", null, null, null, null))
			{
				try
				{
					PropValue[][] mailboxTableInfo = exRpcAdmin.GetMailboxTableInfo(this.DatabaseInfo.Guid, this.MailboxGuid, MailboxTableFlags.IncludeSoftDeletedMailbox, new PropTag[]
					{
						PropTag.MailboxMiscFlags
					});
					if (mailboxTableInfo.Length < 1 || mailboxTableInfo[0].Length < 1)
					{
						ExTraceGlobals.EventDispatcherTracer.TraceDebug<MailboxDispatcher>((long)this.GetHashCode(), "{0}: GetMailboxTableInfo returned invalid response", this);
						return false;
					}
					MailboxMiscFlags @int = (MailboxMiscFlags)mailboxTableInfo[0][0].GetInt(0);
					if ((@int & MailboxMiscFlags.CreatedByMove) != MailboxMiscFlags.None)
					{
						ExTraceGlobals.EventDispatcherTracer.TraceDebug<MailboxDispatcher>((long)this.GetHashCode(), "{0}: Mailbox is the destination mailbox for the move.", this);
						return true;
					}
					if ((@int & (MailboxMiscFlags.DisabledMailbox | MailboxMiscFlags.SoftDeletedMailbox | MailboxMiscFlags.MRSSoftDeletedMailbox)) != MailboxMiscFlags.None)
					{
						ExTraceGlobals.EventDispatcherTracer.TraceDebug<MailboxDispatcher, string>((long)this.GetHashCode(), "{0}: Mailbox exists in mailbox table, but marked as inaccessible: {1}", this, @int.ToString());
						return false;
					}
				}
				catch (MapiExceptionNotFound arg)
				{
					ExTraceGlobals.EventDispatcherTracer.TraceDebug<MailboxDispatcher, MapiExceptionNotFound>((long)this.GetHashCode(), "{0}: Mailbox does not exist: {1}", this, arg);
					return false;
				}
				ExTraceGlobals.EventDispatcherTracer.TraceDebug<MailboxDispatcher>((long)this.GetHashCode(), "{0}: Mailbox exists", this);
				result = true;
			}
			return result;
		}

		// Token: 0x0600031E RID: 798 RVA: 0x0000FF40 File Offset: 0x0000E140
		private MailboxSession CreateMailboxSession()
		{
			MailboxSession mailboxSession = null;
			bool flag = false;
			try
			{
				ExTraceGlobals.EventDispatcherTracer.TraceDebug<MailboxDispatcher>((long)this.GetHashCode(), "{0}: Creating mailbox session...", this);
				mailboxSession = this.DatabaseInfo.GetMailbox(this.mailboxOwner, ClientType.EventBased, "EventDispatcher");
				base.TracePfd("PFD AIS {0} {1}: Created mailbox session.", new object[]
				{
					30807,
					this
				});
				mailboxSession.ExTimeZone = ExTimeZone.CurrentTimeZone;
				flag = true;
			}
			finally
			{
				if (!flag && mailboxSession != null)
				{
					mailboxSession.Dispose();
				}
			}
			this.controller.DatabaseCounters.MailboxSessionsInUseByDispatchers.Increment();
			return mailboxSession;
		}

		// Token: 0x040001D1 RID: 465
		private static Action syncWithTestCodeBeginningOfSetAsDead;

		// Token: 0x040001D2 RID: 466
		private static Action syncWithTestCodeEndOfSetAsDead;

		// Token: 0x040001D3 RID: 467
		private Dictionary<Guid, EventDispatcherPrivate> assistantDispatchers;

		// Token: 0x040001D4 RID: 468
		private EventControllerPrivate controller;

		// Token: 0x040001D5 RID: 469
		private long decayedEventCounter;

		// Token: 0x040001D6 RID: 470
		private object deadMailboxLocker = new object();

		// Token: 0x040001D7 RID: 471
		private object mailboxSessionLocker = new object();

		// Token: 0x040001D8 RID: 472
		private ExchangePrincipal mailboxOwner;

		// Token: 0x040001D9 RID: 473
		private MailboxSession mailboxSession;

		// Token: 0x040001DA RID: 474
		private int numberOfActiveDispatchers;

		// Token: 0x0200006B RID: 107
		// (Invoke) Token: 0x06000322 RID: 802
		public delegate bool MailboxFilterDelegate(ExchangePrincipal mailboxOwner);

		// Token: 0x0200006C RID: 108
		// (Invoke) Token: 0x06000326 RID: 806
		public delegate void DispatchDelegate(MailboxSession mailboxSession);

		// Token: 0x0200006D RID: 109
		// (Invoke) Token: 0x0600032A RID: 810
		private delegate void OpenMailboxDelegate();
	}
}
