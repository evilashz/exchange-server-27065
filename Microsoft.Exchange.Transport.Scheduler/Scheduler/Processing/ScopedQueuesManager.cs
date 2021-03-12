using System;
using System.Collections.Generic;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Scheduler.Contracts;

namespace Microsoft.Exchange.Transport.Scheduler.Processing
{
	// Token: 0x0200002E RID: 46
	internal sealed class ScopedQueuesManager : IScopedQueuesManager, IQueueLogProvider
	{
		// Token: 0x06000108 RID: 264 RVA: 0x00004FB0 File Offset: 0x000031B0
		public ScopedQueuesManager(TimeSpan scopedQueueTtl, TimeSpan updateInterval, IQueueFactory queueFactory, ISchedulerThrottler throttler, ISchedulerDiagnostics schedulerDiagnostics, Func<DateTime> timeProvider = null)
		{
			ArgumentValidator.ThrowIfOutOfRange<TimeSpan>("scopedQueueTtl", scopedQueueTtl, TimeSpan.Zero.Add(TimeSpan.FromTicks(1L)), TimeSpan.MaxValue);
			ArgumentValidator.ThrowIfOutOfRange<TimeSpan>("updateInterval", updateInterval, TimeSpan.Zero.Add(TimeSpan.FromTicks(1L)), TimeSpan.MaxValue);
			ArgumentValidator.ThrowIfNull("queueFactory", queueFactory);
			ArgumentValidator.ThrowIfNull("throttler", throttler);
			ArgumentValidator.ThrowIfNull("schedulerDiagnostics", schedulerDiagnostics);
			this.ScopedQueueTtl = scopedQueueTtl;
			this.UpdateInterval = updateInterval;
			this.queueFactory = queueFactory;
			this.throttler = throttler;
			this.schedulerDiagnostics = schedulerDiagnostics;
			this.timeProvider = timeProvider;
			this.LastUpdated = this.GetCurrentTime();
			this.schedulerDiagnostics.RegisterQueueLogging(this);
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000109 RID: 265 RVA: 0x00005089 File Offset: 0x00003289
		public IDictionary<IMessageScope, ScopedQueue> ScopedQueue
		{
			get
			{
				return this.scopedQueues;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600010A RID: 266 RVA: 0x00005091 File Offset: 0x00003291
		// (set) Token: 0x0600010B RID: 267 RVA: 0x00005099 File Offset: 0x00003299
		public DateTime LastUpdated { get; private set; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600010C RID: 268 RVA: 0x000050A2 File Offset: 0x000032A2
		// (set) Token: 0x0600010D RID: 269 RVA: 0x000050AA File Offset: 0x000032AA
		private TimeSpan UpdateInterval { get; set; }

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600010E RID: 270 RVA: 0x000050B3 File Offset: 0x000032B3
		// (set) Token: 0x0600010F RID: 271 RVA: 0x000050BB File Offset: 0x000032BB
		private TimeSpan ScopedQueueTtl { get; set; }

		// Token: 0x06000110 RID: 272 RVA: 0x000050C4 File Offset: 0x000032C4
		public void Add(ISchedulableMessage message, IMessageScope throttledScope)
		{
			if (!this.scopedQueues.ContainsKey(throttledScope))
			{
				this.scopedQueues.Add(throttledScope, new ScopedQueue(throttledScope, this.queueFactory.CreateNewQueueInstance(), this.timeProvider));
				this.schedulerDiagnostics.ScopedQueueCreated(1);
			}
			this.scopedQueues[throttledScope].Enqueue(message);
		}

		// Token: 0x06000111 RID: 273 RVA: 0x00005120 File Offset: 0x00003320
		public bool IsAlreadyScoped(IEnumerable<IMessageScope> scopes, out IMessageScope throttledScope)
		{
			foreach (IMessageScope messageScope in scopes)
			{
				if (this.scopedQueues.ContainsKey(messageScope) && !this.scopedQueues[messageScope].IsEmpty)
				{
					throttledScope = messageScope;
					return true;
				}
			}
			throttledScope = null;
			return false;
		}

		// Token: 0x06000112 RID: 274 RVA: 0x00005190 File Offset: 0x00003390
		public bool TryGetNext(out ISchedulableMessage message)
		{
			while (this.nextQueueToSelect != null)
			{
				ScopedQueue value = this.nextQueueToSelect.Value;
				if (!value.Locked && !value.IsEmpty)
				{
					if (!this.ShouldBeLocked(value))
					{
						this.AdvanceUnlockedQueuesIterator();
						return value.TryDequeue(out message);
					}
					value.Lock();
				}
				LinkedListNode<ScopedQueue> node = this.nextQueueToSelect;
				this.AdvanceUnlockedQueuesIterator();
				this.unlockedQueues.Remove(node);
				if (this.unlockedQueues.Count == 0)
				{
					this.nextQueueToSelect = null;
				}
			}
			message = null;
			return false;
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00005214 File Offset: 0x00003414
		public void TimedUpdate()
		{
			DateTime currentTime = this.GetCurrentTime();
			if (this.LastUpdated.Add(this.UpdateInterval) < currentTime)
			{
				this.EvictUnusedQueues(currentTime);
				this.UpdateUnlockedQueuesIterator(currentTime);
				this.schedulerDiagnostics.VisitCurrentScopedQueues(this.scopedQueues);
				this.LastUpdated = this.GetCurrentTime();
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00005270 File Offset: 0x00003470
		public IEnumerable<QueueLogInfo> FlushLogs(DateTime checkpoint, ISchedulerMetering metering)
		{
			List<QueueLogInfo> list = new List<QueueLogInfo>();
			foreach (KeyValuePair<IMessageScope, ScopedQueue> keyValuePair in this.scopedQueues)
			{
				IMessageScope key = keyValuePair.Key;
				ScopedQueue value = keyValuePair.Value;
				QueueLogInfo queueLogInfo = new QueueLogInfo(key.Display, checkpoint);
				value.Flush(checkpoint, queueLogInfo);
				UsageData usageData;
				if (metering.TryGetUsage(key, out usageData))
				{
					queueLogInfo.UsageData = usageData;
				}
				list.Add(queueLogInfo);
			}
			return list;
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00005308 File Offset: 0x00003508
		private void AdvanceUnlockedQueuesIterator()
		{
			if (this.nextQueueToSelect.Next != null)
			{
				this.nextQueueToSelect = this.nextQueueToSelect.Next;
				return;
			}
			this.nextQueueToSelect = this.nextQueueToSelect.List.First;
		}

		// Token: 0x06000116 RID: 278 RVA: 0x0000533F File Offset: 0x0000353F
		private bool ShouldBeLocked(ScopedQueue queue)
		{
			return this.throttler.ShouldThrottle(queue.Scope);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x00005354 File Offset: 0x00003554
		private void EvictUnusedQueues(DateTime nowTimestamp)
		{
			List<IMessageScope> list = new List<IMessageScope>();
			DateTime t = nowTimestamp - this.ScopedQueueTtl;
			foreach (KeyValuePair<IMessageScope, ScopedQueue> keyValuePair in this.scopedQueues)
			{
				ScopedQueue value = keyValuePair.Value;
				if (value.IsEmpty && value.LastActivity < t)
				{
					list.Add(keyValuePair.Key);
				}
			}
			foreach (IMessageScope key in list)
			{
				this.scopedQueues.Remove(key);
			}
			this.schedulerDiagnostics.ScopedQueueDestroyed(list.Count);
		}

		// Token: 0x06000118 RID: 280 RVA: 0x00005434 File Offset: 0x00003634
		private void UpdateUnlockedQueuesIterator(DateTime nowTimeStamp)
		{
			List<ScopedQueue> list = new List<ScopedQueue>(this.scopedQueues.Count);
			foreach (KeyValuePair<IMessageScope, ScopedQueue> keyValuePair in this.scopedQueues)
			{
				ScopedQueue value = keyValuePair.Value;
				if (this.ShouldBeLocked(value))
				{
					value.Lock();
				}
				else
				{
					value.Unlock();
				}
				if (!value.IsEmpty && !value.Locked)
				{
					list.Add(value);
				}
			}
			list.Sort(new Comparison<ScopedQueue>(this.CompareScopedQueue));
			this.unlockedQueues = new LinkedList<ScopedQueue>(list);
			this.nextQueueToSelect = this.unlockedQueues.First;
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000054F0 File Offset: 0x000036F0
		private int CompareScopedQueue(ScopedQueue queue1, ScopedQueue queue2)
		{
			ISchedulableMessage schedulableMessage;
			queue1.TryPeek(out schedulableMessage);
			ISchedulableMessage schedulableMessage2;
			queue2.TryPeek(out schedulableMessage2);
			return schedulableMessage.SubmitTime.CompareTo(schedulableMessage2.SubmitTime);
		}

		// Token: 0x0600011A RID: 282 RVA: 0x00005523 File Offset: 0x00003723
		private DateTime GetCurrentTime()
		{
			if (this.timeProvider == null)
			{
				return DateTime.UtcNow;
			}
			return this.timeProvider();
		}

		// Token: 0x0400009C RID: 156
		private readonly IQueueFactory queueFactory;

		// Token: 0x0400009D RID: 157
		private readonly Func<DateTime> timeProvider;

		// Token: 0x0400009E RID: 158
		private readonly ISchedulerThrottler throttler;

		// Token: 0x0400009F RID: 159
		private readonly ISchedulerDiagnostics schedulerDiagnostics;

		// Token: 0x040000A0 RID: 160
		private IDictionary<IMessageScope, ScopedQueue> scopedQueues = new Dictionary<IMessageScope, ScopedQueue>();

		// Token: 0x040000A1 RID: 161
		private LinkedList<ScopedQueue> unlockedQueues = new LinkedList<ScopedQueue>();

		// Token: 0x040000A2 RID: 162
		private LinkedListNode<ScopedQueue> nextQueueToSelect;
	}
}
