using System;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Scheduler.Contracts;

namespace Microsoft.Exchange.Transport.Scheduler.Processing
{
	// Token: 0x0200002A RID: 42
	internal class SchedulerDiagnostics : RefreshableComponent, ISchedulerDiagnostics
	{
		// Token: 0x060000DC RID: 220 RVA: 0x00004714 File Offset: 0x00002914
		public SchedulerDiagnostics(TimeSpan queueLogsRefreshTimeSpan, ISchedulerMetering metering, IQueueLogWriter queueLogWriter, Func<DateTime> timeProvider) : base(queueLogsRefreshTimeSpan, timeProvider)
		{
			ArgumentValidator.ThrowIfNull("timeProvider", timeProvider);
			ArgumentValidator.ThrowIfNull("queueLogWriter", queueLogWriter);
			ArgumentValidator.ThrowIfNull("metering", metering);
			TimeSpan slidingWindowLength = TimeSpan.FromSeconds(60.0);
			TimeSpan bucketLength = TimeSpan.FromSeconds(5.0);
			this.queueLogWriter = queueLogWriter;
			this.metering = metering;
			this.receivedCounter = new SlidingTotalCounter(slidingWindowLength, bucketLength, timeProvider);
			this.dispatchedCounter = new SlidingTotalCounter(slidingWindowLength, bucketLength, timeProvider);
			this.throttledCounter = new SlidingTotalCounter(slidingWindowLength, bucketLength, timeProvider);
			this.processedCounter = new SlidingTotalCounter(slidingWindowLength, bucketLength, timeProvider);
			this.createdQueueCounter = new SlidingTotalCounter(slidingWindowLength, bucketLength, timeProvider);
			this.deletedQueueCounter = new SlidingTotalCounter(slidingWindowLength, bucketLength, timeProvider);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00004810 File Offset: 0x00002A10
		public SchedulerDiagnostics(TimeSpan queueLogsRefreshTimeSpan, ISchedulerMetering metering, IQueueLogWriter queueLogWriter) : this(queueLogsRefreshTimeSpan, metering, queueLogWriter, () => DateTime.UtcNow)
		{
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004838 File Offset: 0x00002A38
		public SchedulerDiagnostics(Func<DateTime> timeProvider) : this(SchedulerDiagnostics.DefaultQueueLogRefreshTimeSpan, SchedulerDiagnostics.DefaultNoOpMetering, SchedulerDiagnostics.DefaultNoOpQueueLogWriter, timeProvider)
		{
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00004857 File Offset: 0x00002A57
		public SchedulerDiagnostics() : this(() => DateTime.UtcNow)
		{
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x0000487C File Offset: 0x00002A7C
		public void RegisterQueueLogging(IQueueLogProvider logProvider)
		{
			ArgumentValidator.ThrowIfNull("logProvider", logProvider);
			lock (this.syncRoot)
			{
				this.queueLogProviders.Add(logProvider);
			}
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000048D0 File Offset: 0x00002AD0
		public void Received()
		{
			Interlocked.Increment(ref this.receivedTotal);
			this.receivedCounter.AddValue(1L);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x000048EC File Offset: 0x00002AEC
		public void Dispatched()
		{
			this.dispatchedTotal += 1L;
			this.dispatchedCounter.AddValue(1L);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000490B File Offset: 0x00002B0B
		public void Throttled()
		{
			this.throttledTotal += 1L;
			this.throttledCounter.AddValue(1L);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x0000492A File Offset: 0x00002B2A
		public void Processed()
		{
			this.processedTotal += 1L;
			this.processedCounter.AddValue(1L);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004949 File Offset: 0x00002B49
		public void ScopedQueueCreated(int count)
		{
			this.createdQueueCounter.AddValue((long)count);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00004959 File Offset: 0x00002B59
		public void ScopedQueueDestroyed(int count)
		{
			this.deletedQueueCounter.AddValue((long)count);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000496C File Offset: 0x00002B6C
		public void VisitCurrentScopedQueues(IDictionary<IMessageScope, ScopedQueue> currentQueues)
		{
			DateTime t = DateTime.MaxValue;
			DateTime t2 = DateTime.MaxValue;
			long num = 0L;
			if (currentQueues != null)
			{
				foreach (KeyValuePair<IMessageScope, ScopedQueue> keyValuePair in currentQueues)
				{
					ScopedQueue value = keyValuePair.Value;
					if (value.CreateDateTime < t)
					{
						t = value.CreateDateTime;
					}
					if (!value.IsEmpty && value.Locked && value.LockDateTime < t2)
					{
						t2 = value.LockDateTime;
					}
				}
				num = (long)currentQueues.Count;
			}
			this.oldestScopedQueueCreateTime = t;
			this.oldestScopedLockTimeStamp = t2;
			this.totalScopedQueues = num;
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x00004A2C File Offset: 0x00002C2C
		public SchedulerDiagnosticsInfo GetDiagnosticsInfo()
		{
			return new SchedulerDiagnosticsInfo
			{
				Dispatched = this.dispatchedTotal,
				DispatchRate = this.dispatchedCounter.Sum,
				Received = this.receivedTotal,
				ReceiveRate = this.receivedCounter.Sum,
				Throttled = this.throttledTotal,
				ThrottleRate = this.throttledCounter.Sum,
				Processed = this.processedTotal,
				ProcessRate = this.processedCounter.Sum,
				TotalScopedQueues = this.totalScopedQueues,
				ScopedQueuesCreatedRate = this.createdQueueCounter.Sum,
				ScopedQueuesDestroyedRate = this.deletedQueueCounter.Sum,
				OldestLockTimeStamp = this.oldestScopedLockTimeStamp,
				OldestScopedQueueCreateTime = this.oldestScopedQueueCreateTime
			};
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00004AFC File Offset: 0x00002CFC
		protected override void Refresh(DateTime timestamp)
		{
			List<QueueLogInfo> list = new List<QueueLogInfo>();
			int count = this.queueLogProviders.Count;
			for (int i = 0; i < count; i++)
			{
				list.AddRange(this.queueLogProviders[i].FlushLogs(timestamp, this.metering));
			}
			this.lastQueueLogEntries = list;
			this.WriteCurrentLogEntries();
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00004B54 File Offset: 0x00002D54
		private void WriteCurrentLogEntries()
		{
			List<QueueLogInfo> list = this.lastQueueLogEntries;
			foreach (QueueLogInfo logInfo in list)
			{
				this.queueLogWriter.Write(logInfo);
			}
		}

		// Token: 0x04000078 RID: 120
		public static readonly TimeSpan DefaultQueueLogRefreshTimeSpan = TimeSpan.FromMinutes(15.0);

		// Token: 0x04000079 RID: 121
		public static readonly IQueueLogWriter DefaultNoOpQueueLogWriter = new NoOpQueueLogWriter();

		// Token: 0x0400007A RID: 122
		public static readonly ISchedulerMetering DefaultNoOpMetering = new NoOpMetering();

		// Token: 0x0400007B RID: 123
		private readonly SlidingTotalCounter receivedCounter;

		// Token: 0x0400007C RID: 124
		private readonly SlidingTotalCounter dispatchedCounter;

		// Token: 0x0400007D RID: 125
		private readonly SlidingTotalCounter throttledCounter;

		// Token: 0x0400007E RID: 126
		private readonly SlidingTotalCounter processedCounter;

		// Token: 0x0400007F RID: 127
		private readonly SlidingTotalCounter createdQueueCounter;

		// Token: 0x04000080 RID: 128
		private readonly SlidingTotalCounter deletedQueueCounter;

		// Token: 0x04000081 RID: 129
		private readonly List<IQueueLogProvider> queueLogProviders = new List<IQueueLogProvider>();

		// Token: 0x04000082 RID: 130
		private readonly object syncRoot = new object();

		// Token: 0x04000083 RID: 131
		private readonly IQueueLogWriter queueLogWriter;

		// Token: 0x04000084 RID: 132
		private readonly ISchedulerMetering metering;

		// Token: 0x04000085 RID: 133
		private List<QueueLogInfo> lastQueueLogEntries = new List<QueueLogInfo>();

		// Token: 0x04000086 RID: 134
		private long receivedTotal;

		// Token: 0x04000087 RID: 135
		private long dispatchedTotal;

		// Token: 0x04000088 RID: 136
		private long throttledTotal;

		// Token: 0x04000089 RID: 137
		private long processedTotal;

		// Token: 0x0400008A RID: 138
		private long totalScopedQueues;

		// Token: 0x0400008B RID: 139
		private DateTime oldestScopedQueueCreateTime = DateTime.MaxValue;

		// Token: 0x0400008C RID: 140
		private DateTime oldestScopedLockTimeStamp = DateTime.MaxValue;
	}
}
