using System;
using Microsoft.Exchange.Data.Directory.Recipient;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Assistants;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x0200003E RID: 62
	internal sealed class EventDispatcherPrivate : EventDispatcher
	{
		// Token: 0x06000254 RID: 596 RVA: 0x0000D170 File Offset: 0x0000B370
		internal static void SetTestHookForAfterEventQueueCountDecrementedForRetry(Action testhook)
		{
			EventDispatcherPrivate.syncWithTestCodeAfterEventQueueCountDecrementedForRetry = testhook;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x0000D178 File Offset: 0x0000B378
		public EventDispatcherPrivate(MailboxDispatcher parentMailboxDispatcher, AssistantCollectionEntry assistant, EventControllerPrivate controller, long watermark) : base(assistant, new MailboxGovernor(controller.Governor, new Throttle("EventDispatcherPrivate", 1, controller.Throttle)), controller)
		{
			this.parentMailboxDispatcher = parentMailboxDispatcher;
			this.committedWatermark = watermark;
			this.highestEventQueued = watermark;
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000256 RID: 598 RVA: 0x0000D1B5 File Offset: 0x0000B3B5
		// (set) Token: 0x06000257 RID: 599 RVA: 0x0000D1BD File Offset: 0x0000B3BD
		public long CommittedWatermark
		{
			get
			{
				return this.committedWatermark;
			}
			set
			{
				this.committedWatermark = value;
			}
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000258 RID: 600 RVA: 0x0000D1C6 File Offset: 0x0000B3C6
		public bool IsMailboxDead
		{
			get
			{
				return this.parentMailboxDispatcher.IsMailboxDead;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000259 RID: 601 RVA: 0x0000D1D3 File Offset: 0x0000B3D3
		public bool IsIdle
		{
			get
			{
				return !this.IsInRetry && base.ActiveQueue.Count == 0 && base.PendingQueue.Count == 0;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x0600025A RID: 602 RVA: 0x0000D1FC File Offset: 0x0000B3FC
		public long RecoveryEventCounter
		{
			get
			{
				return this.recoveryEventCounter;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x0600025B RID: 603 RVA: 0x0000D204 File Offset: 0x0000B404
		public Guid MailboxGuid
		{
			get
			{
				return this.parentMailboxDispatcher.MailboxGuid;
			}
		}

		// Token: 0x170000B4 RID: 180
		// (get) Token: 0x0600025C RID: 604 RVA: 0x0000D211 File Offset: 0x0000B411
		public EventControllerPrivate ControllerPrivate
		{
			get
			{
				return (EventControllerPrivate)base.Controller;
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600025D RID: 605 RVA: 0x0000D21E File Offset: 0x0000B41E
		public override string MailboxDisplayName
		{
			get
			{
				return this.parentMailboxDispatcher.MailboxDisplayName;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x0600025E RID: 606 RVA: 0x0000D22B File Offset: 0x0000B42B
		public Guid AssistantIdentity
		{
			get
			{
				return base.Assistant.Identity;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x0600025F RID: 607 RVA: 0x0000D238 File Offset: 0x0000B438
		protected override bool Shutdown
		{
			get
			{
				return base.Shutdown || this.IsMailboxDead || this.parentMailboxDispatcher.Shutdown;
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000260 RID: 608 RVA: 0x0000D257 File Offset: 0x0000B457
		protected override bool CountEvents
		{
			get
			{
				return !this.IsInRetry;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000261 RID: 609 RVA: 0x0000D262 File Offset: 0x0000B462
		// (set) Token: 0x06000262 RID: 610 RVA: 0x0000D26A File Offset: 0x0000B46A
		private bool IsInRetry
		{
			get
			{
				return this.isInRetry;
			}
			set
			{
				this.isInRetry = value;
			}
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000D274 File Offset: 0x0000B474
		public void Initialize(EventAccess eventAccess, MapiEvent[] eventTable, long databaseCounter)
		{
			try
			{
				this.InitFromWatermark(eventAccess, eventTable, databaseCounter);
			}
			catch (MapiExceptionUnknownMailbox arg)
			{
				ExTraceGlobals.EventDispatcherTracer.TraceError<EventDispatcherPrivate, MapiExceptionUnknownMailbox>((long)this.GetHashCode(), "{0}: Unable to InitFromWatermark: {1}", this, arg);
				long lastEventCounter = this.ControllerPrivate.GetLastEventCounter();
				try
				{
					this.InitFromWatermark(eventAccess, eventTable, databaseCounter);
				}
				catch (MapiExceptionUnknownMailbox arg2)
				{
					ExTraceGlobals.EventDispatcherTracer.TraceError<EventDispatcherPrivate, MapiExceptionUnknownMailbox>((long)this.GetHashCode(), "{0}: Still unable to InitFromWatermark: {1}", this, arg2);
					this.SetAsDeadMailbox(databaseCounter, lastEventCounter);
				}
			}
			ExTraceGlobals.EventDispatcherTracer.TraceDebug<EventDispatcherPrivate>((long)this.GetHashCode(), "{0}: Constructed", this);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000D314 File Offset: 0x0000B514
		public override string ToString()
		{
			return "Dispatcher for " + this.MailboxDisplayName + " and " + base.Assistant.Instance.Name;
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000D340 File Offset: 0x0000B540
		public bool ProcessPolledEvent(EmergencyKit emergencyKit)
		{
			ExTraceGlobals.EventDispatcherTracer.TraceDebug<EventDispatcherPrivate, long>((long)this.GetHashCode(), "{0}: ProcessPolledEvent {1}", this, emergencyKit.MapiEvent.EventCounter);
			if (this.IsInRetry)
			{
				ExTraceGlobals.EventDispatcherTracer.TraceDebug<EventDispatcherPrivate, long>((long)this.GetHashCode(), "{0}: In Retry; discarding event {1}", this, emergencyKit.MapiEvent.EventCounter);
				return false;
			}
			if (emergencyKit.MapiEvent.EventCounter <= this.committedWatermark)
			{
				ExTraceGlobals.EventDispatcherTracer.TraceDebug<EventDispatcherPrivate, long, long>((long)this.GetHashCode(), "{0}: Ignoring event {1} because it is below the watermark {2}", this, emergencyKit.MapiEvent.EventCounter, this.committedWatermark);
				return false;
			}
			if (emergencyKit.MapiEvent.EventCounter <= this.highestEventQueued)
			{
				ExTraceGlobals.EventDispatcherTracer.TraceDebug<EventDispatcherPrivate, long, long>((long)this.GetHashCode(), "{0}: Ignoring event {1} because it was already queued {2}", this, emergencyKit.MapiEvent.EventCounter, this.committedWatermark);
				return false;
			}
			return base.EnqueueIfInteresting(emergencyKit);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000D41C File Offset: 0x0000B61C
		public Watermark GetCurrentWatermark(ref long databaseWatermark)
		{
			ExTraceGlobals.EventDispatcherTracer.TraceDebug<EventDispatcherPrivate>((long)this.GetHashCode(), "{0}: GetCurrentWatermark", this);
			Watermark result;
			lock (base.Locker)
			{
				long num = this.committedWatermark;
				if (this.IsMailboxDead)
				{
					ExTraceGlobals.EventDispatcherTracer.TraceDebug<EventDispatcherPrivate>((long)this.GetHashCode(), "{0}: Not writing watermark for dead mailbox", this);
					result = null;
				}
				else
				{
					InterestingEvent interestingEvent;
					if (base.ActiveQueue.Count > 0)
					{
						interestingEvent = base.ActiveQueue[0];
					}
					else if (base.PendingQueue.Count > 0)
					{
						interestingEvent = base.PendingQueue[0];
					}
					else
					{
						interestingEvent = null;
					}
					if (this.IsInRetry)
					{
						if (interestingEvent == null)
						{
							ExTraceGlobals.EventDispatcherTracer.TraceDebug<EventDispatcherPrivate>((long)this.GetHashCode(), "{0}: Not writing watermark for retry mailbox with empty queue", this);
						}
						else
						{
							num = interestingEvent.MapiEvent.EventCounter - 1L;
						}
					}
					else
					{
						if (interestingEvent == null)
						{
							num = Math.Max(databaseWatermark, this.committedWatermark);
						}
						else
						{
							num = interestingEvent.MapiEvent.EventCounter - 1L;
						}
						if (num < databaseWatermark)
						{
							ExTraceGlobals.EventDispatcherTracer.TraceDebug<EventDispatcherPrivate, long>((long)this.GetHashCode(), "{0}: lowering databaseWatermark to {1}", this, num);
							databaseWatermark = num;
						}
					}
					if (this.committedWatermark != num)
					{
						ExTraceGlobals.EventDispatcherTracer.TraceDebug<EventDispatcherPrivate, long, long>((long)this.GetHashCode(), "{0}: Will update watermark for mailbox from {1} to {2}", this, this.committedWatermark, num);
						result = Watermark.GetMailboxWatermark(this.MailboxGuid, num);
					}
					else
					{
						ExTraceGlobals.EventDispatcherTracer.TraceDebug<EventDispatcherPrivate, long>((long)this.GetHashCode(), "{0}: Watermark has not changed from {1}.", this, num);
						result = null;
					}
				}
			}
			return result;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000D5AC File Offset: 0x0000B7AC
		public void Recover()
		{
			base.Assistant.PerformanceCounters.FailedDispatchers.Decrement();
			lock (base.Locker)
			{
				this.IsInRetry = false;
				this.highestEventQueued = this.recoveryEventCounter - 1L;
			}
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0000D614 File Offset: 0x0000B814
		public void ClearPendingQueue()
		{
			ExTraceGlobals.EventDispatcherTracer.TraceDebug<EventDispatcherPrivate, int>((long)this.GetHashCode(), "{0}: Clearing PendingQueue, queue count:{1}", this, base.PendingQueue.Count);
			lock (base.Locker)
			{
				if (base.PendingQueue.Count != 0)
				{
					base.Assistant.PerformanceCounters.EventsInQueueCurrent.IncrementBy((long)(-(long)base.PendingQueue.Count));
					if (this.CountEvents)
					{
						base.Controller.DecrementEventQueueCount((long)base.PendingQueue.Count);
					}
					base.PendingQueue.Clear();
				}
			}
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0000D6CC File Offset: 0x0000B8CC
		public bool IsAssistantInterestedInMailbox(ExchangePrincipal mailboxOwner)
		{
			bool flag = this.ShouldProcessMailbox(mailboxOwner);
			base.Assistant.PerformanceCounters.EventsDiscardedByMailboxFilterBase.Increment();
			if (!flag)
			{
				ExTraceGlobals.EventDispatcherTracer.TraceDebug<EventDispatcherPrivate>((long)this.GetHashCode(), "{0}: This type of mailbox is not interesting to this assistant.", this);
				base.Assistant.PerformanceCounters.EventsDiscardedByMailboxFilter.Increment();
			}
			return flag;
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0000D728 File Offset: 0x0000B928
		public override void ExportToQueryableObject(QueryableObject queryableObject)
		{
			base.ExportToQueryableObject(queryableObject);
			QueryableEventDispatcher queryableEventDispatcher = queryableObject as QueryableEventDispatcher;
			if (queryableEventDispatcher != null)
			{
				queryableEventDispatcher.AssistantName = base.Assistant.Name;
				queryableEventDispatcher.AssistantGuid = base.Assistant.Identity;
				queryableEventDispatcher.CommittedWatermark = this.committedWatermark;
				queryableEventDispatcher.HighestEventQueued = this.highestEventQueued;
				queryableEventDispatcher.RecoveryEventCounter = this.recoveryEventCounter;
				queryableEventDispatcher.IsInRetry = this.isInRetry;
				queryableEventDispatcher.ActiveQueueLength = base.ActiveQueue.Count;
				queryableEventDispatcher.PendingQueueLength = base.PendingQueue.Count;
				queryableEventDispatcher.PendingWorkers = base.PendingWorkers;
				queryableEventDispatcher.ActiveWorkers = base.ActiveWorkers;
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000D7D8 File Offset: 0x0000B9D8
		protected override void EnqueueEvent(InterestingEvent interestingEvent)
		{
			ExTraceGlobals.EventDispatcherTracer.TraceDebug<EventDispatcherPrivate, long>((long)this.GetHashCode(), "{0}: queueing event {1}...", this, interestingEvent.MapiEvent.EventCounter);
			lock (base.Locker)
			{
				if (base.Shutdown)
				{
					ExTraceGlobals.EventDispatcherTracer.TraceDebug<EventDispatcherPrivate, long>((long)this.GetHashCode(), "{0}: Not queueing event {1} for shutdown mailbox", this, interestingEvent.MapiEvent.EventCounter);
				}
				else if (this.IsMailboxDead)
				{
					ExTraceGlobals.EventDispatcherTracer.TraceDebug<EventDispatcherPrivate, long>((long)this.GetHashCode(), "{0}: Not queueing event {1} for dead mailbox", this, interestingEvent.MapiEvent.EventCounter);
				}
				else if (this.IsInRetry)
				{
					ExTraceGlobals.EventDispatcherTracer.TraceDebug<EventDispatcherPrivate, long>((long)this.GetHashCode(), "{0}: Not queueing event {1} for retry mailbox", this, interestingEvent.MapiEvent.EventCounter);
				}
				else
				{
					base.EnqueueEvent(interestingEvent);
					ExTraceGlobals.EventDispatcherTracer.TraceDebug<EventDispatcherPrivate, long, long>((long)this.GetHashCode(), "{0}: Queued event {1}, old highestEventQueued: {2}", this, interestingEvent.MapiEvent.EventCounter, this.highestEventQueued);
					this.highestEventQueued = Math.Max(this.highestEventQueued, interestingEvent.MapiEvent.EventCounter);
				}
			}
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000D90C File Offset: 0x0000BB0C
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.isInRetry)
				{
					base.Assistant.PerformanceCounters.FailedDispatchers.Decrement();
					this.isInRetry = false;
				}
				base.Assistant.PerformanceCounters.EventDispatchers.Decrement();
			}
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000D94C File Offset: 0x0000BB4C
		protected override void OnWorkersStarted()
		{
			this.parentMailboxDispatcher.OnWorkersStarted();
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0000D959 File Offset: 0x0000BB59
		protected override void OnWorkersClear()
		{
			this.parentMailboxDispatcher.OnWorkersClear();
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000DA84 File Offset: 0x0000BC84
		protected override AIException DangerousProcessItem(EmergencyKit kit, InterestingEvent interestingEvent)
		{
			ExTraceGlobals.EventDispatcherTracer.TraceDebug<EventDispatcherPrivate, long>((long)this.GetHashCode(), "{0}: Dequeued event {1}", this, interestingEvent.MapiEvent.EventCounter);
			long lastEventCounter = 0L;
			AIException ex = null;
			try
			{
				base.CatchMeIfYouCan(delegate
				{
					try
					{
						this.DispatchOneEvent(kit, interestingEvent);
					}
					catch (DeadMailboxException ex3)
					{
						ExTraceGlobals.EventDispatcherTracer.TraceError<EventDispatcherPrivate, DeadMailboxException>((long)this.GetHashCode(), "{0}: Encountered DeadMailboxException #1: {1}", this, ex3);
						lastEventCounter = this.ControllerPrivate.GetLastEventCounter();
						ExTraceGlobals.EventDispatcherTracer.TraceError((long)this.GetHashCode(), "{0}: Read lastEventCounter {1} after DeadMailboxExcetion.  Retrying eventDispatch {2}.  Previous exception: {3}", new object[]
						{
							this,
							lastEventCounter,
							interestingEvent.MapiEvent.EventCounter,
							ex3
						});
						this.DispatchOneEvent(kit, interestingEvent);
						ExTraceGlobals.EventDispatcherTracer.TraceDebug<EventDispatcherPrivate, long>((long)this.GetHashCode(), "{0}: DispatchOneEvent returned successfully for mapiEvent {1}", this, interestingEvent.MapiEvent.EventCounter);
					}
				}, (base.Assistant != null) ? base.Assistant.Name : "<null>");
			}
			catch (AIException ex2)
			{
				ExTraceGlobals.EventDispatcherTracer.TraceError<EventDispatcherPrivate, AIException>((long)this.GetHashCode(), "{0}: Exception from DispatchOneEvent: {1}", this, ex2);
				ex = ex2;
			}
			if (ex is DeadMailboxException)
			{
				ExTraceGlobals.EventDispatcherTracer.TraceError((long)this.GetHashCode(), "{0}: Encountered DeadMailboxException #2.  mapiEvent: {1}, lastEventCounter: {2}, exception: {3}", new object[]
				{
					this,
					interestingEvent.MapiEvent.EventCounter,
					lastEventCounter,
					ex
				});
				this.SetAsDeadMailbox(interestingEvent.MapiEvent.EventCounter, lastEventCounter);
			}
			return ex;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000DBB8 File Offset: 0x0000BDB8
		protected override void OnCompletedItem(InterestingEvent interestingEvent, AIException exception)
		{
			base.OnCompletedItem(interestingEvent, exception);
			if (this.IsInRetry && !this.IsMailboxDead)
			{
				ExTraceGlobals.EventDispatcherTracer.TraceDebug<EventDispatcherPrivate, long>((long)this.GetHashCode(), "{0}: Recovering from retry state, mapiEvent {1}", this, interestingEvent.MapiEvent.EventCounter);
				this.recoveryEventCounter = interestingEvent.MapiEvent.EventCounter + 1L;
				this.ControllerPrivate.RequestRecovery(this);
			}
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000DC20 File Offset: 0x0000BE20
		protected override void OnTransientFailure(InterestingEvent interestingEvent, AIException exception)
		{
			if (exception is TransientMailboxException)
			{
				ExTraceGlobals.EventDispatcherTracer.TraceError<EventDispatcherPrivate, long, AIException>((long)this.GetHashCode(), "{0}: retryable mailbox exception, mapiEvent {1}: {2}", this, interestingEvent.MapiEvent.EventCounter, exception);
				lock (base.Locker)
				{
					this.ClearPendingQueue();
					if (!this.IsInRetry)
					{
						if (this.IsMailboxDead && ExTraceGlobals.EventDispatcherTracer.IsTraceEnabled(TraceType.DebugTrace))
						{
							ExTraceGlobals.EventDispatcherTracer.TraceDebug<EventDispatcherPrivate>((long)this.GetHashCode(), "{0}: Mailbox had been on TransientFailure, but was found dead", this);
						}
						this.isInRetry = true;
						base.Controller.DecrementEventQueueCount();
						base.Assistant.PerformanceCounters.FailedDispatchers.Increment();
						if (EventDispatcherPrivate.syncWithTestCodeAfterEventQueueCountDecrementedForRetry != null)
						{
							EventDispatcherPrivate.syncWithTestCodeAfterEventQueueCountDecrementedForRetry();
						}
					}
				}
			}
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000DCFC File Offset: 0x0000BEFC
		private MapiEvent FindNextEventForMailbox(EventAccess eventAccess, long beginCounter)
		{
			Restriction restriction = Restriction.EQ(PropTag.EventMailboxGuid, this.MailboxGuid.ToByteArray());
			Restriction filter = (base.Controller.Filter == null) ? restriction : Restriction.And(new Restriction[]
			{
				restriction,
				base.Controller.Filter
			});
			ExTraceGlobals.EventDispatcherTracer.TraceDebug<EventDispatcherPrivate, long>((long)this.GetHashCode(), "{0}: Finding next for this mailbox from {1}...", this, beginCounter);
			long num;
			MapiEvent[] array = eventAccess.ReadEvents(beginCounter, 1, int.MaxValue, filter, out num);
			if (array.Length > 0)
			{
				ExTraceGlobals.EventDispatcherTracer.TraceDebug<EventDispatcherPrivate, long, MapiEvent>((long)this.GetHashCode(), "{0}: Found next event for this mailbox from {1}: {2}", this, beginCounter, array[0]);
				return array[0];
			}
			ExTraceGlobals.EventDispatcherTracer.TraceDebug<EventDispatcherPrivate, long>((long)this.GetHashCode(), "{0}: Found no events for mailbox after {1}", this, beginCounter);
			return null;
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000DDC0 File Offset: 0x0000BFC0
		private MapiEvent FindNextEventForMailboxFromEventTable(MapiEvent[] eventTable, long beginCounter)
		{
			for (long num = 0L; num < (long)eventTable.Length; num += 1L)
			{
				checked
				{
					if (eventTable[(int)((IntPtr)num)].EventCounter >= beginCounter && eventTable[(int)((IntPtr)num)].MailboxGuid.Equals(this.MailboxGuid))
					{
						ExTraceGlobals.EventDispatcherTracer.TraceDebug<EventDispatcherPrivate, long, MapiEvent>(unchecked((long)this.GetHashCode()), "{0}: Found next event from the mailbox table from {1}: {2}", this, beginCounter, eventTable[(int)((IntPtr)num)]);
						return eventTable[(int)((IntPtr)num)];
					}
				}
			}
			ExTraceGlobals.EventDispatcherTracer.TraceDebug<EventDispatcherPrivate, long>((long)this.GetHashCode(), "{0}: Found no events on the mailbox table after {1}", this, beginCounter);
			return null;
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000DE40 File Offset: 0x0000C040
		private void InitFromWatermark(EventAccess eventAccess, MapiEvent[] eventTable, long databaseCounter)
		{
			if (this.committedWatermark != 0L && this.committedWatermark < databaseCounter)
			{
				ExTraceGlobals.EventDispatcherTracer.TraceDebug<EventDispatcherPrivate>((long)this.GetHashCode(), "{0}: Searching for retryEvent...", this);
				MapiEvent mapiEvent;
				if (eventTable != null)
				{
					mapiEvent = this.FindNextEventForMailboxFromEventTable(eventTable, this.committedWatermark + 1L);
				}
				else
				{
					mapiEvent = this.FindNextEventForMailbox(eventAccess, this.committedWatermark + 1L);
				}
				if (mapiEvent != null && mapiEvent.EventCounter < databaseCounter)
				{
					ExTraceGlobals.EventDispatcherTracer.TraceDebug((long)this.GetHashCode(), "{0}: Starting in retry.  mailboxWatermark: {1}, databaseCounter: {2}, retryEvent: {3}", new object[]
					{
						this,
						this.committedWatermark,
						databaseCounter,
						mapiEvent.EventCounter
					});
					this.IsInRetry = true;
					ExTraceGlobals.EventDispatcherTracer.TraceDebug<EventDispatcherPrivate>((long)this.GetHashCode(), "{0}: Starting in retry.", this);
					base.Assistant.PerformanceCounters.FailedDispatchers.Increment();
					base.EnqueueItem(InterestingEvent.CreateUnprocessed(mapiEvent));
				}
			}
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000DF3D File Offset: 0x0000C13D
		private void SetAsDeadMailbox(long databaseCounter, long lastEventCounter)
		{
			this.parentMailboxDispatcher.SetAsDeadMailbox(databaseCounter, lastEventCounter);
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000DF4C File Offset: 0x0000C14C
		private bool ShouldProcessMailbox(ExchangePrincipal mailboxOwner)
		{
			if (!base.Assistants.NeedsMailboxSession)
			{
				ExTraceGlobals.EventDispatcherTracer.TraceDebug<EventDispatcherPrivate>((long)this.GetHashCode(), "{0}: No assistant is interested in opening mailbox sessions. Mailbox Filter not applicable. Resume.", this);
				return true;
			}
			if (mailboxOwner == null)
			{
				ExTraceGlobals.EventDispatcherTracer.TraceDebug<EventDispatcherPrivate>((long)this.GetHashCode(), "{0}: The mailbox owner is not known at this time. Resume.", this);
				return true;
			}
			if (mailboxOwner.RecipientTypeDetails == RecipientTypeDetails.PublicFolderMailbox && !base.Assistant.Type.ProcessesPublicDatabases)
			{
				ExTraceGlobals.EventDispatcherTracer.TraceDebug<EventDispatcherPrivate>((long)this.GetHashCode(), "{0}: Assistant is not interested in public folder mailboxes.", this);
				return false;
			}
			IMailboxFilter mailboxFilter = base.Assistant.Type.MailboxFilter;
			if (mailboxFilter == null)
			{
				ExTraceGlobals.EventDispatcherTracer.TraceDebug<EventDispatcherPrivate>((long)this.GetHashCode(), "{0}: Assistant does not implement a mailbox filter. Assume it is interested in all types of mailboxes.", this);
				return true;
			}
			if (mailboxOwner.MailboxInfo.IsArchive)
			{
				if (mailboxFilter.MailboxType.Contains(MailboxType.Archive))
				{
					ExTraceGlobals.EventDispatcherTracer.TraceDebug<EventDispatcherPrivate>((long)this.GetHashCode(), "{0}: Assistant is interested in Archive mailboxes.", this);
					return true;
				}
			}
			else if (mailboxOwner.RecipientTypeDetails == RecipientTypeDetails.ArbitrationMailbox)
			{
				if (mailboxFilter.MailboxType.Contains(MailboxType.Arbitration))
				{
					ExTraceGlobals.EventDispatcherTracer.TraceDebug<EventDispatcherPrivate>((long)this.GetHashCode(), "{0}: Assistant is interested in Arbitration mailboxes.", this);
					return true;
				}
			}
			else
			{
				if (mailboxFilter.MailboxType.Contains(MailboxType.User) && mailboxOwner.RecipientType == RecipientType.UserMailbox)
				{
					ExTraceGlobals.EventDispatcherTracer.TraceDebug<EventDispatcherPrivate>((long)this.GetHashCode(), "{0}: Assistant is interested in User mailboxes.", this);
					return true;
				}
				if (mailboxFilter.MailboxType.Contains(MailboxType.System) && mailboxOwner.RecipientType == RecipientType.SystemMailbox)
				{
					ExTraceGlobals.EventDispatcherTracer.TraceDebug<EventDispatcherPrivate>((long)this.GetHashCode(), "{0}: Assistant is interested in System mailboxes.", this);
					return true;
				}
			}
			ExTraceGlobals.EventDispatcherTracer.TraceDebug((long)this.GetHashCode(), "{0}: Assistant is not interested in this mailbox {1}, type {2}, details {3}, archive: {4}. Its mailbox filter is {5}", new object[]
			{
				this,
				mailboxOwner,
				mailboxOwner.RecipientType,
				mailboxOwner.RecipientTypeDetails,
				mailboxOwner.MailboxInfo.IsArchive,
				mailboxFilter.MailboxType
			});
			return false;
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000E21C File Offset: 0x0000C41C
		private void DispatchOneEvent(EmergencyKit kit, InterestingEvent interestingEvent)
		{
			ExTraceGlobals.EventDispatcherTracer.TraceDebug<EventDispatcherPrivate, long>((long)this.GetHashCode(), "{0}: DispatchOneEvent {1}", this, kit.MapiEvent.EventCounter);
			if (!base.Assistants.NeedsMailboxSession)
			{
				this.HandleEventWithoutSession(kit, interestingEvent);
				return;
			}
			this.parentMailboxDispatcher.DispatchEvent(interestingEvent, delegate(ExchangePrincipal mailboxOwner)
			{
				bool flag = this.ShouldProcessMailbox(mailboxOwner);
				this.Assistant.PerformanceCounters.QueuedEventsDiscardedByMailboxFilterBase.Increment();
				if (!flag)
				{
					ExTraceGlobals.EventDispatcherTracer.TraceDebug<EventDispatcherPrivate, MapiEvent>((long)this.GetHashCode(), "{0}: A previously queued event has been discarded by the mailbox filter. Event: {1}", this, kit.MapiEvent);
					this.Assistant.PerformanceCounters.QueuedEventsDiscardedByMailboxFilter.Increment();
				}
				return flag;
			}, delegate(MailboxSession mailboxSession)
			{
				using (Item item = this.TryOpenItem(mailboxSession, kit.MapiEvent))
				{
					this.HandleEvent(kit, interestingEvent, mailboxSession, item);
				}
			}, (base.Assistant != null) ? base.Assistant.Name : "<null>");
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000E2DC File Offset: 0x0000C4DC
		private Item TryOpenItem(MailboxSession mailboxSession, MapiEvent mapiEvent)
		{
			Item item = null;
			if (mapiEvent.ItemEntryId != null && mapiEvent.ItemType == ObjectType.MAPI_MESSAGE && (mapiEvent.EventMask & MapiEventTypeFlags.ObjectDeleted) == (MapiEventTypeFlags)0)
			{
				ExTraceGlobals.EventDispatcherTracer.TraceDebug<EventDispatcherPrivate>((long)this.GetHashCode(), "{0}: Getting itemId...", this);
				StoreObjectId storeObjectId = StoreObjectId.FromProviderSpecificId(mapiEvent.ItemEntryId);
				ExTraceGlobals.EventDispatcherTracer.TraceDebug<EventDispatcherPrivate, StoreObjectId>((long)this.GetHashCode(), "{0}: Binding to item: {1}", this, storeObjectId);
				Exception ex = null;
				bool flag = false;
				try
				{
					item = Item.Bind(mailboxSession, storeObjectId, base.Assistants.PreloadItemProperties);
					ExTraceGlobals.EventDispatcherTracer.TraceDebug<EventDispatcherPrivate>((long)this.GetHashCode(), "{0}: Bound item. Opening item...", this);
					item.OpenAsReadWrite();
					ExTraceGlobals.EventDispatcherTracer.TraceDebug<EventDispatcherPrivate>((long)this.GetHashCode(), "{0}: Opened item.", this);
					flag = true;
				}
				catch (ObjectNotFoundException ex2)
				{
					ex = ex2;
				}
				catch (ConversionFailedException ex3)
				{
					ex = ex3;
				}
				catch (VirusMessageDeletedException ex4)
				{
					ex = ex4;
				}
				finally
				{
					if (!flag && item != null)
					{
						item.Dispose();
						item = null;
					}
				}
				if (ex != null)
				{
					ExTraceGlobals.EventDispatcherTracer.TraceError((long)this.GetHashCode(), "{0}: failed to open itemId {1} for eventId {2}.  Exception: {3}", new object[]
					{
						this,
						storeObjectId,
						mapiEvent.EventCounter,
						ex
					});
				}
			}
			return item;
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000E438 File Offset: 0x0000C638
		private void HandleEventWithoutSession(EmergencyKit kit, InterestingEvent interestingEvent)
		{
			base.HandleEvent(kit, interestingEvent, null, null);
		}

		// Token: 0x040001A6 RID: 422
		private static Action syncWithTestCodeAfterEventQueueCountDecrementedForRetry;

		// Token: 0x040001A7 RID: 423
		private long committedWatermark;

		// Token: 0x040001A8 RID: 424
		private long highestEventQueued;

		// Token: 0x040001A9 RID: 425
		private long recoveryEventCounter;

		// Token: 0x040001AA RID: 426
		private bool isInRetry;

		// Token: 0x040001AB RID: 427
		private MailboxDispatcher parentMailboxDispatcher;
	}
}
