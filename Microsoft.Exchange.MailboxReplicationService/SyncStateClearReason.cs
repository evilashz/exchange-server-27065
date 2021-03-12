using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200004F RID: 79
	public enum SyncStateClearReason
	{
		// Token: 0x040001C9 RID: 457
		MoveComplete = 1,
		// Token: 0x040001CA RID: 458
		MailboxSignatureChange,
		// Token: 0x040001CB RID: 459
		CleanupOrphanedMailbox,
		// Token: 0x040001CC RID: 460
		DeleteReplica,
		// Token: 0x040001CD RID: 461
		JobCanceled,
		// Token: 0x040001CE RID: 462
		MergeComplete
	}
}
