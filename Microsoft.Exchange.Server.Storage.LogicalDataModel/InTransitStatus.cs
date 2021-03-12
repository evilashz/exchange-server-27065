using System;

namespace Microsoft.Exchange.Server.Storage.LogicalDataModel
{
	// Token: 0x0200006B RID: 107
	[Flags]
	public enum InTransitStatus
	{
		// Token: 0x040003FC RID: 1020
		NotInTransit = 0,
		// Token: 0x040003FD RID: 1021
		SourceOfMove = 1,
		// Token: 0x040003FE RID: 1022
		DestinationOfMove = 2,
		// Token: 0x040003FF RID: 1023
		DirectionMask = 15,
		// Token: 0x04000400 RID: 1024
		OnlineMove = 16,
		// Token: 0x04000401 RID: 1025
		AllowLargeItem = 32,
		// Token: 0x04000402 RID: 1026
		ForPublicFolderMove = 64,
		// Token: 0x04000403 RID: 1027
		ControlMask = -16,
		// Token: 0x04000404 RID: 1028
		KnownControlFlags = 112
	}
}
