using System;

namespace Microsoft.Exchange.Transport.Sync.Common.Rpc.Completion
{
	// Token: 0x0200009E RID: 158
	internal enum SubscriptionCompletionStatus
	{
		// Token: 0x04000234 RID: 564
		NoError,
		// Token: 0x04000235 RID: 565
		SyncError,
		// Token: 0x04000236 RID: 566
		DisableSubscription,
		// Token: 0x04000237 RID: 567
		HubShutdown = 4,
		// Token: 0x04000238 RID: 568
		InvalidState,
		// Token: 0x04000239 RID: 569
		DeleteSubscription
	}
}
