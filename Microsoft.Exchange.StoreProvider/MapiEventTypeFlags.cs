using System;

namespace Microsoft.Mapi
{
	// Token: 0x0200008C RID: 140
	[Flags]
	internal enum MapiEventTypeFlags
	{
		// Token: 0x0400056A RID: 1386
		CriticalError = 1,
		// Token: 0x0400056B RID: 1387
		NewMail = 2,
		// Token: 0x0400056C RID: 1388
		ObjectCreated = 4,
		// Token: 0x0400056D RID: 1389
		ObjectDeleted = 8,
		// Token: 0x0400056E RID: 1390
		ObjectModified = 16,
		// Token: 0x0400056F RID: 1391
		ObjectMoved = 32,
		// Token: 0x04000570 RID: 1392
		ObjectCopied = 64,
		// Token: 0x04000571 RID: 1393
		SearchComplete = 128,
		// Token: 0x04000572 RID: 1394
		TableModified = 256,
		// Token: 0x04000573 RID: 1395
		StatusObjectModified = 512,
		// Token: 0x04000574 RID: 1396
		MailSubmitted = 1024,
		// Token: 0x04000575 RID: 1397
		Extended = -2147483648,
		// Token: 0x04000576 RID: 1398
		MailboxCreated = 2048,
		// Token: 0x04000577 RID: 1399
		MailboxDeleted = 4096,
		// Token: 0x04000578 RID: 1400
		MailboxDisconnected = 8192,
		// Token: 0x04000579 RID: 1401
		MailboxReconnected = 16384,
		// Token: 0x0400057A RID: 1402
		MailboxMoveStarted = 32768,
		// Token: 0x0400057B RID: 1403
		MailboxMoveSucceeded = 65536,
		// Token: 0x0400057C RID: 1404
		MailboxMoveFailed = 131072
	}
}
