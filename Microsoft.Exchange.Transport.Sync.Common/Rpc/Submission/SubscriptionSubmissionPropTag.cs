using System;

namespace Microsoft.Exchange.Transport.Sync.Common.Rpc.Submission
{
	// Token: 0x020000A2 RID: 162
	internal enum SubscriptionSubmissionPropTag : uint
	{
		// Token: 0x04000253 RID: 595
		InArgSubmissionType = 2684354562U,
		// Token: 0x04000254 RID: 596
		InArgUserLegacyDN = 2684420127U,
		// Token: 0x04000255 RID: 597
		InArgSubscriptionMessageID = 2684485890U,
		// Token: 0x04000256 RID: 598
		InArgSubscriptionGuid = 2684551240U,
		// Token: 0x04000257 RID: 599
		InArgRecoverySync = 2684616715U,
		// Token: 0x04000258 RID: 600
		InArgDatabaseGuid = 2684682312U,
		// Token: 0x04000259 RID: 601
		InArgUserMailboxGuid = 2684747848U,
		// Token: 0x0400025A RID: 602
		InArgMailboxServer = 2684813343U,
		// Token: 0x0400025B RID: 603
		InArgTenantGuid = 2684878920U,
		// Token: 0x0400025C RID: 604
		InArgAggregationType = 2684944386U,
		// Token: 0x0400025D RID: 605
		InArgInitialSync = 2685009931U,
		// Token: 0x0400025E RID: 606
		InArgSubscription = 2685075714U,
		// Token: 0x0400025F RID: 607
		InArgIsSyncNow = 2685141003U,
		// Token: 0x04000260 RID: 608
		InArgSyncWatermark = 2685206559U,
		// Token: 0x04000261 RID: 609
		InArgMailboxServerGuid = 2685272136U,
		// Token: 0x04000262 RID: 610
		InArgSyncPhase = 2685337602U,
		// Token: 0x04000263 RID: 611
		OutArgErrorCode = 2835349507U
	}
}
