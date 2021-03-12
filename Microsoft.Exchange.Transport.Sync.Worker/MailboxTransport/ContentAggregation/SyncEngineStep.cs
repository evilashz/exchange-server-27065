using System;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x02000215 RID: 533
	internal enum SyncEngineStep
	{
		// Token: 0x04000A0C RID: 2572
		PreSyncStepInEnumerateChangesMode,
		// Token: 0x04000A0D RID: 2573
		PreSyncStepInCheckForChangesMode,
		// Token: 0x04000A0E RID: 2574
		AuthenticateCloudInCheckForChangesMode,
		// Token: 0x04000A0F RID: 2575
		AuthenticateCloudInEnumerateChangesMode,
		// Token: 0x04000A10 RID: 2576
		CheckForChangesInCloud,
		// Token: 0x04000A11 RID: 2577
		EnumCloud,
		// Token: 0x04000A12 RID: 2578
		ApplyNative,
		// Token: 0x04000A13 RID: 2579
		AcknowledgeCloud,
		// Token: 0x04000A14 RID: 2580
		PostSyncStepInCheckForChangesMode,
		// Token: 0x04000A15 RID: 2581
		PostSyncStepInEnumerateChangesMode,
		// Token: 0x04000A16 RID: 2582
		GetCloudStatistcsInEnumerateChangesMode,
		// Token: 0x04000A17 RID: 2583
		End
	}
}
