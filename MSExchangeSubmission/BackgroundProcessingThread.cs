using System;
using Microsoft.Exchange.MailboxTransport.Submission.StoreDriverSubmission;
using Microsoft.Exchange.Transport;

namespace Microsoft.Exchange.MailboxTransport.Submission
{
	// Token: 0x02000002 RID: 2
	internal class BackgroundProcessingThread : BackgroundProcessingThreadBase
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		internal BackgroundProcessingThread()
		{
			BackgroundProcessingThread.hangDetectionInterval = SubmissionConfiguration.Instance.App.HangDetectionInterval;
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020EC File Offset: 0x000002EC
		public override void Stop()
		{
			base.Stop();
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020F4 File Offset: 0x000002F4
		protected override void Run()
		{
			DateTime utcNow = DateTime.UtcNow;
			this.lastScan = utcNow;
			this.lastTimeThrottlingManagerSwept = utcNow;
			base.Run();
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000211C File Offset: 0x0000031C
		protected override void RunMain(DateTime utcNow)
		{
			if (utcNow - this.lastHangDetection > BackgroundProcessingThread.hangDetectionInterval)
			{
				MailboxTransportSubmissionService.DetectSubmissionHang();
				this.lastHangDetection = utcNow;
			}
			if (utcNow - this.lastScan > BackgroundProcessingThread.SlowScanInterval)
			{
				IStoreDriverSubmission storeDriverSubmission;
				bool flag = Components.TryGetStoreDriverSubmission(out storeDriverSubmission);
				if (flag)
				{
					Components.StoreDriverSubmission.ExpireOldSubmissionConnections();
				}
				this.lastScan = utcNow;
			}
			if (utcNow - this.lastTimeThrottlingManagerSwept > BackgroundProcessingThread.FiveMinuteInterval)
			{
				Components.MessageThrottlingComponent.MessageThrottlingManager.CleanupIdleEntries();
				Components.UnhealthyTargetFilterComponent.CleanupExpiredEntries();
				this.lastTimeThrottlingManagerSwept = utcNow;
			}
			if (Components.ResourceManager.IsMonitoringEnabled && utcNow - Components.ResourceManager.LastTimeResourceMonitored > Components.ResourceManager.MonitorInterval)
			{
				Components.ResourceManager.OnMonitorResource(null);
			}
		}

		// Token: 0x04000001 RID: 1
		public static readonly TimeSpan SlowScanInterval = TimeSpan.FromSeconds(5.0);

		// Token: 0x04000002 RID: 2
		private static readonly TimeSpan DefaultHeartBeatCheckInterval = TimeSpan.FromMinutes(1.0);

		// Token: 0x04000003 RID: 3
		private static readonly TimeSpan FiveMinuteInterval = TimeSpan.FromMinutes(5.0);

		// Token: 0x04000004 RID: 4
		private static readonly DateTime SystemStartTime = DateTime.UtcNow;

		// Token: 0x04000005 RID: 5
		private static TimeSpan hangDetectionInterval = TimeSpan.FromMinutes(5.0);

		// Token: 0x04000006 RID: 6
		private DateTime lastHangDetection;

		// Token: 0x04000007 RID: 7
		private DateTime lastScan;

		// Token: 0x04000008 RID: 8
		private DateTime lastTimeThrottlingManagerSwept;
	}
}
