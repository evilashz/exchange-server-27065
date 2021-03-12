using System;

namespace Microsoft.Mapi
{
	// Token: 0x0200020B RID: 523
	[Flags]
	internal enum MailboxMiscFlags
	{
		// Token: 0x04000F2F RID: 3887
		None = 0,
		// Token: 0x04000F30 RID: 3888
		CreatedByMove = 16,
		// Token: 0x04000F31 RID: 3889
		ArchiveMailbox = 32,
		// Token: 0x04000F32 RID: 3890
		DisabledMailbox = 64,
		// Token: 0x04000F33 RID: 3891
		SoftDeletedMailbox = 128,
		// Token: 0x04000F34 RID: 3892
		MRSSoftDeletedMailbox = 256
	}
}
