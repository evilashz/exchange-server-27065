using System;

namespace Microsoft.Exchange.Protocols.Smtp
{
	// Token: 0x0200043B RID: 1083
	internal enum DelayedAckStatus
	{
		// Token: 0x04001900 RID: 6400
		None,
		// Token: 0x04001901 RID: 6401
		Stamped,
		// Token: 0x04001902 RID: 6402
		ShadowRedundancyManagerNotified,
		// Token: 0x04001903 RID: 6403
		WaitingForShadowRedundancyManager
	}
}
