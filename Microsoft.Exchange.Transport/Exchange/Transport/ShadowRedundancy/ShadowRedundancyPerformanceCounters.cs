using System;
using Microsoft.Exchange.Common;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Threading;
using Microsoft.Exchange.Transport.RemoteDelivery;

namespace Microsoft.Exchange.Transport.ShadowRedundancy
{
	// Token: 0x0200038C RID: 908
	internal sealed class ShadowRedundancyPerformanceCounters : IShadowRedundancyPerformanceCounters
	{
		// Token: 0x060027C5 RID: 10181 RVA: 0x0009C154 File Offset: 0x0009A354
		public ShadowRedundancyPerformanceCounters()
		{
			this.performanceCounters = new ExPerformanceCounter[]
			{
				ShadowRedundancyPerformanceCounters.perfCounters.RedundantMessageDiscardEvents,
				ShadowRedundancyPerformanceCounters.perfCounters.RedundantMessageDiscardEventsExpired,
				ShadowRedundancyPerformanceCounters.queuingPerfCounters.AggregateShadowQueueLength,
				ShadowRedundancyPerformanceCounters.queuingPerfCounters.ShadowQueueAutoDiscardsTotal
			};
			this.refreshSlidingCountersTimer = new GuardedTimer(delegate(object state)
			{
				this.RefreshSlidingCounters();
			}, null, TimeSpan.FromMinutes(1.0), TimeSpan.FromMinutes(1.0));
		}

		// Token: 0x17000BEF RID: 3055
		// (get) Token: 0x060027C6 RID: 10182 RVA: 0x0009C1E4 File Offset: 0x0009A3E4
		// (set) Token: 0x060027C7 RID: 10183 RVA: 0x0009C1F5 File Offset: 0x0009A3F5
		public long RedundantMessageDiscardEvents
		{
			get
			{
				return ShadowRedundancyPerformanceCounters.perfCounters.RedundantMessageDiscardEvents.RawValue;
			}
			set
			{
				ShadowRedundancyPerformanceCounters.perfCounters.RedundantMessageDiscardEvents.RawValue = value;
			}
		}

		// Token: 0x060027C8 RID: 10184 RVA: 0x0009C208 File Offset: 0x0009A408
		public bool IsValid(ShadowRedundancyCounterId shadowRedundancyCounterName)
		{
			if (shadowRedundancyCounterName.Equals(ShadowRedundancyCounterId.RedundantMessageDiscardEvents) || shadowRedundancyCounterName.Equals(ShadowRedundancyCounterId.RedundantMessageDiscardEventsExpired))
			{
				return ShadowRedundancyPerformanceCounters.perfCounters != null;
			}
			if (shadowRedundancyCounterName.Equals(ShadowRedundancyCounterId.AggregateShadowQueueLength) || shadowRedundancyCounterName.Equals(ShadowRedundancyCounterId.ShadowQueueAutoDiscardsTotal))
			{
				return ShadowRedundancyPerformanceCounters.queuingPerfCounters != null;
			}
			throw new ArgumentOutOfRangeException("shadowRedundancyCounterName");
		}

		// Token: 0x060027C9 RID: 10185 RVA: 0x0009C283 File Offset: 0x0009A483
		public void IncrementCounter(ShadowRedundancyCounterId shadowRedundancyCounterName)
		{
			if (this.IsValid(shadowRedundancyCounterName))
			{
				this.performanceCounters[(int)shadowRedundancyCounterName].Increment();
			}
		}

		// Token: 0x060027CA RID: 10186 RVA: 0x0009C29C File Offset: 0x0009A49C
		public void IncrementCounterBy(ShadowRedundancyCounterId shadowRedundancyCounterName, long value)
		{
			if (this.IsValid(shadowRedundancyCounterName))
			{
				this.performanceCounters[(int)shadowRedundancyCounterName].IncrementBy(value);
			}
		}

		// Token: 0x060027CB RID: 10187 RVA: 0x0009C2B6 File Offset: 0x0009A4B6
		public void DecrementCounter(ShadowRedundancyCounterId shadowRedundancyCounterName)
		{
			if (this.IsValid(shadowRedundancyCounterName))
			{
				this.performanceCounters[(int)shadowRedundancyCounterName].Decrement();
			}
		}

		// Token: 0x060027CC RID: 10188 RVA: 0x0009C2CF File Offset: 0x0009A4CF
		public void DecrementCounterBy(ShadowRedundancyCounterId shadowRedundancyCounterName, long value)
		{
			if (this.IsValid(shadowRedundancyCounterName))
			{
				this.performanceCounters[(int)shadowRedundancyCounterName].IncrementBy(-value);
			}
		}

		// Token: 0x060027CD RID: 10189 RVA: 0x0009C2EA File Offset: 0x0009A4EA
		public void DelayedAckExpired(long messageCount)
		{
			if (ShadowRedundancyPerformanceCounters.perfCounters != null)
			{
				ShadowRedundancyPerformanceCounters.perfCounters.CurrentMessagesAckBeforeRelayCompleted.IncrementBy(messageCount);
			}
		}

		// Token: 0x060027CE RID: 10190 RVA: 0x0009C304 File Offset: 0x0009A504
		public void DelayedAckDeliveredAfterExpiry(long messageCount)
		{
			if (ShadowRedundancyPerformanceCounters.perfCounters != null)
			{
				ShadowRedundancyPerformanceCounters.perfCounters.CurrentMessagesAckBeforeRelayCompleted.IncrementBy(-messageCount);
			}
		}

		// Token: 0x060027CF RID: 10191 RVA: 0x0009C31F File Offset: 0x0009A51F
		public void UpdateShadowQueueLength(string hostname, int changeAmount)
		{
			ShadowRedundancyPerformanceCounters.shadowQueueLengthCounters.Get(hostname).IncrementBy((long)changeAmount);
		}

		// Token: 0x060027D0 RID: 10192 RVA: 0x0009C334 File Offset: 0x0009A534
		public ITimerCounter ShadowSelectionLatencyCounter()
		{
			return new AverageTimeCounter(ShadowRedundancyPerformanceCounters.perfCounters.ShadowHostSelectionAverageTime, ShadowRedundancyPerformanceCounters.perfCounters.ShadowHostSelectionAverageTimeBase);
		}

		// Token: 0x060027D1 RID: 10193 RVA: 0x0009C34F File Offset: 0x0009A54F
		public ITimerCounter ShadowNegotiationLatencyCounter()
		{
			return new AverageTimeCounter(ShadowRedundancyPerformanceCounters.perfCounters.ShadowHostNegotiationAverageTime, ShadowRedundancyPerformanceCounters.perfCounters.ShadowHostNegotiationAverageTimeBase);
		}

		// Token: 0x060027D2 RID: 10194 RVA: 0x0009C36A File Offset: 0x0009A56A
		public IAverageCounter ShadowSuccessfulNegotiationLatencyCounter()
		{
			return new AverageCounter(ShadowRedundancyPerformanceCounters.perfCounters.ShadowHostSuccessfulNegotiationAverageTime, ShadowRedundancyPerformanceCounters.perfCounters.ShadowHostSuccessfulNegotiationAverageTimeBase);
		}

		// Token: 0x060027D3 RID: 10195 RVA: 0x0009C385 File Offset: 0x0009A585
		public ITimerCounter ShadowHeartbeatLatencyCounter(string hostname)
		{
			return new AverageTimeCounter(ShadowRedundancyPerformanceCounters.shadowHeartbeatLatencyCounters.Get(hostname), ShadowRedundancyPerformanceCounters.shadowHeartbeatLatencyBaseCounters.Get(hostname));
		}

		// Token: 0x060027D4 RID: 10196 RVA: 0x0009C3A2 File Offset: 0x0009A5A2
		public void ShadowFailure(string hostname)
		{
			ShadowRedundancyPerformanceCounters.shadowFailureCounters.Get(hostname).Increment();
		}

		// Token: 0x060027D5 RID: 10197 RVA: 0x0009C3B5 File Offset: 0x0009A5B5
		public void HeartbeatFailure(string hostname)
		{
			ShadowRedundancyPerformanceCounters.heartbeatFailureCounters.Get(hostname).Increment();
		}

		// Token: 0x060027D6 RID: 10198 RVA: 0x0009C3C8 File Offset: 0x0009A5C8
		public void TrackMessageMadeRedundant(bool success)
		{
			if (ShadowRedundancyPerformanceCounters.perfCounters != null)
			{
				if (!success)
				{
					ShadowRedundancyPerformanceCounters.perfCounters.MessagesFailedToBeMadeRedundant.RawValue = ShadowRedundancyPerformanceCounters.failureToMakeMessageRedundantCounter.AddValue(1L);
				}
				ShadowRedundancyPerformanceCounters.perfCounters.MessagesFailedToBeMadeRedundantPercentage.RawValue = (long)ShadowRedundancyPerformanceCounters.messagesFailedToBeMadeRedundantPercentageCounter.Add(success ? 0L : 1L, 1L);
			}
		}

		// Token: 0x060027D7 RID: 10199 RVA: 0x0009C41F File Offset: 0x0009A61F
		public void SubmitMessagesFromShadowQueue(string hostname, int count)
		{
			ShadowRedundancyPerformanceCounters.resubmittedMessageCounters.Get(hostname).IncrementBy((long)count);
		}

		// Token: 0x060027D8 RID: 10200 RVA: 0x0009C434 File Offset: 0x0009A634
		public void SmtpTimeout()
		{
			if (ShadowRedundancyPerformanceCounters.perfCounters != null)
			{
				ShadowRedundancyPerformanceCounters.perfCounters.TotalSmtpTimeouts.RawValue = ShadowRedundancyPerformanceCounters.totalSmtpTimeoutsCounter.AddValue(1L);
			}
		}

		// Token: 0x060027D9 RID: 10201 RVA: 0x0009C458 File Offset: 0x0009A658
		public void SmtpClientFailureAfterAccept()
		{
			if (ShadowRedundancyPerformanceCounters.perfCounters != null)
			{
				ShadowRedundancyPerformanceCounters.perfCounters.ClientAckFailureCount.RawValue = ShadowRedundancyPerformanceCounters.clientFailureAfterAcceptCounter.AddValue(1L);
			}
		}

		// Token: 0x060027DA RID: 10202 RVA: 0x0009C47C File Offset: 0x0009A67C
		public void MessageShadowed(string shadowServer, bool remote)
		{
			if (ShadowRedundancyPerformanceCounters.perfCounters != null)
			{
				double value = ShadowRedundancyPerformanceCounters.localRemoteShadowPercentageCounter.Add(remote ? 0L : 1L, 1L);
				ShadowRedundancyPerformanceCounters.perfCounters.LocalSiteShadowPercentage.RawValue = Convert.ToInt64(value);
			}
			ShadowRedundancyPerformanceCounters.shadowedMessageCounters.Get(shadowServer).Increment();
		}

		// Token: 0x060027DB RID: 10203 RVA: 0x0009C4CC File Offset: 0x0009A6CC
		private void RefreshSlidingCounters()
		{
			if (ShadowRedundancyPerformanceCounters.perfCounters != null)
			{
				ShadowRedundancyPerformanceCounters.perfCounters.TotalSmtpTimeouts.RawValue = ShadowRedundancyPerformanceCounters.totalSmtpTimeoutsCounter.AddValue(0L);
				ShadowRedundancyPerformanceCounters.perfCounters.ClientAckFailureCount.RawValue = ShadowRedundancyPerformanceCounters.clientFailureAfterAcceptCounter.AddValue(0L);
				ShadowRedundancyPerformanceCounters.perfCounters.MessagesFailedToBeMadeRedundant.RawValue = ShadowRedundancyPerformanceCounters.failureToMakeMessageRedundantCounter.AddValue(0L);
				ShadowRedundancyPerformanceCounters.perfCounters.LocalSiteShadowPercentage.RawValue = this.GetCounterValue(ShadowRedundancyPerformanceCounters.localRemoteShadowPercentageCounter);
				ShadowRedundancyPerformanceCounters.perfCounters.MessagesFailedToBeMadeRedundantPercentage.RawValue = this.GetCounterValue(ShadowRedundancyPerformanceCounters.messagesFailedToBeMadeRedundantPercentageCounter);
			}
		}

		// Token: 0x060027DC RID: 10204 RVA: 0x0009C568 File Offset: 0x0009A768
		private long GetCounterValue(SlidingPercentageCounter counter)
		{
			double slidingPercentage = counter.GetSlidingPercentage();
			if (counter.Denominator == 0L)
			{
				return 0L;
			}
			return Convert.ToInt64(slidingPercentage);
		}

		// Token: 0x0400142F RID: 5167
		private const string PerfCounterInstanceName = "_total";

		// Token: 0x04001430 RID: 5168
		private static readonly TimeSpan slidingWindowLength = TimeSpan.FromMinutes(15.0);

		// Token: 0x04001431 RID: 5169
		private static readonly TimeSpan bucketLength = TimeSpan.FromSeconds(10.0);

		// Token: 0x04001432 RID: 5170
		private static readonly AutoReadThroughCache<string, ExPerformanceCounter> shadowQueueLengthCounters = new AutoReadThroughCache<string, ExPerformanceCounter>((string name) => ShadowRedundancyInstancePerfCounters.GetInstance(name).ShadowQueueLength);

		// Token: 0x04001433 RID: 5171
		private static readonly AutoReadThroughCache<string, ExPerformanceCounter> shadowHeartbeatLatencyCounters = new AutoReadThroughCache<string, ExPerformanceCounter>((string name) => ShadowRedundancyInstancePerfCounters.GetInstance(name).ShadowHeartbeatLatencyAverageTime);

		// Token: 0x04001434 RID: 5172
		private static readonly AutoReadThroughCache<string, ExPerformanceCounter> shadowHeartbeatLatencyBaseCounters = new AutoReadThroughCache<string, ExPerformanceCounter>((string name) => ShadowRedundancyInstancePerfCounters.GetInstance(name).ShadowHeartbeatLatencyAverageTimeBase);

		// Token: 0x04001435 RID: 5173
		private static readonly AutoReadThroughCache<string, ExPerformanceCounter> shadowFailureCounters = new AutoReadThroughCache<string, ExPerformanceCounter>((string name) => ShadowRedundancyInstancePerfCounters.GetInstance(name).ShadowFailureCount);

		// Token: 0x04001436 RID: 5174
		private static readonly AutoReadThroughCache<string, ExPerformanceCounter> heartbeatFailureCounters = new AutoReadThroughCache<string, ExPerformanceCounter>((string name) => ShadowRedundancyInstancePerfCounters.GetInstance(name).HeartbeatFailureCount);

		// Token: 0x04001437 RID: 5175
		private static readonly AutoReadThroughCache<string, ExPerformanceCounter> shadowedMessageCounters = new AutoReadThroughCache<string, ExPerformanceCounter>((string name) => ShadowRedundancyInstancePerfCounters.GetInstance(name).ShadowedMessageCount);

		// Token: 0x04001438 RID: 5176
		private static readonly AutoReadThroughCache<string, ExPerformanceCounter> resubmittedMessageCounters = new AutoReadThroughCache<string, ExPerformanceCounter>((string name) => ShadowRedundancyInstancePerfCounters.GetInstance(name).ResubmittedMessageCount);

		// Token: 0x04001439 RID: 5177
		private static readonly SlidingTotalCounter totalSmtpTimeoutsCounter = new SlidingTotalCounter(ShadowRedundancyPerformanceCounters.slidingWindowLength, ShadowRedundancyPerformanceCounters.bucketLength);

		// Token: 0x0400143A RID: 5178
		private static readonly SlidingTotalCounter clientFailureAfterAcceptCounter = new SlidingTotalCounter(ShadowRedundancyPerformanceCounters.slidingWindowLength, ShadowRedundancyPerformanceCounters.bucketLength);

		// Token: 0x0400143B RID: 5179
		private static readonly SlidingTotalCounter failureToMakeMessageRedundantCounter = new SlidingTotalCounter(ShadowRedundancyPerformanceCounters.slidingWindowLength, ShadowRedundancyPerformanceCounters.bucketLength);

		// Token: 0x0400143C RID: 5180
		private static readonly SlidingPercentageCounter localRemoteShadowPercentageCounter = new SlidingPercentageCounter(ShadowRedundancyPerformanceCounters.slidingWindowLength, ShadowRedundancyPerformanceCounters.bucketLength);

		// Token: 0x0400143D RID: 5181
		private static readonly SlidingPercentageCounter messagesFailedToBeMadeRedundantPercentageCounter = new SlidingPercentageCounter(ShadowRedundancyPerformanceCounters.slidingWindowLength, ShadowRedundancyPerformanceCounters.bucketLength);

		// Token: 0x0400143E RID: 5182
		private static ShadowRedundancyPerfCountersInstance perfCounters = ShadowRedundancyPerfCounters.GetInstance("_total");

		// Token: 0x0400143F RID: 5183
		private static QueuingPerfCountersInstance queuingPerfCounters = QueueManager.GetTotalPerfCounters();

		// Token: 0x04001440 RID: 5184
		private ExPerformanceCounter[] performanceCounters;

		// Token: 0x04001441 RID: 5185
		private GuardedTimer refreshSlidingCountersTimer;
	}
}
