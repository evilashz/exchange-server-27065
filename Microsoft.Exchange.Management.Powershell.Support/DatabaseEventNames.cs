using System;

namespace Microsoft.Exchange.Management.Powershell.Support
{
	// Token: 0x0200002F RID: 47
	[Flags]
	public enum DatabaseEventNames
	{
		// Token: 0x040000D3 RID: 211
		NewMail = 2,
		// Token: 0x040000D4 RID: 212
		ObjectCreated = 4,
		// Token: 0x040000D5 RID: 213
		ObjectDeleted = 8,
		// Token: 0x040000D6 RID: 214
		ObjectModified = 16,
		// Token: 0x040000D7 RID: 215
		ObjectMoved = 32,
		// Token: 0x040000D8 RID: 216
		ObjectCopied = 16,
		// Token: 0x040000D9 RID: 217
		MailSubmitted = 1024,
		// Token: 0x040000DA RID: 218
		MailboxCreated = 2048,
		// Token: 0x040000DB RID: 219
		MailboxDeleted = 4096,
		// Token: 0x040000DC RID: 220
		MailboxDisconnected = 8192,
		// Token: 0x040000DD RID: 221
		MailboxReconnected = 16384,
		// Token: 0x040000DE RID: 222
		MailboxMoveStarted = 32768,
		// Token: 0x040000DF RID: 223
		MailboxMoveSucceeded = 65536,
		// Token: 0x040000E0 RID: 224
		MailboxMoveFailed = 131072
	}
}
