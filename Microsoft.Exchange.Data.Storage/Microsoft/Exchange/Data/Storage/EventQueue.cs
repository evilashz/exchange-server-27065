using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Mapi;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000703 RID: 1795
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class EventQueue : EventSink, IRecoveryEventSink, IDisposable
	{
		// Token: 0x06004714 RID: 18196 RVA: 0x0012E564 File Offset: 0x0012C764
		private EventQueue(Guid mailboxGuid, bool isPublicFolderDatabase, EventCondition condition, int maxQueueSize, EventWatermark firstMissedEventWatermark) : base(mailboxGuid, isPublicFolderDatabase, condition)
		{
			using (DisposeGuard disposeGuard = this.Guard())
			{
				if (maxQueueSize < 10 || maxQueueSize > 1000)
				{
					throw new ArgumentOutOfRangeException("maxQueueSize", ServerStrings.ExInvalidMaxQueueSize);
				}
				this.maxQueueSize = maxQueueSize;
				this.mainQueue = new Queue<Event>(10);
				this.recoveryQueue = new Queue<Event>(10);
				if (firstMissedEventWatermark != null)
				{
					this.firstMissedEventWatermark = firstMissedEventWatermark;
					this.startInRecovery = true;
					this.areThereMissedEvents = true;
				}
				disposeGuard.Success();
			}
		}

		// Token: 0x06004715 RID: 18197 RVA: 0x0012E620 File Offset: 0x0012C820
		protected override DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<EventQueue>(this);
		}

		// Token: 0x06004716 RID: 18198 RVA: 0x0012E628 File Offset: 0x0012C828
		internal override void HandleException(Exception exception)
		{
			lock (this.thisLock)
			{
				base.HandleException(exception);
				this.SetEventAvailable();
			}
		}

		// Token: 0x06004717 RID: 18199 RVA: 0x0012E670 File Offset: 0x0012C870
		protected override void InternalDispose(bool disposing)
		{
			if (disposing && this.eventAvailableEvent != null)
			{
				lock (this.lockEventAvailableEvent)
				{
					this.eventAvailableEvent.Dispose();
					this.eventAvailableEvent = null;
				}
			}
			base.InternalDispose(disposing);
		}

		// Token: 0x06004718 RID: 18200 RVA: 0x0012E6D0 File Offset: 0x0012C8D0
		protected override void InternalConsume(MapiEvent mapiEvent)
		{
			bool flag = false;
			lock (this.thisLock)
			{
				if (this.startInRecovery)
				{
					this.lastMissedEventWatermark = mapiEvent.Watermark.EventCounter;
					this.startInRecovery = false;
					flag = true;
					this.state = EventQueue.State.NeedRecovery;
				}
				this.currentEventsCount++;
				if (this.mainQueue.Count == this.maxQueueSize || this.areThereMissedEvents)
				{
					if (!this.areThereMissedEvents)
					{
						this.firstMissedEventWatermark = new EventWatermark(base.MdbGuid, mapiEvent.Watermark.EventCounter, false);
						this.areThereMissedEvents = true;
					}
					this.lastMissedEventWatermark = mapiEvent.Watermark.EventCounter;
				}
				else
				{
					this.mainQueue.Enqueue(new Event(base.MdbGuid, mapiEvent));
					if (!this.areThereRecoveryMissedEvents)
					{
						this.SetEventAvailable();
					}
				}
				this.lastKnownWatermark = mapiEvent.Watermark.EventCounter;
			}
			if (flag)
			{
				base.RequestRecovery();
			}
		}

		// Token: 0x06004719 RID: 18201 RVA: 0x0012E7DC File Offset: 0x0012C9DC
		internal override IRecoveryEventSink StartRecovery()
		{
			this.CheckDisposed(null);
			lock (this.thisLock)
			{
				if (!this.areThereRecoveryMissedEvents)
				{
					this.recoveryFirstMissedEventWatermark = this.firstMissedEventWatermark;
					this.recoveryLastMissedEventWatermark = this.lastMissedEventWatermark;
					this.areThereRecoveryMissedEvents = this.areThereMissedEvents;
					this.firstMissedEventWatermark = null;
					this.lastMissedEventWatermark = 0L;
					this.areThereMissedEvents = false;
				}
				this.state = EventQueue.State.Recovery;
			}
			return this;
		}

		// Token: 0x0600471A RID: 18202 RVA: 0x0012E86C File Offset: 0x0012CA6C
		internal override EventWatermark GetCurrentEventWatermark()
		{
			EventWatermark result;
			lock (this.thisLock)
			{
				if (this.recoveryQueue.Count != 0)
				{
					result = new EventWatermark(base.MdbGuid, this.recoveryQueue.Peek().MapiWatermark, false);
				}
				else if (this.areThereRecoveryMissedEvents)
				{
					result = this.recoveryFirstMissedEventWatermark;
				}
				else if (this.mainQueue.Count != 0)
				{
					result = new EventWatermark(base.MdbGuid, this.mainQueue.Peek().MapiWatermark, false);
				}
				else if (this.areThereMissedEvents)
				{
					result = this.firstMissedEventWatermark;
				}
				else if (base.FirstEventToConsumeWatermark > this.lastKnownWatermark)
				{
					result = new EventWatermark(base.MdbGuid, base.FirstEventToConsumeWatermark, false);
				}
				else
				{
					result = new EventWatermark(base.MdbGuid, this.lastKnownWatermark, true);
				}
			}
			return result;
		}

		// Token: 0x0600471B RID: 18203 RVA: 0x0012E95C File Offset: 0x0012CB5C
		internal override void SetLastKnownWatermark(long mapiWatermark, bool mayInitiateRecovery)
		{
			bool flag = false;
			lock (this.thisLock)
			{
				this.lastKnownWatermark = mapiWatermark;
				if (this.startInRecovery && mayInitiateRecovery)
				{
					this.lastMissedEventWatermark = mapiWatermark;
					this.startInRecovery = false;
					flag = true;
					this.state = EventQueue.State.NeedRecovery;
				}
			}
			if (flag)
			{
				base.RequestRecovery();
			}
		}

		// Token: 0x0600471C RID: 18204 RVA: 0x0012E9CC File Offset: 0x0012CBCC
		internal override void SetFirstEventToConsumeOnSink(long firstEventToConsumeWatermark)
		{
			lock (this.thisLock)
			{
				base.FirstEventToConsumeWatermark = firstEventToConsumeWatermark;
			}
		}

		// Token: 0x0600471D RID: 18205 RVA: 0x0012EA10 File Offset: 0x0012CC10
		bool IRecoveryEventSink.RecoveryConsume(MapiEvent mapiEvent)
		{
			this.CheckDisposed(null);
			base.CheckForFinalEvents(mapiEvent);
			if (base.IsEventRelevant(mapiEvent))
			{
				Event item = new Event(base.MdbGuid, mapiEvent);
				lock (this.thisLock)
				{
					this.recoveryFirstMissedEventWatermark = new EventWatermark(base.MdbGuid, mapiEvent.Watermark.EventCounter, true);
					this.recoveryQueue.Enqueue(item);
					this.SetEventAvailable();
					if (this.recoveryQueue.Count == this.maxQueueSize)
					{
						this.state = EventQueue.State.Normal;
						return false;
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x0600471E RID: 18206 RVA: 0x0012EAC0 File Offset: 0x0012CCC0
		void IRecoveryEventSink.EndRecovery()
		{
			lock (this.thisLock)
			{
				this.state = EventQueue.State.Normal;
				this.areThereRecoveryMissedEvents = false;
			}
		}

		// Token: 0x170014B4 RID: 5300
		// (get) Token: 0x0600471F RID: 18207 RVA: 0x0012EB08 File Offset: 0x0012CD08
		EventWatermark IRecoveryEventSink.FirstMissedEventWatermark
		{
			get
			{
				this.CheckDisposed(null);
				return this.recoveryFirstMissedEventWatermark;
			}
		}

		// Token: 0x170014B5 RID: 5301
		// (get) Token: 0x06004720 RID: 18208 RVA: 0x0012EB17 File Offset: 0x0012CD17
		long IRecoveryEventSink.LastMissedEventWatermark
		{
			get
			{
				this.CheckDisposed(null);
				return this.recoveryLastMissedEventWatermark;
			}
		}

		// Token: 0x170014B6 RID: 5302
		// (get) Token: 0x06004721 RID: 18209 RVA: 0x0012EB26 File Offset: 0x0012CD26
		// (set) Token: 0x06004722 RID: 18210 RVA: 0x0012EB2D File Offset: 0x0012CD2D
		public static TimeSpan PollingInterval
		{
			get
			{
				return EventPump.PollingTimeSpan;
			}
			set
			{
				EventPump.PollingTimeSpan = value;
			}
		}

		// Token: 0x06004723 RID: 18211 RVA: 0x0012EB6C File Offset: 0x0012CD6C
		public static EventQueue Create(StoreSession session, EventCondition condition, int maxQueueSize)
		{
			return EventSink.InternalCreateEventSink<EventQueue>(session, null, () => new EventQueue(session.MailboxGuid, session is PublicFolderSession, condition, maxQueueSize, null));
		}

		// Token: 0x06004724 RID: 18212 RVA: 0x0012EBE8 File Offset: 0x0012CDE8
		public static EventQueue Create(StoreSession session, EventCondition condition, int maxQueueSize, EventWatermark watermark)
		{
			if (watermark == null)
			{
				throw new ArgumentNullException("watermark");
			}
			return EventSink.InternalCreateEventSink<EventQueue>(session, watermark, () => new EventQueue(session.MailboxGuid, session is PublicFolderSession, condition, maxQueueSize, watermark));
		}

		// Token: 0x170014B7 RID: 5303
		// (get) Token: 0x06004725 RID: 18213 RVA: 0x0012EC48 File Offset: 0x0012CE48
		public bool HasMissedEvents
		{
			get
			{
				this.CheckDisposed(null);
				bool result;
				lock (this.thisLock)
				{
					result = (this.areThereMissedEvents || this.areThereRecoveryMissedEvents);
				}
				return result;
			}
		}

		// Token: 0x170014B8 RID: 5304
		// (get) Token: 0x06004726 RID: 18214 RVA: 0x0012EC9C File Offset: 0x0012CE9C
		public int CurrentEventsCount
		{
			get
			{
				this.CheckDisposed(null);
				return this.currentEventsCount;
			}
		}

		// Token: 0x06004727 RID: 18215 RVA: 0x0012ECAC File Offset: 0x0012CEAC
		public Event GetEvent()
		{
			this.CheckDisposed(null);
			Event result = null;
			bool flag = false;
			lock (this.thisLock)
			{
				try
				{
					base.CheckException();
				}
				finally
				{
					if (base.IsExceptionPresent)
					{
						this.ResetEventAvailable();
					}
				}
				if (this.recoveryQueue.Count != 0)
				{
					result = this.recoveryQueue.Dequeue();
					if (this.recoveryQueue.Count == 0)
					{
						if (this.areThereRecoveryMissedEvents || this.mainQueue.Count == 0)
						{
							this.ResetEventAvailable();
						}
						if (this.NeedToRecover())
						{
							this.state = EventQueue.State.NeedRecovery;
							flag = true;
						}
					}
				}
				else if (this.mainQueue.Count != 0 && !this.areThereRecoveryMissedEvents)
				{
					result = this.mainQueue.Dequeue();
					if (this.mainQueue.Count == 0)
					{
						this.ResetEventAvailable();
						if (this.NeedToRecover())
						{
							this.state = EventQueue.State.NeedRecovery;
							flag = true;
						}
					}
				}
				this.currentEventsCount--;
			}
			if (flag)
			{
				base.RequestRecovery();
			}
			return result;
		}

		// Token: 0x170014B9 RID: 5305
		// (get) Token: 0x06004728 RID: 18216 RVA: 0x0012EDC8 File Offset: 0x0012CFC8
		public WaitHandle EventAvailableWaitHandle
		{
			get
			{
				this.CheckDisposed(null);
				if (this.eventAvailableEvent == null)
				{
					this.eventAvailableEvent = new ManualResetEvent(false);
				}
				return this.eventAvailableEvent;
			}
		}

		// Token: 0x06004729 RID: 18217 RVA: 0x0012EDEB File Offset: 0x0012CFEB
		public void RegisterEventAvailableHandler(EventQueue.EventAvailableHandler handler)
		{
			this.CheckDisposed(null);
			this.eventAvailable = handler;
		}

		// Token: 0x0600472A RID: 18218 RVA: 0x0012EDFC File Offset: 0x0012CFFC
		public bool IsQueueEmptyAndUpToDate()
		{
			this.CheckDisposed(null);
			bool result;
			lock (this.thisLock)
			{
				result = (this.mainQueue.Count == 0 && this.recoveryQueue.Count == 0 && !this.areThereMissedEvents && !this.areThereRecoveryMissedEvents);
			}
			return result;
		}

		// Token: 0x0600472B RID: 18219 RVA: 0x0012EE70 File Offset: 0x0012D070
		public void ResetQueue()
		{
			this.CheckDisposed(null);
			lock (this.thisLock)
			{
				this.mainQueue.Clear();
				this.recoveryQueue.Clear();
				this.areThereMissedEvents = false;
				this.areThereRecoveryMissedEvents = false;
				this.lastMissedEventWatermark = 0L;
				this.recoveryFirstMissedEventWatermark = null;
				this.recoveryLastMissedEventWatermark = 0L;
				this.startInRecovery = false;
				this.lastKnownWatermark = 0L;
				this.currentEventsCount = 0;
				this.state = EventQueue.State.Normal;
				this.ResetEventAvailable();
			}
		}

		// Token: 0x0600472C RID: 18220 RVA: 0x0012EF10 File Offset: 0x0012D110
		private void SetEventAvailable()
		{
			if (Interlocked.Exchange(ref this.eventAvailableCount, 1) == 0 && this.eventAvailable != null)
			{
				this.eventAvailable();
			}
			if (this.eventAvailableEvent != null)
			{
				lock (this.lockEventAvailableEvent)
				{
					if (this.eventAvailableEvent != null)
					{
						this.eventAvailableEvent.Set();
					}
				}
			}
		}

		// Token: 0x0600472D RID: 18221 RVA: 0x0012EF8C File Offset: 0x0012D18C
		private void ResetEventAvailable()
		{
			Interlocked.Exchange(ref this.eventAvailableCount, 0);
			if (this.eventAvailableEvent != null)
			{
				lock (this.lockEventAvailableEvent)
				{
					if (this.eventAvailableEvent != null)
					{
						this.eventAvailableEvent.Reset();
					}
				}
			}
		}

		// Token: 0x0600472E RID: 18222 RVA: 0x0012EFF0 File Offset: 0x0012D1F0
		private bool NeedToRecover()
		{
			return this.state == EventQueue.State.Normal && (this.areThereRecoveryMissedEvents || (this.mainQueue.Count == 0 && this.areThereMissedEvents));
		}

		// Token: 0x040026DD RID: 9949
		public const int DefaultEventQueueSize = 10;

		// Token: 0x040026DE RID: 9950
		private readonly Queue<Event> mainQueue;

		// Token: 0x040026DF RID: 9951
		private long lastMissedEventWatermark;

		// Token: 0x040026E0 RID: 9952
		private bool areThereMissedEvents;

		// Token: 0x040026E1 RID: 9953
		private readonly Queue<Event> recoveryQueue;

		// Token: 0x040026E2 RID: 9954
		private EventWatermark recoveryFirstMissedEventWatermark;

		// Token: 0x040026E3 RID: 9955
		private long recoveryLastMissedEventWatermark;

		// Token: 0x040026E4 RID: 9956
		private bool areThereRecoveryMissedEvents;

		// Token: 0x040026E5 RID: 9957
		private bool startInRecovery;

		// Token: 0x040026E6 RID: 9958
		private EventQueue.State state;

		// Token: 0x040026E7 RID: 9959
		private ManualResetEvent eventAvailableEvent;

		// Token: 0x040026E8 RID: 9960
		private readonly object lockEventAvailableEvent = new object();

		// Token: 0x040026E9 RID: 9961
		private readonly int maxQueueSize;

		// Token: 0x040026EA RID: 9962
		private readonly object thisLock = new object();

		// Token: 0x040026EB RID: 9963
		private long lastKnownWatermark;

		// Token: 0x040026EC RID: 9964
		private EventQueue.EventAvailableHandler eventAvailable;

		// Token: 0x040026ED RID: 9965
		private int eventAvailableCount;

		// Token: 0x040026EE RID: 9966
		private int currentEventsCount;

		// Token: 0x02000704 RID: 1796
		// (Invoke) Token: 0x06004730 RID: 18224
		public delegate void EventAvailableHandler();

		// Token: 0x02000705 RID: 1797
		private enum State
		{
			// Token: 0x040026F0 RID: 9968
			Normal,
			// Token: 0x040026F1 RID: 9969
			NeedRecovery,
			// Token: 0x040026F2 RID: 9970
			Recovery
		}
	}
}
