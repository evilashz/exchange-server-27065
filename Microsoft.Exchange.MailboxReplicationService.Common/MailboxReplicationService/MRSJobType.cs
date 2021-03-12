using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000203 RID: 515
	public enum MRSJobType
	{
		// Token: 0x04000A95 RID: 2709
		Unknown = -1,
		// Token: 0x04000A96 RID: 2710
		RequestJobE14R3,
		// Token: 0x04000A97 RID: 2711
		RequestJobE14R4_WithGuid,
		// Token: 0x04000A98 RID: 2712
		RequestJobE14R4_WithArchive,
		// Token: 0x04000A99 RID: 2713
		RequestJobE14R4_WithPush,
		// Token: 0x04000A9A RID: 2714
		RequestJobE14R4_WithSuspend,
		// Token: 0x04000A9B RID: 2715
		RequestJobE14R4_WithDurations,
		// Token: 0x04000A9C RID: 2716
		RequestJobE14R5_WithImportExportMerge,
		// Token: 0x04000A9D RID: 2717
		RequestJobE14R5_PrimaryOrArchiveExclusiveMoves,
		// Token: 0x04000A9E RID: 2718
		RequestJobE14R6_CompressedReports,
		// Token: 0x04000A9F RID: 2719
		RequestJobE15_TenantHint,
		// Token: 0x04000AA0 RID: 2720
		RequestJobE15_AutoResume,
		// Token: 0x04000AA1 RID: 2721
		RequestJobE15_SubType,
		// Token: 0x04000AA2 RID: 2722
		RequestJobE15_AutoResumeMerges,
		// Token: 0x04000AA3 RID: 2723
		RequestJobE15_CreatePublicFoldersUnderParentInSecondary
	}
}
