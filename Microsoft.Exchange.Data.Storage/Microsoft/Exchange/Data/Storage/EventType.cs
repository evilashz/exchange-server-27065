using System;

namespace Microsoft.Exchange.Data.Storage
{
	// Token: 0x020001E9 RID: 489
	[Flags]
	internal enum EventType
	{
		// Token: 0x04000D9A RID: 3482
		None = 0,
		// Token: 0x04000D9B RID: 3483
		NewMail = 1,
		// Token: 0x04000D9C RID: 3484
		ObjectCreated = 2,
		// Token: 0x04000D9D RID: 3485
		ObjectDeleted = 4,
		// Token: 0x04000D9E RID: 3486
		ObjectModified = 8,
		// Token: 0x04000D9F RID: 3487
		ObjectMoved = 16,
		// Token: 0x04000DA0 RID: 3488
		ObjectCopied = 32,
		// Token: 0x04000DA1 RID: 3489
		FreeBusyChanged = 64,
		// Token: 0x04000DA2 RID: 3490
		CriticalError = 256,
		// Token: 0x04000DA3 RID: 3491
		MailboxDeleted = 512,
		// Token: 0x04000DA4 RID: 3492
		MailboxDisconnected = 1024,
		// Token: 0x04000DA5 RID: 3493
		MailboxMoveFailed = 2048,
		// Token: 0x04000DA6 RID: 3494
		MailboxMoveStarted = 4096,
		// Token: 0x04000DA7 RID: 3495
		MailboxMoveSucceeded = 8192
	}
}
