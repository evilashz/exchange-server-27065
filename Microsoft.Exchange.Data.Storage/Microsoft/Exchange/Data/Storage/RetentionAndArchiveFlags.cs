using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x02000640 RID: 1600
	[Flags]
	internal enum RetentionAndArchiveFlags
	{
		// Token: 0x04002464 RID: 9316
		None = 0,
		// Token: 0x04002465 RID: 9317
		ExplicitTag = 1,
		// Token: 0x04002466 RID: 9318
		UserOverride = 2,
		// Token: 0x04002467 RID: 9319
		Autotag = 4,
		// Token: 0x04002468 RID: 9320
		PersonalTag = 8,
		// Token: 0x04002469 RID: 9321
		AllRetentionFlags = 15,
		// Token: 0x0400246A RID: 9322
		ExplictArchiveTag = 16,
		// Token: 0x0400246B RID: 9323
		KeepInPlace = 32,
		// Token: 0x0400246C RID: 9324
		AllArchiveFlags = 48,
		// Token: 0x0400246D RID: 9325
		SystemData = 64,
		// Token: 0x0400246E RID: 9326
		NeedsRescan = 128,
		// Token: 0x0400246F RID: 9327
		PendingRescan = 256,
		// Token: 0x04002470 RID: 9328
		EHAMigration = 512,
		// Token: 0x04002471 RID: 9329
		[Obsolete("This feature has been retired.  Keeping this value to prevent re-use as objects may already have this bit enabled.")]
		RescanForTags = 1024
	}
}
