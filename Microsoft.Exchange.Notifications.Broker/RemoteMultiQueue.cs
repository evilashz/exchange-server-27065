using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.Exchange.Data.Storage;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.Notifications.Broker;

namespace Microsoft.Exchange.Notifications.Broker
{
	// Token: 0x02000035 RID: 53
	[ClassAccessLevel(AccessLevel.Implementation)]
	public class RemoteMultiQueue : DisposeTrackableBase
	{
		// Token: 0x06000211 RID: 529 RVA: 0x0000C0A8 File Offset: 0x0000A2A8
		internal RemoteMultiQueue()
		{
			this.ReadyQueueSemaphore = new SemaphoreSlim(0);
			this.RetryTimer = new System.Timers.Timer(1000.0);
			this.InactiveTimer = new System.Timers.Timer(300000.0);
			this.RetryTimer.Elapsed += this.ProcessRetryTimer;
			this.InactiveTimer.Elapsed += this.ProcessInactiveTimer;
			this.AllQueues = new ConcurrentDictionary<string, RemoteQueue>();
			this.ActiveQueues = new Dictionary<string, RemoteQueue>();
			this.InactiveQueues = new Dictionary<string, RemoteQueue>();
			this.ReadyQueues = new Queue<RemoteQueue>();
			this.RetryQueues = new PriorityQueue<RemoteQueue>();
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000212 RID: 530 RVA: 0x0000C154 File Offset: 0x0000A354
		internal static RemoteMultiQueue Singleton
		{
			get
			{
				return RemoteMultiQueue.singleton;
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000213 RID: 531 RVA: 0x0000C15B File Offset: 0x0000A35B
		// (set) Token: 0x06000214 RID: 532 RVA: 0x0000C163 File Offset: 0x0000A363
		internal ConcurrentDictionary<string, RemoteQueue> AllQueues { get; private set; }

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000215 RID: 533 RVA: 0x0000C16C File Offset: 0x0000A36C
		// (set) Token: 0x06000216 RID: 534 RVA: 0x0000C174 File Offset: 0x0000A374
		internal Queue<RemoteQueue> ReadyQueues { get; private set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000217 RID: 535 RVA: 0x0000C17D File Offset: 0x0000A37D
		// (set) Token: 0x06000218 RID: 536 RVA: 0x0000C185 File Offset: 0x0000A385
		internal Dictionary<string, RemoteQueue> ActiveQueues { get; private set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000219 RID: 537 RVA: 0x0000C18E File Offset: 0x0000A38E
		// (set) Token: 0x0600021A RID: 538 RVA: 0x0000C196 File Offset: 0x0000A396
		internal Dictionary<string, RemoteQueue> InactiveQueues { get; private set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600021B RID: 539 RVA: 0x0000C19F File Offset: 0x0000A39F
		// (set) Token: 0x0600021C RID: 540 RVA: 0x0000C1A7 File Offset: 0x0000A3A7
		internal PriorityQueue<RemoteQueue> RetryQueues { get; private set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600021D RID: 541 RVA: 0x0000C1B0 File Offset: 0x0000A3B0
		// (set) Token: 0x0600021E RID: 542 RVA: 0x0000C1B8 File Offset: 0x0000A3B8
		internal System.Timers.Timer RetryTimer { get; private set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600021F RID: 543 RVA: 0x0000C1C1 File Offset: 0x0000A3C1
		// (set) Token: 0x06000220 RID: 544 RVA: 0x0000C1C9 File Offset: 0x0000A3C9
		internal System.Timers.Timer InactiveTimer { get; private set; }

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000221 RID: 545 RVA: 0x0000C1D2 File Offset: 0x0000A3D2
		// (set) Token: 0x06000222 RID: 546 RVA: 0x0000C1DA File Offset: 0x0000A3DA
		internal SemaphoreSlim ReadyQueueSemaphore { get; set; }

		// Token: 0x06000223 RID: 547 RVA: 0x0000C1F8 File Offset: 0x0000A3F8
		public void Put(BrokerNotification notification, RemoteMessenger remoteMessenger)
		{
			bool flag = false;
			RemoteQueue remoteQueue = null;
			string groupingKey = remoteMessenger.GroupingKey;
			lock (this.AllQueues)
			{
				try
				{
					if (this.AllQueues.TryGetValue(groupingKey, out remoteQueue))
					{
						if (remoteQueue.QueueState == QueueState.Inactive)
						{
							if (!this.InactiveQueues.Remove(remoteQueue.GroupingKey))
							{
								throw new InvalidOperationException();
							}
							flag = true;
						}
					}
					else
					{
						remoteQueue = this.AllQueues.GetOrAdd(groupingKey, (string value) => new RemoteQueue(remoteMessenger));
						flag = true;
					}
				}
				finally
				{
					remoteQueue.Put(notification);
					if (flag)
					{
						remoteQueue.QueueState = QueueState.Ready;
						this.ReadyQueues.Enqueue(remoteQueue);
						ExTraceGlobals.RemoteConduitTracer.TraceDebug((long)this.GetHashCode(), "RemoteMultiQueue.Put: about to release ReadyQueueSemaphore");
						this.ReadyQueueSemaphore.Release();
					}
				}
			}
		}

		// Token: 0x06000224 RID: 548 RVA: 0x0000C4A0 File Offset: 0x0000A6A0
		public async Task<RemoteQueue> GetNextQueueAsync()
		{
			ExTraceGlobals.RemoteConduitTracer.TraceDebug((long)this.GetHashCode(), "RemoteMultiQueue.GetNextQueueAsync: wait for ReadyQueueSemaphore");
			await this.ReadyQueueSemaphore.WaitAsync();
			ExTraceGlobals.RemoteConduitTracer.TraceDebug((long)this.GetHashCode(), "RemoteMultiQueue.GetNextQueueAsync: ReadyQueueSemaphore acquired");
			RemoteQueue result;
			lock (this.ReadyQueues)
			{
				RemoteQueue remoteQueue = this.ReadyQueues.Dequeue();
				this.ActiveQueues.Add(remoteQueue.GroupingKey, remoteQueue);
				result = remoteQueue;
			}
			return result;
		}

		// Token: 0x06000225 RID: 549 RVA: 0x0000C4E8 File Offset: 0x0000A6E8
		internal void BatchSendSucceeded(RemoteQueue remoteQueue)
		{
			remoteQueue.DiscardSentBatch();
			this.ActiveQueues.Remove(remoteQueue.GroupingKey);
			if (remoteQueue.IsEmpty)
			{
				remoteQueue.QueueState = QueueState.Inactive;
				this.InactiveQueues.Add(remoteQueue.GroupingKey, remoteQueue);
				return;
			}
			remoteQueue.QueueState = QueueState.Ready;
			this.ReadyQueues.Enqueue(remoteQueue);
			ExTraceGlobals.RemoteConduitTracer.TraceDebug((long)this.GetHashCode(), "RemoteMultiQueue.BatchSendSucceeded: about to release ReadyQueueSemaphore");
			this.ReadyQueueSemaphore.Release();
		}

		// Token: 0x06000226 RID: 550 RVA: 0x0000C564 File Offset: 0x0000A764
		internal void BatchSendFailed(RemoteQueue remoteQueue)
		{
			this.ActiveQueues.Remove(remoteQueue.GroupingKey);
			ExTraceGlobals.RemoteConduitTracer.TraceDebug<int>((long)this.GetHashCode(), "RemoteMultiQueue.BatchSendFailed: NumAttempted was {0}", remoteQueue.NumAttempts);
			if (remoteQueue.IsAllowedToRetry())
			{
				remoteQueue.PrepareForBackoff();
				remoteQueue.QueueState = QueueState.Retry;
				this.RetryQueues.Enqueue(remoteQueue);
				return;
			}
			this.AddDroppedNotificationForEachConsumer(remoteQueue);
		}

		// Token: 0x06000227 RID: 551 RVA: 0x0000C5C8 File Offset: 0x0000A7C8
		internal void StartTimers()
		{
			this.RetryTimer.Enabled = true;
			this.InactiveTimer.Enabled = true;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x0000C5E2 File Offset: 0x0000A7E2
		internal void StopTimers()
		{
			this.RetryTimer.Enabled = false;
			this.InactiveTimer.Enabled = false;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x0000C5FC File Offset: 0x0000A7FC
		protected override void InternalDispose(bool isDisposing)
		{
			if (isDisposing)
			{
				this.ReadyQueueSemaphore.Dispose();
				this.AllQueues = null;
				this.ActiveQueues = null;
				this.InactiveQueues = null;
				this.ReadyQueues = null;
				this.RetryQueues = null;
			}
		}

		// Token: 0x0600022A RID: 554 RVA: 0x0000C62F File Offset: 0x0000A82F
		protected override DisposeTracker InternalGetDisposeTracker()
		{
			return DisposeTracker.Get<RemoteMultiQueue>(this);
		}

		// Token: 0x0600022B RID: 555 RVA: 0x0000C637 File Offset: 0x0000A837
		private void AddDroppedNotificationForEachConsumer(RemoteQueue remoteQueue)
		{
			remoteQueue.Flush();
			this.ReadyQueues.Enqueue(remoteQueue);
			ExTraceGlobals.RemoteConduitTracer.TraceDebug((long)this.GetHashCode(), "RemoteMultiQueue.AddDroppedNotificationForEachConsumer: about to release ReadyQueueSemaphore");
			this.ReadyQueueSemaphore.Release();
		}

		// Token: 0x0600022C RID: 556 RVA: 0x0000C670 File Offset: 0x0000A870
		private void ProcessRetryTimer(object sender, ElapsedEventArgs e)
		{
			lock (this.RetryQueues)
			{
				DateTime utcNow = DateTime.UtcNow;
				for (int i = 0; i < this.RetryQueues.Count(); i++)
				{
					RemoteQueue remoteQueue = this.RetryQueues.Peek();
					if (remoteQueue.TimeToRetry > utcNow)
					{
						break;
					}
					RemoteQueue remoteQueue2 = this.RetryQueues.Dequeue();
					remoteQueue2.QueueState = QueueState.Ready;
					this.ReadyQueues.Enqueue(remoteQueue2);
					ExTraceGlobals.RemoteConduitTracer.TraceDebug((long)this.GetHashCode(), "RemoteMultiQueue.ProcessRetryTimer: about to release ReadyQueueSemaphore");
					this.ReadyQueueSemaphore.Release();
				}
			}
		}

		// Token: 0x0600022D RID: 557 RVA: 0x0000C728 File Offset: 0x0000A928
		private void ProcessInactiveTimer(object sender, ElapsedEventArgs e)
		{
			ExTraceGlobals.RemoteConduitTracer.TraceDebug<int>((long)this.GetHashCode(), "RemoteMultiQueue.ProcessInactiveTimer: {0} inactive remote queue(s)", this.InactiveQueues.Count);
		}

		// Token: 0x040000EA RID: 234
		private static RemoteMultiQueue singleton = new RemoteMultiQueue();
	}
}
