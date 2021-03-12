using System;

namespace Microsoft.Exchange.Transport
{
	// Token: 0x02000341 RID: 833
	internal enum WaitConditionManagerBreadcrumbs
	{
		// Token: 0x04001296 RID: 4758
		EMPTY,
		// Token: 0x04001297 RID: 4759
		IncrementInUse,
		// Token: 0x04001298 RID: 4760
		DecrementInUse,
		// Token: 0x04001299 RID: 4761
		WaitlistNewItem,
		// Token: 0x0400129A RID: 4762
		ReactivateOlderItem,
		// Token: 0x0400129B RID: 4763
		ReactivateOlderCondition,
		// Token: 0x0400129C RID: 4764
		NewItemTokenUsed,
		// Token: 0x0400129D RID: 4765
		ReturnTokenUnused,
		// Token: 0x0400129E RID: 4766
		ConditionExceedsQuota,
		// Token: 0x0400129F RID: 4767
		ActivateWaitingItemFound,
		// Token: 0x040012A0 RID: 4768
		ActivateBlockedCondition,
		// Token: 0x040012A1 RID: 4769
		UpdatePriority,
		// Token: 0x040012A2 RID: 4770
		RemoveEmptyInUse,
		// Token: 0x040012A3 RID: 4771
		RemoveEmptyWaitlist,
		// Token: 0x040012A4 RID: 4772
		ActivateAll,
		// Token: 0x040012A5 RID: 4773
		ItemNotFoundToActivate,
		// Token: 0x040012A6 RID: 4774
		RemoveBlockedCondition,
		// Token: 0x040012A7 RID: 4775
		AddBlockedCondition,
		// Token: 0x040012A8 RID: 4776
		ExceededMaxThreads,
		// Token: 0x040012A9 RID: 4777
		CleanupItem,
		// Token: 0x040012AA RID: 4778
		CleanupQueue,
		// Token: 0x040012AB RID: 4779
		CleanupCondition,
		// Token: 0x040012AC RID: 4780
		BlockedExceedsQuota,
		// Token: 0x040012AD RID: 4781
		OlderItemFound,
		// Token: 0x040012AE RID: 4782
		AddDisabled
	}
}
