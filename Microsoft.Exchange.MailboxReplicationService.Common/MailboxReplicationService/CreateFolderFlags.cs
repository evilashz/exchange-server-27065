using System;

namespace Microsoft.Exchange.MailboxReplicationService
{
	// Token: 0x02000013 RID: 19
	[Flags]
	internal enum CreateFolderFlags
	{
		// Token: 0x040000B1 RID: 177
		None = 0,
		// Token: 0x040000B2 RID: 178
		FailIfExists = 1,
		// Token: 0x040000B3 RID: 179
		CreatePublicFolderDumpster = 2,
		// Token: 0x040000B4 RID: 180
		InternalAccess = 4
	}
}
