using System;

namespace Microsoft.Exchange.Data.Directory.Recipient
{
	// Token: 0x02000240 RID: 576
	public enum RequestStatus
	{
		// Token: 0x04000D79 RID: 3449
		[LocDescription(DirectoryStrings.IDs.MailboxMoveStatusNone)]
		None,
		// Token: 0x04000D7A RID: 3450
		[LocDescription(DirectoryStrings.IDs.MailboxMoveStatusQueued)]
		Queued,
		// Token: 0x04000D7B RID: 3451
		[LocDescription(DirectoryStrings.IDs.MailboxMoveStatusInProgress)]
		InProgress,
		// Token: 0x04000D7C RID: 3452
		[LocDescription(DirectoryStrings.IDs.MailboxMoveStatusAutoSuspended)]
		AutoSuspended,
		// Token: 0x04000D7D RID: 3453
		[LocDescription(DirectoryStrings.IDs.MailboxMoveStatusCompletionInProgress)]
		CompletionInProgress,
		// Token: 0x04000D7E RID: 3454
		[LocDescription(DirectoryStrings.IDs.MailboxMoveStatusSynced)]
		Synced,
		// Token: 0x04000D7F RID: 3455
		[LocDescription(DirectoryStrings.IDs.MailboxMoveStatusCompleted)]
		Completed = 10,
		// Token: 0x04000D80 RID: 3456
		[LocDescription(DirectoryStrings.IDs.MailboxMoveStatusCompletedWithWarning)]
		CompletedWithWarning,
		// Token: 0x04000D81 RID: 3457
		[LocDescription(DirectoryStrings.IDs.MailboxMoveStatusSuspended)]
		Suspended = 98,
		// Token: 0x04000D82 RID: 3458
		[LocDescription(DirectoryStrings.IDs.MailboxMoveStatusFailed)]
		Failed
	}
}
