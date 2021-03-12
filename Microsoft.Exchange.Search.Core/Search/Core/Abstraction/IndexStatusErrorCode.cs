using System;

namespace Microsoft.Exchange.Search.Core.Abstraction
{
	// Token: 0x0200002F RID: 47
	internal enum IndexStatusErrorCode
	{
		// Token: 0x04000036 RID: 54
		Unknown,
		// Token: 0x04000037 RID: 55
		Success,
		// Token: 0x04000038 RID: 56
		InternalError,
		// Token: 0x04000039 RID: 57
		CrawlingDatabase,
		// Token: 0x0400003A RID: 58
		DatabaseOffline,
		// Token: 0x0400003B RID: 59
		MapiNetworkError,
		// Token: 0x0400003C RID: 60
		CatalogCorruption,
		// Token: 0x0400003D RID: 61
		SeedingCatalog,
		// Token: 0x0400003E RID: 62
		CatalogSuspended,
		// Token: 0x0400003F RID: 63
		CatalogReseed,
		// Token: 0x04000040 RID: 64
		IndexNotEnabled,
		// Token: 0x04000041 RID: 65
		CatalogExcluded,
		// Token: 0x04000042 RID: 66
		ActivationPreferenceSkipped,
		// Token: 0x04000043 RID: 67
		LagCopySkipped,
		// Token: 0x04000044 RID: 68
		RecoveryDatabaseSkipped,
		// Token: 0x04000045 RID: 69
		FastError,
		// Token: 0x04000046 RID: 70
		ServiceNotRunning,
		// Token: 0x04000047 RID: 71
		IndexStatusTimestampTooOld,
		// Token: 0x04000048 RID: 72
		CatalogCorruptionWhenFeedingStarts,
		// Token: 0x04000049 RID: 73
		CatalogCorruptionWhenFeedingCompletes,
		// Token: 0x0400004A RID: 74
		EventsMissingWithNotificationsWatermark,
		// Token: 0x0400004B RID: 75
		CrawlOnNonPreferredActiveWithNotificationsWatermark,
		// Token: 0x0400004C RID: 76
		CrawlOnNonPreferredActiveWithTooManyNotificationEvents,
		// Token: 0x0400004D RID: 77
		CrawlOnPassive
	}
}
