using System;

namespace Microsoft.Exchange.Cluster.ActiveManagerServer
{
	// Token: 0x02000093 RID: 147
	[Flags]
	public enum EnableSubComponentFlag
	{
		// Token: 0x04000251 RID: 593
		None = 0,
		// Token: 0x04000252 RID: 594
		TransientFailoverSuppressor = 1,
		// Token: 0x04000253 RID: 595
		ServiceKillStatusContainer = 2,
		// Token: 0x04000254 RID: 596
		ServerNameCacheManager = 4,
		// Token: 0x04000255 RID: 597
		DbNodeAttemptTable = 8,
		// Token: 0x04000256 RID: 598
		SystemEventQueue = 16,
		// Token: 0x04000257 RID: 599
		DatabaseQueueManager = 32,
		// Token: 0x04000258 RID: 600
		StoreStateMarker = 64,
		// Token: 0x04000259 RID: 601
		PeriodicEventManager = 128,
		// Token: 0x0400025A RID: 602
		ClusterMonitor = 256,
		// Token: 0x0400025B RID: 603
		NetworkMonitor = 512,
		// Token: 0x0400025C RID: 604
		AmPerfCounterUpdater = 1024,
		// Token: 0x0400025D RID: 605
		PamCachedLastLogUpdater = 2048,
		// Token: 0x0400025E RID: 606
		ClusdbPeriodicCleanup = 4096,
		// Token: 0x0400025F RID: 607
		DataStorePeriodicChecker = 8192,
		// Token: 0x04000260 RID: 608
		All = 16383
	}
}
