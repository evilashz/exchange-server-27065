using System;

namespace Microsoft.Exchange.Rpc.Cluster
{
	// Token: 0x02000140 RID: 320
	[Flags]
	internal enum SeederRpcFlags
	{
		// Token: 0x04000ABE RID: 2750
		None = 0,
		// Token: 0x04000ABF RID: 2751
		SkipSettingReseedAutoReseedState = 1,
		// Token: 0x04000AC0 RID: 2752
		CIAutoReseedReasonBehindBacklog = 2,
		// Token: 0x04000AC1 RID: 2753
		CIAutoReseedReasonBehindRetry = 4,
		// Token: 0x04000AC2 RID: 2754
		CIAutoReseedReasonFailedAndSuspended = 8,
		// Token: 0x04000AC3 RID: 2755
		CIAutoReseedReasonUpgrade = 16,
		// Token: 0x04000AC4 RID: 2756
		CatalogCorruptionWhenFeedingStarts = 32,
		// Token: 0x04000AC5 RID: 2757
		CatalogCorruptionWhenFeedingCompletes = 64,
		// Token: 0x04000AC6 RID: 2758
		EventsMissingWithNotificationsWatermark = 128,
		// Token: 0x04000AC7 RID: 2759
		CrawlOnNonPreferredActiveWithNotificationsWatermark = 256,
		// Token: 0x04000AC8 RID: 2760
		CrawlOnNonPreferredActiveWithTooManyNotificationEvents = 512,
		// Token: 0x04000AC9 RID: 2761
		CrawlOnPassive = 1024,
		// Token: 0x04000ACA RID: 2762
		Unknown = 2048
	}
}
