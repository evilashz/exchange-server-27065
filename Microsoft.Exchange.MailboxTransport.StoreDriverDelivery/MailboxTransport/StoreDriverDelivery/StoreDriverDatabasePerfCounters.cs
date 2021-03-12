using System;
using Microsoft.Exchange.Collections;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Diagnostics.Components.StoreDriverDelivery;

namespace Microsoft.Exchange.MailboxTransport.StoreDriverDelivery
{
	// Token: 0x02000016 RID: 22
	internal static class StoreDriverDatabasePerfCounters
	{
		// Token: 0x06000183 RID: 387 RVA: 0x00008A08 File Offset: 0x00006C08
		public static void IncrementDeliveryAttempt(string mdbGuid, bool calculateOnly = false)
		{
			StoreDriverDatabasePerfCounters.InstanceEntry instanceEntry = StoreDriverDatabasePerfCounters.GetInstanceEntry(mdbGuid);
			if (instanceEntry != null)
			{
				if (!calculateOnly)
				{
					instanceEntry.DeliveryAttemptsCounter.AddValue(1L);
				}
				lock (MSExchangeStoreDriverDatabase.TotalInstance.DeliveryAttempts)
				{
					instanceEntry.PerfCounterInstance.DeliveryAttempts.RawValue = instanceEntry.DeliveryAttemptsCounter.CalculateAverage();
				}
			}
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00008A7C File Offset: 0x00006C7C
		public static void IncrementDeliveryFailure(string mdbGuid, bool calculateOnly = false)
		{
			StoreDriverDatabasePerfCounters.InstanceEntry instanceEntry = StoreDriverDatabasePerfCounters.GetInstanceEntry(mdbGuid);
			if (instanceEntry != null)
			{
				if (!calculateOnly)
				{
					instanceEntry.DeliveryFailuresCounter.AddValue(1L);
				}
				lock (MSExchangeStoreDriverDatabase.TotalInstance.DeliveryFailures)
				{
					instanceEntry.PerfCounterInstance.DeliveryFailures.RawValue = instanceEntry.DeliveryFailuresCounter.CalculateAverage();
				}
			}
		}

		// Token: 0x06000185 RID: 389 RVA: 0x00008AF0 File Offset: 0x00006CF0
		public static void AddDeliveryThreadSample(string mdbGuid, long sample)
		{
			StoreDriverDatabasePerfCounters.InstanceEntry instanceEntry = StoreDriverDatabasePerfCounters.GetInstanceEntry(mdbGuid);
			if (instanceEntry != null)
			{
				instanceEntry.DeliveryThreadsCounter.AddValue(sample);
				lock (MSExchangeStoreDriverDatabase.TotalInstance.CurrentDeliveryThreadsPerMdb)
				{
					instanceEntry.PerfCounterInstance.CurrentDeliveryThreadsPerMdb.RawValue = instanceEntry.DeliveryThreadsCounter.CalculateAverage();
				}
			}
		}

		// Token: 0x06000186 RID: 390 RVA: 0x00008B60 File Offset: 0x00006D60
		public static void RefreshPerformanceCounters()
		{
			foreach (string mdbGuid in StoreDriverDatabasePerfCounters.PerfCountersDictionary.Keys)
			{
				StoreDriverDatabasePerfCounters.IncrementDeliveryAttempt(mdbGuid, true);
				StoreDriverDatabasePerfCounters.IncrementDeliveryFailure(mdbGuid, true);
			}
		}

		// Token: 0x06000187 RID: 391 RVA: 0x00008BB8 File Offset: 0x00006DB8
		private static StoreDriverDatabasePerfCounters.InstanceEntry GetInstanceEntry(string mdbGuid)
		{
			if (string.IsNullOrEmpty(mdbGuid))
			{
				return null;
			}
			return StoreDriverDatabasePerfCounters.PerfCountersDictionary.AddIfNotExists(mdbGuid, new SynchronizedDictionary<string, StoreDriverDatabasePerfCounters.InstanceEntry>.CreationalMethod(StoreDriverDatabasePerfCounters.CreateInstanceEntry));
		}

		// Token: 0x06000188 RID: 392 RVA: 0x00008BDC File Offset: 0x00006DDC
		private static StoreDriverDatabasePerfCounters.InstanceEntry CreateInstanceEntry(string mdbGuid)
		{
			MSExchangeStoreDriverDatabaseInstance msexchangeStoreDriverDatabaseInstance = null;
			try
			{
				if (mdbGuid != null)
				{
					msexchangeStoreDriverDatabaseInstance = MSExchangeStoreDriverDatabase.GetInstance(mdbGuid);
				}
			}
			catch (InvalidOperationException arg)
			{
				StoreDriverDatabasePerfCounters.Diag.TraceError<string, InvalidOperationException>(0L, "Get StoreDriver PerfCounters Instance {0} failed due to: {1}", mdbGuid, arg);
			}
			if (msexchangeStoreDriverDatabaseInstance == null)
			{
				return null;
			}
			return new StoreDriverDatabasePerfCounters.InstanceEntry(msexchangeStoreDriverDatabaseInstance);
		}

		// Token: 0x0400009A RID: 154
		private static readonly Trace Diag = ExTraceGlobals.MapiDeliverTracer;

		// Token: 0x0400009B RID: 155
		private static readonly SynchronizedDictionary<string, StoreDriverDatabasePerfCounters.InstanceEntry> PerfCountersDictionary = new SynchronizedDictionary<string, StoreDriverDatabasePerfCounters.InstanceEntry>(100, StringComparer.OrdinalIgnoreCase);

		// Token: 0x0400009C RID: 156
		private static readonly TimeSpan SlidingWindowLength = TimeSpan.FromMinutes(5.0);

		// Token: 0x0400009D RID: 157
		private static readonly TimeSpan SlidingBucketLength = TimeSpan.FromMinutes(1.0);

		// Token: 0x0400009E RID: 158
		private static readonly TimeSpan SlidingSequenceWindowLength = TimeSpan.FromMinutes(1.0);

		// Token: 0x0400009F RID: 159
		private static readonly TimeSpan SlidingSequenceBucketLength = TimeSpan.FromSeconds(2.0);

		// Token: 0x02000017 RID: 23
		private class InstanceEntry
		{
			// Token: 0x0600018A RID: 394 RVA: 0x00008C9C File Offset: 0x00006E9C
			internal InstanceEntry(MSExchangeStoreDriverDatabaseInstance perfCounterInstance)
			{
				this.PerfCounterInstance = perfCounterInstance;
				this.DeliveryAttemptsCounter = new SlidingAverageCounter(StoreDriverDatabasePerfCounters.SlidingWindowLength, StoreDriverDatabasePerfCounters.SlidingBucketLength);
				this.DeliveryFailuresCounter = new SlidingAverageCounter(StoreDriverDatabasePerfCounters.SlidingWindowLength, StoreDriverDatabasePerfCounters.SlidingBucketLength);
				this.DeliveryThreadsCounter = new AverageSlidingSequence(StoreDriverDatabasePerfCounters.SlidingSequenceWindowLength, StoreDriverDatabasePerfCounters.SlidingSequenceBucketLength);
			}

			// Token: 0x1700008F RID: 143
			// (get) Token: 0x0600018B RID: 395 RVA: 0x00008CF5 File Offset: 0x00006EF5
			// (set) Token: 0x0600018C RID: 396 RVA: 0x00008CFD File Offset: 0x00006EFD
			internal MSExchangeStoreDriverDatabaseInstance PerfCounterInstance { get; private set; }

			// Token: 0x17000090 RID: 144
			// (get) Token: 0x0600018D RID: 397 RVA: 0x00008D06 File Offset: 0x00006F06
			// (set) Token: 0x0600018E RID: 398 RVA: 0x00008D0E File Offset: 0x00006F0E
			internal SlidingAverageCounter DeliveryAttemptsCounter { get; private set; }

			// Token: 0x17000091 RID: 145
			// (get) Token: 0x0600018F RID: 399 RVA: 0x00008D17 File Offset: 0x00006F17
			// (set) Token: 0x06000190 RID: 400 RVA: 0x00008D1F File Offset: 0x00006F1F
			internal SlidingAverageCounter DeliveryFailuresCounter { get; private set; }

			// Token: 0x17000092 RID: 146
			// (get) Token: 0x06000191 RID: 401 RVA: 0x00008D28 File Offset: 0x00006F28
			// (set) Token: 0x06000192 RID: 402 RVA: 0x00008D30 File Offset: 0x00006F30
			internal AverageSlidingSequence DeliveryThreadsCounter { get; private set; }
		}
	}
}
