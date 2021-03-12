using System;

namespace Microsoft.Exchange.Transport.Sync.Common.Subscription.DeltaSync
{
	// Token: 0x020000DE RID: 222
	internal enum DeltaSyncAccountStatus
	{
		// Token: 0x0400038F RID: 911
		Normal,
		// Token: 0x04000390 RID: 912
		Blocked = 256,
		// Token: 0x04000391 RID: 913
		HipRequired = 512
	}
}
