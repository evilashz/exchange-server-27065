using System;
using System.Threading;
using Microsoft.Exchange.Compliance.TaskDistributionCommon.Instrumentation;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.Compliance.TaskDistributionFabric.Optics
{
	// Token: 0x02000025 RID: 37
	internal class PerformanceCounterAccessor : IPerformanceCounterAccessor
	{
		// Token: 0x060000C5 RID: 197 RVA: 0x00005640 File Offset: 0x00003840
		public PerformanceCounterAccessor(string instanceName)
		{
			this.counters = TaskDistributionFabricPerfCounters.GetInstance(instanceName);
			SlidingTotalCounter counter = new SlidingTotalCounter(PerformanceCounterAccessor.SlidingCounterInterval, PerformanceCounterAccessor.SlidingCounterPrecision);
			this.successfulRequests = new PerformanceCounterAccessor.PeriodicSlidingTotalCounter(PerformanceCounterAccessor.BatchDuration, counter);
			SlidingTotalCounter counter2 = new SlidingTotalCounter(PerformanceCounterAccessor.SlidingCounterInterval, PerformanceCounterAccessor.SlidingCounterPrecision);
			this.failedRequests = new PerformanceCounterAccessor.PeriodicSlidingTotalCounter(PerformanceCounterAccessor.BatchDuration, counter2);
			PercentileCounter counter3 = new PercentileCounter(PerformanceCounterAccessor.PercentileCounterInterval, PerformanceCounterAccessor.PercentileCounterPrecision, 1L, 1000L);
			this.queueLength = new PerformanceCounterAccessor.PeriodicPercentileCounter(PerformanceCounterAccessor.BatchDuration, counter3);
			PercentileCounter counter4 = new PercentileCounter(PerformanceCounterAccessor.PercentileCounterInterval, PerformanceCounterAccessor.PercentileCounterPrecision, 10L, 10000L);
			this.queueLatency = new PerformanceCounterAccessor.PeriodicPercentileCounter(PerformanceCounterAccessor.BatchDuration, counter4);
			PercentileCounter counter5 = new PercentileCounter(PerformanceCounterAccessor.PercentileCounterInterval, PerformanceCounterAccessor.PercentileCounterPrecision, 5L, 5000L);
			this.processorLatency = new PerformanceCounterAccessor.PeriodicPercentileCounter(PerformanceCounterAccessor.BatchDuration, counter5);
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00005720 File Offset: 0x00003920
		public void AddQueueEvent(QueueEvent qEvent)
		{
			switch (qEvent)
			{
			case QueueEvent.Enqueue:
				this.counters.CurrentQueueLength.Increment();
				break;
			case QueueEvent.Dequeue:
				this.counters.CurrentQueueLength.Decrement();
				break;
			}
			if (this.queueLength.AddValue(this.counters.CurrentQueueLength.RawValue))
			{
				this.UpdateQueueSizePercentileCounters();
			}
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00005786 File Offset: 0x00003986
		public void AddDequeueLatency(long latency)
		{
			if (this.queueLatency.AddValue(latency))
			{
				this.UpdateQueueLatencyPercentileCounters();
			}
		}

		// Token: 0x060000C8 RID: 200 RVA: 0x0000579C File Offset: 0x0000399C
		public void AddProcessingCompletionEvent(ProcessingCompletionEvent pEvent, long latency)
		{
			switch (pEvent)
			{
			case ProcessingCompletionEvent.Success:
				if (this.successfulRequests.AddValue(1L))
				{
					this.counters.RecentSuccessfulRequestsCount.RawValue = this.successfulRequests.Sum;
				}
				break;
			case ProcessingCompletionEvent.Failure:
				if (this.failedRequests.AddValue(1L))
				{
					this.counters.RecentFailedRequestsCount.RawValue = this.failedRequests.Sum;
				}
				break;
			}
			if (this.processorLatency.AddValue(latency))
			{
				this.UpdateProcessorLatencyPercentileCounters();
			}
		}

		// Token: 0x060000C9 RID: 201 RVA: 0x00005828 File Offset: 0x00003A28
		public void AddProcessorEvent(ProcessorEvent pEvent)
		{
			switch (pEvent)
			{
			case ProcessorEvent.StartProcessing:
				this.counters.CurrentRequests.Increment();
				return;
			case ProcessorEvent.EndProcessing:
				this.counters.CurrentRequests.Decrement();
				return;
			default:
				return;
			}
		}

		// Token: 0x060000CA RID: 202 RVA: 0x0000586C File Offset: 0x00003A6C
		public void UpdateCounters()
		{
			this.queueLength.SubmitBatch();
			this.queueLatency.SubmitBatch();
			this.UpdateQueueSizePercentileCounters();
			this.UpdateQueueLatencyPercentileCounters();
			this.successfulRequests.SubmitBatch();
			this.failedRequests.SubmitBatch();
			this.processorLatency.SubmitBatch();
			this.counters.RecentSuccessfulRequestsCount.RawValue = this.successfulRequests.Sum;
			this.counters.RecentFailedRequestsCount.RawValue = this.failedRequests.Sum;
			this.UpdateProcessorLatencyPercentileCounters();
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000058F8 File Offset: 0x00003AF8
		private void UpdateQueueSizePercentileCounters()
		{
			this.counters.QueueSize75Percentile.RawValue = this.queueLength.PercentileQuery(75.0);
			this.counters.QueueSize90Percentile.RawValue = this.queueLength.PercentileQuery(90.0);
			this.counters.QueueSize99Percentile.RawValue = this.queueLength.PercentileQuery(99.0);
		}

		// Token: 0x060000CC RID: 204 RVA: 0x00005974 File Offset: 0x00003B74
		private void UpdateQueueLatencyPercentileCounters()
		{
			this.counters.QueueLatency75Percentile.RawValue = this.queueLatency.PercentileQuery(75.0);
			this.counters.QueueLatency90Percentile.RawValue = this.queueLatency.PercentileQuery(90.0);
			this.counters.QueueLatency99Percentile.RawValue = this.queueLatency.PercentileQuery(99.0);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000059F0 File Offset: 0x00003BF0
		private void UpdateProcessorLatencyPercentileCounters()
		{
			this.counters.ProcessorLatency75Percentile.RawValue = this.processorLatency.PercentileQuery(75.0);
			this.counters.ProcessorLatency90Percentile.RawValue = this.processorLatency.PercentileQuery(90.0);
			this.counters.ProcessorLatency99Percentile.RawValue = this.processorLatency.PercentileQuery(99.0);
		}

		// Token: 0x0400004A RID: 74
		private static readonly TimeSpan SlidingCounterInterval = TimeSpan.FromMinutes(15.0);

		// Token: 0x0400004B RID: 75
		private static readonly TimeSpan SlidingCounterPrecision = TimeSpan.FromSeconds(15.0);

		// Token: 0x0400004C RID: 76
		private static readonly TimeSpan PercentileCounterInterval = TimeSpan.FromMinutes(15.0);

		// Token: 0x0400004D RID: 77
		private static readonly TimeSpan PercentileCounterPrecision = TimeSpan.FromSeconds(15.0);

		// Token: 0x0400004E RID: 78
		private static readonly TimeSpan BatchDuration = TimeSpan.FromSeconds(30.0);

		// Token: 0x0400004F RID: 79
		private TaskDistributionFabricPerfCountersInstance counters;

		// Token: 0x04000050 RID: 80
		private PerformanceCounterAccessor.PeriodicSlidingTotalCounter successfulRequests;

		// Token: 0x04000051 RID: 81
		private PerformanceCounterAccessor.PeriodicSlidingTotalCounter failedRequests;

		// Token: 0x04000052 RID: 82
		private PerformanceCounterAccessor.PeriodicPercentileCounter queueLength;

		// Token: 0x04000053 RID: 83
		private PerformanceCounterAccessor.PeriodicPercentileCounter queueLatency;

		// Token: 0x04000054 RID: 84
		private PerformanceCounterAccessor.PeriodicPercentileCounter processorLatency;

		// Token: 0x02000026 RID: 38
		private class PeriodicPercentileCounter
		{
			// Token: 0x060000CF RID: 207 RVA: 0x00005AD8 File Offset: 0x00003CD8
			public PeriodicPercentileCounter(TimeSpan batchDuration, PercentileCounter counter)
			{
				this.batchDuration = batchDuration;
				this.counter = counter;
				this.syncObj = new object();
			}

			// Token: 0x060000D0 RID: 208 RVA: 0x00005AFC File Offset: 0x00003CFC
			public bool AddValue(long value)
			{
				if (value < 0L)
				{
					value = 0L;
				}
				Interlocked.Add(ref this.batchSum, value);
				Interlocked.Increment(ref this.batchCount);
				if (DateTime.UtcNow.Subtract(this.lastSubmitTime) >= this.batchDuration)
				{
					this.SubmitBatch();
					return true;
				}
				return false;
			}

			// Token: 0x060000D1 RID: 209 RVA: 0x00005B54 File Offset: 0x00003D54
			public void SubmitBatch()
			{
				lock (this.syncObj)
				{
					long num = Interlocked.Read(ref this.batchSum);
					long num2 = Interlocked.Read(ref this.batchCount);
					if (num2 != 0L)
					{
						long value = num / num2;
						this.counter.AddValue(value);
						this.batchCount = 0L;
						this.batchSum = 0L;
					}
					this.lastSubmitTime = DateTime.UtcNow;
				}
			}

			// Token: 0x060000D2 RID: 210 RVA: 0x00005BD8 File Offset: 0x00003DD8
			public long PercentileQuery(double percentage)
			{
				return this.counter.PercentileQuery(percentage);
			}

			// Token: 0x04000055 RID: 85
			private readonly TimeSpan batchDuration;

			// Token: 0x04000056 RID: 86
			private long batchSum;

			// Token: 0x04000057 RID: 87
			private long batchCount;

			// Token: 0x04000058 RID: 88
			private DateTime lastSubmitTime;

			// Token: 0x04000059 RID: 89
			private object syncObj;

			// Token: 0x0400005A RID: 90
			private PercentileCounter counter;
		}

		// Token: 0x02000027 RID: 39
		private class PeriodicSlidingTotalCounter
		{
			// Token: 0x060000D3 RID: 211 RVA: 0x00005BE6 File Offset: 0x00003DE6
			public PeriodicSlidingTotalCounter(TimeSpan batchDuration, SlidingTotalCounter counter)
			{
				this.batchDuration = batchDuration;
				this.counter = counter;
				this.syncObj = new object();
			}

			// Token: 0x17000028 RID: 40
			// (get) Token: 0x060000D4 RID: 212 RVA: 0x00005C07 File Offset: 0x00003E07
			public long Sum
			{
				get
				{
					return this.counter.Sum;
				}
			}

			// Token: 0x060000D5 RID: 213 RVA: 0x00005C14 File Offset: 0x00003E14
			public bool AddValue(long value)
			{
				Interlocked.Add(ref this.batchTotal, value);
				if (DateTime.UtcNow.Subtract(this.lastSubmitTime) >= this.batchDuration)
				{
					this.SubmitBatch();
					return true;
				}
				return false;
			}

			// Token: 0x060000D6 RID: 214 RVA: 0x00005C58 File Offset: 0x00003E58
			public void SubmitBatch()
			{
				lock (this.syncObj)
				{
					long value = Interlocked.Read(ref this.batchTotal);
					this.batchTotal = 0L;
					this.counter.AddValue(value);
					this.lastSubmitTime = DateTime.UtcNow;
				}
			}

			// Token: 0x0400005B RID: 91
			private readonly TimeSpan batchDuration;

			// Token: 0x0400005C RID: 92
			private long batchTotal;

			// Token: 0x0400005D RID: 93
			private DateTime lastSubmitTime;

			// Token: 0x0400005E RID: 94
			private object syncObj;

			// Token: 0x0400005F RID: 95
			private SlidingTotalCounter counter;
		}
	}
}
