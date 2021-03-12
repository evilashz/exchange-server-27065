using System;

namespace Microsoft.Exchange.Transport.Sync.Common.Rpc.Notification
{
	// Token: 0x020000A1 RID: 161
	internal enum SubscriptionNotificationRpcMethodCode
	{
		// Token: 0x04000249 RID: 585
		None = 1,
		// Token: 0x0400024A RID: 586
		SubscriptionAdd,
		// Token: 0x0400024B RID: 587
		SubscriptionDelete,
		// Token: 0x0400024C RID: 588
		SubscriptionSyncNowNeeded,
		// Token: 0x0400024D RID: 589
		SubscriptionUpdated,
		// Token: 0x0400024E RID: 590
		SubscriptionUpdatedAndSyncNowNeeded,
		// Token: 0x0400024F RID: 591
		OWALogonTriggeredSyncNow,
		// Token: 0x04000250 RID: 592
		OWAActivityTriggeredSyncNow,
		// Token: 0x04000251 RID: 593
		OWARefreshButtonTriggeredSyncNow
	}
}
