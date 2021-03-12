using System;

namespace Microsoft.Exchange.Server.Storage.StoreCommonServices
{
	// Token: 0x02000053 RID: 83
	[Flags]
	internal enum MailboxMiscFlags
	{
		// Token: 0x0400029C RID: 668
		None = 0,
		// Token: 0x0400029D RID: 669
		QuotaExceeded = 1,
		// Token: 0x0400029E RID: 670
		Gateway = 2,
		// Token: 0x0400029F RID: 671
		Mailbox = 4,
		// Token: 0x040002A0 RID: 672
		SDNotInSyncWithDS = 8,
		// Token: 0x040002A1 RID: 673
		CreatedByMove = 16,
		// Token: 0x040002A2 RID: 674
		ArchiveMailbox = 32,
		// Token: 0x040002A3 RID: 675
		DisabledMailbox = 64,
		// Token: 0x040002A4 RID: 676
		SoftDeletedMailbox = 128,
		// Token: 0x040002A5 RID: 677
		MRSSoftDeletedMailbox = 256,
		// Token: 0x040002A6 RID: 678
		MRSPreservingMailboxSignature = 512
	}
}
