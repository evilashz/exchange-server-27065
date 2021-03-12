using System;

namespace Microsoft.Exchange.Transport.MessageThrottling
{
	// Token: 0x0200012F RID: 303
	internal enum MessageThrottlingReason
	{
		// Token: 0x040005C8 RID: 1480
		NotThrottled,
		// Token: 0x040005C9 RID: 1481
		IPAddressLimitExceeded,
		// Token: 0x040005CA RID: 1482
		UserLimitExceeded
	}
}
