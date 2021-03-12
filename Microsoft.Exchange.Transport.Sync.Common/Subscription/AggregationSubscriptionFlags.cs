using System;

namespace Microsoft.Exchange.Transport.Sync.Common.Subscription
{
	// Token: 0x020000B2 RID: 178
	[Flags]
	internal enum AggregationSubscriptionFlags
	{
		// Token: 0x040002C5 RID: 709
		IsMigration = 1,
		// Token: 0x040002C6 RID: 710
		IsInitialSyncDone = 16
	}
}
