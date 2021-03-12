using System;

namespace Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch
{
	// Token: 0x02000D1E RID: 3358
	public enum SearchState
	{
		// Token: 0x0400510C RID: 20748
		[LocDescription(ServerStrings.IDs.SearchStateInProgress)]
		InProgress,
		// Token: 0x0400510D RID: 20749
		[LocDescription(ServerStrings.IDs.SearchStateFailed)]
		Failed,
		// Token: 0x0400510E RID: 20750
		[LocDescription(ServerStrings.IDs.SearchStateStopping)]
		Stopping,
		// Token: 0x0400510F RID: 20751
		[LocDescription(ServerStrings.IDs.SearchStateStopped)]
		Stopped,
		// Token: 0x04005110 RID: 20752
		[LocDescription(ServerStrings.IDs.SearchStateSucceeded)]
		Succeeded,
		// Token: 0x04005111 RID: 20753
		[LocDescription(ServerStrings.IDs.SearchStatePartiallySucceeded)]
		PartiallySucceeded,
		// Token: 0x04005112 RID: 20754
		[LocDescription(ServerStrings.IDs.EstimateStateInProgress)]
		EstimateInProgress,
		// Token: 0x04005113 RID: 20755
		[LocDescription(ServerStrings.IDs.EstimateStateFailed)]
		EstimateFailed,
		// Token: 0x04005114 RID: 20756
		[LocDescription(ServerStrings.IDs.EstimateStateStopping)]
		EstimateStopping,
		// Token: 0x04005115 RID: 20757
		[LocDescription(ServerStrings.IDs.EstimateStateStopped)]
		EstimateStopped,
		// Token: 0x04005116 RID: 20758
		[LocDescription(ServerStrings.IDs.EstimateStateSucceeded)]
		EstimateSucceeded,
		// Token: 0x04005117 RID: 20759
		[LocDescription(ServerStrings.IDs.EstimateStatePartiallySucceeded)]
		EstimatePartiallySucceeded,
		// Token: 0x04005118 RID: 20760
		[LocDescription(ServerStrings.IDs.SearchStateNotStarted)]
		NotStarted,
		// Token: 0x04005119 RID: 20761
		[LocDescription(ServerStrings.IDs.SearchStateQueued)]
		Queued,
		// Token: 0x0400511A RID: 20762
		[LocDescription(ServerStrings.IDs.SearchStateDeletionInProgress)]
		DeletionInProgress
	}
}
