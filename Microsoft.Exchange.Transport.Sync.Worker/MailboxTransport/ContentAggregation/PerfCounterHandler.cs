using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Net;
using Microsoft.Exchange.Transport.Sync.Worker;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x0200000D RID: 13
	[ClassAccessLevel(AccessLevel.MSInternal)]
	internal sealed class PerfCounterHandler
	{
		// Token: 0x060000F0 RID: 240 RVA: 0x00006954 File Offset: 0x00004B54
		private PerfCounterHandler()
		{
			this.totalDownloadedBytes = TransportSyncWorkerCore.TotalDownloadedBytes;
			this.totalUploadedBytes = TransportSyncWorkerCore.TotalUploadedBytes;
			this.totalMessagesSubmittedToPipeline = TransportSyncWorkerCore.MessagesSubmittedToPipeline;
			this.totalOutstandingJobs = TransportSyncWorkerCore.OutstandingJobs;
			this.totalOutstandingJobsInRetry = TransportSyncWorkerCore.OutstandingJobsInRetry;
			this.totalSubscriptionsAggregated = TransportSyncWorkerCore.TotalSubscriptionsAggregated;
			this.ResetCounters();
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x060000F1 RID: 241 RVA: 0x000069B0 File Offset: 0x00004BB0
		public static PerfCounterHandler Instance
		{
			get
			{
				if (PerfCounterHandler.instance == null)
				{
					lock (PerfCounterHandler.instanceInitializationLock)
					{
						if (PerfCounterHandler.instance == null)
						{
							PerfCounterHandler.instance = new PerfCounterHandler();
						}
					}
				}
				return PerfCounterHandler.instance;
			}
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00006A08 File Offset: 0x00004C08
		public void OnAggregationMailSubmission(object sender, EventArgs e)
		{
			this.totalMessagesSubmittedToPipeline.Increment();
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00006A16 File Offset: 0x00004C16
		public void OnDownloadCompletion(object sender, DownloadCompleteEventArgs e)
		{
			this.totalDownloadedBytes.IncrementBy(e.BytesDownloaded);
			this.totalUploadedBytes.IncrementBy(e.BytesUploaded);
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00006A3C File Offset: 0x00004C3C
		public void OnWorkItemSubmitted(object sender, EventArgs e)
		{
			this.totalOutstandingJobs.Increment();
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00006A4A File Offset: 0x00004C4A
		public void OnWorkItemDropped(object sender, EventArgs e)
		{
			this.totalOutstandingJobs.Decrement();
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00006A58 File Offset: 0x00004C58
		public void OnWorkItemAggregated(object sender, EventArgs e)
		{
			this.totalOutstandingJobs.Decrement();
			this.totalSubscriptionsAggregated.Increment();
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00006A72 File Offset: 0x00004C72
		public void OnRetryQueueLengthChanged(object sender, RetryableWorkQueueEventArgs e)
		{
			this.totalOutstandingJobsInRetry.IncrementBy((long)e.Difference);
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00006A88 File Offset: 0x00004C88
		private void ResetCounters()
		{
			this.totalDownloadedBytes.RawValue = 0L;
			this.totalUploadedBytes.RawValue = 0L;
			this.totalMessagesSubmittedToPipeline.RawValue = 0L;
			this.totalOutstandingJobs.RawValue = 0L;
			this.totalOutstandingJobsInRetry.RawValue = 0L;
			this.totalSubscriptionsAggregated.RawValue = 0L;
		}

		// Token: 0x04000091 RID: 145
		private static object instanceInitializationLock = new object();

		// Token: 0x04000092 RID: 146
		private static PerfCounterHandler instance;

		// Token: 0x04000093 RID: 147
		private ExPerformanceCounter totalDownloadedBytes;

		// Token: 0x04000094 RID: 148
		private ExPerformanceCounter totalUploadedBytes;

		// Token: 0x04000095 RID: 149
		private ExPerformanceCounter totalMessagesSubmittedToPipeline;

		// Token: 0x04000096 RID: 150
		private ExPerformanceCounter totalOutstandingJobs;

		// Token: 0x04000097 RID: 151
		private ExPerformanceCounter totalOutstandingJobsInRetry;

		// Token: 0x04000098 RID: 152
		private ExPerformanceCounter totalSubscriptionsAggregated;
	}
}
