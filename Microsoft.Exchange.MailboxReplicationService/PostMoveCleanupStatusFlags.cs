using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200004D RID: 77
	[Flags]
	public enum PostMoveCleanupStatusFlags
	{
		// Token: 0x040001B6 RID: 438
		None = 0,
		// Token: 0x040001B7 RID: 439
		DestinationResetInTransitStatus = 1,
		// Token: 0x040001B8 RID: 440
		DestinationSeedMBICache = 2,
		// Token: 0x040001B9 RID: 441
		SourceMailboxCleanup = 4,
		// Token: 0x040001BA RID: 442
		AddTargetMailboxDataToReport = 8,
		// Token: 0x040001BB RID: 443
		TargetMailboxCleanup = 11,
		// Token: 0x040001BC RID: 444
		SetRelatedRequestsRehome = 16,
		// Token: 0x040001BD RID: 445
		UpdateSourceMailbox = 32,
		// Token: 0x040001BE RID: 446
		All = 63
	}
}
