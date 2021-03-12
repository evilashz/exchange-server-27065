using System;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x0200023F RID: 575
	[Flags]
	public enum RequestFlags
	{
		// Token: 0x04000D68 RID: 3432
		None = 0,
		// Token: 0x04000D69 RID: 3433
		CrossOrg = 1,
		// Token: 0x04000D6A RID: 3434
		IntraOrg = 2,
		// Token: 0x04000D6B RID: 3435
		Push = 4,
		// Token: 0x04000D6C RID: 3436
		Pull = 8,
		// Token: 0x04000D6D RID: 3437
		Offline = 16,
		// Token: 0x04000D6E RID: 3438
		Protected = 32,
		// Token: 0x04000D6F RID: 3439
		RemoteLegacy = 64,
		// Token: 0x04000D70 RID: 3440
		HighPriority = 128,
		// Token: 0x04000D71 RID: 3441
		Suspend = 256,
		// Token: 0x04000D72 RID: 3442
		SuspendWhenReadyToComplete = 512,
		// Token: 0x04000D73 RID: 3443
		MoveOnlyPrimaryMailbox = 1024,
		// Token: 0x04000D74 RID: 3444
		MoveOnlyArchiveMailbox = 2048,
		// Token: 0x04000D75 RID: 3445
		TargetIsAggregatedMailbox = 4096,
		// Token: 0x04000D76 RID: 3446
		Join = 8192,
		// Token: 0x04000D77 RID: 3447
		Split = 16384
	}
}
