using System;

namespace Microsoft.Exchange.MessagingPolicies.UnJournalAgent
{
	// Token: 0x0200001B RID: 27
	internal enum ProcessingStatus
	{
		// Token: 0x040000C7 RID: 199
		NotDone,
		// Token: 0x040000C8 RID: 200
		PermanentError,
		// Token: 0x040000C9 RID: 201
		TransientError,
		// Token: 0x040000CA RID: 202
		UnwrapProcessSuccess,
		// Token: 0x040000CB RID: 203
		NdrProcessSuccess,
		// Token: 0x040000CC RID: 204
		LegacyArchiveJournallingDisabled,
		// Token: 0x040000CD RID: 205
		NonJournalMsgFromLegacyArchiveCustomer,
		// Token: 0x040000CE RID: 206
		AlreadyProcessed,
		// Token: 0x040000CF RID: 207
		DropJournalReportWithoutNdr,
		// Token: 0x040000D0 RID: 208
		NoUsersResolved,
		// Token: 0x040000D1 RID: 209
		NdrJournalReport
	}
}
