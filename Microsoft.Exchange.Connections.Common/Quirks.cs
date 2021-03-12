using System;

namespace Microsoft.Exchange.Connections.Common
{
	// Token: 0x0200001B RID: 27
	[Flags]
	internal enum Quirks
	{
		// Token: 0x0400005D RID: 93
		None = 0,
		// Token: 0x0400005E RID: 94
		EnumerateNativeDeletesOnly = 1,
		// Token: 0x0400005F RID: 95
		EnumerateItemChangeAsDeleteAndAdd = 2,
		// Token: 0x04000060 RID: 96
		ApplyItemChangeAsDeleteAndAdd = 4,
		// Token: 0x04000061 RID: 97
		OnlyDeleteFoldersIfNoSubFolders = 8,
		// Token: 0x04000062 RID: 98
		AllowDirectCloudFolderUpdates = 32,
		// Token: 0x04000063 RID: 99
		DoNotTerminateSlowSyncs = 64
	}
}
