using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Threading;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Manager.Throttling;

namespace Microsoft.Exchange.Transport.Sync.Manager
{
	// Token: 0x02000037 RID: 55
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class ManagerPerfCounterHandler
	{
		// Token: 0x060002BB RID: 699 RVA: 0x000133A4 File Offset: 0x000115A4
		private ManagerPerfCounterHandler()
		{
			this.expiryGranularity = TimeSpan.FromSeconds(ContentAggregationConfig.PCExpiryInterval.TotalSeconds / (double)ContentAggregationConfig.SLAExpiryBuckets);
			this.maxProcessingTimeInSeconds = (long)TimeSpan.FromHours(1.0).TotalSeconds;
			this.typeToString = new Dictionary<AggregationSubscriptionType, string>(4);
			this.processingTimePercentileCounters = new Dictionary<AggregationSubscriptionType, PercentileCounter>(4);
			this.subscriptionsCompletingSyncCounters = new Dictionary<AggregationSubscriptionType, SlidingPercentageCounter>(4);
			this.slaSecondsToString = new Dictionary<long, string>(3);
			this.slaPercentileCounters = new Dictionary<long, PercentileCounter>(3);
			this.totalInstanceForProtocolCounters = MsExchangeTransportSyncManagerByProtocolPerf.GetInstance("All");
			MsExchangeTransportSyncManagerByProtocolPerf.ResetInstance("All");
			this.typeToString.Add(AggregationSubscriptionType.All, "All");
			SlidingPercentageCounter value = new SlidingPercentageCounter(ContentAggregationConfig.PCExpiryInterval, this.expiryGranularity);
			this.subscriptionsCompletingSyncCounters.Add(AggregationSubscriptionType.All, value);
			this.InitializeMultiInstanceCounter(AggregationSubscriptionType.DeltaSyncMail);
			this.InitializeMultiInstanceCounter(AggregationSubscriptionType.IMAP);
			this.InitializeMultiInstanceCounter(AggregationSubscriptionType.Facebook);
			this.InitializeMultiInstanceCounter(AggregationSubscriptionType.Pop);
			this.InitializeMultiInstanceCounter(AggregationSubscriptionType.LinkedIn);
			this.GetSlaPercentileCounter(ContentAggregationConfig.AggregationInitialSyncInterval);
			this.GetSlaPercentileCounter(ContentAggregationConfig.AggregationIncrementalSyncInterval);
			this.GetSlaPercentileCounter(ContentAggregationConfig.MigrationInitialSyncInterval);
			this.GetSlaPercentileCounter(ContentAggregationConfig.MigrationIncrementalSyncInterval);
			this.databaseGuidToString = new Dictionary<Guid, string>(30);
			this.syncIntervalToString = new Dictionary<TimeSpan, string>(5);
			this.totalInstanceForSubscriptionCounters = MsExchangeTransportSyncManagerByDatabasePerf.GetInstance("All");
			MsExchangeTransportSyncManagerByDatabasePerf.ResetInstance("All");
			this.ResetSingleInstanceCounters();
		}

		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x060002BC RID: 700 RVA: 0x0001351C File Offset: 0x0001171C
		public static ManagerPerfCounterHandler Instance
		{
			get
			{
				if (ManagerPerfCounterHandler.instance == null)
				{
					lock (ManagerPerfCounterHandler.InitializationLock)
					{
						if (ManagerPerfCounterHandler.instance == null)
						{
							ManagerPerfCounterHandler.instance = new ManagerPerfCounterHandler();
						}
					}
				}
				return ManagerPerfCounterHandler.instance;
			}
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00013574 File Offset: 0x00011774
		public void StartUpdatingCounters()
		{
			if (this.timer == null)
			{
				this.timer = new GuardedTimer(new TimerCallback(this.UpdateCounters), null, ContentAggregationConfig.SLAPerfCounterUpdateInterval);
			}
		}

		// Token: 0x060002BE RID: 702 RVA: 0x0001359B File Offset: 0x0001179B
		public void StopUpdatingCounters()
		{
			if (this.timer != null)
			{
				this.timer.Dispose(true);
				this.timer = null;
			}
		}

		// Token: 0x060002BF RID: 703 RVA: 0x000135B8 File Offset: 0x000117B8
		internal void SetWaitToGetSubscriptionsCacheToken(long valueInMilliSeconds)
		{
			MsExchangeTransportSyncManagerPerf.LastWaitToGetSubscriptionsCacheToken.RawValue = valueInMilliSeconds;
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x000135C5 File Offset: 0x000117C5
		internal void IncrementMailboxesInSubscriptionCachesBy(long countOfNewMailboxes)
		{
			MsExchangeTransportSyncManagerPerf.TotalNumberOfMailboxesInSubscriptionCaches.IncrementBy(countOfNewMailboxes);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x000135D3 File Offset: 0x000117D3
		internal void IncrementCacheMessagesRebuilt()
		{
			MsExchangeTransportSyncManagerPerf.TotalNumberOfMailboxesRebuiltInSubscriptionCaches.Increment();
		}

		// Token: 0x060002C2 RID: 706 RVA: 0x000135E0 File Offset: 0x000117E0
		internal void IncrementCacheMessagesRebuildRepaired()
		{
			MsExchangeTransportSyncManagerPerf.TotalNumberOfMailboxesRepairRebuiltInSubscriptionCaches.Increment();
		}

		// Token: 0x060002C3 RID: 707 RVA: 0x000135ED File Offset: 0x000117ED
		internal void IncrementMailboxesToBeRebuilt()
		{
			MsExchangeTransportSyncManagerPerf.TotalNumberOfMailboxesToBeRebuiltInSubscriptionCaches.Increment();
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x000135FA File Offset: 0x000117FA
		internal void DecrementMailboxesToBeRebuilt()
		{
			MsExchangeTransportSyncManagerPerf.TotalNumberOfMailboxesToBeRebuiltInSubscriptionCaches.Decrement();
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x00013608 File Offset: 0x00011808
		internal void OnReportSyncQueueDispatchLagTimeEvent(object sender, SyncQueueEventArgs e)
		{
			MsExchangeTransportSyncManagerPerf.SubscriptionQueueDispatchLag.RawValue = (long)e.DispatchLagTime.TotalSeconds;
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x00013630 File Offset: 0x00011830
		internal void IncrementSubscriptionsQueued(AggregationSubscriptionType type)
		{
			MsExchangeTransportSyncManagerByProtocolPerfInstance multiInstanceCounter = this.GetMultiInstanceCounter(type);
			this.totalInstanceForProtocolCounters.SubscriptionsQueued.Increment();
			multiInstanceCounter.SubscriptionsQueued.Increment();
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x00013664 File Offset: 0x00011864
		internal void DecrementSubscriptionsQueued(AggregationSubscriptionType type)
		{
			MsExchangeTransportSyncManagerByProtocolPerfInstance multiInstanceCounter = this.GetMultiInstanceCounter(type);
			this.totalInstanceForProtocolCounters.SubscriptionsQueued.Decrement();
			multiInstanceCounter.SubscriptionsQueued.Decrement();
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x00013698 File Offset: 0x00011898
		internal void IncrementSyncNowSubscriptionsQueued(AggregationSubscriptionType type)
		{
			MsExchangeTransportSyncManagerByProtocolPerfInstance multiInstanceCounter = this.GetMultiInstanceCounter(type);
			this.totalInstanceForProtocolCounters.SyncNowSubscriptionsQueued.Increment();
			multiInstanceCounter.SyncNowSubscriptionsQueued.Increment();
			this.IncrementSubscriptionsQueued(type);
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x000136D4 File Offset: 0x000118D4
		internal void DecrementSyncNowSubscriptionsQueued(AggregationSubscriptionType type)
		{
			MsExchangeTransportSyncManagerByProtocolPerfInstance multiInstanceCounter = this.GetMultiInstanceCounter(type);
			this.totalInstanceForProtocolCounters.SyncNowSubscriptionsQueued.Decrement();
			multiInstanceCounter.SyncNowSubscriptionsQueued.Decrement();
			this.DecrementSubscriptionsQueued(type);
		}

		// Token: 0x060002CA RID: 714 RVA: 0x00013710 File Offset: 0x00011910
		internal void IncrementSuccessfulSubmissions(AggregationSubscriptionType type)
		{
			MsExchangeTransportSyncManagerByProtocolPerfInstance multiInstanceCounter = this.GetMultiInstanceCounter(type);
			this.totalInstanceForProtocolCounters.SuccessfulSubmissions.Increment();
			multiInstanceCounter.SuccessfulSubmissions.Increment();
			this.IncrementSubscriptionsDispatched(multiInstanceCounter);
			this.subscriptionsCompletingSyncCounters[type].AddDenominator(1L);
			this.subscriptionsCompletingSyncCounters[AggregationSubscriptionType.All].AddDenominator(1L);
		}

		// Token: 0x060002CB RID: 715 RVA: 0x00013778 File Offset: 0x00011978
		internal void IncrementDuplicateSubmissions(AggregationSubscriptionType type)
		{
			MsExchangeTransportSyncManagerByProtocolPerfInstance multiInstanceCounter = this.GetMultiInstanceCounter(type);
			this.totalInstanceForProtocolCounters.DuplicateSubmissions.Increment();
			multiInstanceCounter.DuplicateSubmissions.Increment();
			this.IncrementSubscriptionsDispatched(multiInstanceCounter);
		}

		// Token: 0x060002CC RID: 716 RVA: 0x000137B4 File Offset: 0x000119B4
		internal void IncrementTemporarySubmissionFailures(AggregationSubscriptionType type)
		{
			MsExchangeTransportSyncManagerByProtocolPerfInstance multiInstanceCounter = this.GetMultiInstanceCounter(type);
			this.totalInstanceForProtocolCounters.TemporarySubmissionFailures.Increment();
			multiInstanceCounter.TemporarySubmissionFailures.Increment();
			this.IncrementSubscriptionsDispatched(multiInstanceCounter);
		}

		// Token: 0x060002CD RID: 717 RVA: 0x000137F0 File Offset: 0x000119F0
		internal void IncrementAverageDispatchTimeBy(AggregationSubscriptionType type, long valueInMilliSeconds)
		{
			MsExchangeTransportSyncManagerByProtocolPerfInstance multiInstanceCounter = this.GetMultiInstanceCounter(type);
			this.totalInstanceForProtocolCounters.AverageDispatchTime.IncrementBy(valueInMilliSeconds);
			multiInstanceCounter.AverageDispatchTime.IncrementBy(valueInMilliSeconds);
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00013824 File Offset: 0x00011A24
		internal void IncrementAverageDispatchTimeBase(AggregationSubscriptionType type)
		{
			MsExchangeTransportSyncManagerByProtocolPerfInstance multiInstanceCounter = this.GetMultiInstanceCounter(type);
			this.totalInstanceForProtocolCounters.AverageDispatchTimeBase.Increment();
			multiInstanceCounter.AverageDispatchTimeBase.Increment();
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00013858 File Offset: 0x00011A58
		internal void SetTimeToCompleteLastDispatch(AggregationSubscriptionType type, long valueInMilliSeconds)
		{
			MsExchangeTransportSyncManagerByProtocolPerfInstance multiInstanceCounter = this.GetMultiInstanceCounter(type);
			multiInstanceCounter.LastDispatchTime.RawValue = valueInMilliSeconds;
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x0001387C File Offset: 0x00011A7C
		internal void SetLastSubscriptionProcessingTime(AggregationSubscriptionType type, long valueInMilliSeconds)
		{
			long value = Convert.ToInt64(valueInMilliSeconds / 1000L);
			MsExchangeTransportSyncManagerByProtocolPerfInstance multiInstanceCounter = this.GetMultiInstanceCounter(type);
			multiInstanceCounter.LastSubscriptionProcessingTime.RawValue = valueInMilliSeconds;
			this.processingTimePercentileCounters[type].AddValue(value);
			this.subscriptionsCompletingSyncCounters[type].AddNumerator(1L);
			this.subscriptionsCompletingSyncCounters[AggregationSubscriptionType.All].AddNumerator(1L);
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x000138EC File Offset: 0x00011AEC
		internal void AddSubscriptionSyncInterval(TimeSpan expectedSyncInterval, TimeSpan actualSyncInterval)
		{
			PercentileCounter slaPercentileCounter = this.GetSlaPercentileCounter(expectedSyncInterval);
			slaPercentileCounter.AddValue(Convert.ToInt64(actualSyncInterval.TotalSeconds));
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00013914 File Offset: 0x00011B14
		internal void OnSubscriptionAddedOrRemovedEvent(object sender, SyncQueueEventArgs e)
		{
			MsExchangeTransportSyncManagerByDatabasePerfInstance multiInstanceCounter = this.GetMultiInstanceCounter(e.DatabaseGuid);
			multiInstanceCounter.TotalSubscriptionsQueuedInDatabaseQueueManager.IncrementBy((long)e.NumberOfItemsChanged);
			this.totalInstanceForSubscriptionCounters.TotalSubscriptionsQueuedInDatabaseQueueManager.IncrementBy((long)e.NumberOfItemsChanged);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x0001395C File Offset: 0x00011B5C
		internal void OnSubscriptionSyncEnqueuedOrDequeuedEvent(object sender, SyncQueueEventArgs e)
		{
			MsExchangeTransportSyncManagerByDatabasePerfInstance multiInstanceCounter = this.GetMultiInstanceCounter(e.DatabaseGuid);
			multiInstanceCounter.TotalSubscriptionInstancesQueuedInDatabaseQueueManager.IncrementBy((long)e.NumberOfItemsChanged);
			MsExchangeTransportSyncManagerByDatabasePerfInstance multiInstanceCounter2 = this.GetMultiInstanceCounter(e.SyncInterval);
			multiInstanceCounter2.TotalSubscriptionInstancesQueuedInDatabaseQueueManager.IncrementBy((long)e.NumberOfItemsChanged);
			this.totalInstanceForSubscriptionCounters.TotalSubscriptionInstancesQueuedInDatabaseQueueManager.IncrementBy((long)e.NumberOfItemsChanged);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x000139C4 File Offset: 0x00011BC4
		private MsExchangeTransportSyncManagerByDatabasePerfInstance GetMultiInstanceCounter(Guid databaseGuid)
		{
			MsExchangeTransportSyncManagerByDatabasePerfInstance result = null;
			string text = null;
			lock (this.databaseGuidToString)
			{
				if (!this.databaseGuidToString.TryGetValue(databaseGuid, out text))
				{
					text = databaseGuid.ToString();
					this.databaseGuidToString.Add(databaseGuid, text);
					result = MsExchangeTransportSyncManagerByDatabasePerf.GetInstance(text);
					MsExchangeTransportSyncManagerByDatabasePerf.ResetInstance(text);
				}
				else
				{
					result = MsExchangeTransportSyncManagerByDatabasePerf.GetInstance(text);
				}
			}
			return result;
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00013A44 File Offset: 0x00011C44
		private MsExchangeTransportSyncManagerByDatabasePerfInstance GetMultiInstanceCounter(TimeSpan syncInterval)
		{
			MsExchangeTransportSyncManagerByDatabasePerfInstance result = null;
			string text = null;
			lock (this.syncIntervalToString)
			{
				if (!this.syncIntervalToString.TryGetValue(syncInterval, out text))
				{
					text = string.Format(CultureInfo.InvariantCulture, syncInterval.ToString(), new object[0]);
					this.syncIntervalToString.Add(syncInterval, text);
					result = MsExchangeTransportSyncManagerByDatabasePerf.GetInstance(text);
					MsExchangeTransportSyncManagerByDatabasePerf.ResetInstance(text);
				}
				else
				{
					result = MsExchangeTransportSyncManagerByDatabasePerf.GetInstance(text);
				}
			}
			return result;
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x00013AD4 File Offset: 0x00011CD4
		private void ResetSingleInstanceCounters()
		{
			MsExchangeTransportSyncManagerPerf.LastWaitToGetSubscriptionsCacheToken.RawValue = 0L;
			MsExchangeTransportSyncManagerPerf.SubscriptionQueueDispatchLag.RawValue = 0L;
			MsExchangeTransportSyncManagerPerf.TotalNumberOfMailboxesInSubscriptionCaches.RawValue = 0L;
			MsExchangeTransportSyncManagerPerf.TotalNumberOfMailboxesRebuiltInSubscriptionCaches.RawValue = 0L;
			MsExchangeTransportSyncManagerPerf.TotalNumberOfMailboxesRepairRebuiltInSubscriptionCaches.RawValue = 0L;
			MsExchangeTransportSyncManagerPerf.TotalNumberOfMailboxesToBeRebuiltInSubscriptionCaches.RawValue = 0L;
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00013B2C File Offset: 0x00011D2C
		private void UpdateCounters(object state)
		{
			lock (this.slaSyncObject)
			{
				foreach (KeyValuePair<long, PercentileCounter> keyValuePair in this.slaPercentileCounters)
				{
					MsExchangeTransportSyncManagerBySLAPerfInstance msExchangeTransportSyncManagerBySLAPerfInstance = MsExchangeTransportSyncManagerBySLAPerf.GetInstance(this.slaSecondsToString[keyValuePair.Key]);
					long rawValue = keyValuePair.Value.PercentileQuery(95.0);
					msExchangeTransportSyncManagerBySLAPerfInstance.SubscriptionsPollingFrequency95Percent.RawValue = rawValue;
				}
			}
			foreach (KeyValuePair<AggregationSubscriptionType, PercentileCounter> keyValuePair2 in this.processingTimePercentileCounters)
			{
				MsExchangeTransportSyncManagerByProtocolPerfInstance msExchangeTransportSyncManagerByProtocolPerfInstance = MsExchangeTransportSyncManagerByProtocolPerf.GetInstance(this.typeToString[keyValuePair2.Key]);
				long rawValue2 = keyValuePair2.Value.PercentileQuery(95.0);
				msExchangeTransportSyncManagerByProtocolPerfInstance.ProcessingTimeToSyncSubscription95Percent.RawValue = rawValue2;
			}
			foreach (KeyValuePair<AggregationSubscriptionType, SlidingPercentageCounter> keyValuePair3 in this.subscriptionsCompletingSyncCounters)
			{
				MsExchangeTransportSyncManagerByProtocolPerfInstance msExchangeTransportSyncManagerByProtocolPerfInstance2 = MsExchangeTransportSyncManagerByProtocolPerf.GetInstance(this.typeToString[keyValuePair3.Key]);
				double slidingPercentage = keyValuePair3.Value.GetSlidingPercentage();
				long rawValue3;
				if (slidingPercentage == 1.7976931348623157E+308)
				{
					rawValue3 = long.MaxValue;
				}
				else if (slidingPercentage == -1.7976931348623157E+308)
				{
					rawValue3 = long.MinValue;
				}
				else
				{
					rawValue3 = Convert.ToInt64(slidingPercentage);
				}
				msExchangeTransportSyncManagerByProtocolPerfInstance2.SubscriptionsCompletingSync.RawValue = rawValue3;
			}
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00013D0C File Offset: 0x00011F0C
		private MsExchangeTransportSyncManagerByProtocolPerfInstance GetMultiInstanceCounter(AggregationSubscriptionType type)
		{
			if (!this.typeToString.ContainsKey(type))
			{
				throw new InvalidOperationException("Unexpected aggregation type encountered for which a counter hasnt been established yet: " + type);
			}
			return MsExchangeTransportSyncManagerByProtocolPerf.GetInstance(this.typeToString[type]);
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x00013D44 File Offset: 0x00011F44
		private void InitializeMultiInstanceCounter(AggregationSubscriptionType type)
		{
			string text = type.ToString();
			this.typeToString.Add(type, text);
			MsExchangeTransportSyncManagerByProtocolPerf.GetInstance(text);
			MsExchangeTransportSyncManagerByProtocolPerf.ResetInstance(text);
			PercentileCounter value = new PercentileCounter(ContentAggregationConfig.PCExpiryInterval, this.expiryGranularity, 1L, this.maxProcessingTimeInSeconds);
			SlidingPercentageCounter value2 = new SlidingPercentageCounter(ContentAggregationConfig.PCExpiryInterval, this.expiryGranularity);
			this.processingTimePercentileCounters.Add(type, value);
			this.subscriptionsCompletingSyncCounters.Add(type, value2);
		}

		// Token: 0x060002DA RID: 730 RVA: 0x00013DBC File Offset: 0x00011FBC
		private PercentileCounter GetSlaPercentileCounter(TimeSpan syncInterval)
		{
			long num = Convert.ToInt64(syncInterval.TotalSeconds);
			PercentileCounter percentileCounter;
			lock (this.slaSyncObject)
			{
				if (!this.slaPercentileCounters.TryGetValue(num, out percentileCounter))
				{
					string text = string.Format(CultureInfo.InvariantCulture, syncInterval.ToString(), new object[0]);
					this.slaSecondsToString.Add(num, text);
					MsExchangeTransportSyncManagerBySLAPerf.GetInstance(text);
					MsExchangeTransportSyncManagerBySLAPerf.ResetInstance(text);
					long num2 = num + 7200L;
					long valueGranularity = Convert.ToInt64(num2 / (long)ContentAggregationConfig.SLADataBuckets);
					percentileCounter = new PercentileCounter(ContentAggregationConfig.PCExpiryInterval, this.expiryGranularity, valueGranularity, num2);
					this.slaPercentileCounters.Add(num, percentileCounter);
				}
			}
			return percentileCounter;
		}

		// Token: 0x060002DB RID: 731 RVA: 0x00013E8C File Offset: 0x0001208C
		private void IncrementSubscriptionsDispatched(MsExchangeTransportSyncManagerByProtocolPerfInstance perfInstance)
		{
			this.totalInstanceForProtocolCounters.SubscriptionsDispatched.Increment();
			perfInstance.SubscriptionsDispatched.Increment();
		}

		// Token: 0x0400018D RID: 397
		private const string TotalInstanceName = "All";

		// Token: 0x0400018E RID: 398
		private const int SubscriptionTypeCounters = 4;

		// Token: 0x0400018F RID: 399
		private const int SlaCounters = 3;

		// Token: 0x04000190 RID: 400
		private const int DefaultNumberOfDatabasesPerMailboxServer = 30;

		// Token: 0x04000191 RID: 401
		private const int PercentileCounterPercentage = 95;

		// Token: 0x04000192 RID: 402
		private const int ProcessingTimeDataGranularity = 1;

		// Token: 0x04000193 RID: 403
		private const int NumberOfSyncIntervals = 5;

		// Token: 0x04000194 RID: 404
		private const int TwoHoursInSecs = 7200;

		// Token: 0x04000195 RID: 405
		private static readonly object InitializationLock = new object();

		// Token: 0x04000196 RID: 406
		private readonly object slaSyncObject = new object();

		// Token: 0x04000197 RID: 407
		private readonly TimeSpan expiryGranularity;

		// Token: 0x04000198 RID: 408
		private readonly long maxProcessingTimeInSeconds;

		// Token: 0x04000199 RID: 409
		private static ManagerPerfCounterHandler instance;

		// Token: 0x0400019A RID: 410
		private MsExchangeTransportSyncManagerByDatabasePerfInstance totalInstanceForSubscriptionCounters;

		// Token: 0x0400019B RID: 411
		private Dictionary<Guid, string> databaseGuidToString;

		// Token: 0x0400019C RID: 412
		private Dictionary<TimeSpan, string> syncIntervalToString;

		// Token: 0x0400019D RID: 413
		private MsExchangeTransportSyncManagerByProtocolPerfInstance totalInstanceForProtocolCounters;

		// Token: 0x0400019E RID: 414
		private Dictionary<AggregationSubscriptionType, string> typeToString;

		// Token: 0x0400019F RID: 415
		private Dictionary<AggregationSubscriptionType, PercentileCounter> processingTimePercentileCounters;

		// Token: 0x040001A0 RID: 416
		private Dictionary<AggregationSubscriptionType, SlidingPercentageCounter> subscriptionsCompletingSyncCounters;

		// Token: 0x040001A1 RID: 417
		private Dictionary<long, string> slaSecondsToString;

		// Token: 0x040001A2 RID: 418
		private Dictionary<long, PercentileCounter> slaPercentileCounters;

		// Token: 0x040001A3 RID: 419
		private GuardedTimer timer;
	}
}
