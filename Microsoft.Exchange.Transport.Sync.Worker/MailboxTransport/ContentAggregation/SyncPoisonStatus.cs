using System;

namespace Microsoft.Exchange.MailboxTransport.ContentAggregation
{
	// Token: 0x02000033 RID: 51
	internal enum SyncPoisonStatus
	{
		// Token: 0x04000163 RID: 355
		CleanSubscription,
		// Token: 0x04000164 RID: 356
		SuspectedSubscription,
		// Token: 0x04000165 RID: 357
		PoisonousItems,
		// Token: 0x04000166 RID: 358
		PoisonousSubscription
	}
}
