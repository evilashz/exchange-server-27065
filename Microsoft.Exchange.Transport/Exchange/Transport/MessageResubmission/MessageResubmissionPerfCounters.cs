using System;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Data.Directory.Management;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Threading;

namespace Microsoft.Exchange.Transport.MessageResubmission
{
	// Token: 0x02000138 RID: 312
	internal class MessageResubmissionPerfCounters : IMessageResubmissionPerfCounters
	{
		// Token: 0x06000DD5 RID: 3541 RVA: 0x00032E80 File Offset: 0x00031080
		public MessageResubmissionPerfCounters()
		{
			this.refreshSlidingCountersTimer = new GuardedTimer(delegate(object state)
			{
				this.RefreshSlidingCounters();
			}, null, TimeSpan.FromMinutes(1.0), TimeSpan.FromMinutes(1.0));
		}

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x06000DD6 RID: 3542 RVA: 0x00032ECE File Offset: 0x000310CE
		// (set) Token: 0x06000DD7 RID: 3543 RVA: 0x00032ED5 File Offset: 0x000310D5
		public static IMessageResubmissionPerfCounters Instance
		{
			get
			{
				return MessageResubmissionPerfCounters.defaultCounters;
			}
			set
			{
				MessageResubmissionPerfCounters.defaultCounters = value;
			}
		}

		// Token: 0x06000DD8 RID: 3544 RVA: 0x00032EE0 File Offset: 0x000310E0
		public void ResetCounters()
		{
			MessageResubmissionPerfCounters.perfCounters.SafetyNetResubmissionCount.RawValue = 0L;
			MessageResubmissionPerfCounters.perfCounters.ShadowSafetyNetResubmissionCount.RawValue = 0L;
			MessageResubmissionPerfCounters.perfCounters.ResubmitLatencyAverageTime.RawValue = 0L;
			MessageResubmissionPerfCounters.perfCounters.ResubmitLatencyAverageTimeBase.RawValue = 0L;
			MessageResubmissionPerfCounters.perfCounters.ResubmitRequestCount.RawValue = 0L;
			MessageResubmissionPerfCounters.perfCounters.RecentResubmitRequestCount.RawValue = 0L;
			MessageResubmissionPerfCounters.perfCounters.RecentShadowResubmitRequestCount.RawValue = 0L;
		}

		// Token: 0x06000DD9 RID: 3545 RVA: 0x00032F64 File Offset: 0x00031164
		public void UpdateResubmissionCount(int count, bool isShadowResubmit)
		{
			if (MessageResubmissionPerfCounters.perfCounters != null)
			{
				if (isShadowResubmit)
				{
					MessageResubmissionPerfCounters.perfCounters.ShadowSafetyNetResubmissionCount.IncrementBy((long)count);
					return;
				}
				MessageResubmissionPerfCounters.perfCounters.SafetyNetResubmissionCount.IncrementBy((long)count);
			}
		}

		// Token: 0x06000DDA RID: 3546 RVA: 0x00032F95 File Offset: 0x00031195
		public ITimerCounter ResubmitMessagesLatencyCounter()
		{
			return new AverageTimeCounter(MessageResubmissionPerfCounters.perfCounters.ResubmitLatencyAverageTime, MessageResubmissionPerfCounters.perfCounters.ResubmitLatencyAverageTimeBase);
		}

		// Token: 0x06000DDB RID: 3547 RVA: 0x00032FB0 File Offset: 0x000311B0
		public void UpdateResubmitRequestCount(ResubmitRequestState state, int changeAmount)
		{
			MessageResubmissionPerfCounters.resubmitRequestCountCounters.Get(state.ToString()).IncrementBy((long)changeAmount);
			MessageResubmissionPerfCounters.perfCounters.ResubmitRequestCount.IncrementBy((long)changeAmount);
		}

		// Token: 0x06000DDC RID: 3548 RVA: 0x00032FE1 File Offset: 0x000311E1
		public void ChangeResubmitRequestState(ResubmitRequestState oldState, ResubmitRequestState newState)
		{
			MessageResubmissionPerfCounters.resubmitRequestCountCounters.Get(oldState.ToString()).IncrementBy(-1L);
			MessageResubmissionPerfCounters.resubmitRequestCountCounters.Get(newState.ToString()).IncrementBy(1L);
		}

		// Token: 0x06000DDD RID: 3549 RVA: 0x0003301D File Offset: 0x0003121D
		public void IncrementRecentRequestCount(bool isShadowResubmit)
		{
			if (!isShadowResubmit)
			{
				MessageResubmissionPerfCounters.perfCounters.RecentResubmitRequestCount.RawValue = MessageResubmissionPerfCounters.recentResubmitRequestCounter.AddValue(1L);
				return;
			}
			MessageResubmissionPerfCounters.perfCounters.RecentShadowResubmitRequestCount.RawValue = MessageResubmissionPerfCounters.recentShadowResubmitRequestCounter.AddValue(1L);
		}

		// Token: 0x06000DDE RID: 3550 RVA: 0x0003305C File Offset: 0x0003125C
		public void RecordResubmitRequestTimeSpan(TimeSpan timeSpan)
		{
			MessageResubmissionPerfCounters.averageResubmitRequestTimeSpanCounter.AddValue((long)timeSpan.TotalSeconds);
			long num;
			MessageResubmissionPerfCounters.perfCounters.AverageResubmitRequestTimeSpan.RawValue = MessageResubmissionPerfCounters.averageResubmitRequestTimeSpanCounter.CalculateAverageAcrossAllSamples(out num);
		}

		// Token: 0x06000DDF RID: 3551 RVA: 0x00033098 File Offset: 0x00031298
		private void RefreshSlidingCounters()
		{
			MessageResubmissionPerfCounters.perfCounters.RecentResubmitRequestCount.RawValue = MessageResubmissionPerfCounters.recentResubmitRequestCounter.AddValue(0L);
			MessageResubmissionPerfCounters.perfCounters.RecentShadowResubmitRequestCount.RawValue = MessageResubmissionPerfCounters.recentShadowResubmitRequestCounter.AddValue(0L);
			long num;
			MessageResubmissionPerfCounters.perfCounters.AverageResubmitRequestTimeSpan.RawValue = MessageResubmissionPerfCounters.averageResubmitRequestTimeSpanCounter.CalculateAverageAcrossAllSamples(out num);
		}

		// Token: 0x040005EC RID: 1516
		private const string PerfCounterInstanceName = "_total";

		// Token: 0x040005ED RID: 1517
		private static readonly TimeSpan slidingWindowLength = TimeSpan.FromMinutes(15.0);

		// Token: 0x040005EE RID: 1518
		private static readonly TimeSpan bucketLength = TimeSpan.FromSeconds(10.0);

		// Token: 0x040005EF RID: 1519
		private static readonly AutoReadThroughCache<string, ExPerformanceCounter> resubmitRequestCountCounters = new AutoReadThroughCache<string, ExPerformanceCounter>((string name) => MessageResubmissionPerformanceCounters.GetInstance(name).ResubmitRequestCount);

		// Token: 0x040005F0 RID: 1520
		private static readonly SlidingTotalCounter recentResubmitRequestCounter = new SlidingTotalCounter(MessageResubmissionPerfCounters.slidingWindowLength, MessageResubmissionPerfCounters.bucketLength);

		// Token: 0x040005F1 RID: 1521
		private static readonly SlidingTotalCounter recentShadowResubmitRequestCounter = new SlidingTotalCounter(MessageResubmissionPerfCounters.slidingWindowLength, MessageResubmissionPerfCounters.bucketLength);

		// Token: 0x040005F2 RID: 1522
		private static readonly SlidingAverageCounter averageResubmitRequestTimeSpanCounter = new SlidingAverageCounter(MessageResubmissionPerfCounters.slidingWindowLength, MessageResubmissionPerfCounters.bucketLength);

		// Token: 0x040005F3 RID: 1523
		private static MessageResubmissionPerformanceCountersInstance perfCounters = MessageResubmissionPerformanceCounters.GetInstance("_total");

		// Token: 0x040005F4 RID: 1524
		private static IMessageResubmissionPerfCounters defaultCounters = new MessageResubmissionPerfCounters();

		// Token: 0x040005F5 RID: 1525
		private GuardedTimer refreshSlidingCountersTimer;
	}
}
