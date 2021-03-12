using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200004E RID: 78
	public enum SyncStateError
	{
		// Token: 0x040001C0 RID: 448
		None,
		// Token: 0x040001C1 RID: 449
		NullSyncState,
		// Token: 0x040001C2 RID: 450
		NullIcsSyncState,
		// Token: 0x040001C3 RID: 451
		WrongRequestGuid,
		// Token: 0x040001C4 RID: 452
		CorruptSyncState,
		// Token: 0x040001C5 RID: 453
		CorruptIcsSyncState,
		// Token: 0x040001C6 RID: 454
		NullReplaySyncState,
		// Token: 0x040001C7 RID: 455
		CorruptReplaySyncState
	}
}
