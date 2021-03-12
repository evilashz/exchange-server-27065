using System;

namespace Microsoft.Exchange.Transport.Sync.Common.Rpc.Notification
{
	// Token: 0x020000A0 RID: 160
	internal enum SubscriptionNotificationResult
	{
		// Token: 0x04000242 RID: 578
		Success,
		// Token: 0x04000243 RID: 579
		ServerVersionMismatch,
		// Token: 0x04000244 RID: 580
		ServerStopped,
		// Token: 0x04000245 RID: 581
		ProcessingFailed,
		// Token: 0x04000246 RID: 582
		PropertyBagError,
		// Token: 0x04000247 RID: 583
		UnknownMethodError
	}
}
