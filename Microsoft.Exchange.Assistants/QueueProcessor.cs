using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics.Components.Assistants;

namespace Microsoft.Exchange.Assistants
{
	// Token: 0x0200003C RID: 60
	internal abstract class QueueProcessor<T> : Base, IDisposable
	{
		// Token: 0x06000223 RID: 547 RVA: 0x0000C150 File Offset: 0x0000A350
		internal static void SetTestHookBeforeOnTransientFailure(Action testhook)
		{
			QueueProcessor<T>.syncWithTestCodeBeforeOnTransientFailure = testhook;
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000C158 File Offset: 0x0000A358
		internal static void SetTestHookAfterOnCompletedItem(Action testhook)
		{
			QueueProcessor<T>.syncWithTestCodeAfterOnCompletedItem = testhook;
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000C160 File Offset: 0x0000A360
		protected QueueProcessor(ThrottleGovernor governor)
		{
			this.governor = governor;
		}

		// Token: 0x1700009D RID: 157
		// (get) Token: 0x06000226 RID: 550 RVA: 0x0000C191 File Offset: 0x0000A391
		protected ThrottleGovernor Governor
		{
			get
			{
				return this.governor;
			}
		}

		// Token: 0x1700009E RID: 158
		// (get) Token: 0x06000227 RID: 551 RVA: 0x0000C199 File Offset: 0x0000A399
		protected Throttle Throttle
		{
			get
			{
				return this.governor.Throttle;
			}
		}

		// Token: 0x1700009F RID: 159
		// (get) Token: 0x06000228 RID: 552 RVA: 0x0000C1A6 File Offset: 0x0000A3A6
		protected object Locker
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170000A0 RID: 160
		// (get) Token: 0x06000229 RID: 553 RVA: 0x0000C1A9 File Offset: 0x0000A3A9
		protected List<T> PendingQueue
		{
			get
			{
				return this.pendingQueue;
			}
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x0600022A RID: 554 RVA: 0x0000C1B1 File Offset: 0x0000A3B1
		protected List<T> ActiveQueue
		{
			get
			{
				return this.activeQueue;
			}
		}

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x0600022B RID: 555 RVA: 0x0000C1B9 File Offset: 0x0000A3B9
		protected int PendingWorkers
		{
			get
			{
				return this.pendingWorkers;
			}
		}

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x0600022C RID: 556 RVA: 0x0000C1C1 File Offset: 0x0000A3C1
		protected int ActiveWorkers
		{
			get
			{
				return this.activeWorkers;
			}
		}

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x0600022D RID: 557
		protected abstract bool Shutdown { get; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x0600022E RID: 558
		protected abstract PerformanceCountersPerDatabaseInstance PerformanceCounters { get; }

		// Token: 0x0600022F RID: 559 RVA: 0x0000C1CC File Offset: 0x0000A3CC
		public void WaitForShutdown()
		{
			lock (this.Locker)
			{
				if (this.activeWorkers == 0)
				{
					ExTraceGlobals.QueueProcessorTracer.TraceDebug<QueueProcessor<T>>((long)this.GetHashCode(), "{0}: Clear for shutdown", this);
					this.shutdownEvent.Set();
				}
				else
				{
					ExTraceGlobals.QueueProcessorTracer.TraceDebug<QueueProcessor<T>, int>((long)this.GetHashCode(), "{0}: Waiting for {1} workers to exit...", this, this.activeWorkers);
				}
			}
			this.shutdownEvent.WaitOne();
			base.TracePfd("PFD AIS {0} {1}: finished shutting down", new object[]
			{
				22615,
				this
			});
			ExTraceGlobals.QueueProcessorTracer.TraceDebug<QueueProcessor<T>>((long)this.GetHashCode(), "{0}: Workers have exited.", this);
		}

		// Token: 0x06000230 RID: 560 RVA: 0x0000C298 File Offset: 0x0000A498
		public void EnqueueItem(T item)
		{
			lock (this.Locker)
			{
				ExTraceGlobals.QueueProcessorTracer.TraceDebug<QueueProcessor<T>, T>((long)this.GetHashCode(), "{0}: Enqueing item: {1}", this, item);
				this.OnEnqueueItem(item);
				this.pendingQueue.Add(item);
				this.QueueWorkersIfNecessary();
			}
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000C304 File Offset: 0x0000A504
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000232 RID: 562
		protected abstract AIException ProcessItem(T item);

		// Token: 0x06000233 RID: 563 RVA: 0x0000C30D File Offset: 0x0000A50D
		protected virtual void OnEnqueueItem(T item)
		{
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000C30F File Offset: 0x0000A50F
		protected virtual void OnCompletedItem(T item, AIException exception)
		{
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000C311 File Offset: 0x0000A511
		protected virtual void OnTransientFailure(T item, AIException exception)
		{
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000C313 File Offset: 0x0000A513
		protected virtual void OnWorkersStarted()
		{
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000C315 File Offset: 0x0000A515
		protected virtual void OnWorkersClear()
		{
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000C317 File Offset: 0x0000A517
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.governor.Dispose();
			}
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000C327 File Offset: 0x0000A527
		private static void Worker(object state)
		{
			((QueueProcessor<T>)state).Worker();
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000C334 File Offset: 0x0000A534
		private void QueueWorkersIfNecessary()
		{
			int num = this.Shutdown ? 0 : Math.Min(this.Throttle.OpenThrottleValue, this.pendingQueue.Count + this.activeQueue.Count);
			ExTraceGlobals.QueueProcessorTracer.TraceDebug((long)this.GetHashCode(), "{0}: desiredWorkers: {1}, pending: {2}, active: {3}", new object[]
			{
				this,
				num,
				this.pendingWorkers,
				this.activeWorkers
			});
			while (this.pendingWorkers + this.activeWorkers < num)
			{
				this.Throttle.QueueUserWorkItem(QueueProcessor<T>.workerCallback, this);
				this.pendingWorkers++;
				ExTraceGlobals.QueueProcessorTracer.TraceDebug((long)this.GetHashCode(), "{0}: Queued worker. desired: {1}, pending: {2}, active: {3}", new object[]
				{
					this,
					num,
					this.pendingWorkers,
					this.activeWorkers
				});
			}
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000C434 File Offset: 0x0000A634
		private void Worker()
		{
			lock (this.Locker)
			{
				this.pendingWorkers--;
				if (this.Shutdown)
				{
					ExTraceGlobals.QueueProcessorTracer.TraceDebug<QueueProcessor<T>>((long)this.GetHashCode(), "{0}: Shutdown before Worker could start.", this);
					return;
				}
				this.PerformanceCounters.NumberOfThreadsUsed.Increment();
				if (++this.activeWorkers == 1)
				{
					ExTraceGlobals.QueueProcessorTracer.TraceDebug<QueueProcessor<T>>((long)this.GetHashCode(), "{0}: First worker entering", this);
					this.OnWorkersStarted();
				}
				ExTraceGlobals.QueueProcessorTracer.TraceDebug<QueueProcessor<T>, int>((long)this.GetHashCode(), "{0}: New active worker #{1}", this, this.activeWorkers);
				int num = 0;
				while (!this.Shutdown && !this.Throttle.IsOverThrottle && this.pendingQueue.Count > 0 && num < 128)
				{
					this.ProcessOneItem();
					num++;
				}
				ExTraceGlobals.QueueProcessorTracer.TraceDebug<QueueProcessor<T>>((long)this.GetHashCode(), "{0}: Out of worker loop", this);
				if (--this.activeWorkers == 0)
				{
					ExTraceGlobals.QueueProcessorTracer.TraceDebug<QueueProcessor<T>>((long)this.GetHashCode(), "{0}: Last worker exiting", this);
					this.OnWorkersClear();
					if (this.Shutdown)
					{
						ExTraceGlobals.QueueProcessorTracer.TraceDebug<QueueProcessor<T>>((long)this.GetHashCode(), "{0}: Last worker setting shutdownEvent", this);
						this.shutdownEvent.Set();
					}
				}
				this.QueueWorkersIfNecessary();
			}
			this.PerformanceCounters.NumberOfThreadsUsed.Decrement();
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000C5D0 File Offset: 0x0000A7D0
		private void ProcessOneItem()
		{
			bool flag = false;
			T t = this.pendingQueue[0];
			this.pendingQueue.RemoveAt(0);
			this.activeQueue.Add(t);
			ExTraceGlobals.QueueProcessorTracer.TraceDebug<QueueProcessor<T>, T>((long)this.GetHashCode(), "{0}: ProcessOneItem releasing lock for item: {1}", this, t);
			Monitor.Exit(this.Locker);
			try
			{
				AIException ex = this.ProcessItem(t);
				ExTraceGlobals.QueueProcessorTracer.TraceDebug<QueueProcessor<T>, T, AIException>((long)this.GetHashCode(), "{0}: Item: {1}, Result: {2}", this, t, ex);
				flag = this.governor.ReportResult(ex);
				if (flag)
				{
					ExTraceGlobals.QueueProcessorTracer.TraceDebug<QueueProcessor<T>, T, AIException>((long)this.GetHashCode(), "{0}: OpComplete, Item: {1}, Result: {2}", this, t, ex);
					this.OnCompletedItem(t, ex);
					if (QueueProcessor<T>.syncWithTestCodeAfterOnCompletedItem != null)
					{
						QueueProcessor<T>.syncWithTestCodeAfterOnCompletedItem();
					}
				}
				else
				{
					ExTraceGlobals.QueueProcessorTracer.TraceDebug<QueueProcessor<T>, T, AIException>((long)this.GetHashCode(), "{0}: WillRetry, Item: {1}, Result: {2}", this, t, ex);
					if (QueueProcessor<T>.syncWithTestCodeBeforeOnTransientFailure != null)
					{
						QueueProcessor<T>.syncWithTestCodeBeforeOnTransientFailure();
					}
					this.OnTransientFailure(t, ex);
				}
			}
			finally
			{
				Monitor.Enter(this.Locker);
				ExTraceGlobals.QueueProcessorTracer.TraceDebug<QueueProcessor<T>, T>((long)this.GetHashCode(), "{0}: ProcessOneItem reacquire lock for item: {1}", this, t);
				this.activeQueue.Remove(t);
				if (!flag)
				{
					ExTraceGlobals.QueueProcessorTracer.TraceDebug<QueueProcessor<T>, T>((long)this.GetHashCode(), "{0}: Requeing item for retry: {1}", this, t);
					this.pendingQueue.Insert(0, t);
					this.QueueWorkersIfNecessary();
				}
			}
		}

		// Token: 0x0400019A RID: 410
		private const int MaximumEventsProcessedPerThread = 128;

		// Token: 0x0400019B RID: 411
		private static WaitCallback workerCallback = new WaitCallback(QueueProcessor<T>.Worker);

		// Token: 0x0400019C RID: 412
		private static Action syncWithTestCodeBeforeOnTransientFailure = null;

		// Token: 0x0400019D RID: 413
		private static Action syncWithTestCodeAfterOnCompletedItem = null;

		// Token: 0x0400019E RID: 414
		private ThrottleGovernor governor;

		// Token: 0x0400019F RID: 415
		private List<T> pendingQueue = new List<T>();

		// Token: 0x040001A0 RID: 416
		private List<T> activeQueue = new List<T>();

		// Token: 0x040001A1 RID: 417
		private FastManualResetEvent shutdownEvent = new FastManualResetEvent(false);

		// Token: 0x040001A2 RID: 418
		private int pendingWorkers;

		// Token: 0x040001A3 RID: 419
		private int activeWorkers;
	}
}
