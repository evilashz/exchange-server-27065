using System;

namespace Microsoft.Exchange.Transport.Sync.Migration.Rpc
{
	// Token: 0x02000011 RID: 17
	internal enum MigrationServiceRpcMethodCode
	{
		// Token: 0x04000066 RID: 102
		None = 1,
		// Token: 0x04000067 RID: 103
		CreateSyncSubscription,
		// Token: 0x04000068 RID: 104
		UpdateSyncSubscription,
		// Token: 0x04000069 RID: 105
		GetSyncSubscriptionState,
		// Token: 0x0400006A RID: 106
		RegisterMigrationBatch,
		// Token: 0x0400006B RID: 107
		SubscriptionStatusChanged
	}
}
