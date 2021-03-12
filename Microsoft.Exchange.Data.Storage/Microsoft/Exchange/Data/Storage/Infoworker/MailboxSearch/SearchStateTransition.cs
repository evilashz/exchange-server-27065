using System;

namespace Microsoft.Exchange.Data.Storage.Infoworker.MailboxSearch
{
	// Token: 0x02000D1D RID: 3357
	internal enum SearchStateTransition
	{
		// Token: 0x04005104 RID: 20740
		StartSearch,
		// Token: 0x04005105 RID: 20741
		DeleteSearch,
		// Token: 0x04005106 RID: 20742
		StopSearch,
		// Token: 0x04005107 RID: 20743
		ResetSearch,
		// Token: 0x04005108 RID: 20744
		MoveToNextState,
		// Token: 0x04005109 RID: 20745
		MoveToNextPartialSuccessState,
		// Token: 0x0400510A RID: 20746
		Fail
	}
}
