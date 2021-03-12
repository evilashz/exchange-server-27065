using System;
using System.Threading;
using Microsoft.Exchange.Data.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Data.Storage;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000706 RID: 1798
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class EventSubscription : IDisposeTrackable, IDisposable
	{
		// Token: 0x06004733 RID: 18227 RVA: 0x0012F01C File Offset: 0x0012D21C
		internal EventSubscription(EventQueue eventQueue, IEventHandler eventHandler)
		{
			StorageGlobals.TraceConstructIDisposable(this);
			this.disposeTracker = this.GetDisposeTracker();
			this.eventQueue = eventQueue;
			this.eventQueue.RegisterEventAvailableHandler(new EventQueue.EventAvailableHandler(this.EventAvailableHandler));
			this.eventHandler = eventHandler;
			ExTraceGlobals.EventTracer.TraceDebug<EventSubscription>((long)this.GetHashCode(), "EventSubscription::Constructor. {0}", this);
		}

		// Token: 0x06004734 RID: 18228 RVA: 0x0012F088 File Offset: 0x0012D288
		public static EventSubscription Create(StoreSession session, EventCondition condition, IEventHandler eventHandler)
		{
			return EventSubscription.InternalCreate(session, condition, eventHandler, null);
		}

		// Token: 0x06004735 RID: 18229 RVA: 0x0012F093 File Offset: 0x0012D293
		public static EventSubscription Create(StoreSession session, EventCondition condition, IEventHandler eventHandler, EventWatermark watermark)
		{
			if (watermark == null)
			{
				throw new ArgumentNullException("watermark");
			}
			return EventSubscription.InternalCreate(session, condition, eventHandler, watermark);
		}

		// Token: 0x06004736 RID: 18230 RVA: 0x0012F0AC File Offset: 0x0012D2AC
		public virtual DisposeTracker GetDisposeTracker()
		{
			return DisposeTracker.Get<EventSubscription>(this);
		}

		// Token: 0x06004737 RID: 18231 RVA: 0x0012F0B4 File Offset: 0x0012D2B4
		public void SuppressDisposeTracker()
		{
			if (this.disposeTracker != null)
			{
				this.disposeTracker.Suppress();
			}
		}

		// Token: 0x06004738 RID: 18232 RVA: 0x0012F0CC File Offset: 0x0012D2CC
		private static EventSubscription InternalCreate(StoreSession session, EventCondition condition, IEventHandler eventHandler, EventWatermark watermark)
		{
			if (session == null)
			{
				throw new ArgumentNullException("session");
			}
			if (condition == null)
			{
				throw new ArgumentNullException("condition");
			}
			if (eventHandler == null)
			{
				throw new ArgumentNullException("eventHandler");
			}
			EventQueue eventQueue = null;
			if (watermark != null)
			{
				eventQueue = EventQueue.Create(session, condition, 10, watermark);
			}
			else
			{
				eventQueue = EventQueue.Create(session, condition, 10);
			}
			bool flag = false;
			EventSubscription result;
			try
			{
				EventSubscription eventSubscription = new EventSubscription(eventQueue, eventHandler);
				flag = true;
				result = eventSubscription;
			}
			finally
			{
				if (!flag && eventQueue != null)
				{
					eventQueue.Dispose();
					eventQueue = null;
				}
			}
			return result;
		}

		// Token: 0x06004739 RID: 18233 RVA: 0x0012F150 File Offset: 0x0012D350
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600473A RID: 18234 RVA: 0x0012F15F File Offset: 0x0012D35F
		private void Dispose(bool disposing)
		{
			StorageGlobals.TraceDispose(this, this.isDisposed, disposing);
			if (!this.isDisposed)
			{
				this.isDisposed = true;
				this.InternalDispose(disposing);
				ExTraceGlobals.EventTracer.TraceDebug<EventSubscription>((long)this.GetHashCode(), "EventSubscription::Dispose. {0}", this);
			}
		}

		// Token: 0x0600473B RID: 18235 RVA: 0x0012F19C File Offset: 0x0012D39C
		protected virtual void InternalDispose(bool disposing)
		{
			if (disposing)
			{
				lock (this.thisLock)
				{
					if (this.eventQueue != null)
					{
						this.eventQueue.Dispose();
					}
				}
				if (this.disposeTracker != null)
				{
					this.disposeTracker.Dispose();
				}
			}
		}

		// Token: 0x170014BA RID: 5306
		// (get) Token: 0x0600473C RID: 18236 RVA: 0x0012F200 File Offset: 0x0012D400
		protected bool IsDisposed
		{
			get
			{
				return this.isDisposed;
			}
		}

		// Token: 0x0600473D RID: 18237 RVA: 0x0012F208 File Offset: 0x0012D408
		public override string ToString()
		{
			return string.Format("Internal Event Queue = {0}.", this.eventQueue);
		}

		// Token: 0x0600473E RID: 18238 RVA: 0x0012F21C File Offset: 0x0012D41C
		private void CallBack(object state)
		{
			if (Interlocked.Increment(ref this.countWorkerThreads) > 1)
			{
				return;
			}
			LocalizedException ex;
			for (;;)
			{
				Event @event = null;
				ex = null;
				do
				{
					lock (this.thisLock)
					{
						if (this.IsDisposed)
						{
							ExTraceGlobals.EventTracer.TraceDebug<EventSubscription>((long)this.GetHashCode(), "EventSubscription::CallBack. {0}. Aborted notification delivery because EventSubscription was disposed.", this);
							return;
						}
						try
						{
							@event = this.eventQueue.GetEvent();
						}
						catch (StoragePermanentException ex2)
						{
							ex = ex2;
						}
						catch (StorageTransientException ex3)
						{
							ex = ex3;
						}
					}
					if (@event != null)
					{
						this.eventHandler.Consume(@event);
					}
					else if (ex != null)
					{
						goto Block_4;
					}
				}
				while (@event != null);
				if (Interlocked.Decrement(ref this.countWorkerThreads) <= 0)
				{
					return;
				}
			}
			Block_4:
			this.eventHandler.HandleException(ex);
		}

		// Token: 0x0600473F RID: 18239 RVA: 0x0012F2F4 File Offset: 0x0012D4F4
		private void EventAvailableHandler()
		{
			ThreadPool.QueueUserWorkItem(new WaitCallback(this.CallBack));
		}

		// Token: 0x040026F3 RID: 9971
		private readonly EventQueue eventQueue;

		// Token: 0x040026F4 RID: 9972
		private readonly IEventHandler eventHandler;

		// Token: 0x040026F5 RID: 9973
		private bool isDisposed;

		// Token: 0x040026F6 RID: 9974
		private readonly DisposeTracker disposeTracker;

		// Token: 0x040026F7 RID: 9975
		private readonly object thisLock = new object();

		// Token: 0x040026F8 RID: 9976
		private int countWorkerThreads;
	}
}
