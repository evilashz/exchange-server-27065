using System;

namespace Microsoft.Mapi
{
	// Token: 0x0200020A RID: 522
	[Flags]
	internal enum InTransitStatus
	{
		// Token: 0x04000F26 RID: 3878
		NotInTransit = 0,
		// Token: 0x04000F27 RID: 3879
		MoveSource = 1,
		// Token: 0x04000F28 RID: 3880
		MoveDestination = 2,
		// Token: 0x04000F29 RID: 3881
		OnlineMove = 16,
		// Token: 0x04000F2A RID: 3882
		AllowLargeItems = 32,
		// Token: 0x04000F2B RID: 3883
		ForPublicFolderMove = 64,
		// Token: 0x04000F2C RID: 3884
		SyncSource = 17,
		// Token: 0x04000F2D RID: 3885
		SyncDestination = 18
	}
}
