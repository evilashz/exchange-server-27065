using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x0200002F RID: 47
	[Flags]
	public enum MailboxCopierFlags
	{
		// Token: 0x040000DD RID: 221
		None = 0,
		// Token: 0x040000DE RID: 222
		CrossOrg = 1,
		// Token: 0x040000DF RID: 223
		Merge = 2,
		// Token: 0x040000E0 RID: 224
		PublicFolderMigration = 4,
		// Token: 0x040000E1 RID: 225
		SourceIsArchive = 8,
		// Token: 0x040000E2 RID: 226
		TargetIsArchive = 16,
		// Token: 0x040000E3 RID: 227
		SourceIsPST = 32,
		// Token: 0x040000E4 RID: 228
		TargetIsPST = 64,
		// Token: 0x040000E5 RID: 229
		Root = 128,
		// Token: 0x040000E6 RID: 230
		Imap = 256,
		// Token: 0x040000E7 RID: 231
		Pop = 512,
		// Token: 0x040000E8 RID: 232
		Eas = 1024,
		// Token: 0x040000E9 RID: 233
		ContainerAggregated = 2048,
		// Token: 0x040000EA RID: 234
		ContainerOrg = 4096,
		// Token: 0x040000EB RID: 235
		Olc = 8192
	}
}
