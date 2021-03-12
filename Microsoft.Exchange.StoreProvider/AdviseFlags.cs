using System;

namespace Microsoft.Mapi
{
	// Token: 0x02000004 RID: 4
	[Flags]
	internal enum AdviseFlags
	{
		// Token: 0x04000003 RID: 3
		CriticalError = 1,
		// Token: 0x04000004 RID: 4
		NewMail = 2,
		// Token: 0x04000005 RID: 5
		ObjectCreated = 4,
		// Token: 0x04000006 RID: 6
		ObjectDeleted = 8,
		// Token: 0x04000007 RID: 7
		ObjectModified = 16,
		// Token: 0x04000008 RID: 8
		ObjectMoved = 32,
		// Token: 0x04000009 RID: 9
		ObjectCopied = 64,
		// Token: 0x0400000A RID: 10
		SearchComplete = 128,
		// Token: 0x0400000B RID: 11
		TableModified = 256,
		// Token: 0x0400000C RID: 12
		StatusObjectModified = 512,
		// Token: 0x0400000D RID: 13
		MailSubmitted = 1024,
		// Token: 0x0400000E RID: 14
		ConnectionDropped = 2048,
		// Token: 0x0400000F RID: 15
		Extended = -2147483648
	}
}
