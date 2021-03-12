using System;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000237 RID: 567
	[Flags]
	public enum ElcMailboxFlags
	{
		// Token: 0x04000D26 RID: 3366
		None = 0,
		// Token: 0x04000D27 RID: 3367
		ExpirationSuspended = 1,
		// Token: 0x04000D28 RID: 3368
		ElcV2 = 2,
		// Token: 0x04000D29 RID: 3369
		DisableCalendarLogging = 4,
		// Token: 0x04000D2A RID: 3370
		LitigationHold = 8,
		// Token: 0x04000D2B RID: 3371
		SingleItemRecovery = 16,
		// Token: 0x04000D2C RID: 3372
		ValidArchiveDatabase = 32,
		// Token: 0x04000D2D RID: 3373
		ShouldUseDefaultRetentionPolicy = 128,
		// Token: 0x04000D2E RID: 3374
		EnableSiteMailboxMessageDedup = 256
	}
}
