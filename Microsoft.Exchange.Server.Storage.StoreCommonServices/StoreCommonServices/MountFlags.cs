using System;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000054 RID: 84
	[Flags]
	public enum MountFlags : uint
	{
		// Token: 0x040002A8 RID: 680
		None = 0U,
		// Token: 0x040002A9 RID: 681
		ForceDatabaseCreation = 1U,
		// Token: 0x040002AA RID: 682
		AllowDatabasePatch = 2U,
		// Token: 0x040002AB RID: 683
		AcceptDataLoss = 4U,
		// Token: 0x040002AC RID: 684
		LogReplay = 8U
	}
}
