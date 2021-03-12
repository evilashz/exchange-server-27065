using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Cluster.Replay;

namespace Microsoft.Exchange.Cluster.Replay
{
	// Token: 0x02000127 RID: 295
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal class PrioritizedQueue<TCallback> where TCallback : IQueuedCallback
	{
		// Token: 0x17000280 RID: 640
		// (get) Token: 0x06000B4C RID: 2892 RVA: 0x00031E46 File Offset: 0x00030046
		// (set) Token: 0x06000B4D RID: 2893 RVA: 0x00031E4E File Offset: 0x0003004E
		internal bool IsEnabled { get; set; }

		// Token: 0x17000281 RID: 641
		// (get) Token: 0x06000B4E RID: 2894 RVA: 0x00031E58 File Offset: 0x00030058
		internal bool IsIdle
		{
			get
			{
				bool result;
				lock (this.m_locker)
				{
					result = (this.m_highPriorityQueue.Count == 0 && this.m_queue.Count == 0 && !this.m_fInProcessing);
				}
				return result;
			}
		}

		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000B4F RID: 2895 RVA: 0x00031EBC File Offset: 0x000300BC
		internal TCallback ItemInProcessing
		{
			get
			{
				TCallback result;
				lock (this.m_locker)
				{
					result = ((this.m_itemInProcessing != null) ? this.m_itemInProcessing.Callback : default(TCallback));
				}
				return result;
			}
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x00031F18 File Offset: 0x00030118
		public PrioritizedQueue()
		{
			this.m_locker = new object();
			this.m_highPriorityQueue = new List<PrioritizedQueue<TCallback>.QueuedItem<TCallback>>();
			this.m_queue = new List<PrioritizedQueue<TCallback>.QueuedItem<TCallback>>();
			this.IsEnabled = true;
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x00031F48 File Offset: 0x00030148
		public void PrepareToStop()
		{
			lock (this.m_locker)
			{
				this.IsEnabled = false;
				if (this.ItemInProcessing != null)
				{
					TCallback itemInProcessing = this.ItemInProcessing;
					itemInProcessing.Cancel();
				}
				this.m_fPrepareToStopCalled = true;
			}
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x00031FB4 File Offset: 0x000301B4
		public void Stop()
		{
			lock (this.m_locker)
			{
				if (!this.m_fPrepareToStopCalled)
				{
					this.PrepareToStop();
				}
				goto IL_32;
			}
			IL_2B:
			Thread.Sleep(50);
			IL_32:
			if (this.IsIdle)
			{
				return;
			}
			goto IL_2B;
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x0003200C File Offset: 0x0003020C
		public bool EnqueueHighPriority(TCallback callback, EventWaitHandle waitHandle)
		{
			return this.EnqueueInternal(callback, waitHandle, true);
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x00032017 File Offset: 0x00030217
		public bool Enqueue(TCallback callback, EventWaitHandle waitHandle)
		{
			return this.EnqueueInternal(callback, waitHandle, false);
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x00032024 File Offset: 0x00030224
		public bool EnqueueUniqueItem(TCallback callback, EventWaitHandle waitHandle)
		{
			TCallback tcallback;
			EventWaitHandle eventWaitHandle;
			return this.EnqueueUniqueItem(callback, waitHandle, false, out tcallback, out eventWaitHandle);
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x000320A0 File Offset: 0x000302A0
		public bool EnqueueUniqueItem(TCallback callback, EventWaitHandle waitHandle, bool includeInProgressItem, out TCallback existingItem, out EventWaitHandle existingWaitHandle)
		{
			bool result = false;
			existingItem = default(TCallback);
			existingWaitHandle = null;
			lock (this.m_locker)
			{
				PrioritizedQueue<TCallback>.QueuedItem<TCallback> queuedItem = null;
				if (includeInProgressItem && this.m_itemInProcessing != null)
				{
					TCallback callback2 = this.m_itemInProcessing.Callback;
					if (callback2.IsEquivalentOrSuperset(callback))
					{
						queuedItem = this.m_itemInProcessing;
					}
				}
				if (queuedItem == null)
				{
					queuedItem = this.m_highPriorityQueue.Find(delegate(PrioritizedQueue<TCallback>.QueuedItem<TCallback> item)
					{
						TCallback callback3 = item.Callback;
						return callback3.IsEquivalentOrSuperset(callback);
					});
					if (queuedItem == null)
					{
						queuedItem = this.m_queue.Find(delegate(PrioritizedQueue<TCallback>.QueuedItem<TCallback> item)
						{
							TCallback callback3 = item.Callback;
							return callback3.IsEquivalentOrSuperset(callback);
						});
					}
				}
				if (queuedItem == null)
				{
					ExTraceGlobals.ReplayManagerTracer.TraceDebug<string>((long)this.GetHashCode(), "EnqueueUniqueItem: Enqueueing item of type '{0}' because it isn't already in the queue.", callback.GetType().Name);
					result = this.Enqueue(callback, waitHandle);
				}
				if (queuedItem != null)
				{
					ExTraceGlobals.ReplayManagerTracer.TraceDebug<string>((long)this.GetHashCode(), "EnqueueUniqueItem: Item of type '{0}' was not enqueued because another equivalent one is already in the queue. Returning that one to wait on.", callback.GetType().Name);
					callback.Cancel();
					existingItem = queuedItem.Callback;
					existingWaitHandle = queuedItem.CompletedEvent;
				}
			}
			return result;
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x00032224 File Offset: 0x00030424
		private bool EnqueueInternal(TCallback callback, EventWaitHandle waitHandle, bool isHighPriority)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback", "Cannot enqueue a null callback.");
			}
			bool result = false;
			lock (this.m_locker)
			{
				if (!this.IsEnabled)
				{
					ExTraceGlobals.ReplayManagerTracer.TraceDebug<string>((long)this.GetHashCode(), "EnqueueInternal: Item of type '{0}' not enqueued because the queue ha been disabled. Cancelling it now.", callback.GetType().Name);
					callback.Cancel();
					return false;
				}
				PrioritizedQueue<TCallback>.QueuedItem<TCallback> item = new PrioritizedQueue<TCallback>.QueuedItem<TCallback>(callback, waitHandle);
				result = true;
				if (isHighPriority)
				{
					this.m_highPriorityQueue.Add(item);
					ExTraceGlobals.ReplayManagerTracer.TraceDebug<string>((long)this.GetHashCode(), "EnqueueInternal: Item of type '{0}' enqueued into the high-priority queue.", callback.GetType().Name);
				}
				else
				{
					this.m_queue.Add(item);
					ExTraceGlobals.ReplayManagerTracer.TraceDebug<string>((long)this.GetHashCode(), "EnqueueInternal: Item of type '{0}' enqueued into the normal-priority queue.", callback.GetType().Name);
				}
				if (!this.m_fInProcessing)
				{
					this.m_fInProcessing = true;
					this.QueueProcessingThread();
				}
				else
				{
					ExTraceGlobals.ReplayManagerTracer.TraceDebug((long)this.GetHashCode(), "EnqueueInternal: Processing thread is currently busy. Skipping QueueProcessingThread()...");
				}
			}
			return result;
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x00032368 File Offset: 0x00030568
		private void QueueProcessingThread()
		{
			ThreadPool.QueueUserWorkItem(new WaitCallback(this.ProcessItems));
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x0003237C File Offset: 0x0003057C
		private void ProcessItems(object stateIgnored)
		{
			PrioritizedQueue<TCallback>.QueuedItem<TCallback> queuedItem = null;
			for (;;)
			{
				lock (this.m_locker)
				{
					ExTraceGlobals.ReplayManagerTracer.TraceDebug<int, int>((long)this.GetHashCode(), "ProcessItems: Number of high-priority items: {0}; Number of normal-priority items: {1}", this.m_highPriorityQueue.Count, this.m_queue.Count);
					if (this.m_highPriorityQueue.Count > 0)
					{
						queuedItem = this.Dequeue(this.m_highPriorityQueue);
						this.m_itemInProcessing = queuedItem;
						Trace replayManagerTracer = ExTraceGlobals.ReplayManagerTracer;
						long id = (long)this.GetHashCode();
						string formatString = "ProcessItems: Dequeued a high-priority operation of type '{0}'.";
						TCallback callback = queuedItem.Callback;
						replayManagerTracer.TraceDebug<string>(id, formatString, callback.GetType().Name);
					}
					else
					{
						if (this.m_queue.Count <= 0)
						{
							this.m_itemInProcessing = null;
							this.m_fInProcessing = false;
							break;
						}
						queuedItem = this.Dequeue(this.m_queue);
						this.m_itemInProcessing = queuedItem;
						Trace replayManagerTracer2 = ExTraceGlobals.ReplayManagerTracer;
						long id2 = (long)this.GetHashCode();
						string formatString2 = "ProcessItems: Dequeued a normal-priority operation of type '{0}'.";
						TCallback callback2 = queuedItem.Callback;
						replayManagerTracer2.TraceDebug<string>(id2, formatString2, callback2.GetType().Name);
					}
				}
				if (this.IsEnabled)
				{
					this.RunOperation(queuedItem);
				}
				else
				{
					Trace replayManagerTracer3 = ExTraceGlobals.ReplayManagerTracer;
					long id3 = (long)this.GetHashCode();
					string formatString3 = "ProcessItems: Cancelling operation of type '{0}' because the queue is no longer enabled.";
					TCallback callback3 = queuedItem.Callback;
					replayManagerTracer3.TraceDebug<string>(id3, formatString3, callback3.GetType().Name);
					TCallback callback4 = queuedItem.Callback;
					callback4.Cancel();
				}
			}
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x000324FC File Offset: 0x000306FC
		private PrioritizedQueue<TCallback>.QueuedItem<TCallback> Dequeue(List<PrioritizedQueue<TCallback>.QueuedItem<TCallback>> queue)
		{
			PrioritizedQueue<TCallback>.QueuedItem<TCallback> result = null;
			if (queue.Count > 0)
			{
				result = queue[0];
				queue.RemoveAt(0);
			}
			return result;
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x00032524 File Offset: 0x00030724
		private void RunOperation(PrioritizedQueue<TCallback>.QueuedItem<TCallback> item)
		{
			bool flag = true;
			try
			{
				TCallback callback = item.Callback;
				callback.ReportStatus(QueuedItemStatus.Started);
				TCallback callback2 = item.Callback;
				callback2.StartTimeUtc = DateTime.UtcNow;
				TCallback callback3 = item.Callback;
				callback3.Execute();
				TCallback callback4 = item.Callback;
				if (callback4.LastException != null)
				{
					Trace replayManagerTracer = ExTraceGlobals.ReplayManagerTracer;
					long id = (long)this.GetHashCode();
					string formatString = "PrioritizedQueue.RunOperation: Operation of type '{0}' returned exception: {1}";
					TCallback callback5 = item.Callback;
					string name = callback5.GetType().Name;
					TCallback callback6 = item.Callback;
					replayManagerTracer.TraceDebug<string, Exception>(id, formatString, name, callback6.LastException);
				}
				else
				{
					Trace replayManagerTracer2 = ExTraceGlobals.ReplayManagerTracer;
					long id2 = (long)this.GetHashCode();
					string formatString2 = "PrioritizedQueue.RunOperation: Operation of type '{0}' completed successfully without any errors.";
					TCallback callback7 = item.Callback;
					replayManagerTracer2.TraceDebug<string>(id2, formatString2, callback7.GetType().Name);
				}
				flag = false;
				TCallback callback8 = item.Callback;
				callback8.ReportStatus(QueuedItemStatus.Completed);
			}
			catch (OperationAbortedException)
			{
				flag = false;
				TCallback callback9 = item.Callback;
				callback9.ReportStatus(QueuedItemStatus.Cancelled);
			}
			finally
			{
				TCallback callback10 = item.Callback;
				callback10.EndTimeUtc = DateTime.UtcNow;
				if (flag)
				{
					TCallback callback11 = item.Callback;
					callback11.ReportStatus(QueuedItemStatus.Failed);
				}
				if (item.CompletedEvent != null)
				{
					item.CompletedEvent.Set();
				}
			}
		}

		// Token: 0x040004B1 RID: 1201
		private List<PrioritizedQueue<TCallback>.QueuedItem<TCallback>> m_queue;

		// Token: 0x040004B2 RID: 1202
		private List<PrioritizedQueue<TCallback>.QueuedItem<TCallback>> m_highPriorityQueue;

		// Token: 0x040004B3 RID: 1203
		private PrioritizedQueue<TCallback>.QueuedItem<TCallback> m_itemInProcessing;

		// Token: 0x040004B4 RID: 1204
		private bool m_fInProcessing;

		// Token: 0x040004B5 RID: 1205
		private bool m_fPrepareToStopCalled;

		// Token: 0x040004B6 RID: 1206
		protected object m_locker;

		// Token: 0x02000128 RID: 296
		[ClassAccessLevel(AccessLevel.Implementation)]
		private class QueuedItem<T> where T : IQueuedCallback
		{
			// Token: 0x17000283 RID: 643
			// (get) Token: 0x06000B5C RID: 2908 RVA: 0x000326B8 File Offset: 0x000308B8
			// (set) Token: 0x06000B5D RID: 2909 RVA: 0x000326C0 File Offset: 0x000308C0
			internal T Callback { get; private set; }

			// Token: 0x17000284 RID: 644
			// (get) Token: 0x06000B5E RID: 2910 RVA: 0x000326C9 File Offset: 0x000308C9
			// (set) Token: 0x06000B5F RID: 2911 RVA: 0x000326D1 File Offset: 0x000308D1
			internal EventWaitHandle CompletedEvent { get; private set; }

			// Token: 0x06000B60 RID: 2912 RVA: 0x000326DA File Offset: 0x000308DA
			public QueuedItem(T callback, EventWaitHandle waitHandle)
			{
				this.Callback = callback;
				this.CompletedEvent = waitHandle;
			}
		}
	}
}
