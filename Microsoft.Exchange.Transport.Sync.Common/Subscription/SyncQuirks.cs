using System;

namespace Microsoft.Exchange.Transport.Sync.Common.Subscription
{
	// Token: 0x020000CF RID: 207
	[Flags]
	internal enum SyncQuirks
	{
		// Token: 0x0400034C RID: 844
		None = 0,
		// Token: 0x0400034D RID: 845
		EnumerateNativeDeletesOnly = 1,
		// Token: 0x0400034E RID: 846
		EnumerateItemChangeAsDeleteAndAdd = 2,
		// Token: 0x0400034F RID: 847
		ApplyItemChangeAsDeleteAndAdd = 4,
		// Token: 0x04000350 RID: 848
		OnlyDeleteFoldersIfNoSubFolders = 8,
		// Token: 0x04000351 RID: 849
		AllowDirectCloudFolderUpdates = 32,
		// Token: 0x04000352 RID: 850
		DoNotTerminateSlowSyncs = 64
	}
}
