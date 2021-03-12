using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.ExchangeSystem;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Exceptions;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;
using Microsoft.Exchange.Transport.Sync.Worker.Framework;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x02000049 RID: 73
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal sealed class FrameworkPerfCounterHandler
	{
		// Token: 0x0600034C RID: 844 RVA: 0x0000FA24 File Offset: 0x0000DC24
		private FrameworkPerfCounterHandler()
		{
			this.pop3MessageBytesReceivedTotal = MSExchangePop3Aggregation.MessageBytesReceivedTotal;
			this.pop3MessagesReceivedTotal = MSExchangePop3Aggregation.MessagesReceivedTotal;
			this.deltaSyncTotalBytesDownloaded = MSExchangeDeltaSyncAggregation.TotalBytesDownloaded;
			this.deltaSyncTotalBytesUploaded = MSExchangeDeltaSyncAggregation.TotalBytesUploaded;
			this.deltaSyncTotalMessagesDownloaded = MSExchangeDeltaSyncAggregation.TotalMessagesDownloaded;
			this.deltaSyncTotalMessagesUploaded = MSExchangeDeltaSyncAggregation.TotalMessagesUploaded;
			this.imapSyncTotalBytesDownloaded = MSExchangeImapAggregation.TotalBytesDownloaded;
			this.imapSyncTotalBytesUploaded = MSExchangeImapAggregation.TotalBytesUploaded;
			this.imapSyncTotalMessagesDownloaded = MSExchangeImapAggregation.TotalMessagesDownloaded;
			this.imapSyncTotalMessagesUploaded = MSExchangeImapAggregation.TotalMessagesUploaded;
			this.successfulSyncs = TransportSyncWorkerFramework.SuccessfulSyncs;
			this.failedSyncs = TransportSyncWorkerFramework.FailedSyncs;
			this.canceledSyncs = TransportSyncWorkerFramework.CanceledSyncs;
			this.temporaryFailedSyncs = TransportSyncWorkerFramework.TemporaryFailedSyncs;
			this.outstandingSyncs = TransportSyncWorkerFramework.OutstandingSyncs;
			this.averageSyncTime = TransportSyncWorkerFramework.AverageSyncTime;
			this.averageSyncTimeBase = TransportSyncWorkerFramework.AverageSyncTimeBase;
			this.lastSyncTime = TransportSyncWorkerFramework.LastSyncTime;
			this.successfulDeletes = TransportSyncWorkerFramework.SuccessfulDeletes;
			this.failedDeletes = TransportSyncWorkerFramework.FailedDeletes;
			this.outstandingDeletes = TransportSyncWorkerFramework.OutstandingDeletes;
			this.averageDeleteTime = TransportSyncWorkerFramework.AverageDeleteTime;
			this.averageDeleteTimeBase = TransportSyncWorkerFramework.AverageDeleteTimeBase;
			this.lastDeleteTime = TransportSyncWorkerFramework.LastDeleteTime;
			this.totalInstanceForPeopleConnectionCounters = this.GetPeopleConnectionCounterInstance("All");
			this.facebookPeopleConnectionCounter = this.GetPeopleConnectionCounterInstance(AggregationSubscriptionType.Facebook.ToString());
			this.linkedInPeopleConnectionCounter = this.GetPeopleConnectionCounterInstance(AggregationSubscriptionType.LinkedIn.ToString());
			this.ResetCounters();
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x0600034D RID: 845 RVA: 0x0000FB88 File Offset: 0x0000DD88
		internal static FrameworkPerfCounterHandler Instance
		{
			get
			{
				if (FrameworkPerfCounterHandler.instance == null)
				{
					lock (FrameworkPerfCounterHandler.instanceInitializationLock)
					{
						if (FrameworkPerfCounterHandler.instance == null)
						{
							FrameworkPerfCounterHandler.instance = new FrameworkPerfCounterHandler();
						}
					}
				}
				return FrameworkPerfCounterHandler.instance;
			}
		}

		// Token: 0x0600034E RID: 846 RVA: 0x0000FBE0 File Offset: 0x0000DDE0
		internal void OnDeltaSyncDownloadCompletion(object sender, DownloadCompleteEventArgs e)
		{
			this.deltaSyncTotalBytesDownloaded.IncrementBy(e.BytesDownloaded);
			this.deltaSyncTotalBytesUploaded.IncrementBy(e.BytesUploaded);
			PerfCounterHandler.Instance.OnDownloadCompletion(sender, e);
		}

		// Token: 0x0600034F RID: 847 RVA: 0x0000FC12 File Offset: 0x0000DE12
		internal void OnDeltaSyncMessageDownloadCompletion(object sender, EventArgs e)
		{
			this.deltaSyncTotalMessagesDownloaded.Increment();
		}

		// Token: 0x06000350 RID: 848 RVA: 0x0000FC20 File Offset: 0x0000DE20
		internal void OnDeltaSyncMessageUploadCompletion(object sender, EventArgs e)
		{
			this.deltaSyncTotalMessagesUploaded.Increment();
		}

		// Token: 0x06000351 RID: 849 RVA: 0x0000FC2E File Offset: 0x0000DE2E
		internal void OnImapSyncDownloadCompletion(object sender, DownloadCompleteEventArgs e)
		{
			this.imapSyncTotalBytesDownloaded.IncrementBy(e.BytesDownloaded);
			this.imapSyncTotalBytesUploaded.IncrementBy(e.BytesUploaded);
			PerfCounterHandler.Instance.OnDownloadCompletion(sender, e);
		}

		// Token: 0x06000352 RID: 850 RVA: 0x0000FC60 File Offset: 0x0000DE60
		internal void OnImapSyncMessageDownloadCompletion(object sender, EventArgs e)
		{
			this.imapSyncTotalMessagesDownloaded.Increment();
		}

		// Token: 0x06000353 RID: 851 RVA: 0x0000FC6E File Offset: 0x0000DE6E
		internal void OnImapSyncMessageUploadCompletion(object sender, EventArgs e)
		{
			this.imapSyncTotalMessagesUploaded.Increment();
		}

		// Token: 0x06000354 RID: 852 RVA: 0x0000FC7C File Offset: 0x0000DE7C
		internal void OnPop3RetrieveMessageCompletion(object sender, DownloadCompleteEventArgs e)
		{
			PerfCounterHandler.Instance.OnDownloadCompletion(sender, e);
			this.pop3MessagesReceivedTotal.Increment();
			this.pop3MessageBytesReceivedTotal.IncrementBy(e.BytesDownloaded);
		}

		// Token: 0x06000355 RID: 853 RVA: 0x0000FCA8 File Offset: 0x0000DEA8
		internal void OnSyncStarted()
		{
			this.outstandingSyncs.Increment();
		}

		// Token: 0x06000356 RID: 854 RVA: 0x0000FCB8 File Offset: 0x0000DEB8
		internal void OnSyncCompletion(AsyncOperationResult<SyncEngineResultData> result)
		{
			this.outstandingSyncs.Decrement();
			if (result.Exception == null)
			{
				TimeSpan timeSpan = ExDateTime.UtcNow - result.Data.StartSyncTime;
				this.averageSyncTime.IncrementBy(timeSpan.Ticks);
				this.averageSyncTimeBase.Increment();
				this.lastSyncTime.RawValue = timeSpan.Ticks / 10000L;
				this.successfulSyncs.Increment();
				return;
			}
			if (result.Exception is OperationCanceledException)
			{
				this.canceledSyncs.Increment();
				return;
			}
			if (result.Exception is SyncPermanentException)
			{
				this.failedSyncs.Increment();
				return;
			}
			if (result.Exception is SyncTransientException)
			{
				this.temporaryFailedSyncs.Increment();
			}
		}

		// Token: 0x06000357 RID: 855 RVA: 0x0000FD81 File Offset: 0x0000DF81
		internal void OnFacebookSyncDownloadCompletion(object sender, DownloadCompleteEventArgs e)
		{
			this.facebookPeopleConnectionCounter.TotalBytesDownloaded.IncrementBy(e.BytesDownloaded);
			this.totalInstanceForPeopleConnectionCounters.TotalBytesDownloaded.IncrementBy(e.BytesDownloaded);
		}

		// Token: 0x06000358 RID: 856 RVA: 0x0000FDB1 File Offset: 0x0000DFB1
		internal void OnFacebookContactDownload(object sender, EventArgs e)
		{
			this.facebookPeopleConnectionCounter.TotalContactsDownloaded.Increment();
			this.totalInstanceForPeopleConnectionCounters.TotalContactsDownloaded.Increment();
		}

		// Token: 0x06000359 RID: 857 RVA: 0x0000FDD5 File Offset: 0x0000DFD5
		internal void OnFacebookRequest(object sender, EventArgs e)
		{
			this.facebookPeopleConnectionCounter.TotalRequests.Increment();
			this.totalInstanceForPeopleConnectionCounters.TotalRequests.Increment();
		}

		// Token: 0x0600035A RID: 858 RVA: 0x0000FDF9 File Offset: 0x0000DFF9
		internal void OnFacebookRequestWithNoChanges(object sender, EventArgs e)
		{
			this.facebookPeopleConnectionCounter.TotalRequestsWithNoChanges.Increment();
			this.totalInstanceForPeopleConnectionCounters.TotalRequestsWithNoChanges.Increment();
		}

		// Token: 0x0600035B RID: 859 RVA: 0x0000FE1D File Offset: 0x0000E01D
		internal void OnLinkedInSyncDownloadCompletion(object sender, DownloadCompleteEventArgs e)
		{
			this.linkedInPeopleConnectionCounter.TotalBytesDownloaded.IncrementBy(e.BytesDownloaded);
			this.totalInstanceForPeopleConnectionCounters.TotalBytesDownloaded.IncrementBy(e.BytesDownloaded);
		}

		// Token: 0x0600035C RID: 860 RVA: 0x0000FE4D File Offset: 0x0000E04D
		internal void OnLinkedInContactDownload()
		{
			this.linkedInPeopleConnectionCounter.TotalContactsDownloaded.Increment();
			this.totalInstanceForPeopleConnectionCounters.TotalContactsDownloaded.Increment();
		}

		// Token: 0x0600035D RID: 861 RVA: 0x0000FE71 File Offset: 0x0000E071
		internal void OnLinkedInRequest()
		{
			this.linkedInPeopleConnectionCounter.TotalRequests.Increment();
			this.totalInstanceForPeopleConnectionCounters.TotalRequests.Increment();
		}

		// Token: 0x0600035E RID: 862 RVA: 0x0000FE95 File Offset: 0x0000E095
		internal void OnDeleteStarted()
		{
			this.outstandingDeletes.Increment();
		}

		// Token: 0x0600035F RID: 863 RVA: 0x0000FEA4 File Offset: 0x0000E0A4
		internal void OnDeleteCompletion(AsyncOperationResult<SyncEngineResultData> result)
		{
			this.outstandingDeletes.Decrement();
			if (result.Exception == null)
			{
				TimeSpan timeSpan = ExDateTime.UtcNow - result.Data.StartSyncTime;
				this.averageDeleteTime.IncrementBy(timeSpan.Ticks);
				this.averageDeleteTimeBase.Increment();
				this.lastDeleteTime.RawValue = timeSpan.Ticks / 10000L;
				this.successfulDeletes.Increment();
				return;
			}
			if (result.Exception is FailedDeletePeopleConnectSubscriptionException)
			{
				this.failedDeletes.Increment();
			}
		}

		// Token: 0x06000360 RID: 864 RVA: 0x0000FF3C File Offset: 0x0000E13C
		private void ResetCounters()
		{
			this.pop3MessageBytesReceivedTotal.RawValue = 0L;
			this.pop3MessagesReceivedTotal.RawValue = 0L;
			this.deltaSyncTotalBytesDownloaded.RawValue = 0L;
			this.deltaSyncTotalBytesUploaded.RawValue = 0L;
			this.deltaSyncTotalMessagesDownloaded.RawValue = 0L;
			this.deltaSyncTotalMessagesUploaded.RawValue = 0L;
			this.imapSyncTotalBytesDownloaded.RawValue = 0L;
			this.imapSyncTotalBytesUploaded.RawValue = 0L;
			this.imapSyncTotalMessagesDownloaded.RawValue = 0L;
			this.imapSyncTotalMessagesUploaded.RawValue = 0L;
			this.successfulSyncs.RawValue = 0L;
			this.failedSyncs.RawValue = 0L;
			this.canceledSyncs.RawValue = 0L;
			this.temporaryFailedSyncs.RawValue = 0L;
			this.outstandingSyncs.RawValue = 0L;
			this.averageSyncTime.RawValue = 0L;
			this.averageSyncTimeBase.RawValue = 0L;
			this.lastSyncTime.RawValue = 0L;
			this.successfulDeletes.RawValue = 0L;
			this.failedDeletes.RawValue = 0L;
			this.outstandingDeletes.RawValue = 0L;
			this.averageDeleteTime.RawValue = 0L;
			this.averageDeleteTimeBase.RawValue = 0L;
			this.lastDeleteTime.RawValue = 0L;
		}

		// Token: 0x06000361 RID: 865 RVA: 0x00010081 File Offset: 0x0000E281
		private MSExchangePeopleConnectionInstance GetPeopleConnectionCounterInstance(string instanceName)
		{
			MSExchangePeopleConnection.ResetInstance(instanceName);
			return MSExchangePeopleConnection.GetInstance(instanceName);
		}

		// Token: 0x040001C1 RID: 449
		private const string TotalInstanceName = "All";

		// Token: 0x040001C2 RID: 450
		private static object instanceInitializationLock = new object();

		// Token: 0x040001C3 RID: 451
		private static FrameworkPerfCounterHandler instance;

		// Token: 0x040001C4 RID: 452
		private ExPerformanceCounter pop3MessageBytesReceivedTotal;

		// Token: 0x040001C5 RID: 453
		private ExPerformanceCounter pop3MessagesReceivedTotal;

		// Token: 0x040001C6 RID: 454
		private ExPerformanceCounter deltaSyncTotalBytesDownloaded;

		// Token: 0x040001C7 RID: 455
		private ExPerformanceCounter deltaSyncTotalBytesUploaded;

		// Token: 0x040001C8 RID: 456
		private ExPerformanceCounter deltaSyncTotalMessagesDownloaded;

		// Token: 0x040001C9 RID: 457
		private ExPerformanceCounter deltaSyncTotalMessagesUploaded;

		// Token: 0x040001CA RID: 458
		private ExPerformanceCounter imapSyncTotalBytesDownloaded;

		// Token: 0x040001CB RID: 459
		private ExPerformanceCounter imapSyncTotalBytesUploaded;

		// Token: 0x040001CC RID: 460
		private ExPerformanceCounter imapSyncTotalMessagesDownloaded;

		// Token: 0x040001CD RID: 461
		private ExPerformanceCounter imapSyncTotalMessagesUploaded;

		// Token: 0x040001CE RID: 462
		private MSExchangePeopleConnectionInstance totalInstanceForPeopleConnectionCounters;

		// Token: 0x040001CF RID: 463
		private MSExchangePeopleConnectionInstance facebookPeopleConnectionCounter;

		// Token: 0x040001D0 RID: 464
		private MSExchangePeopleConnectionInstance linkedInPeopleConnectionCounter;

		// Token: 0x040001D1 RID: 465
		private ExPerformanceCounter successfulSyncs;

		// Token: 0x040001D2 RID: 466
		private ExPerformanceCounter failedSyncs;

		// Token: 0x040001D3 RID: 467
		private ExPerformanceCounter canceledSyncs;

		// Token: 0x040001D4 RID: 468
		private ExPerformanceCounter temporaryFailedSyncs;

		// Token: 0x040001D5 RID: 469
		private ExPerformanceCounter outstandingSyncs;

		// Token: 0x040001D6 RID: 470
		private ExPerformanceCounter averageSyncTime;

		// Token: 0x040001D7 RID: 471
		private ExPerformanceCounter averageSyncTimeBase;

		// Token: 0x040001D8 RID: 472
		private ExPerformanceCounter lastSyncTime;

		// Token: 0x040001D9 RID: 473
		private ExPerformanceCounter successfulDeletes;

		// Token: 0x040001DA RID: 474
		private ExPerformanceCounter failedDeletes;

		// Token: 0x040001DB RID: 475
		private ExPerformanceCounter outstandingDeletes;

		// Token: 0x040001DC RID: 476
		private ExPerformanceCounter averageDeleteTime;

		// Token: 0x040001DD RID: 477
		private ExPerformanceCounter averageDeleteTimeBase;

		// Token: 0x040001DE RID: 478
		private ExPerformanceCounter lastDeleteTime;
	}
}
