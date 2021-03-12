using System;

namespace Microsoft.Exchange.Transport.Sync.Common.Rpc.Cache
{
	// Token: 0x02000099 RID: 153
	[Serializable]
	internal enum SubscriptionCacheObjectState
	{
		// Token: 0x0400020D RID: 525
		Valid,
		// Token: 0x0400020E RID: 526
		Invalid,
		// Token: 0x0400020F RID: 527
		Missing,
		// Token: 0x04000210 RID: 528
		Unexpected
	}
}
