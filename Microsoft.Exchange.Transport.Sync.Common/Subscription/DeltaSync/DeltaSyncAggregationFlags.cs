using System;

namespace Microsoft.Exchange.Transport.Sync.Common.Subscription.DeltaSync
{
	// Token: 0x020000DF RID: 223
	[Flags]
	internal enum DeltaSyncAggregationFlags
	{
		// Token: 0x04000393 RID: 915
		AggregationTypeMask = 48,
		// Token: 0x04000394 RID: 916
		AccountStatusBlocked = 256,
		// Token: 0x04000395 RID: 917
		AccountStatusHipped = 512,
		// Token: 0x04000396 RID: 918
		AccountStatusMask = 768
	}
}
