using System;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x020000DF RID: 223
	public enum NotificationPublishPhase : uint
	{
		// Token: 0x04000510 RID: 1296
		PreCommit = 2U,
		// Token: 0x04000511 RID: 1297
		PostCommit = 4U,
		// Token: 0x04000512 RID: 1298
		Pumping = 8U
	}
}
