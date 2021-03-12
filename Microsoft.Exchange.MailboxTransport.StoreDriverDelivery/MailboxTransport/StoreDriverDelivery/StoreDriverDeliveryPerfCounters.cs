using System;
using Microsoft.Exchange.Diagnostics;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x02000018 RID: 24
	internal class StoreDriverDeliveryPerfCounters
	{
		// Token: 0x06000193 RID: 403 RVA: 0x00008D3C File Offset: 0x00006F3C
		private StoreDriverDeliveryPerfCounters()
		{
		}

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x06000194 RID: 404 RVA: 0x00008DFF File Offset: 0x00006FFF
		public static StoreDriverDeliveryPerfCounters Instance
		{
			get
			{
				return StoreDriverDeliveryPerfCounters.DefaultInstance;
			}
		}

		// Token: 0x06000195 RID: 405 RVA: 0x00008E06 File Offset: 0x00007006
		public static void RefreshPerformanceCounters()
		{
			StoreDriverDeliveryPerfCounters.Instance.IncrementDeliveryAttempt(true);
			StoreDriverDeliveryPerfCounters.Instance.IncrementDeliveryFailure(false, true);
			StoreDriverDeliveryPerfCounters.Instance.AddDeliveryLatencySample(TimeSpan.Zero, true);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x00008E30 File Offset: 0x00007030
		public void IncrementDeliveryAttempt(bool calculateOnly = false)
		{
			if (!calculateOnly)
			{
				this.deliveryAttemptsCounter.AddValue(1L);
			}
			lock (this.deliveryAttemptSyncObject)
			{
				MSExchangeStoreDriver.DeliveryAttempts.RawValue = this.deliveryAttemptsCounter.CalculateAverage();
				if (!calculateOnly)
				{
					this.recipientLevelPercentFailedDeliveries.AddDenominator(1L);
					this.recipientLevelPercentTemporaryFailedDeliveries.AddDenominator(1L);
				}
			}
		}

		// Token: 0x06000197 RID: 407 RVA: 0x00008EB0 File Offset: 0x000070B0
		public void IncrementDeliveryFailure(bool isPermanentFailure, bool calculateOnly = false)
		{
			if (!calculateOnly)
			{
				MSExchangeStoreDriver.FailedDeliveries.Increment();
				this.deliveryFailuresCounter.AddValue(1L);
			}
			lock (this.deliveryFailureSyncObject)
			{
				MSExchangeStoreDriver.DeliveryFailures.RawValue = this.deliveryFailuresCounter.CalculateAverage();
				if (!calculateOnly)
				{
					if (isPermanentFailure)
					{
						this.recipientLevelPercentFailedDeliveries.AddNumerator(1L);
					}
					else
					{
						this.recipientLevelPercentTemporaryFailedDeliveries.AddNumerator(1L);
					}
				}
				MSExchangeStoreDriver.RecipientLevelPercentFailedDeliveries.RawValue = (long)((int)this.recipientLevelPercentFailedDeliveries.GetSlidingPercentage());
				MSExchangeStoreDriver.RecipientLevelPercentTemporaryFailedDeliveries.RawValue = (long)((int)this.recipientLevelPercentTemporaryFailedDeliveries.GetSlidingPercentage());
			}
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00008F6C File Offset: 0x0000716C
		public void AddDeliveryLatencySample(TimeSpan latency, bool calculateOnly = false)
		{
			lock (this.deliveryLatencySyncObject)
			{
				if (!calculateOnly)
				{
					this.deliveryLatencyPerRecipient.AddValue((long)latency.Milliseconds);
				}
				MSExchangeStoreDriver.DeliveryLatencyPerRecipientMilliseconds.RawValue = this.deliveryLatencyPerRecipient.CalculateAverage();
			}
		}

		// Token: 0x040000A4 RID: 164
		private static readonly TimeSpan SlidingWindowLength = TimeSpan.FromMinutes(5.0);

		// Token: 0x040000A5 RID: 165
		private static readonly TimeSpan SlidingBucketLength = TimeSpan.FromMinutes(1.0);

		// Token: 0x040000A6 RID: 166
		private static readonly TimeSpan SlidingSequenceWindowLength = TimeSpan.FromMinutes(1.0);

		// Token: 0x040000A7 RID: 167
		private static readonly TimeSpan SlidingSequenceBucketLength = TimeSpan.FromSeconds(2.0);

		// Token: 0x040000A8 RID: 168
		private static readonly StoreDriverDeliveryPerfCounters DefaultInstance = new StoreDriverDeliveryPerfCounters();

		// Token: 0x040000A9 RID: 169
		private readonly SlidingAverageCounter deliveryAttemptsCounter = new SlidingAverageCounter(StoreDriverDeliveryPerfCounters.SlidingWindowLength, StoreDriverDeliveryPerfCounters.SlidingBucketLength);

		// Token: 0x040000AA RID: 170
		private readonly SlidingAverageCounter deliveryFailuresCounter = new SlidingAverageCounter(StoreDriverDeliveryPerfCounters.SlidingWindowLength, StoreDriverDeliveryPerfCounters.SlidingBucketLength);

		// Token: 0x040000AB RID: 171
		private readonly SlidingPercentageCounter recipientLevelPercentFailedDeliveries = new SlidingPercentageCounter(TimeSpan.FromMinutes(5.0), TimeSpan.FromSeconds(30.0), true);

		// Token: 0x040000AC RID: 172
		private readonly SlidingPercentageCounter recipientLevelPercentTemporaryFailedDeliveries = new SlidingPercentageCounter(TimeSpan.FromMinutes(5.0), TimeSpan.FromSeconds(30.0), true);

		// Token: 0x040000AD RID: 173
		private readonly AverageSlidingSequence deliveryLatencyPerRecipient = new AverageSlidingSequence(StoreDriverDeliveryPerfCounters.SlidingSequenceWindowLength, StoreDriverDeliveryPerfCounters.SlidingSequenceBucketLength);

		// Token: 0x040000AE RID: 174
		private readonly object deliveryAttemptSyncObject = new object();

		// Token: 0x040000AF RID: 175
		private readonly object deliveryFailureSyncObject = new object();

		// Token: 0x040000B0 RID: 176
		private readonly object deliveryLatencySyncObject = new object();
	}
}
