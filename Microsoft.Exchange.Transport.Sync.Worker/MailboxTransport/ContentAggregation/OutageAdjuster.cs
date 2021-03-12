using System;
using Microsoft.Exchange.Diagnostics;
using Microsoft.Exchange.Transport.Sync.Common;
using Microsoft.Exchange.Transport.Sync.Common.Logging;
using Microsoft.Exchange.Transport.Sync.Common.Subscription;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x02000209 RID: 521
	[ClassAccessLevel(AccessLevel.Implementation)]
	internal class OutageAdjuster
	{
		// Token: 0x060011B0 RID: 4528 RVA: 0x00039BC4 File Offset: 0x00037DC4
		public void Execute(ISyncWorkerData subscription, DateTime? previousSyncTime, TimeSpan outageDetectionThreshold, SyncLogSession syncLogSession, Trace tracer, string machineName, Guid databaseGuid)
		{
			SyncUtilities.ThrowIfArgumentNull("subscription", subscription);
			SyncUtilities.ThrowIfArgumentNull("syncLogSession", syncLogSession);
			SyncUtilities.ThrowIfArgumentNull("tracer", tracer);
			SyncUtilities.ThrowIfArgumentNullOrEmpty("machineName", machineName);
			SyncUtilities.ThrowIfGuidEmpty("databaseGuid", databaseGuid);
			if (this.WasSubscriptionReactivated(subscription, previousSyncTime))
			{
				subscription.AdjustedLastSuccessfulSyncTime = subscription.LastSyncTime.Value;
				return;
			}
			TimeSpan timeSpan;
			if (!this.IsSyncAfterOutage(subscription, previousSyncTime, outageDetectionThreshold, out timeSpan))
			{
				return;
			}
			subscription.AppendOutageDetectionDiagnostics(machineName, databaseGuid, outageDetectionThreshold, timeSpan);
			syncLogSession.LogDebugging((TSLID)1306UL, tracer, "Outage Detected. Database: {0}, Threshold: {1}, Observed Duration: {2}", new object[]
			{
				databaseGuid,
				outageDetectionThreshold,
				timeSpan
			});
			subscription.AdjustedLastSuccessfulSyncTime += timeSpan;
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x00039C94 File Offset: 0x00037E94
		private bool WasSubscriptionReactivated(ISyncWorkerData subscription, DateTime? previousSyncTime)
		{
			if (previousSyncTime == null)
			{
				return subscription.AdjustedLastSuccessfulSyncTime > subscription.CreationTime;
			}
			return subscription.AdjustedLastSuccessfulSyncTime > previousSyncTime.Value;
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x00039CC4 File Offset: 0x00037EC4
		private bool IsSyncAfterOutage(ISyncWorkerData subscription, DateTime? previousSyncTime, TimeSpan outageDetectionThreshold, out TimeSpan timeSinceLastSync)
		{
			if (previousSyncTime != null)
			{
				timeSinceLastSync = subscription.LastSyncTime.Value - previousSyncTime.Value;
			}
			else
			{
				timeSinceLastSync = subscription.LastSyncTime.Value - subscription.CreationTime;
			}
			return timeSinceLastSync >= outageDetectionThreshold;
		}
	}
}
